using Microsoft.SemanticKernel;

namespace PackingListChatBot.SemanticKernel.KernelFactory
{
    public class KernelFactory
    {
        public static Kernel BuildKernel()
        {
            var kernelBuilder = Kernel.CreateBuilder();

            kernelBuilder.AddAzureOpenAIChatCompletion(
                deploymentName: "AZURE_DEPLOYMENT_NAME",
                apiKey: "AZURE_API_KEY",
                endpoint: "AZURE_ENDPOINT"
                );

            var kernel = kernelBuilder.Build();

            return kernel;
        } 
    }
}
