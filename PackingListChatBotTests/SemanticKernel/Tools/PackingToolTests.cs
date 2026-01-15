using PackingListChatBotTests.SemanticKernel.Mocks;


namespace PackingListChatBot.SemanticKernel.Tools.Tests
{
    [TestClass()]
    public class PackingToolTests
    {
        [TestMethod()]
        public async Task ReturnsValidPackingIntent_WhenJsonIsCorrect()
        {
            var json = """
            {
                "location": "Chicago, IL",
                "startTime": "2026-03-15",
                "endTime": "2026-03-30",
                "activities": ["Hiking", "WineTasting"],
                "unknownActivities": []
            }
            """;

            var tool = new PackingTool(new MockKernelInvoker(json), new MockPromptProvider());

            var result = await tool.GetPackingIntentAsync("Trip to Chicago");

            Assert.AreEqual("Chicago, IL", result.Location);
            Assert.IsTrue(result.Activities?.Count == 2);
            Assert.AreEqual(DateOnly.Parse("2026-03-15"), result.StartTime.Value);
            Assert.AreEqual(DateOnly.Parse("2026-03-30"), result.EndTime.Value);
        }

        [TestMethod()]
        public async Task RequestsClarification_WhenDatesMissing()
        {
            var json = """
            {
                "location": "Chicago",
                "activities": ["Hiking"]
            }
            """;

            var tool = new PackingTool(new MockKernelInvoker(json), new MockPromptProvider());

            var result = await tool.GetPackingIntentAsync("Going hiking in Chicago");

            Assert.AreEqual("Chicago", result.Location);
            Assert.IsTrue(result.Activities?.Count == 1);
            Assert.IsFalse(result.StartTime.HasValue);
            Assert.IsFalse(result.EndTime.HasValue);
        }

        [TestMethod()]
        public async Task UnknownActivitiesReduceConfidence()
        {
            var json = """
            {
                "location": "Denver",
                "startTime": "2026-05-01",
                "endTime": "2026-05-10",
                "activities": ["Hiking"],
                "unknownActivities": ["Paragliding"]
            }
            """;

            var tool = new PackingTool(new MockKernelInvoker(json), new MockPromptProvider());

            var result = await tool.GetPackingIntentAsync("Trip to Denver");

            Assert.IsNotNull(result.UnknownActivities);
        }
    }
}