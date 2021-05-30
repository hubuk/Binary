// <copyright file="ITransactionalBinaryWriter.cs" company="LeetABit">
//   Copyright (c) Hubert Bukowski. All rights reserved.
//   Licensed under the MIT License.
//   See LICENSE file in the project root for full license information.
// </copyright>

namespace LeetABit.Binary
{
    /// <summary>
    ///     Defines syntax available for objects that represents a transactional binary writer.
    /// </summary>
    public interface ITransactionalBinaryWriter : IBinaryWriter, ITransactional
    {
    }
}
