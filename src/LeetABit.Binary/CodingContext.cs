// <copyright file="CodingContext.cs" company="LeetABit">
//   Copyright (c) Hubert Bukowski. All rights reserved.
//   Licensed under the MIT License.
//   See LICENSE file in the project root for full license information.
// </copyright>

namespace LeetABit.Binary
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    ///     Base implementation of the binary coding context.
    /// </summary>
    public abstract class CodingContext : ICodingContext
    {
        /// <summary>
        ///     Variables storage.
        /// </summary>
        private readonly TransactionalStorage<string, object?> variables = new();

        /// <summary>
        ///     Fields mapping storage.
        /// </summary>
        private readonly TransactionalStorage<LogicalPath, FieldMapping> fields = new();

        /// <summary>
        ///     Block's data storage.
        /// </summary>
        private readonly TransactionalStorage<IDefinitionBlock, object> blocksData = new();

        /// <summary>
        ///     Gets or sets a current logical path.
        /// </summary>
        public virtual LogicalPath Path
        {
            get;
            protected set;
        }

        /// <summary>
        ///     Gets a current binary position.
        /// </summary>
        public abstract int Position
        {
            get;
        }

        /// <summary>
        ///     Changes the current logical path.
        /// </summary>
        /// <param name="path">
        ///     New logical path to be set.
        /// </param>
        public virtual void ChangePath(LogicalPath path)
        {
            this.Path /= path;
        }

        /// <summary>
        ///     Moves current binary position specified number of bits.
        /// </summary>
        /// <param name="offset">
        ///     Number of bits to move the current binary position.
        /// </param>
        /// <returns>
        ///     Operation result.
        /// </returns>
        public abstract Result Move(int offset);

        /// <summary>
        ///     Gets a current value of the specified variable.
        /// </summary>
        /// <param name="variableName">
        ///     Name of the variable which value shall be obtained.
        /// </param>
        /// <returns>
        ///     Current value of the specified variable or error.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="variableName"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="ArgumentException">
        ///     <paramref name="variableName"/> is composed of white spaces only.
        /// </exception>
        public virtual Result<object?> GetVariable(string variableName)
        {
            _ = Requires.ArgumentNotNullAndNotWhiteSpace(variableName, nameof(variableName));

            return this.variables.Get(variableName);
        }

        /// <summary>
        ///     Sets a current value of the specified variable.
        /// </summary>
        /// <param name="variableName">
        ///     Name of the variable which value shall be obtained.
        /// </param>
        /// <param name="value">
        ///     A new variable value.
        /// </param>
        public virtual void SetVariable(string variableName, object? value)
        {
            _ = Requires.ArgumentNotNullAndNotWhiteSpace(variableName, nameof(variableName));

            this.variables.Set(variableName, value);
        }

        /// <summary>
        ///     Gets a binary value mapped to the specified logical path.
        /// </summary>
        /// <param name="fieldPath">
        ///     Logical path which binary value shall be obtained.
        /// </param>
        /// <returns>
        ///     Information about mapped binary value or error.
        /// </returns>
        /// <exception cref="ArgumentException">
        ///     <paramref name="fieldPath"/> is a root path.
        ///     <para>-or-</para>
        ///     <paramref name="fieldPath"/> is a relative path.
        /// </exception>
        public virtual Result<FieldMapping> GetFieldMapping(LogicalPath fieldPath)
        {
            _ = Requires.ArgumentNotRootAbsolutePath(fieldPath, nameof(fieldPath));

            return this.fields.Get(this.Path / fieldPath);
        }

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
        public virtual Result MapField(LogicalPath fieldPath, int length, IBinaryValueConverter converter, object? defaultValue)
        {
            _ = Requires.ArgumentNotRootAbsolutePath(fieldPath, nameof(fieldPath));
            _ = Requires.ArgumentGreaterThanZero(length, nameof(length));

            return this.CreateFieldMapping(this.Path / fieldPath, length, converter, defaultValue)
                .ContinueWith(mapping => this.fields.Add(mapping!.Path, mapping));
        }

        /// <summary>
        ///     Retrieves previously stored binary definition block's data.
        /// </summary>
        /// <param name="block">
        ///     Block which value to be retrieved.
        /// </param>
        /// <returns>
        ///     Retrieved block's data or error.
        /// </returns>
        public virtual Result<object> RetrieveBlockData(IDefinitionBlock block)
        {
            _ = Requires.ArgumentNotNull(block, nameof(block));

            return this.blocksData.Get(block);
        }

        /// <summary>
        ///     Stores binary definition block's data.
        /// </summary>
        /// <param name="block">
        ///     Block which value is to be stored.
        /// </param>
        /// <param name="data">
        ///     Value to be stored.
        /// </param>
        public virtual void StoreBlockData(IDefinitionBlock block, object? data)
        {
            _ = Requires.ArgumentNotNull(block, nameof(block));

            this.blocksData.Set(block, data);
        }

        /// <summary>
        ///     Starts registering all the revertable changes and make them permanent or revert them on transaction disposition.
        /// </summary>
        /// <returns>
        ///     A transaction object that shall be used to control committment or rollback of the cahnges.
        /// </returns>
        public virtual Transaction BeginTransaction()
        {
            TransactionActions actions = this.PrepareTransaction();
            return new Transaction(actions);
        }

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
        ///     Field mapping or error.
        /// </returns>
        protected abstract Result<FieldMapping> CreateFieldMapping(LogicalPath fieldPath, int length, IBinaryValueConverter converter, object? defaultValue);

        /// <summary>
        ///     Prepares actions for the transaction that is abaout to be started.
        /// </summary>
        /// <returns>
        ///     Transaction actions for the prepared transaction.
        /// </returns>
        protected virtual TransactionActions PrepareTransaction()
        {
            var result = new TransactionActions();
            var disposables = new List<IDisposable>();

            var pathClone = this.Path;
            result.RegisterRolbackAction(() => this.Path = pathClone);

            try
            {
                disposables.Add(RegisterTransaction(this.variables.BeginTransaction(), result));
                disposables.Add(RegisterTransaction(this.fields.BeginTransaction(), result));
                disposables.Add(RegisterTransaction(this.blocksData.BeginTransaction(), result));
            }
            catch
            {
                foreach (var disposable in disposables)
                {
                    disposable.Dispose();
                }

                throw;
            }

            return result;
        }

        /// <summary>
        ///     Registers specfied trnsaction in a transaction actions.
        /// </summary>
        /// <param name="transaction">
        ///     Transaction to register.
        /// </param>
        /// <param name="actions">
        ///     Transaction actions in which the transaction shall be registered.
        /// </param>
        /// <returns>
        ///     <paramref name="transaction"/> parameter.
        /// </returns>
        private static Transaction RegisterTransaction(Transaction transaction, TransactionActions actions)
        {
            actions.RegisterTransaction(transaction);
            return transaction;
        }
    }
}
