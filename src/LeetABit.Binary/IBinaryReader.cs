// <copyright file="IBinaryReader.cs" company="LeetABit">
//   Copyright (c) Hubert Bukowski. All rights reserved.
//   Licensed under the MIT License.
//   See LICENSE file in the project root for full license information.
// </copyright>

namespace LeetABit.Binary
{
    /// <summary>
    ///     Defines syntax available for objects that supports reading binary values.
    /// </summary>
    public interface IBinaryReader : IBinarySeeker
    {
        /// <summary>
        ///     Read binary value that consists of specified number of bits and move current position the same amount.
        /// </summary>
        /// <param name="length">
        ///     Number of bits to read.
        /// </param>
        /// <returns>
        ///     Binary value read with the requested length.
        /// </returns>
        Result<IBinaryValue> Read(int length);
    }
}
