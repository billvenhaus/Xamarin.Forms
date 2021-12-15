using Android.Content;

namespace Xamarin.Forms.Platform.Android
{
	public class CollectionViewRenderer : ReorderableItemsViewRenderer<ReorderableItemsView, ReorderableItemsViewAdapter<ReorderableItemsView, IGroupableItemsViewSource>, IGroupableItemsViewSource>
	{
		public CollectionViewRenderer(Context context) : base(context)
		{
		}
	}
}