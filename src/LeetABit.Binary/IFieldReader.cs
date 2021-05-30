// <copyright file="IFieldReader.cs" company="LeetABit">
//   Copyright (c) Hubert Bukowski. All rights reserved.
//   Licensed under the MIT License.
//   See LICENSE file in the project root for full license information.
// </copyright>

namespace LeetABit.Binary
{
    /// <summary>
    ///     Defines syntax available for objects that represents a binary value field reader.
    /// </summary>
    public interface IFieldReader
    {
        /// <summary>
        ///     Reads a binary value field under specified logical path.
        /// </summary>
        /// <param name="fieldPath">
        ///     Logical path to the field which value shall be obtained.
        /// </param>
        /// <returns>
        ///     Value of the binary field located under specified logical path.
        /// </returns>
        Result<object> ReadField(LogicalPath fieldPath);
    }
}
