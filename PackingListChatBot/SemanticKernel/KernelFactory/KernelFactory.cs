using Microsoft.SemanticKernel;

namespace PackingListChatBot.SemanticKernel.KernelFactory
{
    public class KernelFactory
    {
        public static Kernel BuildKernel()
        {
            // var temp = Kernel.CreateBuilder().Build();
            var kernelBuilder = Kernel.CreateBuilder();

            kernelBuilder.AddAzureOpenAIChatCompletion(
                deploymentName: "packing-chat-bot-gpt-4o",
                apiKey: "8hEbYWrwIX22h1W57UpVVzCApx0xTbhYA7i9vugNLmi2N7MWPYJnJQQJ99CAACHYHv6XJ3w3AAAAACOGWp2b",
                endpoint: "https://david-mk9b8eh6-eastus2.cognitiveservices.azure.com/"
                );

            var kernel = kernelBuilder.Build();

            return kernel;
        }
    }
}