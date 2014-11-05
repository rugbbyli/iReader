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

		public DataBase() : base(ConnectionString) { }

		public Table<BookInfo> Books;
	}
}
