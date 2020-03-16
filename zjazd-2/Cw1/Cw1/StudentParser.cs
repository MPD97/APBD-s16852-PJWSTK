using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
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
                while ((row = stream.ReadLine()) != null)
                {
                    line++;
                    string[] columns = row.Split(',');
                    if (columns.Length != numberOfColumns)
                    {
                        Log.Logger.Error($"Line: {line} is not containging: {numberOfColumns} columns.");
                        continue;
                    }
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
