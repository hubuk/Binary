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
        /// <param name="argument">
        ///     Argument object to check.
        /// </param>
        /// <param name="parameterName">
        ///     Name of the parameter for which the arguemnt was bound.
        /// </param>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="argument"/> is <see langword="null"/>.
        /// </exception>
        public static void ArgumentNotNull(object argument, string parameterName)
        {
            _ = argument ?? throw new ArgumentNullException(parameterName);
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
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="argument"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="ArgumentException">
        ///     <paramref name="argument"/> is composed of white spaces only.
        /// </exception>
        public static void ArgumentNotNullAndNotWhiteSpace(string argument, string parameterName)
        {
            ArgumentNotNull(argument, parameterName);

            if (string.IsNullOrWhiteSpace(argument))
            {
                throw new ArgumentException(Resources.Exception_Argument_WhiteSpace);
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
    }
}
