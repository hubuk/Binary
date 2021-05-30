// <copyright file="IFieldWriter.cs" company="LeetABit">
//   Copyright (c) Hubert Bukowski. All rights reserved.
//   Licensed under the MIT License.
//   See LICENSE file in the project root for full license information.
// </copyright>

namespace LeetABit.Binary
{
    /// <summary>
    ///     Defines syntax available for objects that represents a binary value field writer.
    /// </summary>
    public interface IFieldWriter
    {
        /// <summary>
        ///     Writes a binary field value at the specified logical path.
        /// </summary>
        /// <param name="fieldPath">
        ///     Logical path to the field which value shall be written.
        /// </param>
        /// <param name="value">
        ///     Value for the binary field to be written.
        /// </param>
        /// <returns>
        ///     Object that represents write operation result.
        /// </returns>
        Result WriteField(LogicalPath fieldPath, object value);
    }
}
