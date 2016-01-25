namespace DayOfWeekService
{
    using System;

    public class DayOfWeekGetter : IDayOfWeekGetter
    {
        public string GetDayOfWeek(DateTime date)
        {
            switch (date.DayOfWeek)
            {
                case DayOfWeek.Monday: return "Понеделник";
                case DayOfWeek.Tuesday: return "Вторник";
                case DayOfWeek.Wednesday: return "Сряда";
                case DayOfWeek.Thursday: return "Четвъртък";
                case DayOfWeek.Friday: return "Петък";
                case DayOfWeek.Saturday: return "Събота";
                case DayOfWeek.Sunday: return "Неделя";

                default: return null;
            }
        }
    }
}