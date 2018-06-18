using System.IO;
using static System.Console;
using static System.ConsoleColor;

namespace FileObserver
{
	class Program
	{
		static void Main(string[] args)
		{

			// instantiate the object
			var fileSystemWatcher = new FileSystemWatcher();

			// Associate event handlers with the events
			fileSystemWatcher.Created += FileSystemWatcher_Created;
			fileSystemWatcher.Changed += FileSystemWatcher_Changed;
			fileSystemWatcher.Deleted += FileSystemWatcher_Deleted;
			fileSystemWatcher.Renamed += FileSystemWatcher_Renamed;
			fileSystemWatcher.IncludeSubdirectories = true;

			// tell the watcher where to look
			fileSystemWatcher.Path = @"C:\Users\JNungaray\Documents\Visual Studio 2017\Projects\ConsoleApp4\FileObserver\Test\";

			// You must add this line - this allows events to fire.
			fileSystemWatcher.EnableRaisingEvents = true;

			WriteLine("Listening...");
			WriteLine("(Press any key to exit.)");

			ReadLine();
		}

		private static void FileSystemWatcher_Renamed(object sender, RenamedEventArgs e)
		{
			ForegroundColor = Yellow;
			WriteLine($"A new file has been renamed from {e.OldName} to {e.Name} ({FileTypeChanged(e)})");
		}

		private static void FileSystemWatcher_Deleted(object sender, FileSystemEventArgs e)
		{
			ForegroundColor = Red;
			WriteLine($"A new file has been deleted - {e.Name} ({FileTypeChanged(e)})");
		}

		private static void FileSystemWatcher_Changed(object sender, FileSystemEventArgs e)
		{
			ForegroundColor = Green;

			WriteLine($"A new file has been changed - {e.Name} ({FileTypeChanged(e)})");
		}

		private static void FileSystemWatcher_Created(object sender, FileSystemEventArgs e)
		{
			ForegroundColor = Blue;
			WriteLine($"A new file has been created - {e.Name} ({FileTypeChanged(e)})");
		}

		private static FileType FileTypeChanged(FileSystemEventArgs e)
		{
			FileInfo file = new FileInfo(e.FullPath);
			if (file.Attributes == FileAttributes.Directory)
			{
				return FileType.Directory;
			}

			return FileType.File;
		}
		
	}

	public enum FileType
	{
		Directory,
		File
	}
}
