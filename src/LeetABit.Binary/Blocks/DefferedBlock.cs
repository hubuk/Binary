// <copyright file="DefferedBlock.cs" company="LeetABit">
//   Copyright (c) Hubert Bukowski. All rights reserved.
//   Licensed under the MIT License.
//   See LICENSE file in the project root for full license information.
// </copyright>

namespace LeetABit.Binary.Blocks
{
    /// <summary>
    ///     Represents a definition block which processing is deffered till explicit processed by <see cref="ProcessBlock"/>.
    /// </summary>
    public class DefferedBlock : DecoratorBlock
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="DefferedBlock"/> class.
        /// </summary>
        /// <param name="inner">
        ///     Definition block that shall be decorated.
        /// </param>
        public DefferedBlock(IDefinitionBlock inner)
            : base(inner)
        {
        }

        /// <summary>
        ///     Implements processing of the current definition block using specified coding context.
        /// </summary>
        /// <param name="context">
        ///     Coding context that contans current coding data.
        /// </param>
        /// <returns>
        ///     Object that represents processing result.
        /// </returns>
        public override Result Process(ICodingContext context)
        {
            _ = Requires.ArgumentNotNull(context, nameof(context));

            context.StoreBlockData(this, CaptureState(context));
            return Result.Success;
        }

        /// <summary>
        ///     Implements deffered processing of the current definition block using specified coding context.
        /// </summary>
        /// <param name="context">
        ///     Coding context that contans current coding data.
        /// </param>
        /// <returns>
        ///     Object that represents processing result.
        /// </returns>
        public Result DefferedProcess(ICodingContext context)
        {
            _ = Requires.ArgumentNotNull(context, nameof(context));

            var stateBackup = CaptureState(context);

            try
            {
                return context.RetrieveBlockData(this)
                    .ContinueWith(data => ((string path, int position))data!)
                    .ContinueWith(state => ApplyState(context, state))
                    .ContinueWith(() => this.Inner.Process(context));
            }
            finally
            {
                ApplyState(context, stateBackup).Unwrap();
            }
        }

        /// <summary>
        ///     Captures a coding state from the specifired coding context.
        /// </summary>
        /// <param name="context">
        ///     Coding context from which the state shall be captured.
        /// </param>
        /// <returns>
        ///     Captured coding state.
        /// </returns>
        private static (string Path, int Position) CaptureState(ICodingContext context)
        {
            return (context.Path, context.Position);
        }

        /// <summary>
        ///     Spplies specified coding state to the specified coding context.
        /// </summary>
        /// <param name="context">
        ///     Coding context to which the specified state shall be applied.
        /// </param>
        /// <param name="state">
        ///     A coding state which shall be applied.
        /// </param>
        /// <returns>
        ///     Operation execution result.
        /// </returns>
        private static Result ApplyState(ICodingContext context, (string Path, int Position) state)
        {
            context.ChangePath(state.Path);
            return context.Move(state.Position - context.Position);
        }
    }
}
