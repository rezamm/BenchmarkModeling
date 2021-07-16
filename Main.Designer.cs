namespace BenchmarkModeling
{
    partial class Main
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.select_result_directory = new System.Windows.Forms.Button();
            this.btn_select_test_file = new System.Windows.Forms.Button();
            this.txt_result_dir = new System.Windows.Forms.TextBox();
            this.btn_select_train_file = new System.Windows.Forms.Button();
            this.txt_test_file = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txt_train_file = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.train_file = new System.Windows.Forms.OpenFileDialog();
            this.test_file = new System.Windows.Forms.OpenFileDialog();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.ProgressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.current_method = new System.Windows.Forms.ToolStripStatusLabel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dt_methods = new System.Windows.Forms.DataGridView();
            this.title = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pipeline = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.configurations = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.is_enabled = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.btn_start = new System.Windows.Forms.Button();
            this.backgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.result_directory_dialog = new System.Windows.Forms.FolderBrowserDialog();
            this.btn_show_results = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dt_methods)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Train File";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.select_result_directory);
            this.groupBox1.Controls.Add(this.btn_select_test_file);
            this.groupBox1.Controls.Add(this.txt_result_dir);
            this.groupBox1.Controls.Add(this.btn_select_train_file);
            this.groupBox1.Controls.Add(this.txt_test_file);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txt_train_file);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(661, 115);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Input";
            // 
            // select_result_directory
            // 
            this.select_result_directory.Location = new System.Drawing.Point(509, 74);
            this.select_result_directory.Name = "select_result_directory";
            this.select_result_directory.Size = new System.Drawing.Size(132, 23);
            this.select_result_directory.TabIndex = 4;
            this.select_result_directory.Text = "Select Directory";
            this.select_result_directory.UseVisualStyleBackColor = true;
            this.select_result_directory.Click += new System.EventHandler(this.Select_result_directory_Click);
            // 
            // btn_select_test_file
            // 
            this.btn_select_test_file.Location = new System.Drawing.Point(509, 48);
            this.btn_select_test_file.Name = "btn_select_test_file";
            this.btn_select_test_file.Size = new System.Drawing.Size(132, 23);
            this.btn_select_test_file.TabIndex = 4;
            this.btn_select_test_file.Text = "Select File";
            this.btn_select_test_file.UseVisualStyleBackColor = true;
            this.btn_select_test_file.Click += new System.EventHandler(this.Btn_select_test_file_Click);
            // 
            // txt_result_dir
            // 
            this.txt_result_dir.Location = new System.Drawing.Point(92, 74);
            this.txt_result_dir.Name = "txt_result_dir";
            this.txt_result_dir.Size = new System.Drawing.Size(405, 20);
            this.txt_result_dir.TabIndex = 3;
            // 
            // btn_select_train_file
            // 
            this.btn_select_train_file.Location = new System.Drawing.Point(509, 24);
            this.btn_select_train_file.Name = "btn_select_train_file";
            this.btn_select_train_file.Size = new System.Drawing.Size(132, 23);
            this.btn_select_train_file.TabIndex = 2;
            this.btn_select_train_file.Text = "Select File";
            this.btn_select_train_file.UseVisualStyleBackColor = true;
            this.btn_select_train_file.Click += new System.EventHandler(this.Btn_select_train_file_Click);
            // 
            // txt_test_file
            // 
            this.txt_test_file.Location = new System.Drawing.Point(92, 50);
            this.txt_test_file.Name = "txt_test_file";
            this.txt_test_file.Size = new System.Drawing.Size(405, 20);
            this.txt_test_file.TabIndex = 3;
            this.txt_test_file.Text = "test.xlsx";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 77);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(82, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Result Directory";
            // 
            // txt_train_file
            // 
            this.txt_train_file.Location = new System.Drawing.Point(92, 24);
            this.txt_train_file.Name = "txt_train_file";
            this.txt_train_file.Size = new System.Drawing.Size(405, 20);
            this.txt_train_file.TabIndex = 1;
            this.txt_train_file.Text = "train.xlsx";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 52);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Test File";
            // 
            // train_file
            // 
            this.train_file.FileName = "openFileDialog1";
            // 
            // test_file
            // 
            this.test_file.FileName = "openFileDialog1";
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel,
            this.ProgressBar,
            this.current_method});
            this.statusStrip.Location = new System.Drawing.Point(0, 469);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(784, 22);
            this.statusStrip.TabIndex = 2;
            this.statusStrip.Text = "statusStrip1";
            // 
            // toolStripStatusLabel
            // 
            this.toolStripStatusLabel.Name = "toolStripStatusLabel";
            this.toolStripStatusLabel.Size = new System.Drawing.Size(39, 17);
            this.toolStripStatusLabel.Text = "Ready";
            // 
            // ProgressBar
            // 
            this.ProgressBar.Name = "ProgressBar";
            this.ProgressBar.Size = new System.Drawing.Size(100, 16);
            // 
            // current_method
            // 
            this.current_method.Name = "current_method";
            this.current_method.Size = new System.Drawing.Size(15, 17);
            this.current_method.Text = "C";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dt_methods);
            this.groupBox2.Location = new System.Drawing.Point(12, 133);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(760, 286);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Methods";
            // 
            // dt_methods
            // 
            this.dt_methods.AllowUserToAddRows = false;
            this.dt_methods.AllowUserToDeleteRows = false;
            this.dt_methods.AllowUserToOrderColumns = true;
            this.dt_methods.AllowUserToResizeRows = false;
            this.dt_methods.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dt_methods.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.title,
            this.pipeline,
            this.configurations,
            this.is_enabled});
            this.dt_methods.Location = new System.Drawing.Point(6, 19);
            this.dt_methods.Name = "dt_methods";
            this.dt_methods.Size = new System.Drawing.Size(748, 261);
            this.dt_methods.TabIndex = 0;
            // 
            // title
            // 
            this.title.HeaderText = "Title";
            this.title.Name = "title";
            this.title.Width = 150;
            // 
            // pipeline
            // 
            this.pipeline.HeaderText = "Pipeline";
            this.pipeline.Name = "pipeline";
            this.pipeline.Width = 300;
            // 
            // configurations
            // 
            this.configurations.HeaderText = "Configurations";
            this.configurations.Name = "configurations";
            this.configurations.Width = 200;
            // 
            // is_enabled
            // 
            this.is_enabled.HeaderText = "Enabled";
            this.is_enabled.Name = "is_enabled";
            this.is_enabled.Width = 38;
            // 
            // btn_start
            // 
            this.btn_start.Location = new System.Drawing.Point(12, 425);
            this.btn_start.Name = "btn_start";
            this.btn_start.Size = new System.Drawing.Size(75, 23);
            this.btn_start.TabIndex = 5;
            this.btn_start.Text = "Start";
            this.btn_start.UseVisualStyleBackColor = true;
            this.btn_start.Click += new System.EventHandler(this.Btn_start_Click);
            // 
            // backgroundWorker
            // 
            this.backgroundWorker.WorkerReportsProgress = true;
            this.backgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.BackgroundWorker_DoWork);
            this.backgroundWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.BackgroundWorker_ProgressChanged);
            this.backgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.BackgroundWorker_RunWorkerCompleted);
            // 
            // btn_show_results
            // 
            this.btn_show_results.Location = new System.Drawing.Point(94, 424);
            this.btn_show_results.Name = "btn_show_results";
            this.btn_show_results.Size = new System.Drawing.Size(129, 23);
            this.btn_show_results.TabIndex = 6;
            this.btn_show_results.Text = "Show Latest Results";
            this.btn_show_results.UseVisualStyleBackColor = true;
            this.btn_show_results.Click += new System.EventHandler(this.Btn_show_results_Click);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 491);
            this.Controls.Add(this.btn_show_results);
            this.Controls.Add(this.btn_start);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.groupBox1);
            this.MaximumSize = new System.Drawing.Size(800, 530);
            this.MinimumSize = new System.Drawing.Size(800, 530);
            this.Name = "Main";
            this.Text = "Main";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dt_methods)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btn_select_test_file;
        private System.Windows.Forms.Button btn_select_train_file;
        private System.Windows.Forms.TextBox txt_test_file;
        private System.Windows.Forms.TextBox txt_train_file;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.OpenFileDialog train_file;
        private System.Windows.Forms.OpenFileDialog test_file;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel;
        private System.Windows.Forms.ToolStripProgressBar ProgressBar;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView dt_methods;
        private System.Windows.Forms.DataGridViewTextBoxColumn title;
        private System.Windows.Forms.DataGridViewTextBoxColumn pipeline;
        private System.Windows.Forms.DataGridViewTextBoxColumn configurations;
        private System.Windows.Forms.DataGridViewCheckBoxColumn is_enabled;
        private System.Windows.Forms.Button btn_start;
        private System.ComponentModel.BackgroundWorker backgroundWorker;
        private System.Windows.Forms.Button select_result_directory;
        private System.Windows.Forms.TextBox txt_result_dir;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.FolderBrowserDialog result_directory_dialog;
        private System.Windows.Forms.ToolStripStatusLabel current_method;
        private System.Windows.Forms.Button btn_show_results;
    }
}