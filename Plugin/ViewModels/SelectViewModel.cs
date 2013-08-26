using System;
using System.Windows.Input;
using AirGeo.Wpf.Mvvm;

namespace XsltPlugin.ViewModels
{
	public class SelectViewModel : ViewModelBase
	{
		private DelegateCommand selectXmlCommand;

		private DelegateCommand selectXsltCommand;

		private DelegateCommand applyCommand;

		private bool multiple;

		public SelectViewModel()
		{
		}
		
		public event EventHandler OnXmlClick;

		public event EventHandler OnXsltClick;

		public event EventHandler OnApplyClick;

		public ICommand SelectXmlCommand
		{
			get
			{
				if (this.selectXmlCommand == null)
				{
					this.selectXmlCommand = new DelegateCommand(this.OnSelectXml);
				}

				return this.selectXmlCommand;
			}
		}

		public ICommand SelectXsltCommand
		{
			get
			{
				if (this.selectXsltCommand == null)
				{
					this.selectXsltCommand = new DelegateCommand(this.OnSelectXslt);
				}

				return this.selectXsltCommand;
			}
		}

		public ICommand ApplyCommand
		{
			get
			{
				if (this.applyCommand == null)
				{
					this.applyCommand = new DelegateCommand(this.OnApply);
				}

				return this.applyCommand;
			}
		}

		public bool Multiple 
		{
			get
			{
				return this.multiple;
			}

			set
			{
				this.multiple = value;
				this.OnPropertyChanged("Multiple");
			}
		}

		private void OnSelectXml()
		{
			if (this.OnXmlClick != null)
			{
				this.OnXmlClick(this, EventArgs.Empty);
			}
		}

		private void OnSelectXslt()
		{
			if (this.OnXsltClick != null)
			{
				this.OnXsltClick(this, EventArgs.Empty);
			}
		}

		private void OnApply()
		{
			if (this.OnApplyClick != null)
			{
				this.OnApplyClick(this, EventArgs.Empty);
			}
		}
	}
}
