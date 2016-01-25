namespace StringsAnalyzerClient
{
    using System;

    class ConsoleClient
    {
        static void Main()
        {
            var client = new ServiceReferenceStringAnalizer.StringAnalyzerClient();

            Console.WriteLine(client.CountStringOccurrences("Ala bala, portokala, ala, no ne e kabala", "ala"));

            client.Close();
        }
    }
}