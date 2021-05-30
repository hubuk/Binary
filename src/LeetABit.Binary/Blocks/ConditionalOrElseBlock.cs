// <copyright file="ConditionalOrElseBlock.cs" company="LeetABit">
//   Copyright (c) Hubert Bukowski. All rights reserved.
//   Licensed under the MIT License.
//   See LICENSE file in the project root for full license information.
// </copyright>

namespace LeetABit.Binary.Blocks
{
    using System;

    /// <summary>
    ///     Represents a decorator that processes one of the two blocks depending on the specified condition.
    /// </summary>
    public class ConditionalOrElseBlock : ConditionalBlock
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="ConditionalOrElseBlock"/> class.
        /// </summary>
        /// <param name="inner">
        ///     Definition block that shall be decorated.
        /// </param>
        /// <param name="elseBlock">
        ///     Definition block that should be processed when the condition is not meet.
        /// </param>
        /// <param name="condition">
        ///     Function that obtains a condition for a decorator.
        /// </param>
        protected ConditionalOrElseBlock(IDefinitionBlock inner, IDefinitionBlock elseBlock, Func<IEvaluationContext, bool> condition)
            : base(inner, condition)
        {
            this.ElseBlock = Requires.ArgumentNotNull(elseBlock, nameof(elseBlock));
        }

        /// <summary>
        ///     Gets a definition block that should be processed when the condition is not meet.
        /// </summary>
        protected IDefinitionBlock ElseBlock
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
        public override Result Process(ICodingContext context)
        {
            _ = Requires.ArgumentNotNull(context, nameof(context));

            return this.Condition(context)
                ? this.Inner.Process(context)
                : this.ElseBlock.Process(context);
        }
    }
}
