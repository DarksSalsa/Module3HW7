using Logging.Services;

namespace Logging
{
    public static class Starter
    {
        public static async Task RunAsync()
        {
            for (int i = 0; i < 50; i++)
            {
                try
                {
                    switch (Random.Shared.Next(1, 4))
                    {
                        case 1:
                            await Actions.InfoMethod(i);
                            break;
                        case 2:
                            Actions.WarningMethod();
                            break;
                        case 3:
                            Actions.ErrorMethod();
                            break;
                        default:
                            break;
                    }
                }
                catch (BusinessException ex)
                {
                    string msg = $"Logger got this custom exception: {ex.Message}";
                    await Logger.GetInstance.Message(LogType.Warning, msg);
                }
                catch (Exception ex)
                {
                    string msg = $"Action failed by reason: {ex}";
                    await Logger.GetInstance.Message(LogType.Error, msg);
                }
            }
        }

        public static void StartBackupOperation()
        {
            Logger.GetInstance.GetManagement.CreateBackup();
        }
    }
}