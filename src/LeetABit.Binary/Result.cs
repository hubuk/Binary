// <copyright file="Result.cs" company="LeetABit">
//   Copyright (c) Hubert Bukowski. All rights reserved.
//   Licensed under the MIT License.
//   See LICENSE file in the project root for full license information.
// </copyright>

namespace LeetABit.Binary
{
    using System;
    using System.Runtime.ExceptionServices;
    using System.Threading.Tasks;

    /// <summary>
    ///     Represents an outcome of an operation. The outcome may be either success or an exception.
    /// </summary>
    public class Result
    {
        /// <summary>
        ///     Represents a result of a successfully executed operation.
        /// </summary>
        public static readonly Result Success = new(null);

        /// <summary>
        ///     Initializes a new instance of the <see cref="Result"/> class.
        /// </summary>
        /// <param name="exception">
        ///     Exception that caused the operation to fail or <see langword="null"/> for a successfully executed operation.
        /// </param>
        private protected Result(Exception? exception)
        {
            this.Exception = exception;
        }

        /// <summary>
        ///     Gets an exception that caused the operation to fail or <see langword="null"/> for a successfully executed operation.
        /// </summary>
        public Exception? Exception
        {
            get;
        }

        /// <summary>
        ///     Gets a value indicating whether the operation was executed successfully or not.
        /// </summary>
        public bool IsSuccess => this.Exception is null;

        /// <summary>
        ///     Gets a value indicating whether the operation was executed with error or not.
        /// </summary>
        public bool IsError => !this.IsSuccess;

        /// <summary>
        ///     Creates a new instance of the <see cref="Result"/> class from an exception that represents an operation execution error.
        /// </summary>
        /// <param name="exception">
        ///     Exception that represents an operation execution error.
        /// </param>
        /// <returns>
        ///     A newly created instance of the <see cref="Result"/> class from an exception that represents an operation execution error.
        /// </returns>
        public static Result FromException(Exception exception)
        {
            Requires.ArgumentNotNull(exception, nameof(exception));

            return new Result(exception);
        }

        /// <summary>
        ///     Creates a new instance of the <see cref="Result{T}"/> class from an exception that represents an operation execution error.
        /// </summary>
        /// <typeparam name="T">
        ///     Type of the expected operation result.
        /// </typeparam>
        /// <param name="exception">
        ///     Exception that represents an operation execution error.
        /// </param>
        /// <returns>
        ///     A newly created instance of the <see cref="Result{T}"/> class from an exception that represents an operation execution error.
        /// </returns>
        public static Result<T> FromException<T>(Exception exception)
        {
            Requires.ArgumentNotNull(exception, nameof(exception));

            return new Result<T>(exception);
        }

        /// <summary>
        ///     Creates a new instance of the <see cref="Result{T}"/> class from operation's return value.
        /// </summary>
        /// <typeparam name="T">
        ///     Type of the operation result.
        /// </typeparam>
        /// <param name="value">
        ///     Operation's return value.
        /// </param>
        /// <returns>
        ///     A newly created instance of the <see cref="Result{T}"/> class with a operation's return value.
        /// </returns>
        public static Result<T> FromReturnValue<T>(T? value)
        {
            return new Result<T>(value);
        }

        /// <summary>
        ///     Executes a specified action and captures its execution outcome as an instance of the <see cref="Result"/> class.
        /// </summary>
        /// <param name="action">
        ///     Action which execution result shall be captured.
        /// </param>
        /// <param name="exceptionFilter">
        ///     A function that determines whether the exception thrown shall be captured by the method.
        /// </param>
        /// <returns>
        ///     An instance of the <see cref="Result"/> class that represents outcome of the specified <paramref name="action"/> execution.
        /// </returns>
        public static Result Capture(Action action, Func<Exception, bool> exceptionFilter)
        {
            Requires.ArgumentNotNull(action, nameof(action));
            Requires.ArgumentNotNull(exceptionFilter, nameof(exceptionFilter));

            try
            {
                action();
            }
            catch (Exception e) when (exceptionFilter(e))
            {
                return FromException(e);
            }

            return Success;
        }

        /// <summary>
        ///     Executes a specified function and captures its execution outcome as an instance of the <see cref="Result{T}"/> class.
        /// </summary>
        /// <typeparam name="T">
        ///     Type of the function return value.
        /// </typeparam>
        /// <param name="func">
        ///     Function which execution result shall be captured.
        /// </param>
        /// <param name="exceptionFilter">
        ///     A function that determines whether the exception thrown shall be captured by the method.
        /// </param>
        /// <returns>
        ///     An instance of the <see cref="Result"/> class that represents outcome of the specified <paramref name="func"/> execution.
        /// </returns>
        public static Result<T> Capture<T>(Func<T?> func, Func<Exception, bool> exceptionFilter)
        {
            Requires.ArgumentNotNull(func, nameof(func));
            Requires.ArgumentNotNull(exceptionFilter, nameof(exceptionFilter));

            try
            {
                return Result.FromReturnValue<T>(func());
            }
            catch (Exception e) when (exceptionFilter(e))
            {
                return Result.FromException<T>(e);
            }
        }

