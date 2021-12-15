namespace Xamarin.Forms.Platform.Android
{
	public interface IObservableItemsViewSource : IItemsViewSource
	{
		bool ObserveChanges { get; set; }
	}
}