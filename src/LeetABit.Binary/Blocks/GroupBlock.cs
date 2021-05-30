// <copyright file="GroupBlock.cs" company="LeetABit">
//   Copyright (c) Hubert Bukowski. All rights reserved.
//   Licensed under the MIT License.
//   See LICENSE file in the project root for full license information.
// </copyright>

namespace LeetABit.Binary.Blocks
{
    using System.Collections.Generic;

    /// <summary>
    ///     Represents a definition block that encapsulates other blocks into one group.
    /// </summary>
    public class GroupBlock : IDefinitionBlock
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="GroupBlock"/> class.
        /// </summary>
        /// <param name="children">
        ///     Collection of the child blocks.
        /// </param>
        public GroupBlock(IEnumerable<IDefinitionBlock> children)
        {
            this.Children = Requires.ArgumentNotNull(children, nameof(children));
        }

        /// <summary>
        ///     Gets a collection of the child blocks.
        /// </summary>
        public IEnumerable<IDefinitionBlock> Children
        {
            get;
        }

        /// <summary>
        ///     Implements processing of the current definition block using specified coding context.
        /// </summary>
        /// <param name="context">
        ///     Coding context that contans current coding data.
        /// </param>
        /// <returns>
        ///     Object that represents processing result.
        /// </returns>
        public virtual Result Process(ICodingContext context)
        {
            foreach (var child in this.Children)
            {
                var result = child.Process(context);
                if (result.IsError)
                {
                    return result;
                }
            }

            return Result.Success;
        }
    }
}
