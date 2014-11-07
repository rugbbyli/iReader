using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace iReader.Controls
{
	public partial class TextListView : ItemsControl
	{
		Book ItemsList
		{
			get
			{
				return ItemsSource as Book;
			}
		}

		public VirtualizingStackPanel Panel { get; private set; }

		public TextListView()
		{
			InitializeComponent();
			
		}

		void Panel_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
			//System.Diagnostics.Debug.WriteLine(Panel.VerticalOffset);
			var count = this.Items.Count;
			var count2 = this.Panel.Children.Count;
			var loc = count2 / 4;
			var cur = Panel.Children[Panel.Children.Count / 2] as ContentPresenter;
			
			System.Diagnostics.Debug.WriteLine("current item:" + (cur.Content as Page).ID);
		}

		protected override void PrepareContainerForItemOverride(DependencyObject element, object item)
		{
			base.PrepareContainerForItemOverride(element, item);
			//var index = ItemsList.IndexOf(item.ToString());
			//System.Diagnostics.Debug.WriteLine("---------prepare : " + index + "-----------");
		}

		protected override void ClearContainerForItemOverride(DependencyObject element, object item)
		{
			//var index = ItemsList.IndexOf(item.ToString());
			base.ClearContainerForItemOverride(element, item);
			//System.Diagnostics.Debug.WriteLine("---------clear : " + index + "-----------");
		}

		protected override DependencyObject GetContainerForItemOverride()
		{
			System.Diagnostics.Debug.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().Name);
			return base.GetContainerForItemOverride();
		}

		protected override bool IsItemItsOwnContainerOverride(object item)
		{
			//var index = ItemsList.IndexOf(item.ToString());
			//System.Diagnostics.Debug.WriteLine("---------search : " + index + "-----------");
			return base.IsItemItsOwnContainerOverride(item);
		}

		private void ItemsPanelTemplate_Panel_Loaded(object sender, RoutedEventArgs e)
		{
			Panel = (sender as VirtualizingStackPanel);
			Panel.MouseLeftButtonUp += Panel_MouseLeftButtonUp;
			OnInited();
		}

		public event EventHandler Inited;
		void OnInited()
		{
			if (Inited != null)
			{
				Inited(this, EventArgs.Empty);
			}
		}
	}
}
