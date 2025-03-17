using System.Net.Http.Json;
using Subscriber.Dtos;
using System.Net;

Console.WriteLine("Press ESC to stop");

// DO NOT use ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true; in production.
// In production, use a valid SSL certificate from a trusted CA.
var handler = new HttpClientHandler()
{
    ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
};

HttpClient client = new HttpClient(handler);

do
{
    Console.WriteLine("Listening...");
    while (!Console.KeyAvailable)
    {
        List<int> ackIds = await GetMessagesAsync(client);

        Thread.Sleep(2000);

        if (ackIds.Count > 0)
        {
            await AckMessagesAsync(client, ackIds);
        }
    }

} while (Console.ReadKey(true).Key != ConsoleKey.Escape);

static async Task<List<int>> GetMessagesAsync(HttpClient httpClient)
{
    List<int> ackIds = new List<int>();
    List<MessageReadDto>? newMessages = new List<MessageReadDto>();

    try
    {
        newMessages = await httpClient.GetFromJsonAsync<List<MessageReadDto>>("https://localhost:5001/api/subscriptions/2/messages");
    }
    catch (Exception ex)
    {
        return ackIds;
    }

    foreach (MessageReadDto msg in newMessages!)
    {
        Console.WriteLine($"{msg.Id} - {msg.TopicMessage} - {msg.MessageStatus}");
        ackIds.Add(msg.Id);
    }

    return ackIds;
}

static async Task AckMessagesAsync(HttpClient httpClient, List<int> ackIds)
{
    var response = await httpClient.PostAsJsonAsync("https://localhost:5001/api/subscriptions/2/messages", ackIds);
    var returnMessage = await response.Content.ReadAsStringAsync();

    Console.WriteLine(returnMessage);
}