namespace Logging
{
    public class Actions
    {
        public static async Task InfoMethod(int i)
        {
            string msg = "Start method: InfoMethod";
            await Logger.GetInstance.Message(LogType.Info, msg.ToString());
        }

        public static void WarningMethod()
        {
            throw new BusinessException("Skipped logic in method");
        }

        public static void ErrorMethod()
        {
            throw new Exception("I broke the logic");
        }
    }
}
