using System;
using System.Windows;
using System.Windows.Controls;
using XsltPlugin.ViewModels;

namespace XsltPlugin.Views
{	
	/// <summary>
	/// Interaction logic for Select.xaml
	/// </summary>
	public partial class Select : UserControl
	{
		public Select()
		{
			this.InitializeComponent();				
		}

		public SelectViewModel Model 
		{
			get
			{
				return (SelectViewModel)this.DataContext;
			}
			
			set 
			{
				this.DataContext = value;
			}
		}
	}
}
