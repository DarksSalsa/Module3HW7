using Logging.FileConfig;
using Logging.Services;

namespace Logging
{
    public class Logger
    {
        private static readonly Lazy<Logger> _instance = new Lazy<Logger>(() => new Logger());
        private readonly ExternalManagementService _management = new ExternalManagementService();
        private readonly FilePath _configuration = ConfigDeserializationService.FilePathSerialization().PathToFile;
        private readonly object _lock = new object();
        private int _counter = 0;

        private Logger()
        {
        }

        public event Action BackupHandler;
        public static Logger GetInstance
        {
            get
            {
                return _instance.Value;
            }
        }

        public ExternalManagementService GetManagement
        {
            get
            {
                return _management;
            }
        }

        public async Task Message(LogType type, string details)
        {
            string constructMessage = $"{DateTime.Now:T}: {type}: {details}";
            Console.WriteLine(constructMessage);
            _counter++;
            lock (_lock)
            {
                AskForBackup();
            }

            await _management.WriteReport(constructMessage);
        }

        public void AskForBackup()
        {
            if (_counter == _configuration.BackupInterval)
            {
                _counter = 0;
                BackupHandler.Invoke();
            }
        }
    }
}