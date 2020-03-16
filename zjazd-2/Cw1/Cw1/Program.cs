using System;

namespace Cw1
{
    class Program
    {
        public static string PathCSV { get; set; } = "data.csv";
        public static string ResultPath { get; set; } = "żesult.xml";
        public static OutputFormat OutputFormat { get; set; } = OutputFormat.XML;
        static void Main(string[] args)
        {

        }
    }
    public enum OutputFormat
    {
        XML
    }
}
