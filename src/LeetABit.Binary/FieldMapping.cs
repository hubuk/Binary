// <copyright file="FieldMapping.cs" company="LeetABit">
//   Copyright (c) Hubert Bukowski. All rights reserved.
//   Licensed under the MIT License.
//   See LICENSE file in the project root for full license information.
// </copyright>

namespace LeetABit.Binary
{
    using System;

    /// <summary>
    ///     Contains information about a binary value and its converted interpretation mapped int o a binary position and logical path.
    /// </summary>
    public class FieldMapping
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="FieldMapping"/> class.
        /// </summary>
        /// <param name="path">
        ///     Logical path under which the value is located.
        /// </param>
        /// <param name="position">
        ///     Binary position at which the value is located.
        /// </param>
        /// <param name="value">
        ///     Mapped binary value.
        /// </param>
        /// <param name="convertedValue">
        ///     Binary value interpretation.
        /// </param>
        /// <exception cref="ArgumentException">
        ///     <paramref name="path"/> is a root path.
        ///     <para>-or-</para>
        ///     <paramref name="path"/> is a relative path.
        /// </exception>
        public FieldMapping(LogicalPath path, int position, IBinaryValue value, object convertedValue)
        {
            _ = Requires.ArgumentNotRootAbsolutePath(path, nameof(path));
            _ = Requires.ArgumentNotNull(value, nameof(value));

            this.Path = path;
            this.Position = position;
            this.Value = value;
            this.ConvertedValue = convertedValue;
        }

        /// <summary>
        ///     Gets a logical path under which the value is located.
        /// </summary>
        public LogicalPath Path
        {
            get;
        }

        /// <summary>
        ///     Gets a binary position at which the value is located.
        /// </summary>
        public int Position
        {
            get;
        }

        /// <summary>
        ///     Gets a mapped binary value.
        /// </summary>
        public IBinaryValue Value
        {
            get;
        }

        /// <summary>
        ///     Gets a binary value interpretation.
        /// </summary>
        public object ConvertedValue
        {
            get;
        }
    }
}
