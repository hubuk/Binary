// <copyright file="CaseBlock.cs" company="LeetABit">
//   Copyright (c) Hubert Bukowski. All rights reserved.
//   Licensed under the MIT License.
//   See LICENSE file in the project root for full license information.
// </copyright>

namespace LeetABit.Binary.Blocks
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    ///     Represents a switch's case block.
    /// </summary>
    public class CaseBlock
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="CaseBlock"/> class.
        /// </summary>
        /// <param name="testValues">
        ///     Function that obtains a collection of values for the case block comparison.
        /// </param>
        /// <param name="body">
        ///     Definition block that shall be processed when the current case's test value matches switch's value.
        /// </param>
        public CaseBlock(IEnumerable<Func<IEvaluationContext, object>> testValues, IDefinitionBlock body)
        {
            this.TestValues = Requires.ArgumentNotNull(testValues, nameof(testValues));
            this.Body = Requires.ArgumentNotNull(body, nameof(body));
        }

        /// <summary>
        ///     Gets a function that obtains a collection of values for the case block comparison.
        /// </summary>
        public IEnumerable<Func<IEvaluationContext, object>> TestValues
        {
            get;
        }

        /// <summary>
        ///     Gets a definition block that shall be processed when the current case's test value matches switch's value.
        /// </summary>
        public IDefinitionBlock Body
        {
            get;
        }
    }
}
