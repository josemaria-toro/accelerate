using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace Accelerate.Application.Dtos
{
    /// <summary>
    /// Base class for data transfer objects.
    /// </summary>
    [Serializable]
    public abstract class DataTransferObject : IDataTransferObject
    {
        private Boolean _disposed;

        /// <summary>
        /// Initialize a new instance of <see cref="DataTransferObject" /> class.
        /// </summary>
        protected DataTransferObject()
        {

        }

        /// <summary>
        /// Initialize a new instance of <seealso cref="DataTransferObject" /> class.
        /// </summary>
        /// <param name="info">
        /// The <seealso cref="SerializationInfo" /> to populate with data.
        /// </param>
        /// <param name="context">
        /// The destination (see <seealso cref="StreamingContext" />) for this serialization.
        /// </param>
        [ExcludeFromCodeCoverage]
        [Obsolete("This constructor is obsolete for security reasons")]
        protected DataTransferObject(SerializationInfo info, StreamingContext context)
        {
            var properties = this.GetType()
                                 .GetProperties();

            foreach (var property in properties)
            {
                if (property.SetMethod != null)
                {
                    var value = info.GetValue(property.Name, property.PropertyType);
                    property.SetValue(this, value);
                }
            }
        }

        /// <summary>
        /// Creates a new object that is a copy of the current instance.
        /// </summary>
        public virtual Object Clone()
        {
            var type = this.GetType();
            var instance = Activator.CreateInstance(type);
            var properties = type.GetProperties();

            foreach (var property in properties)
            {
                var value = property.GetValue(this);
                property.SetValue(instance, value);
            }

            return instance;
        }
        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        /// <param name="disposing">
        /// Indicate if object is currently freeing, releasing, or resetting unmanaged resources.
        /// </param>
        protected virtual void Dispose(Boolean disposing)
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(GetType().Name);
            }

            _disposed = true;
        }
        /// <summary>
        /// Populates a <seealso cref="SerializationInfo" /> with the data needed to serialize the target object
        /// </summary>
        /// <param name="info">
        /// The <seealso cref="SerializationInfo" /> to populate with data.
        /// </param>
        /// <param name="context">
        /// The destination (see <seealso cref="StreamingContext" />) for this serialization.
        /// </param>
        [ExcludeFromCodeCoverage]
        [Obsolete("This method is obsolete for security reasons")]
        public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            var properties = this.GetType()
                                 .GetProperties();

            foreach (var property in properties)
            {
                var value = property.GetValue(this);
                info.AddValue(property.Name, value);
            }
        }
    }
}
