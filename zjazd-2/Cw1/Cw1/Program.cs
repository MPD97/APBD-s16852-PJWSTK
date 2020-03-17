using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace Cw1
{
    class Program
    {
        private static readonly string CurrentDirectory = Directory.GetCurrentDirectory();

        private static string PathCsv { get; set; } = Path.Combine(CurrentDirectory, "data.csv");
        private static string ResultPath { get; set; } = Path.Combine(CurrentDirectory, "żesult.xml");
        private static OutputFormat OutputFormat { get; set; } = OutputFormat.XML;

        static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.File("łog.txt")
                .CreateLogger();

            if (args.Length >= 1 && string.IsNullOrEmpty(args[0]) == false)
            {
                ValidatePossiblePathToFile(args[0]);
                var combinedPath = Path.Combine(CurrentDirectory, args[0]);
                PathCsv = args[0];
            }

            if (args.Length >= 2 && string.IsNullOrEmpty(args[1]) == false)
            {
                if (ResultPath.Contains('.'))
                {
                    var indexOfDot = ResultPath.LastIndexOf('.');
                    ResultPath = ResultPath.Substring(indexOfDot, ResultPath.Length - indexOfDot);
                }
                ValidatePossiblePathToFile(args[1]);
                ResultPath = args[1];
            }

            if (args.Length >= 3 && string.IsNullOrEmpty(args[2]) == false)
            {
                object output;
                Enum.TryParse(typeof(OutputFormat), args[2], out output);
                if ((OutputFormat) output == OutputFormat.NULL)
                {
                    Log.Logger.Error($"Nieznany typ pliku wyjściowego. Używam formatu domyślnego: {OutputFormat}");
                }
                OutputFormat = (OutputFormat) output;
            }
            List<Student> students;

            var parser = new StudentParser(PathCsv, ResultPath, OutputFormat);
            if ((students = parser.TryLoadInputFile()) == null)
            {
                var message = "Wynik przetwarzania pliku wejściowego to null.";
                Log.Logger.Error(message);
                throw new ArgumentNullException(message);
            }
            var college = new Uczelnia();
            college.Author = "Mateusz Ambroziak";
            college.CreatedAt = DateTime.Now.ToString("dd.MM.yyyy");
            college.Students = students;


            
            XmlSerializer serializer = new XmlSerializer(typeof(Uczelnia));

            using (StreamWriter streamWriter = new StreamWriter(ResultPath))
            {
                
            }

            Console.ReadLine();
        }

        private static void ValidatePossiblePathToFile(string file)
        {
            if (Directory.Exists(file))
            {
                InputError();
            }
            if (Directory.Exists(Path.Combine(CurrentDirectory, file)))
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
