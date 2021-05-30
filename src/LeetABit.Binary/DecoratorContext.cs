// <copyright file="DecoratorContext.cs" company="LeetABit">
//   Copyright (c) Hubert Bukowski. All rights reserved.
//   Licensed under the MIT License.
//   See LICENSE file in the project root for full license information.
// </copyright>

namespace LeetABit.Binary
{
    /// <summary>
    ///     Represents a coding context that decorates another coding context with custom functionality.
    /// </summary>
    public abstract class DecoratorContext : ICodingContext
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="DecoratorContext"/> class.
        /// </summary>
        /// <param name="inner">
        ///     Decorated coding context instance.
        /// </param>
        protected DecoratorContext(ICodingContext inner)
        {
            this.Inner = Requires.ArgumentNotNull(inner, nameof(inner));
        }

        /// <summary>
        ///     Gets a current logical path.
        /// </summary>
        public virtual LogicalPath Path
        {
            get
            {
                return this.Inner.Path;
            }
        }

        /// <summary>
        ///     Gets a current binary position.
        /// </summary>
        public virtual int Position
        {
            get
            {
                return this.Inner.Position;
            }
        }

        /// <summary>
        ///     Gets a reference to wrapped context.
        /// </summary>
        protected virtual ICodingContext Inner
        {
            get;
        }

        /// <summary>
        ///     Starts registering all the revertable changes and make them permanent or revert them on transaction disposition.
        /// </summary>
        /// <returns>
        ///     A transaction object that shall be used to control committment or rollback of the cahnges.
        /// </returns>
        public virtual Transaction BeginTransaction()
        {
            return this.Inner.BeginTransaction();
        }

        /// <summary>
        ///     Changes the current logical path.
        /// </summary>
        /// <param name="path">
        ///     New logical path to be set.
        /// </param>
        public virtual void ChangePath(LogicalPath path)
        {
            this.Inner.ChangePath(path);
        }

        /// <summary>
        ///     Gets a binary value mapped to the specified logical path.
        /// </summary>
        /// <param name="fieldPath">
        ///     Logical path which binary value shall be obtained.
        /// </param>
        /// <returns>
        ///     Information about mapped binary value or error.
        /// </returns>
        public virtual Result<FieldMapping> GetFieldMapping(LogicalPath fieldPath)
        {
            return this.Inner.GetFieldMapping(fieldPath);
        }

        /// <summary>
        ///     Gets a current value of the specified variable.
        /// </summary>
        /// <param name="variableName">
        ///     Name of the variable which value shall be obtained.
        /// </param>
        /// <returns>
        ///     Current value of the specified variable or error.
        /// </returns>
        public virtual Result<object?> GetVariable(string variableName)
        {
            return this.Inner.GetVariable(variableName);
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
        public virtual Result MapField(LogicalPath fieldPath, int length, IBinaryValueConverter converter, object? defaultValue)
        {
            return this.Inner.MapField(fieldPath, length, converter, defaultValue);
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
        public virtual Result Move(int offset)
        {
            return this.Inner.Move(offset);
        }

        /// <summary>
        ///     Retrieves previously stored binary definition block's data.
        /// </summary>
        /// <param name="block">
        ///     Block which value to be retrieved.
        /// </param>
        /// <returns>
        ///     Retrieved block's data or error.
        /// </returns>
        public virtual Result<object> RetrieveBlockData(IDefinitionBlock block)
        {
            return this.Inner.RetrieveBlockData(block);
        }

        /// <summary>
        ///     Sets a current value of the specified variable.
        /// </summary>
        /// <param name="variableName">
        ///     Name of the variable which value shall be obtained.
        /// </param>
        /// <param name="value">
        ///     A new variable value.
        /// </param>
        public virtual void SetVariable(string variableName, object? value)
        {
            this.Inner.SetVariable(variableName, value);
        }

        /// <summary>
        ///     Stores binary definition block's data.
        /// </summary>
        /// <param name="block">
        ///     Block which value is to be stored.
        /// </param>
        /// <param name="data">
        ///     Value to be stored.
        /// </param>
        public virtual void StoreBlockData(IDefinitionBlock block, object data)
        {
            this.Inner.StoreBlockData(block, data);
        }
    }
}
