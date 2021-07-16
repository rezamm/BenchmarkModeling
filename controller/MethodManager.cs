using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenchmarkModeling.controller
{
    public class MethodManager
    {
        public ProxyManipulator PM { get; set; }
        public AppSettings Configs { get; set; }

        public MethodManager(AppSettings configs)
        {
            PM = new ProxyManipulator();
            Configs = configs;
        }

        private static bool IsPropertyExist(dynamic settings, string name)
        {
            if (settings is ExpandoObject)
                return ((IDictionary<string, object>)settings).ContainsKey(name);

            return settings.GetType().GetProperty(name) != null;
        }

        public static bool PropertyExists(dynamic obj, string name)
        {
            if (obj == null) return false;

            else if (obj is IDictionary<string, object> dict)
            {
                return dict.ContainsKey(name);
            }

            else if (obj is Newtonsoft.Json.Linq.JObject jObject)
            {
                return jObject.ContainsKey(name);
            }

            else
            {
                return obj.GetType().GetProperty(name) != null;
            }
        }

        public void RunMethod(string methodTitle, string train_file_path, string test_file_path, string result_directory)
        {
            // get configurations to run
            var method = Configs.Methods.First(m => m.Properties[0] == methodTitle);
            dynamic config = JsonConvert.DeserializeObject(method.Properties[1]);
            var vectorizer = Convert.ToString(config.vectorizer);
            var classifier = Convert.ToString(config.classifier);

            var dim_reduction = "";
            if (PropertyExists(config, "dim_reduction"))
            {
                dim_reduction = Convert.ToString(config.dim_reduction);
            }

            string[] args;
            if (!string.IsNullOrEmpty(dim_reduction))
            {
                args = new string[] { $"\"{train_file_path}\"", $"\"{test_file_path}\"", $"\"{result_directory}\"", $"\"{vectorizer}\"", $"\"{classifier}\"", $"--dimentionality_reduction \"{dim_reduction}\"" };
            }
            else
            {
                args = new string[] { $"\"{train_file_path}\"", $"\"{test_file_path}\"", $"\"{result_directory}\"", $"\"{vectorizer}\"", $"\"{classifier}\"" };
            }

            PM.Run(method.Properties[2], args);
        }
    }
}
