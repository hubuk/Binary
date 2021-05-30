// <copyright file="DefferedBinaryWriter.cs" company="LeetABit">
//   Copyright (c) Hubert Bukowski. All rights reserved.
//   Licensed under the MIT License.
//   See LICENSE file in the project root for full license information.
// </copyright>

namespace LeetABit.Binary
{
    using System.Collections.Generic;

    /// <summary>
    ///     Represents a binary writer with a transactional capabilities.
    /// </summary>
    public sealed class DefferedBinaryWriter : ITransactionalBinaryWriter
    {
        /// <summary>
        ///     A reference to the inner writer.
        /// </summary>
        private readonly IBinaryWriter inner;

        /// <summary>
        ///     List with all the deffered values.
        /// </summary>
        private readonly List<IBinaryValue> defferedValues = new();

        /// <summary>
        ///     Initializes a new instance of the <see cref="DefferedBinaryWriter"/> class.
        /// </summary>
        /// <param name="inner">
        ///     Inner writer.
        /// </param>
        public DefferedBinaryWriter(IBinaryWriter inner)
        {
            this.inner = Requires.ArgumentNotNull(inner, nameof(inner));
        }

        /// <summary>
        ///     Gets the current bit position in the binary data.
        /// </summary>
        public int Position
        {
            get
            {
                return this.inner.Position;
            }
        }

        /// <summary>
        ///     Changes position within binary data.
        /// </summary>
        /// <param name="offset">
        ///     Number of bits to move the position. Positive value moves position away from the first transmitted bit.
        /// </param>
        /// <returns>
        ///     Operation result.
        /// </returns>
        public Result Move(int offset)
        {
            return this.inner.Move(offset);
        }

        /// <summary>
        ///     Write specified binary value and move current position by the length of the data written.
        /// </summary>
        /// <param name="value">
        ///     Binary value to write.
        /// </param>
        /// <returns>
        ///     Result of the operatoin.
        /// </returns>
        public Result Write(IBinaryValue value)
        {
            _ = Requires.ArgumentNotNull(value, nameof(value));

            this.defferedValues.Add(value);
            return this.inner.Move(value.Length);
        }

        /// <summary>
        ///     Starts registering all the revertable changes and make them permanent or revert them on transaction disposition.
        /// </summary>
        /// <returns>
        ///     A transaction object that shall be used to control committment or rollback of the cahnges.
        /// </returns>
        public Transaction BeginTransaction()
        {
            var actions = new TransactionActions();
            actions.RegisterCommitAction(this.Commit);
            return new Transaction(actions);
        }

        /// <summary>
        ///     Commits all the deffered changes to the inner writter.
        /// </summary>
        private void Commit()
        {
            foreach (var value in this.defferedValues)
            {
                this.inner.Write(value).Unwrap();
            }
        }
    }
}
