// <copyright file="ResultExtensions.cs" company="LeetABit">
//   Copyright (c) Hubert Bukowski. All rights reserved.
//   Licensed under the MIT License.
//   See LICENSE file in the project root for full license information.
// </copyright>

namespace LeetABit.Binary
{
    using System;
    using System.Threading.Tasks;
    using LeetABit.Binary.Properties;

    /// <summary>
    ///     Extension methods for <see cref="Result"/> and <see cref="Result{T}"/> classes.
    /// </summary>
    public static class ResultExtensions
    {
        /// <summary>
        ///     Implements a monadic bind operation for void returning <see cref="Result"/> class instance.
        /// </summary>
        /// <param name="source">
        ///     Source result object.
        /// </param>
        /// <param name="selector">
        ///     Function that will be called in order to produce a new <see cref="Result"/> object when <paramref name="source"/> represents failed operation.
        /// </param>
        /// <returns>
        ///     <paramref name="source"/> if it represents failed operation or object returned by <paramref name="selector"/> function otherwise.
        /// </returns>
        public static Result Bind(this Result source, Func<Result> selector)
        {
            Requires.ArgumentNotNull(source, nameof(source));
            Requires.ArgumentNotNull(selector, nameof(selector));

            return source.IsError ? source : selector();
        }

        /// <summary>
        ///     Implements a monadic bind operation that returns <see cref="Result{T}"/> class instance.
        /// </summary>
        /// <typeparam name="TTarget">
        ///     Type of the target <see cref="Result{T}"/> object.
        /// </typeparam>
        /// <param name="source">
        ///     Source result object.
        /// </param>
        /// <param name="selector">
        ///     Function that will be called in order to produce a new <see cref="Result{T}"/> object when <paramref name="source"/> represents failed operation.
        /// </param>
        /// <returns>
        ///     <paramref name="source"/> if it represents failed operation or object returned by <paramref name="selector"/> function otherwise.
        /// </returns>
        public static Result<TTarget> Bind<TTarget>(this Result source, Func<Result<TTarget>> selector)
        {
            Requires.ArgumentNotNull(source, nameof(source));
            Requires.ArgumentNotNull(selector, nameof(selector));

            return source.IsError ? Result.FromException<TTarget>(source.Exception!) : selector();
        }

        /// <summary>
        ///     Implements a monadic bind operation that returns void returning <see cref="Result"/> class instance.
        /// </summary>
        /// <typeparam name="TSource">
        ///     Type of the source <see cref="Result{T}"/> object.
        /// </typeparam>
        /// <param name="source">
        ///     Source result object.
        /// </param>
        /// <param name="selector">
        ///     Function that will be called in order to produce a new <see cref="Result"/> object when <paramref name="source"/> represents failed operation.
        /// </param>
        /// <returns>
        ///     <paramref name="source"/> if it represents failed operation or object returned by <paramref name="selector"/> function otherwise.
        /// </returns>
        public static Result Bind<TSource>(this Result<TSource> source, Func<TSource?, Result> selector)
        {
            Requires.ArgumentNotNull(source, nameof(source));
            Requires.ArgumentNotNull(selector, nameof(selector));

            return source.IsError ? Result.FromException(source.Exception!) : selector(source.Value);
        }

        /// <summary>
        ///     Implements a monadic bind operation that returns <see cref="Result{T}"/> class instance.
        /// </summary>
        /// <typeparam name="TSource">
        ///     Type of the source <see cref="Result{T}"/> object.
        /// </typeparam>
        /// <typeparam name="TTarget">
        ///     Type of the target <see cref="Result{T}"/> object.
        /// </typeparam>
        /// <param name="source">
        ///     Source result object.
        /// </param>
        /// <param name="selector">
        ///     Function that will be called in order to produce a new <see cref="Result{T}"/> object when <paramref name="source"/> represents failed operation.
        /// </param>
        /// <returns>
        ///     <paramref name="source"/> if it represents failed operation or object returned by <paramref name="selector"/> function otherwise.
        /// </returns>
        public static Result<TTarget> Bind<TSource, TTarget>(this Result<TSource> source, Func<TSource?, Result<TTarget>> selector)
        {
            Requires.ArgumentNotNull(source, nameof(source));
            Requires.ArgumentNotNull(selector, nameof(selector));

            return source.IsError ? Result.FromException<TTarget>(source.Exception!) : selector(source.Value);
        }

