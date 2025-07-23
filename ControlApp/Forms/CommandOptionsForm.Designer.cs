namespace ControlApp;

partial class CommandOptionsForm
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
        saveAndExitButton = new Button();
        tabPageGeneral = new TabPage();
        miscOptionsPanel = new Panel();
        delayBetweenOutstandingCommandsNumericUpDown = new NumericUpDown();
        textBoxDownloadFolder = new TextBox();
        buttonSelectNewDownloadFolder = new Button();
        label7 = new Label();
        runAllOutstandingCheckBox = new CheckBox();
        autoRunSentExeCheckBox = new CheckBox();
        darkModeCheckbox = new CheckBox();
        webcamCountCheckbox = new CheckBox();
        label5 = new Label();
        showBlockedCheckbox = new CheckBox();
        disallowPanel = new Panel();
        disableMouseCheckbox = new CheckBox();
        ttsCheckbox = new CheckBox();
        webcamCheckbox = new CheckBox();
        disableInputCheckbox = new CheckBox();
        reminderCheckbox = new CheckBox();
        sendDeleteCheckbox = new CheckBox();
        watchForMeCheckbox = new CheckBox();
        twitterCheckbox = new CheckBox();
        screenshotCheckbox = new CheckBox();
        writeForMeCheckbox = new CheckBox();
        audioCheckbox = new CheckBox();
        subliminalCheckbox = new CheckBox();
        messageCheckbox = new CheckBox();
        popupCheckbox = new CheckBox();
        label2 = new Label();
        websiteCheckbox = new CheckBox();
        runnableCheckbox = new CheckBox();
        wallpaperCheckbox = new CheckBox();
        downloadCheckbox = new CheckBox();
        tabPageWallpaper = new TabPage();
        wallpaperPanel = new Panel();
        scaleToFitRadioButton = new RadioButton();
        scaleStretchRadioButton = new RadioButton();
        label3 = new Label();
        tabPagePopups = new TabPage();
        popupTypePanel = new Panel();
        panel6 = new Panel();
        movingRadioButton = new RadioButton();
        stillRadioButton = new RadioButton();
        panel5 = new Panel();
        seethroughRadioButton = new RadioButton();
        opaqueRadioButton = new RadioButton();
        panel11 = new Panel();
        clickableRadioButton = new RadioButton();
        clickthroughRadioButton = new RadioButton();
        normalRadioButton = new RadioButton();
        label11 = new Label();
        popupModePanel = new Panel();
        fullscreenCheckbox = new CheckBox();
        parallelRadioButton = new RadioButton();
        serialRadioButton = new RadioButton();
        label4 = new Label();
        popupLengthPanel = new Panel();
        longPopupRadioButton = new RadioButton();
        shortPopupRadioButton = new RadioButton();
        label1 = new Label();
        tabControl1 = new TabControl();
        folderBrowserDialog1 = new FolderBrowserDialog();
        tabPageGeneral.SuspendLayout();
        miscOptionsPanel.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)delayBetweenOutstandingCommandsNumericUpDown).BeginInit();
        disallowPanel.SuspendLayout();
        tabPageWallpaper.SuspendLayout();
        wallpaperPanel.SuspendLayout();
        tabPagePopups.SuspendLayout();
        popupTypePanel.SuspendLayout();
        panel6.SuspendLayout();
        panel5.SuspendLayout();
        panel11.SuspendLayout();
        popupModePanel.SuspendLayout();
        popupLengthPanel.SuspendLayout();
        tabControl1.SuspendLayout();
        SuspendLayout();
        // 
        // saveAndExitButton
        // 
        saveAndExitButton.Location = new Point(301, 477);
        saveAndExitButton.Name = "saveAndExitButton";
        saveAndExitButton.Size = new Size(112, 23);
        saveAndExitButton.TabIndex = 1;
        saveAndExitButton.Text = "Save and Exit ";
        saveAndExitButton.UseVisualStyleBackColor = true;
        // 
        // tabPageGeneral
        // 
        tabPageGeneral.Controls.Add(miscOptionsPanel);
        tabPageGeneral.Controls.Add(disallowPanel);
        tabPageGeneral.Location = new Point(4, 24);
        tabPageGeneral.Name = "tabPageGeneral";
        tabPageGeneral.Padding = new Padding(3);
        tabPageGeneral.Size = new Size(685, 431);
        tabPageGeneral.TabIndex = 0;
        tabPageGeneral.Text = "General";
        tabPageGeneral.UseVisualStyleBackColor = true;
        // 
        // miscOptionsPanel
        // 
        miscOptionsPanel.Controls.Add(delayBetweenOutstandingCommandsNumericUpDown);
        miscOptionsPanel.Controls.Add(textBoxDownloadFolder);
        miscOptionsPanel.Controls.Add(buttonSelectNewDownloadFolder);
        miscOptionsPanel.Controls.Add(label7);
        miscOptionsPanel.Controls.Add(runAllOutstandingCheckBox);
        miscOptionsPanel.Controls.Add(autoRunSentExeCheckBox);
        miscOptionsPanel.Controls.Add(darkModeCheckbox);
        miscOptionsPanel.Controls.Add(webcamCountCheckbox);
        miscOptionsPanel.Controls.Add(label5);
        miscOptionsPanel.Controls.Add(showBlockedCheckbox);
        miscOptionsPanel.Location = new Point(347, 6);
        miscOptionsPanel.Name = "miscOptionsPanel";
        miscOptionsPanel.Size = new Size(334, 266);
        miscOptionsPanel.TabIndex = 29;
        // 
        // delayBetweenOutstandingCommandsNumericUpDown
        // 
        delayBetweenOutstandingCommandsNumericUpDown.Location = new Point(21, 143);
        delayBetweenOutstandingCommandsNumericUpDown.Name = "delayBetweenOutstandingCommandsNumericUpDown";
        delayBetweenOutstandingCommandsNumericUpDown.Size = new Size(103, 23);
        delayBetweenOutstandingCommandsNumericUpDown.TabIndex = 34;
        // 
        // textBoxDownloadFolder
        // 
        textBoxDownloadFolder.Location = new Point(21, 201);
        textBoxDownloadFolder.Name = "textBoxDownloadFolder";
        textBoxDownloadFolder.ReadOnly = true;
        textBoxDownloadFolder.Size = new Size(295, 23);
        textBoxDownloadFolder.TabIndex = 32;
        // 
        // buttonSelectNewDownloadFolder
        // 
        buttonSelectNewDownloadFolder.Location = new Point(21, 172);
        buttonSelectNewDownloadFolder.Name = "buttonSelectNewDownloadFolder";
        buttonSelectNewDownloadFolder.Size = new Size(163, 23);
        buttonSelectNewDownloadFolder.TabIndex = 31;
        buttonSelectNewDownloadFolder.Text = "Select new download folder";
        buttonSelectNewDownloadFolder.UseVisualStyleBackColor = true;
        buttonSelectNewDownloadFolder.Click += OnClickSelectNewDownloadFolderButton;
        // 
        // label7
        // 
        label7.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        label7.AutoSize = true;
        label7.Location = new Point(130, 146);
        label7.Name = "label7";
        label7.Size = new Size(201, 15);
        label7.TabIndex = 30;
        label7.Text = "Delay between commands (seconds)";
        // 
        // runAllOutstandingCheckBox
        // 
        runAllOutstandingCheckBox.AutoSize = true;
        runAllOutstandingCheckBox.Location = new Point(21, 118);
        runAllOutstandingCheckBox.Name = "runAllOutstandingCheckBox";
        runAllOutstandingCheckBox.Size = new Size(133, 19);
        runAllOutstandingCheckBox.TabIndex = 27;
        runAllOutstandingCheckBox.Text = "Run All Outstanding";
        runAllOutstandingCheckBox.UseVisualStyleBackColor = true;
        // 
        // autoRunSentExeCheckBox
        // 
        autoRunSentExeCheckBox.AutoSize = true;
        autoRunSentExeCheckBox.Location = new Point(21, 93);
        autoRunSentExeCheckBox.Name = "autoRunSentExeCheckBox";
        autoRunSentExeCheckBox.Size = new Size(118, 19);
        autoRunSentExeCheckBox.TabIndex = 24;
        autoRunSentExeCheckBox.Text = "Auto run sent exe";
        autoRunSentExeCheckBox.UseVisualStyleBackColor = true;
        // 
        // darkModeCheckbox
        // 
        darkModeCheckbox.AutoSize = true;
        darkModeCheckbox.Location = new Point(21, 68);
        darkModeCheckbox.Name = "darkModeCheckbox";
        darkModeCheckbox.Size = new Size(84, 19);
        darkModeCheckbox.TabIndex = 23;
        darkModeCheckbox.Text = "Dark mode";
        darkModeCheckbox.UseVisualStyleBackColor = true;
        // 
        // webcamCountCheckbox
        // 
        webcamCountCheckbox.AutoSize = true;
        webcamCountCheckbox.Location = new Point(21, 18);
        webcamCountCheckbox.Name = "webcamCountCheckbox";
        webcamCountCheckbox.Size = new Size(137, 19);
        webcamCountCheckbox.TabIndex = 22;
        webcamCountCheckbox.Text = "Webcam countdown";
        webcamCountCheckbox.UseVisualStyleBackColor = true;
        // 
        // label5
        // 
        label5.AutoSize = true;
        label5.Location = new Point(7, 0);
        label5.Name = "label5";
        label5.Size = new Size(82, 15);
        label5.TabIndex = 21;
        label5.Text = "Miscellaneous";
        // 
        // showBlockedCheckbox
        // 
        showBlockedCheckbox.AutoSize = true;
        showBlockedCheckbox.Location = new Point(21, 43);
        showBlockedCheckbox.Name = "showBlockedCheckbox";
        showBlockedCheckbox.Size = new Size(163, 19);
        showBlockedCheckbox.TabIndex = 20;
        showBlockedCheckbox.Text = "Show blocked commands";
        showBlockedCheckbox.UseVisualStyleBackColor = true;
        // 
        // disallowPanel
        // 
        disallowPanel.Controls.Add(disableMouseCheckbox);
        disallowPanel.Controls.Add(ttsCheckbox);
        disallowPanel.Controls.Add(webcamCheckbox);
        disallowPanel.Controls.Add(disableInputCheckbox);
        disallowPanel.Controls.Add(reminderCheckbox);
        disallowPanel.Controls.Add(sendDeleteCheckbox);
        disallowPanel.Controls.Add(watchForMeCheckbox);
        disallowPanel.Controls.Add(twitterCheckbox);
        disallowPanel.Controls.Add(screenshotCheckbox);
        disallowPanel.Controls.Add(writeForMeCheckbox);
        disallowPanel.Controls.Add(audioCheckbox);
        disallowPanel.Controls.Add(subliminalCheckbox);
        disallowPanel.Controls.Add(messageCheckbox);
        disallowPanel.Controls.Add(popupCheckbox);
        disallowPanel.Controls.Add(label2);
        disallowPanel.Controls.Add(websiteCheckbox);
        disallowPanel.Controls.Add(runnableCheckbox);
        disallowPanel.Controls.Add(wallpaperCheckbox);
        disallowPanel.Controls.Add(downloadCheckbox);
        disallowPanel.Location = new Point(6, 6);
        disallowPanel.Name = "disallowPanel";
        disallowPanel.Size = new Size(334, 266);
        disallowPanel.TabIndex = 26;
        // 
        // disableMouseCheckbox
        // 
        disableMouseCheckbox.AutoSize = true;
        disableMouseCheckbox.Location = new Point(122, 210);
        disableMouseCheckbox.Name = "disableMouseCheckbox";
        disableMouseCheckbox.Size = new Size(103, 19);
        disableMouseCheckbox.TabIndex = 17;
        disableMouseCheckbox.Text = "Disable Mouse";
        disableMouseCheckbox.UseVisualStyleBackColor = true;
        // 
        // ttsCheckbox
        // 
        ttsCheckbox.AutoSize = true;
        ttsCheckbox.Location = new Point(122, 160);
        ttsCheckbox.Name = "ttsCheckbox";
        ttsCheckbox.Size = new Size(46, 19);
        ttsCheckbox.TabIndex = 16;
        ttsCheckbox.Text = "TTS";
        ttsCheckbox.UseVisualStyleBackColor = true;
        // 
        // webcamCheckbox
        // 
        webcamCheckbox.AutoSize = true;
        webcamCheckbox.Location = new Point(122, 135);
        webcamCheckbox.Name = "webcamCheckbox";
        webcamCheckbox.Size = new Size(73, 19);
        webcamCheckbox.TabIndex = 15;
        webcamCheckbox.Text = "Webcam";
        webcamCheckbox.UseVisualStyleBackColor = true;
        // 
        // disableInputCheckbox
        // 
        disableInputCheckbox.AutoSize = true;
        disableInputCheckbox.Location = new Point(122, 185);
        disableInputCheckbox.Name = "disableInputCheckbox";
        disableInputCheckbox.Size = new Size(95, 19);
        disableInputCheckbox.TabIndex = 17;
        disableInputCheckbox.Text = "Disable Input";
        disableInputCheckbox.UseVisualStyleBackColor = true;
        disableInputCheckbox.CheckedChanged += disableInputCheckbox_Changed;
        // 
        // reminderCheckbox
        // 
        reminderCheckbox.AutoSize = true;
        reminderCheckbox.Location = new Point(122, 234);
        reminderCheckbox.Name = "reminderCheckbox";
        reminderCheckbox.Size = new Size(143, 19);
        reminderCheckbox.TabIndex = 14;
        reminderCheckbox.Text = "Outstanding reminder";
        reminderCheckbox.UseVisualStyleBackColor = true;
        // 
        // sendDeleteCheckbox
        // 
        sendDeleteCheckbox.AutoSize = true;
        sendDeleteCheckbox.Location = new Point(122, 110);
        sendDeleteCheckbox.Name = "sendDeleteCheckbox";
        sendDeleteCheckbox.Size = new Size(102, 19);
        sendDeleteCheckbox.TabIndex = 13;
        sendDeleteCheckbox.Text = "Send or Delete";
        sendDeleteCheckbox.UseVisualStyleBackColor = true;
        // 
        // watchForMeCheckbox
        // 
        watchForMeCheckbox.AutoSize = true;
        watchForMeCheckbox.Location = new Point(122, 85);
        watchForMeCheckbox.Name = "watchForMeCheckbox";
        watchForMeCheckbox.Size = new Size(98, 19);
        watchForMeCheckbox.TabIndex = 12;
        watchForMeCheckbox.Text = "Watch for me";
        watchForMeCheckbox.UseVisualStyleBackColor = true;
        // 
        // twitterCheckbox
        // 
        twitterCheckbox.AutoSize = true;
        twitterCheckbox.Location = new Point(122, 60);
        twitterCheckbox.Name = "twitterCheckbox";
        twitterCheckbox.Size = new Size(88, 19);
        twitterCheckbox.TabIndex = 11;
        twitterCheckbox.Text = "Twitter post";
        twitterCheckbox.UseVisualStyleBackColor = true;
        // 
        // screenshotCheckbox
        // 
        screenshotCheckbox.AutoSize = true;
        screenshotCheckbox.Location = new Point(122, 35);
        screenshotCheckbox.Name = "screenshotCheckbox";
        screenshotCheckbox.Size = new Size(92, 19);
        screenshotCheckbox.TabIndex = 10;
        screenshotCheckbox.Text = "Screen shots";
        screenshotCheckbox.UseVisualStyleBackColor = true;
        // 
        // writeForMeCheckbox
        // 
        writeForMeCheckbox.AutoSize = true;
        writeForMeCheckbox.Location = new Point(12, 234);
        writeForMeCheckbox.Name = "writeForMeCheckbox";
        writeForMeCheckbox.Size = new Size(92, 19);
        writeForMeCheckbox.TabIndex = 9;
        writeForMeCheckbox.Text = "Write for me";
        writeForMeCheckbox.UseVisualStyleBackColor = true;
        // 
        // audioCheckbox
        // 
        audioCheckbox.AutoSize = true;
        audioCheckbox.Location = new Point(12, 209);
        audioCheckbox.Name = "audioCheckbox";
        audioCheckbox.Size = new Size(58, 19);
        audioCheckbox.TabIndex = 8;
        audioCheckbox.Text = "Audio";
        audioCheckbox.UseVisualStyleBackColor = true;
        // 
        // subliminalCheckbox
        // 
        subliminalCheckbox.AutoSize = true;
        subliminalCheckbox.Location = new Point(12, 185);
        subliminalCheckbox.Name = "subliminalCheckbox";
        subliminalCheckbox.Size = new Size(82, 19);
        subliminalCheckbox.TabIndex = 7;
        subliminalCheckbox.Text = "Subliminal";
        subliminalCheckbox.UseVisualStyleBackColor = true;
        // 
        // messageCheckbox
        // 
        messageCheckbox.AutoSize = true;
        messageCheckbox.Location = new Point(12, 160);
        messageCheckbox.Name = "messageCheckbox";
        messageCheckbox.Size = new Size(77, 19);
        messageCheckbox.TabIndex = 6;
        messageCheckbox.Text = "Messages";
        messageCheckbox.UseVisualStyleBackColor = true;
        // 
        // popupCheckbox
        // 
        popupCheckbox.AutoSize = true;
        popupCheckbox.Location = new Point(12, 135);
        popupCheckbox.Name = "popupCheckbox";
        popupCheckbox.Size = new Size(70, 19);
        popupCheckbox.TabIndex = 5;
        popupCheckbox.Text = "Pop Ups";
        popupCheckbox.UseVisualStyleBackColor = true;
        // 
        // label2
        // 
        label2.AutoSize = true;
        label2.Location = new Point(12, 0);
        label2.Name = "label2";
        label2.Size = new Size(56, 15);
        label2.TabIndex = 4;
        label2.Text = "Dis-allow";
        // 
        // websiteCheckbox
        // 
        websiteCheckbox.AutoSize = true;
        websiteCheckbox.Location = new Point(12, 110);
        websiteCheckbox.Name = "websiteCheckbox";
        websiteCheckbox.Size = new Size(100, 19);
        websiteCheckbox.TabIndex = 3;
        websiteCheckbox.Text = "Open Website";
        websiteCheckbox.UseVisualStyleBackColor = true;
        // 
        // runnableCheckbox
        // 
        runnableCheckbox.AutoSize = true;
        runnableCheckbox.Location = new Point(12, 85);
        runnableCheckbox.Name = "runnableCheckbox";
        runnableCheckbox.Size = new Size(93, 19);
        runnableCheckbox.TabIndex = 2;
        runnableCheckbox.Text = "Runable files";
        runnableCheckbox.UseVisualStyleBackColor = true;
        // 
        // wallpaperCheckbox
        // 
        wallpaperCheckbox.AutoSize = true;
        wallpaperCheckbox.Location = new Point(12, 60);
        wallpaperCheckbox.Name = "wallpaperCheckbox";
        wallpaperCheckbox.Size = new Size(79, 19);
        wallpaperCheckbox.TabIndex = 1;
        wallpaperCheckbox.Text = "Wallpaper";
        wallpaperCheckbox.UseVisualStyleBackColor = true;
        // 
        // downloadCheckbox
        // 
        downloadCheckbox.AutoSize = true;
        downloadCheckbox.Location = new Point(12, 35);
        downloadCheckbox.Name = "downloadCheckbox";
        downloadCheckbox.Size = new Size(80, 19);
        downloadCheckbox.TabIndex = 0;
        downloadCheckbox.Text = "Download";
        downloadCheckbox.UseVisualStyleBackColor = true;
        // 
        // tabPageWallpaper
        // 
        tabPageWallpaper.Controls.Add(wallpaperPanel);
        tabPageWallpaper.Location = new Point(4, 24);
        tabPageWallpaper.Name = "tabPageWallpaper";
        tabPageWallpaper.Padding = new Padding(3);
        tabPageWallpaper.Size = new Size(685, 431);
        tabPageWallpaper.TabIndex = 2;
        tabPageWallpaper.Text = "Wallpaper";
        tabPageWallpaper.UseVisualStyleBackColor = true;
        // 
        // wallpaperPanel
        // 
        wallpaperPanel.Controls.Add(scaleToFitRadioButton);
        wallpaperPanel.Controls.Add(scaleStretchRadioButton);
        wallpaperPanel.Controls.Add(label3);
        wallpaperPanel.Location = new Point(6, 6);
        wallpaperPanel.Name = "wallpaperPanel";
        wallpaperPanel.Size = new Size(334, 48);
        wallpaperPanel.TabIndex = 28;
        // 
        // scaleToFitRadioButton
        // 
        scaleToFitRadioButton.AutoSize = true;
        scaleToFitRadioButton.Location = new Point(122, 18);
        scaleToFitRadioButton.Name = "scaleToFitRadioButton";
        scaleToFitRadioButton.Size = new Size(75, 19);
        scaleToFitRadioButton.TabIndex = 2;
        scaleToFitRadioButton.TabStop = true;
        scaleToFitRadioButton.Text = "Fit screen";
        scaleToFitRadioButton.UseVisualStyleBackColor = true;
        // 
        // scaleStretchRadioButton
        // 
        scaleStretchRadioButton.AutoSize = true;
        scaleStretchRadioButton.Location = new Point(20, 18);
        scaleStretchRadioButton.Name = "scaleStretchRadioButton";
        scaleStretchRadioButton.Size = new Size(62, 19);
        scaleStretchRadioButton.TabIndex = 1;
        scaleStretchRadioButton.TabStop = true;
        scaleStretchRadioButton.Text = "Stretch";
        scaleStretchRadioButton.UseVisualStyleBackColor = true;
        // 
        // label3
        // 
        label3.AutoSize = true;
        label3.Location = new Point(12, -3);
        label3.Name = "label3";
        label3.Size = new Size(60, 15);
        label3.TabIndex = 0;
        label3.Text = "Wallpaper";
        // 
        // tabPagePopups
        // 
        tabPagePopups.Controls.Add(popupTypePanel);
        tabPagePopups.Controls.Add(popupModePanel);
        tabPagePopups.Controls.Add(popupLengthPanel);
        tabPagePopups.Location = new Point(4, 24);
        tabPagePopups.Name = "tabPagePopups";
        tabPagePopups.Padding = new Padding(3);
        tabPagePopups.Size = new Size(685, 431);
        tabPagePopups.TabIndex = 1;
        tabPagePopups.Text = "Pop-ups";
        tabPagePopups.UseVisualStyleBackColor = true;
        // 
        // popupTypePanel
        // 
        popupTypePanel.Controls.Add(fullscreenCheckbox);
        popupTypePanel.Controls.Add(panel6);
        popupTypePanel.Controls.Add(panel5);
        popupTypePanel.Controls.Add(panel11);
        popupTypePanel.Controls.Add(label11);
        popupTypePanel.Location = new Point(6, 99);
        popupTypePanel.Name = "popupTypePanel";
        popupTypePanel.Size = new Size(334, 200);
        popupTypePanel.TabIndex = 30;
        // 
        // panel6
        // 
        panel6.Controls.Add(movingRadioButton);
        panel6.Controls.Add(stillRadioButton);
        panel6.Location = new Point(146, 40);
        panel6.Name = "panel6";
        panel6.Size = new Size(119, 87);
        panel6.TabIndex = 9;
        // 
        // movingRadioButton
        // 
        movingRadioButton.AutoSize = true;
        movingRadioButton.Location = new Point(13, 32);
        movingRadioButton.Name = "movingRadioButton";
        movingRadioButton.Size = new Size(66, 19);
        movingRadioButton.TabIndex = 1;
        movingRadioButton.TabStop = true;
        movingRadioButton.Text = "Moving";
        movingRadioButton.UseVisualStyleBackColor = true;
        // 
        // stillRadioButton
        // 
        stillRadioButton.AutoSize = true;
        stillRadioButton.Location = new Point(13, 7);
        stillRadioButton.Name = "stillRadioButton";
        stillRadioButton.Size = new Size(72, 19);
        stillRadioButton.TabIndex = 6;
        stillRadioButton.TabStop = true;
        stillRadioButton.Text = "Standard";
        stillRadioButton.UseVisualStyleBackColor = true;
        // 
        // panel5
        // 
        panel5.Controls.Add(seethroughRadioButton);
        panel5.Controls.Add(opaqueRadioButton);
        panel5.Location = new Point(21, 125);
        panel5.Name = "panel5";
        panel5.Size = new Size(119, 62);
        panel5.TabIndex = 8;
        // 
        // seethroughRadioButton
        // 
        seethroughRadioButton.AutoSize = true;
        seethroughRadioButton.Location = new Point(13, 32);
        seethroughRadioButton.Name = "seethroughRadioButton";
        seethroughRadioButton.Size = new Size(89, 19);
        seethroughRadioButton.TabIndex = 1;
        seethroughRadioButton.TabStop = true;
        seethroughRadioButton.Text = "See through";
        seethroughRadioButton.UseVisualStyleBackColor = true;
        // 
        // opaqueRadioButton
        // 
        opaqueRadioButton.AutoSize = true;
        opaqueRadioButton.Location = new Point(13, 7);
        opaqueRadioButton.Name = "opaqueRadioButton";
        opaqueRadioButton.Size = new Size(72, 19);
        opaqueRadioButton.TabIndex = 6;
        opaqueRadioButton.TabStop = true;
        opaqueRadioButton.Text = "Standard";
        opaqueRadioButton.UseVisualStyleBackColor = true;
        // 
        // panel11
        // 
        panel11.Controls.Add(clickableRadioButton);
        panel11.Controls.Add(clickthroughRadioButton);
        panel11.Controls.Add(normalRadioButton);
        panel11.Location = new Point(21, 40);
        panel11.Name = "panel11";
        panel11.Size = new Size(119, 87);
        panel11.TabIndex = 7;
        // 
        // clickableRadioButton
        // 
        clickableRadioButton.AutoSize = true;
        clickableRadioButton.Location = new Point(13, 56);
        clickableRadioButton.Name = "clickableRadioButton";
        clickableRadioButton.Size = new Size(73, 19);
        clickableRadioButton.TabIndex = 7;
        clickableRadioButton.TabStop = true;
        clickableRadioButton.Text = "Clickable";
        clickableRadioButton.UseVisualStyleBackColor = true;
        // 
        // clickthroughRadioButton
        // 
        clickthroughRadioButton.AutoSize = true;
        clickthroughRadioButton.Location = new Point(13, 32);
        clickthroughRadioButton.Name = "clickthroughRadioButton";
        clickthroughRadioButton.Size = new Size(97, 19);
        clickthroughRadioButton.TabIndex = 1;
        clickthroughRadioButton.TabStop = true;
        clickthroughRadioButton.Text = "Click through";
        clickthroughRadioButton.UseVisualStyleBackColor = true;
        // 
        // normalRadioButton
        // 
        normalRadioButton.AutoSize = true;
        normalRadioButton.Location = new Point(13, 7);
        normalRadioButton.Name = "normalRadioButton";
        normalRadioButton.Size = new Size(72, 19);
        normalRadioButton.TabIndex = 6;
        normalRadioButton.TabStop = true;
        normalRadioButton.Text = "Standard";
        normalRadioButton.UseVisualStyleBackColor = true;
        // 
        // label11
        // 
        label11.AutoSize = true;
        label11.Location = new Point(3, 0);
        label11.Name = "label11";
        label11.Size = new Size(64, 15);
        label11.TabIndex = 0;
        label11.Text = "Panel Type";
        // 
        // popupModePanel
        // 
        popupModePanel.Controls.Add(parallelRadioButton);
        popupModePanel.Controls.Add(serialRadioButton);
        popupModePanel.Controls.Add(label4);
        popupModePanel.Location = new Point(346, 6);
        popupModePanel.Name = "popupModePanel";
        popupModePanel.Size = new Size(334, 108);
        popupModePanel.TabIndex = 27;
        // 
        // fullscreenCheckbox
        // 
        fullscreenCheckbox.AutoSize = true;
        fullscreenCheckbox.Location = new Point(159, 132);
        fullscreenCheckbox.Name = "fullscreenCheckbox";
        fullscreenCheckbox.Size = new Size(83, 19);
        fullscreenCheckbox.TabIndex = 10;
        fullscreenCheckbox.Text = "Full Screen";
        fullscreenCheckbox.UseVisualStyleBackColor = true;
        fullscreenCheckbox.CheckedChanged += fullscreenCheckbox_changed;
        // 
        // parallelRadioButton
        // 
        parallelRadioButton.AutoSize = true;
        parallelRadioButton.Location = new Point(21, 43);
        parallelRadioButton.Name = "parallelRadioButton";
        parallelRadioButton.Size = new Size(198, 19);
        parallelRadioButton.TabIndex = 2;
        parallelRadioButton.TabStop = true;
        parallelRadioButton.Text = "All at once (may cause pc issues)";
        parallelRadioButton.UseVisualStyleBackColor = true;
        parallelRadioButton.CheckedChanged += parallelRadioButton_Changed;
        // 
        // serialRadioButton
        // 
        serialRadioButton.AutoSize = true;
        serialRadioButton.Location = new Point(21, 18);
        serialRadioButton.Name = "serialRadioButton";
        serialRadioButton.Size = new Size(96, 19);
        serialRadioButton.TabIndex = 1;
        serialRadioButton.TabStop = true;
        serialRadioButton.Text = "One at a time";
        serialRadioButton.UseVisualStyleBackColor = true;
        // 
        // label4
        // 
        label4.AutoSize = true;
        label4.Location = new Point(3, 0);
        label4.Name = "label4";
        label4.Size = new Size(94, 15);
        label4.TabIndex = 0;
        label4.Text = "Display Behavior";
        // 
        // popupLengthPanel
        // 
        popupLengthPanel.Controls.Add(longPopupRadioButton);
        popupLengthPanel.Controls.Add(shortPopupRadioButton);
        popupLengthPanel.Controls.Add(label1);
        popupLengthPanel.Location = new Point(6, 6);
        popupLengthPanel.Name = "popupLengthPanel";
        popupLengthPanel.Size = new Size(334, 87);
        popupLengthPanel.TabIndex = 25;
        // 
        // longPopupRadioButton
        // 
        longPopupRadioButton.AutoSize = true;
        longPopupRadioButton.Location = new Point(12, 43);
        longPopupRadioButton.Name = "longPopupRadioButton";
        longPopupRadioButton.Size = new Size(145, 19);
        longPopupRadioButton.TabIndex = 2;
        longPopupRadioButton.TabStop = true;
        longPopupRadioButton.Text = "Long (1 min - 10 mins)";
        longPopupRadioButton.UseVisualStyleBackColor = true;
        longPopupRadioButton.CheckedChanged += longPopupRadioButton_CheckedChanged;
        // 
        // shortPopupRadioButton
        // 
        shortPopupRadioButton.AutoSize = true;
        shortPopupRadioButton.Location = new Point(12, 18);
        shortPopupRadioButton.Name = "shortPopupRadioButton";
        shortPopupRadioButton.Size = new Size(137, 19);
        shortPopupRadioButton.TabIndex = 1;
        shortPopupRadioButton.TabStop = true;
        shortPopupRadioButton.Text = "Short (10 sec - 1 min)";
        shortPopupRadioButton.UseVisualStyleBackColor = true;
        shortPopupRadioButton.CheckedChanged += shortPopupRadioButton_CheckedChanged;
        // 
        // label1
        // 
        label1.AutoSize = true;
        label1.Location = new Point(12, 0);
        label1.Name = "label1";
        label1.Size = new Size(85, 15);
        label1.TabIndex = 0;
        label1.Text = "Display Length";
        // 
        // tabControl1
        // 
        tabControl1.Controls.Add(tabPageGeneral);
        tabControl1.Controls.Add(tabPagePopups);
        tabControl1.Controls.Add(tabPageWallpaper);
        tabControl1.Location = new Point(12, 12);
        tabControl1.Name = "tabControl1";
        tabControl1.SelectedIndex = 0;
        tabControl1.Size = new Size(693, 459);
        tabControl1.TabIndex = 20;
        // 
        // folderBrowserDialog1
        // 
        folderBrowserDialog1.AddToRecent = false;
        folderBrowserDialog1.OkRequiresInteraction = true;
        folderBrowserDialog1.RootFolder = Environment.SpecialFolder.MyComputer;
        folderBrowserDialog1.ShowHiddenFiles = true;
        // 
        // CommandOptionsForm
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        AutoSize = true;
        AutoSizeMode = AutoSizeMode.GrowAndShrink;
        ClientSize = new Size(709, 509);
        Controls.Add(tabControl1);
        Controls.Add(saveAndExitButton);
        MaximumSize = new Size(725, 548);
        MinimumSize = new Size(725, 548);
        Name = "CommandOptionsForm";
        Text = "Options";
        tabPageGeneral.ResumeLayout(false);
        miscOptionsPanel.ResumeLayout(false);
        miscOptionsPanel.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)delayBetweenOutstandingCommandsNumericUpDown).EndInit();
        disallowPanel.ResumeLayout(false);
        disallowPanel.PerformLayout();
        tabPageWallpaper.ResumeLayout(false);
        wallpaperPanel.ResumeLayout(false);
        wallpaperPanel.PerformLayout();
        tabPagePopups.ResumeLayout(false);
        popupTypePanel.ResumeLayout(false);
        popupTypePanel.PerformLayout();
        panel6.ResumeLayout(false);
        panel6.PerformLayout();
        panel5.ResumeLayout(false);
        panel5.PerformLayout();
        panel11.ResumeLayout(false);
        panel11.PerformLayout();
        popupModePanel.ResumeLayout(false);
        popupModePanel.PerformLayout();
        popupLengthPanel.ResumeLayout(false);
        popupLengthPanel.PerformLayout();
        tabControl1.ResumeLayout(false);
        ResumeLayout(false);
    }

    #endregion
    private Button saveAndExitButton;
	private TabPage tabPageGeneral;
	private TabPage tabPagePopups;
	private TabPage tabPageWallpaper;
    private System.Windows.Forms.Panel popupTypePanel;
	private System.Windows.Forms.Panel panel6;
	private RadioButton movingRadioButton;
	private RadioButton stillRadioButton;
	private System.Windows.Forms.Panel panel5;
	private RadioButton seethroughRadioButton;
	private RadioButton opaqueRadioButton;
	private System.Windows.Forms.Panel panel11;
	private RadioButton clickableRadioButton;
	private RadioButton clickthroughRadioButton;
	private RadioButton normalRadioButton;
	private Label label11;
	private Panel miscOptionsPanel;
	private CheckBox webcamCountCheckbox;
	private Label label5;
	private CheckBox showBlockedCheckbox;
	private System.Windows.Forms.Panel popupModePanel;
	private RadioButton parallelRadioButton;
	private RadioButton serialRadioButton;
	private Label label4;
	private Panel wallpaperPanel;
	private System.Windows.Forms.RadioButton scaleToFitRadioButton;
	private System.Windows.Forms.RadioButton scaleStretchRadioButton;
	private System.Windows.Forms.Label label3;
	private System.Windows.Forms.Panel disallowPanel;
	private CheckBox disableMouseCheckbox;
	private CheckBox ttsCheckbox;
	private CheckBox webcamCheckbox;
	private CheckBox reminderCheckbox;
	private CheckBox sendDeleteCheckbox;
	private CheckBox watchForMeCheckbox;
	private CheckBox twitterCheckbox;
	private CheckBox screenshotCheckbox;
	private CheckBox writeForMeCheckbox;
	private CheckBox audioCheckbox;
	private CheckBox subliminalCheckbox;
	private CheckBox messageCheckbox;
	private CheckBox popupCheckbox;
	private System.Windows.Forms.Label label2;
	private CheckBox websiteCheckbox;
	private CheckBox runnableCheckbox;
	private CheckBox wallpaperCheckbox;
	private CheckBox downloadCheckbox;
	private System.Windows.Forms.Panel popupLengthPanel;
	private System.Windows.Forms.RadioButton longPopupRadioButton;
	private System.Windows.Forms.RadioButton shortPopupRadioButton;
	private System.Windows.Forms.Label label1;
	private TabControl tabControl1;
	private System.Windows.Forms.CheckBox fullscreenCheckbox;
	private CheckBox darkModeCheckbox;
	private CheckBox disableInputCheckbox;
    private CheckBox checkBox1;
    private CheckBox checkBox2;
    private Label label7;
    private ComboBox delayCombo;
    private Button buttonSelectNewDownloadFolder;
    private FolderBrowserDialog folderBrowserDialog1;
    private TextBox textBoxDownloadFolder;
    private CheckBox autoRunSentExeCheckBox;
    private CheckBox runAllOutstandingCheckBox;
    private NumericUpDown delayBetweenOutstandingCommandsNumericUpDown;
}