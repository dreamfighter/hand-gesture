namespace Motion_Detection_v2
{
    partial class RecognitionV2
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RecognitionV2));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.ListOfDevices = new System.Windows.Forms.ComboBox();
            this.intervalNum = new System.Windows.Forms.NumericUpDown();
            this.checkSaturation = new System.Windows.Forms.CheckBox();
            this.trackHsvMax = new System.Windows.Forms.TrackBar();
            this.trackHsvMin = new System.Windows.Forms.TrackBar();
            this.previewCheck = new System.Windows.Forms.CheckBox();
            this.hmmParams = new System.Windows.Forms.ComboBox();
            this.svmParams = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.menuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadConfigToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openConfigToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.configToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mouseEnableToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.trackingEnableToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.actionEnableToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.sVMToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hMMToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.captureVideoToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.seccondFormToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mainFormToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.faceDetectionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.depthImageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.originalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.filteringToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.extractionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.redusingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.timerObserve = new System.Windows.Forms.Timer(this.components);
            this.timerCapture = new System.Windows.Forms.Timer(this.components);
            this.timerGesture = new System.Windows.Forms.Timer(this.components);
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.recognitionBut = new System.Windows.Forms.Button();
            this.saveConfigDialog = new System.Windows.Forms.SaveFileDialog();
            this.openConfigDialog = new System.Windows.Forms.OpenFileDialog();
            this.recordVideoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.intervalNum)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackHsvMax)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackHsvMin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.checkBox1);
            this.groupBox1.Controls.Add(this.ListOfDevices);
            this.groupBox1.Controls.Add(this.intervalNum);
            this.groupBox1.Controls.Add(this.checkSaturation);
            this.groupBox1.Controls.Add(this.trackHsvMax);
            this.groupBox1.Controls.Add(this.trackHsvMin);
            this.groupBox1.Controls.Add(this.previewCheck);
            this.groupBox1.Controls.Add(this.hmmParams);
            this.groupBox1.Controls.Add(this.svmParams);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(10, 32);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(326, 321);
            this.groupBox1.TabIndex = 23;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Setting Parameter";
            // 
            // ListOfDevices
            // 
            this.ListOfDevices.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.ListOfDevices.FormattingEnabled = true;
            this.ListOfDevices.Location = new System.Drawing.Point(109, 252);
            this.ListOfDevices.Name = "ListOfDevices";
            this.ListOfDevices.Size = new System.Drawing.Size(202, 24);
            this.ListOfDevices.TabIndex = 30;
            // 
            // intervalNum
            // 
            this.intervalNum.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.intervalNum.Location = new System.Drawing.Point(109, 210);
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
            this.intervalNum.Size = new System.Drawing.Size(202, 30);
            this.intervalNum.TabIndex = 26;
            this.intervalNum.Value = new decimal(new int[] {
            30,
            0,
            0,
            0});
            // 
            // checkSaturation
            // 
            this.checkSaturation.AutoSize = true;
            this.checkSaturation.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.checkSaturation.Location = new System.Drawing.Point(10, 30);
            this.checkSaturation.Name = "checkSaturation";
            this.checkSaturation.Size = new System.Drawing.Size(82, 19);
            this.checkSaturation.TabIndex = 29;
            this.checkSaturation.Text = "Saturation";
            this.checkSaturation.UseVisualStyleBackColor = true;
            this.checkSaturation.CheckedChanged += new System.EventHandler(this.checkSaturation_CheckedChanged);
            // 
            // trackHsvMax
            // 
            this.trackHsvMax.LargeChange = 1;
            this.trackHsvMax.Location = new System.Drawing.Point(10, 91);
            this.trackHsvMax.Maximum = 255;
            this.trackHsvMax.Name = "trackHsvMax";
            this.trackHsvMax.Size = new System.Drawing.Size(301, 45);
            this.trackHsvMax.TabIndex = 28;
            // 
            // trackHsvMin
            // 
            this.trackHsvMin.Location = new System.Drawing.Point(10, 56);
            this.trackHsvMin.Maximum = 255;
            this.trackHsvMin.Name = "trackHsvMin";
            this.trackHsvMin.Size = new System.Drawing.Size(301, 45);
            this.trackHsvMin.TabIndex = 27;
            // 
            // previewCheck
            // 
            this.previewCheck.AutoSize = true;
            this.previewCheck.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.previewCheck.Location = new System.Drawing.Point(10, 289);
            this.previewCheck.Name = "previewCheck";
            this.previewCheck.Size = new System.Drawing.Size(76, 21);
            this.previewCheck.TabIndex = 26;
            this.previewCheck.Text = "Preview";
            this.previewCheck.UseVisualStyleBackColor = true;
            this.previewCheck.CheckedChanged += new System.EventHandler(this.previewCheck_CheckedChanged);
            // 
            // hmmParams
            // 
            this.hmmParams.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.hmmParams.FormattingEnabled = true;
            this.hmmParams.Items.AddRange(new object[] {
            "Select Parameter"});
            this.hmmParams.Location = new System.Drawing.Point(109, 175);
            this.hmmParams.Name = "hmmParams";
            this.hmmParams.Size = new System.Drawing.Size(202, 24);
            this.hmmParams.TabIndex = 24;
            this.hmmParams.Text = "Select Parameter";
            // 
            // svmParams
            // 
            this.svmParams.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.svmParams.FormattingEnabled = true;
            this.svmParams.Items.AddRange(new object[] {
            "Select Parameter"});
            this.svmParams.Location = new System.Drawing.Point(109, 142);
            this.svmParams.Name = "svmParams";
            this.svmParams.Size = new System.Drawing.Size(202, 24);
            this.svmParams.TabIndex = 24;
            this.svmParams.Text = "Select Parameter";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label4.Location = new System.Drawing.Point(6, 252);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(72, 20);
            this.label4.TabIndex = 0;
            this.label4.Text = "Webcam";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label3.Location = new System.Drawing.Point(6, 210);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(61, 20);
            this.label3.TabIndex = 0;
            this.label3.Text = "Interval";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label2.Location = new System.Drawing.Point(6, 179);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(108, 20);
            this.label2.TabIndex = 0;
            this.label2.Text = "HMM MODEL";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label1.Location = new System.Drawing.Point(6, 145);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(105, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "SVM MODEL";
            // 
            // pictureBox3
            // 
            this.pictureBox3.Location = new System.Drawing.Point(351, 44);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(320, 240);
            this.pictureBox3.TabIndex = 24;
            this.pictureBox3.TabStop = false;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuToolStripMenuItem,
            this.configToolStripMenuItem,
            this.viewToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(683, 24);
            this.menuStrip1.TabIndex = 25;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // menuToolStripMenuItem
            // 
            this.menuToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadConfigToolStripMenuItem,
            this.openConfigToolStripMenuItem});
            this.menuToolStripMenuItem.Name = "menuToolStripMenuItem";
            this.menuToolStripMenuItem.Size = new System.Drawing.Size(50, 20);
            this.menuToolStripMenuItem.Text = "Menu";
            // 
            // loadConfigToolStripMenuItem
            // 
            this.loadConfigToolStripMenuItem.Name = "loadConfigToolStripMenuItem";
            this.loadConfigToolStripMenuItem.Size = new System.Drawing.Size(143, 22);
            this.loadConfigToolStripMenuItem.Text = "Save Setting";
            this.loadConfigToolStripMenuItem.Click += new System.EventHandler(this.loadConfigToolStripMenuItem_Click);
            // 
            // openConfigToolStripMenuItem
            // 
            this.openConfigToolStripMenuItem.Name = "openConfigToolStripMenuItem";
            this.openConfigToolStripMenuItem.Size = new System.Drawing.Size(143, 22);
            this.openConfigToolStripMenuItem.Text = "Open Setting";
            this.openConfigToolStripMenuItem.Click += new System.EventHandler(this.openConfigToolStripMenuItem_Click);
            // 
            // configToolStripMenuItem
            // 
            this.configToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mouseEnableToolStripMenuItem,
            this.toolStripMenuItem1,
            this.sVMToolStripMenuItem,
            this.hMMToolStripMenuItem,
            this.captureVideoToolStripMenuItem1,
            this.seccondFormToolStripMenuItem,
            this.mainFormToolStripMenuItem,
            this.faceDetectionToolStripMenuItem,
            this.depthImageToolStripMenuItem,
            this.recordVideoToolStripMenuItem});
            this.configToolStripMenuItem.Name = "configToolStripMenuItem";
            this.configToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
            this.configToolStripMenuItem.Text = "Edit";
            // 
            // mouseEnableToolStripMenuItem
            // 
            this.mouseEnableToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.trackingEnableToolStripMenuItem,
            this.actionEnableToolStripMenuItem});
            this.mouseEnableToolStripMenuItem.Name = "mouseEnableToolStripMenuItem";
            this.mouseEnableToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.mouseEnableToolStripMenuItem.Text = "Mouse";
            // 
            // trackingEnableToolStripMenuItem
            // 
            this.trackingEnableToolStripMenuItem.Name = "trackingEnableToolStripMenuItem";
            this.trackingEnableToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.trackingEnableToolStripMenuItem.Text = "Tracking Enable";
            this.trackingEnableToolStripMenuItem.Click += new System.EventHandler(this.trackingEnableToolStripMenuItem_Click);
            // 
            // actionEnableToolStripMenuItem
            // 
            this.actionEnableToolStripMenuItem.Name = "actionEnableToolStripMenuItem";
            this.actionEnableToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.actionEnableToolStripMenuItem.Text = "Action Enable";
            this.actionEnableToolStripMenuItem.Click += new System.EventHandler(this.actionEnableToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(149, 6);
            // 
            // sVMToolStripMenuItem
            // 
            this.sVMToolStripMenuItem.Name = "sVMToolStripMenuItem";
            this.sVMToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.sVMToolStripMenuItem.Text = "SVM";
            this.sVMToolStripMenuItem.Click += new System.EventHandler(this.sVMToolStripMenuItem_Click);
            // 
            // hMMToolStripMenuItem
            // 
            this.hMMToolStripMenuItem.Name = "hMMToolStripMenuItem";
            this.hMMToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.hMMToolStripMenuItem.Text = "HMM";
            this.hMMToolStripMenuItem.Click += new System.EventHandler(this.hMMToolStripMenuItem_Click);
            // 
            // captureVideoToolStripMenuItem1
            // 
            this.captureVideoToolStripMenuItem1.Name = "captureVideoToolStripMenuItem1";
            this.captureVideoToolStripMenuItem1.Size = new System.Drawing.Size(152, 22);
            this.captureVideoToolStripMenuItem1.Text = "Capture Video";
            this.captureVideoToolStripMenuItem1.Click += new System.EventHandler(this.captureVideoToolStripMenuItem1_Click);
            // 
            // seccondFormToolStripMenuItem
            // 
            this.seccondFormToolStripMenuItem.Name = "seccondFormToolStripMenuItem";
            this.seccondFormToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.seccondFormToolStripMenuItem.Text = "Seccond Form";
            this.seccondFormToolStripMenuItem.Click += new System.EventHandler(this.seccondFormToolStripMenuItem_Click);
            // 
            // mainFormToolStripMenuItem
            // 
            this.mainFormToolStripMenuItem.Name = "mainFormToolStripMenuItem";
            this.mainFormToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.mainFormToolStripMenuItem.Text = "Main Form";
            this.mainFormToolStripMenuItem.Click += new System.EventHandler(this.mainFormToolStripMenuItem_Click);
            // 
            // faceDetectionToolStripMenuItem
            // 
            this.faceDetectionToolStripMenuItem.Name = "faceDetectionToolStripMenuItem";
            this.faceDetectionToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.faceDetectionToolStripMenuItem.Text = "Face Detection";
            this.faceDetectionToolStripMenuItem.Click += new System.EventHandler(this.faceDetectionToolStripMenuItem_Click);
            // 
            // depthImageToolStripMenuItem
            // 
            this.depthImageToolStripMenuItem.Name = "depthImageToolStripMenuItem";
            this.depthImageToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.depthImageToolStripMenuItem.Text = "Depth Image";
            this.depthImageToolStripMenuItem.Click += new System.EventHandler(this.depthImageToolStripMenuItem_Click);
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.originalToolStripMenuItem,
            this.filteringToolStripMenuItem,
            this.extractionToolStripMenuItem,
            this.redusingToolStripMenuItem});
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.viewToolStripMenuItem.Text = "View";
            // 
            // originalToolStripMenuItem
            // 
            this.originalToolStripMenuItem.Checked = true;
            this.originalToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.originalToolStripMenuItem.Name = "originalToolStripMenuItem";
            this.originalToolStripMenuItem.Size = new System.Drawing.Size(126, 22);
            this.originalToolStripMenuItem.Text = "Original";
            this.originalToolStripMenuItem.Click += new System.EventHandler(this.originalToolStripMenuItem_Click);
            // 
            // filteringToolStripMenuItem
            // 
            this.filteringToolStripMenuItem.Name = "filteringToolStripMenuItem";
            this.filteringToolStripMenuItem.Size = new System.Drawing.Size(126, 22);
            this.filteringToolStripMenuItem.Text = "Filtering";
            this.filteringToolStripMenuItem.Click += new System.EventHandler(this.filteringToolStripMenuItem_Click);
            // 
            // extractionToolStripMenuItem
            // 
            this.extractionToolStripMenuItem.Name = "extractionToolStripMenuItem";
            this.extractionToolStripMenuItem.Size = new System.Drawing.Size(126, 22);
            this.extractionToolStripMenuItem.Text = "Extraction";
            this.extractionToolStripMenuItem.Click += new System.EventHandler(this.extractionToolStripMenuItem_Click);
            // 
            // redusingToolStripMenuItem
            // 
            this.redusingToolStripMenuItem.Name = "redusingToolStripMenuItem";
            this.redusingToolStripMenuItem.Size = new System.Drawing.Size(126, 22);
            this.redusingToolStripMenuItem.Text = "Redusing";
            // 
            // timerObserve
            // 
            this.timerObserve.Interval = 30;
            this.timerObserve.Tick += new System.EventHandler(this.timerObserve_Tick);
            // 
            // timerCapture
            // 
            this.timerCapture.Interval = 30;
            this.timerCapture.Tick += new System.EventHandler(this.capture_Tick);
            // 
            // timerGesture
            // 
            this.timerGesture.Interval = 30;
            this.timerGesture.Tick += new System.EventHandler(this.timerGesture_Tick);
            // 
            // trackBar1
            // 
            this.trackBar1.LargeChange = 1;
            this.trackBar1.Location = new System.Drawing.Point(351, 287);
            this.trackBar1.Maximum = 27;
            this.trackBar1.Minimum = 1;
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(320, 45);
            this.trackBar1.TabIndex = 26;
            this.trackBar1.Value = 1;
            // 
            // recognitionBut
            // 
            this.recognitionBut.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.recognitionBut.Location = new System.Drawing.Point(351, 318);
            this.recognitionBut.Name = "recognitionBut";
            this.recognitionBut.Size = new System.Drawing.Size(320, 37);
            this.recognitionBut.TabIndex = 27;
            this.recognitionBut.Text = "Start Recognition";
            this.recognitionBut.UseVisualStyleBackColor = true;
            this.recognitionBut.Click += new System.EventHandler(this.recognitionBut_Click_1);
            // 
            // saveConfigDialog
            // 
            this.saveConfigDialog.FileName = "setting";
            this.saveConfigDialog.Filter = "Setting Files (*.stt)|*.stt";
            // 
            // openConfigDialog
            // 
            this.openConfigDialog.FileName = "openFileDialog1";
            // 
            // recordVideoToolStripMenuItem
            // 
            this.recordVideoToolStripMenuItem.Name = "recordVideoToolStripMenuItem";
            this.recordVideoToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.recordVideoToolStripMenuItem.Text = "Record Video";
            this.recordVideoToolStripMenuItem.Click += new System.EventHandler(this.recordVideoToolStripMenuItem_Click);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.checkBox1.Location = new System.Drawing.Point(109, 289);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(116, 21);
            this.checkBox1.TabIndex = 31;
            this.checkBox1.Text = "Using Camera";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // RecognitionV2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(683, 367);
            this.Controls.Add(this.recognitionBut);
            this.Controls.Add(this.trackBar1);
            this.Controls.Add(this.pictureBox3);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "RecognitionV2";
            this.Text = "Recognition v2";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Recognition_FormClosed);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.intervalNum)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackHsvMax)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackHsvMin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox hmmParams;
        private System.Windows.Forms.ComboBox svmParams;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox previewCheck;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.CheckBox checkSaturation;
        private System.Windows.Forms.TrackBar trackHsvMax;
        private System.Windows.Forms.TrackBar trackHsvMin;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem menuToolStripMenuItem;
        private System.Windows.Forms.Timer timerObserve;
        private System.Windows.Forms.Timer timerCapture;
        private System.Windows.Forms.NumericUpDown intervalNum;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Timer timerGesture;
        private System.Windows.Forms.TrackBar trackBar1;
        private System.Windows.Forms.Button recognitionBut;
        private System.Windows.Forms.ToolStripMenuItem configToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sVMToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem hMMToolStripMenuItem;
        private System.Windows.Forms.ComboBox ListOfDevices;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ToolStripMenuItem mouseEnableToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadConfigToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openConfigToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem captureVideoToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem seccondFormToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem originalToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem filteringToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem extractionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem redusingToolStripMenuItem;
        private System.Windows.Forms.SaveFileDialog saveConfigDialog;
        private System.Windows.Forms.OpenFileDialog openConfigDialog;
        private System.Windows.Forms.ToolStripMenuItem trackingEnableToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem actionEnableToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mainFormToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem faceDetectionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem depthImageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem recordVideoToolStripMenuItem;
        private System.Windows.Forms.CheckBox checkBox1;
    }
}