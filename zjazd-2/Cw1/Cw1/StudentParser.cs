using System;
using System.Collections.Generic;
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
    }
}
