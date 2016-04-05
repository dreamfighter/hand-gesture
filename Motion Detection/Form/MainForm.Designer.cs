namespace Motion_Detection_v2
{
    partial class MainForm
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
            this.captureButton = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.button1 = new System.Windows.Forms.Button();
            this.dilateCheck = new System.Windows.Forms.CheckBox();
            this.blurCheck = new System.Windows.Forms.CheckBox();
            this.CannyCheck = new System.Windows.Forms.CheckBox();
            this.trackDilate = new System.Windows.Forms.TrackBar();
            this.trackBlur = new System.Windows.Forms.TrackBar();
            this.trackSkinMin = new System.Windows.Forms.TrackBar();
            this.trackSkinMax = new System.Windows.Forms.TrackBar();
            this.trackBarMinVal = new System.Windows.Forms.TextBox();
            this.trackBarMaxVal = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.trackBarLapace = new System.Windows.Forms.TrackBar();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.radioButton5 = new System.Windows.Forms.RadioButton();
            this.radioButton4 = new System.Windows.Forms.RadioButton();
            this.radioButton3 = new System.Windows.Forms.RadioButton();
            this.hsvFilter = new System.Windows.Forms.CheckBox();
            this.ycrcbFilter = new System.Windows.Forms.CheckBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.trackCbMaxVal = new System.Windows.Forms.TextBox();
            this.trackCbMinVal = new System.Windows.Forms.TextBox();
            this.trackCrMaxVal = new System.Windows.Forms.TextBox();
            this.trackCbMax = new System.Windows.Forms.TrackBar();
            this.trackCrMinVal = new System.Windows.Forms.TextBox();
            this.trackCbMin = new System.Windows.Forms.TrackBar();
            this.trackCrMax = new System.Windows.Forms.TrackBar();
            this.trackCrMin = new System.Windows.Forms.TrackBar();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.info = new System.Windows.Forms.TextBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.checkPredict = new System.Windows.Forms.CheckBox();
            this.classLabel = new System.Windows.Forms.ComboBox();
            this.TestingBut = new System.Windows.Forms.Button();
            this.predictBut = new System.Windows.Forms.Button();
            this.trainingBut = new System.Windows.Forms.Button();
            this.checkTrainData = new System.Windows.Forms.CheckBox();
            this.checkTestingData = new System.Windows.Forms.CheckBox();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.butHmmTesting = new System.Windows.Forms.Button();
            this.butHmmLearning = new System.Windows.Forms.Button();
            this.checkPredictHmm = new System.Windows.Forms.CheckBox();
            this.originalPicture = new System.Windows.Forms.PictureBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.openFileButton = new System.Windows.Forms.Button();
            this.fileNameTextBox = new System.Windows.Forms.TextBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackDilate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBlur)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackSkinMin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackSkinMax)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarLapace)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackCbMax)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackCbMin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackCrMax)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackCrMin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.groupBox5.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.originalPicture)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(3, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(332, 309);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // captureButton
            // 
            this.captureButton.Location = new System.Drawing.Point(842, 407);
            this.captureButton.Name = "captureButton";
            this.captureButton.Size = new System.Drawing.Size(75, 23);
            this.captureButton.TabIndex = 1;
            this.captureButton.Text = "Capture";
            this.captureButton.UseVisualStyleBackColor = true;
            this.captureButton.Click += new System.EventHandler(this.captureButton_Click);
            // 
            // timer1
            // 
            this.timer1.Interval = 30;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(745, 407);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(91, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "GetSkinColor";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // dilateCheck
            // 
            this.dilateCheck.AutoSize = true;
            this.dilateCheck.Location = new System.Drawing.Point(19, 19);
            this.dilateCheck.Name = "dilateCheck";
            this.dilateCheck.Size = new System.Drawing.Size(53, 17);
            this.dilateCheck.TabIndex = 3;
            this.dilateCheck.Text = "Dilate";
            this.dilateCheck.UseVisualStyleBackColor = true;
            // 
            // blurCheck
            // 
            this.blurCheck.AutoSize = true;
            this.blurCheck.Location = new System.Drawing.Point(19, 102);
            this.blurCheck.Name = "blurCheck";
            this.blurCheck.Size = new System.Drawing.Size(61, 17);
            this.blurCheck.TabIndex = 4;
            this.blurCheck.Text = "Median";
            this.blurCheck.UseVisualStyleBackColor = true;
            this.blurCheck.CheckedChanged += new System.EventHandler(this.blurCheck_CheckedChanged);
            // 
            // CannyCheck
            // 
            this.CannyCheck.AutoSize = true;
            this.CannyCheck.Location = new System.Drawing.Point(19, 149);
            this.CannyCheck.Name = "CannyCheck";
            this.CannyCheck.Size = new System.Drawing.Size(56, 17);
            this.CannyCheck.TabIndex = 5;
            this.CannyCheck.Text = "Canny";
            this.CannyCheck.UseVisualStyleBackColor = true;
            // 
            // trackDilate
            // 
            this.trackDilate.Location = new System.Drawing.Point(78, 19);
            this.trackDilate.Minimum = 1;
            this.trackDilate.Name = "trackDilate";
            this.trackDilate.Size = new System.Drawing.Size(168, 45);
            this.trackDilate.TabIndex = 7;
            this.trackDilate.Value = 1;
            // 
            // trackBlur
            // 
            this.trackBlur.LargeChange = 3;
            this.trackBlur.Location = new System.Drawing.Point(78, 102);
            this.trackBlur.Maximum = 21;
            this.trackBlur.Minimum = 3;
            this.trackBlur.Name = "trackBlur";
            this.trackBlur.Size = new System.Drawing.Size(168, 45);
            this.trackBlur.TabIndex = 8;
            this.trackBlur.Value = 7;
            // 
            // trackSkinMin
            // 
            this.trackSkinMin.Location = new System.Drawing.Point(6, 19);
            this.trackSkinMin.Maximum = 360;
            this.trackSkinMin.Name = "trackSkinMin";
            this.trackSkinMin.Size = new System.Drawing.Size(205, 45);
            this.trackSkinMin.TabIndex = 9;
            this.trackSkinMin.Value = 1;
            this.trackSkinMin.Scroll += new System.EventHandler(this.trackSkinMin_Scroll);
            // 
            // trackSkinMax
            // 
            this.trackSkinMax.Location = new System.Drawing.Point(6, 57);
            this.trackSkinMax.Maximum = 360;
            this.trackSkinMax.Name = "trackSkinMax";
            this.trackSkinMax.Size = new System.Drawing.Size(205, 45);
            this.trackSkinMax.TabIndex = 10;
            this.trackSkinMax.Value = 1;
            this.trackSkinMax.Scroll += new System.EventHandler(this.trackSkinMax_Scroll);
            // 
            // trackBarMinVal
            // 
            this.trackBarMinVal.Location = new System.Drawing.Point(217, 19);
            this.trackBarMinVal.Name = "trackBarMinVal";
            this.trackBarMinVal.Size = new System.Drawing.Size(36, 20);
            this.trackBarMinVal.TabIndex = 12;
            // 
            // trackBarMaxVal
            // 
            this.trackBarMaxVal.Location = new System.Drawing.Point(217, 57);
            this.trackBarMaxVal.Name = "trackBarMaxVal";
            this.trackBarMaxVal.Size = new System.Drawing.Size(36, 20);
            this.trackBarMaxVal.TabIndex = 13;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.checkBox1);
            this.groupBox1.Controls.Add(this.trackBar1);
            this.groupBox1.Controls.Add(this.trackBarLapace);
            this.groupBox1.Controls.Add(this.dilateCheck);
            this.groupBox1.Controls.Add(this.blurCheck);
            this.groupBox1.Controls.Add(this.CannyCheck);
            this.groupBox1.Controls.Add(this.trackDilate);
            this.groupBox1.Controls.Add(this.trackBlur);
            this.groupBox1.Location = new System.Drawing.Point(12, 362);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(259, 200);
            this.groupBox1.TabIndex = 14;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Filtering";
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(19, 59);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(54, 17);
            this.checkBox1.TabIndex = 11;
            this.checkBox1.Text = "Erode";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // trackBar1
            // 
            this.trackBar1.Location = new System.Drawing.Point(78, 59);
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(168, 45);
            this.trackBar1.TabIndex = 10;
            // 
            // trackBarLapace
            // 
            this.trackBarLapace.Location = new System.Drawing.Point(78, 149);
            this.trackBarLapace.Maximum = 20;
            this.trackBarLapace.Minimum = 10;
            this.trackBarLapace.Name = "trackBarLapace";
            this.trackBarLapace.Size = new System.Drawing.Size(168, 45);
            this.trackBarLapace.TabIndex = 9;
            this.trackBarLapace.TickFrequency = 3;
            this.trackBarLapace.Value = 15;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.radioButton5);
            this.groupBox2.Controls.Add(this.radioButton4);
            this.groupBox2.Controls.Add(this.radioButton3);
            this.groupBox2.Controls.Add(this.trackBarMaxVal);
            this.groupBox2.Controls.Add(this.hsvFilter);
            this.groupBox2.Controls.Add(this.trackBarMinVal);
            this.groupBox2.Controls.Add(this.trackSkinMax);
            this.groupBox2.Controls.Add(this.trackSkinMin);
            this.groupBox2.Location = new System.Drawing.Point(12, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(259, 152);
            this.groupBox2.TabIndex = 15;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "HSV";
            // 
            // radioButton5
            // 
            this.radioButton5.AutoSize = true;
            this.radioButton5.Location = new System.Drawing.Point(136, 108);
            this.radioButton5.Name = "radioButton5";
            this.radioButton5.Size = new System.Drawing.Size(52, 17);
            this.radioButton5.TabIndex = 16;
            this.radioButton5.Text = "Value";
            this.radioButton5.UseVisualStyleBackColor = true;
            this.radioButton5.CheckedChanged += new System.EventHandler(this.radioButton5_CheckedChanged);
            // 
            // radioButton4
            // 
            this.radioButton4.AutoSize = true;
            this.radioButton4.Checked = true;
            this.radioButton4.Location = new System.Drawing.Point(57, 108);
            this.radioButton4.Name = "radioButton4";
            this.radioButton4.Size = new System.Drawing.Size(73, 17);
            this.radioButton4.TabIndex = 15;
            this.radioButton4.TabStop = true;
            this.radioButton4.Text = "Saturation";
            this.radioButton4.UseVisualStyleBackColor = true;
            this.radioButton4.CheckedChanged += new System.EventHandler(this.radioButton4_CheckedChanged);
            // 
            // radioButton3
            // 
            this.radioButton3.AutoSize = true;
            this.radioButton3.Location = new System.Drawing.Point(6, 108);
            this.radioButton3.Name = "radioButton3";
            this.radioButton3.Size = new System.Drawing.Size(45, 17);
            this.radioButton3.TabIndex = 14;
            this.radioButton3.Text = "Hue";
            this.radioButton3.UseVisualStyleBackColor = true;
            this.radioButton3.CheckedChanged += new System.EventHandler(this.radioButton3_CheckedChanged);
            // 
            // hsvFilter
            // 
            this.hsvFilter.AutoSize = true;
            this.hsvFilter.Location = new System.Drawing.Point(6, 0);
            this.hsvFilter.Name = "hsvFilter";
            this.hsvFilter.Size = new System.Drawing.Size(48, 17);
            this.hsvFilter.TabIndex = 4;
            this.hsvFilter.Text = "HSV";
            this.hsvFilter.UseVisualStyleBackColor = true;
            this.hsvFilter.CheckedChanged += new System.EventHandler(this.hsvFilter_CheckedChanged);
            // 
            // ycrcbFilter
            // 
            this.ycrcbFilter.AutoSize = true;
            this.ycrcbFilter.Location = new System.Drawing.Point(6, 0);
            this.ycrcbFilter.Name = "ycrcbFilter";
            this.ycrcbFilter.Size = new System.Drawing.Size(56, 17);
            this.ycrcbFilter.TabIndex = 5;
            this.ycrcbFilter.Text = "YCrCb";
            this.ycrcbFilter.UseVisualStyleBackColor = true;
            this.ycrcbFilter.CheckedChanged += new System.EventHandler(this.ycrcbFilter_CheckedChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.trackCbMaxVal);
            this.groupBox3.Controls.Add(this.trackCbMinVal);
            this.groupBox3.Controls.Add(this.trackCrMaxVal);
            this.groupBox3.Controls.Add(this.trackCbMax);
            this.groupBox3.Controls.Add(this.trackCrMinVal);
            this.groupBox3.Controls.Add(this.trackCbMin);
            this.groupBox3.Controls.Add(this.trackCrMax);
            this.groupBox3.Controls.Add(this.ycrcbFilter);
            this.groupBox3.Controls.Add(this.trackCrMin);
            this.groupBox3.Location = new System.Drawing.Point(12, 170);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(259, 186);
            this.groupBox3.TabIndex = 16;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "YCrCb";
            // 
            // trackCbMaxVal
            // 
            this.trackCbMaxVal.Location = new System.Drawing.Point(217, 133);
            this.trackCbMaxVal.Name = "trackCbMaxVal";
            this.trackCbMaxVal.Size = new System.Drawing.Size(36, 20);
            this.trackCbMaxVal.TabIndex = 19;
            // 
            // trackCbMinVal
            // 
            this.trackCbMinVal.Location = new System.Drawing.Point(217, 95);
            this.trackCbMinVal.Name = "trackCbMinVal";
            this.trackCbMinVal.Size = new System.Drawing.Size(36, 20);
            this.trackCbMinVal.TabIndex = 18;
            // 
            // trackCrMaxVal
            // 
            this.trackCrMaxVal.Location = new System.Drawing.Point(217, 57);
            this.trackCrMaxVal.Name = "trackCrMaxVal";
            this.trackCrMaxVal.Size = new System.Drawing.Size(36, 20);
            this.trackCrMaxVal.TabIndex = 15;
            // 
            // trackCbMax
            // 
            this.trackCbMax.Location = new System.Drawing.Point(6, 133);
            this.trackCbMax.Maximum = 255;
            this.trackCbMax.Name = "trackCbMax";
            this.trackCbMax.Size = new System.Drawing.Size(205, 45);
            this.trackCbMax.TabIndex = 17;
            this.trackCbMax.Value = 1;
            this.trackCbMax.Scroll += new System.EventHandler(this.trackCbMax_Scroll);
            // 
            // trackCrMinVal
            // 
            this.trackCrMinVal.Location = new System.Drawing.Point(217, 19);
            this.trackCrMinVal.Name = "trackCrMinVal";
            this.trackCrMinVal.Size = new System.Drawing.Size(36, 20);
            this.trackCrMinVal.TabIndex = 14;
            // 
            // trackCbMin
            // 
            this.trackCbMin.Location = new System.Drawing.Point(6, 95);
            this.trackCbMin.Maximum = 255;
            this.trackCbMin.Name = "trackCbMin";
            this.trackCbMin.Size = new System.Drawing.Size(205, 45);
            this.trackCbMin.TabIndex = 16;
            this.trackCbMin.Value = 1;
            this.trackCbMin.Scroll += new System.EventHandler(this.trackCbMin_Scroll);
            // 
            // trackCrMax
            // 
            this.trackCrMax.Location = new System.Drawing.Point(6, 57);
            this.trackCrMax.Maximum = 255;
            this.trackCrMax.Name = "trackCrMax";
            this.trackCrMax.Size = new System.Drawing.Size(205, 45);
            this.trackCrMax.TabIndex = 15;
            this.trackCrMax.Value = 1;
            this.trackCrMax.Scroll += new System.EventHandler(this.trackCrMax_Scroll);
            // 
            // trackCrMin
            // 
            this.trackCrMin.Location = new System.Drawing.Point(6, 19);
            this.trackCrMin.Maximum = 255;
            this.trackCrMin.Name = "trackCrMin";
            this.trackCrMin.Size = new System.Drawing.Size(205, 45);
            this.trackCrMin.TabIndex = 14;
            this.trackCrMin.Value = 1;
            this.trackCrMin.Scroll += new System.EventHandler(this.trackCrMin_Scroll);
            // 
            // pictureBox2
            // 
            this.pictureBox2.Location = new System.Drawing.Point(639, 407);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(100, 100);
            this.pictureBox2.TabIndex = 18;
            this.pictureBox2.TabStop = false;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.radioButton2);
            this.groupBox5.Controls.Add(this.radioButton1);
            this.groupBox5.Location = new System.Drawing.Point(12, 568);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(259, 100);
            this.groupBox5.TabIndex = 19;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Centroid";
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(19, 42);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(61, 17);
            this.radioButton2.TabIndex = 1;
            this.radioButton2.TabStop = true;
            this.radioButton2.Text = "K-mean";
            this.radioButton2.UseVisualStyleBackColor = true;
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Checked = true;
            this.radioButton1.Location = new System.Drawing.Point(19, 19);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(68, 17);
            this.radioButton1.TabIndex = 0;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "Moments";
            this.radioButton1.UseVisualStyleBackColor = true;
            // 
            // timer2
            // 
            this.timer2.Interval = 1000;
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // info
            // 
            this.info.Location = new System.Drawing.Point(604, 547);
            this.info.Multiline = true;
            this.info.Name = "info";
            this.info.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.info.Size = new System.Drawing.Size(313, 88);
            this.info.TabIndex = 21;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.checkPredict);
            this.groupBox4.Controls.Add(this.classLabel);
            this.groupBox4.Controls.Add(this.TestingBut);
            this.groupBox4.Controls.Add(this.predictBut);
            this.groupBox4.Controls.Add(this.trainingBut);
            this.groupBox4.Controls.Add(this.checkTrainData);
            this.groupBox4.Controls.Add(this.checkTestingData);
            this.groupBox4.Location = new System.Drawing.Point(277, 407);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(284, 123);
            this.groupBox4.TabIndex = 22;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "SVM";
            // 
            // checkPredict
            // 
            this.checkPredict.AutoSize = true;
            this.checkPredict.Location = new System.Drawing.Point(157, 42);
            this.checkPredict.Name = "checkPredict";
            this.checkPredict.Size = new System.Drawing.Size(59, 17);
            this.checkPredict.TabIndex = 27;
            this.checkPredict.Text = "Predict";
            this.checkPredict.UseVisualStyleBackColor = true;
            this.checkPredict.CheckedChanged += new System.EventHandler(this.checkPredict_CheckedChanged);
            // 
            // classLabel
            // 
            this.classLabel.FormattingEnabled = true;
            this.classLabel.Items.AddRange(new object[] {
            "0",
            "1",
            "2",
            "3",
            "4",
            "5"});
            this.classLabel.Location = new System.Drawing.Point(157, 18);
            this.classLabel.Name = "classLabel";
            this.classLabel.Size = new System.Drawing.Size(121, 21);
            this.classLabel.TabIndex = 26;
            this.classLabel.Text = "1";
            // 
            // TestingBut
            // 
            this.TestingBut.Location = new System.Drawing.Point(168, 76);
            this.TestingBut.Name = "TestingBut";
            this.TestingBut.Size = new System.Drawing.Size(75, 23);
            this.TestingBut.TabIndex = 25;
            this.TestingBut.Text = "Testing";
            this.TestingBut.UseVisualStyleBackColor = true;
            this.TestingBut.Click += new System.EventHandler(this.TestingBut_Click);
            // 
            // predictBut
            // 
            this.predictBut.Location = new System.Drawing.Point(87, 74);
            this.predictBut.Name = "predictBut";
            this.predictBut.Size = new System.Drawing.Size(75, 23);
            this.predictBut.TabIndex = 24;
            this.predictBut.Text = "Predict";
            this.predictBut.UseVisualStyleBackColor = true;
            this.predictBut.Click += new System.EventHandler(this.predictBut_Click);
            // 
            // trainingBut
            // 
            this.trainingBut.Location = new System.Drawing.Point(6, 74);
            this.trainingBut.Name = "trainingBut";
            this.trainingBut.Size = new System.Drawing.Size(75, 23);
            this.trainingBut.TabIndex = 23;
            this.trainingBut.Text = "Training";
            this.trainingBut.UseVisualStyleBackColor = true;
            this.trainingBut.Click += new System.EventHandler(this.button2_Click);
            // 
            // checkTrainData
            // 
            this.checkTrainData.AutoSize = true;
            this.checkTrainData.Location = new System.Drawing.Point(6, 42);
            this.checkTrainData.Name = "checkTrainData";
            this.checkTrainData.Size = new System.Drawing.Size(124, 17);
            this.checkTrainData.TabIndex = 1;
            this.checkTrainData.Text = "Create Training Data";
            this.checkTrainData.UseVisualStyleBackColor = true;
            // 
            // checkTestingData
            // 
            this.checkTestingData.AutoSize = true;
            this.checkTestingData.Location = new System.Drawing.Point(6, 19);
            this.checkTestingData.Name = "checkTestingData";
            this.checkTestingData.Size = new System.Drawing.Size(121, 17);
            this.checkTestingData.TabIndex = 0;
            this.checkTestingData.Text = "Create Testing Data";
            this.checkTestingData.UseVisualStyleBackColor = true;
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.butHmmTesting);
            this.groupBox6.Controls.Add(this.butHmmLearning);
            this.groupBox6.Controls.Add(this.checkPredictHmm);
            this.groupBox6.Location = new System.Drawing.Point(277, 536);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(284, 123);
            this.groupBox6.TabIndex = 28;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "HMM";
            // 
            // butHmmTesting
            // 
            this.butHmmTesting.Location = new System.Drawing.Point(122, 94);
            this.butHmmTesting.Name = "butHmmTesting";
            this.butHmmTesting.Size = new System.Drawing.Size(75, 23);
            this.butHmmTesting.TabIndex = 29;
            this.butHmmTesting.Text = "Testing";
            this.butHmmTesting.UseVisualStyleBackColor = true;
            this.butHmmTesting.Click += new System.EventHandler(this.butHmmTesting_Click);
            // 
            // butHmmLearning
            // 
            this.butHmmLearning.Location = new System.Drawing.Point(203, 94);
            this.butHmmLearning.Name = "butHmmLearning";
            this.butHmmLearning.Size = new System.Drawing.Size(75, 23);
            this.butHmmLearning.TabIndex = 28;
            this.butHmmLearning.Text = "Learning";
            this.butHmmLearning.UseVisualStyleBackColor = true;
            this.butHmmLearning.Click += new System.EventHandler(this.butHmmLearning_Click);
            // 
            // checkPredictHmm
            // 
            this.checkPredictHmm.AutoSize = true;
            this.checkPredictHmm.Location = new System.Drawing.Point(6, 19);
            this.checkPredictHmm.Name = "checkPredictHmm";
            this.checkPredictHmm.Size = new System.Drawing.Size(59, 17);
            this.checkPredictHmm.TabIndex = 27;
            this.checkPredictHmm.Text = "Predict";
            this.checkPredictHmm.UseVisualStyleBackColor = true;
            this.checkPredictHmm.CheckedChanged += new System.EventHandler(this.checkPredictHmm_CheckedChanged);
            // 
            // originalPicture
            // 
            this.originalPicture.Location = new System.Drawing.Point(3, 0);
            this.originalPicture.Name = "originalPicture";
            this.originalPicture.Size = new System.Drawing.Size(329, 306);
            this.originalPicture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.originalPicture.TabIndex = 29;
            this.originalPicture.TabStop = false;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog";
            this.openFileDialog1.Filter = "Video File (*.avi) |*.avi| Bitmap File (*.bmp)|*.bmp|JPEG File (*.jpg)|*.jpg";
            // 
            // openFileButton
            // 
            this.openFileButton.Location = new System.Drawing.Point(480, 362);
            this.openFileButton.Name = "openFileButton";
            this.openFileButton.Size = new System.Drawing.Size(75, 23);
            this.openFileButton.TabIndex = 30;
            this.openFileButton.Text = "Open File";
            this.openFileButton.UseVisualStyleBackColor = true;
            this.openFileButton.Click += new System.EventHandler(this.openFileButton_Click);
            // 
            // fileNameTextBox
            // 
            this.fileNameTextBox.Location = new System.Drawing.Point(283, 363);
            this.fileNameTextBox.Name = "fileNameTextBox";
            this.fileNameTextBox.Size = new System.Drawing.Size(191, 20);
            this.fileNameTextBox.TabIndex = 31;
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Location = new System.Drawing.Point(283, 13);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(84, 17);
            this.checkBox2.TabIndex = 32;
            this.checkBox2.Text = "Use Camera";
            this.checkBox2.UseVisualStyleBackColor = true;
            this.checkBox2.CheckedChanged += new System.EventHandler(this.checkBox2_CheckedChanged);
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.Controls.Add(this.originalPicture);
            this.panel1.Location = new System.Drawing.Point(283, 44);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(335, 312);
            this.panel1.TabIndex = 33;
            // 
            // panel2
            // 
            this.panel2.AutoScroll = true;
            this.panel2.Controls.Add(this.pictureBox1);
            this.panel2.Location = new System.Drawing.Point(624, 44);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(335, 312);
            this.panel2.TabIndex = 34;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1022, 680);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.checkBox2);
            this.Controls.Add(this.fileNameTextBox);
            this.Controls.Add(this.openFileButton);
            this.Controls.Add(this.groupBox6);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.info);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.captureButton);
            this.Name = "MainForm";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.form_closing);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackDilate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBlur)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackSkinMin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackSkinMax)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarLapace)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackCbMax)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackCbMin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackCrMax)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackCrMin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.originalPicture)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button captureButton;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.CheckBox dilateCheck;
        private System.Windows.Forms.CheckBox blurCheck;
        private System.Windows.Forms.CheckBox CannyCheck;
        private System.Windows.Forms.TrackBar trackDilate;
        private System.Windows.Forms.TrackBar trackBlur;
        private System.Windows.Forms.TrackBar trackSkinMin;
        private System.Windows.Forms.TrackBar trackSkinMax;
        private System.Windows.Forms.TextBox trackBarMinVal;
        private System.Windows.Forms.TextBox trackBarMaxVal;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox hsvFilter;
        private System.Windows.Forms.CheckBox ycrcbFilter;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox trackCbMaxVal;
        private System.Windows.Forms.TextBox trackCbMinVal;
        private System.Windows.Forms.TextBox trackCrMaxVal;
        private System.Windows.Forms.TrackBar trackCbMax;
        private System.Windows.Forms.TextBox trackCrMinVal;
        private System.Windows.Forms.TrackBar trackCbMin;
        private System.Windows.Forms.TrackBar trackCrMax;
        private System.Windows.Forms.TrackBar trackCrMin;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.Timer timer2;
        private System.Windows.Forms.RadioButton radioButton5;
        private System.Windows.Forms.RadioButton radioButton4;
        private System.Windows.Forms.RadioButton radioButton3;
        private System.Windows.Forms.TrackBar trackBarLapace;
        private System.Windows.Forms.TextBox info;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.CheckBox checkTrainData;
        private System.Windows.Forms.CheckBox checkTestingData;
        private System.Windows.Forms.Button trainingBut;
        private System.Windows.Forms.Button predictBut;
        private System.Windows.Forms.Button TestingBut;
        private System.Windows.Forms.ComboBox classLabel;
        private System.Windows.Forms.CheckBox checkPredict;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.CheckBox checkPredictHmm;
        private System.Windows.Forms.Button butHmmLearning;
        private System.Windows.Forms.Button butHmmTesting;
        private System.Windows.Forms.TrackBar trackBar1;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.PictureBox originalPicture;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button openFileButton;
        private System.Windows.Forms.TextBox fileNameTextBox;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
    }
}

