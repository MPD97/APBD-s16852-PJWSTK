using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Cw1
{
    class StudentParser
    {
        private string InputCSVPath { get; }
        private string ResultPath { get; }
        private OutputFormat OutputFormat { get; }

        public StudentParser(string inputCSVPath, string resultPath, OutputFormat outputFormat)
        {
            InputCSVPath = inputCSVPath;
            ResultPath = resultPath;
            OutputFormat = outputFormat;
        }

        public bool TryLoadInputFile()
        {
            if(File.Exists(InputCSVPath) == false)
            {
                InputError(InputCSVPath);
            }
            using (StreamReader stream = new StreamReader(InputCSVPath, Encoding.UTF8))
            {
                string row = string.Empty;
                int line = 0;
                int numberOfColumns = 9;

                List<Student> students = new List<Student>();

                while ((row = stream.ReadLine()) != null)
                {
                    line++;
                    string[] columns = row.Split(',');
                    if (columns.Length != numberOfColumns)
                    {
                        Log.Logger.Error($"Linia numer: {line} nie posiada: {numberOfColumns} kolumn.");
                        continue;
                    }

                    for (int i = 0; i < columns.Length; i++)
                    {
                        if (string.IsNullOrWhiteSpace(columns[i]))
                        {
                            Log.Logger.Error($"Kolumna nr: {i}(liczone od 0) w linijce nr: {line}(liczone od 1) posiada pustą wartość.");
                            break;                       
                        }
                    }
                    
                    //Wojciech,Jankowski314,Informatyka dzienne po angielsku,Dzienne,4512,2000-02-12,314@pjwstk.edu.pl,Alina,Adam
                    var student = new Student();

                    student.Fname = columns[0];
                    student.Lname = columns[1];
                    student.Index = columns[4];

                    if(students.Any(ele => ele.Fname == student.Fname
                                        && ele.Lname == student.Lname
                                        && ele.Index == student.Index))
                    {
                        Log.Logger.Warning($"Niepoprawny duplikat studenta o indeksie: {student.Index}");
                        continue;
                    }


                    var value = string.Empty;
                    


                }
            }
        }
        private void InputError(string file)
        {
            var errorMessage = $"Plik: {file} nie istnieje";
            Log.Logger.Error(errorMessage);
            throw new FileNotFoundException(errorMessage);
        }
    }
}
