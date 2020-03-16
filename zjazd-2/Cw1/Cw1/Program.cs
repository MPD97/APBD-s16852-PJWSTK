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
                ValidatePossiblePathToFile(args[0]);
                PathCSV = args[0];
            }

            if (args.Length == 2 && string.IsNullOrEmpty(args[1]) == false)
            {
                ValidatePossiblePathToFile(args[1]);
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
                OutputFormat = (OutputFormat) output;
            }

            Console.ReadLine();
        }

        private static void ValidatePossiblePathToFile(string arg)
        {
            if (Directory.Exists(arg))
            {
                InputError();
            }
            if (arg.IndexOfAny(Path.GetInvalidPathChars()) != -1)
            {
                InputError();
            }
        }

        private static void InputError()
        {
            var errorMessage = "Podana ścieżka jest niepoprawna";
            Log.Logger.Error(errorMessage);
            throw new ArgumentException(errorMessage);
        }
    }
}
