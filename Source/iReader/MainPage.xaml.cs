using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using iReader.Resources;
using LinqToVisualTree;

namespace iReader
{
    public partial class MainPage : PhoneApplicationPage
    {
        BookManager _manager;

        // 构造函数
        public MainPage()
        {
            InitializeComponent();
            var bar = GetTemplateChild("VerticalScrollBar");
            _manager = new BookManager(App.Current.CurrentBook);

			//ListBox.ListBox.ItemsSource = _manager.List;

			ListBox.ItemsSource = _manager.Book;

			ListBox.Inited += ListBox_Loaded;

			LayoutRoot.AddHandler(Grid.TapEvent, new EventHandler<System.Windows.Input.GestureEventArgs>(Page_Taped), true);
            Menu.Visibility = System.Windows.Visibility.Collapsed;
        }

		void ListBox_Loaded(object sender, EventArgs e)
		{
			//var elements = ListBox.Descendants().ToList();

            //ListBox.ScrollBar.Value = App.Current.CurrentBook.ScrollProgress;
			//ListBox.Panel.PageDown();
		}

		void Page_Taped(object sender, System.Windows.Input.GestureEventArgs e)
		{
            if (Menu.Visibility == System.Windows.Visibility.Visible)
            {
                Menu.Visibility = System.Windows.Visibility.Collapsed;
                return;
            }
			var pos = e.GetPosition(this);

            var col = (int)(pos.X * 3 / this.ActualWidth);
            var row = (int)(pos.Y * 3 / this.ActualHeight);
			
			if(col == 2 || row == 2)
			{
				//ListBox.Panel.LineUp(); 
				//ListBox.Panel.PageDown();
                //ListBox.Panel.SetVerticalOffset(ListBox.Panel.VerticalOffset + 1.62);
			}
			else if (col == 0 || row == 0)
			{
				//ListBox.Panel.LineDown();
				//ListBox.Panel.PageUp();
                //ListBox.Panel.SetVerticalOffset(ListBox.Panel.VerticalOffset - 1.62);
                
			}
            else
            {
                Menu.Visibility = System.Windows.Visibility.Visible;
            }
		}

        // 用于生成本地化 ApplicationBar 的示例代码
        //private void BuildLocalizedApplicationBar()
        //{
        //    // 将页面的 ApplicationBar 设置为 ApplicationBar 的新实例。
        //    ApplicationBar = new ApplicationBar();

        //    // 创建新按钮并将文本值设置为 AppResources 中的本地化字符串。
        //    ApplicationBarIconButton appBarButton = new ApplicationBarIconButton(new Uri("/Assets/AppBar/appbar.add.rest.png", UriKind.Relative));
        //    appBarButton.Text = AppResources.AppBarButtonText;
        //    ApplicationBar.Buttons.Add(appBarButton);

        //    // 使用 AppResources 中的本地化字符串创建新菜单项。
        //    ApplicationBarMenuItem appBarMenuItem = new ApplicationBarMenuItem(AppResources.AppBarMenuItemText);
        //    ApplicationBar.MenuItems.Add(appBarMenuItem);
        //}
    }
}