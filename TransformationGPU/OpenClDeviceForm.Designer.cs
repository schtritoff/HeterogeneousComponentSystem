namespace TransformationGPU
{
    partial class OpenClDeviceForm
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
            this.PlatformComboBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.OkButton = new System.Windows.Forms.Button();
            this.DeviceComboBox = new System.Windows.Forms.ComboBox();
            this.KernelsComboBox = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // PlatformComboBox
            // 
            this.PlatformComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.PlatformComboBox.FormattingEnabled = true;
            this.PlatformComboBox.Location = new System.Drawing.Point(12, 27);
            this.PlatformComboBox.Name = "PlatformComboBox";
            this.PlatformComboBox.Size = new System.Drawing.Size(425, 21);
            this.PlatformComboBox.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Platform";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 64);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Devices";
            // 
            // OkButton
            // 
            this.OkButton.Location = new System.Drawing.Point(356, 171);
            this.OkButton.Name = "OkButton";
            this.OkButton.Size = new System.Drawing.Size(81, 23);
            this.OkButton.TabIndex = 4;
            this.OkButton.Text = "Ok";
            this.OkButton.UseVisualStyleBackColor = true;
            this.OkButton.Click += new System.EventHandler(this.OkButton_Click);
            // 
            // DeviceComboBox
            // 
            this.DeviceComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.DeviceComboBox.FormattingEnabled = true;
            this.DeviceComboBox.Location = new System.Drawing.Point(12, 80);
            this.DeviceComboBox.Name = "DeviceComboBox";
            this.DeviceComboBox.Size = new System.Drawing.Size(425, 21);
            this.DeviceComboBox.TabIndex = 0;
            // 
            // KernelsComboBox
            // 
            this.KernelsComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.KernelsComboBox.FormattingEnabled = true;
            this.KernelsComboBox.Location = new System.Drawing.Point(12, 133);
            this.KernelsComboBox.Name = "KernelsComboBox";
            this.KernelsComboBox.Size = new System.Drawing.Size(425, 21);
            this.KernelsComboBox.TabIndex = 0;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 117);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(42, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Kernels";
            // 
            // OpenClDeviceForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(449, 210);
            this.Controls.Add(this.OkButton);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.KernelsComboBox);
            this.Controls.Add(this.DeviceComboBox);
            this.Controls.Add(this.PlatformComboBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "OpenClDeviceForm";
            this.Text = "OpenCL platform and device";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.ComboBox PlatformComboBox;
        private System.Windows.Forms.Button OkButton;
        public System.Windows.Forms.ComboBox DeviceComboBox;
        public System.Windows.Forms.ComboBox KernelsComboBox;
        private System.Windows.Forms.Label label3;
    }
}