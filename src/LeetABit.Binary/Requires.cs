// <copyright file="Requires.cs" company="LeetABit">
//   Copyright (c) Hubert Bukowski. All rights reserved.
//   Licensed under the MIT License.
//   See LICENSE file in the project root for full license information.
// </copyright>

namespace LeetABit.Binary
{
    using System;
    using LeetABit.Binary.Properties;

    /// <summary>
    ///     Provides methods for checking method requirements such as argument and object state verification.
    /// </summary>
    internal static class Requires
    {
        /// <summary>
        ///     Checks whether the specified argument is not <see langword="null"/>.
        /// </summary>
        /// <typeparam name="T">
        ///     Type of the argument.
        /// </typeparam>
        /// <param name="argument">
        ///     Argument object to check.
        /// </param>
        /// <param name="parameterName">
        ///     Name of the parameter for which the arguemnt was bound.
        /// </param>
        /// <returns>
        ///     <paramref name="argument"/> argument.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="argument"/> is <see langword="null"/>.
        /// </exception>
        public static T ArgumentNotNull<T>(T argument, string parameterName)
        {
            return argument ?? throw new ArgumentNullException(parameterName);
        }

        /// <summary>
        ///     Checks whether the specified argument is not <see langword="null"/> and is not composed of white spaces only.
        /// </summary>
        /// <param name="argument">
        ///     Argument object to check.
        /// </param>
        /// <param name="parameterName">
        ///     Name of the parameter for which the arguemnt was bound.
        /// </param>
        /// <returns>
        ///     <paramref name="argument"/> argument.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="argument"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="ArgumentException">
        ///     <paramref name="argument"/> is composed of white spaces only.
        /// </exception>
        public static string ArgumentNotNullAndNotWhiteSpace(string argument, string parameterName)
        {
            _ = ArgumentNotNull(argument, parameterName);

            return !string.IsNullOrWhiteSpace(argument)
                ? argument
                : throw new ArgumentException(Resources.Exception_Argument_WhiteSpace, parameterName);
        }

        /// <summary>
        ///     Checks whether the specified path is not a root absolute path.
        /// </summary>
        /// <param name="path">
        ///     Path to check.
        /// </param>
        /// <param name="parameterName">
        ///     Name of the parameter for which the arguemnt was bound.
        /// </param>
        /// <returns>
        ///     <paramref name="path"/> argument.
        /// </returns>
        /// <exception cref="ArgumentException">
        ///     <paramref name="path"/> is a root path.
        ///     <para>-or-</para>
        ///     <paramref name="path"/> is a relative path.
        /// </exception>
        public static LogicalPath ArgumentNotRootAbsolutePath(LogicalPath path, string parameterName)
        {
            _ = ArgumentNotRootPath(path, parameterName);
            return ArgumentAbsolutePath(path, parameterName);
        }

        /// <summary>
        ///     Checks whether the specified path is not a root path.
        /// </summary>
        /// <param name="path">
        ///     Path to check.
        /// </param>
        /// <param name="parameterName">
        ///     Name of the parameter for which the arguemnt was bound.
        /// </param>
        /// <returns>
        ///     <paramref name="path"/> argument.
        /// </returns>
        /// <exception cref="ArgumentException">
        ///     <paramref name="path"/> is a root path.
        /// </exception>
        public static LogicalPath ArgumentNotRootPath(LogicalPath path, string parameterName)
        {
            return !path.IsRoot
                ? path
                : throw new ArgumentException(Resources.Exception_Argument_RootPath, parameterName);
        }

        /// <summary>
        ///     Checks whether the specified path is an absolute path.
        /// </summary>
        /// <param name="path">
        ///     Path to check.
        /// </param>
        /// <param name="parameterName">
        ///     Name of the parameter for which the arguemnt was bound.
        /// </param>
        /// <returns>
        ///     <paramref name="path"/> argument.
        /// </returns>
        /// <exception cref="ArgumentException">
        ///     <paramref name="path"/> is a relative path.
        /// </exception>
        public static LogicalPath ArgumentAbsolutePath(LogicalPath path, string parameterName)
        {
            return path.IsAbsolute
                ? path
                : throw new ArgumentException(Resources.Exception_Argument_AbsolutePath, parameterName);
        }

        /// <summary>
        ///     Checks whether the specified integer is greater than zero.
        /// </summary>
        /// <param name="value">
        ///     Value to verify.
        /// </param>
        /// <param name="parameterName">
        ///     Name of the parameter for which the arguemnt was bound.
        /// </param>
        /// <returns>
        ///     <paramref name="value"/> argument.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        ///     <paramref name="value"/> is not greater than zero.
        /// </exception>
        public static int ArgumentGreaterThanZero(int value, string parameterName)
        {
            return value > 0
                ? value
                : throw new ArgumentOutOfRangeException(parameterName, value, Resources.Exception_ArgumentOutOfRange_PositiveValue);
        }

        /// <summary>
        ///     Throws an <see cref="ArgumentException"/> if the <paramref name="condition"/> is <see langword="false"/>.
        /// </summary>
        /// <param name="condition">
        ///     Condition to evaluate.
        /// </param>
        /// <param name="parameterName">
        ///     Name of the parameter for which verification failed.
        /// </param>
        /// <param name="message">
        ///     Message of the exception to be thrown.
        /// </param>
        /// <exception cref="ArgumentException">
        ///     <paramref name="condition"/> is <see langword="false"/>.
        /// </exception>
        public static void ArgumentState(bool condition, string parameterName, string message)
        {
            if (!condition)
            {
                throw new ArgumentException(message, parameterName);
            }
        }

        /// <summary>
        ///     Throws an <see cref="InvalidOperationException"/> if the <paramref name="condition"/> is <see langword="false"/>.
        /// </summary>
        /// <param name="condition">
        ///     Condition to evaluate.
        /// </param>
        /// <param name="message">
        ///     Message of the exception to be thrown.
        /// </param>
        /// <exception cref="InvalidOperationException">
        ///     <paramref name="condition"/> is <see langword="false"/>.
        /// </exception>
        public static void ObjectState(bool condition, string message)
        {
            if (!condition)
            {
                throw new InvalidOperationException(message);
            }
        }
    }
}
