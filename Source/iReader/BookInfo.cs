using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Linq.Mapping;

namespace iReader
{
	[Table(Name="Books")]
	public class BookInfo
	{
		[Column(IsPrimaryKey = true, IsDbGenerated = true, DbType = "INT NOT NULL Identity", CanBeNull = false, AutoSync = AutoSync.OnInsert)]
		public string ID { get; set; }

		[Column(CanBeNull=false)]
		public string Title { get; set; }

		[Column(CanBeNull = false)]
		public int Position { get; set; }

		[Column(CanBeNull = false)]
		public int Length { get; set; }

		[Column(CanBeNull = false)]
		public string Location { get; set; }

		[Column(CanBeNull = false)]
		public DateTime LastReadTime { get; set; }

		[Column]
		public double ScrollProgress { get; set; }

		public void CopyProperties(BookInfo other)
		{
			this.ID = other.ID;
			this.Title = other.Title;
			this.Position = other.Position;
			this.Length = other.Length;
			this.Location = other.Location;
			this.LastReadTime = other.LastReadTime;
			this.ScrollProgress = other.ScrollProgress;
		}
	}
}
