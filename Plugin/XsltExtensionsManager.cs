using System;
using System.Configuration;
using System.Reflection;
using XsltPlugin.Config;

namespace XsltPlugin
{
	public class XsltExtensionsManager
	{
		private const string ConfigFile = @"\Xsltplugin.dll.config";

		private const string ConfigSection = "XsltPlugin";

		public event Action<string> OnError;

		/// <summary>
		/// Get Extension objects from the config
		/// </summary>
		public void GetExtensionsFromConfig()
		{
			Configuration config = this.GetConfigFile();

			XsltConfigSection section = (XsltConfigSection)config.GetSection(ConfigSection);
			//XsltConfigSection section = (XsltConfigSection)ConfigurationManager.GetSection("XsltPlugin");

			if (section != null)
			{
				foreach (XsltExtensionElement x in section.XsltExtensions)
				{
					try
					{
						Assembly asm = Assembly.LoadFile(x.AssemblyName);			//must be full path to assembly
						this.AddExtensionsToXslt(asm, x.TypeName, x.Alias);
					}
					catch (Exception e)
					{
						if (this.OnError != null)
						{
							this.OnError(e.Message);
						}
					}
				}
			}
		}

		/// <summary>
		///  adds extension objects to xslt processor
		/// </summary>
		/// <param name="asm"></param>
		/// <param name="typeName"></param>
		/// <param name="alias"></param>
		public void AddExtensionsToXslt(Assembly asm, string typeName, string alias)
		{
			string urn = "urn:";
			bool all = (typeName == String.Empty);						  //if type is not set then add all assembly

			if (all)
			{
				this.AddAssemblyToXslt(asm, urn);
			}
			else
			{
				this.AddTypeToXslt(asm, typeName, urn, alias);
			}
		}

		/// <summary>
		/// adds all qualifying objects of the assembly into the xslt processor
		/// </summary>
		/// <param name="asm">the assembly</param>
		/// <param name="urn">the namespace</param>
		private void AddAssemblyToXslt(Assembly asm, string urn)
		{
			foreach (Type t in asm.GetExportedTypes())						//get all the public exposed types
			{
				this.AddToXslt(asm, t, urn, string.Empty);
			}
		}

		/// <summary>
		/// adds the object into the xslt processor
		/// </summary>
		/// <param name="asm">the assembly</param>
		/// <param name="typeName">the type to add</param>
		/// <param name="urn">the namespace</param>
		private void AddTypeToXslt(Assembly asm, string typeName, string urn, string alias)
		{
			Type t = asm.GetType(typeName);								//get the defined type, must be full namespace name		

			this.AddToXslt(asm, t, urn, alias);

			if (t == null)												//Notify of type does not exist
			{
				if (this.OnError != null)
				{
					this.OnError("Xslt Extension Type " + typeName + " does not Exist. Make sure you use a full namespace name");
				}
			}
		}

		/// <summary>
		/// add the type into the xslt processor
		/// </summary>
		/// <param name="asm"></param>
		/// <param name="typeObj"></param>
		/// <param name="urn"></param>
		private void AddToXslt(Assembly asm, Type typeObj, string urn, string alias)
		{
			string objectName = string.Empty;
			bool aliased = (alias != string.Empty);

			if (typeObj != null)											//if type exists
			{
				if (typeObj.IsClass && !typeObj.IsAbstract &&
					(typeObj.GetConstructor(Type.EmptyTypes) != null) &&
					!typeObj.IsGenericType)	//make sure it is a class instanciable with only default constructor and not a generic type
				{
					if (aliased)
					{
						objectName = urn + alias;
					}
					else
					{
						objectName = urn + typeObj.FullName;
					}

					if (!XSLT.XsltTransformer.XsltExtensionObjectExists(objectName)) //check if it is already added
					{
						object extension = asm.CreateInstance(typeObj.FullName);
						XSLT.XsltTransformer.AddXsltExtensionObject(objectName, extension);
					}
				}
			}
		}

		private Configuration GetConfigFile()
		{
			//string assemblyPath = new Uri(Assembly.GetExecutingAssembly().CodeBase).AbsolutePath;
			//Configuration cfg = ConfigurationManager.OpenExeConfiguration(assemblyPath+".config");	
			ExeConfigurationFileMap filemap = new ExeConfigurationFileMap();
			filemap.ExeConfigFilename = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + ConfigFile;
			Configuration config = ConfigurationManager.OpenMappedExeConfiguration(filemap, ConfigurationUserLevel.None);

			return config;
		}
	}
}
