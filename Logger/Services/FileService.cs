// Use Filestream.WriteLineAsync without using
using System.IO;

namespace Logging.Services
{
    public class FileService
    {
        private readonly SemaphoreSlim _semaphore = new SemaphoreSlim(1);
        private readonly string _extension = "txt";
        private StreamWriter _writer;
        public void CountFiles(string path)
        {
            var fileList = Directory.EnumerateFiles(path).ToList();
            fileList.Sort();
            if (fileList.Count > 3)
            {
                File.Delete($"{fileList.Last()}");
            }
        }

        public void CreateFileName(string path)
        {
            CountFiles(path);
            var fileName = $"{path}{DateTime.Now:hh.mm.ss.ffff dd.MM.yyyy}.{_extension}";
            _writer = new StreamWriter(fileName);
        }

        public void CreateBackupFileName(string pathOne, string pathTwo)
        {
            var fileList = Directory.EnumerateFiles(pathOne).ToList();
            fileList.Sort();
            File.Copy($"{fileList.Last()}", $"{pathTwo}{DateTime.Now:hh.mm.ss.ffff dd.MM.yyyy}.{_extension}");
        }

        public async Task WriteToFile(string text)
        {
            await _semaphore.WaitAsync();
            await _writer.WriteLineAsync(text);
            await _writer.FlushAsync();
            _semaphore.Release();
        }
    }
}
