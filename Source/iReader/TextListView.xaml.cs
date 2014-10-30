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

namespace iReader
{
	public partial class TextListView : ListBox
	{
		public ScrollViewer ScrollViewer { get; private set; }

		public TextListView()
		{
			InitializeComponent();
			//ScrollViewer = (Content as Control).GetTemplateChild("ScrollViewer") as ScrollViewer;
		}
	}
}
