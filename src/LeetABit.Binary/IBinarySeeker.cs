// <copyright file="IBinarySeeker.cs" company="LeetABit">
//   Copyright (c) Hubert Bukowski. All rights reserved.
//   Licensed under the MIT License.
//   See LICENSE file in the project root for full license information.
// </copyright>

namespace LeetABit.Binary
{
    /// <summary>
    ///     Defines syntax available for objects that supports seekable position in the binary data.
    /// </summary>
    public interface IBinarySeeker
    {
        /// <summary>
        ///     Gets the current bit position in the binary data.
        /// </summary>
        int Position
        {
            get;
        }

        /// <summary>
        ///     Changes position within binary data.
        /// </summary>
        /// <param name="offset">
        ///     Number of bits to move the position. Positive value moves position away from the first transmitted bit.
        /// </param>
        /// <returns>
        ///     Operation result.
        /// </returns>
        Result Move(int offset);
    }
}
