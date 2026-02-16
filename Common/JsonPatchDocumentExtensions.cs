using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.JsonPatch.Operations;

namespace Habits.Common
{
    public static class JsonPatchDocumentExtensions
    {
        public static JsonPatchDocument Sanitize(this JsonPatchDocument patchDoc)
        {
            List<OperationType> operationsAllowed = [OperationType.Replace];

            patchDoc.Operations.RemoveAll(operation => !operationsAllowed.Contains(operation.OperationType));
            patchDoc.Operations.RemoveAll(operation => operation.value is null || operation.path is null || operation.op is null);

            return patchDoc;
        }
    }
}
