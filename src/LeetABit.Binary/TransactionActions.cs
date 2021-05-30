// <copyright file="TransactionActions.cs" company="LeetABit">
//   Copyright (c) Hubert Bukowski. All rights reserved.
//   Licensed under the MIT License.
//   See LICENSE file in the project root for full license information.
// </copyright>

namespace LeetABit.Binary
{
    using System;

    /// <summary>
    ///     Represents storage for actions executed on transaction finalization.
    /// </summary>
    public sealed class TransactionActions
    {
        /// <summary>
        ///     Gets an action that will be executed on transaction rollback.
        /// </summary>
        public Action RollbackAction { get; private set; } = () => { };

        /// <summary>
        ///     Gets an action that will be executed on transaction rollback.
        /// </summary>
        public Action CommitAction { get; private set; } = () => { };

        /// <summary>
        ///     Gets an action that will be executed on transaction finalization after rollback or commitment logic.
        /// </summary>
        public Action FinalizeAction { get; private set; } = () => { };

        /// <summary>
        ///     Registers an action for execution on transaction rollback.
        /// </summary>
        /// <param name="rollbackAction">
        ///     Action to register.
        /// </param>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="rollbackAction"/> is <see langword="null"/>.
        /// </exception>
        public void RegisterRolbackAction(Action rollbackAction)
        {
            _ = Requires.ArgumentNotNull(rollbackAction, nameof(rollbackAction));

            this.RollbackAction += rollbackAction;
        }

        /// <summary>
        ///     Registers an action for execution on transaction commitment.
        /// </summary>
        /// <param name="commitAction">
        ///     Action to register.
        /// </param>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="commitAction"/> is <see langword="null"/>.
        /// </exception>
        public void RegisterCommitAction(Action commitAction)
        {
            _ = Requires.ArgumentNotNull(commitAction, nameof(commitAction));

            this.CommitAction += commitAction;
        }

        /// <summary>
        ///     Registers an action for execution on transaction finalization after rollback or commitment logic.
        /// </summary>
        /// <param name="finalizeAction">
        ///     Action to register.
        /// </param>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="finalizeAction"/> is <see langword="null"/>.
        /// </exception>
        public void RegisterFinalizeAction(Action finalizeAction)
        {
            _ = Requires.ArgumentNotNull(finalizeAction, nameof(finalizeAction));

            this.FinalizeAction += finalizeAction;
        }

        /// <summary>
        ///     Registers specified transaction to be finalized when a transaction to which current object is assigned will be finalized.
        /// </summary>
        /// <param name="transaction">
        ///     Transaction to register.
        /// </param>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="transaction"/> is <see langword="null"/>.
        /// </exception>
        public void RegisterTransaction(Transaction transaction)
        {
            _ = Requires.ArgumentNotNull(transaction, nameof(transaction));

            this.RegisterCommitAction(() => transaction.Commit());
            this.RegisterRolbackAction(() => transaction.Rollback());
            this.RegisterFinalizeAction(() => transaction.Dispose());
        }
    }
}
