using System;
using System.Collections.Generic;
using System.ComponentModel;


namespace SharpServer
{
    public class StoredProcedureInfo : ICustomTypeDescriptor
    {
        private dynamic spHead;
        private string spName;

        public Dictionary<string, ValueChooserInfo> propertyChoosers = new Dictionary<string, ValueChooserInfo>();

        private PropertyDescriptorCollection propertyDescriptors =
          new PropertyDescriptorCollection(null);


        public StoredProcedureInfo(dynamic iHead, string Name)
        {
            this.spHead = iHead;
            this.spName = Name;
        }

        public dynamic Head
        {
            get { return spHead; }
        }

        public void AddProperty<T>(
          string name,
          T value,
          string displayName,
          string description,
          string category,
          bool readOnly,
          ValueChooserInfo chooser,
          IEnumerable<Attribute> attributes)
        {
            var attrs = attributes == null ? new List<Attribute>()
                                           : new List<Attribute>(attributes);

            if (!String.IsNullOrEmpty(displayName))
                attrs.Add(new DisplayNameAttribute(displayName));

            if (!String.IsNullOrEmpty(description))
                attrs.Add(new DescriptionAttribute(description));

            if (!String.IsNullOrEmpty(category))
                attrs.Add(new CategoryAttribute(category));

            if (readOnly)
                attrs.Add(new ReadOnlyAttribute(true));

            if (chooser != null)
            {

                propertyChoosers.Add(name, chooser);
                //switch (chooser.Vocabulary.ToUpper())
                //{
                //    case ("FILE"): attrs.Add(new EditorAttribute(typeof(System.Windows.Forms.Design.FileNameEditor), typeof(System.Drawing.Design.UITypeEditor))); break;
                //    case ("EDSFILE"): attrs.Add(new EditorAttribute(typeof(EdsFileEditor), typeof(System.Drawing.Design.UITypeEditor))); break;
                //        //default: attrs.Add(new EditorAttribute(typeof(ChooserEditor), typeof(System.Drawing.Design.UITypeEditor))); break;
                //}
            }

            propertyDescriptors.Add(new GenericPropertyDescriptor<T>(
              name, value, attrs.ToArray()));
        }

        public void AddProperty<T>(
          string name,
          T value,
          string description,
          string category,
          bool readOnly,
          ValueChooserInfo chooser)
        {
            AddProperty<T>(name, value, name, description, category, readOnly, chooser, null);
        }

        public void RemoveProperty(string propertyName)
        {
            var descriptor = propertyDescriptors.Find(propertyName, true);
            if (descriptor != null)
                propertyDescriptors.Remove(descriptor);
            else
                throw new Exception("Property is not found");
        }

        private object GetPropertyValue(string propertyName)
        {
            var descriptor = propertyDescriptors.Find(propertyName, true);
            if (descriptor != null)
                return descriptor.GetValue(null);
            else
                return null;
        }

        private void SetPropertyValue(string propertyName, object value)
        {
            var descriptor = propertyDescriptors.Find(propertyName, true);
            if (descriptor != null)
                descriptor.SetValue(null, value);
            else
                throw new Exception("Property is not found");
        }

        public object this[string propertyName]
        {
            get { return GetPropertyValue(propertyName); }
            set { SetPropertyValue(propertyName, value); }
        }

        public bool HasProperty(string propertyName)
        {
            return propertyDescriptors.Find(propertyName, true) != null;
        }

        public string GetClassName()
        {
            return "Хранимая процедура";
        }

        public string GetComponentName()
        {
            return spName;
        }

        public AttributeCollection GetAttributes()
        {
            return TypeDescriptor.GetAttributes(this, true);
        }

        public TypeConverter GetConverter()
        {
            return TypeDescriptor.GetConverter(this, true);
        }

        public EventDescriptor GetDefaultEvent()
        {
            return TypeDescriptor.GetDefaultEvent(this, true);
        }

        public PropertyDescriptor GetDefaultProperty()
        {
            return TypeDescriptor.GetDefaultProperty(this, true);
        }

        public object GetEditor(Type editorBaseType)
        {
            return TypeDescriptor.GetEditor(this, editorBaseType, true);
        }

        public EventDescriptorCollection GetEvents()
        {
            return TypeDescriptor.GetEvents(this, true);
        }

        public EventDescriptorCollection GetEvents(Attribute[] attributes)
        {
            return TypeDescriptor.GetEvents(this, attributes, true);
        }

        public PropertyDescriptorCollection GetProperties()
        {
            return propertyDescriptors;
        }

        public PropertyDescriptorCollection GetProperties(Attribute[] attributes)
        {
            return propertyDescriptors;
        }

        public object GetPropertyOwner(PropertyDescriptor pd)
        {
            return this;
        }
    }
    //class EdsFileEditor : FileNameEditor
    //{
    //    protected override void InitializeDialog(OpenFileDialog ofd)
    //    {
    //        ofd.CheckFileExists = true;
    //        ofd.Filter = "Eds files (*.sig)|*.sig|All files (*.*)|*.*";
    //    }
    //}
}
