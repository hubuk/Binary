// <copyright file="Transaction.cs" company="LeetABit">
//   Copyright (c) Hubert Bukowski. All rights reserved.
//   Licensed under the MIT License.
//   See LICENSE file in the project root for full license information.
// </copyright>

namespace LeetABit.Binary
{
    using System;

    /// <summary>
    ///     Represents a transaction that may be used to revert transactional changes made to a resource.
    /// </summary>
    public sealed class Transaction : IDisposable
    {
        /// <summary>
        ///     Holds a reference to actions for the transaction finalization.
        /// </summary>
        private readonly TransactionActions actions;

        /// <summary>
        ///     Holds a value that indicates whether all the transactional changes made to a resource should be commited on disposition.
        /// </summary>
        private bool shouldCommit;

        /// <summary>
        ///     Holds a value that indicatges whether the transaction has been already disposed.
        /// </summary>
        private bool disposed;

        /// <summary>
        ///     Initializes a new instance of the <see cref="Transaction"/> class.
        /// </summary>
        /// <param name="actions">
        ///     Actions for the transaction finalization.
        /// </param>
        public Transaction(TransactionActions actions)
        {
            this.actions = actions;
        }

        /// <summary>
        ///     Marks a transation to made all the transactional changes permanenet on disposition.
        /// </summary>
        /// <remarks>
        ///     When a transaction is disposed without calling this methods all the transactional cahnges are reverted.
        /// </remarks>
        public void Commit()
        {
            this.shouldCommit = true;
        }

        /// <summary>
        ///     Marks a transation to revert all the transactional changes on disposition.
        /// </summary>
        public void Rollback()
        {
            this.shouldCommit = false;
        }

        /// <summary>
        ///     Disposes current transaction committing or reverting all the changes.
        /// </summary>
        public void Dispose()
        {
            if (this.disposed)
            {
                return;
            }

            this.disposed = true;
            if (this.shouldCommit)
            {
                this.actions.CommitAction();
            }
            else
            {
                this.actions.RollbackAction();
            }

            this.actions.FinalizeAction();
        }
    }
}
