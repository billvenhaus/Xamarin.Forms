﻿using System.Collections.Specialized;
using System.ComponentModel;
using Windows.UI.Xaml.Controls;

namespace Xamarin.Forms.Platform.UWP
{
	public partial class ReorderableItemsViewRenderer<TItemsView> : GroupableItemsViewRenderer<TItemsView> where TItemsView : ReorderableItemsView
	{
		bool _trackerAllowDrop;

		void HandleDragItemsStarting(object sender, DragItemsStartingEventArgs e)
		{
			// Built in reordering only supports ungrouped sources & observable collections.
			var supportsReorder = Element != null && !Element.IsGrouped && Element.ItemsSource is INotifyCollectionChanged;
			if (supportsReorder)
			{
				// The AllowDrop property needs to be enabled when we start the drag operation.
				// We can't simply enable it when we set CanReorderItems because the VisualElementTracker also updates this property.
				// That means the tracker can overwrite any set we do in UpdateCanReorderItems.
				// To avoid that possibility, let's force it to true when the user begins to drag an item.
				// Reset it back to what it was when finished.
				_trackerAllowDrop = ListViewBase.AllowDrop;
				ListViewBase.AllowDrop = true;
			}
			else
			{
				e.Cancel = true;
			}
		}

		void HandleDragItemsCompleted(ListViewBase sender, DragItemsCompletedEventArgs args)
		{
			ListViewBase.AllowDrop = _trackerAllowDrop;

			Element?.SendReorderCompleted();
		}

		protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs changedProperty)
		{
			base.OnElementPropertyChanged(sender, changedProperty);

			if (changedProperty.PropertyName == ReorderableItemsView.CanReorderItemsProperty.PropertyName)
			{
				UpdateCanReorderItems();
			}
		}

		protected override void SetUpNewElement(ItemsView newElement)
		{
			base.SetUpNewElement(newElement);

			if (newElement == null || ListViewBase == null)
			{
				return;
			}

			ListViewBase.DragItemsStarting += HandleDragItemsStarting;
			ListViewBase.DragItemsCompleted += HandleDragItemsCompleted;

			UpdateCanReorderItems();
		}

		protected override void TearDownOldElement(ItemsView oldElement)
		{
			if (ListViewBase != null)
			{
				ListViewBase.DragItemsStarting -= HandleDragItemsStarting;
				ListViewBase.DragItemsCompleted -= HandleDragItemsCompleted;
			}

			base.TearDownOldElement(oldElement);
		}

		void UpdateCanReorderItems()
		{
			if (Element == null || ListViewBase == null)
			{
				return;
			}

			if (Element.CanReorderItems)
			{
				ListViewBase.CanDragItems = true;
				ListViewBase.CanReorderItems = true;
				ListViewBase.IsSwipeEnabled = true; // Needed so user can reorder with touch (according to docs).
			}
			else
			{
				ListViewBase.CanDragItems = false;
				ListViewBase.CanReorderItems = false;
				ListViewBase.IsSwipeEnabled = false;
			}
		}
	}
}