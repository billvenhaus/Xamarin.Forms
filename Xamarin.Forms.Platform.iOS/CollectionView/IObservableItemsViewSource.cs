using System;

namespace Xamarin.Forms.Platform.iOS
{
	public interface IObservableItemsViewSource : IItemsViewSource
	{
		bool ObserveChanges { get; set; }
	}
}