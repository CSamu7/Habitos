namespace Habits.Features.Tasks
{
    public class DailyTaskFilter()
    {
        public List<string> Errors { get; set; } = new List<string>();
        private const int MaxMinutes = 480; // 8 horas
        public Dictionary<string, string[]> Validate(PatchDailyTask body)
        {
            Dictionary<string, string[]> errors = new Dictionary<string, string[]>();

            var minutesError = ValidateMinutes(body.Minutes);

            if (minutesError.Count > 0)
                errors.TryAdd("Minutes", minutesError.ToArray());

            return errors;
        }
        private List<string> ValidateMinutes(int minutes)
        {
            List<string> errors = new List<string>();

            if (minutes >= MaxMinutes)
                errors.Add("Los minutos no pueden ser mayor a 8 horas");

            if (minutes <= 0)
                errors.Add("Los minutos deben ser positivos");

            return errors;
        }
    }
}
