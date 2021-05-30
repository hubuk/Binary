// <copyright file="BufferBlock.cs" company="LeetABit">
//   Copyright (c) Hubert Bukowski. All rights reserved.
//   Licensed under the MIT License.
//   See LICENSE file in the project root for full license information.
// </copyright>

namespace LeetABit.Binary.Blocks
{
    using System;

    /// <summary>
    ///     Represents a decorator that limits binary source to a buffer of the specified length.
    /// </summary>
    public partial class BufferBlock : DecoratorBlock
    {
        /// <summary>
        ///     Function that obtains a length of the buffer.
        /// </summary>
        private readonly Func<IEvaluationContext, int> length;

        /// <summary>
        ///     Initializes a new instance of the <see cref="BufferBlock"/> class.
        /// </summary>
        /// <param name="inner">
        ///     Definition block that shall be decorated.
        /// </param>
        /// <param name="length">
        ///     Function that obtains a length of the buffer.
        /// </param>
        public BufferBlock(IDefinitionBlock inner, Func<IEvaluationContext, int> length)
            : base(inner)
        {
            this.length = Requires.ArgumentNotNull(length, nameof(length));
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
        public override Result Process(ICodingContext context)
        {
            _ = Requires.ArgumentNotNull(context, nameof(context));

            var bufferLength = this.length(context);
            var bufferContext = new ContextWrapper(context, bufferLength);
            return this.Inner.Process(bufferContext);
        }
    }
}
