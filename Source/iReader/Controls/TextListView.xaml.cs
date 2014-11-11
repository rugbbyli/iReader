using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Windows.Controls.Primitives;
using LinqToVisualTree;

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

        public ScrollBar ScrollBar { get; private set; }

        public TextListView()
        {
            InitializeComponent();
            this.Opacity = 0;
        }

        //void SetScrollViewerBinding()
        //{
        //    var binding = new System.Windows.Data.Binding();
        //    binding.Source = ScrollViewer;
        //    binding.Path = new PropertyPath("VerticalOffset");
        //    binding.Mode = System.Windows.Data.BindingMode.OneWay;
        //    this.SetBinding(ScrollViewVerticalOffsetProperty, binding);
        //}

        //public static readonly DependencyProperty ScrollViewVerticalOffsetProperty =
        //DependencyProperty.Register(
        //                            "ScrollViewVerticalOffset",
        //                            typeof(double),
        //                            typeof(TextListView),
        //                            new PropertyMetadata(new PropertyChangedCallback(OnScrollViewVerticalOffsetChanged))
        //                            );

        //private static void OnScrollViewVerticalOffsetChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        //{
        //    System.Diagnostics.Debug.WriteLine(e.NewValue);
        //}

        //public double ScrollViewVerticalOffset
        //{
        //    get { 
        //        return (double)this.GetValue(ScrollViewVerticalOffsetProperty); }
        //    set { 
        //        this.SetValue(ScrollViewVerticalOffsetProperty, value); }
        //}

        //void Panel_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        //{
        //    //System.Diagnostics.Debug.WriteLine(Panel.VerticalOffset);
        //    var count = this.Items.Count;
        //    var count2 = this.Panel.Children.Count;
        //    var loc = count2 / 4;
        //    var cur = Panel.Children[Panel.Children.Count / 2] as ContentPresenter;

        //    System.Diagnostics.Debug.WriteLine("current item:" + (cur.Content as Page).ID);
        //}

        //protected override void PrepareContainerForItemOverride(DependencyObject element, object item)
        //{
        //    base.PrepareContainerForItemOverride(element, item);
        //    //var index = ItemsList.IndexOf(item.ToString());
        //    //System.Diagnostics.Debug.WriteLine("---------prepare : " + index + "-----------");
        //}

        //protected override void ClearContainerForItemOverride(DependencyObject element, object item)
        //{
        //    //var index = ItemsList.IndexOf(item.ToString());
        //    base.ClearContainerForItemOverride(element, item);
        //    //System.Diagnostics.Debug.WriteLine("---------clear : " + index + "-----------");
        //}

        //protected override DependencyObject GetContainerForItemOverride()
        //{
        //    System.Diagnostics.Debug.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().Name);
        //    return base.GetContainerForItemOverride();
        //}

        //protected override bool IsItemItsOwnContainerOverride(object item)
        //{
        //    //var index = ItemsList.IndexOf(item.ToString());
        //    //System.Diagnostics.Debug.WriteLine("---------search : " + index + "-----------");
        //    return base.IsItemItsOwnContainerOverride(item);
        //}

        private async void ItemsPanelTemplate_Panel_Loaded(object sender, RoutedEventArgs e)
        {
            if (Panel != null) return;
            Panel = (sender as VirtualizingStackPanel);

            await System.Threading.Tasks.Task.Delay(1);

            Panel.ScrollOwner.ScrollToVerticalOffset(App.Current.CurrentBook.ScrollProgress);
            this.Opacity = 1;

            ScrollBar = (ScrollBar)Panel.ScrollOwner.Descendants<ScrollBar>().Single((obj) => { return (obj as ScrollBar).Name == "VerticalScrollBar"; });
            ScrollBar.Scroll += ScrollBar_Scroll;
            ScrollBar.ValueChanged += ScrollBar_ValueChanged;
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

        void ScrollBar_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            System.Diagnostics.Debug.WriteLine(e.NewValue + "," + Panel.VerticalOffset + "," + Panel.ScrollOwner.ScrollableHeight);
            App.Current.CurrentBook.ScrollProgress = ScrollBar.Value;
            App.Current.CurrentBook.Position = (int)(ScrollBar.Value / ScrollBar.Maximum * App.Current.CurrentBook.Length);

            if (_delySaveTimer != null)
            {
                _delySaveTimer.Change(5000, System.Threading.Timeout.Infinite);
            }
            else
            {
                _delySaveTimer = new System.Threading.Timer((cb) => 
                {
                    System.Diagnostics.Debug.WriteLine("save progress");
                    DataBase.DataBase.Current.Update(App.Current.CurrentBook);
                }, null, 5000, System.Threading.Timeout.Infinite);
            }
        }
        private System.Threading.Timer _delySaveTimer;

        private void ScrollBar_Scroll(object sender, ScrollEventArgs e)
        {
            //if (e.ScrollEventType == ScrollEventType.EndScroll)
            {
                System.Diagnostics.Debug.WriteLine(e.ScrollEventType + ":" + e.NewValue);
            }
        }
    }
}
