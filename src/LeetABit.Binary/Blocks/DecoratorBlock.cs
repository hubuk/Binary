// <copyright file="DecoratorBlock.cs" company="LeetABit">
//   Copyright (c) Hubert Bukowski. All rights reserved.
//   Licensed under the MIT License.
//   See LICENSE file in the project root for full license information.
// </copyright>

namespace LeetABit.Binary.Blocks
{
    /// <summary>
    ///     Represents a block that decorates another block with custom functionality.
    /// </summary>
    public abstract class DecoratorBlock : IDefinitionBlock
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="DecoratorBlock"/> class.
        /// </summary>
        /// <param name="inner">
        ///     Definition block that shall be decorated.
        /// </param>
        protected DecoratorBlock(IDefinitionBlock inner)
        {
            this.Inner = Requires.ArgumentNotNull(inner, nameof(inner));
        }

        /// <summary>
        ///     Gets a definition block that is decorated.
        /// </summary>
        protected IDefinitionBlock Inner
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
        public abstract Result Process(ICodingContext context);
    }
}
