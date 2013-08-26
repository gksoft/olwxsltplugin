using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Xsl;

namespace XSLT
{
	public delegate void XSLTLoadingErrorEventHandler(string message);

	internal class XsltTransformer
	{
		private static XslCompiledTransform trnsfrm = new XslCompiledTransform();

		private static XsltArgumentList args = new XsltArgumentList();

		public static event XSLTLoadingErrorEventHandler OnXSLTLoadingError;

		/// <summary>
		///  loads the xslt extension objects
		/// </summary>
		public static void AddXsltExtensionObject(string namespaceUri, object extension)
		{
			if (!XsltExtensionObjectExists(namespaceUri))
			{
				args.AddExtensionObject(namespaceUri, extension);
			}
		}

		/// <summary>
		/// checks if extension exists
		/// </summary>
		/// <param name="namespaceUri"></param>
		/// <returns></returns>
		public static bool XsltExtensionObjectExists(string namespaceUri)
		{
			return (args.GetExtensionObject(namespaceUri) != null);
		}

		/// <summary>
		/// transforms to a string
		/// </summary>
		/// <param name="xmlFile">the path to xml file</param>
		/// <param name="xsltFile">the path to xslt file</param>
		/// <returns>the transformed content as string</returns>
		public static string Transform(string xmlFile, string xsltFile)
		{
			string result = null;
			try
			{
				if (LoadXslt(xsltFile))
				{
					XmlWriterSettings xmlWriterSettings = trnsfrm.OutputSettings;
					StringBuilder output = new StringBuilder();
					XmlWriter xmlWriter = XmlWriter.Create(output, xmlWriterSettings);
					trnsfrm.Transform(xmlFile, args, xmlWriter);    // the transformation  with extension objects in args

					result = output.ToString();
				}
			}
			catch (Exception e)
			{
				if (OnXSLTLoadingError != null)
				{
					OnXSLTLoadingError(e.Message);
				}
			}

			return result;
		}

		/// <summary>
		/// transforms to a file 
		/// </summary>
		/// <param name="xmlFile">the path to xml file</param>
		/// <param name="xsltFile">the path to xslt file</param>
		/// <param name="outputFile">the path to output file</param>
		/// <returns></returns>
		public static void Transform(string xmlFile, string xsltFile, string outputFile)
		{
			if (LoadXslt(xsltFile))								// load the xslt stylesheet
			{
				trnsfrm.Transform(xmlFile, outputFile);			// transform the xml file
			}
		}

		/// <summary>
		/// transforms to a string
		/// </summary>
		/// <param name="xml"></param>
		/// <param name="xslt"></param>
		/// <returns>the transformed string </returns>
		public static string Transform(XmlReader xml, XmlReader xslt)
		{
			string result = null;

			try
			{
				if (LoadXslt(xslt))
				{
					XmlWriterSettings xmlWriterSettings = trnsfrm.OutputSettings;
					StringBuilder output = new StringBuilder();
					XmlWriter xmlWriter = XmlWriter.Create(output, xmlWriterSettings);
					trnsfrm.Transform(xml, args, xmlWriter);    // the transformation  with extension objects in args    // the xml instance

					result = output.ToString();
				}
			}
			catch (Exception e)
			{
				if (OnXSLTLoadingError != null)
				{
					OnXSLTLoadingError(e.Message);
				}
			}


			return result;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="xml">xml in reader format</param>
		/// <param name="xslt">the path to xslt file</param>
		/// <returns>the transformed string</returns>
		public static string Transform(XmlReader xml, string xslt)
		{
			string result = null;

			try
			{
				if (LoadXslt(xslt))
				{
					XmlWriterSettings xmlWriterSettings = trnsfrm.OutputSettings;
					StringBuilder output = new StringBuilder();
					XmlWriter xmlWriter = XmlWriter.Create(output, xmlWriterSettings);
					trnsfrm.Transform(xml, args, xmlWriter);    // the transformation  with extension objects in args    // the xml instance

					result = output.ToString();
				}
			}
			catch (Exception e)
			{
				if (OnXSLTLoadingError != null)
				{
					OnXSLTLoadingError(e.Message);
				}
			}

			return result;
		}

		//public static void CompileToFile(string xsltFile)
		//{
		//    // XslCompiledTransform.CompileToType();
		//}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="xslt">the path to xslt file</param>
		/// <returns>true if succeeds</returns>
		protected static bool LoadXslt(string xslt)
		{
			bool result = false;
			string errorMessage = String.Empty;

			try
			{
				trnsfrm.Load(xslt, XsltSettings.TrustedXslt, new XmlUrlResolver());  // the xslt stylesheet, enable embedded scripting and document	
				result = true;
			}
			catch (Exception e)
			{
				errorMessage = e.Message + "\n" + e.StackTrace;
				if (OnXSLTLoadingError != null)
				{
					OnXSLTLoadingError(errorMessage);
				}
			}

			return result;
		}

		protected static bool LoadXslt(XmlReader xslt)
		{
			bool result = false;
			string errorMessage = String.Empty;

			try
			{
				trnsfrm.Load(xslt, XsltSettings.TrustedXslt, new XmlUrlResolver());  // the xslt stylesheet, enable embedded scripting and document	
				result = true;
			}
			catch (Exception e)
			{
				errorMessage = e.Message + "\n" + e.StackTrace;
				if (OnXSLTLoadingError != null)
				{
					OnXSLTLoadingError(errorMessage);
				}
			}

			return result;
		}
	}
}
