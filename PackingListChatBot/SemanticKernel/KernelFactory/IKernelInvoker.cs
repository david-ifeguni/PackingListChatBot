namespace PackingListChatBot.SemanticKernel.KernelFactory

{
    public interface IKernelInvoker
    {
        Task<string> InvokeAsync(string prompt, Dictionary<string, object?> parameters);
    }
}
