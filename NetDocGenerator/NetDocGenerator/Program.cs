using NetDocGeneratorCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetDocGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            var generator = new DocGenerator();
            generator.Generate();
        }
    }
}
