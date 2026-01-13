using PackingListChatBot.Packing;
using PackingListChatBot.SemanticKernel.KernelFactory;
using PackingListChatBot.SemanticKernel.Prompts;
using PackingListChatBot.SemanticKernel.Tools;
using PackingListChatBot.Store;

namespace PackingListChatBot.Services.Helpers
{
    public static class DependencyInjectionHelper
    {
        public static IServiceCollection BuildDependencies(this IServiceCollection services)
        {
            services.AddSingleton(KernelFactory.BuildKernel());
            services.AddSingleton<ITravelContextStore, TravelContextStore>();

            services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(
                    new System.Text.Json.Serialization.JsonStringEnumConverter()
                );
            });

            services.AddScoped<IActivityRules, ActivityRules>();
            services.AddScoped<IClothingRules, ClothingRules>();
            services.AddScoped<PackingConstraintsAggregator>();
            services.AddHttpClient<IWeatherService, WeatherService>();

            services.AddScoped<IPromptProvider, FilePromptProvider>();
            services.AddScoped<IKernelInvoker, SemanticKernelInvoker>();
            services.AddScoped<IPackingTool, PackingTool>();

            services.AddScoped<IPackingListGenerator, PackingListGenerator>();
            services.AddScoped<IPackingListService, PackingListService>();

            return services;
        }
    }
}
