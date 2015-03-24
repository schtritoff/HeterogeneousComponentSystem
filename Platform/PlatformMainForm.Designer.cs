namespace Platform
{
    partial class PlatformMainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PlatformMainForm));
            this.LoadImageButton = new System.Windows.Forms.Button();
            this.SplitContainerMain = new System.Windows.Forms.SplitContainer();
            this.TransformationMetricLabel = new System.Windows.Forms.Label();
            this.PreviousImageButton = new System.Windows.Forms.Button();
            this.LoadComponentsButton = new System.Windows.Forms.Button();
            this.NextImageButton = new System.Windows.Forms.Button();
            this.TransformationsListBox = new System.Windows.Forms.ListBox();
            this.ImageNumberLabel = new System.Windows.Forms.Label();
            this.TransformButton = new System.Windows.Forms.Button();
            this.TransformationInfoLabel = new System.Windows.Forms.Label();
            this.ImageBoxMain = new System.Windows.Forms.PictureBox();
            this.LoaderPictureBox = new System.Windows.Forms.PictureBox();
            this.RemoveImageButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.SplitContainerMain)).BeginInit();
            this.SplitContainerMain.Panel1.SuspendLayout();
            this.SplitContainerMain.Panel2.SuspendLayout();
            this.SplitContainerMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ImageBoxMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.LoaderPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // LoadImageButton
            // 
            this.LoadImageButton.Location = new System.Drawing.Point(18, 19);
            this.LoadImageButton.Name = "LoadImageButton";
            this.LoadImageButton.Size = new System.Drawing.Size(160, 23);
            this.LoadImageButton.TabIndex = 0;
            this.LoadImageButton.Text = "Load image";
            this.LoadImageButton.UseVisualStyleBackColor = true;
            this.LoadImageButton.Click += new System.EventHandler(this.LoadImageButton_Click);
            // 
            // SplitContainerMain
            // 
            this.SplitContainerMain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.SplitContainerMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SplitContainerMain.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.SplitContainerMain.IsSplitterFixed = true;
            this.SplitContainerMain.Location = new System.Drawing.Point(0, 0);
            this.SplitContainerMain.Name = "SplitContainerMain";
            // 
            // SplitContainerMain.Panel1
            // 
            this.SplitContainerMain.Panel1.Controls.Add(this.RemoveImageButton);
            this.SplitContainerMain.Panel1.Controls.Add(this.LoaderPictureBox);
            this.SplitContainerMain.Panel1.Controls.Add(this.TransformationMetricLabel);
            this.SplitContainerMain.Panel1.Controls.Add(this.LoadImageButton);
            this.SplitContainerMain.Panel1.Controls.Add(this.PreviousImageButton);
            this.SplitContainerMain.Panel1.Controls.Add(this.LoadComponentsButton);
            this.SplitContainerMain.Panel1.Controls.Add(this.NextImageButton);
            this.SplitContainerMain.Panel1.Controls.Add(this.TransformationsListBox);
            this.SplitContainerMain.Panel1.Controls.Add(this.ImageNumberLabel);
            this.SplitContainerMain.Panel1.Controls.Add(this.TransformButton);
            this.SplitContainerMain.Panel1.Controls.Add(this.TransformationInfoLabel);
            // 
            // SplitContainerMain.Panel2
            // 
            this.SplitContainerMain.Panel2.Controls.Add(this.ImageBoxMain);
            this.SplitContainerMain.Size = new System.Drawing.Size(734, 461);
            this.SplitContainerMain.SplitterDistance = 200;
            this.SplitContainerMain.TabIndex = 1;
            // 
            // TransformationMetricLabel
            // 
            this.TransformationMetricLabel.AutoSize = true;
            this.TransformationMetricLabel.Location = new System.Drawing.Point(18, 299);
            this.TransformationMetricLabel.Name = "TransformationMetricLabel";
            this.TransformationMetricLabel.Size = new System.Drawing.Size(0, 13);
            this.TransformationMetricLabel.TabIndex = 9;
            // 
            // PreviousImageButton
            // 
            this.PreviousImageButton.Location = new System.Drawing.Point(18, 48);
            this.PreviousImageButton.Name = "PreviousImageButton";
            this.PreviousImageButton.Size = new System.Drawing.Size(35, 23);
            this.PreviousImageButton.TabIndex = 1;
            this.PreviousImageButton.Text = "<<";
            this.PreviousImageButton.UseVisualStyleBackColor = true;
            this.PreviousImageButton.Click += new System.EventHandler(this.PreviousImageButton_Click);
            // 
            // LoadComponentsButton
            // 
            this.LoadComponentsButton.Location = new System.Drawing.Point(18, 117);
            this.LoadComponentsButton.Name = "LoadComponentsButton";
            this.LoadComponentsButton.Size = new System.Drawing.Size(160, 23);
            this.LoadComponentsButton.TabIndex = 8;
            this.LoadComponentsButton.Text = "Load components";
            this.LoadComponentsButton.UseVisualStyleBackColor = true;
            this.LoadComponentsButton.Click += new System.EventHandler(this.LoadComponentsButton_Click);
            // 
            // NextImageButton
            // 
            this.NextImageButton.Location = new System.Drawing.Point(59, 48);
            this.NextImageButton.Name = "NextImageButton";
            this.NextImageButton.Size = new System.Drawing.Size(35, 23);
            this.NextImageButton.TabIndex = 2;
            this.NextImageButton.Text = ">>";
            this.NextImageButton.UseVisualStyleBackColor = true;
            this.NextImageButton.Click += new System.EventHandler(this.NextImageButton_Click);
            // 
            // TransformationsListBox
            // 
            this.TransformationsListBox.FormattingEnabled = true;
            this.TransformationsListBox.Location = new System.Drawing.Point(18, 176);
            this.TransformationsListBox.Name = "TransformationsListBox";
            this.TransformationsListBox.Size = new System.Drawing.Size(160, 82);
            this.TransformationsListBox.TabIndex = 5;
            this.TransformationsListBox.Visible = false;
            // 
            // ImageNumberLabel
            // 
            this.ImageNumberLabel.AutoSize = true;
            this.ImageNumberLabel.Location = new System.Drawing.Point(109, 54);
            this.ImageNumberLabel.Name = "ImageNumberLabel";
            this.ImageNumberLabel.Size = new System.Drawing.Size(24, 13);
            this.ImageNumberLabel.TabIndex = 3;
            this.ImageNumberLabel.Text = "0/0";
            // 
            // TransformButton
            // 
            this.TransformButton.Location = new System.Drawing.Point(18, 266);
            this.TransformButton.Name = "TransformButton";
            this.TransformButton.Size = new System.Drawing.Size(160, 23);
            this.TransformButton.TabIndex = 6;
            this.TransformButton.Text = "Transform current image";
            this.TransformButton.UseVisualStyleBackColor = true;
            this.TransformButton.Visible = false;
            this.TransformButton.Click += new System.EventHandler(this.TransformButton_Click);
            // 
            // TransformationInfoLabel
            // 
            this.TransformationInfoLabel.AutoSize = true;
            this.TransformationInfoLabel.Location = new System.Drawing.Point(18, 145);
            this.TransformationInfoLabel.Name = "TransformationInfoLabel";
            this.TransformationInfoLabel.Size = new System.Drawing.Size(158, 26);
            this.TransformationInfoLabel.TabIndex = 7;
            this.TransformationInfoLabel.Text = "Available image transformations \r\nfrom loaded components";
            this.TransformationInfoLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.TransformationInfoLabel.Visible = false;
            // 
            // ImageBoxMain
            // 
            this.ImageBoxMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ImageBoxMain.Location = new System.Drawing.Point(0, 0);
            this.ImageBoxMain.Name = "ImageBoxMain";
            this.ImageBoxMain.Size = new System.Drawing.Size(528, 459);
            this.ImageBoxMain.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.ImageBoxMain.TabIndex = 0;
            this.ImageBoxMain.TabStop = false;
            // 
            // LoaderPictureBox
            // 
            this.LoaderPictureBox.Image = ((System.Drawing.Image)(resources.GetObject("LoaderPictureBox.Image")));
            this.LoaderPictureBox.Location = new System.Drawing.Point(72, 340);
            this.LoaderPictureBox.Name = "LoaderPictureBox";
            this.LoaderPictureBox.Size = new System.Drawing.Size(35, 34);
            this.LoaderPictureBox.TabIndex = 10;
            this.LoaderPictureBox.TabStop = false;
            this.LoaderPictureBox.Visible = false;
            // 
            // RemoveImageButton
            // 
            this.RemoveImageButton.Location = new System.Drawing.Point(153, 48);
            this.RemoveImageButton.Name = "RemoveImageButton";
            this.RemoveImageButton.Size = new System.Drawing.Size(23, 23);
            this.RemoveImageButton.TabIndex = 11;
            this.RemoveImageButton.Text = "X";
            this.RemoveImageButton.UseVisualStyleBackColor = true;
            this.RemoveImageButton.Click += new System.EventHandler(this.RemoveImageButton_Click);
            // 
            // PlatformMainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(734, 461);
            this.Controls.Add(this.SplitContainerMain);
            this.Name = "PlatformMainForm";
            this.Text = "Heterogeneous Component System - Platform";
            this.Load += new System.EventHandler(this.PlatformMainForm_Load);
            this.SplitContainerMain.Panel1.ResumeLayout(false);
            this.SplitContainerMain.Panel1.PerformLayout();
            this.SplitContainerMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.SplitContainerMain)).EndInit();
            this.SplitContainerMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ImageBoxMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.LoaderPictureBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button LoadImageButton;
        private System.Windows.Forms.SplitContainer SplitContainerMain;
        private System.Windows.Forms.PictureBox ImageBoxMain;
        private System.Windows.Forms.Button NextImageButton;
        private System.Windows.Forms.Button PreviousImageButton;
        private System.Windows.Forms.Label ImageNumberLabel;
        private System.Windows.Forms.Label TransformationInfoLabel;
        private System.Windows.Forms.Button TransformButton;
        private System.Windows.Forms.ListBox TransformationsListBox;
        private System.Windows.Forms.Button LoadComponentsButton;
        private System.Windows.Forms.Label TransformationMetricLabel;
        private System.Windows.Forms.PictureBox LoaderPictureBox;
        private System.Windows.Forms.Button RemoveImageButton;
    }
}

