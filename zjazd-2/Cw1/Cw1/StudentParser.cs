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
                string line = String.Empty;
                while ((line = stream.ReadLine()) != null)
                {

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
