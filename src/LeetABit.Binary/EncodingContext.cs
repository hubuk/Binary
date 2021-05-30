// <copyright file="EncodingContext.cs" company="LeetABit">
//   Copyright (c) Hubert Bukowski. All rights reserved.
//   Licensed under the MIT License.
//   See LICENSE file in the project root for full license information.
// </copyright>

namespace LeetABit.Binary
{
    /// <summary>
    ///     Binary context used for encoding values from logical to binary form.
    /// </summary>
    public class EncodingContext : CodingContext
    {
        /// <summary>
        ///     Field value writer that uses binary destination.
        /// </summary>
        private readonly ITransactionalBinaryWriter binary;

        /// <summary>
        ///     Field value reader that uses binary source.
        /// </summary>
        private readonly IFieldReader fields;

        /// <summary>
        ///     Initializes a new instance of the <see cref="EncodingContext"/> class.
        /// </summary>
        /// <param name="binary">
        ///     Field value writer that uses binary destination.
        /// </param>
        /// <param name="fields">
        ///     Field value reader that uses binary source.
        /// </param>
        public EncodingContext(ITransactionalBinaryWriter binary, IFieldReader fields)
        {
            this.binary = Requires.ArgumentNotNull(binary, nameof(binary));
            this.fields = Requires.ArgumentNotNull(fields, nameof(fields));
        }

        /// <summary>
        ///     Gets a current binary position.
        /// </summary>
        public override int Position
        {
            get
            {
                return this.binary.Position;
            }
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
        public override Result Move(int offset)
        {
            return this.binary.Move(offset);
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
        protected override Result<FieldMapping> CreateFieldMapping(LogicalPath fieldPath, int length, IBinaryValueConverter converter, object? defaultValue)
        {
            int position = this.binary.Position;

            return this.fields.ReadField(fieldPath).OnError(defaultValue)
                .ContinueWith(value => converter.ConvertTo(this, value, length)
                    .ContinueWith(convertedValue => this.binary.Write(convertedValue!)
                        .ContinueWith(new FieldMapping(fieldPath, position, convertedValue!, value!))));
        }

        /// <summary>
        ///     Prepares actions for the transaction that is abaout to be started.
        /// </summary>
        /// <returns>
        ///     Transaction actions for the prepared transaction.
        /// </returns>
        protected override TransactionActions PrepareTransaction()
        {
            TransactionActions actions = base.PrepareTransaction();
            var binaryTransaction = this.binary.BeginTransaction();
            actions.RegisterTransaction(binaryTransaction);
            return actions;
        }
    }
}
