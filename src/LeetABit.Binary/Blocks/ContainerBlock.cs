// <copyright file="ContainerBlock.cs" company="LeetABit">
//   Copyright (c) Hubert Bukowski. All rights reserved.
//   Licensed under the MIT License.
//   See LICENSE file in the project root for full license information.
// </copyright>

namespace LeetABit.Binary.Blocks
{
    using System;

    /// <summary>
    ///     Represents container for definition blocks.
    /// </summary>
    public class ContainerBlock : DecoratorBlock
    {
        /// <summary>
        ///     Function that obtains a path to the container.
        /// </summary>
        private readonly Func<IEvaluationContext, LogicalPath> containerPath;

        /// <summary>
        ///     Initializes a new instance of the <see cref="ContainerBlock"/> class.
        /// </summary>
        /// <param name="inner">
        ///     Definition block that shall be decorated.
        /// </param>
        /// <param name="containerPath">
        ///     Function that obtains a path to the container.
        /// </param>
        public ContainerBlock(IDefinitionBlock inner, Func<IEvaluationContext, LogicalPath> containerPath)
            : base(inner)
        {
            this.containerPath = Requires.ArgumentNotNull(containerPath, nameof(containerPath));
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

            var path = context.Path;
            context.ChangePath(this.containerPath(context));
            try
            {
                return this.Inner.Process(context);
            }
            finally
            {
                context.ChangePath(path);
            }
        }
    }
}