        /// <summary>
        ///     Implements an asynchronous monadic bind operation for void returning <see cref="Result"/> class instance.
        /// </summary>
        /// <param name="source">
        ///     Source result object.
        /// </param>
        /// <param name="selector">
        ///     Function that will be called in order to produce a new <see cref="Result"/> object when <paramref name="source"/> represents failed operation.
        /// </param>
        /// <returns>
        ///     A task that represents an operation of obtaining <paramref name="source"/> if it represents failed operation or object returned by <paramref name="selector"/> function otherwise.
        /// </returns>
        public static async Task<Result> BindAsync(this Result source, Func<Task<Result>> selector)
        {
            Requires.ArgumentNotNull(source, nameof(source));
            Requires.ArgumentNotNull(selector, nameof(selector));

            return source.IsError ? source : await selector().ConfigureAwait(false);
        }

        /// <summary>
        ///     Implements an asynchronous monadic bind operation that returns <see cref="Result{T}"/> class instance.
        /// </summary>
        /// <typeparam name="TTarget">
        ///     Type of the target <see cref="Result{T}"/> object.
        /// </typeparam>
        /// <param name="source">
        ///     Source result object.
        /// </param>
        /// <param name="selector">
        ///     Function that will be called in order to produce a new <see cref="Result{T}"/> object when <paramref name="source"/> represents failed operation.
        /// </param>
        /// <returns>
        ///     A task that represents an operation of obtaining <paramref name="source"/> if it represents failed operation or object returned by <paramref name="selector"/> function otherwise.
        /// </returns>
        public static async Task<Result<TTarget>> BindAsync<TTarget>(this Result source, Func<Task<Result<TTarget>>> selector)
        {
            Requires.ArgumentNotNull(source, nameof(source));
            Requires.ArgumentNotNull(selector, nameof(selector));

            return source.IsError ? Result.FromException<TTarget>(source.Exception!) : await selector().ConfigureAwait(false);
        }

        /// <summary>
        ///     Implements an asynchronous monadic bind operation that returns void returning <see cref="Result"/> class instance.
        /// </summary>
        /// <typeparam name="TSource">
        ///     Type of the source <see cref="Result{T}"/> object.
        /// </typeparam>
        /// <param name="source">
        ///     Source result object.
        /// </param>
        /// <param name="selector">
        ///     Function that will be called in order to produce a new <see cref="Result"/> object when <paramref name="source"/> represents failed operation.
        /// </param>
        /// <returns>
        ///     A task that represents an operation of obtaining <paramref name="source"/> if it represents failed operation or object returned by <paramref name="selector"/> function otherwise.
        /// </returns>
        public static async Task<Result> BindAsync<TSource>(this Result<TSource> source, Func<TSource?, Task<Result>> selector)
        {
            Requires.ArgumentNotNull(source, nameof(source));
            Requires.ArgumentNotNull(selector, nameof(selector));

            return source.IsError ? Result.FromException(source.Exception!) : await selector(source.Value).ConfigureAwait(false);
        }

        /// <summary>
        ///     Implements an asynchronous monadic bind operation that returns <see cref="Result{T}"/> class instance.
        /// </summary>
        /// <typeparam name="TSource">
        ///     Type of the source <see cref="Result{T}"/> object.
        /// </typeparam>
        /// <typeparam name="TTarget">
        ///     Type of the target <see cref="Result{T}"/> object.
        /// </typeparam>
        /// <param name="source">
        ///     Source result object.
        /// </param>
        /// <param name="selector">
        ///     Function that will be called in order to produce a new <see cref="Result{T}"/> object when <paramref name="source"/> represents failed operation.
        /// </param>
        /// <returns>
        ///     A task that represents an operation of obtaining <paramref name="source"/> if it represents failed operation or object returned by <paramref name="selector"/> function otherwise.
        /// </returns>
        public static async Task<Result<TTarget>> BindAsync<TSource, TTarget>(this Result<TSource> source, Func<TSource?, Task<Result<TTarget>>> selector)
        {
            Requires.ArgumentNotNull(source, nameof(source));
            Requires.ArgumentNotNull(selector, nameof(selector));

            return source.IsError ? Result.FromException<TTarget>(source.Exception!) : await selector(source.Value).ConfigureAwait(false);
        }

