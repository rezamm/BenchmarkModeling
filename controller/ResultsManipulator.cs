using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenchmarkModeling.controller
{
    public class ResultsManipulator
    {
        public ProxyManipulator PM { get; set; }
        public AppSettings Configs { get; set; }

        public ResultsManipulator(AppSettings configs)
        {
            PM = new ProxyManipulator();
            Configs = configs;
        }

        public void GenerateChart(string resultDirectory)
        {
            PM.Run(Configs.ChartGeneratorScriptLocation, new string[] { $"\"{resultDirectory}\"" });
        }

    }
}
