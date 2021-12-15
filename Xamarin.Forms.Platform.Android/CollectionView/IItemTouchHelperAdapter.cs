namespace Xamarin.Forms.Platform.Android
{
	public interface IItemTouchHelperAdapter
	{
		bool OnItemMove(int fromPosition, int toPosition);
	}
}