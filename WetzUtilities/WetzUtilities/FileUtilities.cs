using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace WetzUtilities
{
    /// <summary>
    /// Helper methods for simple file access scenarios
    /// </summary>
    public static class FileUtilities
    {
        /// <summary>
        /// Creates directory if it doesn't already exist
        /// </summary>
        static public void SetupDirectory(string dirPath)
        {
            if (!Directory.Exists(dirPath))
            {
                Directory.CreateDirectory(dirPath);
            }
        }

        public static string LoadTextFile(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath))
            {
                throw new ArgumentException("File path missing");
            }

            if (!File.Exists(filePath))
            {
                return null;
            }

            string text = null;
            using (StreamReader sr = File.OpenText(filePath))
            {
                text = sr.ReadToEnd();
                sr.DiscardBufferedData();
            }
            return text;
        }

        public static async Task<string> LoadTextFileAsync(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath))
            {
                throw new ArgumentException("File path missing");
            }

            if (!File.Exists(filePath))
            {
                return null;
            }

            string text = null;
            using (StreamReader sr = File.OpenText(filePath))
            {
                text = await sr.ReadToEndAsync();
                sr.DiscardBufferedData();
            }
            return text;
        }

        public static void WriteTextFile(string dirPath, string fileName, string text)
        {
            if (string.IsNullOrWhiteSpace(dirPath))
            {
                throw new ArgumentException("Directory missing");
            }

            if (string.IsNullOrWhiteSpace(fileName))
            {
                throw new ArgumentException("File name missing");
            }

            if (!Directory.Exists(dirPath))
            {
                Directory.CreateDirectory(dirPath);
            }

            string filePath = Path.Combine(dirPath, fileName);
            using (StreamWriter sw = File.CreateText(filePath))
            {
                sw.Write(text);
                sw.Flush();
            }
        }

        public static async Task WriteTextFileAsync(string dirPath, string fileName, string text)
        {
            if (string.IsNullOrWhiteSpace(dirPath))
            {
                throw new ArgumentException("Directory missing");
            }

            if (string.IsNullOrWhiteSpace(fileName))
            {
                throw new ArgumentException("File name missing");
            }

            if (!Directory.Exists(dirPath))
            {
                Directory.CreateDirectory(dirPath);
            }

            string filePath = Path.Combine(dirPath, fileName);
            using (StreamWriter sw = File.CreateText(filePath))
            {
                await sw.WriteAsync(text);
                await sw.FlushAsync();
            }
        }

        /// <summary>
        /// Returns next iterative name for given file and path.
        /// For example, if f:\temp\sample.txt exists, it will return f:\temp\sample-1.txt
        /// </summary>
        /// <param name="dirPath">Target directory path (e.g. f:\temp)</param>
        /// <param name="fileName">File name, including extension (e.g. sample.txt)</param>
        /// <returns></returns>
        public static string GetNextName(string dirPath, string fileName)
        {
            var name = Path.GetFileNameWithoutExtension(fileName);
            var extension = Path.GetExtension(fileName);
            var filePath = Path.Combine(dirPath, name);
            while (File.Exists(filePath))
            {
                int index = name.LastIndexOf('-');
                if (index < 0)
                {
                    name += "-1";
                }
                else
                {
                    string val = name.Substring(index + 1);
                    if (!Int32.TryParse(val, out var i))
                    {
                        name += "-1";
                    }
                    else
                    {
                        i++;
                        name = name.Substring(0, index);
                        name += $"-{i}";
                    }
                }
                filePath = Path.Combine(dirPath, name + extension);
            }
            return filePath;
        }

        /// <summary>
        /// Move the given file to target folder, creating folder if necessary and renaming file if the name already exists in target location.
        /// </summary>
        public static void SafeMoveFile(string existingFilePath, string targetFolderPath)
        {
            if (existingFilePath.IsEmpty() || targetFolderPath.IsEmpty())
            {
                return;
            }
            if (!File.Exists(existingFilePath))
            {
                return;
            }
            var name = Path.GetFileName(existingFilePath);
            if (!Directory.Exists(targetFolderPath))
            {
                Directory.CreateDirectory(targetFolderPath);
            }
            var filePath = GetNextName(targetFolderPath, name);
            File.Move(existingFilePath, filePath);
        }

        /// <summary>
        /// Move the given file to target folder, creating folder if necessary and renaming file if the name already exists in target location.
        /// </summary>
        public static void SafeMoveFile(FileInfo f, string targetFolderPath)
        {
            if (f == null || targetFolderPath.IsEmpty())
            {
                return;
            }
            if (!f.Exists)
            {
                return;
            }
            if (!Directory.Exists(targetFolderPath))
            {
                Directory.CreateDirectory(targetFolderPath);
            }
            var filePath = GetNextName(targetFolderPath, f.Name);
            File.Move(f.FullName, filePath);
        }

        /// <summary>
        /// Copy the given file to target folder, creating folder if necessary and renaming file if the name already exists in target location.
        /// </summary>
        public static void SafeCopyFile(string existingFilePath, string targetFolderPath)
        {
            if (existingFilePath.IsEmpty() || targetFolderPath.IsEmpty())
            {
                return;
            }
            if (!File.Exists(existingFilePath))
            {
                return;
            }
            var name = Path.GetFileName(existingFilePath);
            if (!Directory.Exists(targetFolderPath))
            {
                Directory.CreateDirectory(targetFolderPath);
            }
            var filePath = GetNextName(targetFolderPath, name);
            File.Copy(existingFilePath, filePath);
        }

        /// <summary>
        /// Copy the given file to target folder, creating folder if necessary and renaming file if the name already exists in target location.
        /// </summary>
        public static void SafeCopyFile(FileInfo f, string targetFolderPath)
        {
            if (f == null || targetFolderPath.IsEmpty())
            {
                return;
            }
            if (!f.Exists)
            {
                return;
            }
            if (!Directory.Exists(targetFolderPath))
            {
                Directory.CreateDirectory(targetFolderPath);
            }
            var filePath = GetNextName(targetFolderPath, f.Name);
            File.Copy(f.FullName, filePath);
        }

        /// <summary>
        /// Rename the given file, iterating to avoid overwriting existing files with preferred name.
        /// </summary>
        public static void SafeRenameFile(string existingFilePath, string targetFileName)
        {
            if (existingFilePath.IsEmpty() || targetFileName.IsEmpty())
            {
                return;
            }
            if (!File.Exists(existingFilePath))
            {
                return;
            }
            var dirPath = Path.GetDirectoryName(existingFilePath);
            var filePath = GetNextName(dirPath, targetFileName);
            File.Move(existingFilePath, filePath);
        }

        /// <summary>
        /// Rename the given file, iterating to avoid overwriting existing files with preferred name.
        /// </summary>
        public static void SafeRenameFile(FileInfo f, string targetFileName)
        {
            if (targetFileName.IsEmpty() || f == null || !f.Exists)
            {
                return;
            }
            var filePath = GetNextName(f.DirectoryName, targetFileName);
            File.Move(f.FullName, filePath);
        }
    }
}