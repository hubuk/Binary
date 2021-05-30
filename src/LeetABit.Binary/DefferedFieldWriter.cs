// <copyright file="DefferedFieldWriter.cs" company="LeetABit">
//   Copyright (c) Hubert Bukowski. All rights reserved.
//   Licensed under the MIT License.
//   See LICENSE file in the project root for full license information.
// </copyright>

namespace LeetABit.Binary
{
    using System.Collections.Generic;

    /// <summary>
    ///     Represents a field writer with a transactional capabilities.
    /// </summary>
    public class DefferedFieldWriter : ITransactionalFieldWriter
    {
        /// <summary>
        ///     A reference to the inner writer.
        /// </summary>
        private readonly IFieldWriter inner;

        /// <summary>
        ///     List with all the deffered values.
        /// </summary>
        private readonly List<(LogicalPath, object)> defferedValues = new();

        /// <summary>
        ///     Initializes a new instance of the <see cref="DefferedFieldWriter"/> class.
        /// </summary>
        /// <param name="inner">
        ///     Inner writer.
        /// </param>
        public DefferedFieldWriter(IFieldWriter inner)
        {
            this.inner = Requires.ArgumentNotNull(inner, nameof(inner));
        }

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
        public Result WriteField(LogicalPath fieldPath, object value)
        {
            this.defferedValues.Add((fieldPath, value));
            return Result.Success;
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
            foreach (var (fieldPath, value) in this.defferedValues)
            {
                this.inner.WriteField(fieldPath, value).Unwrap();
            }
        }
    }
}
