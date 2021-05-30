// <copyright file="ChoiceBlock.cs" company="LeetABit">
//   Copyright (c) Hubert Bukowski. All rights reserved.
//   Licensed under the MIT License.
//   See LICENSE file in the project root for full license information.
// </copyright>

namespace LeetABit.Binary.Blocks
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    ///     Represents a definition block that processes inner blocks selected by the specified switch.
    /// </summary>
    public class ChoiceBlock : IDefinitionBlock
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="ChoiceBlock"/> class.
        /// </summary>
        /// <param name="switchValue">
        ///     A function that obtaines a switch value.
        /// </param>
        /// <param name="cases">
        ///     Collection of blocks that represents a switch blocks.
        /// </param>
        public ChoiceBlock(Func<IEvaluationContext, object> switchValue, IEnumerable<CaseBlock> cases)
        {
            this.SwitchValue = Requires.ArgumentNotNull(switchValue, nameof(switchValue));
            this.Cases = Requires.ArgumentNotNull(cases, nameof(cases));
        }

        /// <summary>
        ///     Gets a function that obtaines a switch value.
        /// </summary>
        public Func<IEvaluationContext, object> SwitchValue
        {
            get;
        }

        /// <summary>
        ///     Gets a collection of blocks that represents a switch blocks.
        /// </summary>
        public IEnumerable<CaseBlock> Cases
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
            var switchValue = this.SwitchValue(context);

            foreach (var caseBlock in this.Cases)
            {
                if (caseBlock.TestValues.Any(tv => IBinaryValue.Equals(tv, switchValue)))
                {
                    var result = caseBlock.Body.Process(context);
                    if (result.IsError)
                    {
                        return result;
                    }
                }
            }

            return Result.Success;
        }
    }
}
