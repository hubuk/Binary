// <copyright file="ConditionalBlock.cs" company="LeetABit">
//   Copyright (c) Hubert Bukowski. All rights reserved.
//   Licensed under the MIT License.
//   See LICENSE file in the project root for full license information.
// </copyright>

namespace LeetABit.Binary.Blocks
{
    using System;

    /// <summary>
    ///     Represents a decorator that processes inner block when a condition is meet.
    /// </summary>
    public class ConditionalBlock : DecoratorBlock
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="ConditionalBlock"/> class.
        /// </summary>
        /// <param name="inner">
        ///     Definition block that shall be decorated.
        /// </param>
        /// <param name="condition">
        ///     Function that obtains a condition for a decorator.
        /// </param>
        public ConditionalBlock(IDefinitionBlock inner, Func<IEvaluationContext, bool> condition)
            : base(inner)
        {
            this.Condition = Requires.ArgumentNotNull(condition, nameof(condition));
        }

        /// <summary>
        ///     Gets a function that obtains a condition for a decorator.
        /// </summary>
        protected Func<IEvaluationContext, bool> Condition
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
                : Result.Success;
        }
    }
}
