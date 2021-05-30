// <copyright file="TransactionalStorage{TKey,TValue}.cs" company="LeetABit">
//   Copyright (c) Hubert Bukowski. All rights reserved.
//   Licensed under the MIT License.
//   See LICENSE file in the project root for full license information.
// </copyright>

namespace LeetABit.Binary
{
    using System;
    using System.Collections.Generic;
    using LeetABit.Binary.Properties;

    /// <summary>
    ///     Represents a key-value storage with additional transaction support.
    /// </summary>
    /// <typeparam name="TKey">
    ///     Type of the storage keys.
    /// </typeparam>
    /// <typeparam name="TValue">
    ///     Type of the storage values.
    /// </typeparam>
    public class TransactionalStorage<TKey, TValue> : ITransactional
        where TKey : notnull
    {
        /// <summary>
        ///     Inned dictionary storage.
        /// </summary>
        private readonly IDictionary<TKey, TValue?> inner = new Dictionary<TKey, TValue?>();

        /// <summary>
        ///     Gets a value added under specified key or an error if the key is not found in the collection.
        /// </summary>
        /// <param name="key">
        ///     Key under which the value is to be found.
        /// </param>
        /// <returns>
        ///     Value added under specified key or an error if the key is not found in the collection.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="key"/> is <see langword="null"/>.
        /// </exception>
        public Result<TValue> Get(TKey key)
        {
            _ = Requires.ArgumentNotNull(key, nameof(key));

            return this.inner.TryGetValue(key, out TValue? value)
                ? Result.FromReturnValue(value)
                : Result.FromException<TValue>(new KeyNotFoundException(Resources.Exception_KeyNotFound_StorageKeyMissing));
        }

        /// <summary>
        ///     Sets a specified value under specified key in the storage.
        /// </summary>
        /// <param name="key">
        ///     Key under which the value shall be stored.
        /// </param>
        /// <param name="value">
        ///     Value to be stored.
        /// </param>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="key"/> is <see langword="null"/>.
        /// </exception>
        public void Set(TKey key, TValue? value)
        {
            _ = Requires.ArgumentNotNull(key, nameof(key));

            this.inner[key] = value;
        }

        /// <summary>
        ///     Adds a specified value under specified key in the storage or throws an exception if a value is already stored under the specified key.
        /// </summary>
        /// <param name="key">
        ///     Key under which the value shall be stored.
        /// </param>
        /// <param name="value">
        ///     Value to be stored.
        /// </param>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="key"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="ArgumentException">
        ///     An item with the same key has already been added.
        /// </exception>
        public void Add(TKey key, TValue? value)
        {
            _ = Requires.ArgumentNotNull(key, nameof(key));

            if (!this.inner.TryAdd(key, value))
            {
                throw new ArgumentException(Resources.Exception_Argument_DuplicateKey, nameof(key));
            }
        }

        /// <summary>
        ///     Removes a value under specified key from the storage if found.
        /// </summary>
        /// <param name="key">
        ///     Key of the value to be removed.
        /// </param>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="key"/> is <see langword="null"/>.
        /// </exception>
        public void Remove(TKey key)
        {
            _ = Requires.ArgumentNotNull(key, nameof(key));

            _ = this.inner.Remove(key);
        }

        /// <summary>
        ///     Starts registering all the revertable changes and make them permanent or revert them on transaction disposition.
        /// </summary>
        /// <returns>
        ///     A transaction object that shall be used to control committment or rollback of the cahnges.
        /// </returns>
        public Transaction BeginTransaction()
        {
            var data = this.CloneData();
            var actions = new TransactionActions();
            actions.RegisterRolbackAction(() => this.Restore(data));
            return new Transaction(actions);
        }

        /// <summary>
        ///     Clones all the stored data.
        /// </summary>
        /// <returns>
        ///     Cloned data.
        /// </returns>
        private IEnumerable<KeyValuePair<TKey, TValue?>> CloneData()
        {
            return new Dictionary<TKey, TValue?>(this.inner);
        }

        /// <summary>
        ///     Replaces all the currently stored data with the specified collection.
        /// </summary>
        /// <param name="data">
        ///     Data to be restored.
        /// </param>
        private void Restore(IEnumerable<KeyValuePair<TKey, TValue?>> data)
        {
            this.inner.Clear();
            foreach (var pair in data)
            {
                this.inner.Add(pair);
            }
        }
    }
}
