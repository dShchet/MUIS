using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.Xml;

namespace SharpServer
{
    [Serializable]
    [XmlRoot(ElementName = "EapiStoredProcedureInfo")]
    public class EapiStoredProcedureInfo
    {
        public EapiStoredProcedureInfo()
        {
            EapiStoredProcedure = new List<EapiStoredProcedure>();
        }

        [XmlElement]
        public List<EapiStoredProcedure> EapiStoredProcedure { get; set; }
    }

    [Serializable]
    public class EapiStoredProcedure
    {
        [XmlAttribute]
        public string Name { get; set; }

        [XmlAttribute]
        public string Alias { get; set; }

        [XmlAttribute]
        public string Group { get; set; }

        public string Note { get; set; }

        public ArgumentInfo ArgumentInfo { get; set; }

        public EapiStoredProcedure()
        {
            ArgumentInfo = new ArgumentInfo();
        }
    }

    [Serializable]
    public class ArgumentInfo
    {
        public ArgumentInfo()
        {
            Argument = new List<Argument>();
        }

        [XmlElement]
        public List<Argument> Argument { get; set; }
    }

    [Serializable]
    public class Argument
    {
        [XmlAttribute]
        public string Name { get; set; }

        [XmlAttribute]
        public string DataType { get; set; }

        [XmlAttribute]
        public string Direction { get; set; }

        [XmlAttribute]
        public string Visible { get; set; }

        [XmlAttribute]
        public string Nullable { get; set; }

        public string Note { get; set; }

        public string ValidValuesNote { get; set; }

        public ValueChooserInfo ValueChooser { get; set; }

        public Argument()
        {
            ValueChooser = new ValueChooserInfo();
        }
    }

    [Serializable]
    public class ValueChooserInfo
    {
        [XmlAttribute]
        public string Vocabulary { get; set; }

        [XmlAttribute]
        public string ResultValue { get; set; }

        [XmlAttribute]
        public string Select { get; set; }

        [XmlAttribute]
        public string ItemType { get; set; }
    }
}
