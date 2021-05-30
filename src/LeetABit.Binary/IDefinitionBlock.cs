// <copyright file="IDefinitionBlock.cs" company="LeetABit">
//   Copyright (c) Hubert Bukowski. All rights reserved.
//   Licensed under the MIT License.
//   See LICENSE file in the project root for full license information.
// </copyright>

namespace LeetABit.Binary
{
    /// <summary>
    ///     Defines syntax available for objects that represents a binary data definition block.
    /// </summary>
    public interface IDefinitionBlock
    {
        /// <summary>
        ///     Implements processing of the current definition block using specified coding context.
        /// </summary>
        /// <param name="context">
        ///     Coding context that contans current coding data.
        /// </param>
        /// <returns>
        ///     Object that represents processing result.
        /// </returns>
        Result Process(ICodingContext context);
    }
}
