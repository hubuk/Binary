// <copyright file="ITransactional.cs" company="LeetABit">
//   Copyright (c) Hubert Bukowski. All rights reserved.
//   Licensed under the MIT License.
//   See LICENSE file in the project root for full license information.
// </copyright>

namespace LeetABit.Binary
{
    /// <summary>
    ///     Provides a mechanism for reverting object changes to the state from before transaction.
    /// </summary>
    public interface ITransactional
    {
        /// <summary>
        ///     Starts registering all the revertable changes and make them permanent or revert them on transaction disposition.
        /// </summary>
        /// <returns>
        ///     A transaction object that shall be used to control committment or rollback of the cahnges.
        /// </returns>
        Transaction BeginTransaction();
    }
}
