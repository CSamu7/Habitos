using Habits.API.Routines.DTO;
using System.Net.Http.Json;
using Testing.IntegrationTesting.Helpers;

namespace Testing.IntegrationTesting.RoutineEndpoints
{
    [Collection("Database collection")]
    public class PatchRoutineTests(CustomWebApplication application) : IAsyncLifetime
    {
        //necesitamos buscar la manera de como separar toda esta mierda
        [Fact]
        public async Task Patch_routine_is_successful()
        {
            CancellationToken token = CancellationToken.None;
            //create routine
            var client = application.CreateAuthClient();
            PostRoutineRequest routine = new("TestData", 30, []);
            var result = await client.PostAsJsonAsync("api/users/samu/routines/", routine, token);

            result.EnsureSuccessStatusCode();
            //edit routine
            var patchRoutine = "{'name': 'Cambio' }";
            var patchResult = await client.PatchAsJsonAsync(result.Headers.Location, patchRoutine, token);
            
            patchResult.EnsureSuccessStatusCode();

            //
            var getRoutine = await client.GetAsync(result.Headers.Location, token);
            var json = await getRoutine.Content.ReadFromJsonAsync<GetRoutineResponse>(token);

            Assert.Equal(routine.Name, json?.Name);
        }
        public ValueTask InitializeAsync() => ValueTask.CompletedTask;
        public async ValueTask DisposeAsync() => await application.ResetDB();
    }
}
