using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace NetDocGeneratorCore
{
    public class DocGenerator
    {
        private string InputFileName = "input.cs";

        public DocGenerator()
        {
            var fileDetails = File.ReadAllLines(Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), InputFileName));
        }

        public void Generate()
        {

        }
    }
}