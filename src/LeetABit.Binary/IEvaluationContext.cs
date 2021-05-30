// <copyright file="IEvaluationContext.cs" company="LeetABit">
//   Copyright (c) Hubert Bukowski. All rights reserved.
//   Licensed under the MIT License.
//   See LICENSE file in the project root for full license information.
// </copyright>

namespace LeetABit.Binary
{
    /// <summary>
    ///     Defines syntax available for objects that represents a context for expression evaluation during coding.
    /// </summary>
    public interface IEvaluationContext
    {
        /// <summary>
        ///     Gets a current logical path.
        /// </summary>
        LogicalPath Path
        {
            get;
        }

        /// <summary>
        ///     Gets a current binary position.
        /// </summary>
        int Position
        {
            get;
        }

        /// <summary>
        ///     Gets a current value of the specified variable.
        /// </summary>
        /// <param name="variableName">
        ///     Name of the variable which value shall be obtained.
        /// </param>
        /// <returns>
        ///     Current value of the specified variable or error.
        /// </returns>
        Result<object?> GetVariable(string variableName);

        /// <summary>
        ///     Gets a binary value mapped to the specified logical path.
        /// </summary>
        /// <param name="fieldPath">
        ///     Logical path which binary value shall be obtained.
        /// </param>
        /// <returns>
        ///     Information about mapped binary value or error.
        /// </returns>
        Result<FieldMapping> GetFieldMapping(LogicalPath fieldPath);
    }
}
