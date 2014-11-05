using System;
using System.Collections.Generic;
using System.IO;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iReader
{
	/// <summary>
	/// 封装了常用的文件操作
	/// </summary>
	class StorageFileHelper
	{
		static IsolatedStorageFile _store = IsolatedStorageFile.GetUserStoreForApplication();

		public static bool FileExists(string fileName)
		{
			return _store.FileExists(fileName);
		}

		public static bool FolderExists(string folderName)
		{
			return _store.DirectoryExists(folderName);
		}

		public static Stream CreateFile(string fileName)
		{
			return _store.CreateFile(fileName);
		}

		public static Stream OpenFile(string fileName, FileMode mode = FileMode.Open, FileAccess access = FileAccess.ReadWrite)
		{
			return _store.OpenFile(fileName, mode, access);
		}

		public static void DeleteFile(string fileName)
		{
			_store.DeleteFile(fileName);
		}

		public static void CreateFolder(string path)
		{
			_store.CreateDirectory(path);
		}

		public static string[] OpenFolder(string path)
		{
			return _store.GetFileNames(path);
		}
	}
}
