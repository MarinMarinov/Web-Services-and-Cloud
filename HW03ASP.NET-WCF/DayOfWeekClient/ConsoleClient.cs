namespace DayOfWeekClient
{
    using System;
    using System.Text;

    public class ConsoleClient
    {
        static void Main()
        {
            // Don't forget to first start the service from DayOfWeekService.svc -> View in Browser

            var client = new ServiceReferenceDayOfWeekInBulgarian.DayOfWeekGetterClient();

            Console.OutputEncoding = Encoding.UTF8;

            Console.WriteLine(client.GetDayOfWeek(new DateTime(1977, 03, 18)));

            client.Close();
        }
    }
}