        /// <summary>
        ///     Implements a monadic map operation that returns <see cref="Result{T}"/> class instance.
        /// </summary>
        /// <typeparam name="TTarget">
        ///     Type of the target <see cref="Result{T}"/> object.
        /// </typeparam>
        /// <param name="source">
        ///     Source result object.
        /// </param>
        /// <param name="mapper">
        ///     Function that will be called in order to produce a new <see cref="Result{T}"/> object when <paramref name="source"/> represents successfull operation.
        /// </param>
        /// <returns>
        ///     <paramref name="source"/> if it represents failed operation or <see cref="Result{T}"/> object that represents result of <paramref name="mapper"/> function execution otherwise.
        /// </returns>
        public static Result<TTarget> Map<TTarget>(this Result source, Func<TTarget> mapper)
        {
            Requires.ArgumentNotNull(source, nameof(source));
            Requires.ArgumentNotNull(mapper, nameof(mapper));

            return Bind(source, () => Result.Capture(() => mapper()));
        }

        /// <summary>
        ///     Implements a monadic map operation that returns <see cref="Result{T}"/> class instance.
        /// </summary>
        /// <typeparam name="TSource">
        ///     Type of the source <see cref="Result{T}"/> object.
        /// </typeparam>
        /// <typeparam name="TTarget">
        ///     Type of the target <see cref="Result{T}"/> object.
        /// </typeparam>
        /// <param name="source">
        ///     Source result object.
        /// </param>
        /// <param name="mapper">
        ///     Function that will be called in order to produce a new <see cref="Result{T}"/> object when <paramref name="source"/> represents successfull operation.
        /// </param>
        /// <returns>
        ///     <paramref name="source"/> if it represents failed operation or <see cref="Result{T}"/> object that represents result of <paramref name="mapper"/> function execution otherwise.
        /// </returns>
        public static Result<TTarget> Map<TSource, TTarget>(this Result<TSource> source, Func<TSource?, TTarget> mapper)
        {
            Requires.ArgumentNotNull(source, nameof(source));
            Requires.ArgumentNotNull(mapper, nameof(mapper));

            return Bind(source, value => Result.Capture(() => mapper(value)));
        }

        /// <summary>
        ///     Implements an synchronous monadic map operation that returns <see cref="Result{T}"/> class instance.
        /// </summary>
        /// <typeparam name="TTarget">
        ///     Type of the target <see cref="Result{T}"/> object.
        /// </typeparam>
        /// <param name="source">
        ///     Source result object.
        /// </param>
        /// <param name="mapper">
        ///     Function that will be called in order to produce a new <see cref="Result{T}"/> object when <paramref name="source"/> represents successfull operation.
        /// </param>
        /// <returns>
        ///     A task that represents an operation of obtaining <paramref name="source"/> if it represents failed operation or <see cref="Result{T}"/> object that represents result
        ///     of <paramref name="mapper"/> function execution otherwise.
        /// </returns>
        public static async Task<Result<TTarget>> MapAsync<TTarget>(this Result source, Func<Task<TTarget>> mapper)
        {
            Requires.ArgumentNotNull(source, nameof(source));
            Requires.ArgumentNotNull(mapper, nameof(mapper));

            return await BindAsync(source, async () => await Result.CaptureAsync(async () => await mapper().ConfigureAwait(false)).ConfigureAwait(false)).ConfigureAwait(false);
        }

        /// <summary>
        ///     Implements an synchronous monadic map operation that returns <see cref="Result{T}"/> class instance.
        /// </summary>
        /// <typeparam name="TSource">
        ///     Type of the source <see cref="Result{T}"/> object.
        /// </typeparam>
        /// <typeparam name="TTarget">
        ///     Type of the target <see cref="Result{T}"/> object.
        /// </typeparam>
        /// <param name="source">
        ///     Source result object.
        /// </param>
        /// <param name="mapper">
        ///     Function that will be called in order to produce a new <see cref="Result{T}"/> object when <paramref name="source"/> represents successfull operation.
        /// </param>
        /// <returns>
        ///     A task that represents an operation of obtaining <paramref name="source"/> if it represents failed operation or <see cref="Result{T}"/> object that represents result
        ///     of <paramref name="mapper"/> function execution otherwise.
        /// </returns>
        public static async Task<Result<TTarget>> MapAsync<TSource, TTarget>(this Result<TSource> source, Func<TSource?, Task<TTarget>> mapper)
        {
            Requires.ArgumentNotNull(source, nameof(source));
            Requires.ArgumentNotNull(mapper, nameof(mapper));

            return await BindAsync(source, async value => await Result.CaptureAsync(async () => await mapper(value).ConfigureAwait(false)).ConfigureAwait(false)).ConfigureAwait(false);
        }

