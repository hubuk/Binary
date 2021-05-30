// <copyright file="BufferBlock.ContextWrapper.cs" company="LeetABit">
//   Copyright (c) Hubert Bukowski. All rights reserved.
//   Licensed under the MIT License.
//   See LICENSE file in the project root for full license information.
// </copyright>

namespace LeetABit.Binary.Blocks
{
    using System;
    using LeetABit.Binary.Properties;

    /// <content>
    ///     Represents a decorator that limits binary source to a buffer of the specified length.
    /// </content>
    public partial class BufferBlock
    {
        /// <summary>
        ///     Decorates specified coding context with a functionality that limits binary data access to a specified buffer.
        /// </summary>
        private class ContextWrapper : DecoratorContext
        {
            /// <summary>
            ///     Start position of the limit buffer.
            /// </summary>
            private readonly int startPosition;

            /// <summary>
            ///     Length of the limit buffer.
            /// </summary>
            private readonly int length;

            /// <summary>
            ///     Initializes a new instance of the <see cref="ContextWrapper"/> class.
            /// </summary>
            /// <param name="inner">
            ///     Decorated coding context instance.
            /// </param>
            /// <param name="length">
            ///     Length of the limit buffer.
            /// </param>
            public ContextWrapper(ICodingContext inner, int length)
                : base(inner)
            {
                this.startPosition = inner.Position;
                this.length = Requires.ArgumentGreaterThanZero(length, nameof(length));
            }

            /// <summary>
            ///     Gets a current binary position.
            /// </summary>
            public override int Position
            {
                get
                {
                    return this.Inner.Position - this.startPosition;
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
                return this.EnsureWithinLimit(offset).ContinueWith(this.Inner.Move(offset));
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
            public override Result MapField(LogicalPath fieldPath, int length, IBinaryValueConverter converter, object? defaultValue)
            {
                return this.EnsureWithinLimit(length).ContinueWith(base.MapField(fieldPath, length, converter, defaultValue));
            }

            /// <summary>
            ///     Verifies if the specified offset is lying outside of the limit buffer.
            /// </summary>
            /// <param name="offset">
            ///     Binary offset to verify.
            /// </param>
            /// <returns>
            ///     Verification result.
            /// </returns>
            private Result EnsureWithinLimit(int offset)
            {
                int newPosition = this.Position + offset;
                return newPosition >= 0 && newPosition <= this.length
                    ? Result.Success
                    : Result.FromException(new InvalidOperationException(Resources.Exception_InvalidOperation_OffsetOutsideLimit));
            }
        }
    }
}
