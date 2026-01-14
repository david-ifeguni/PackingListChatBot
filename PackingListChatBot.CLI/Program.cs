using Microsoft.Extensions.DependencyInjection;
using PackingListChatBot.Services.Helpers;
using PackingListChatBot.Models;
using PackingListChatBot.Services.Packing;

var services = new ServiceCollection();
services.BuildDependencies();

var serviceProvider = services.BuildServiceProvider();

var packingListService = serviceProvider.GetRequiredService<IPackingListService>();

Console.WriteLine("Hello! I am your packing list chat bot, here to assist you with organizing your packing list for your upcoming vacation. Please tell me your destination, dates of travel and any activities you have planned, and I will design a packing list just for you.");
Console.WriteLine("Type your message below (or type 'exit' to exit program):");
Console.WriteLine();

var conversationId = Guid.NewGuid().ToString();

while(true)
{
    Console.Write("> ");
    var message = Console.ReadLine();

    if (string.IsNullOrEmpty(message) || message.Equals("exit", StringComparison.OrdinalIgnoreCase))
    {
        break;
    }

    var response = await packingListService.GeneratePackingListAsync(new PackingChatRequest
    {
        Message = message
    },
    conversationId);

    if (response.NeedsClarification)
    {
        Console.WriteLine();
        Console.WriteLine(response.ClarificationQuestion);
        Console.WriteLine();
    }
    else if (!string.IsNullOrEmpty(response.Error))
    {
        Console.WriteLine();
        Console.WriteLine(response.Error);
        Console.WriteLine();
    }
    else
    {
        Console.WriteLine();
        Console.WriteLine(response.FormattedPackingList);
        Console.WriteLine();
    }
    
}