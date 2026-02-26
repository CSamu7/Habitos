using FluentValidation;
using Habits.API.DailyRoutines.DTO;

namespace Habits.API.DailyRoutines.Validation
{
    public class PatchDailyTaskValidation : AbstractValidator<PatchDailyRoutineRequest>
    {
        public PatchDailyTaskValidation()
        {
            RuleFor(x => x.Minutes).MinutesAreValid();
        }
    }
}
