using Habits.Common;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.JsonPatch.Operations;
using Newtonsoft.Json;

namespace Testing.UnitTesting.API
{
    public class JsonPatchDocumentExtensionsTests
    {
        [Fact]
        public void Remove_null_paths_and_null_operations()
        {
            JsonPatchDocument document = CreateInvalidDocument();

            document.Sanitize();

            Assert.All(document.Operations, op => Assert.True(op.OperationType == OperationType.Replace));
            Assert.All(document.Operations, op => Assert.True(op.path is not null));
        }

        private JsonPatchDocument CreateInvalidDocument()
        {
            string rawJson = "[\n  {\n    \"op\": \"replace\",\n    \"path\": \"/IdTask\",\n    \"value\": \"Grokking Algorithms\"\n  },\n  {\n    \"op\": \"replace\"\n  }\n]";

            var document = JsonConvert.DeserializeObject<JsonPatchDocument>(rawJson);

            return document!;
        }
    }
}
