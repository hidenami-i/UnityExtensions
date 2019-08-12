using System;
using System.IO;
using System.Text;
using System.Runtime.Serialization.Formatters.Binary;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

namespace UnityExtensions
{
	/// <summary>
	/// IO Extensions
	/// ref https://docs.microsoft.com/ja-jp/dotnet/api/system.io?view=netframework-4.7.2
	/// </summary>
	public static class ExIO
	{
		static readonly byte[] EmptyBytes = new byte[0];

		/// <summary>
		/// Creates the directory not exsits.
		/// </summary>
		/// <param name="folderPath">Folder path.</param>
		public static void CreateDirectoryNotExist(string folderPath) {
			if (!Directory.Exists(folderPath)) {
				Directory.CreateDirectory(folderPath);
			}
		}

		/// <summary>
		/// Writes all text.
		/// </summary>
		/// <param name="filePath"></param>
		/// <param name="content"></param>
		public static void WriteAllText(string filePath, string content) {
			CreateDirectoryNotExist(Path.GetDirectoryName(filePath));
			File.WriteAllText(filePath, content, Encoding.UTF8);
		}

		/// <summary> 
		/// Writes all text.
		/// File Path is create from the folder path and file name.
		/// </summary>
		/// <param name="folderPath">Folder path.</param>
		/// <param name="fileName">File name.</param>
		/// <param name="content">Content.</param>
		/// <param name="encoding">Encoding.</param>
		public static void WriteAllText(string folderPath, string fileName, string content) {
			CreateDirectoryNotExist(folderPath);
			string filePath = Path.Combine(folderPath, fileName);
			File.WriteAllText(filePath, content, Encoding.UTF8);
		}

		/// <summary>
		/// Writes all bytes.
		/// </summary>
		/// <param name="filePath"></param>
		/// <param name="bytes"></param>
		public static void WriteAllBytes(string filePath, byte[] bytes) {
			CreateDirectoryNotExist(Path.GetDirectoryName(filePath));
			File.WriteAllBytes(filePath, bytes);
		}

		/// <summary>
		/// Writes all bytes.
		/// </summary>
		/// <param name="folderPath">Folder path.</param>
		/// <param name="fileName">File name.</param>
		/// <param name="bytes">Bytes.</param>
		public static void WriteAllBytes(string folderPath, string fileName, byte[] bytes) {
			CreateDirectoryNotExist(folderPath);
			string filePath = Path.Combine(folderPath, fileName);
			File.WriteAllBytes(filePath, bytes);
		}

		/// <summary>
		/// Reads all text as utf8.
		/// File Path is create from the folder path and file name.
		/// </summary>
		/// <param name="folderPath"></param>
		/// <param name="fileName"></param>
		/// <returns></returns>
		public static string ReadAllText(string folderPath, string fileName) {
			return ReadAllText(Path.Combine(folderPath, fileName));
		}

		/// <summary>
		/// Reads all text as utf8.
		/// </summary>
		/// <param name="filePath"></param>
		public static string ReadAllText(string filePath) {
			try {
				return File.ReadAllText(filePath, Encoding.UTF8);
			}
			catch (DirectoryNotFoundException e) {
				ExDebug.LogWarning($"FilePath:{filePath}\nDirectoryNotFoundException:{e.StackTrace}");
				return "";
			}
			catch (FileNotFoundException e) {
				ExDebug.LogWarning($"FilePath:{filePath}\nFileNotFoundException:{e.StackTrace}");
				return "";
			}
			catch (Exception e) {
				ExDebug.LogError($"FilePath:{filePath}\nException:{e.StackTrace}");
				throw;
			}
		}

		/// <summary>
		/// Reads all bytes.
		/// File Path is create from the folder path and file name.
		/// </summary>
		/// <param name="folderPath"></param>
		/// <param name="fileName"></param>
		/// <returns></returns>
		public static byte[] ReadAllBytes(string folderPath, string fileName) {
			return ReadAllBytes(Path.Combine(folderPath, fileName));
		}

