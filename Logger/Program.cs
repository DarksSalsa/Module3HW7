using Logging;

public class Program
{
    public static async Task Main(string[] args)
    {
        await Task.WhenAll(Starter.RunAsync(), Starter.RunAsync());
    }
}