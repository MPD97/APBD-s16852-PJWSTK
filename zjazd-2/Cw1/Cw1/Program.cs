using Serilog;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
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
            Stopwatch sw = new Stopwatch();
            sw.Start();

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
                RepairResultPath();
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


            List<ActiveStudieses> activeStudies = new List<ActiveStudieses>();

            foreach (var student in students)
            {
                var studiesName = student.Studies.Name;
                ActiveStudieses findActive = null;
                if ((findActive = activeStudies.FirstOrDefault(ele => ele.Name == studiesName)) == null)
                {
                    var active = new ActiveStudieses();
                    active.Name = studiesName;

                    activeStudies.Add(active);
                }
                else
                {
                    findActive.NumberOfStudents++;
                }
            }

            college.ActiveStudieses = activeStudies;

            RepairResultPath();

            if (OutputFormat == OutputFormat.XML)
            {
                XmlSerializer serializer = new XmlSerializer(typeof(Uczelnia));

                using (StreamWriter streamWriter = new StreamWriter(ResultPath + "." + OutputFormat.ToString().ToLower()))
                {
                    serializer.Serialize(streamWriter, college);
                }
            }

            sw.Stop();
            Console.WriteLine($"Ukończenie serializacji pliku w czasie: {sw.ElapsedMilliseconds} milisekund");
            Console.ReadLine();
        }

        private static void RepairResultPath()
        {
            if (ResultPath.Contains('.'))
            {
                var indexOfDot = ResultPath.LastIndexOf('.');
                ResultPath = ResultPath.Substring(0, ResultPath.Length - (ResultPath.Length - indexOfDot));
            }
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
