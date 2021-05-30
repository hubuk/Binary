// <copyright file="CodingState.cs" company="LeetABit">
//   Copyright (c) Hubert Bukowski. All rights reserved.
//   Licensed under the MIT License.
//   See LICENSE file in the project root for full license information.
// </copyright>

namespace LeetABit.Binary
{
    /// <summary>
    ///     Represents a specified state of the coding context.
    /// </summary>
    public class CodingState
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="CodingState"/> class.
        /// </summary>
        /// <param name="path">
        ///     Coding logical path.
        /// </param>
        /// <param name="position">
        ///     Current binary position.
        /// </param>
        public CodingState(LogicalPath path, int position)
        {
            this.Path = Requires.ArgumentAbsolutePath(path, nameof(path));
            this.Position = position;
        }

        /// <summary>
        ///     Gets a coding logical path.
        /// </summary>
        public LogicalPath Path
        {
            get;
        }

        /// <summary>
        ///     Gets a current binary position.
        /// </summary>
        public int Position
        {
            get;
        }

        /// <summary>
        ///     Creates a new instance of the <see cref="CodingState"/> class using current state of the <see cref="ICodingContext"/> context.
        /// </summary>
        /// <param name="context">
        ///     Evaluation context from which the coding state shall be extracted.
        /// </param>
        /// <returns>
        ///     A newly created instance of the <see cref="CodingState"/> class using current state of the <see cref="ICodingContext"/> context.
        /// </returns>
        public static CodingState FromContext(ICodingContext context)
        {
            _ = Requires.ArgumentNotNull(context, nameof(context));

            return new CodingState(context.Path, context.Position);
        }
    }
}
