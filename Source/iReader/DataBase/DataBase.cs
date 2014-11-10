using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iReader.DataBase
{
	public class DataBase : DataContext
	{
		const string ConnectionString = "Data Source='isostore:/DB.sdf'";
        public static DataBase Current { get; private set; }

		public DataBase() : base(ConnectionString) 
        {
            Current = this;
        }

		public Table<BookInfo> Books;

		public void Update(BookInfo book)
		{
			var old = Books.Single(item => item.ID == book.ID);
			old.CopyProperties(book);
			this.SubmitChanges(ConflictMode.ContinueOnConflict);
		}

		public void Insert(BookInfo book)
		{
			Books.InsertOnSubmit(book);
			SubmitChanges();
		}

		public void Delete(BookInfo book)
		{
			Books.DeleteOnSubmit(book);
			SubmitChanges();
		}
	}
}
