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
                "unknownActivities": [],
                "needsClarification": false
            }
            """;

            var tool = new PackingTool(new MockKernelInvoker(json), new MockPromptProvider());

            var result = await tool.GetPackingIntentAsync("Trip to Chicago");

            Assert.IsFalse(result.NeedsClarification);
            Assert.AreEqual("Chicago, IL", result.Location);
            Assert.IsTrue(result.ConfidenceScore >= 0.6);
        }

        [TestMethod()]
        public async Task RequestsClarification_WhenDatesMissing()
        {
            var json = """
            {
                "location": "Chicago",
                "activities": ["Hiking"],
                "needsClarification": true
            }
            """;

            var tool = new PackingTool(new MockKernelInvoker(json), new MockPromptProvider());

            var result = await tool.GetPackingIntentAsync("Going hiking in Chicago");

            Assert.IsTrue(result.NeedsClarification);
            Assert.IsTrue(result.ConfidenceScore < 0.6);
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
                "unknownActivities": ["Paragliding"],
                "needsClarification": false
            }
            """;

            var tool = new PackingTool(new MockKernelInvoker(json), new MockPromptProvider());

            var result = await tool.GetPackingIntentAsync("Trip to Denver");

            Assert.IsNotNull(result.UnknownActivities);
            Assert.IsTrue(result.UnknownActivities.Contains("Paragliding"));
            Assert.IsTrue(result.ConfidenceScore < 1.0);
        }
    }
}