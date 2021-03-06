﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iReader
{
    class Book : System.Collections.ObjectModel.ObservableCollection<Page>
    {
        public Book()
        {
            //this.Capacity = 5;
        }
    }

    class BookManager
    {
        FileCache _cache;
        Book _book;
        public Book Book
        {
            get
            {
                return _book;
            }
        }

        public BookManager(BookInfo book)
        {
            _book = new Book();
            _cache = new FileCache(book);
            _cache.Init(BookCacheInited);
        }

        void BookCacheInited()
        {
            var block = 64;
            var index = 0;
			var next = 0;
            while(index < _cache.Cache.Length)
            {
                var length = index + block > _cache.Cache.Length ? _cache.Cache.Length - index : block;
				next = FindNewLinePosition(_cache.Cache, index + length);
				if (next > 0)
				{
					var txt = _cache.Cache.ToString(index, next - index);
					_book.Add(new Page() { ID = _book.Count + 1, Content = txt });
					
					while (next < _cache.Cache.Length && (_cache.Cache[next] == '\r' || _cache.Cache[next] == '\n'))
					{
						next++;
					}
					index = next;
				}
				else
				{
					_book.Add(new Page() { ID = _book.Count + 1, Content = _cache.Cache.ToString(index, length) });
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
