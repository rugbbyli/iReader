using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iReader
{
	public class BookListViewModel : INotifyPropertyChanged 
	{
		public DataBase.DataBase DB;

		public BookListViewModel()
		{
			DB = new DataBase.DataBase();

			var items = from item in DB.Books select item;
			_booklist = new ObservableCollection<BookInfo>(items);
		}

		public void AddBook(BookInfo book)
		{
			DB.Insert(book);
			_booklist.Insert(0, book);
		}

		public void RemoveBook(BookInfo book)
		{
			DB.Delete(book);
			_booklist.Remove(book);
		}

		public void UpdateBook(BookInfo book)
		{
			DB.Update(book);
		}

		private ObservableCollection<BookInfo> _booklist;
		public ObservableCollection<BookInfo> BookList
		{
			get
			{
				return _booklist;
			}
			//set
			//{
			//	if (_booklist != value)
			//	{
			//		_booklist = value;
			//		NotifyPropertyChanged("BookList");
			//	}
			//}
		}

		public event PropertyChangedEventHandler PropertyChanged;
		private void NotifyPropertyChanged(string propertyName)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
}
