using System.Configuration;

namespace XsltPlugin.Config
{
	/// <summary>
	/// The Class that will have the XML config file data loaded into it via the configuration Manager.
	/// </summary>
	public class XsltConfigSection : ConfigurationSection
	{
		/// <summary>
		/// The value of the property here "XsltExtensions" needs to match that of the config file section
		/// </summary>
		[ConfigurationProperty("XsltExtensions")]
		public XsltExtensionsCollection XsltExtensions
		{
			get { return ((XsltExtensionsCollection)(base["XsltExtensions"])); }
		}
	}
}
