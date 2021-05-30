// <copyright file="FillBlock.cs" company="LeetABit">
//   Copyright (c) Hubert Bukowski. All rights reserved.
//   Licensed under the MIT License.
//   See LICENSE file in the project root for full license information.
// </copyright>

namespace LeetABit.Binary.Blocks
{
    /// <summary>
    ///     Represents a definition block that processes inner block till an error occures.
    /// </summary>
    public class FillBlock : DecoratorBlock
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="FillBlock"/> class.
        /// </summary>
        /// <param name="inner">
        ///     Definition block that shall be decorated.
        /// </param>
        public FillBlock(IDefinitionBlock inner)
            : base(inner)
        {
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

            var result = Result.Success;

            while (result.IsSuccess)
            {
                using var transaction = context.BeginTransaction();
                result = this.Inner.Process(context);
                if (result.IsSuccess)
                {
                    transaction.Commit();
                }
            }

            return Result.Success;
        }
    }
}