        /// <summary>
        ///     Executes a specified action and captures its execution outcome as an instance of the <see cref="Result"/> class.
        /// </summary>
        /// <param name="action">
        ///     Action which execution result shall be captured.
        /// </param>
        /// <returns>
        ///     An instance of the <see cref="Result"/> class that represents outcome of the specified <paramref name="action"/> execution.
        /// </returns>
        public static Result Capture(Action action)
        {
            Requires.ArgumentNotNull(action, nameof(action));

            return Result.Capture(action, e => true);
        }

        /// <summary>
        ///     Executes a specified function and captures its execution outcome as an instance of the <see cref="Result{T}"/> class.
        /// </summary>
        /// <typeparam name="T">
        ///     Type of the function return value.
        /// </typeparam>
        /// <param name="func">
        ///     Function which execution result shall be captured.
        /// </param>
        /// <returns>
        ///     An instance of the <see cref="Result"/> class that represents outcome of the specified <paramref name="func"/> execution.
        /// </returns>
        public static Result<T> Capture<T>(Func<T?> func)
        {
            Requires.ArgumentNotNull(func, nameof(func));

            return Result.Capture(func, e => true);
        }

        /// <summary>
        ///     Asynchronously executes a specified action and captures its execution outcome as an instance of the <see cref="Result"/> class.
        /// </summary>
        /// <param name="action">
        ///     Action which execution result shall be captured.
        /// </param>
        /// <param name="exceptionFilter">
        ///     A function that determines whether the exception thrown shall be captured by the method.
        /// </param>
        /// <returns>
        ///     A task that represents an ongoing operation that returns an instance of the <see cref="Result"/> class that represents outcome of the specified <paramref name="action"/> execution.
        /// </returns>
        public static async Task<Result> CaptureAsync(Func<Task> action, Func<Exception, bool> exceptionFilter)
        {
            Requires.ArgumentNotNull(action, nameof(action));
            Requires.ArgumentNotNull(exceptionFilter, nameof(exceptionFilter));

            try
            {
                await action().ConfigureAwait(false);
            }
            catch (Exception e) when (exceptionFilter(e))
            {
                return FromException(e);
            }

            return Success;
        }

        /// <summary>
        ///     Asynchronously executes a specified function and captures its execution outcome as an instance of the <see cref="Result{T}"/> class.
        /// </summary>
        /// <typeparam name="T">
        ///     Type of the function return value.
        /// </typeparam>
        /// <param name="func">
        ///     Function which execution result shall be captured.
        /// </param>
        /// <param name="exceptionFilter">
        ///     A function that determines whether the exception thrown shall be captured by the method.
        /// </param>
        /// <returns>
        ///     A task that represents an ongoing operation that returns an instance of the <see cref="Result"/> class that represents outcome of the specified <paramref name="func"/> execution.
        /// </returns>
        public static async Task<Result<T>> CaptureAsync<T>(Func<Task<T>> func, Func<Exception, bool> exceptionFilter)
        {
            Requires.ArgumentNotNull(func, nameof(func));
            Requires.ArgumentNotNull(exceptionFilter, nameof(exceptionFilter));

            try
            {
                return Result.FromReturnValue<T>(await func().ConfigureAwait(false));
            }
            catch (Exception e) when (exceptionFilter(e))
            {
                return Result.FromException<T>(e);
            }
        }

        /// <summary>
        ///     Asynchronously executes a specified action and captures its execution outcome as an instance of the <see cref="Result"/> class.
        /// </summary>
        /// <param name="action">
        ///     Action which execution result shall be captured.
        /// </param>
        /// <returns>
        ///     A task that represents an ongoing operation that returns an instance of the <see cref="Result"/> class that represents outcome of the specified <paramref name="action"/> execution.
        /// </returns>
        public static async Task<Result> CaptureAsync(Func<Task> action)
        {
            Requires.ArgumentNotNull(action, nameof(action));

            return await Result.CaptureAsync(action, e => true).ConfigureAwait(true);
        }

        /// <summary>
        ///     Asynchronously executes a specified function and captures its execution outcome as an instance of the <see cref="Result{T}"/> class.
        /// </summary>
        /// <typeparam name="T">
        ///     Type of the function return value.
        /// </typeparam>
        /// <param name="func">
        ///     Function which execution result shall be captured.
        /// </param>
        /// <returns>
        ///     A task that represents an ongoing operation that returns an instance of the <see cref="Result"/> class that represents outcome of the specified <paramref name="func"/> execution.
        /// </returns>
        public static async Task<Result<T>> CaptureAsync<T>(Func<Task<T>> func)
        {
            Requires.ArgumentNotNull(func, nameof(func));

            return await Result.CaptureAsync(func, e => true).ConfigureAwait(true);
        }

        /// <summary>
        ///     Returns a human readible string representation of the operation outcome.
        /// </summary>
        /// <returns>
        ///     A human readible string representation of the operation outcome.
        /// </returns>
        public override string ToString()
        {
            return this.IsError ? $"Exception: {this.Exception}" : "Success";
        }

        /// <summary>
        ///     Throws an exception passed as an operation execution error cause.
        /// </summary>
        public void Unwrap()
        {
            if (this.Exception is not null)
            {
                ExceptionDispatchInfo.Capture(this.Exception).Throw();
            }
        }
    }
}
