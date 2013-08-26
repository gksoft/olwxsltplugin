using System.ComponentModel;

namespace AirGeo.Wpf.Mvvm
{
	/// <summary>
	/// Provides common functionality for ViewModel classes
	/// </summary>
	public abstract class ViewModelBase : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		public static void OnPropertyChanged(object sender,PropertyChangedEventHandler propertyChanged,string propertyName)
		{
			if (propertyChanged != null)
			{
				propertyChanged(sender, new PropertyChangedEventArgs(propertyName));
			}
		}

		protected void OnPropertyChanged(string propertyName)
		{
			PropertyChangedEventHandler handler = this.PropertyChanged;

			if (handler != null)
			{
				handler(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
}
