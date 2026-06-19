using Habits.API.Users.DTO;
using Habits.Models;
using Microsoft.EntityFrameworkCore;

namespace Habits.Services.Users
{
    public class UserService
    {
        private readonly HabitsContext _db;
        public UserService(HabitsContext db)
        {
            _db = db;
        }
        public async Task<Result<PatchUserRequest>> ModifyUser(string username, PatchUserRequest body)
        {
            var userDb = await _db.Users.SingleOrDefaultAsync(user => user.UserName == username);

            if (userDb == null) return Result<PatchUserRequest>.Failure(Status.NotOwner, "No tienes acceso a este recurso");

            DateTimeOffset lastWeek = DateTimeOffset.Now.AddDays(-7);
            List<MinGoalHistory> recentChanges = await _db.MinGoalHistory
                .Where(table => table.IdUser == userDb.Id && table.ChangedAt >= lastWeek)
                .OrderBy(table => table.ChangedAt)
                .ToListAsync();

            userDb.TimeZone = body.Timezone ?? userDb.TimeZone;
            userDb.CutOffTime = body.CutOffTime ?? userDb.CutOffTime;

            if (recentChanges.Count >= 2)
            {
                DateTimeOffset availableDate = recentChanges[0].ChangedAt.AddDays(7);
                return Result<PatchUserRequest>.Failure(Status.InvalidData, $"Ya has modificado tu meta dos veces esta semana. Espera hasta {availableDate.ToString()}");
            }

            if (body.MinGoal is not null && body.MinGoal != userDb.MinGoal)
            {
                await _db.MinGoalHistory.AddAsync
                    (new() { MinGoal = body.MinGoal.Value, ChangedAt = DateTimeOffset.Now, IdUser = userDb.Id});
                userDb.MinGoal = (byte?)body.MinGoal ?? userDb.MinGoal;
            }

            await _db.SaveChangesAsync();
            return Result<PatchUserRequest>.Success(body);
        }
    }
}
