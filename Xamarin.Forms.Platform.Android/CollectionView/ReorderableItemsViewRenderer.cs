using System.ComponentModel;
using Android.Content;
using AndroidX.RecyclerView.Widget;

namespace Xamarin.Forms.Platform.Android
{
	public class ReorderableItemsViewRenderer<TItemsView, TAdapter, TItemsViewSource> : GroupableItemsViewRenderer<TItemsView, TAdapter, TItemsViewSource>
		where TItemsView : ReorderableItemsView
		where TAdapter : ReorderableItemsViewAdapter<TItemsView, TItemsViewSource>
		where TItemsViewSource : IGroupableItemsViewSource
	{
		ItemTouchHelper _itemTouchHelper;
		SimpleItemTouchHelperCallback _itemTouchHelperCallback;

		public ReorderableItemsViewRenderer(Context context) : base(context)
		{
		}

		protected override TAdapter CreateAdapter()
		{
			return (TAdapter)new ReorderableItemsViewAdapter<TItemsView, TItemsViewSource>(ItemsView);
		}

		protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs changedProperty)
		{
			base.OnElementPropertyChanged(sender, changedProperty);

			if (changedProperty.Is(ReorderableItemsView.CanReorderItemsProperty))
			{
				UpdateCanReorderItems();
			}
		}

		protected override void SetUpNewElement(TItemsView newElement)
		{
			base.SetUpNewElement(newElement);

			UpdateCanReorderItems();
		}

		protected override void TearDownOldElement(ItemsView oldElement)
		{
			base.TearDownOldElement(oldElement);

			if (_itemTouchHelper != null)
			{
				_itemTouchHelper.AttachToRecyclerView(null);
				_itemTouchHelper.Dispose();
				_itemTouchHelper = null;
			}

			if (_itemTouchHelperCallback != null)
			{
				_itemTouchHelperCallback.Dispose();
				_itemTouchHelperCallback = null;
			}
		}

		protected override void UpdateAdapter()
		{
			base.UpdateAdapter();

			_itemTouchHelperCallback?.SetAdapter(ItemsViewAdapter);
		}

		void UpdateCanReorderItems()
		{
			var canReorderItems = (ItemsView as ReorderableItemsView)?.CanReorderItems == true;

			if (canReorderItems)
			{
				if (_itemTouchHelperCallback == null)
				{
					_itemTouchHelperCallback = new SimpleItemTouchHelperCallback();
				}
				if (_itemTouchHelper == null)
				{
					_itemTouchHelper = new ItemTouchHelper(_itemTouchHelperCallback);
					_itemTouchHelper.AttachToRecyclerView(this);
				}
				_itemTouchHelperCallback.SetAdapter(ItemsViewAdapter);
			}
			else
			{
				if (_itemTouchHelper != null)
				{
					_itemTouchHelper.AttachToRecyclerView(null);
					_itemTouchHelper.Dispose();
					_itemTouchHelper = null;
				}
				if (_itemTouchHelperCallback != null)
				{
					_itemTouchHelperCallback.Dispose();
					_itemTouchHelperCallback = null;
				}
			}
		}
	}
}