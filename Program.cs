// See https://aka.ms/new-console-template for more information

public class Program
{
    public static async Task Main(string[] args)
    {
        try
        {
            Console.WriteLine("Starting async task...");
            int result = await PerformAsyncTask();
            Console.WriteLine($"Async task completed with results: {result}");
        }catch (Exception ex)
        {
            Console.WriteLine($"Exception caught: {ex.Message}");
        }

        //Usage of static class
        Logger.Log("This ia s log message from Logger Static class & Static Log method");

        //Usage of singleton class
        SingletonLogger singletonLogger = SingletonLogger.Instance;
        singletonLogger.LogMessage();
        singletonLogger.UpdateLogMessage("Updated text.");
        singletonLogger.LogMessage();

        Console.ReadKey();
    }

    public async static Task<int> PerformAsyncTask()
    {
        Console.WriteLine("Task started...");
        await Task.Delay(2000); // Simulate delay for 2 seconds
        Console.WriteLine("Task Finished.");
        return 42;
    }
}

public static class Logger
{
    public static void Log(string message)
    {
        Console.WriteLine(message);
    }
}

public class SingletonLogger
{
    public string Message { get; private set; }

    // Defer iinstantiation until the instance is requested using Lazy<T>
    /* private static readonly Lazy<SingletonLogger> instance =
         new Lazy<SingletonLogger>(() => new SingletonLogger());
    */

    private static readonly SingletonLogger instance = new SingletonLogger();

    // Private constructor to prevent initialization
    private SingletonLogger()
    {
        //Initialize LogMessage
        Message = "Default Log Message from SingletonClass";
    }

    // Public static property to provide global access
    public static SingletonLogger Instance
    {
        get
        {
            return instance;
        }
    }

    public void UpdateLogMessage(string newMessage)
    {
        Message = newMessage;
    }

    public void LogMessage()
    {
        Console.WriteLine(Message);
    }
}


