// <copyright file="FieldBlock.cs" company="LeetABit">
//   Copyright (c) Hubert Bukowski. All rights reserved.
//   Licensed under the MIT License.
//   See LICENSE file in the project root for full license information.
// </copyright>

namespace LeetABit.Binary.Blocks
{
    using System;

    /// <summary>
    ///     Represents field definition block.
    /// </summary>
    public class FieldBlock : IDefinitionBlock
    {
        /// <summary>
        ///     Function that obtains a path to the defined field.
        /// </summary>
        private readonly Func<IEvaluationContext, LogicalPath> fieldPath;

        /// <summary>
        ///     Function that obtains a length of the field.
        /// </summary>
        private readonly Func<IEvaluationContext, int> length;

        /// <summary>
        ///     Function that obtains a default value for the field.
        /// </summary>
        private readonly Func<IEvaluationContext, object> defaultValue;

        /// <summary>
        ///     Function that obtains a field value converter.
        /// </summary>
        private readonly Func<IEvaluationContext, IBinaryValueConverter> converter;

        /// <summary>
        /// Initializes a new instance of the <see cref="FieldBlock"/> class.
        /// </summary>
        /// <param name="fieldPath">
        ///     Function that obtains a path to the defined field.
        /// </param>
        /// <param name="length">
        ///     Function that obtains a length of the field.
        /// </param>
        /// <param name="defaultValue">
        ///     Function that obtains a default value for the field.
        /// </param>
        /// <param name="converter">
        ///     Function that obtains a field value converter.
        /// </param>
        public FieldBlock(
            Func<IEvaluationContext, LogicalPath> fieldPath,
            Func<IEvaluationContext, int> length,
            Func<IEvaluationContext, object> defaultValue,
            Func<IEvaluationContext, IBinaryValueConverter> converter)
        {
            this.fieldPath = Requires.ArgumentNotNull(fieldPath, nameof(fieldPath));
            this.length = Requires.ArgumentNotNull(length, nameof(length));
            this.defaultValue = Requires.ArgumentNotNull(defaultValue, nameof(defaultValue));
            this.converter = Requires.ArgumentNotNull(converter, nameof(converter));
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

            return context.MapField(this.fieldPath(context), this.length(context), this.converter(context), this.defaultValue(context));
        }
    }
}
