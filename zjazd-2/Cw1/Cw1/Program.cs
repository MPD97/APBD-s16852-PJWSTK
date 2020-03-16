using Serilog;
using System;
using System.IO;

namespace Cw1
{
    class Program
    {
        public static string PathCSV { get; set; } = "data.csv";
        public static string ResultPath { get; set; } = "żesult.xml";
        public static OutputFormat OutputFormat { get; set; } = OutputFormat.XML;
        static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.File("łog.txt")
                .CreateLogger();

            if (args.Length == 1 && string.IsNullOrEmpty(args[0]) == false)
            {
                if (Directory.Exists(args[0]))
                {
                    InputError();
                }
                if (args[0].IndexOfAny(Path.GetInvalidPathChars()) != -1)
                {
                    InputError();
                }
                PathCSV = args[0];
            }


            if (args.Length == 2 && string.IsNullOrEmpty(args[1]) == false)
            {
                if (Directory.Exists(args[1]))
                {
                    InputError();
                }
                if (args[1].IndexOfAny(Path.GetInvalidPathChars()) != -1)
                {
                    InputError();
                }
                ResultPath = args[1];
            }

            if (args.Length == 3 && string.IsNullOrEmpty(args[2]) == false)
            {
                object output;
                Enum.TryParse(typeof(OutputFormat), args[2], out output);
                if ((OutputFormat) output == OutputFormat.NULL)
                {
                    Log.Logger.Error($"Nieznany typ pliku wyjściowego. Używam formatu domyślnego: {OutputFormat}");
                }
                ResultPath = args[1];
            }

            Console.ReadLine();
        }

        private static void InputError()
        {
            var errorMessage = "Podana ścieżka jest niepoprawna";
            Log.Logger.Error(errorMessage);
            throw new ArgumentException(errorMessage);
        }
    }
    public enum OutputFormat
    {
        NULL,
        XML
    }
}
