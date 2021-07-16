using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using BenchmarkModeling.model;

namespace BenchmarkModeling
{

    public class AppSettings
    {
        public Dictionary<string, string> Settings = new Dictionary<string, string>();

        public List<MethodModel> Methods = new List<MethodModel>();
        public string ChartGeneratorScriptLocation { get; set; }

        public dynamic config;

        public AppSettings()
        {
            // load config
            ReadAllSettings();

            LoadConfig(Settings["config_file_location"]);
        }

        private void ReadAllSettings()
        {
            try
            {
                var appSettings = ConfigurationManager.AppSettings;

                if (appSettings.Count == 0)
                {
                    Console.WriteLine("AppSettings is empty.");
                }
                else
                {
                    foreach (var key in appSettings.AllKeys)
                    {
                        Console.WriteLine("Key: {0} Value: {1}", key, appSettings[key]);
                        Settings[key] = appSettings[key];
                    }
                }
            }
            catch (ConfigurationErrorsException)
            {
                Console.WriteLine("Error reading app settings");
            }
        }

        private void LoadConfig(string file_name)
        {
            string json = File.ReadAllText(file_name);
            config = JsonConvert.DeserializeObject(json);

            ChartGeneratorScriptLocation = config.chart_generator_script_location;

            foreach (var method in config.methods)
            {
                string title = method.title;
                string pipeline = method.pipeline.ToString(Formatting.None);
                string script_file_name = method.script_file_name;
                bool isSelecetedByDefault = method.is_selected_by_default;
                if (method.ContainsKey("configs"))
                {
                    string configs = method.configs.ToString(Formatting.None);
                    Methods.Add(
                        new MethodModel(
                            isSelecetedByDefault,
                            new List<string> {
                                title,
                                pipeline.Replace('\n', ' ').Replace('\\', ' '),
                                script_file_name,
                                configs.Replace('\n', ' ').Replace('\\', ' ')
                            }
                            )
                        );

                }
                else
                {
                    Methods.Add(
                        new MethodModel(
                            isSelecetedByDefault,
                            new List<string> {
                                title,
                                pipeline.Replace('\n', ' ').Replace('\\', ' '),
                                script_file_name,
                                ""
                            }
                            )
                        );
                }

            }
        }
    }
}
