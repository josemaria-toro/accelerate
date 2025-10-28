using System;

namespace Zetatech.Accelerate;

/// <summary>
/// Represents a base class for implementing custom cloneable objects.
/// </summary>
[Serializable]
public abstract class BaseCloneable : ICloneable
{
    /// <summary>
    /// Creates a shallow copy of the current object instance.
    /// </summary>
    /// <returns>
    /// A new object that is a copy of this instance.
    /// </returns>
    public virtual Object Clone()
    {
        var type = GetType();
        var instance = Activator.CreateInstance(type);
        var properties = type.GetProperties();

        foreach (var property in properties)
        {
            var value = property.GetValue(this);
            property.SetValue(instance, value);
        }

        return instance;
    }
}