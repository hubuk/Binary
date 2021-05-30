// <copyright file="OffsetBlock.cs" company="LeetABit">
//   Copyright (c) Hubert Bukowski. All rights reserved.
//   Licensed under the MIT License.
//   See LICENSE file in the project root for full license information.
// </copyright>

namespace LeetABit.Binary.Blocks
{
    using System;

    /// <summary>
    ///     Represents a definition block that may be used to control binary position.
    /// </summary>
    public class OffsetBlock : IDefinitionBlock
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="OffsetBlock"/> class.
        /// </summary>
        /// <param name="offset">
        ///     Function that obtains an offset value for which the binary position shall be moved.
        /// </param>
        public OffsetBlock(Func<IEvaluationContext, int> offset)
        {
            this.Offset = Requires.ArgumentNotNull(offset, nameof(offset));
        }

        /// <summary>
        ///     Gets a function that obtains an offset value for which the binary position shall be moved.
        /// </summary>
        public Func<IEvaluationContext, int> Offset
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
        public Result Process(ICodingContext context)
        {
            _ = Requires.ArgumentNotNull(context, nameof(context));

            return context.Move(this.Offset(context));
        }
    }
}
