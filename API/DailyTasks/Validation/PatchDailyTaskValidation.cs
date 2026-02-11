using FluentValidation;
using Habits.API.DailyTasks.DTO;

namespace Habits.API.DailyTasks.Validation
{
    public class PatchDailyTaskValidation : AbstractValidator<PatchDailyTaskRequest>
    {
        //TODO: Como testear una 
        public PatchDailyTaskValidation()
        {
            RuleFor(x => x.Minutes).MinutesAreValid();
        }
    }
}
