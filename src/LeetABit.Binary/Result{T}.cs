// <copyright file="Result{T}.cs" company="LeetABit">
//   Copyright (c) Hubert Bukowski. All rights reserved.
//   Licensed under the MIT License.
//   See LICENSE file in the project root for full license information.
// </copyright>

namespace LeetABit.Binary
{
    using System;
    using System.Runtime.ExceptionServices;

    /// <summary>
    ///     Represents an outcome of an operation. The outcome may be either return value or an exception.
    /// </summary>
    /// <typeparam name="T">
    ///     Type of the operation result.
    /// </typeparam>
    public sealed class Result<T> : Result
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="Result{T}"/> class.
        /// </summary>
        /// <param name="value">
        ///     Successfull operation return value.
        /// </param>
        internal Result(T? value)
            : base(null)
        {
            this.Value = value;
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="Result{T}"/> class.
        /// </summary>
        /// <param name="exception">
        ///     Exception that caused the operation to fail or <see langword="null"/> for a successfully executed operation.
        /// </param>
        internal Result(Exception exception)
            : base(exception)
        {
            Requires.ArgumentNotNull(exception, nameof(exception));

            this.Value = default;
        }

        /// <summary>
        ///     Gets a return value of the operation if finished successfully.
        /// </summary>
        public T? Value
        {
            get;
        }

        /// <summary>
        ///     Returns a human readible string representation of the operation outcome.
        /// </summary>
        /// <returns>
        ///     A human readible string representation of the operation outcome.
        /// </returns>
        public override string ToString()
        {
            return this.IsError ? $"Exception: {this.Exception}" : $"Return value: {this.Value}";
        }

        /// <summary>
        ///     Returns operation's return value or throws an exception passed as an operation error cause.
        /// </summary>
        /// <returns>
        ///     Operation's return value.
        /// </returns>
        public new T? Unwrap()
        {
            if (this.Exception is not null)
            {
                ExceptionDispatchInfo.Capture(this.Exception).Throw();
            }

            return this.Value;
        }
    }
}
