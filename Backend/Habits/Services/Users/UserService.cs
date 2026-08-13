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
        public async Task<Result<PatchUserRequest>> ModifyUser(User user, PatchUserRequest body)
        {
            DateTime? cutOffTimeUTC = null;
            List<MinGoalHistory> minGoalChanges = await GetMinGoalChanges(user);

            user.TimeZone = body.Timezone ?? user.TimeZone;

            if (body.CutOffTime is not null)
            {
                //if timezone doesn't exist, return 404.
                TimeZoneInfo timezone = TimeZoneInfo.FindSystemTimeZoneById(user.TimeZone);
                cutOffTimeUTC = TimeZoneInfo.ConvertTimeToUtc(new(1, 1, 1, body.CutOffTime.Value, 0, 0), timezone);
            }

            user.CutOffTime = cutOffTimeUTC?.Hour ?? user.CutOffTime;

            if (minGoalChanges.Count >= 2 && body.MinGoal is not null && body.MinGoal != user.MinGoal)
            {
                DateTimeOffset availableDate = minGoalChanges[0].ChangedAt.AddDays(7);
                return Result<PatchUserRequest>.Failure(Status.InvalidData, $"Ya has modificado tu meta dos veces esta semana. Espera hasta el s{availableDate.ToString()}");
            }

            if (body.MinGoal is not null)
            {
                await _db.MinGoalHistory.AddAsync
                        (new() { MinGoal = body.MinGoal.Value, ChangedAt = DateTimeOffset.Now, IdUser = user.Id });

                user.MinGoal = (byte?)body.MinGoal ?? user.MinGoal;
            }

            await _db.SaveChangesAsync();
            return Result<PatchUserRequest>.Success(body);
        }

        private async Task<List<MinGoalHistory>> GetMinGoalChanges(User userDb)
        {
            //if the user wants to modify its min goal.
            DateTimeOffset lastWeek = DateTimeOffset.Now.AddDays(-7);
            List<MinGoalHistory> recentChanges = await _db.MinGoalHistory
                .Where(table => table.IdUser == userDb.Id && table.ChangedAt >= lastWeek)
                .OrderBy(table => table.ChangedAt)
                .ToListAsync();

            return recentChanges;
        }
    }
}
