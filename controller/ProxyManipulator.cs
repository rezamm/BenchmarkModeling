using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenchmarkModeling.controller
{
    public class ProxyManipulator
    {
        public ProxyManipulator()
        {

        }

        public void Run(
            string script_file_name,
            string[] args
        )
        {
            var argsString = "";
            foreach (var arg in args)
            {
                argsString += " " + arg;
            }

            string command = $"python {script_file_name} {argsString}";

            System.Diagnostics.ProcessStartInfo procStartInfo = new System.Diagnostics.ProcessStartInfo("cmd", "/c " + command);

            // The following commands are needed to redirect the standard output.
            // This means that it will be redirected to the Process.StandardOutput StreamReader.
            procStartInfo.RedirectStandardOutput = true;
            procStartInfo.UseShellExecute = false;
            // Do not create the black window.
            procStartInfo.CreateNoWindow = true;
            // Now we create a process, assign its ProcessStartInfo and start it
            Process proc = new Process();
            proc.StartInfo = procStartInfo;
            proc.Start();
            // Get the output into a string
            string result = proc.StandardOutput.ReadToEnd();
            // Display the command output.
            Console.WriteLine(result);
        }

        public void OpenDirectory(
            string directory
        )
        {
            Process.Start(@"" + directory);
        }
    }
}
