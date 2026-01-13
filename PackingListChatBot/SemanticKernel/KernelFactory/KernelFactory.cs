using Microsoft.SemanticKernel;

namespace PackingListChatBot.SemanticKernel.KernelFactory
{
    public class KernelFactory
    {
        public static Kernel BuildKernel()
        {
            var kernelBuilder = Kernel.CreateBuilder();

            string deploymentName = Environment.GetEnvironmentVariable("AZURE_OPENAI_DEPLOYMENT_NAME");
            string apiKey = Environment.GetEnvironmentVariable("AZURE_OPENAI_API_KEY");
            string endpoint = Environment.GetEnvironmentVariable("AZURE_OPENAI_ENDPOINT");

            if (string.IsNullOrEmpty(deploymentName) || string.IsNullOrEmpty(apiKey) || string.IsNullOrEmpty(endpoint))
            {
                throw new Exception("Azure OpenAI credentials for Semantic Kernel not found");
            }

            kernelBuilder.AddAzureOpenAIChatCompletion(
                deploymentName: deploymentName,
                apiKey: apiKey,
                endpoint: endpoint
                );

            var kernel = kernelBuilder.Build();

            return kernel;
        }
    }
}