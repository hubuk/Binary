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
        ///     Continues successfull execution with a specified action.
        /// </summary>
        /// <param name="source">
        ///     Source result object.
        /// </param>
        /// <param name="action">
        ///     Action to execute if the <paramref name="source"/> result has succeeded.
        /// </param>
        /// <returns>
        ///     Result captured from the <paramref name="action"/> execution or <paramref name="source"/> error.
        /// </returns>
        public static Result ContinueWith(this Result source, Action action)
        {
            _ = Requires.ArgumentNotNull(source, nameof(source));
            _ = Requires.ArgumentNotNull(action, nameof(action));

            return ContinueWith(source, () => Result.Capture(action));
        }

        /// <summary>
        ///     Continues successfull execution with a specified return value.
        /// </summary>
        /// <param name="source">
        ///     Source result object.
        /// </param>
        /// <param name="returnValue">
        ///     Return value if the <paramref name="source"/> result has succeeded.
        /// </param>
        /// <returns>
        ///     Result captured from the <paramref name="returnValue"/> or <paramref name="source"/> error.
        /// </returns>
        public static Result ContinueWith(this Result source, Result returnValue)
        {
            _ = Requires.ArgumentNotNull(source, nameof(source));

            return source.IsError ? source : returnValue;
        }

        /// <summary>
        ///     Continues successfull execution with a specified action.
        /// </summary>
        /// <param name="source">
        ///     Source result object.
        /// </param>
        /// <param name="function">
        ///     Function to execute if the <paramref name="source"/> result has succeeded.
        /// </param>
        /// <returns>
        ///     Result captured from the <paramref name="function"/> execution or <paramref name="source"/> error.
        /// </returns>
        public static Result ContinueWith(this Result source, Func<Result> function)
        {
            _ = Requires.ArgumentNotNull(source, nameof(source));
            _ = Requires.ArgumentNotNull(function, nameof(function));

            return source.IsError ? source : function();
        }

        /// <summary>
        ///     Continues successfull execution with a specified return value.
        /// </summary>
        /// <typeparam name="TSource">
        ///     Type of the operation result.
        /// </typeparam>
        /// <param name="source">
        ///     Source result object.
        /// </param>
        /// <param name="returnValue">
        ///     Return value if the <paramref name="source"/> result has succeeded.
        /// </param>
        /// <returns>
        ///     Result captured from the <paramref name="returnValue"/> execution or <paramref name="source"/> error.
        /// </returns>
        public static Result<TSource> ContinueWith<TSource>(this Result source, TSource returnValue)
        {
            _ = Requires.ArgumentNotNull(source, nameof(source));

            return ContinueWith(source, () => Result.FromReturnValue(returnValue));
        }

        /// <summary>
        ///     Continues successfull execution with a specified action.
        /// </summary>
        /// <typeparam name="TSource">
        ///     Type of the operation result.
        /// </typeparam>
        /// <param name="source">
        ///     Source result object.
        /// </param>
        /// <param name="function">
        ///     Function to execute if the <paramref name="source"/> result has succeeded.
        /// </param>
        /// <returns>
        ///     Result captured from the <paramref name="function"/> execution or <paramref name="source"/> error.
        /// </returns>
        public static Result<TSource> ContinueWith<TSource>(this Result source, Func<TSource> function)
        {
            _ = Requires.ArgumentNotNull(source, nameof(source));
            _ = Requires.ArgumentNotNull(function, nameof(function));

            return ContinueWith(source, () => Result.Capture(function));
        }

        /// <summary>
        ///     Continues successfull execution with a specified return value.
        /// </summary>
        /// <typeparam name="TSource">
        ///     Type of the operation result.
        /// </typeparam>
        /// <param name="source">
        ///     Source result object.
        /// </param>
        /// <param name="returnValue">
        ///     Return value if the <paramref name="source"/> result has succeeded.
        /// </param>
        /// <returns>
        ///     Result captured from the <paramref name="returnValue"/> execution or <paramref name="source"/> error.
        /// </returns>
        public static Result<TSource> ContinueWith<TSource>(this Result source, Result<TSource> returnValue)
        {
            _ = Requires.ArgumentNotNull(source, nameof(source));

            return ContinueWith(source, () => returnValue);
        }

        /// <summary>
        ///     Continues successfull execution with a specified action.
        /// </summary>
        /// <typeparam name="TSource">
        ///     Type of the operation result.
        /// </typeparam>
        /// <param name="source">
        ///     Source result object.
        /// </param>
        /// <param name="function">
        ///     Function to execute if the <paramref name="source"/> result has succeeded.
        /// </param>
        /// <returns>
        ///     Result captured from the <paramref name="function"/> execution or <paramref name="source"/> error.
        /// </returns>
        public static Result<TSource> ContinueWith<TSource>(this Result source, Func<Result<TSource>> function)
        {
            _ = Requires.ArgumentNotNull(source, nameof(source));
            _ = Requires.ArgumentNotNull(function, nameof(function));

            return source.IsError ? source.MapError<TSource>() : function();
        }

        /// <summary>
        ///     Continues successfull execution with a specified action.
        /// </summary>
        /// <typeparam name="TSource">
        ///     Type of the operation result.
        /// </typeparam>
        /// <param name="source">
        ///     Source result object.
        /// </param>
        /// <param name="action">
        ///     Action to execute if the <paramref name="source"/> result has succeeded.
        /// </param>
        /// <returns>
        ///     Result captured from the <paramref name="action"/> execution or <paramref name="source"/> error.
        /// </returns>
        public static Result<TSource> ContinueWith<TSource>(this Result<TSource> source, Action action)
        {
            _ = Requires.ArgumentNotNull(source, nameof(source));
            _ = Requires.ArgumentNotNull(action, nameof(action));

            return source.IsError ? source : Result.Capture(action).ContinueWith(() => source);
        }

        /// <summary>
        ///     Continues successfull execution with a specified action.
        /// </summary>
        /// <typeparam name="TSource">
        ///     Type of the operation result.
        /// </typeparam>
        /// <param name="source">
        ///     Source result object.
        /// </param>
        /// <param name="action">
        ///     Action to execute if the <paramref name="source"/> result has succeeded.
        /// </param>
        /// <returns>
        ///     Result captured from the <paramref name="action"/> execution or <paramref name="source"/> error.
        /// </returns>
        public static Result<TSource> ContinueWith<TSource>(this Result<TSource> source, Action<TSource?> action)
        {
            _ = Requires.ArgumentNotNull(source, nameof(source));
            _ = Requires.ArgumentNotNull(action, nameof(action));

            return source.IsError ? source : Result.Capture(() => action(source.Value)).ContinueWith(() => source);
        }

        /// <summary>
        ///     Continues successfull execution with a specified return value.
        /// </summary>
        /// <typeparam name="TSource">
        ///     Type of the operation result.
        /// </typeparam>
        /// <param name="source">
        ///     Source result object.
        /// </param>
        /// <param name="returnValue">
        ///     Return value if the <paramref name="source"/> result has succeeded.
        /// </param>
        /// <returns>
        ///     Result captured from the <paramref name="returnValue"/> or <paramref name="source"/> error.
        /// </returns>
        public static Result ContinueWith<TSource>(this Result<TSource> source, Result returnValue)
        {
            _ = Requires.ArgumentNotNull(source, nameof(source));

            return source.IsError ? source : Result.FromReturnValue(returnValue);
        }

        /// <summary>
        ///     Continues successfull execution with a specified action.
        /// </summary>
        /// <typeparam name="TSource">
        ///     Type of the operation result.
        /// </typeparam>
        /// <param name="source">
        ///     Source result object.
        /// </param>
        /// <param name="function">
        ///     Function to execute if the <paramref name="source"/> result has succeeded.
        /// </param>
        /// <returns>
        ///     Result captured from the <paramref name="function"/> execution or <paramref name="source"/> error.
        /// </returns>
        public static Result ContinueWith<TSource>(this Result<TSource> source, Func<Result> function)
        {
            _ = Requires.ArgumentNotNull(source, nameof(source));
            _ = Requires.ArgumentNotNull(function, nameof(function));

            return source.IsError ? source : function();
        }

        /// <summary>
        ///     Continues successfull execution with a specified action.
        /// </summary>
        /// <typeparam name="TSource">
        ///     Type of the operation result.
        /// </typeparam>
        /// <param name="source">
        ///     Source result object.
        /// </param>
        /// <param name="function">
        ///     Function to execute if the <paramref name="source"/> result has succeeded.
        /// </param>
        /// <returns>
        ///     Result captured from the <paramref name="function"/> execution or <paramref name="source"/> error.
        /// </returns>
        public static Result ContinueWith<TSource>(this Result<TSource> source, Func<TSource?, Result> function)
        {
            _ = Requires.ArgumentNotNull(source, nameof(source));
            _ = Requires.ArgumentNotNull(function, nameof(function));

            return source.IsError ? source : function(source.Value);
        }

        /// <summary>
        ///     Continues successfull execution with a return value.
        /// </summary>
        /// <typeparam name="TSource">
        ///     Type of the operation result.
        /// </typeparam>
        /// <typeparam name="TTarget">
        ///     Type of the target result.
        /// </typeparam>
        /// <param name="source">
        ///     Source result object.
        /// </param>
        /// <param name="returnValue">
        ///     Return value if the <paramref name="source"/> result has succeeded.
        /// </param>
        /// <returns>
        ///     Result captured from the <paramref name="returnValue"/> or <paramref name="source"/> error.
        /// </returns>
        public static Result<TTarget> ContinueWith<TSource, TTarget>(this Result<TSource> source, TTarget returnValue)
        {
            _ = Requires.ArgumentNotNull(source, nameof(source));

            return source.IsError ? source.MapError<TTarget>() : Result.FromReturnValue(returnValue);
        }

        /// <summary>
        ///     Continues successfull execution with a specified action.
        /// </summary>
        /// <typeparam name="TSource">
        ///     Type of the operation result.
        /// </typeparam>
        /// <typeparam name="TTarget">
        ///     Type of the target result.
        /// </typeparam>
        /// <param name="source">
        ///     Source result object.
        /// </param>
        /// <param name="function">
        ///     Function to execute if the <paramref name="source"/> result has succeeded.
        /// </param>
        /// <returns>
        ///     Result captured from the <paramref name="function"/> execution or <paramref name="source"/> error.
        /// </returns>
        public static Result<TTarget> ContinueWith<TSource, TTarget>(this Result<TSource> source, Func<TTarget> function)
        {
            _ = Requires.ArgumentNotNull(source, nameof(source));
            _ = Requires.ArgumentNotNull(function, nameof(function));

            return source.IsError ? source.MapError<TTarget>() : Result.Capture(function);
        }

        /// <summary>
        ///     Continues successfull execution with a specified action.
        /// </summary>
        /// <typeparam name="TSource">
        ///     Type of the operation result.
        /// </typeparam>
        /// <typeparam name="TTarget">
        ///     Type of the target result.
        /// </typeparam>
        /// <param name="source">
        ///     Source result object.
        /// </param>
        /// <param name="function">
        ///     Function to execute if the <paramref name="source"/> result has succeeded.
        /// </param>
        /// <returns>
        ///     Result captured from the <paramref name="function"/> execution or <paramref name="source"/> error.
        /// </returns>
        public static Result<TTarget> ContinueWith<TSource, TTarget>(this Result<TSource> source, Func<TSource?, TTarget> function)
        {
            _ = Requires.ArgumentNotNull(source, nameof(source));
            _ = Requires.ArgumentNotNull(function, nameof(function));

            return source.IsError ? source.MapError<TTarget>() : Result.Capture(() => function(source.Value));
        }

        /// <summary>
        ///     Continues successfull execution with a specified action.
        /// </summary>
        /// <typeparam name="TSource">
        ///     Type of the operation result.
        /// </typeparam>
        /// <typeparam name="TTarget">
        ///     Type of the target result.
        /// </typeparam>
        /// <param name="source">
        ///     Source result object.
        /// </param>
        /// <param name="function">
        ///     Function to execute if the <paramref name="source"/> result has succeeded.
        /// </param>
        /// <returns>
        ///     Result captured from the <paramref name="function"/> execution or <paramref name="source"/> error.
        /// </returns>
        public static Result<TTarget> ContinueWith<TSource, TTarget>(this Result<TSource> source, Func<TSource?, Result<TTarget>> function)
        {
            _ = Requires.ArgumentNotNull(source, nameof(source));
            _ = Requires.ArgumentNotNull(function, nameof(function));

            return source.IsError ? source.MapError<TTarget>() : function(source.Value);
        }

        /// <summary>
        ///     Continues successfull execution with a specified action.
        /// </summary>
        /// <typeparam name="TSource">
        ///     Type of the operation result.
        /// </typeparam>
        /// <typeparam name="TTarget">
        ///     Type of the target result.
        /// </typeparam>
        /// <typeparam name="TJoined">
        ///     Type of the result with joined source and target objects.
        /// </typeparam>
        /// <param name="source">
        ///     Source result object.
        /// </param>
        /// <param name="function">
        ///     Function to execute if the <paramref name="source"/> result has succeeded.
        /// </param>
        /// <param name="joiner">
        ///     Function that produces joined result for source and target objects.
        /// </param>
        /// <returns>
        ///     Result captured from the <paramref name="function"/> execution or <paramref name="source"/> error.
        /// </returns>
        public static Result<TJoined> ContinueWith<TSource, TTarget, TJoined>(this Result<TSource> source, Func<TTarget> function, Func<TSource?, TTarget?, TJoined> joiner)
        {
            _ = Requires.ArgumentNotNull(source, nameof(source));
            _ = Requires.ArgumentNotNull(function, nameof(function));

            return source.IsError ? source.MapError<TJoined>() : Result.Capture(function).ContinueWith(res => joiner(source.Value, res));
        }

        /// <summary>
        ///     Continues successfull execution with a specified action.
        /// </summary>
        /// <typeparam name="TSource">
        ///     Type of the operation result.
        /// </typeparam>
        /// <typeparam name="TTarget">
        ///     Type of the target result.
        /// </typeparam>
        /// <typeparam name="TJoined">
        ///     Type of the result with joined source and target objects.
        /// </typeparam>
        /// <param name="source">
        ///     Source result object.
        /// </param>
        /// <param name="function">
        ///     Function to execute if the <paramref name="source"/> result has succeeded.
        /// </param>
        /// <param name="joiner">
        ///     Function that produces joined result for source and target objects.
        /// </param>
        /// <returns>
        ///     Result captured from the <paramref name="function"/> execution or <paramref name="source"/> error.
        /// </returns>
        public static Result<TJoined> ContinueWith<TSource, TTarget, TJoined>(this Result<TSource> source, Func<TSource?, TTarget> function, Func<TSource?, TTarget?, TJoined> joiner)
        {
            _ = Requires.ArgumentNotNull(source, nameof(source));
            _ = Requires.ArgumentNotNull(function, nameof(function));

            return source.IsError ? source.MapError<TJoined>() : Result.Capture(() => function(source.Value)).ContinueWith(res => joiner(source.Value, res));
        }

        /// <summary>
        ///     Continues successfull execution with a specified action.
        /// </summary>
        /// <typeparam name="TSource">
        ///     Type of the operation result.
        /// </typeparam>
        /// <typeparam name="TTarget">
        ///     Type of the target result.
        /// </typeparam>
        /// <typeparam name="TJoined">
        ///     Type of the result with joined source and target objects.
        /// </typeparam>
        /// <param name="source">
        ///     Source result object.
        /// </param>
        /// <param name="function">
        ///     Function to execute if the <paramref name="source"/> result has succeeded.
        /// </param>
        /// <param name="joiner">
        ///     Function that produces joined result for source and target objects.
        /// </param>
        /// <returns>
        ///     Result captured from the <paramref name="function"/> execution or <paramref name="source"/> error.
        /// </returns>
        public static Result<TJoined> ContinueWith<TSource, TTarget, TJoined>(this Result<TSource> source, Func<TSource?, Result<TTarget>> function, Func<TSource?, TTarget?, TJoined> joiner)
        {
            _ = Requires.ArgumentNotNull(source, nameof(source));
            _ = Requires.ArgumentNotNull(function, nameof(function));

            return source.IsError ? source.MapError<TJoined>() : function(source.Value).ContinueWith(res => joiner(source.Value, res));
        }

        /// <summary>
        ///     Asynchronously continues successfull execution with a specified action.
        /// </summary>
        /// <param name="source">
        ///     Source result object.
        /// </param>
        /// <param name="action">
        ///     Action to execute if the <paramref name="source"/> result has succeeded.
        /// </param>
        /// <returns>
        ///     Task that represents an ongoing operation of capturing result from the <paramref name="action"/> execution or <paramref name="source"/> error.
        /// </returns>
        public static async Task<Result> ContinueWithAsync(this Result source, Func<Task> action)
        {
            _ = Requires.ArgumentNotNull(source, nameof(source));
            _ = Requires.ArgumentNotNull(action, nameof(action));

            return await ContinueWithAsync(source, async () => await Result.CaptureAsync(action).ConfigureAwait(false)).ConfigureAwait(false);
        }

        /// <summary>
        ///     Continues successfull execution with a specified return value.
        /// </summary>
        /// <param name="source">
        ///     Source result object.
        /// </param>
        /// <param name="returnValue">
        ///     Return value if the <paramref name="source"/> result has succeeded.
        /// </param>
        /// <returns>
        ///     Task that represents an ongoing operation of capturing result from the <paramref name="returnValue"/> or <paramref name="source"/> error.
        /// </returns>
        public static async Task<Result> ContinueWithAsync(this Result source, Task<Result> returnValue)
        {
            _ = Requires.ArgumentNotNull(source, nameof(source));
            _ = Requires.ArgumentNotNull(returnValue, nameof(returnValue));

            return source.IsError ? source : await returnValue.ConfigureAwait(false);
        }

        /// <summary>
        ///     Continues successfull execution with a specified action.
        /// </summary>
        /// <param name="source">
        ///     Source result object.
        /// </param>
        /// <param name="function">
        ///     Function to execute if the <paramref name="source"/> result has succeeded.
        /// </param>
        /// <returns>
        ///     Task that represents an ongoing operation of capturing result from the <paramref name="function"/> execution or <paramref name="source"/> error.
        /// </returns>
        public static async Task<Result> ContinueWithAsync(this Result source, Func<Task<Result>> function)
        {
            _ = Requires.ArgumentNotNull(source, nameof(source));
            _ = Requires.ArgumentNotNull(function, nameof(function));

            return source.IsError ? source : await function().ConfigureAwait(false);
        }

        /// <summary>
        ///     Continues successfull execution with a specified return value.
        /// </summary>
        /// <typeparam name="TSource">
        ///     Type of the operation result.
        /// </typeparam>
        /// <param name="source">
        ///     Source result object.
        /// </param>
        /// <param name="returnValue">
        ///     Return value if the <paramref name="source"/> result has succeeded.
        /// </param>
        /// <returns>
        ///     Task that represents an ongoing operation of capturing result from the <paramref name="returnValue"/> or <paramref name="source"/> error.
        /// </returns>
        public static async Task<Result<TSource>> ContinueWithAsync<TSource>(this Result source, Task<TSource> returnValue)
        {
            _ = Requires.ArgumentNotNull(source, nameof(source));
            _ = Requires.ArgumentNotNull(returnValue, nameof(returnValue));

            return await ContinueWithAsync(source, async () => Result.FromReturnValue(await returnValue.ConfigureAwait(false))).ConfigureAwait(false);
        }

        /// <summary>
        ///     Continues successfull execution with a specified action.
        /// </summary>
        /// <typeparam name="TSource">
        ///     Type of the operation result.
        /// </typeparam>
        /// <param name="source">
        ///     Source result object.
        /// </param>
        /// <param name="function">
        ///     Function to execute if the <paramref name="source"/> result has succeeded.
        /// </param>
        /// <returns>
        ///     Task that represents an ongoing operation of capturing result from the <paramref name="function"/> execution or <paramref name="source"/> error.
        /// </returns>
        public static async Task<Result<TSource>> ContinueWithAsync<TSource>(this Result source, Func<Task<TSource>> function)
        {
            _ = Requires.ArgumentNotNull(source, nameof(source));
            _ = Requires.ArgumentNotNull(function, nameof(function));

            return await ContinueWithAsync(source, async () => await Result.CaptureAsync(function).ConfigureAwait(false)).ConfigureAwait(false);
        }

        /// <summary>
        ///     Continues successfull execution with a specified return value.
        /// </summary>
        /// <typeparam name="TSource">
        ///     Type of the operation result.
        /// </typeparam>
        /// <param name="source">
        ///     Source result object.
        /// </param>
        /// <param name="returnValue">
        ///     Return value if the <paramref name="source"/> result has succeeded.
        /// </param>
        /// <returns>
        ///     Task that represents an ongoing operation of capturing result from the <paramref name="returnValue"/> or <paramref name="source"/> error.
        /// </returns>
        public static async Task<Result<TSource>> ContinueWithAsync<TSource>(this Result source, Task<Result<TSource>> returnValue)
        {
            _ = Requires.ArgumentNotNull(source, nameof(source));
            _ = Requires.ArgumentNotNull(returnValue, nameof(returnValue));

            return source.IsError ? source.MapError<TSource>() : await returnValue.ConfigureAwait(false);
        }

        /// <summary>
        ///     Continues successfull execution with a specified action.
        /// </summary>
        /// <typeparam name="TSource">
        ///     Type of the operation result.
        /// </typeparam>
        /// <param name="source">
        ///     Source result object.
        /// </param>
        /// <param name="function">
        ///     Function to execute if the <paramref name="source"/> result has succeeded.
        /// </param>
        /// <returns>
        ///     Task that represents an ongoing operation of capturing result from the <paramref name="function"/> execution or <paramref name="source"/> error.
        /// </returns>
        public static async Task<Result<TSource>> ContinueWithAsync<TSource>(this Result source, Func<Task<Result<TSource>>> function)
        {
            _ = Requires.ArgumentNotNull(source, nameof(source));
            _ = Requires.ArgumentNotNull(function, nameof(function));

            return source.IsError ? source.MapError<TSource>() : await function().ConfigureAwait(false);
        }

        /// <summary>
        ///     Continues successfull execution with a specified action.
        /// </summary>
        /// <typeparam name="TSource">
        ///     Type of the operation result.
        /// </typeparam>
        /// <param name="source">
        ///     Source result object.
        /// </param>
        /// <param name="action">
        ///     Action to execute if the <paramref name="source"/> result has succeeded.
        /// </param>
        /// <returns>
        ///     Task that represents an ongoing operation of capturing result from the <paramref name="action"/> execution or <paramref name="source"/> error.
        /// </returns>
        public static async Task<Result<TSource>> ContinueWithAsync<TSource>(this Result<TSource> source, Func<Task> action)
        {
            _ = Requires.ArgumentNotNull(source, nameof(source));
            _ = Requires.ArgumentNotNull(action, nameof(action));

            return source.IsError ? source : (await Result.CaptureAsync(action).ConfigureAwait(false)).ContinueWith(() => source);
        }

        /// <summary>
        ///     Continues successfull execution with a specified action.
        /// </summary>
        /// <typeparam name="TSource">
        ///     Type of the operation result.
        /// </typeparam>
        /// <param name="source">
        ///     Source result object.
        /// </param>
        /// <param name="action">
        ///     Action to execute if the <paramref name="source"/> result has succeeded.
        /// </param>
        /// <returns>
        ///     Task that represents an ongoing operation of capturing result from the <paramref name="action"/> execution or <paramref name="source"/> error.
        /// </returns>
        public static async Task<Result<TSource>> ContinueWithAsync<TSource>(this Result<TSource> source, Func<TSource?, Task> action)
        {
            _ = Requires.ArgumentNotNull(source, nameof(source));
            _ = Requires.ArgumentNotNull(action, nameof(action));

            return source.IsError ? source : (await Result.CaptureAsync(async () => await action(source.Value).ConfigureAwait(false)).ConfigureAwait(false)).ContinueWith(() => source);
        }

        /// <summary>
        ///     Continues successfull execution with a specified action.
        /// </summary>
        /// <typeparam name="TSource">
        ///     Type of the operation result.
        /// </typeparam>
        /// <param name="source">
        ///     Source result object.
        /// </param>
        /// <param name="function">
        ///     Function to execute if the <paramref name="source"/> result has succeeded.
        /// </param>
        /// <returns>
        ///     Task that represents an ongoing operation of capturing result from the <paramref name="function"/> execution or <paramref name="source"/> error.
        /// </returns>
        public static async Task<Result> ContinueWithAsync<TSource>(this Result<TSource> source, Func<Task<Result>> function)
        {
            _ = Requires.ArgumentNotNull(source, nameof(source));
            _ = Requires.ArgumentNotNull(function, nameof(function));

            return source.IsError ? source : await function().ConfigureAwait(false);
        }

        /// <summary>
        ///     Continues successfull execution with a specified return value.
        /// </summary>
        /// <typeparam name="TSource">
        ///     Type of the operation result.
        /// </typeparam>
        /// <param name="source">
        ///     Source result object.
        /// </param>
        /// <param name="returnValue">
        ///     Return value if the <paramref name="source"/> result has succeeded.
        /// </param>
        /// <returns>
        ///     Task that represents an ongoing operation of capturing result from the <paramref name="returnValue"/> or <paramref name="source"/> error.
        /// </returns>
        public static async Task<Result> ContinueWithAsync<TSource>(this Result<TSource> source, Task<Result> returnValue)
        {
            _ = Requires.ArgumentNotNull(source, nameof(source));
            _ = Requires.ArgumentNotNull(returnValue, nameof(returnValue));

            return source.IsError ? source : await returnValue.ConfigureAwait(false);
        }

        /// <summary>
        ///     Continues successfull execution with a specified action.
        /// </summary>
        /// <typeparam name="TSource">
        ///     Type of the operation result.
        /// </typeparam>
        /// <param name="source">
        ///     Source result object.
        /// </param>
        /// <param name="function">
        ///     Function to execute if the <paramref name="source"/> result has succeeded.
        /// </param>
        /// <returns>
        ///     Task that represents an ongoing operation of capturing result from the <paramref name="function"/> execution or <paramref name="source"/> error.
        /// </returns>
        public static async Task<Result> ContinueWithAsync<TSource>(this Result<TSource> source, Func<TSource?, Task<Result>> function)
        {
            _ = Requires.ArgumentNotNull(source, nameof(source));
            _ = Requires.ArgumentNotNull(function, nameof(function));

            return source.IsError ? source : await function(source.Value).ConfigureAwait(false);
        }

        /// <summary>
        ///     Continues successfull execution with a specified return value.
        /// </summary>
        /// <typeparam name="TSource">
        ///     Type of the operation result.
        /// </typeparam>
        /// <typeparam name="TTarget">
        ///     Type of the target result.
        /// </typeparam>
        /// <param name="source">
        ///     Source result object.
        /// </param>
        /// <param name="returnValue">
        ///     Return value if the <paramref name="source"/> result has succeeded.
        /// </param>
        /// <returns>
        ///     Task that represents an ongoing operation of capturing result from the <paramref name="returnValue"/> or <paramref name="source"/> error.
        /// </returns>
        public static async Task<Result<TTarget>> ContinueWithAsync<TSource, TTarget>(this Result<TSource> source, Task<TTarget> returnValue)
        {
            _ = Requires.ArgumentNotNull(source, nameof(source));
            _ = Requires.ArgumentNotNull(returnValue, nameof(returnValue));

            return source.IsError ? source.MapError<TTarget>() : Result.FromReturnValue(await returnValue.ConfigureAwait(false));
        }

        /// <summary>
        ///     Continues successfull execution with a specified action.
        /// </summary>
        /// <typeparam name="TSource">
        ///     Type of the operation result.
        /// </typeparam>
        /// <typeparam name="TTarget">
        ///     Type of the target result.
        /// </typeparam>
        /// <param name="source">
        ///     Source result object.
        /// </param>
        /// <param name="function">
        ///     Function to execute if the <paramref name="source"/> result has succeeded.
        /// </param>
        /// <returns>
        ///     Task that represents an ongoing operation of capturing result from the <paramref name="function"/> execution or <paramref name="source"/> error.
        /// </returns>
        public static async Task<Result<TTarget>> ContinueWithAsync<TSource, TTarget>(this Result<TSource> source, Func<Task<TTarget>> function)
        {
            _ = Requires.ArgumentNotNull(source, nameof(source));
            _ = Requires.ArgumentNotNull(function, nameof(function));

            return source.IsError ? source.MapError<TTarget>() : await Result.CaptureAsync(function).ConfigureAwait(false);
        }

        /// <summary>
        ///     Continues successfull execution with a specified action.
        /// </summary>
        /// <typeparam name="TSource">
        ///     Type of the operation result.
        /// </typeparam>
        /// <typeparam name="TTarget">
        ///     Type of the target result.
        /// </typeparam>
        /// <param name="source">
        ///     Source result object.
        /// </param>
        /// <param name="function">
        ///     Function to execute if the <paramref name="source"/> result has succeeded.
        /// </param>
        /// <returns>
        ///     Task that represents an ongoing operation of capturing result from the <paramref name="function"/> execution or <paramref name="source"/> error.
        /// </returns>
        public static async Task<Result<TTarget>> ContinueWithAsync<TSource, TTarget>(this Result<TSource> source, Func<TSource?, Task<TTarget>> function)
        {
            _ = Requires.ArgumentNotNull(source, nameof(source));
            _ = Requires.ArgumentNotNull(function, nameof(function));

            return source.IsError ? source.MapError<TTarget>() : await Result.CaptureAsync(async () => await function(source.Value).ConfigureAwait(false)).ConfigureAwait(false);
        }

        /// <summary>
        ///     Continues successfull execution with a specified action.
        /// </summary>
        /// <typeparam name="TSource">
        ///     Type of the operation result.
        /// </typeparam>
        /// <typeparam name="TTarget">
        ///     Type of the target result.
        /// </typeparam>
        /// <param name="source">
        ///     Source result object.
        /// </param>
        /// <param name="function">
        ///     Function to execute if the <paramref name="source"/> result has succeeded.
        /// </param>
        /// <returns>
        ///     Task that represents an ongoing operation of capturing result from the <paramref name="function"/> execution or <paramref name="source"/> error.
        /// </returns>
        public static async Task<Result<TTarget>> ContinueWithAsync<TSource, TTarget>(this Result<TSource> source, Func<TSource?, Task<Result<TTarget>>> function)
        {
            _ = Requires.ArgumentNotNull(source, nameof(source));
            _ = Requires.ArgumentNotNull(function, nameof(function));

            return source.IsError ? source.MapError<TTarget>() : await function(source.Value).ConfigureAwait(false);
        }

        /// <summary>
        ///     Continues successfull execution with a specified action.
        /// </summary>
        /// <typeparam name="TSource">
        ///     Type of the operation result.
        /// </typeparam>
        /// <typeparam name="TTarget">
        ///     Type of the target result.
        /// </typeparam>
        /// <typeparam name="TJoined">
        ///     Type of the result with joined source and target objects.
        /// </typeparam>
        /// <param name="source">
        ///     Source result object.
        /// </param>
        /// <param name="function">
        ///     Function to execute if the <paramref name="source"/> result has succeeded.
        /// </param>
        /// <param name="joiner">
        ///     Function that produces joined result for source and target objects.
        /// </param>
        /// <returns>
        ///     Task that represents an ongoing operation of capturing result from the <paramref name="function"/> execution or <paramref name="source"/> error.
        /// </returns>
        public static async Task<Result<TJoined>> ContinueWithAsync<TSource, TTarget, TJoined>(
            this Result<TSource> source,
            Func<Task<TTarget>> function,
            Func<TSource?, TTarget?, Task<TJoined>> joiner)
        {
            _ = Requires.ArgumentNotNull(source, nameof(source));
            _ = Requires.ArgumentNotNull(function, nameof(function));

            return source.IsError ? source.MapError<TJoined>() : await (await Result.CaptureAsync(function).ConfigureAwait(false))
                .ContinueWithAsync(async res => await joiner(source.Value, res).ConfigureAwait(false))
                .ConfigureAwait(false);
        }

        /// <summary>
        ///     Continues successfull execution with a specified action.
        /// </summary>
        /// <typeparam name="TSource">
        ///     Type of the operation result.
        /// </typeparam>
        /// <typeparam name="TTarget">
        ///     Type of the target result.
        /// </typeparam>
        /// <typeparam name="TJoined">
        ///     Type of the result with joined source and target objects.
        /// </typeparam>
        /// <param name="source">
        ///     Source result object.
        /// </param>
        /// <param name="function">
        ///     Function to execute if the <paramref name="source"/> result has succeeded.
        /// </param>
        /// <param name="joiner">
        ///     Function that produces joined result for source and target objects.
        /// </param>
        /// <returns>
        ///     Task that represents an ongoing operation of capturing result from the <paramref name="function"/> execution or <paramref name="source"/> error.
        /// </returns>
        public static async Task<Result<TJoined>> ContinueWithAsync<TSource, TTarget, TJoined>(
            this Result<TSource> source,
            Func<TSource?, Task<TTarget>> function,
            Func<TSource?, TTarget?, Task<TJoined>> joiner)
        {
            _ = Requires.ArgumentNotNull(source, nameof(source));
            _ = Requires.ArgumentNotNull(function, nameof(function));

            return source.IsError ? source.MapError<TJoined>() : await (await Result.CaptureAsync(async () => await function(source.Value).ConfigureAwait(false)).ConfigureAwait(false))
                .ContinueWithAsync(async res => await joiner(source.Value, res).ConfigureAwait(false))
                .ConfigureAwait(false);
        }

        /// <summary>
        ///     Continues successfull execution with a specified action.
        /// </summary>
        /// <typeparam name="TSource">
        ///     Type of the operation result.
        /// </typeparam>
        /// <typeparam name="TTarget">
        ///     Type of the target result.
        /// </typeparam>
        /// <typeparam name="TJoined">
        ///     Type of the result with joined source and target objects.
        /// </typeparam>
        /// <param name="source">
        ///     Source result object.
        /// </param>
        /// <param name="function">
        ///     Function to execute if the <paramref name="source"/> result has succeeded.
        /// </param>
        /// <param name="joiner">
        ///     Function that produces joined result for source and target objects.
        /// </param>
        /// <returns>
        ///     Task that represents an ongoing operation of capturing result from the <paramref name="function"/> execution or <paramref name="source"/> error.
        /// </returns>
        public static async Task<Result<TJoined>> ContinueWithAsync<TSource, TTarget, TJoined>(
            this Result<TSource> source,
            Func<TSource?, Task<Result<TTarget>>> function,
            Func<TSource?, TTarget?, Task<TJoined>> joiner)
        {
            _ = Requires.ArgumentNotNull(source, nameof(source));
            _ = Requires.ArgumentNotNull(function, nameof(function));

            return source.IsError ? source.MapError<TJoined>() : await (await function(source.Value).ConfigureAwait(false))
                .ContinueWithAsync(async res => await joiner(source.Value, res).ConfigureAwait(false))
                .ConfigureAwait(false);
        }

        /// <summary>
        ///     Continues failed execution with a specified return value.
        /// </summary>
        /// <typeparam name="TSource">
        ///     Type of the operation result.
        /// </typeparam>
        /// <param name="source">
        ///     Source result object.
        /// </param>
        /// <param name="returnValue">
        ///     Return value if the <paramref name="source"/> result has failed.
        /// </param>
        /// <returns>
        ///     Result captured from the <paramref name="returnValue"/> or <paramref name="source"/> return value.
        /// </returns>
        public static Result<TSource> OnError<TSource>(this Result<TSource> source, TSource? returnValue)
        {
            _ = Requires.ArgumentNotNull(source, nameof(source));

            return source.IsSuccess ? source : Result.FromReturnValue(returnValue);
        }

        /// <summary>
        ///     Continues failed execution with a specified function.
        /// </summary>
        /// <typeparam name="TSource">
        ///     Type of the operation result.
        /// </typeparam>
        /// <param name="source">
        ///     Source result object.
        /// </param>
        /// <param name="function">
        ///     Function to execute if the <paramref name="source"/> result has failed.
        /// </param>
        /// <returns>
        ///     Result captured from the <paramref name="function"/> execution or <paramref name="source"/> return value.
        /// </returns>
        public static Result<TSource> OnError<TSource>(this Result<TSource> source, Func<TSource> function)
        {
            _ = Requires.ArgumentNotNull(source, nameof(source));
            _ = Requires.ArgumentNotNull(function, nameof(function));

            return source.IsSuccess ? source : Result.Capture(function);
        }

        /// <summary>
        ///     Continues failed execution with a specified function.
        /// </summary>
        /// <typeparam name="TSource">
        ///     Type of the operation result.
        /// </typeparam>
        /// <param name="source">
        ///     Source result object.
        /// </param>
        /// <param name="function">
        ///     Function to execute if the <paramref name="source"/> result has failed.
        /// </param>
        /// <returns>
        ///     Result captured from the <paramref name="function"/> execution or <paramref name="source"/> return value.
        /// </returns>
        public static Result<TSource> OnError<TSource>(this Result<TSource> source, Func<Result<TSource>> function)
        {
            _ = Requires.ArgumentNotNull(source, nameof(source));
            _ = Requires.ArgumentNotNull(function, nameof(function));

            return source.IsSuccess ? source : function();
        }

        /// <summary>
        ///     Continues failed execution with a specified function.
        /// </summary>
        /// <typeparam name="TSource">
        ///     Type of the operation result.
        /// </typeparam>
        /// <param name="source">
        ///     Source result object.
        /// </param>
        /// <param name="function">
        ///     Function to execute if the <paramref name="source"/> result has failed.
        /// </param>
        /// <returns>
        ///     Result captured from the <paramref name="function"/> execution or <paramref name="source"/> return value.
        /// </returns>
        public static async Task<Result<TSource>> OnErrorAsync<TSource>(this Result<TSource> source, Func<Task<TSource>> function)
        {
            _ = Requires.ArgumentNotNull(source, nameof(source));
            _ = Requires.ArgumentNotNull(function, nameof(function));

            return source.IsSuccess ? source : await Result.CaptureAsync(function).ConfigureAwait(false);
        }

        /// <summary>
        ///     Continues failed execution with a specified function.
        /// </summary>
        /// <typeparam name="TSource">
        ///     Type of the operation result.
        /// </typeparam>
        /// <param name="source">
        ///     Source result object.
        /// </param>
        /// <param name="function">
        ///     Function to execute if the <paramref name="source"/> result has failed.
        /// </param>
        /// <returns>
        ///     Result captured from the <paramref name="function"/> execution or <paramref name="source"/> return value.
        /// </returns>
        public static async Task<Result<TSource>> OnErrorAsync<TSource>(this Result<TSource> source, Func<Task<Result<TSource>>> function)
        {
            _ = Requires.ArgumentNotNull(source, nameof(source));
            _ = Requires.ArgumentNotNull(function, nameof(function));

            return source.IsSuccess ? source : await function().ConfigureAwait(false);
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
            _ = Requires.ArgumentNotNull(source, nameof(source));

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
            _ = Requires.ArgumentNotNull(source, nameof(source));

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
            _ = Requires.ArgumentNotNull(source, nameof(source));
            _ = Requires.ArgumentNotNull(successHandler, nameof(successHandler));
            _ = Requires.ArgumentNotNull(errorHandler, nameof(errorHandler));

            return source.IsError ? errorHandler(source.Exception!) : successHandler(source.Value);
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
        public static async Task<TTarget> MatchAsync<TSource, TTarget>(this Result<TSource> source, Func<TSource?, Task<TTarget>> successHandler, Func<Exception, Task<TTarget>> errorHandler)
        {
            _ = Requires.ArgumentNotNull(source, nameof(source));
            _ = Requires.ArgumentNotNull(successHandler, nameof(successHandler));
            _ = Requires.ArgumentNotNull(errorHandler, nameof(errorHandler));

            return source.IsError ? await errorHandler(source.Exception!).ConfigureAwait(false) : await successHandler(source.Value).ConfigureAwait(false);
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
            _ = Requires.ArgumentNotNull(source, nameof(source));

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
            _ = Requires.ArgumentNotNull(source, nameof(source));

            return (source.Value, source.Exception);
        }
    }
}