        /// <summary>
        ///     Maps specified <see cref="Result"/> object that represents failed operation to a specified <see cref="Result{T}"/> type.
        /// </summary>
        /// <typeparam name="TTarget">
        ///     Type of the target <see cref="Result{T}"/> object.
        /// </typeparam>
        /// <param name="source">
        ///     Source result object.
        /// </param>
        /// <returns>
        ///     Newly created instance of the <see cref="Result{T}"/> class that represnts the same execution error as <paramref name="source"/> parameter.
        /// </returns>
        /// <exception cref="ArgumentException">
        ///     <paramref name="source"/> represents an successfull operation result.
        /// </exception>
        public static Result<TTarget> MapError<TTarget>(this Result source)
        {
            Requires.ArgumentNotNull(source, nameof(source));

            return source.IsError ? Result.FromException<TTarget>(source.Exception!) : throw new ArgumentException(Resources.Exception_Argument_ResultSuccessfull);
        }

        /// <summary>
        ///     Flattens specified nested result object.
        /// </summary>
        /// <typeparam name="TTarget">
        ///     Type of the target <see cref="Result{T}"/> object.
        /// </typeparam>
        /// <param name="source">
        ///     Source result object.
        /// </param>
        /// <returns>
        ///     Source result return value object if the <paramref name="source"/> operation is successfull,
        ///     <paramref name="source"/> exception otherwise.
        /// </returns>
        public static Result<TTarget> Flatten<TTarget>(this Result<Result<TTarget>> source)
        {
            Requires.ArgumentNotNull(source, nameof(source));

            return source.IsError ? Result.FromException<TTarget>(source.Exception!) : source.Value!;
        }

        /// <summary>
        ///     Executes a function for wrapped value or exception.
        /// </summary>
        /// <typeparam name="TSource">
        ///     Type of the source <see cref="Result{T}"/> object.
        /// </typeparam>
        /// <typeparam name="TTarget">
        ///     Type of the target object.
        /// </typeparam>
        /// <param name="source">
        ///     Source result object.
        /// </param>
        /// <param name="successHandler">
        /// The function to execute if the source is a return value.
        /// </param>
        /// <param name="errorHandler">
        ///     The function to execute if the source is an exception.
        /// </param>
        /// <returns>
        ///     An object that is an result of the function execution.
        /// </returns>
        public static TTarget Match<TSource, TTarget>(this Result<TSource> source, Func<TSource?, TTarget> successHandler, Func<Exception, TTarget> errorHandler)
        {
            Requires.ArgumentNotNull(source, nameof(source));
            Requires.ArgumentNotNull(successHandler, nameof(successHandler));
            Requires.ArgumentNotNull(errorHandler, nameof(errorHandler));

            return source.IsError ? errorHandler(source.Exception!) : successHandler(source.Value);
        }

        /// <summary>
        ///     Deconstructs an instance of the <see cref="Result{T}"/> into two variables.
        /// </summary>
        /// <typeparam name="T">
        ///     Type of the wrapped operation result.
        /// </typeparam>
        /// <param name="source">
        ///     Source result object.
        /// </param>
        /// <param name="value">
        ///     The operation return value, or <see langword="default"/> if the <paramref name="source"/> represents an error.
        /// </param>
        /// <param name="exception">
        ///     The operation exception, or <see langword="default"/> if the <paramref name="source"/> represents a success.
        /// </param>
        public static void Deconstruct<T>(this Result<T> source, out T? value, out Exception? exception)
        {
            Requires.ArgumentNotNull(source, nameof(source));

            value = source.Value;
            exception = source.Exception;
        }

        /// <summary>
        ///     Deconstructs an instance of the <see cref="Result{T}"/> into two variables.
        /// </summary>
        /// <typeparam name="T">
        ///     Type of the wrapped operation result.
        /// </typeparam>
        /// <param name="source">
        ///     Source result object.
        /// </param>
        /// <returns>
        ///     A tuple with a possible operation return value on first position and exception on second.
        /// </returns>
        public static (T? Value, Exception? Exception) Deconstruct<T>(this Result<T> source)
        {
            Requires.ArgumentNotNull(source, nameof(source));

            return (source.Value, source.Exception);
        }
    }
}
