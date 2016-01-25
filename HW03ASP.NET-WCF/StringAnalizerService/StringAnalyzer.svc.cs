namespace StringAnalizerService
{
    using System;
    using System.Linq;

    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "StringAnalyzer" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select StringAnalyzer.svc or StringAnalyzer.svc.cs at the Solution Explorer and start debugging.
    public class StringAnalyzer : IStringAnalyzer
    {
        public int CountStringOccurrences(string first, string second)
        {
            return first.Split(new string[] { second }, StringSplitOptions.RemoveEmptyEntries).Count();
        }
    }
}