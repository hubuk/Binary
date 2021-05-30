// <copyright file="ProcessBlock.cs" company="LeetABit">
//   Copyright (c) Hubert Bukowski. All rights reserved.
//   Licensed under the MIT License.
//   See LICENSE file in the project root for full license information.
// </copyright>

namespace LeetABit.Binary.Blocks
{
    /// <summary>
    ///     Represents a definition block that processed another deffered block.
    /// </summary>
    public class ProcessBlock : IDefinitionBlock
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="ProcessBlock"/> class.
        /// </summary>
        /// <param name="defferedBlock">
        ///     Deffered block that shall be processed.
        /// </param>
        public ProcessBlock(DefferedBlock defferedBlock)
        {
            this.DefferedBlock = Requires.ArgumentNotNull(defferedBlock, nameof(defferedBlock));
        }

        /// <summary>
        ///     Gets a deffered definition block that shall be processed.
        /// </summary>
        protected DefferedBlock DefferedBlock
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

            return this.DefferedBlock.DefferedProcess(context);
        }
    }
}
