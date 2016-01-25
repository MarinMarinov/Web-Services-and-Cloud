namespace DayOfWeekService
{
    using System;
    using System.ServiceModel;

    [ServiceContract]
    public interface IDayOfWeekGetter
    {
        [OperationContract]
        string GetDayOfWeek(DateTime date);
    }
}