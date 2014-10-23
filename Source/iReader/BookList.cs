using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iReader
{
    class BookList : System.Collections.ObjectModel.ObservableCollection<string>
    {
        public BookList()
        {
            //this.Capacity = 5;
        }
    }

    class BookListManager
    {
        FileCache _cache;
        BookList _list;
        public BookList List
        {
            get
            {
                return _list;
            }
        }

        public BookListManager()
        {
            _list = new BookList();
            _cache = new FileCache("");
            _cache.Init(BookCacheInited);
        }

        void BookCacheInited()
        {
            System.Diagnostics.Debug.WriteLine("fuck ... " + _cache.Cache.Length);
            var block = 550;
            var index = 0;
			var next = 0;
            while(index < _cache.Cache.Length)
            {
                var length = index + block > _cache.Cache.Length ? _cache.Cache.Length - index : block;
				next = FindNewLinePosition(_cache.Cache, index + length);
				if (next > 0)
				{
					_list.Add(_cache.Cache.ToString(index, next - index));
					index = next;
				}
				else
				{
					_list.Add(_cache.Cache.ToString(index, length));
					index += length;
				}
            }
        }

		int FindNewLinePosition(StringBuilder text, int start = 0)
		{
			for (int i = start; i < text.Length; i++)
			{
				if (text[i] == '\n') return i + 1;
			}
			return -1;
		}
    }
}
