using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PackingListChatBot.SemanticKernel;
using PackingListChatBot.SemanticKernel.KernelFactory;

namespace PackingListChatBotTests.SemanticKernel.Mocks
{
    public class MockKernelInvoker : IKernelInvoker
    {
        private readonly string response;

        public MockKernelInvoker(string response)
        {
            this.response = response;
        }

        public Task<string> InvokeAsync(string prompt, Dictionary<string, object?> variables)
        {
            return Task.FromResult(response);
        }
    }
}
