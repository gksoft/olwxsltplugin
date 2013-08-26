using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WindowsLive.Writer.Api;

namespace XsltPlugin
{
	internal class PluginSettings
	{
		private const string DEFAULTOUTPUTTEXT = "<div style='background-color=yellow'>XSLT Plugin Content</div>";
		private const string OUTPUTTEXT = "OUTPUTTEXT";
		private const string XMLFILE = "XMLFILE";
		private const string XSLTFILE = "XSLTFILE";
		private const string MULTIPLE = "MULTIPLE";
		private IProperties settings;

		public PluginSettings()
		{
		}

		public IProperties Properties
		{
			set
			{
				this.settings = value;
			}
		}

		public virtual string XMLFile
		{
			get
			{
				return this.settings.GetString(XMLFILE, String.Empty);
			}

			set
			{
				this.settings.SetString(XMLFILE, value);
			}
		}

		public virtual string XSLTFile
		{
			get
			{
				return this.settings.GetString(XSLTFILE, String.Empty);
			}

			set
			{
				this.settings.SetString(XSLTFILE, value);
			}
		}

		public virtual bool Multiple
		{
			get
			{
				return this.settings.GetBoolean(MULTIPLE, false);
			}

			set
			{
				this.settings.SetBoolean(MULTIPLE, value);
			}
		}

		public virtual string OutputText
		{
			get
			{
				return this.settings.GetString(OUTPUTTEXT, DEFAULTOUTPUTTEXT);
			}

			set
			{
				this.settings.SetString(OUTPUTTEXT, value);
			}
		}
	}
}