		/// <summary>
		/// Reads all bytes.
		/// </summary>
		/// <param name="filePath"></param>
		/// <returns></returns>
		public static byte[] ReadAllBytes(string filePath) {
			try {
				return File.ReadAllBytes(filePath);
			}
			catch (DirectoryNotFoundException e) {
				ExDebug.LogWarning($"FilePath:{filePath}\nDirectoryNotFoundException:{e.StackTrace}");
				return EmptyBytes;
			}
			catch (FileNotFoundException e) {
				ExDebug.LogWarning($"FilePath:{filePath}\nFileNotFoundException:{e.StackTrace}");
				return EmptyBytes;
			}
			catch (Exception e) {
				ExDebug.LogError($"FilePath:{filePath}\nException:{e.StackTrace}");
				throw;
			}
		}

		/// <summary>
		/// Birnaries the formatter serialize.
		/// </summary>
		/// <param name="folderPath">Folder path.</param>
		/// <param name="fileName">File name.</param>
		/// <param name="content">Content.</param>
		public static void BinaryFormatterSerialize(string folderPath, string fileName, string content) {
			CreateDirectoryNotExist(folderPath);
			BinaryFormatter binaryFormatter = new BinaryFormatter();
			FileStream fileStream =
				new FileStream(Path.Combine(folderPath, fileName), FileMode.Create, FileAccess.Write);
			binaryFormatter.Serialize(fileStream, content);
			fileStream.Close();
		}

		/// <summary>
		/// Binaries the formatter deserialize.
		/// </summary>
		/// <returns>The formatter deserialize.</returns>
		/// <param name="folderPath">Folder path.</param>
		/// <param name="fileName">File name.</param>
		public static string BinaryFormatterDeserialize(string folderPath, string fileName) {
			string filePath = Path.Combine(folderPath, fileName);
			BinaryFormatter binaryFormatter = new BinaryFormatter();
			FileStream fileStream = File.Open(filePath, FileMode.Open, FileAccess.Read);
			string saveData = (string)binaryFormatter.Deserialize(fileStream);
			fileStream.Close();
			return saveData;
		}

		/// <summary>
		/// Delete all the files in the directory.
		/// </summary>
		/// <param name="folderPath">Folder path.</param>
		/// <param name="containsValue">Contains value.</param>
		public static void DeleteAllFilesBy(string folderPath, string containsValue = "") {
			if (!Directory.Exists(folderPath)) {
				return;
			}

			foreach (string file in Directory.GetFiles(folderPath)) {

				if (containsValue == string.Empty) {
					File.Delete(file);
				}
				else {
					if (file.Contains(containsValue)) {
						File.Delete(file);
					}
				}
			}

			foreach (string subDir in Directory.GetDirectories(folderPath)) {
				DeleteAllFilesBy(subDir);
			}
		}

		/// <summary>
		/// Retrieve all the files in the directory.
		/// </summary>
		/// <returns>The all files.</returns>
		/// <param name="rootFolderPath">Folder path.</param>
		public static string[] GetAllFiles(string rootFolderPath) {
			if (!Directory.Exists(rootFolderPath)) {
				return new string[] { };
			}

			return Directory.GetFiles(rootFolderPath, "*", SearchOption.AllDirectories);
		}

		/// <summary>
		/// Retrive all the folders in the directory.
		/// </summary>
		/// <param name="rootFolderPath"></param>
		/// <returns></returns>
		public static string[] GetAllDirectories(string rootFolderPath) {
			if (!Directory.Exists(rootFolderPath)) {
				return new string[] { };
			}

			return Directory.GetDirectories(rootFolderPath, "*", SearchOption.AllDirectories);
		}

		/// <summary>
		/// Rename file name.
		/// </summary>
		/// <param name="filePath">File path.</param>
		/// <param name="newFileName">New file name.</param>
		public static void Rename(string filePath, string newFileName) {
			FileInfo fileInfo = new FileInfo(filePath);
			if (!fileInfo.Exists) {
				return;
			}

			string directoryName = Path.GetDirectoryName(filePath);
			string destFileName = Path.Combine(directoryName, newFileName);
			FileInfo newFileInfo = new FileInfo(destFileName);
			if (newFileInfo.Exists) {
				return;
			}

			fileInfo.MoveTo(destFileName);
		}

