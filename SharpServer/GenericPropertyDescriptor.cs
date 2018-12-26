using System;
using System.ComponentModel;

namespace SharpServer
{
    public class GenericPropertyDescriptor<T> : PropertyDescriptor
    {
        private T _value;

        public GenericPropertyDescriptor(string name, Attribute[] attrs)
            : base(name, attrs)
        {
        }

        public GenericPropertyDescriptor(string name, T value, Attribute[] attrs)
            : base(name, attrs)
        {
            _value = value;
        }

        public override bool CanResetValue(object component)
        {
            return false;
        }

        public override System.Type ComponentType
        {
            get
            {
                return typeof(GenericPropertyDescriptor<T>);
            }
        }

        public override object GetValue(object component)
        {
            return _value;
        }

        public override bool IsReadOnly
        {
            get
            {
                return Array.Exists(this.AttributeArray,
                   attr => attr is ReadOnlyAttribute);
            }
        }

        public override System.Type PropertyType
        {
            get
            {
                return typeof(T);
            }
        }

        public override void ResetValue(object component)
        {
        }

        public override void SetValue(object component, object value)
        {
            _value = (T)value;
        }

        public override bool ShouldSerializeValue(object component)
        {
            return false;
        }
    }
}

