// <copyright file="IBinaryValueConverter.cs" company="LeetABit">
//   Copyright (c) Hubert Bukowski. All rights reserved.
//   Licensed under the MIT License.
//   See LICENSE file in the project root for full license information.
// </copyright>

namespace LeetABit.Binary
{
    /// <summary>
    ///     Defines syntax available for objects that represents a binary value converter.
    /// </summary>
    public interface IBinaryValueConverter
    {
        /// <summary>
        ///     Performs conversion from binary value to a target type.
        /// </summary>
        /// <param name="context">
        ///     Current evaluation context.
        /// </param>
        /// <param name="value">
        ///     Value to convert.
        /// </param>
        /// <returns>
        ///     Conversion result or error.
        /// </returns>
        Result<object> ConvertFrom(IEvaluationContext context, IBinaryValue value);

        /// <summary>
        ///     Performs conversion from a source type to a binary value.
        /// </summary>
        /// <param name="context">
        ///     Current evaluation context.
        /// </param>
        /// <param name="value">
        ///     Value to convert.
        /// </param>
        /// <param name="bitLength">
        ///     Expected bit length of the binary value.
        /// </param>
        /// <returns>
        ///     Conversion result or error.
        /// </returns>
        Result<IBinaryValue> ConvertTo(IEvaluationContext context, object? value, int bitLength);
    }
}
