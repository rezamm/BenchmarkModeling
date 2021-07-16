using BenchmarkModeling.controller;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BenchmarkModeling
{
    public partial class Main : Form
    {
        public AppSettings Config { get; set; }
        public string LatestProccessingDateTime { get; set; }
        public string ResultDirectory { get; set; }

        public Main()
        {
            InitializeComponent();
            Config = new AppSettings();

            // set table values
            foreach (var method in Config.Methods)
            {
                dt_methods.Rows.Add(method.Properties[0], method.Properties[1], method.Properties[2], method.IsSelectedByDefault);
            }

            // set default directory for result
            string currentDir = Directory.GetCurrentDirectory();
            txt_result_dir.Text = currentDir + "\\result";

        }

        private void Btn_select_train_file_Click(object sender, EventArgs e)
        {
            train_file.ShowDialog();
            txt_train_file.Text = train_file.FileName;
        }

        private void Btn_select_test_file_Click(object sender, EventArgs e)
        {
            test_file.ShowDialog();
            txt_test_file.Text = test_file.FileName;
        }

        private void Btn_start_Click(object sender, EventArgs e)
        {
            toolStripStatusLabel.Text = "Processing";
            btn_start.Enabled = false;

            backgroundWorker.RunWorkerAsync();
        }

        private void Select_result_directory_Click(object sender, EventArgs e)
        {
            result_directory_dialog.ShowDialog();
            txt_result_dir.Text = result_directory_dialog.SelectedPath;
        }

        private void BackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {

            if (string.IsNullOrEmpty(txt_train_file.Text))
            {
                MessageBox.Show("Please Select Train File!");
                return;
            }

            if (!File.Exists(txt_train_file.Text))
            {
                MessageBox.Show("Train File does not exist!");
                return;
            }

            if (string.IsNullOrEmpty(txt_test_file.Text))
            {
                MessageBox.Show("Please Select Test File!");
                return;
            }

            if (!File.Exists(txt_test_file.Text))
            {
                MessageBox.Show("Test File does not exist!");
                return;
            }

            if (string.IsNullOrEmpty(txt_result_dir.Text))
            {
                MessageBox.Show("Please Result Directory!");
                return;
            }

            if (!Directory.Exists(txt_result_dir.Text))
            {
                MessageBox.Show("Result Directory does not exist!");
                return;
            }

            LatestProccessingDateTime = DateTime.Now.ToString(format: "yyyy'-'MM'-'dd'T'HH'-'mm'-'ss");
            ResultDirectory = Path.Combine(txt_result_dir.Text, LatestProccessingDateTime);

            if (!Directory.Exists(ResultDirectory))
            {
                Directory.CreateDirectory(ResultDirectory);
            }

            // run selected benchmark method
            var selected_methods = new List<string>();
            foreach (DataGridViewRow row in dt_methods.Rows)
            {

                if (Convert.ToBoolean(row.Cells["is_enabled"].FormattedValue))
                {
                    selected_methods.Add(Convert.ToString(row.Cells["title"].FormattedValue));
                }
            }

            if (selected_methods.Count == 0)
            {
                MessageBox.Show("No Method is Selected!");
                return;
            }

            var mm = new MethodManager(Config);
            var each_step_progress = 100 / selected_methods.Count;
            for (int i = 0; i < selected_methods.Count; i++)
            {
                var method = selected_methods[i];
                current_method.Text = "Current Method: " + method;
                mm.RunMethod(method, txt_train_file.Text, txt_test_file.Text, ResultDirectory);

                var progress_value = Convert.ToInt32(each_step_progress * (i + 1));
                backgroundWorker.ReportProgress(progress_value);
            }
        }

        private void BackgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            ProgressBar.Value = e.ProgressPercentage;
        }

        private void BackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            btn_start.Enabled = true;
            toolStripStatusLabel.Text = "Ready";
            current_method.Text = "";
            //backgroundWorker.ReportProgress(0);
            ProgressBar.Value = 0;
        }

        private void Btn_show_results_Click(object sender, EventArgs e)
        {
            IllustrateResults();
        }

        #region Methods

        private void IllustrateResults()
        {
            // get latest experiment
            var experiments = Directory.GetDirectories(txt_result_dir.Text);

            // get directory with valid files
            var idx = experiments.Length;
            string directoryPath;
            string experiment;
            while (true)
            {
                if (idx == 0)
                {
                    MessageBox.Show("No Valid Previous Benchmarks Available!");
                    return;
                }
                experiment = experiments[--idx];
                // check files in directory
                directoryPath = Path.Combine(txt_result_dir.Text, experiment);
                var filesCount = Directory.GetFiles(directoryPath).Length;
                if (filesCount > 0)
                {
                    break;
                }
            }

            var rm = new ResultsManipulator(Config);
            rm.GenerateChart(directoryPath);

            // open result folder
            var pm = new ProxyManipulator();
            pm.OpenDirectory(directoryPath);
        }

        #endregion

    }
}
