namespace Motion_Detection_v2
{
    partial class FormTrainingHmm
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
            this.components = new System.ComponentModel.Container();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.sequenceSource = new System.Windows.Forms.DataGridView();
            this.Target = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Observation = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Result = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Recognition = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Probability = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.butPredictHmm = new System.Windows.Forms.Button();
            this.butTrainHmm = new System.Windows.Forms.Button();
            this.timerCapture = new System.Windows.Forms.Timer(this.components);
            this.timerRate = new System.Windows.Forms.Timer(this.components);
            this.imageDirectory = new System.Windows.Forms.FolderBrowserDialog();
            this.hmmType = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.stateCount = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.trainingData = new System.Windows.Forms.ComboBox();
            this.buttonSaveTraining = new System.Windows.Forms.Button();
            this.saveTrainingDialog = new System.Windows.Forms.SaveFileDialog();
            this.openTrainingDialog = new System.Windows.Forms.OpenFileDialog();
            this.saveModelDialog = new System.Windows.Forms.SaveFileDialog();
            this.openSvmModel = new System.Windows.Forms.OpenFileDialog();
            this.butOpenHmmModel = new System.Windows.Forms.Button();
            this.openModelDialog = new System.Windows.Forms.OpenFileDialog();
            this.butChooseDir = new System.Windows.Forms.Button();
            this.checkSave = new System.Windows.Forms.CheckBox();
            this.videoDirectory = new System.Windows.Forms.FolderBrowserDialog();
            this.analisisBut = new System.Windows.Forms.Button();
            this.timerTick = new System.Windows.Forms.Timer(this.components);
            this.label3 = new System.Windows.Forms.Label();
            this.dataCount = new System.Windows.Forms.NumericUpDown();
            this.savePredictDialog = new System.Windows.Forms.SaveFileDialog();
            this.intervalNum = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.svmParams = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sequenceSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stateCount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataCount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.intervalNum)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox3
            // 
            this.pictureBox3.Location = new System.Drawing.Point(12, 12);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(320, 240);
            this.pictureBox3.TabIndex = 8;
            this.pictureBox3.TabStop = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.sequenceSource);
            this.groupBox1.Location = new System.Drawing.Point(356, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(449, 193);
            this.groupBox1.TabIndex = 20;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Training Result";
            // 
            // sequenceSource
            // 
            this.sequenceSource.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.sequenceSource.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Target,
            this.Observation,
            this.Result,
            this.Recognition,
            this.Probability});
            this.sequenceSource.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sequenceSource.Location = new System.Drawing.Point(3, 16);
            this.sequenceSource.Name = "sequenceSource";
            this.sequenceSource.Size = new System.Drawing.Size(443, 174);
            this.sequenceSource.TabIndex = 1;
            this.sequenceSource.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.sequenceSource_CellClick);
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
            // Probability
            // 
            this.Probability.HeaderText = "Probability";
            this.Probability.Name = "Probability";
            // 
            // butPredictHmm
            // 
            this.butPredictHmm.Location = new System.Drawing.Point(361, 269);
            this.butPredictHmm.Name = "butPredictHmm";
            this.butPredictHmm.Size = new System.Drawing.Size(441, 23);
            this.butPredictHmm.TabIndex = 24;
            this.butPredictHmm.Text = "Predict";
            this.butPredictHmm.UseVisualStyleBackColor = true;
            this.butPredictHmm.Click += new System.EventHandler(this.butPredictHmm_Click);
            // 
            // butTrainHmm
            // 
            this.butTrainHmm.Location = new System.Drawing.Point(361, 240);
            this.butTrainHmm.Name = "butTrainHmm";
            this.butTrainHmm.Size = new System.Drawing.Size(441, 23);
            this.butTrainHmm.TabIndex = 23;
            this.butTrainHmm.Text = "Training";
            this.butTrainHmm.UseVisualStyleBackColor = true;
            this.butTrainHmm.Click += new System.EventHandler(this.butTrainHmm_Click);
            // 
            // timerCapture
            // 
            this.timerCapture.Interval = 24;
            this.timerCapture.Tick += new System.EventHandler(this.timerCapture_Tick);
            // 
            // timerRate
            // 
            this.timerRate.Interval = 42;
            this.timerRate.Tick += new System.EventHandler(this.timerRate_Tick);
            // 
            // hmmType
            // 
            this.hmmType.FormattingEnabled = true;
            this.hmmType.Items.AddRange(new object[] {
            "Ergodic",
            "Left-Right",
            "Parallel Left-Right",
            "Full Left-Right"});
            this.hmmType.Location = new System.Drawing.Point(77, 316);
            this.hmmType.Name = "hmmType";
            this.hmmType.Size = new System.Drawing.Size(255, 21);
            this.hmmType.TabIndex = 28;
            this.hmmType.Text = "Ergodic";
            this.hmmType.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 319);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 13);
            this.label1.TabIndex = 29;
            this.label1.Text = "Type HMM";
            // 
            // stateCount
            // 
            this.stateCount.Location = new System.Drawing.Point(78, 343);
            this.stateCount.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.stateCount.Name = "stateCount";
            this.stateCount.Size = new System.Drawing.Size(254, 20);
            this.stateCount.TabIndex = 30;
            this.stateCount.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 345);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 13);
            this.label2.TabIndex = 29;
            this.label2.Text = "State Count";
            // 
            // trainingData
            // 
            this.trainingData.FormattingEnabled = true;
            this.trainingData.Items.AddRange(new object[] {
            "Select Data Set",
            "From File Sequence",
            "From File Video",
            "From Folder Images"});
            this.trainingData.Location = new System.Drawing.Point(361, 211);
            this.trainingData.Name = "trainingData";
            this.trainingData.Size = new System.Drawing.Size(145, 21);
            this.trainingData.TabIndex = 31;
            this.trainingData.Text = "Select Training Set";
            this.trainingData.SelectedIndexChanged += new System.EventHandler(this.trainingData_SelectedIndexChanged);
            // 
            // buttonSaveTraining
            // 
            this.buttonSaveTraining.Location = new System.Drawing.Point(512, 210);
            this.buttonSaveTraining.Name = "buttonSaveTraining";
            this.buttonSaveTraining.Size = new System.Drawing.Size(290, 23);
            this.buttonSaveTraining.TabIndex = 32;
            this.buttonSaveTraining.Text = "Save Data Set";
            this.buttonSaveTraining.UseVisualStyleBackColor = true;
            this.buttonSaveTraining.Click += new System.EventHandler(this.buttonSaveTraining_Click);
            // 
            // saveTrainingDialog
            // 
            this.saveTrainingDialog.Filter = "HMM Sequence |*.sequence";
            // 
            // openTrainingDialog
            // 
            this.openTrainingDialog.Filter = "HMM Sequence |*.sequence";
            // 
            // saveModelDialog
            // 
            this.saveModelDialog.Filter = "HMM Model |*.hmm";
            // 
            // openSvmModel
            // 
            this.openSvmModel.Filter = "SVM Model (*.svm,*.mdl)|*.mdl||*.svm";
            // 
            // butOpenHmmModel
            // 
            this.butOpenHmmModel.Location = new System.Drawing.Point(12, 287);
            this.butOpenHmmModel.Name = "butOpenHmmModel";
            this.butOpenHmmModel.Size = new System.Drawing.Size(320, 23);
            this.butOpenHmmModel.TabIndex = 35;
            this.butOpenHmmModel.Text = "model/temp.hmm";
            this.butOpenHmmModel.UseVisualStyleBackColor = true;
            this.butOpenHmmModel.Click += new System.EventHandler(this.butOpenHmmModel_Click);
            // 
            // openModelDialog
            // 
            this.openModelDialog.Filter = "HMM Model |*.hmm";
            // 
            // butChooseDir
            // 
            this.butChooseDir.Location = new System.Drawing.Point(450, 327);
            this.butChooseDir.Name = "butChooseDir";
            this.butChooseDir.Size = new System.Drawing.Size(352, 23);
            this.butChooseDir.TabIndex = 37;
            this.butChooseDir.Text = "image";
            this.butChooseDir.UseVisualStyleBackColor = true;
            this.butChooseDir.Click += new System.EventHandler(this.butChooseDir_Click);
            // 
            // checkSave
            // 
            this.checkSave.AutoSize = true;
            this.checkSave.Location = new System.Drawing.Point(361, 327);
            this.checkSave.Name = "checkSave";
            this.checkSave.Size = new System.Drawing.Size(83, 17);
            this.checkSave.TabIndex = 36;
            this.checkSave.Text = "Save Image";
            this.checkSave.UseVisualStyleBackColor = true;
            // 
            // analisisBut
            // 
            this.analisisBut.Location = new System.Drawing.Point(361, 298);
            this.analisisBut.Name = "analisisBut";
            this.analisisBut.Size = new System.Drawing.Size(441, 23);
            this.analisisBut.TabIndex = 38;
            this.analisisBut.Text = "Analysis";
            this.analisisBut.UseVisualStyleBackColor = true;
            this.analisisBut.Click += new System.EventHandler(this.analisisBut_Click);
            // 
            // timerTick
            // 
            this.timerTick.Interval = 42;
            this.timerTick.Tick += new System.EventHandler(this.timerTick_Tick);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 373);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(61, 13);
            this.label3.TabIndex = 29;
            this.label3.Text = "Data Count";
            // 
            // dataCount
            // 
            this.dataCount.Location = new System.Drawing.Point(79, 371);
            this.dataCount.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.dataCount.Name = "dataCount";
            this.dataCount.Size = new System.Drawing.Size(253, 20);
            this.dataCount.TabIndex = 39;
            this.dataCount.Value = new decimal(new int[] {
            30,
            0,
            0,
            0});
            // 
            // savePredictDialog
            // 
            this.savePredictDialog.Filter = "Predict Hmm (*.predict)|*.predict";
            // 
            // intervalNum
            // 
            this.intervalNum.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.intervalNum.Location = new System.Drawing.Point(450, 366);
            this.intervalNum.Maximum = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.intervalNum.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.intervalNum.Name = "intervalNum";
            this.intervalNum.Size = new System.Drawing.Size(352, 20);
            this.intervalNum.TabIndex = 41;
            this.intervalNum.Value = new decimal(new int[] {
            30,
            0,
            0,
            0});
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(358, 371);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(42, 13);
            this.label4.TabIndex = 29;
            this.label4.Text = "Interval";
            // 
            // svmParams
            // 
            this.svmParams.FormattingEnabled = true;
            this.svmParams.Location = new System.Drawing.Point(77, 260);
            this.svmParams.Name = "svmParams";
            this.svmParams.Size = new System.Drawing.Size(255, 21);
            this.svmParams.TabIndex = 42;
            this.svmParams.Text = "temp.mdl";
            this.svmParams.SelectedIndexChanged += new System.EventHandler(this.svmParams_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(9, 263);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(62, 13);
            this.label5.TabIndex = 29;
            this.label5.Text = "SVM Model";
            // 
            // FormTrainingHmm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(817, 399);
            this.Controls.Add(this.svmParams);
            this.Controls.Add(this.intervalNum);
            this.Controls.Add(this.dataCount);
            this.Controls.Add(this.analisisBut);
            this.Controls.Add(this.butChooseDir);
            this.Controls.Add(this.checkSave);
            this.Controls.Add(this.butOpenHmmModel);
            this.Controls.Add(this.buttonSaveTraining);
            this.Controls.Add(this.trainingData);
            this.Controls.Add(this.stateCount);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.hmmType);
            this.Controls.Add(this.butPredictHmm);
            this.Controls.Add(this.butTrainHmm);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.pictureBox3);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "FormTrainingHmm";
            this.Text = "Motion Reconition";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.sequenceSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stateCount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataCount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.intervalNum)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView sequenceSource;
        private System.Windows.Forms.Button butPredictHmm;
        private System.Windows.Forms.Button butTrainHmm;
        private System.Windows.Forms.Timer timerCapture;
        private System.Windows.Forms.Timer timerRate;
        private System.Windows.Forms.FolderBrowserDialog imageDirectory;
        private System.Windows.Forms.ComboBox hmmType;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown stateCount;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox trainingData;
        private System.Windows.Forms.Button buttonSaveTraining;
        private System.Windows.Forms.SaveFileDialog saveTrainingDialog;
        private System.Windows.Forms.OpenFileDialog openTrainingDialog;
        private System.Windows.Forms.SaveFileDialog saveModelDialog;
        private System.Windows.Forms.OpenFileDialog openSvmModel;
        private System.Windows.Forms.Button butOpenHmmModel;
        private System.Windows.Forms.OpenFileDialog openModelDialog;
        private System.Windows.Forms.Button butChooseDir;
        private System.Windows.Forms.CheckBox checkSave;
        private System.Windows.Forms.FolderBrowserDialog videoDirectory;
        private System.Windows.Forms.Button analisisBut;
        private System.Windows.Forms.Timer timerTick;
        private System.Windows.Forms.DataGridViewTextBoxColumn Target;
        private System.Windows.Forms.DataGridViewTextBoxColumn Observation;
        private System.Windows.Forms.DataGridViewTextBoxColumn Result;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Recognition;
        private System.Windows.Forms.DataGridViewTextBoxColumn Probability;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown dataCount;
        private System.Windows.Forms.SaveFileDialog savePredictDialog;
        private System.Windows.Forms.NumericUpDown intervalNum;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox svmParams;
        private System.Windows.Forms.Label label5;
    }
}