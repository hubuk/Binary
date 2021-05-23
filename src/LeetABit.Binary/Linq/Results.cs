// <copyright file="Results.cs" company="LeetABit">
//   Copyright (c) Hubert Bukowski. All rights reserved.
//   Licensed under the MIT License.
//   See LICENSE file in the project root for full license information.
// </copyright>

namespace LeetABit.Binary.Linq
{
    using System;

    /// <summary>
    ///     Extension methods for <see cref="Result"/> and <see cref="Result{T}"/> classes.
    /// </summary>
    public static class Results
    {
        /// <summary>
        ///     Selects a result that will be determined by the specified operation if the source operation succeeded.
        /// </summary>
        /// <typeparam name="TTarget">
        ///     Type of the target <see cref="Result{T}"/> object.
        /// </typeparam>
        /// <param name="source">
        ///     Source result object.
        /// </param>
        /// <param name="operation">
        ///     Function that will be called in order to produce a new <see cref="Result{T}"/> object when <paramref name="source"/> represents successfull operation.
        /// </param>
        /// <returns>
        ///     <paramref name="source"/> if it represents failed operation or <see cref="Result{T}"/> object that represents result of <paramref name="operation"/> function execution otherwise.
        /// </returns>
        public static Result<TTarget> Select<TTarget>(this Result source, Func<TTarget> operation)
        {
            Requires.ArgumentNotNull(source, nameof(source));
            Requires.ArgumentNotNull(operation, nameof(operation));

            return source.Map(operation);
        }

        /// <summary>
        ///     Selects a result that will be determined by the specified operation if the source operation succeeded.
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
        /// <param name="operation">
        ///     Function that will be called in order to produce a new <see cref="Result{T}"/> object when <paramref name="source"/> represents successfull operation.
        /// </param>
        /// <returns>
        ///     <paramref name="source"/> if it represents failed operation or <see cref="Result{T}"/> object that represents result of <paramref name="operation"/> function execution otherwise.
        /// </returns>
        public static Result<TTarget> Select<TSource, TTarget>(this Result<TSource> source, Func<TSource?, TTarget> operation)
        {
            Requires.ArgumentNotNull(source, nameof(source));
            Requires.ArgumentNotNull(operation, nameof(operation));

            return source.Map(operation);
        }

        /// <summary>
        ///     Selects a result that will be determined by the specified selectors if the source operation succeeded.
        /// </summary>
        /// <typeparam name="TSource">
        ///     Type of the source <see cref="Result{T}"/> object.
        /// </typeparam>
        /// <typeparam name="TTarget">
        ///     Type of the target <see cref="Result{T}"/> object.
        /// </typeparam>
        /// <typeparam name="TJoin">
        ///     Type of the joined <see cref="Result{T}"/> object.
        /// </typeparam>
        /// <param name="source">
        ///     Source result object.
        /// </param>
        /// <param name="resultSelector">
        ///     Function that will be called to produce next operation result when <paramref name="source"/> represents successfull operation.
        /// </param>
        /// <param name="joinSelector">
        ///     Function that will be called to produce a joined result for new and source results.
        /// </param>
        /// <returns>
        ///     <paramref name="source"/> if it represents failed operation or <see cref="Result{T}"/> object that represents result of <paramref name="resultSelector"/> and
        ///     <paramref name="joinSelector"/> functions execution otherwise.
        /// </returns>
        public static Result<TJoin> SelectMany<TSource, TTarget, TJoin>(this Result<TSource> source, Func<TSource?, Result<TTarget>> resultSelector, Func<TSource?, TTarget?, TJoin> joinSelector)
        {
            Requires.ArgumentNotNull(source, nameof(source));
            Requires.ArgumentNotNull(resultSelector, nameof(resultSelector));
            Requires.ArgumentNotNull(joinSelector, nameof(joinSelector));

            return source.Bind(a => resultSelector(a).Select(b => joinSelector(a, b)));
        }

        /// <summary>
        ///     Selects a result that will be determined by the specified selectors if the source operation succeeded.
        /// </summary>
        /// <typeparam name="TTarget">
        ///     Type of the target <see cref="Result{T}"/> object.
        /// </typeparam>
        /// <typeparam name="TJoin">
        ///     Type of the joined <see cref="Result{T}"/> object.
        /// </typeparam>
        /// <param name="source">
        ///     Source result object.
        /// </param>
        /// <param name="resultSelector">
        ///     Function that will be called to produce next operation result when <paramref name="source"/> represents successfull operation.
        /// </param>
        /// <param name="joinSelector">
        ///     Function that will be called to produce a joined result for new and source results.
        /// </param>
        /// <returns>
        ///     <paramref name="source"/> if it represents failed operation or <see cref="Result{T}"/> object that represents result of <paramref name="resultSelector"/> and
        ///     <paramref name="joinSelector"/> functions execution otherwise.
        /// </returns>
        public static Result<TJoin> SelectMany<TTarget, TJoin>(this Result source, Func<Result<TTarget>> resultSelector, Func<TTarget?, TJoin> joinSelector)
        {
            Requires.ArgumentNotNull(source, nameof(source));
            Requires.ArgumentNotNull(resultSelector, nameof(resultSelector));
            Requires.ArgumentNotNull(joinSelector, nameof(joinSelector));

            return source.Bind(() => resultSelector().Select(b => joinSelector(b)));
        }

        /// <summary>
        ///     Selects a result that will be determined by the specified selectors if the source operation succeeded.
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
        /// <param name="resultSelector">
        ///     Function that will be called to produce next operation result when <paramref name="source"/> represents successfull operation.
        /// </param>
        /// <returns>
        ///     <paramref name="source"/> if it represents failed operation or <see cref="Result{T}"/> object that represents result of <paramref name="resultSelector"/> function execution otherwise.
        /// </returns>
        public static Result<TTarget> SelectMany<TSource, TTarget>(this Result<TSource> source, Func<TSource?, Result<TTarget>> resultSelector)
        {
            Requires.ArgumentNotNull(source, nameof(source));
            Requires.ArgumentNotNull(resultSelector, nameof(resultSelector));

            return source.Bind(resultSelector);
        }

        /// <summary>
        ///     Selects a result that will be determined by the specified selectors if the source operation succeeded.
        /// </summary>
        /// <typeparam name="TTarget">
        ///     Type of the target <see cref="Result{T}"/> object.
        /// </typeparam>
        /// <param name="source">
        ///     Source result object.
        /// </param>
        /// <param name="resultSelector">
        ///     Function that will be called to produce next operation result when <paramref name="source"/> represents successfull operation.
        /// </param>
        /// <returns>
        ///     <paramref name="source"/> if it represents failed operation or <see cref="Result{T}"/> object that represents result of <paramref name="resultSelector"/> function execution otherwise.
        /// </returns>
        public static Result<TTarget> SelectMany<TTarget>(this Result source, Func<Result<TTarget>> resultSelector)
        {
            Requires.ArgumentNotNull(source, nameof(source));
            Requires.ArgumentNotNull(resultSelector, nameof(resultSelector));

            return source.Bind(resultSelector);
        }
    }
}
