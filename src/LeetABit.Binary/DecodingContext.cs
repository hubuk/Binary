// <copyright file="DecodingContext.cs" company="LeetABit">
//   Copyright (c) Hubert Bukowski. All rights reserved.
//   Licensed under the MIT License.
//   See LICENSE file in the project root for full license information.
// </copyright>

namespace LeetABit.Binary
{
    /// <summary>
    ///     Binary context used for decoding values from binary to logical form.
    /// </summary>
    public class DecodingContext : CodingContext
    {
        /// <summary>
        ///     Field value reader that uses binary source.
        /// </summary>
        private readonly IBinaryReader binary;

        /// <summary>
        ///     Field value writer that uses logical destination.
        /// </summary>
        private readonly ITransactionalFieldWriter fields;

        /// <summary>
        ///     Initializes a new instance of the <see cref="DecodingContext"/> class.
        /// </summary>
        /// <param name="binary">
        ///     Field value reader that uses binary source.
        /// </param>
        /// <param name="fields">
        ///     Field value writer that uses logical destination.
        /// </param>
        public DecodingContext(IBinaryReader binary, ITransactionalFieldWriter fields)
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

            return this.binary.Read(length)
                .ContinueWith(value => converter.ConvertFrom(this, value!).OnError(defaultValue)
                    .ContinueWith(convertedValue => this.fields.WriteField(fieldPath, convertedValue!)
                        .ContinueWith(new FieldMapping(fieldPath, position, value!, convertedValue!))));
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

            var position = this.binary.Position;
            var fieldTransaction = this.fields.BeginTransaction();

            actions.RegisterRolbackAction(() => this.binary.Move(position - this.binary.Position));
            actions.RegisterTransaction(fieldTransaction);
            return actions;
        }
    }
}
