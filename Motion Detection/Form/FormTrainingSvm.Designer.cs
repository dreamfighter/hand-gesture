namespace Motion_Detection_v2
{
    partial class FormTrainingSvm
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
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.datasetSource = new System.Windows.Forms.DataGridView();
            this.Target = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Observation = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Result = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Recognition = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.trainingData = new System.Windows.Forms.ComboBox();
            this.butPredictHmm = new System.Windows.Forms.Button();
            this.butTrainHmm = new System.Windows.Forms.Button();
            this.numGamma = new System.Windows.Forms.NumericUpDown();
            this.numC = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.kernelType = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.imageDirectory = new System.Windows.Forms.FolderBrowserDialog();
            this.analisisBut = new System.Windows.Forms.Button();
            this.openDataDialog = new System.Windows.Forms.OpenFileDialog();
            this.buttonSaveTraining = new System.Windows.Forms.Button();
            this.saveDataDialog = new System.Windows.Forms.SaveFileDialog();
            this.saveModelDialog = new System.Windows.Forms.SaveFileDialog();
            this.openModelDialog = new System.Windows.Forms.OpenFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.datasetSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numGamma)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numC)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox3
            // 
            this.pictureBox3.Location = new System.Drawing.Point(12, 12);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(320, 240);
            this.pictureBox3.TabIndex = 9;
            this.pictureBox3.TabStop = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.datasetSource);
            this.groupBox1.Location = new System.Drawing.Point(338, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(449, 193);
            this.groupBox1.TabIndex = 21;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Training Result";
            // 
            // datasetSource
            // 
            this.datasetSource.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.datasetSource.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Target,
            this.Observation,
            this.Result,
            this.Recognition});
            this.datasetSource.Dock = System.Windows.Forms.DockStyle.Fill;
            this.datasetSource.Location = new System.Drawing.Point(3, 16);
            this.datasetSource.Name = "datasetSource";
            this.datasetSource.Size = new System.Drawing.Size(443, 174);
            this.datasetSource.TabIndex = 1;
            this.datasetSource.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.datasetSource_CellClick);
            this.datasetSource.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.datasetSource_CellEnter);
            // 
            // Target
            // 
            this.Target.HeaderText = "Target";
            this.Target.Name = "Target";
            // 
            // Observation
            // 
            this.Observation.HeaderText = "Observation";
            this.Observation.Name = "Observation";
            // 
            // Result
            // 
            this.Result.HeaderText = "Result";
            this.Result.Name = "Result";
            // 
            // Recognition
            // 
            this.Recognition.FalseValue = "false";
            this.Recognition.HeaderText = "Recognition";
            this.Recognition.Name = "Recognition";
            this.Recognition.TrueValue = "true";
            // 
            // trainingData
            // 
            this.trainingData.FormattingEnabled = true;
            this.trainingData.Items.AddRange(new object[] {
            "Select Data Set",
            "From File Training",
            "From File Image"});
            this.trainingData.Location = new System.Drawing.Point(341, 213);
            this.trainingData.Name = "trainingData";
            this.trainingData.Size = new System.Drawing.Size(147, 21);
            this.trainingData.TabIndex = 34;
            this.trainingData.Text = "Select Data Set";
            this.trainingData.SelectedIndexChanged += new System.EventHandler(this.trainingData_SelectedIndexChanged);
            // 
            // butPredictHmm
            // 
            this.butPredictHmm.Location = new System.Drawing.Point(341, 271);
            this.butPredictHmm.Name = "butPredictHmm";
            this.butPredictHmm.Size = new System.Drawing.Size(443, 23);
            this.butPredictHmm.TabIndex = 33;
            this.butPredictHmm.Text = "Predict";
            this.butPredictHmm.UseVisualStyleBackColor = true;
            this.butPredictHmm.Click += new System.EventHandler(this.butPredictHmm_Click);
            // 
            // butTrainHmm
            // 
            this.butTrainHmm.Location = new System.Drawing.Point(341, 242);
            this.butTrainHmm.Name = "butTrainHmm";
            this.butTrainHmm.Size = new System.Drawing.Size(443, 23);
            this.butTrainHmm.TabIndex = 32;
            this.butTrainHmm.Text = "Training";
            this.butTrainHmm.UseVisualStyleBackColor = true;
            this.butTrainHmm.Click += new System.EventHandler(this.butTrainHmm_Click);
            // 
            // numGamma
            // 
            this.numGamma.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.numGamma.DecimalPlaces = 16;
            this.numGamma.Location = new System.Drawing.Point(81, 311);
            this.numGamma.Name = "numGamma";
            this.numGamma.Size = new System.Drawing.Size(251, 20);
            this.numGamma.TabIndex = 36;
            this.numGamma.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.numGamma.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // numC
            // 
            this.numC.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.numC.DecimalPlaces = 16;
            this.numC.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numC.Location = new System.Drawing.Point(81, 285);
            this.numC.Name = "numC";
            this.numC.Size = new System.Drawing.Size(251, 20);
            this.numC.TabIndex = 37;
            this.numC.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.numC.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 287);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 13);
            this.label1.TabIndex = 38;
            this.label1.Text = "C ( cost )";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 313);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 13);
            this.label2.TabIndex = 38;
            this.label2.Text = "Gamma";
            // 
            // kernelType
            // 
            this.kernelType.FormattingEnabled = true;
            this.kernelType.Items.AddRange(new object[] {
            "RBF (Gaussian)",
            "Linier",
            "Polinomial",
            "Sigmoid"});
            this.kernelType.Location = new System.Drawing.Point(81, 258);
            this.kernelType.Name = "kernelType";
            this.kernelType.Size = new System.Drawing.Size(251, 21);
            this.kernelType.TabIndex = 39;
            this.kernelType.Text = "RBF (Gaussian)";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(11, 261);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(64, 13);
            this.label3.TabIndex = 38;
            this.label3.Text = "Kernel Type";
            // 
            // analisisBut
            // 
            this.analisisBut.Location = new System.Drawing.Point(341, 300);
            this.analisisBut.Name = "analisisBut";
            this.analisisBut.Size = new System.Drawing.Size(443, 23);
            this.analisisBut.TabIndex = 40;
            this.analisisBut.Text = "Analysis";
            this.analisisBut.UseVisualStyleBackColor = true;
            this.analisisBut.Click += new System.EventHandler(this.analisisBut_Click);
            // 
            // openDataDialog
            // 
            this.openDataDialog.Filter = "SVM Dataset |*.train";
            // 
            // buttonSaveTraining
            // 
            this.buttonSaveTraining.Location = new System.Drawing.Point(494, 212);
            this.buttonSaveTraining.Name = "buttonSaveTraining";
            this.buttonSaveTraining.Size = new System.Drawing.Size(290, 23);
            this.buttonSaveTraining.TabIndex = 41;
            this.buttonSaveTraining.Text = "Save Data Set";
            this.buttonSaveTraining.UseVisualStyleBackColor = true;
            this.buttonSaveTraining.Click += new System.EventHandler(this.buttonSaveTraining_Click);
            // 
            // saveDataDialog
            // 
            this.saveDataDialog.FileName = "training";
            this.saveDataDialog.Filter = "SVM Trining |*.train";
            // 
            // saveModelDialog
            // 
            this.saveModelDialog.Filter = "SVM Model (*.mdl)|*.mdl";
            // 
            // openModelDialog
            // 
            this.openModelDialog.FileName = "model";
            this.openModelDialog.Filter = "SVM Model (*.mdl)|*.mdl";
            // 
            // FormTrainingSvm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(798, 342);
            this.Controls.Add(this.buttonSaveTraining);
            this.Controls.Add(this.analisisBut);
            this.Controls.Add(this.kernelType);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.numC);
            this.Controls.Add(this.numGamma);
            this.Controls.Add(this.trainingData);
            this.Controls.Add(this.butPredictHmm);
            this.Controls.Add(this.butTrainHmm);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.pictureBox3);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "FormTrainingSvm";
            this.Text = "FormTrainingSvm";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.datasetSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numGamma)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numC)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView datasetSource;
        private System.Windows.Forms.DataGridViewTextBoxColumn Target;
        private System.Windows.Forms.DataGridViewTextBoxColumn Observation;
        private System.Windows.Forms.DataGridViewTextBoxColumn Result;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Recognition;
        private System.Windows.Forms.ComboBox trainingData;
        private System.Windows.Forms.Button butPredictHmm;
        private System.Windows.Forms.Button butTrainHmm;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.FolderBrowserDialog imageDirectory;
        private System.Windows.Forms.Button analisisBut;
        private System.Windows.Forms.OpenFileDialog openDataDialog;
        private System.Windows.Forms.Button buttonSaveTraining;
        private System.Windows.Forms.SaveFileDialog saveDataDialog;
        public System.Windows.Forms.NumericUpDown numGamma;
        public System.Windows.Forms.NumericUpDown numC;
        public System.Windows.Forms.ComboBox kernelType;
        private System.Windows.Forms.SaveFileDialog saveModelDialog;
        private System.Windows.Forms.OpenFileDialog openModelDialog;
    }
}