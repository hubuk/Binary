// <copyright file="RepeatBlock.cs" company="LeetABit">
//   Copyright (c) Hubert Bukowski. All rights reserved.
//   Licensed under the MIT License.
//   See LICENSE file in the project root for full license information.
// </copyright>

namespace LeetABit.Binary.Blocks
{
    using System;

    /// <summary>
    ///     Represents a decorator that processes inner block as long as the condition is meet.
    /// </summary>
    public abstract class RepeatBlock : ConditionalBlock
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="RepeatBlock"/> class.
        /// </summary>
        /// <param name="inner">
        ///     Definition block that shall be decorated.
        /// </param>
        /// <param name="condition">
        ///     Function that obtains a condition for each repetition.
        /// </param>
        /// <param name="indexVariableName">
        ///     Function that obtains a name of the repetition index variable.
        /// </param>
        protected RepeatBlock(IDefinitionBlock inner, Func<IEvaluationContext, bool> condition, Func<IEvaluationContext, string> indexVariableName)
            : base(inner, condition)
        {
            this.IndexVariableName = Requires.ArgumentNotNull(indexVariableName, nameof(indexVariableName));
        }

        /// <summary>
        ///     Gets a function that obtains a name of the repetition index variable.
        /// </summary>
        protected Func<IEvaluationContext, string> IndexVariableName
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

            var variableName = this.IndexVariableName(context);
            var variableBackup = context.GetVariable(variableName).OnError((object?)null);

            try
            {
                context.SetVariable(variableName, 0);
                for (int i = 0; this.Condition(context); context.SetVariable(variableName, ++i))
                {
                    var result = this.Inner.Process(context);
                    if (result.IsError)
                    {
                        return result;
                    }
                }

                return Result.Success;
            }
            finally
            {
                context.SetVariable(variableName, variableBackup);
            }
        }
    }
}
