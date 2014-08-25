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
        uint _cacheSize = 1024 * 1024;
        string _filePath;

        public FileCache(string path)
        {
            _filePath = path;
        }

        public async void Init(Action Callback)
        {
            _file = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Assets/book.txt"));

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

        public async Task<IStorageFile> PickFile()
        {
            Windows.Storage.Pickers.FileOpenPicker picker = new Windows.Storage.Pickers.FileOpenPicker();
            picker.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.DocumentsLibrary;
            picker.FileTypeFilter.Add(".txt");
            return await picker.PickSingleFileAsync();
        }
    }
}
