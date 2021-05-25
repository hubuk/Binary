// <copyright file="LogicalPath.cs" company="LeetABit">
//   Copyright (c) Hubert Bukowski. All rights reserved.
//   Licensed under the MIT License.
//   See LICENSE file in the project root for full license information.
// </copyright>

namespace LeetABit.Binary
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Runtime.Serialization;
    using System.Security;
    using LeetABit.Binary.Properties;

    /// <summary>
    ///     Represents a path to a tree like structure with a string name assigned to each node.
    /// </summary>
    public struct LogicalPath : IComparable<LogicalPath>, IComparable, IEquatable<LogicalPath>, ISerializable
    {
        /// <summary>
        ///     String that separates path nodes in the path.
        /// </summary>
        public static readonly string SeparatorString = "/";

        /// <summary>
        ///     String that represents a reference to current deriectory.
        /// </summary>
        public static readonly string CurrentPathString = ".";

        /// <summary>
        ///     String that represents a reference to parent deriectory.
        /// </summary>
        public static readonly string ParentPathString = "..";

        /// <summary>
        ///     Path to the root node.
        /// </summary>
        public static readonly LogicalPath Root;

        /// <summary>
        ///     Path to the root node.
        /// </summary>
        public static readonly LogicalPath Parent = new(ParentPathString);

        /// <summary>
        ///     Path to the root node.
        /// </summary>
        public static readonly LogicalPath Current = new(CurrentPathString);

        /// <summary>
        ///     Character that separates path nodes in the path.
        /// </summary>
        private const char SeparatorCharacter = '/';

        /// <summary>
        ///     A normalized form of the path string representation.
        /// </summary>
        private readonly string? path;

        /// <summary>
        ///     Initializes a new instance of the <see cref="LogicalPath"/> struct.
        /// </summary>
        /// <param name="path">
        ///     Path to the logical tree node.
        /// </param>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="path"/> is <see langword="null"/>.
        /// </exception>
        private LogicalPath(string path)
        {
            Requires.ArgumentNotNull(path, nameof(path));

            this.path = path;
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="LogicalPath"/> struct.
        /// </summary>
        /// <param name="info">
        ///     Serialization info.
        /// </param>
        /// <param name="context">
        ///     Streaming context.
        /// </param>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="info"/> is <see langword="null"/>.
        /// </exception>
        private LogicalPath(SerializationInfo info, StreamingContext context)
        {
            Requires.ArgumentNotNull(info, nameof(info));

            this.path = info.GetString(nameof(this.path));
        }

        /// <summary>
        ///     Gets a value indicating whether the current value represents a path that points to logical node directly from the root.
        /// </summary>
        public bool IsAbsolute
        {
            get
            {
                return this.Path.Length > 0 && this.Path[0] == SeparatorCharacter;
            }
        }

        /// <summary>
        ///     Gets a value indicating whether the current value represents a path that points to some logical node from whatever other node it is originated.
        /// </summary>
        public bool IsRelative
        {
            get
            {
                return !this.IsAbsolute;
            }
        }

        /// <summary>
        ///     Gets a value indicating whether the current value represents a root of the nodes tree.
        /// </summary>
        public bool IsRoot
        {
            get
            {
                return this.Path.Length == 1 && this.Path[0] == SeparatorCharacter;
            }
        }

        /// <summary>
        ///     Gets a value indicating whether the current value travers to parent node.
        /// </summary>
        public bool IsBackwardPath
        {
            get
            {
                return this.Path.StartsWith(ParentPathString, StringComparison.Ordinal)
                    && (this.Path.Length == ParentPathString.Length || this.Path[ParentPathString.Length] == SeparatorCharacter);
            }
        }

        /// <summary>
        ///     Gets a value that indicates number of node levels in the current path.
        /// </summary>
        public int Depth
        {
            get
            {
                return this.IsRoot ? 0 : (this.Path.Skip(1).Count(c => c == SeparatorCharacter) + 1);
            }
        }

        /// <summary>
        ///     Gets name of the node to which points current path.
        /// </summary>
        public string NodeName
        {
            get
            {
                return this.Path[(this.Path.LastIndexOf(SeparatorCharacter) + 1)..];
            }
        }

        /// <summary>
        ///     Gets a non-null path string representation.
        /// </summary>
        private string Path
        {
            get
            {
                return this.path ?? SeparatorString;
            }
        }

        /// <summary>
        ///     Implicitly converts an instane of <see cref="LogicalPath"/> value to a <see cref="string"/>.
        /// </summary>
        /// <param name="path">
        ///     Path to convert.
        /// </param>
        public static implicit operator string(LogicalPath path)
        {
            return path.Path;
        }

        /// <summary>
        ///     Implicitly converts a <see cref="string"/> path to a <see cref="LogicalPath"/>.
        /// </summary>
        /// <param name="path">
        ///     <see cref="string"/> to convert.
        /// </param>
        public static implicit operator LogicalPath(string path)
        {
            Requires.ArgumentNotNull(path, nameof(path));

            return FromString(path);
        }

        /// <summary>
        ///     Combines two logical path instances together by navigating to path defined in <paramref name="right"/> parameter
        ///     from the context of <paramref name="left"/> parameter path. If the <paramref name="right"/> path is a relative path
        ///     both paths are combined with usage of <see cref="SeparatorString"/>, otherwise <paramref name="right"/> is returned.
        /// </summary>
        /// <param name="left">
        ///     First path to combine.
        /// </param>
        /// <param name="right">
        ///     Second path to combine.
        /// </param>
        /// <returns>
        ///     If the <paramref name="right"/> path is a relative path a combination of both parameters with usage of <see cref="SeparatorString"/>,
        ///     <paramref name="right"/>, otherwise.
        /// </returns>
        [SuppressMessage("Usage", "CA2225:Operator overloads have named alternates", Justification = "Alternative method for this operator is 'Combine'.")]
        public static LogicalPath operator /(LogicalPath left, LogicalPath right)
        {
            return left.Combine(right);
        }

        /// <summary>
        ///     Indicates whether the two instances are equal.
        /// </summary>
        /// <param name="left">
        ///     The first object to compare.
        /// </param>
        /// <param name="right">
        ///     The second object to compare.
        /// </param>
        /// <returns>
        ///     <see langword="true"/> if the objects are considered equal; otherwise, <paramref langword="false"/>.
        ///     If both <paramref name="left"/> and <paramref name="right"/> are <see langword="null"/>, the method returns <paramref langword="true"/>.
        /// </returns>
        public static bool operator ==(LogicalPath left, LogicalPath right)
        {
            return Equals(left, right);
        }

        /// <summary>
        ///     Indicates whether the two instances are not equal.
        /// </summary>
        /// <param name="left">
        ///     The first object to compare.
        /// </param>
        /// <param name="right">
        ///     The second object to compare.
        /// </param>
        /// <returns>
        ///     <see langword="true"/> if the objects are considered not equal; otherwise, <paramref langword="false"/>.
        ///     If both <paramref name="left"/> and <paramref name="right"/> are <see langword="null"/>, the method returns <paramref langword="false"/>.
        /// </returns>
        public static bool operator !=(LogicalPath left, LogicalPath right)
        {
            return !Equals(left, right);
        }

        /// <summary>
        ///     Compares the current path with another path and returns a value that indicates whether the current path precedes in the lexicographical order the other path.
        /// </summary>
        /// <param name="left">
        ///     The first object to compare.
        /// </param>
        /// <param name="right">
        ///     The second object to compare.
        /// </param>
        /// <returns>
        ///     <see langword="true"/> if the current path precedes in the lexicographical order the other path;
        ///     otherwise, <see langword="false"/>.
        /// </returns>
        public static bool operator <(LogicalPath left, LogicalPath right)
        {
            return Compare(left, right) < 0;
        }

        /// <summary>
        ///     Compares the current path with another path and returns a value that indicates whether the current path follows in the lexicographical order the other path.
        /// </summary>
        /// <param name="left">
        ///     The first object to compare.
        /// </param>
        /// <param name="right">
        ///     The second object to compare.
        /// </param>
        /// <returns>
        ///     <see langword="true"/> if the current path follows in the lexicographical order the other path;
        ///     otherwise, <see langword="false"/>.
        /// </returns>
        public static bool operator >(LogicalPath left, LogicalPath right)
        {
            return Compare(left, right) > 0;
        }

        /// <summary>
        ///     Compares the current path with another path and returns a value that indicates whether the current path precedes or occurs in the same position
        ///     in the lexicographical order as the other path.
        /// </summary>
        /// <param name="left">
        ///     The first object to compare.
        /// </param>
        /// <param name="right">
        ///     The second object to compare.
        /// </param>
        /// <returns>
        ///     <see langword="true"/> if the current path precedes  or occurs in the same position in the lexicographical order as the other path;
        ///     otherwise, <see langword="false"/>.
        /// </returns>
        public static bool operator <=(LogicalPath left, LogicalPath right)
        {
            return Compare(left, right) <= 0;
        }

        /// <summary>
        ///     Compares the current path with another path and returns a value that indicates whether the current path follows or occurs in the same position
        ///     in the lexicographical order as the other path.
        /// </summary>
        /// <param name="left">
        ///     The first object to compare.
        /// </param>
        /// <param name="right">
        ///     The second object to compare.
        /// </param>
        /// <returns>
        ///     <see langword="true"/> if the current path follows  or occurs in the same position in the lexicographical order as the other path;
        ///     otherwise, <see langword="false"/>.
        /// </returns>
        public static bool operator >=(LogicalPath left, LogicalPath right)
        {
            return Compare(left, right) >= 0;
        }

        /// <summary>
        ///     Converts a <see cref="string"/> path to a <see cref="LogicalPath"/>.
        /// </summary>
        /// <param name="path">
        ///     <see cref="string"/> to convert.
        /// </param>
        /// <returns>
        ///     A <see cref="LogicalPath"/> value that corresponds to a specified string representation.
        /// </returns>
        public static LogicalPath FromString(string path)
        {
            return new LogicalPath(Normalize(path));
        }

        /// <summary>
        ///     Indicates whether the two instances are equal.
        /// </summary>
        /// <param name="left">
        ///     The first object to compare.
        /// </param>
        /// <param name="right">
        ///     The second object to compare.
        /// </param>
        /// <returns>
        ///     <see langword="true"/> if the objects are considered equal; otherwise, <paramref langword="false"/>.
        ///     If both <paramref name="left"/> and <paramref name="right"/> are <see langword="null"/>, the method returns <paramref langword="true"/>.
        /// </returns>
        public static bool Equals(LogicalPath left, LogicalPath right)
        {
            return left.Equals(right);
        }

        /// <summary>
        ///     Compares the current path with another path and returns an integer that indicates whether the current instance precedes, follows,
        ///     or occurs in the same position in the lexicographical order as the other object.
        /// </summary>
        /// <param name="left">
        ///     The first object to compare.
        /// </param>
        /// <param name="right">
        ///     The second object to compare.
        /// </param>
        /// <returns>
        ///     A value that indicates the relative order of the paths being compared. The return value has these meanings:
        ///     <list type="table">
        ///         <listheader>
        ///             <term>Value</term>
        ///             <description>Meaning</description>
        ///         </listheader>
        ///         <item>
        ///             <term>Less than zero</term>
        ///             <description>First path precedes the other in the lexicographical order.</description>
        ///         </item>
        ///         <item>
        ///             <term>Zero</term>
        ///             <description>First path occurs in the same position in the lexicographical order as the other.</description>
        ///         </item>
        ///         <item>
        ///             <term>Greater than zero</term>
        ///             <description>First path follows other in the lexicographical order.</description>
        ///         </item>
        ///     </list>
        /// </returns>
        public static int Compare(LogicalPath left, LogicalPath right)
        {
            return left.CompareTo(right);
        }

        /// <summary>
        ///     Combines two logical path instances together by navigating to path defined in <paramref name="right"/> parameter
        ///     from the context of <paramref name="left"/> parameter path. If the <paramref name="right"/> path is a relative path
        ///     both paths are combined with usage of <see cref="SeparatorString"/>, otherwise <paramref name="right"/> is returned.
        /// </summary>
        /// <param name="left">
        ///     First path to combine.
        /// </param>
        /// <param name="right">
        ///     Second path to combine.
        /// </param>
        /// <returns>
        ///     If the <paramref name="right"/> path is a relative path a combination of both parameters with usage of <see cref="SeparatorString"/>,
        ///     <paramref name="right"/>, otherwise.
        /// </returns>
        public static LogicalPath Combine(LogicalPath left, LogicalPath right)
        {
            return left.Combine(right);
        }

        /// <summary>
        ///     Gets a most basic form of the specified path.
        /// </summary>
        /// <param name="path">
        ///     Path to normalize.
        /// </param>
        /// <returns>
        ///     Path to the same logical tree node as input <paramref name="path"/> but in a most basic form.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="path"/> is <see langword="null"/>.
        /// </exception>
        public static string Normalize(string path)
        {
            Requires.ArgumentNotNull(path, nameof(path));

            string trimmed = path.TrimStart();
            bool isAbsolute = trimmed.Length > 0 && trimmed[0] == SeparatorCharacter;
            IEnumerable<string> parts = trimmed.Split(SeparatorCharacter, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).Where(part => part != CurrentPathString);
            var stack = new Stack<string>();
            int partCount = 0;

            foreach (string part in parts)
            {
                if (part == ParentPathString)
                {
                    if (partCount > 0)
                    {
                        _ = stack.Pop();
                        --partCount;
                        continue;
                    }
                }
                else
                {
                    ++partCount;
                }

                stack.Push(part);
            }

            string result = (isAbsolute ? SeparatorString : string.Empty) + string.Join(SeparatorString, stack.ToArray().Reverse());
            return string.IsNullOrEmpty(result) ? CurrentPathString : result;
        }

        /// <summary>
        ///     Gets a value that represents path to the parent of the node represented by the current path.
        /// </summary>
        /// <returns>
        ///     Value that represents path to the parent of the node represented by the current path.
        /// </returns>
        public LogicalPath GetParent()
        {
            return this.Path.LastIndexOf(SeparatorCharacter) switch
            {
                0 => Root,
                -1 => Parent,
                var index => new LogicalPath(this.Path[0..index]),
            };
        }

        /// <summary>
        ///     Indicates whether this instance and a specified object are equal.
        /// </summary>
        /// <param name="obj">
        ///     The object to compare with the current object.
        /// </param>
        /// <returns>
        ///     <see langword="true"/> if the specified object is equal to the current object; otherwise, <see langword="false"/>.
        /// </returns>
        public override bool Equals(object? obj)
        {
            return obj is LogicalPath path && this.Equals(path);
        }

        /// <summary>
        ///     Indicates whether the current path is equal to another object of the same type.
        /// </summary>
        /// <param name="other">
        ///     An object to compare with this object.
        /// </param>
        /// <returns>
        ///     <see langword="true"/> if the current object is equal to the <paramref name="other"/> parameter;
        ///     otherwise, <see langword="false"/>.
        /// </returns>
        public bool Equals(LogicalPath other)
        {
            return this.Path.Equals(other.Path, StringComparison.Ordinal);
        }

        /// <summary>
        ///     Returns the hash code for this path.
        /// </summary>
        /// <returns>
        ///     A 32-bit signed integer hash code.
        /// </returns>
        public override int GetHashCode()
        {
            return this.Path.GetHashCode(StringComparison.Ordinal);
        }

        /// <summary>
        ///     Compares the current path with another one and returns an integer that indicates whether the current instance precedes, follows,
        ///     or occurs in the same position in the lexicographical order as the other object.
        /// </summary>
        /// <param name="other">
        ///     An object to compare with this instance.
        /// </param>
        /// <returns>
        ///     A value that indicates the relative order of the paths being compared. The return value has these meanings:
        ///     <list type="table">
        ///         <listheader>
        ///             <term>Value</term>
        ///             <description>Meaning</description>
        ///         </listheader>
        ///         <item>
        ///             <term>Less than zero</term>
        ///             <description>The current path precedes the other in the lexicographical order.</description>
        ///         </item>
        ///         <item>
        ///             <term>Zero</term>
        ///             <description>The current path occurs in the same position in the lexicographical order as the other.</description>
        ///         </item>
        ///         <item>
        ///             <term>Greater than zero</term>
        ///             <description>The current path follows other in the lexicographical order.</description>
        ///         </item>
        ///     </list>
        /// </returns>
        /// <exception cref="ArgumentException">
        ///     <paramref name="other"/> is of different path kind (absolute or relative) that the current instance.
        /// </exception>
        public int CompareTo(LogicalPath other)
        {
            return this.IsAbsolute != other.IsAbsolute
                ? throw new ArgumentException(Resources.Exception_Argument_LogicalPathOfDifferentKind, nameof(other))
                : this.Path.Zip(other.Path)
                    .Where(pair => pair.First != pair.Second)
                    .Select(pair => pair switch
                        {
                            (SeparatorCharacter, _) => -1,
                            (_, SeparatorCharacter) => 1,
                            _ => pair.First.CompareTo(pair.Second),
                        })
                    .DefaultIfEmpty(this.Path.Length.CompareTo(other.Path.Length))
                    .First();
        }

        /// <summary>
        ///     Compares the current path with another bject and returns an integer that indicates whether the current instance precedes, follows,
        ///     or occurs in the same position in the lexicographical order as the other object.
        /// </summary>
        /// <param name="obj">
        ///     An object to compare with this instance.
        /// </param>
        /// <returns>
        ///     A value that indicates the relative order of the paths being compared. The return value has these meanings:
        ///     <list type="table">
        ///         <listheader>
        ///             <term>Value</term>
        ///             <description>Meaning</description>
        ///         </listheader>
        ///         <item>
        ///             <term>Less than zero</term>
        ///             <description>The current path precedes the other in the lexicographical order.</description>
        ///         </item>
        ///         <item>
        ///             <term>Zero</term>
        ///             <description>The current path occurs in the same position in the lexicographical order as the other.</description>
        ///         </item>
        ///         <item>
        ///             <term>Greater than zero</term>
        ///             <description>The current path follows other in the lexicographical order.</description>
        ///         </item>
        ///     </list>
        /// </returns>
        /// <exception cref="ArgumentException">
        ///     <paramref name="obj"/> is not a <see cref="LogicalPath"/> value.
        /// </exception>
        public int CompareTo(object? obj)
        {
            return obj switch
            {
                null => 1,
                LogicalPath path => this.CompareTo(path),
                _ => throw new ArgumentException(Resources.Exception_Argument_LogicalPathExpected, nameof(obj)),
            };
        }

        /// <summary>
        ///     Returns a string that represents the current path.
        /// </summary>
        /// <returns>
        ///     A string that represents the current path.
        /// </returns>
        public override string ToString()
        {
            return this.Path;
        }

        /// <summary>
        ///     Combines current path with the other one by navigating to <paramref name="path"/>  from the context of the current path.
        ///     If the <paramref name="path"/> is a relative path both paths are combined with usage of <see cref="SeparatorString"/>,
        ///     otherwise <paramref name="path"/> is returned.
        /// </summary>
        /// <param name="path">
        ///     Path to combine with current instance.
        /// </param>
        /// <returns>
        ///     If the <paramref name="path"/> is a relative path a combination of both paths with usage of <see cref="SeparatorString"/>,
        ///     otherwise <paramref name="path"/> is returned.
        /// </returns>
        public LogicalPath Combine(LogicalPath path)
        {
            return path.IsAbsolute ? path : new LogicalPath(this.Path + SeparatorString + path.Path);
        }

        /// <summary>
        ///     Gets a value that indicatges whether the current path points to a child node of the node pointed by the other path.
        /// </summary>
        /// <param name="path">
        ///     Path to a parent node to check.
        /// </param>
        /// <returns>
        ///     <see langword="true"/> if the current path points to a child node of the node pointed by the <paramref name="path"/> path;
        ///     otherwise, <see langword="false"/>.
        /// </returns>
        /// <exception cref="ArgumentException">
        ///     <paramref name="path"/> and current instance are of different path kind.
        /// </exception>
        public bool IsChildOf(LogicalPath path)
        {
            Requires.ArgumentState(this.IsAbsolute == path.IsAbsolute, nameof(path), Resources.Exception_Argument_LogicalPathOfDifferentKind);

            return this.Path.StartsWith(path.Path, StringComparison.Ordinal)
                && this.Path.Length > path.Path.Length
                && this.Path[path.Path.Length] == SeparatorCharacter;
        }

        /// <summary>
        ///     Gets a value that indicatges whether the current path points to a parent node of the node pointed by the other path.
        /// </summary>
        /// <param name="path">
        ///     Path to a child node to check.
        /// </param>
        /// <returns>
        ///     <see langword="true"/> if the current path points to a parent node of the node pointed by the <paramref name="path"/> path;
        ///     otherwise, <see langword="false"/>.
        /// </returns>
        /// <exception cref="ArgumentException">
        ///     <paramref name="path"/> and current instance are of different path kind.
        /// </exception>
        public bool IsParentFor(LogicalPath path)
        {
            Requires.ArgumentState(this.IsAbsolute == path.IsAbsolute, nameof(path), Resources.Exception_Argument_LogicalPathOfDifferentKind);

            return path.IsChildOf(this);
        }

        /// <summary>
        ///     Gets a relative path from the node represented by the current path to a node represented by the another path.
        /// </summary>
        /// <param name="other">
        ///     Another node path.
        /// </param>
        /// <returns>
        ///     Relative path from the node represented by the current path to a node represented by the <paramref name="other"/> parameter.
        /// </returns>
        /// <exception cref="ArgumentException">
        ///     <paramref name="other"/> and current instance are of different path kind.
        /// </exception>
        public LogicalPath GetRelativePathTo(LogicalPath other)
        {
            Requires.ArgumentState(this.IsAbsolute == other.IsAbsolute, nameof(other), Resources.Exception_Argument_LogicalPathOfDifferentKind);

            LogicalPath commonPath = this.GetCommonPath(other);

            return new LogicalPath(
                CurrentPathString
                    + SeparatorString
                    + string.Concat(Enumerable.Repeat(ParentPathString + SeparatorString, this.Depth - commonPath.Depth))
                    + other.Path[commonPath.Path.Length..]);
        }

        /// <summary>
        ///     Extract a common path between the current path and a specified one.
        /// </summary>
        /// <param name="other">
        ///     Another instance of the path to compare with the current instance.
        /// </param>
        /// <returns>
        ///     A common path between the current path and a <paramref name="other"/> parameter.
        /// </returns>
        /// <exception cref="ArgumentException">
        ///     <paramref name="other"/> and current instance are of different path kind.
        /// </exception>
        public LogicalPath GetCommonPath(LogicalPath other)
        {
            Requires.ArgumentState(this.IsAbsolute == other.IsAbsolute, nameof(other), Resources.Exception_Argument_LogicalPathOfDifferentKind);

            int index = this.Path.Zip(other.Path)
                .TakeWhile(pair => pair.First == pair.Second)
                .Select((pair, i) => pair.First == SeparatorCharacter ? i : -1)
                .Max();

            return index switch
            {
                -1 => Current,
                0 => Root,
                _ => new LogicalPath(this.Path.Substring(0, index)),
            };
        }

        /// <summary>
        ///     Gets an array of the names of the path nodes that the current path consist of.
        ///     Name of the node nearest the root is located at the begining of the array.
        /// </summary>
        /// <returns>
        ///     Newly created array of the names of the path nodes that the current path consist of.
        /// </returns>
        public string[] GetNodeNames()
        {
            return this.Path.Split(SeparatorCharacter, StringSplitOptions.RemoveEmptyEntries);
        }

        /// <summary>
        ///     Populates a <see cref="SerializationInfo"/> with the data needed to serialize the target object.
        /// </summary>
        /// <param name="info">
        ///     The <see cref="SerializationInfo"/> to populate with data.
        /// </param>
        /// <param name="context">
        ///     The destination <see cref="StreamingContext"/> for this serialization.
        /// </param>
        /// <exception cref="SecurityException">
        ///     The caller does not have the required permission.
        /// </exception>
        void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue(nameof(this.path), this.path);
        }
    }
}
