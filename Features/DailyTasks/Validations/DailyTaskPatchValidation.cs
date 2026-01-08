using Habits.Features.Tasks.Models;

namespace Habits.Features.Tasks.Validations
{
    public class DailyTaskPatchValidation()
    {
        private Dictionary<string, string[]> _errors = new Dictionary<string, string[]>();
        private const int MaxMinutes = 480; // 8 horas
        public Dictionary<string, string[]> Validate(DailyTaskPatchRequest body)
        {
            var minutesError = ValidateMinutes(body.Minutes);

            if (minutesError.Count > 0)
                _errors.TryAdd("Minutes", minutesError.ToArray());

            return _errors;
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
