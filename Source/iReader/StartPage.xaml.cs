using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Windows.Storage.Pickers;
using Windows.ApplicationModel.Activation;
using Windows.Storage;

namespace iReader
{
	public partial class StartPage : PhoneApplicationPage
	{
		BookListViewModel ViewModel;

		public StartPage()
		{
			InitializeComponent();
			ViewModel = Resources["ViewModel"] as BookListViewModel;
			PhoneApplicationService.Current.ContractActivated += Current_ContractActivated;
		}

		void Current_ContractActivated(object sender, Windows.ApplicationModel.Activation.IActivatedEventArgs e)
		{
			var filePickerContinuationArgs = e as FileOpenPickerContinuationEventArgs;
			if (filePickerContinuationArgs != null)
			{
				var files = filePickerContinuationArgs.Files;
				if (files != null)
				{
					AddFilesToBookList(files);
				}
			}
		}

		async void AddFilesToBookList(IReadOnlyList<StorageFile> files)
		{
			var folder = await ApplicationData.Current.LocalFolder.CreateFolderAsync("books", CreationCollisionOption.OpenIfExists);
			foreach (var file in files)
			{
				if (StorageFileHelper.FileExists(System.IO.Path.Combine("books", file.Name)))
				{
					continue;
				}
				var newFile = await file.CopyAsync(folder, file.Name, NameCollisionOption.ReplaceExisting);
				var size = (await newFile.GetBasicPropertiesAsync()).Size;
				var item = new BookInfo()
				{
					Title = newFile.DisplayName,
					Position = 0,
					Length = (int)size,
					Location = newFile.Path,
					LastReadTime = DateTime.Now,
				};
				ViewModel.AddBook(item);
			}
		}

		private void Button_SelectFile_Click(object sender, EventArgs e)
		{
			FileOpenPicker picker = new FileOpenPicker();
			picker.FileTypeFilter.Add(".txt");
			picker.ViewMode = PickerViewMode.Thumbnail;
			picker.SuggestedStartLocation = PickerLocationId.DocumentsLibrary;
			picker.PickSingleFileAndContinue();
			
		}

		protected override void OnNavigatedTo(NavigationEventArgs e)
		{
			base.OnNavigatedTo(e);


		}

		private void ListBox_BookList_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			var item = ListBox_BookList.SelectedItem as BookInfo;

			if (item != null)
			{
				item.LastReadTime = DateTime.Now;

				App.Current.CurrentBook = item;
				NavigationService.Navigate(new Uri("/iReader;component/MainPage.xaml", System.UriKind.Relative));
			}
		}
	}
}