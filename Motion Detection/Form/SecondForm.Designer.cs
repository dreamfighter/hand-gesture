namespace Motion_Detection_v2
{
    partial class SecondForm
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
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.timerDiference = new System.Windows.Forms.Timer(this.components);
            this.timerCache = new System.Windows.Forms.Timer(this.components);
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabFiltering = new System.Windows.Forms.TabPage();
            this.butRecord = new System.Windows.Forms.Button();
            this.checkSaturation = new System.Windows.Forms.CheckBox();
            this.butcapture = new System.Windows.Forms.Button();
            this.trackHsvMax = new System.Windows.Forms.TrackBar();
            this.trackHsvMin = new System.Windows.Forms.TrackBar();
            this.label2 = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tabSvm = new System.Windows.Forms.TabPage();
            this.infoSvm = new System.Windows.Forms.TextBox();
            this.butTestingCreate = new System.Windows.Forms.Button();
            this.butPredictTesting = new System.Windows.Forms.Button();
            this.butCreateModel = new System.Windows.Forms.Button();
            this.butPredictTraining = new System.Windows.Forms.Button();
            this.butCreateTraining = new System.Windows.Forms.Button();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.tabHmm = new System.Windows.Forms.TabPage();
            this.butPredictHmm = new System.Windows.Forms.Button();
            this.butTrainHmm = new System.Windows.Forms.Button();
            this.pictureBox5 = new System.Windows.Forms.PictureBox();
            this.butTrainingCreateHmm = new System.Windows.Forms.Button();
            this.infoHmm = new System.Windows.Forms.TextBox();
            this.checkSave = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.mouseTimer = new System.Windows.Forms.Timer(this.components);
            this.checkPredict = new System.Windows.Forms.CheckBox();
            this.saveDirectory = new System.Windows.Forms.TextBox();
            this.butChooseDir = new System.Windows.Forms.Button();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.pictureBox6 = new System.Windows.Forms.PictureBox();
            this.pictureBox7 = new System.Windows.Forms.PictureBox();
            this.pictureBox8 = new System.Windows.Forms.PictureBox();
            this.recordTimer = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabFiltering.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackHsvMax)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackHsvMin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.tabSvm.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            this.tabHmm.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox8)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(22, 28);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(337, 282);
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // timerDiference
            // 
            this.timerDiference.Tick += new System.EventHandler(this.timerDiference_Tick);
            // 
            // timerCache
            // 
            this.timerCache.Enabled = true;
            this.timerCache.Interval = 400;
            this.timerCache.Tick += new System.EventHandler(this.timerCache_Tick);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabFiltering);
            this.tabControl1.Controls.Add(this.tabSvm);
            this.tabControl1.Controls.Add(this.tabHmm);
            this.tabControl1.Location = new System.Drawing.Point(-1, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(759, 460);
            this.tabControl1.TabIndex = 2;
            // 
            // tabFiltering
            // 
            this.tabFiltering.Controls.Add(this.butRecord);
            this.tabFiltering.Controls.Add(this.checkSaturation);
            this.tabFiltering.Controls.Add(this.butcapture);
            this.tabFiltering.Controls.Add(this.trackHsvMax);
            this.tabFiltering.Controls.Add(this.trackHsvMin);
            this.tabFiltering.Controls.Add(this.label2);
            this.tabFiltering.Controls.Add(this.pictureBox2);
            this.tabFiltering.Controls.Add(this.label1);
            this.tabFiltering.Controls.Add(this.pictureBox1);
            this.tabFiltering.Location = new System.Drawing.Point(4, 22);
            this.tabFiltering.Name = "tabFiltering";
            this.tabFiltering.Padding = new System.Windows.Forms.Padding(3);
            this.tabFiltering.Size = new System.Drawing.Size(751, 434);
            this.tabFiltering.TabIndex = 0;
            this.tabFiltering.Text = "Filtering";
            this.tabFiltering.UseVisualStyleBackColor = true;
            // 
            // butRecord
            // 
            this.butRecord.Location = new System.Drawing.Point(103, 316);
            this.butRecord.Name = "butRecord";
            this.butRecord.Size = new System.Drawing.Size(101, 23);
            this.butRecord.TabIndex = 13;
            this.butRecord.Text = "Record Video";
            this.butRecord.UseVisualStyleBackColor = true;
            this.butRecord.Click += new System.EventHandler(this.butRecord_Click);
            // 
            // checkSaturation
            // 
            this.checkSaturation.AutoSize = true;
            this.checkSaturation.Location = new System.Drawing.Point(312, 322);
            this.checkSaturation.Name = "checkSaturation";
            this.checkSaturation.Size = new System.Drawing.Size(74, 17);
            this.checkSaturation.TabIndex = 12;
            this.checkSaturation.Text = "Saturation";
            this.checkSaturation.UseVisualStyleBackColor = true;
            this.checkSaturation.CheckedChanged += new System.EventHandler(this.checkSaturation_CheckedChanged);
            // 
            // butcapture
            // 
            this.butcapture.Location = new System.Drawing.Point(22, 316);
            this.butcapture.Name = "butcapture";
            this.butcapture.Size = new System.Drawing.Size(75, 23);
            this.butcapture.TabIndex = 11;
            this.butcapture.Text = "Capture";
            this.butcapture.UseVisualStyleBackColor = true;
            this.butcapture.Click += new System.EventHandler(this.butcapture_Click);
            // 
            // trackHsvMax
            // 
            this.trackHsvMax.Location = new System.Drawing.Point(398, 367);
            this.trackHsvMax.Maximum = 255;
            this.trackHsvMax.Name = "trackHsvMax";
            this.trackHsvMax.Size = new System.Drawing.Size(337, 45);
            this.trackHsvMax.TabIndex = 9;
            // 
            // trackHsvMin
            // 
            this.trackHsvMin.Location = new System.Drawing.Point(398, 316);
            this.trackHsvMin.Maximum = 255;
            this.trackHsvMin.Name = "trackHsvMin";
            this.trackHsvMin.Size = new System.Drawing.Size(337, 45);
            this.trackHsvMin.TabIndex = 8;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(395, 10);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(114, 15);
            this.label2.TabIndex = 4;
            this.label2.Text = "Segmentasi HSV";
            // 
            // pictureBox2
            // 
            this.pictureBox2.Location = new System.Drawing.Point(398, 28);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(337, 282);
            this.pictureBox2.TabIndex = 3;
            this.pictureBox2.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(19, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 15);
            this.label1.TabIndex = 2;
            this.label1.Text = "Citra Asli";
            // 
            // tabSvm
            // 
            this.tabSvm.Controls.Add(this.infoSvm);
            this.tabSvm.Controls.Add(this.butTestingCreate);
            this.tabSvm.Controls.Add(this.butPredictTesting);
            this.tabSvm.Controls.Add(this.butCreateModel);
            this.tabSvm.Controls.Add(this.butPredictTraining);
            this.tabSvm.Controls.Add(this.butCreateTraining);
            this.tabSvm.Controls.Add(this.pictureBox4);
            this.tabSvm.Location = new System.Drawing.Point(4, 22);
            this.tabSvm.Name = "tabSvm";
            this.tabSvm.Padding = new System.Windows.Forms.Padding(3);
            this.tabSvm.Size = new System.Drawing.Size(751, 434);
            this.tabSvm.TabIndex = 1;
            this.tabSvm.Text = "SVM";
            this.tabSvm.UseVisualStyleBackColor = true;
            // 
            // infoSvm
            // 
            this.infoSvm.Location = new System.Drawing.Point(9, 148);
            this.infoSvm.Multiline = true;
            this.infoSvm.Name = "infoSvm";
            this.infoSvm.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.infoSvm.Size = new System.Drawing.Size(255, 116);
            this.infoSvm.TabIndex = 16;
            // 
            // butTestingCreate
            // 
            this.butTestingCreate.Location = new System.Drawing.Point(270, 6);
            this.butTestingCreate.Name = "butTestingCreate";
            this.butTestingCreate.Size = new System.Drawing.Size(136, 23);
            this.butTestingCreate.TabIndex = 14;
            this.butTestingCreate.Text = "Create Testing Data";
            this.butTestingCreate.UseVisualStyleBackColor = true;
            this.butTestingCreate.Click += new System.EventHandler(this.butTestingCreate_Click);
            // 
            // butPredictTesting
            // 
            this.butPredictTesting.Location = new System.Drawing.Point(115, 93);
            this.butPredictTesting.Name = "butPredictTesting";
            this.butPredictTesting.Size = new System.Drawing.Size(149, 23);
            this.butPredictTesting.TabIndex = 13;
            this.butPredictTesting.Text = "Predict Testing";
            this.butPredictTesting.UseVisualStyleBackColor = true;
            this.butPredictTesting.Click += new System.EventHandler(this.butPredictTesting_Click);
            // 
            // butCreateModel
            // 
            this.butCreateModel.Location = new System.Drawing.Point(115, 35);
            this.butCreateModel.Name = "butCreateModel";
            this.butCreateModel.Size = new System.Drawing.Size(149, 23);
            this.butCreateModel.TabIndex = 12;
            this.butCreateModel.Text = "Create Model SVM";
            this.butCreateModel.UseVisualStyleBackColor = true;
            this.butCreateModel.Click += new System.EventHandler(this.butCreateModel_Click);
            // 
            // butPredictTraining
            // 
            this.butPredictTraining.Location = new System.Drawing.Point(115, 64);
            this.butPredictTraining.Name = "butPredictTraining";
            this.butPredictTraining.Size = new System.Drawing.Size(149, 23);
            this.butPredictTraining.TabIndex = 11;
            this.butPredictTraining.Text = "Predict Training";
            this.butPredictTraining.UseVisualStyleBackColor = true;
            this.butPredictTraining.Click += new System.EventHandler(this.butPredictTraining_Click);
            // 
            // butCreateTraining
            // 
            this.butCreateTraining.Location = new System.Drawing.Point(115, 6);
            this.butCreateTraining.Name = "butCreateTraining";
            this.butCreateTraining.Size = new System.Drawing.Size(149, 23);
            this.butCreateTraining.TabIndex = 9;
            this.butCreateTraining.Text = "Create Training Data";
            this.butCreateTraining.UseVisualStyleBackColor = true;
            this.butCreateTraining.Click += new System.EventHandler(this.butCreateTraining_Click);
            // 
            // pictureBox4
            // 
            this.pictureBox4.Location = new System.Drawing.Point(9, 6);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(100, 100);
            this.pictureBox4.TabIndex = 8;
            this.pictureBox4.TabStop = false;
            // 
            // tabHmm
            // 
            this.tabHmm.Controls.Add(this.butPredictHmm);
            this.tabHmm.Controls.Add(this.butTrainHmm);
            this.tabHmm.Controls.Add(this.pictureBox5);
            this.tabHmm.Controls.Add(this.butTrainingCreateHmm);
            this.tabHmm.Controls.Add(this.infoHmm);
            this.tabHmm.Location = new System.Drawing.Point(4, 22);
            this.tabHmm.Name = "tabHmm";
            this.tabHmm.Size = new System.Drawing.Size(751, 434);
            this.tabHmm.TabIndex = 2;
            this.tabHmm.Text = "HMM";
            this.tabHmm.UseVisualStyleBackColor = true;
            // 
            // butPredictHmm
            // 
            this.butPredictHmm.Location = new System.Drawing.Point(119, 61);
            this.butPredictHmm.Name = "butPredictHmm";
            this.butPredictHmm.Size = new System.Drawing.Size(145, 23);
            this.butPredictHmm.TabIndex = 21;
            this.butPredictHmm.Text = "Predict Training";
            this.butPredictHmm.UseVisualStyleBackColor = true;
            this.butPredictHmm.Click += new System.EventHandler(this.butPredictHmm_Click);
            // 
            // butTrainHmm
            // 
            this.butTrainHmm.Location = new System.Drawing.Point(119, 32);
            this.butTrainHmm.Name = "butTrainHmm";
            this.butTrainHmm.Size = new System.Drawing.Size(145, 23);
            this.butTrainHmm.TabIndex = 20;
            this.butTrainHmm.Text = "Training";
            this.butTrainHmm.UseVisualStyleBackColor = true;
            this.butTrainHmm.Click += new System.EventHandler(this.butTrainHmm_Click);
            // 
            // pictureBox5
            // 
            this.pictureBox5.Location = new System.Drawing.Point(9, 3);
            this.pictureBox5.Name = "pictureBox5";
            this.pictureBox5.Size = new System.Drawing.Size(100, 102);
            this.pictureBox5.TabIndex = 19;
            this.pictureBox5.TabStop = false;
            // 
            // butTrainingCreateHmm
            // 
            this.butTrainingCreateHmm.Location = new System.Drawing.Point(119, 3);
            this.butTrainingCreateHmm.Name = "butTrainingCreateHmm";
            this.butTrainingCreateHmm.Size = new System.Drawing.Size(145, 23);
            this.butTrainingCreateHmm.TabIndex = 18;
            this.butTrainingCreateHmm.Text = "Create Training Data";
            this.butTrainingCreateHmm.UseVisualStyleBackColor = true;
            this.butTrainingCreateHmm.Click += new System.EventHandler(this.butTrainingHmm_Click);
            // 
            // infoHmm
            // 
            this.infoHmm.Location = new System.Drawing.Point(9, 111);
            this.infoHmm.Multiline = true;
            this.infoHmm.Name = "infoHmm";
            this.infoHmm.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.infoHmm.Size = new System.Drawing.Size(255, 116);
            this.infoHmm.TabIndex = 17;
            // 
            // checkSave
            // 
            this.checkSave.AutoSize = true;
            this.checkSave.Location = new System.Drawing.Point(764, 389);
            this.checkSave.Name = "checkSave";
            this.checkSave.Size = new System.Drawing.Size(83, 17);
            this.checkSave.TabIndex = 10;
            this.checkSave.Text = "Save Iamge";
            this.checkSave.UseVisualStyleBackColor = true;
            this.checkSave.CheckedChanged += new System.EventHandler(this.checkSave_CheckedChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(761, 14);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(112, 15);
            this.label3.TabIndex = 8;
            this.label3.Text = "Median Filtering";
            // 
            // pictureBox3
            // 
            this.pictureBox3.Location = new System.Drawing.Point(764, 32);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(337, 282);
            this.pictureBox3.TabIndex = 7;
            this.pictureBox3.TabStop = false;
            // 
            // mouseTimer
            // 
            this.mouseTimer.Tick += new System.EventHandler(this.mouseTimer_Tick);
            // 
            // checkPredict
            // 
            this.checkPredict.AutoSize = true;
            this.checkPredict.Location = new System.Drawing.Point(764, 320);
            this.checkPredict.Name = "checkPredict";
            this.checkPredict.Size = new System.Drawing.Size(59, 17);
            this.checkPredict.TabIndex = 15;
            this.checkPredict.Text = "Predict";
            this.checkPredict.UseVisualStyleBackColor = true;
            this.checkPredict.CheckedChanged += new System.EventHandler(this.checkPredict_CheckedChanged);
            // 
            // saveDirectory
            // 
            this.saveDirectory.Location = new System.Drawing.Point(764, 414);
            this.saveDirectory.Name = "saveDirectory";
            this.saveDirectory.Size = new System.Drawing.Size(167, 20);
            this.saveDirectory.TabIndex = 13;
            // 
            // butChooseDir
            // 
            this.butChooseDir.Location = new System.Drawing.Point(937, 412);
            this.butChooseDir.Name = "butChooseDir";
            this.butChooseDir.Size = new System.Drawing.Size(106, 23);
            this.butChooseDir.TabIndex = 16;
            this.butChooseDir.Text = "Choose Directory";
            this.butChooseDir.UseVisualStyleBackColor = true;
            this.butChooseDir.Click += new System.EventHandler(this.butChooseDir_Click);
            // 
            // pictureBox6
            // 
            this.pictureBox6.Location = new System.Drawing.Point(3, 462);
            this.pictureBox6.Name = "pictureBox6";
            this.pictureBox6.Size = new System.Drawing.Size(337, 282);
            this.pictureBox6.TabIndex = 17;
            this.pictureBox6.TabStop = false;
            // 
            // pictureBox7
            // 
            this.pictureBox7.Location = new System.Drawing.Point(346, 462);
            this.pictureBox7.Name = "pictureBox7";
            this.pictureBox7.Size = new System.Drawing.Size(337, 282);
            this.pictureBox7.TabIndex = 18;
            this.pictureBox7.TabStop = false;
            // 
            // pictureBox8
            // 
            this.pictureBox8.Location = new System.Drawing.Point(689, 462);
            this.pictureBox8.Name = "pictureBox8";
            this.pictureBox8.Size = new System.Drawing.Size(337, 282);
            this.pictureBox8.TabIndex = 19;
            this.pictureBox8.TabStop = false;
            // 
            // recordTimer
            // 
            this.recordTimer.Interval = 42;
            this.recordTimer.Tick += new System.EventHandler(this.recordTimer_Tick);
            // 
            // SecondForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(985, 668);
            this.Controls.Add(this.pictureBox8);
            this.Controls.Add(this.pictureBox7);
            this.Controls.Add(this.pictureBox6);
            this.Controls.Add(this.butChooseDir);
            this.Controls.Add(this.saveDirectory);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.checkSave);
            this.Controls.Add(this.checkPredict);
            this.Controls.Add(this.pictureBox3);
            this.Controls.Add(this.tabControl1);
            this.Name = "SecondForm";
            this.Text = "SecondForm";
            this.Load += new System.EventHandler(this.SecondForm_Load);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.SecondForm_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabFiltering.ResumeLayout(false);
            this.tabFiltering.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackHsvMax)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackHsvMin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.tabSvm.ResumeLayout(false);
            this.tabSvm.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            this.tabHmm.ResumeLayout(false);
            this.tabHmm.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox8)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Timer timerDiference;
        private System.Windows.Forms.Timer timerCache;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabFiltering;
        private System.Windows.Forms.TabPage tabSvm;
        private System.Windows.Forms.TabPage tabHmm;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TrackBar trackHsvMax;
        private System.Windows.Forms.TrackBar trackHsvMin;
        private System.Windows.Forms.CheckBox checkSave;
        private System.Windows.Forms.Button butCreateModel;
        private System.Windows.Forms.Button butPredictTraining;
        private System.Windows.Forms.Button butCreateTraining;
        private System.Windows.Forms.PictureBox pictureBox4;
        private System.Windows.Forms.Button butPredictTesting;
        private System.Windows.Forms.Button butcapture;
        private System.Windows.Forms.Button butTestingCreate;
        private System.Windows.Forms.TextBox infoSvm;
        private System.Windows.Forms.TextBox infoHmm;
        private System.Windows.Forms.Button butTrainingCreateHmm;
        private System.Windows.Forms.PictureBox pictureBox5;
        private System.Windows.Forms.Button butTrainHmm;
        private System.Windows.Forms.Button butPredictHmm;
        private System.Windows.Forms.CheckBox checkSaturation;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.Timer mouseTimer;
        private System.Windows.Forms.CheckBox checkPredict;
        private System.Windows.Forms.TextBox saveDirectory;
        private System.Windows.Forms.Button butChooseDir;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.PictureBox pictureBox6;
        private System.Windows.Forms.PictureBox pictureBox7;
        private System.Windows.Forms.PictureBox pictureBox8;
        private System.Windows.Forms.Button butRecord;
        private System.Windows.Forms.Timer recordTimer;
    }
}