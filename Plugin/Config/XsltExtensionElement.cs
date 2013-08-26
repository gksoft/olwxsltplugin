using System.Configuration;

namespace XsltPlugin.Config
{
	/// <summary>
	/// The class that holds onto each element returned by the configuration manager.
	/// </summary>
	public class XsltExtensionElement : ConfigurationElement
	{
		[ConfigurationProperty("assemblyPath", DefaultValue = "", IsKey = true, IsRequired = true)]
		public string AssemblyName
		{
			get
			{
				return ((string)(base["assemblyPath"]));
			}

			set
			{
				base["assemblyPath"] = value;
			}
		}

		[ConfigurationProperty("type", DefaultValue = "", IsKey = false, IsRequired = false)]
		public string TypeName
		{
			get
			{
				return ((string)(base["type"]));
			}

			set
			{
				base["type"] = value;
			}
		}

		[ConfigurationProperty("alias", DefaultValue = "", IsKey = false, IsRequired = false)]
		public string Alias
		{
			get
			{
				return ((string)(base["alias"]));
			}

			set
			{
				base["alias"] = value;
			}
		}
	}
}
