using Logging.Services.Interfaces;

namespace Logging.Services
{
    public class FolderService : IFolderService
    {
        private DirectoryInfo _directory;
        public void CheckOrCreateFolder(string path)
        {
            _directory = new DirectoryInfo(path);
            if (!_directory.Exists)
            {
                _directory.Create();
            }
        }
    }
}
