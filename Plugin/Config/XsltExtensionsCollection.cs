using System;
using System.Configuration;

namespace XsltPlugin.Config
{
	/// <summary>
	/// The collection class that will store the list of each element/item that
	///        is returned back from the configuration manager.
	/// </summary>
	[ConfigurationCollection(typeof(XsltExtensionElement))]
	public class XsltExtensionsCollection : ConfigurationElementCollection
	{
		internal const string PropertyName = "extension";

		public override ConfigurationElementCollectionType CollectionType
		{
			get
			{
				return ConfigurationElementCollectionType.BasicMapAlternate;
			}
		}

		protected override string ElementName
		{
			get
			{
				return PropertyName;
			}
		}

		public XsltExtensionElement this[int idx]
		{
			get
			{
				return (XsltExtensionElement)BaseGet(idx);
			}
		}		

		protected override bool IsElementName(string elementName)
		{
			return elementName.Equals(PropertyName, StringComparison.InvariantCultureIgnoreCase);
		}

		protected override ConfigurationElement CreateNewElement()
		{
			return new XsltExtensionElement();
		}

		protected override object GetElementKey(ConfigurationElement element)
		{
			return ((XsltExtensionElement)(element)).AssemblyName;
		}
	}
}
