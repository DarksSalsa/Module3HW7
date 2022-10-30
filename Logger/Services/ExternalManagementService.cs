using Logging.FileConfig;

namespace Logging.Services
{
    public class ExternalManagementService
    {
        private readonly FolderService _folderService = new FolderService();
        private readonly FileService _fileService = new FileService();
        private readonly FilePath _configuration = ConfigDeserializationService.FilePathSerialization().PathToFile;
        private bool _flag = false;

        public async Task WriteReport(string message)
        {
            if (_flag == false)
            {
                ConfigurateInitialSetup();
                _fileService.CreateFileName(_configuration.PathToReports);
            }

            await _fileService.WriteToFile(message);
        }

        public void CreateBackup()
        {
            if (_flag == false)
            {
                ConfigurateInitialSetup();
            }

            _fileService.CreateBackupFileName(_configuration.PathToReports, _configuration.PathToBackups);
        }

        private void ConfigurateInitialSetup()
        {
            Logger.GetInstance.BackupHandler += Starter.StartBackupOperation;
            _folderService.CheckOrCreateFolder(_configuration.PathToReports);
            _folderService.CheckOrCreateFolder(_configuration.PathToBackups);
            _flag = true;
        }
    }
}
