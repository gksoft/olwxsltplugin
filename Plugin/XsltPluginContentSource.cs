using System;
using System.Collections.Generic;
using System.Windows.Forms;
using WindowsLive.Writer.Api;


namespace XsltPlugin
{
	[WriterPlugin("514f8bdc-1c58-4e9c-ab94-19b3945b55fd", "XSLT Plugin", PublisherUrl = "code-d-code.blogspot.com", Description = "", HasEditableOptions = false, ImagePath = "Images.xml.png")] //ImagePath must be an embedded resource to reference it this way
	[InsertableContentSource("XSLT Plugin", MenuText = "New Plugin MenuText", SidebarText = "XSLT Plugin")]
	//[UrlContentSource("")]
	//[LiveClipboardContentSource("XSLT Plugin", "Format for XSLT plugin")]
	public class XsltPluginContentSource : SmartContentSource
	{
		private PluginSettings settings = new PluginSettings();

		public XsltPluginContentSource()
		{
		}

		/// <summary>
		/// create the content
		/// </summary>
		/// <param name="dialogOwner"></param>
		/// <param name="newContent"></param>
		/// <returns></returns>
		public override DialogResult CreateContent(IWin32Window dialogOwner, ISmartContent newContent)
		{
#if USEINPUTFORM
            using (PluginInputForm form = new PluginInputForm())
            {
                DialogResult result = form.ShowDialog(dialogOwner);
                //base.CreateContent(dialogOwner, newContent);

                return DialogResult.OK;
            }
#endif

			return DialogResult.OK;
		}

//        /// <summary>
//        /// create the content from liveclipboard
//        /// </summary>
//        /// <param name="dialogOwner"></param>
//        /// <param name="lcDocument"></param>
//        /// <param name="newContent"></param>
//        /// <returns></returns>
//        public override DialogResult CreateContentFromLiveClipboard(IWin32Window dialogOwner, System.Xml.XmlDocument lcDocument, ISmartContent newContent)
//        {
//            return base.CreateContentFromLiveClipboard(dialogOwner, lcDocument, newContent);
//        }

//        /// <summary>
//        /// create content from Url
//        /// </summary>
//        /// <param name="url"></param>
//        /// <param name="title"></param>
//        /// <param name="newContent"></param>
//        public override void CreateContentFromUrl(string url, ref string title, ISmartContent newContent)
//        {
//            base.CreateContentFromUrl(url, ref title, newContent);
//        }

		/// <summary>
		/// create the sidebar editor
		/// </summary>
		/// <param name="editorSite"></param>
		/// <returns></returns>
		public override SmartContentEditor CreateEditor(ISmartContentEditorSite editorSite)
		{
			PluginSidebarEditor sidebarEditor = new PluginSidebarEditor();

			return sidebarEditor;
		}

		/// <summary>
		/// generate what will be shown in editor source
		/// </summary>
		/// <param name="content"></param>
		/// <param name="publishingContext"></param>
		/// <returns></returns>
		//public override string GenerateEditorHtml(ISmartContent content, IPublishingContext publishingContext)
		//{
		//    this.settings.Properties = content.Properties;

		//    // settings.OutputText = XSLT.XsltTransformer.Transform(settings.XMLFile, settings.XSLTFile);
		//    return this.settings.OutputText;
		//}

		/// <summary>
		/// generate what will be published
		/// </summary>
		/// <param name="content"></param>
		/// <param name="publishingContext"></param>
		/// <returns></returns>
		public override string GeneratePublishHtml(ISmartContent content, IPublishingContext publishingContext)
		{
			this.settings.Properties = content.Properties;

			return this.settings.OutputText;
		}

		/// <summary>
		/// override global Options,shown in Plugin preferences panel
		/// </summary>
		/// <param name="dialogOwner"></param>
		public override void EditOptions(IWin32Window dialogOwner)
		{
			//Options.SetBoolean("Option1", true);		// here create the global options
			//PluginSettings settings = new PluginSettings(base.Options);
			//this.settings.ShowDialog(dialogOwner);
			// base.EditOptions(dialogOwner);			
		}

		public override void Initialize(IProperties pluginOptions)
		{
			XsltExtensionsManager xsltExtensionsManager = new XsltExtensionsManager();
			xsltExtensionsManager.OnError += new Action<string>(this.XsltExtensionsManagerOnError);
			xsltExtensionsManager.GetExtensionsFromConfig();


			pluginOptions.SetBoolean("Option1", false);
			base.Initialize(pluginOptions);
		}

		private void XsltExtensionsManagerOnError(string message)
		{
			PluginDiagnostics.LogError(message);
		}
	}

	#region Other Utility Classes
	// HtmlServices
	// PluginDiagnostics
	// PluginHttpRequest
	// HtmlServices
	// HtmlScreenCapture
	// AdaptiveHtmlObject
	#endregion
}
