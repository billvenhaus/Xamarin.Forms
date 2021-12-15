using System.ComponentModel;

namespace Xamarin.Forms.Platform.iOS
{
	public class ReorderableItemsViewRenderer<TItemsView, TViewController> : SelectableItemsViewRenderer<TItemsView, TViewController>
		where TItemsView : ReorderableItemsView
		where TViewController : ReorderableItemsViewController<TItemsView>
	{
		[Internals.Preserve(Conditional = true)]
		public ReorderableItemsViewRenderer() { }

		protected override TViewController CreateController(TItemsView itemsView, ItemsViewLayout layout)
		{
			return new ReorderableItemsViewController<TItemsView>(itemsView, layout) as TViewController;
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

			if (newElement == null)
			{
				return;
			}

			UpdateCanReorderItems();
		}

		void UpdateCanReorderItems()
		{
			Controller?.UpdateCanReorderItems();
		}
	}
}