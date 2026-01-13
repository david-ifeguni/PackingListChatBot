using Microsoft.SemanticKernel;

namespace PackingListChatBot.SemanticKernel.KernelFactory
{
    public class SemanticKernelInvoker : IKernelInvoker
    {
        private readonly Kernel kernel;

        public SemanticKernelInvoker(Kernel kernel)
        {
            this.kernel = kernel;
        }

        public async Task<string> InvokeAsync(string prompt, Dictionary<string, object?> parameters)
        {
            var result = await kernel.InvokePromptAsync(prompt, new KernelArguments(parameters));
            return result.ToString();
        }
    }
}
