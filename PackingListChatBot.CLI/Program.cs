// See https://aka.ms/new-console-template for more information

using Microsoft.Extensions.DependencyInjection;
using PackingListChatBot.Services.Helpers;
using PackingListChatBot.Models;
using PackingListChatBot.Services.Packing;

var services = new ServiceCollection();
services.BuildDependencies();

var serviceProvider = services.BuildServiceProvider();

var packingListService = serviceProvider.GetRequiredService<IPackingListService>();

Console.WriteLine("PackingListChatBot CLI Entry Point.");
Console.WriteLine("Type your travel message (or 'exit'):");

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
        // break;
    }
    
}