		/// <summary>
		/// Copy file.
		/// </summary>
		/// <param name="filePath"></param>
		/// <param name="newFileName"></param>
		public static void Copy(string filePath, string newFileName) {
			FileInfo fileInfo = new FileInfo(filePath);
			if (!fileInfo.Exists) {
				return;
			}

			string directoryName = Path.GetDirectoryName(filePath);
			string destFileName = Path.Combine(directoryName, newFileName);
			FileInfo newFileInfo = new FileInfo(destFileName);
			if (newFileInfo.Exists) {
				return;
			}

			fileInfo.CopyTo(destFileName);
		}

		/// <summary>
		/// Safe file delete.
		/// </summary>
		/// <param name="filePath"></param>
		public static void DeleteFile(string filePath) {
			if (!File.Exists(filePath)) {
				ExDebug.LogError($"Not exists {filePath}");
				return;
			}

			File.Delete(filePath);
		}

		/// <summary>
		/// Safe directory delete.
		/// </summary>
		/// <param name="directoryPath"></param>
		public static void DeleteDirectory(string directoryPath) {
			if (!Directory.Exists(directoryPath)) {
				ExDebug.LogError($"Not exists {directoryPath}");
				return;
			}

			Directory.Delete(directoryPath);
		}

		/// <summary>
		/// Delete all files and directories.
		/// </summary>
		/// <param name="rootDirectoryPath">Folder path.</param>
		public static void DeleteAllFilesAndDirectories(string rootDirectoryPath) {
			string[] filePaths = GetAllFiles(rootDirectoryPath);

			if (filePaths.Length <= 0) {
				return;
			}

			foreach (string path in filePaths) {
				DeleteFile(path);
			}

			IEnumerable<string> direcToryPaths = GetAllDirectories(rootDirectoryPath).Reverse();
			foreach (string path in direcToryPaths) {
				DeleteDirectory(path);
			}
		}

		/// <summary>
		/// Move the file.
		/// </summary>
		/// <param name="filePath"></param>
		/// <param name="newFilePath"></param>
		public static void MoveTo(string filePath, string newFilePath) {
			if (!File.Exists(filePath)) {
				return;
			}

			CreateDirectoryNotExist(Path.GetDirectoryName(newFilePath));
			File.Move(filePath, newFilePath);
			DeleteFile(filePath);
		}

		public static void DirectoryCopy(string sourcePath, string destinationPath) {
			Directory.CreateDirectory(destinationPath);

			//Now Create all of the directories
			foreach (string dirPath in Directory.GetDirectories(sourcePath, "*", SearchOption.AllDirectories))
				Directory.CreateDirectory(dirPath.Replace(sourcePath, destinationPath));

			//Copy all the files & Replaces any files with the same name
			foreach (string newPath in Directory.GetFiles(sourcePath, "*.*", SearchOption.AllDirectories))
				File.Copy(newPath, newPath.Replace(sourcePath, destinationPath), true);
		}

		public static string SanitizeFileName(string name) {
			foreach (char c in Path.GetInvalidFileNameChars())
				name = name.Replace(c, '_');

			// Remove additional special characters that Unity doesn't like
			foreach (char c in "/:?<>*|\\~")
				name = name.Replace(c, '_');
			return name.Trim();
		}

		public static long DirectorySizeInBytes(string path) {
			var files = Directory.GetFiles(path, "*.*", SearchOption.AllDirectories);
			long sizeInBytes = 0;
			foreach (var file in files) {
				FileInfo info = new FileInfo(file);
				sizeInBytes += info.Length;
			}

			return sizeInBytes;
		}

		public static void RemovePathAndMeta(string path, bool removeEmptyParent = false) {
			if (Directory.Exists(path))
				Directory.Delete(path, true);
			if (File.Exists(path + ".meta"))
				File.Delete(path + ".meta");
			if (removeEmptyParent) {
				var parent = Directory.GetParent(path);
				if (parent.GetDirectories().Length == 0 && parent.GetFiles().Length == 0)
					RemovePathAndMeta(parent.ToString(), removeEmptyParent);
			}
		}

		public static bool IsFolder(string path) {
			try {
				return File.GetAttributes(path).HasFlag(FileAttributes.Directory);
			}
			catch (Exception e) {

				if (e is FileNotFoundException) {
					return false;
				}

				throw;
			}
		}
	}
}