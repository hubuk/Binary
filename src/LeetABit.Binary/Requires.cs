// <copyright file="Requires.cs" company="LeetABit">
//   Copyright (c) Hubert Bukowski. All rights reserved.
//   Licensed under the MIT License.
//   See LICENSE file in the project root for full license information.
// </copyright>

namespace LeetABit.Binary
{
    using System;

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
    }
}
