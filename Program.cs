// See https://aka.ms/new-console-template for more information

public class Program
{
    public static async Task Main(string[] args)
    {
        /*
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
        */

        /*
         Asynchronous stream got introduced in C#8.0, which allow you to consume asynchronous
            sequences using the IAsyncEnumerable<T> and await foreach

        await ConsumeAsync();
         */

        /*
         Parallel Asynchronous operations
        You can run multiple asynchronous operations in parallel using Task.WhenAll or Task.WhenAny
         
        await RunMultipleTasksAsync();
         */

        /*
         LINQ with async await
         */

        
        var urls = new List<string> {
            "https://jsonplaceholder.typicode.com/posts/1",
            "https://jsonplaceholder.typicode.com/posts/20",
            "https://jsonplaceholder.typicode.com/posts/30"
        };

        //Performs async LINQ Query
        var dataTasks = urls.Select(async url =>
        {
            var data = await DataFetcherAsync.FetchDataAsync(url);
            return new { Url = url, Data = data };
        });

        var results = await Task.WhenAll(dataTasks);

        //Process the result
        foreach(var result in results)
        {
            Console.WriteLine($"Fetching data from: {result.Url} ");
            Console.WriteLine($"Fetched Data: {result.Data.Substring(0, 100)}\n");
        }

        Console.ReadKey();
    }
    public async static Task RunMultipleTasksAsync()
    {
        var task1 = ConsumeAsync();
        var task3 = Task.Delay(2000);
        var task4 = Task.Delay(4000);

        await Task.WhenAll(task1, task3, task4);
        Console.WriteLine("All the tasks are completed.");
    }

    public async static IAsyncEnumerable<int> GetNumbersAsync()
    {
        for(int i=1; i<=5; i++)
        {
            await Task.Delay(1000);
            yield return i;
        }
    }

    public async static Task ConsumeAsync()
    {
        await foreach(var number in GetNumbersAsync())
        {
            Console.WriteLine(number);
        }
    }

    public async static Task<int> PerformAsyncTask()
    {
        Console.WriteLine("Task started...");
        await Task.Delay(2000); // Simulate delay for 2 seconds
        Console.WriteLine("Task Finished.");
        return 42;
    }
}

public class DataFetcherAsync
{
    private static readonly HttpClient httpClient = new HttpClient();

    public async static Task<string> FetchDataAsync(string url)
    {
        await Task.Delay(1000);
        return await httpClient.GetStringAsync(url);
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

    // Defer instantiation until the instance is requested using Lazy<T>
    private static readonly Lazy<SingletonLogger> instance =
         new Lazy<SingletonLogger>(() => new SingletonLogger());
    

    //private static readonly SingletonLogger instance = new SingletonLogger();

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
            return instance.Value;
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


