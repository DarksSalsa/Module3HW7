namespace Logging.Services
{
    public class FileService
    {
        private readonly SemaphoreSlim _semaphore = new SemaphoreSlim(1);
        private readonly string _extension = "txt";
        private readonly int _maximumFileCount = 3;
        private StreamWriter _writer;
        public void CountFiles(string path)
        {
            var fileList = Directory.EnumerateFiles(path).ToList();
            fileList.Sort();
            if (fileList.Count > _maximumFileCount)
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
            FileInfo file;
            var fileList = Directory.EnumerateFiles(pathOne).ToList();
            var fileNaming = $"{pathTwo}{DateTime.Now:hh.mm.ss.fffff dd.MM.yyyy}.{_extension}";
            fileList.Sort();
            if (fileList.Count < _maximumFileCount + 1)
            {
                file = new FileInfo(fileList.Last());
                file.CopyTo(fileNaming);
                return;
            }

            file = new FileInfo(fileList[_maximumFileCount]);
            file.CopyTo(fileNaming);
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
