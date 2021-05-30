// <copyright file="ICodingContext.cs" company="LeetABit">
//   Copyright (c) Hubert Bukowski. All rights reserved.
//   Licensed under the MIT License.
//   See LICENSE file in the project root for full license information.
// </copyright>

namespace LeetABit.Binary
{
    /// <summary>
    ///     Defines syntax available for objects that represents a context for binary coding.
    /// </summary>
    public interface ICodingContext : IEvaluationContext, ITransactional
    {
        /// <summary>
        ///     Changes the current logical path.
        /// </summary>
        /// <param name="path">
        ///     New logical path to be set.
        /// </param>
        void ChangePath(LogicalPath path);

        /// <summary>
        ///     Moves current binary position specified number of bits.
        /// </summary>
        /// <param name="offset">
        ///     Number of bits to move the current binary position.
        /// </param>
        /// <returns>
        ///     Operation result.
        /// </returns>
        Result Move(int offset);

        /// <summary>
        ///     Sets a current value of the specified variable.
        /// </summary>
        /// <param name="variableName">
        ///     Name of the variable which value shall be obtained.
        /// </param>
        /// <param name="value">
        ///     A new variable value.
        /// </param>
        void SetVariable(string variableName, object? value);

        /// <summary>
        ///     Maps binary field value to current coding state.
        /// </summary>
        /// <param name="fieldPath">
        ///     Path of the field which value to be mapped.
        /// </param>
        /// <param name="length">
        ///     Binary field length.
        /// </param>
        /// <param name="converter">
        ///     Field's value converter.
        /// </param>
        /// <param name="defaultValue">
        ///     Field's default value.
        /// </param>
        /// <returns>
        ///     Operation result.
        /// </returns>
        Result MapField(LogicalPath fieldPath, int length, IBinaryValueConverter converter, object? defaultValue);

        /// <summary>
        ///     Retrieves previously stored binary definition block's data.
        /// </summary>
        /// <param name="block">
        ///     Block which value to be retrieved.
        /// </param>
        /// <returns>
        ///     Retrieved block's data or error.
        /// </returns>
        Result<object> RetrieveBlockData(IDefinitionBlock block);

        /// <summary>
        ///     Stores binary definition block's data.
        /// </summary>
        /// <param name="block">
        ///     Block which value is to be stored.
        /// </param>
        /// <param name="data">
        ///     Value to be stored.
        /// </param>
        void StoreBlockData(IDefinitionBlock block, object data);
    }
}
