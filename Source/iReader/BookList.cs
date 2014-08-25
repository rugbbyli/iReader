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
            while(index < _cache.Cache.Length)
            {
                var length = index + block > _cache.Cache.Length ? _cache.Cache.Length - index : block;
                _list.Add(_cache.Cache.ToString(index, length));
                index += length;
            }
        }
    }
}
