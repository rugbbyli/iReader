using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Streams;

namespace iReader
{
    class FileCache
    {
        IStorageFile _file;
        StringBuilder _cache;
        public StringBuilder Cache
        {
            get
            {
                return _cache;
            }
        }
        ulong _position = 0;
        uint _cacheSize = 1024 * 1024 * 4;
		BookInfo _bookInfo;

        public FileCache(BookInfo book)
        {
			_bookInfo = book;
        }

        public async void Init(Action Callback)
        {
            _file = await StorageFile.GetFileFromPathAsync(_bookInfo.Location);

            if (_file != null)
            {
                _position = 0;
                _cache = new StringBuilder((int)_cacheSize);
                await RefreshCache();
            }
            Callback();
        }

        async Task RefreshCache()
        {
            _cache.Clear();
            using (var stream = await _file.OpenReadAsync())
            {
                var input = stream.GetInputStreamAt(_position);
                using (var reader = new DataReader(input))
                {
                    //reader.UnicodeEncoding = Windows.Storage.Streams.UnicodeEncoding.Utf8;
                    //reader.ByteOrder = ByteOrder.BigEndian;
                    var ret = await reader.LoadAsync(_cacheSize);
                    var buf = new byte[ret];
                    reader.ReadBytes(buf);
                    var content = Encoding.UTF8.GetString(buf, 0, buf.Length);
                    _cache.Append(content);
                    _position = stream.Position;
                }
            }
        }
    }
}
