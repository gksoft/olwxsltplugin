using System;
using System.Windows.Forms;
using WindowsLive.Writer.Api;
using XsltPlugin.Views;

namespace XsltPlugin
{
	public class PluginSidebarEditor : SmartContentEditor
	{
		//private ISmartContent content;
		private PluginSettings settings = new PluginSettings();
		private System.Windows.Forms.OpenFileDialog openXMLFileDialog;
		private System.Windows.Forms.OpenFileDialog openXSLTFileDialog;
		private Label lblTitle;
		private TabControl tbctrlXSLTPlugin;
		private TabPage tbpgSelect;
		private System.Windows.Forms.Integration.ElementHost elementHost1;
		private Select select1;
		

		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		public PluginSidebarEditor()
		{
			this.InitializeComponent();
			this.SelectedContentChanged += new EventHandler(this.OnSelectedContentChanged);
			XSLT.XsltTransformer.OnXSLTLoadingError += new XSLT.XSLTLoadingErrorEventHandler(this.XsltTransformerOnXSLTLoadingError);						

			this.InitXamlUI();
		}

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (this.components != null))
			{
				this.components.Dispose();
			}

			base.Dispose(disposing);
		}

		private void InitializeComponent()
		{
			this.openXMLFileDialog = new System.Windows.Forms.OpenFileDialog();
			this.openXSLTFileDialog = new System.Windows.Forms.OpenFileDialog();
			this.lblTitle = new System.Windows.Forms.Label();
			this.tbctrlXSLTPlugin = new System.Windows.Forms.TabControl();
			this.tbpgSelect = new System.Windows.Forms.TabPage();
			this.elementHost1 = new System.Windows.Forms.Integration.ElementHost();
			this.select1 = new XsltPlugin.Views.Select();
			this.tbctrlXSLTPlugin.SuspendLayout();
			this.tbpgSelect.SuspendLayout();
			this.SuspendLayout();
			// 
			// openXMLFileDialog
			// 
			this.openXMLFileDialog.DereferenceLinks = false;
			this.openXMLFileDialog.Filter = "XML Files(*.xml)|*.xml|All Files(*.*)|*.*";
			this.openXMLFileDialog.Title = "Select an XML Instance file";
			// 
			// openXSLTFileDialog
			// 
			this.openXSLTFileDialog.Filter = "XSLT Files(*.xslt)|*.xslt|XSL Files(*.xsl)|*.xsl|All Files(*.*)|*.*";
			this.openXSLTFileDialog.Title = "Select an XSLT file";
			// 
			// lblTitle
			// 
			this.lblTitle.AutoSize = true;
			this.lblTitle.Dock = System.Windows.Forms.DockStyle.Top;
			this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 14F);
			this.lblTitle.ForeColor = System.Drawing.Color.Navy;
			this.lblTitle.Location = new System.Drawing.Point(0, 0);
			this.lblTitle.Name = "lblTitle";
			this.lblTitle.Size = new System.Drawing.Size(111, 25);
			this.lblTitle.TabIndex = 5;
			this.lblTitle.Text = "XSLT Plugin";
			this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// tbctrlXSLTPlugin
			// 
			this.tbctrlXSLTPlugin.Controls.Add(this.tbpgSelect);
			this.tbctrlXSLTPlugin.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tbctrlXSLTPlugin.Location = new System.Drawing.Point(0, 25);
			this.tbctrlXSLTPlugin.Name = "tbctrlXSLTPlugin";
			this.tbctrlXSLTPlugin.SelectedIndex = 0;
			this.tbctrlXSLTPlugin.Size = new System.Drawing.Size(250, 475);
			this.tbctrlXSLTPlugin.TabIndex = 9;
			// 
			// tbpgSelect
			// 
			this.tbpgSelect.Controls.Add(this.elementHost1);
			this.tbpgSelect.Location = new System.Drawing.Point(4, 24);
			this.tbpgSelect.Name = "tbpgSelect";
			this.tbpgSelect.Padding = new System.Windows.Forms.Padding(3);
			this.tbpgSelect.Size = new System.Drawing.Size(242, 447);
			this.tbpgSelect.TabIndex = 0;
			this.tbpgSelect.Text = "Select";
			this.tbpgSelect.UseVisualStyleBackColor = true;
			// 
			// elementHost1
			// 
			this.elementHost1.Dock = System.Windows.Forms.DockStyle.Top;
			this.elementHost1.Location = new System.Drawing.Point(3, 3);
			this.elementHost1.Margin = new System.Windows.Forms.Padding(0);
			this.elementHost1.Name = "elementHost1";
			this.elementHost1.Size = new System.Drawing.Size(236, 268);
			this.elementHost1.TabIndex = 15;
			this.elementHost1.Text = "elementHost1";
			this.elementHost1.Child = this.select1;
			// 
			// PluginSidebarEditor
			// 
			this.AutoValidate = System.Windows.Forms.AutoValidate.EnablePreventFocusChange;
			this.BackColor = System.Drawing.Color.Transparent;
			this.Controls.Add(this.tbctrlXSLTPlugin);
			this.Controls.Add(this.lblTitle);
			this.Name = "PluginSidebarEditor";
			this.Size = new System.Drawing.Size(250, 500);
			this.tbctrlXSLTPlugin.ResumeLayout(false);
			this.tbpgSelect.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		private void InitXamlUI()
		{
			this.select1.Model = new XsltPlugin.ViewModels.SelectViewModel();

			this.select1.Model.OnXmlClick += new EventHandler(this.OnXmlClick);
			this.select1.Model.OnXsltClick += new EventHandler(this.OnXsltClick);
			this.select1.Model.OnApplyClick += new EventHandler(this.OnApplyClick);
		}

		private void OnSelectedContentChanged(object sender, EventArgs e)
		{
			//this.content = this.SelectedContent;
			this.settings.Properties = this.SelectedContent.Properties;

			// DONE here get the plugin settings
			this.select1.tbXML.Text = this.settings.XMLFile;
			this.select1.tbXSLT.Text = this.settings.XSLTFile;			
		}

		private void OnXmlClick(object sender, EventArgs e)
		{
			if (this.openXMLFileDialog.ShowDialog(this) == DialogResult.OK)
			{
				this.select1.tbXML.Text = this.openXMLFileDialog.FileName;				
			}
		}

		private void OnXsltClick(object sender, EventArgs e)
		{
			if (this.openXSLTFileDialog.ShowDialog(this) == DialogResult.OK)
			{
				this.select1.tbXSLT.Text = this.openXSLTFileDialog.FileName;
			}
		}

		private void OnApplyClick(object sender, EventArgs e)
		{
			// DONE here set the plugin settings			
			this.settings.XMLFile = this.select1.tbXML.Text;
			this.settings.XSLTFile = this.select1.tbXSLT.Text;

			this.GenerateXsltContent();

			this.OnContentEdited();
		}

		private void GenerateXsltContent()
		{
			try
			{
				if (!this.settings.XMLFile.Equals(String.Empty) && !this.settings.XMLFile.Equals(String.Empty))
				{
					if (this.select1.Model.Multiple)
					{
						this.settings.OutputText += XSLT.XsltTransformer.Transform(this.settings.XMLFile, this.settings.XSLTFile);
					}
					else
					{
						this.settings.OutputText = XSLT.XsltTransformer.Transform(this.settings.XMLFile, this.settings.XSLTFile);
					}
				}
			}
			catch (Exception e)
			{
				//this.settings.OutputText = "ERROR! " + e.Message;
				PluginDiagnostics.LogException(e);
			}
		}

		private void XsltTransformerOnXSLTLoadingError(string message)
		{
			//this.settings.OutputText += message;
			PluginDiagnostics.LogError(message);
		}
	}
}
