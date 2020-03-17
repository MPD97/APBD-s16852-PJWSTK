using Serilog;
using System;
using System.Collections.Generic;
using System.IO;

namespace Cw1
{
    class Program
    {
        private static string _currentDirectory = Directory.GetCurrentDirectory();

        private static string _pathCSV { get; set; } = Path.Combine(_currentDirectory, "data.csv");
        private static string _resultPath { get; set; } = Path.Combine(_currentDirectory, "żesult.xml");
        private static OutputFormat _outputFormat { get; set; } = OutputFormat.XML;

        static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.File("łog.txt")
                .CreateLogger();

            if (args.Length >= 1 && string.IsNullOrEmpty(args[0]) == false)
            {
                ValidatePossiblePathToFile(args[0]);
                var combinedPath = Path.Combine(_currentDirectory, args[0]);
                _pathCSV = args[0];
            }

            if (args.Length >= 2 && string.IsNullOrEmpty(args[1]) == false)
            {
                ValidatePossiblePathToFile(args[1]);
                _resultPath = args[1];
            }

            if (args.Length >= 3 && string.IsNullOrEmpty(args[2]) == false)
            {
                object output;
                Enum.TryParse(typeof(OutputFormat), args[2], out output);
                if ((OutputFormat) output == OutputFormat.NULL)
                {
                    Log.Logger.Error($"Nieznany typ pliku wyjściowego. Używam formatu domyślnego: {_outputFormat}");
                }
                _outputFormat = (OutputFormat) output;
            }
            List<Student> students;

            var parser = new StudentParser(_pathCSV, _resultPath, _outputFormat);
            if ((students = parser.TryLoadInputFile()) == null)
            {
                Log.Logger.Error($"Wynik przetwarzania pliku wejściowego to null.");
            }


            Console.ReadLine();
        }

        private static void ValidatePossiblePathToFile(string file)
        {
            if (Directory.Exists(file))
            {
                InputError();
            }
            if (Directory.Exists(Path.Combine(_currentDirectory, file)))
            {
                InputError();
            }
            if (file.IndexOfAny(Path.GetInvalidPathChars()) != -1)
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
