using System.Text;
using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.ServiceBus;

//Project to send messages
namespace CoreSender
{
  public class Program
  {

    //The connection string can be found from the Azure portal
    //in the shared access policies
    const string ServiceBusConnectionString = "Endpoint=sb://gabservice.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=jcHIEa2cBfoO+KI+quH3BJJqNt3JIq58fnJBI2Tjkik=";
    const string QueueName = "TestQ";
    static IQueueClient queueClient;

    public static void Main(string[] args)
    {
      MainAsync().GetAwaiter().GetResult();

    }

    //Method that calls the send messages method
    static async Task MainAsync()
    {
      // maximum number of messages that will be sent
      const int numberOfMessages = 5;

      queueClient = new QueueClient(ServiceBusConnectionString, QueueName);

      Console.WriteLine("======================================================");
      Console.WriteLine("Press ENTER key to exit after sending all the messages.");
      Console.WriteLine("======================================================");

      // Send messages.
      await SendMessagesAsync(numberOfMessages);

      Console.ReadKey();

      await queueClient.CloseAsync();
    }

    //Does the work of sending the # of messages according to the
    //numberOfMessagesToSend, which in this case is 10
    static async Task SendMessagesAsync(int numberOfMessagesToSend)
    {
      try
      {
        for (var i = 0; i < numberOfMessagesToSend; i++)
        {
          // Create a new message to send to the queue.
          string messageBody = $"Message {i}";
          var message = new Message(Encoding.UTF8.GetBytes(messageBody));

          // Write the body of the message to the console.
          Console.WriteLine($"Sending message: {messageBody}");

          // Send the message to the queue.
          await queueClient.SendAsync(message);
        }
      }
      catch (Exception exception)
      {
        Console.WriteLine($"{DateTime.Now} :: Exception: {exception.Message}");
      }
    }


  }
}
