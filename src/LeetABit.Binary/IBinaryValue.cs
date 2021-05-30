// <copyright file="IBinaryValue.cs" company="LeetABit">
//   Copyright (c) Hubert Bukowski. All rights reserved.
//   Licensed under the MIT License.
//   See LICENSE file in the project root for full license information.
// </copyright>

namespace LeetABit.Binary
{
    /// <summary>
    ///     Defines syntax available for objects that represents a binary value.
    /// </summary>
    public interface IBinaryValue
    {
        /// <summary>
        ///     Gets a bit length of the binary value.
        /// </summary>
        int Length
        {
            get;
        }
    }
}
