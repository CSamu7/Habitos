using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.JsonPatch.Operations;
using Newtonsoft.Json;
using System.Text.Json;

namespace Habits.Common
{
    public static class JsonPatchDocumentExtensions
    {
        public static JsonPatchDocument Sanitize(this JsonPatchDocument patchDoc, List<OperationType>? operationsAllowed = null)
        {
            operationsAllowed = operationsAllowed ?? [OperationType.Replace];

            patchDoc.Operations.RemoveAll(operation => !operationsAllowed.Contains(operation.OperationType));
            patchDoc.Operations.RemoveAll(operation => operation.path is null || operation.op is null);

            return patchDoc;
        }

        public static JsonPatchDocument? ToPatchDocument(this JsonElement jsonElement)
        {
            var json = jsonElement.GetRawText();
            var doc = JsonConvert.DeserializeObject<JsonPatchDocument>(json)?.Sanitize();

            return doc;
        }
    }
}
