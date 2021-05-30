// <copyright file="IBinaryWriter.cs" company="LeetABit">
//   Copyright (c) Hubert Bukowski. All rights reserved.
//   Licensed under the MIT License.
//   See LICENSE file in the project root for full license information.
// </copyright>

namespace LeetABit.Binary
{
    /// <summary>
    ///     Defines syntax available for objects that supports writing binary values.
    /// </summary>
    public interface IBinaryWriter : IBinarySeeker
    {
        /// <summary>
        ///     Write specified binary value and move current position by the length of the data written.
        /// </summary>
        /// <param name="value">
        ///     Binary value to write.
        /// </param>
        /// <returns>
        ///     Result of the operatoin.
        /// </returns>
        Result Write(IBinaryValue value);
    }
}
