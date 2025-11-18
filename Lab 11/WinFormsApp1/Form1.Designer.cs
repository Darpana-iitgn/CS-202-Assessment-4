namespace WinFormsApp1
{
    partial class EventPlayground
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            lblStatus = new Label();
            btnChangeColor = new Button();
            btnChangeText = new Button();
            cmbColors = new ComboBox();
            SuspendLayout();
            // 
            // lblStatus
            // 
            lblStatus.AutoSize = true;
            lblStatus.Location = new Point(339, 25);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(163, 20);
            lblStatus.TabIndex = 0;
            lblStatus.Text = "Welcome to Events Lab";
            // 
            // btnChangeColor
            // 
            btnChangeColor.Location = new Point(99, 93);
            btnChangeColor.Name = "btnChangeColor";
            btnChangeColor.Size = new Size(196, 29);
            btnChangeColor.TabIndex = 1;
            btnChangeColor.Text = "Change Color";
            btnChangeColor.UseVisualStyleBackColor = true;
            btnChangeColor.Click += btnChangeColor_Click;
            // 
            // btnChangeText
            // 
            btnChangeText.Location = new Point(99, 128);
            btnChangeText.Name = "btnChangeText";
            btnChangeText.Size = new Size(196, 29);
            btnChangeText.TabIndex = 2;
            btnChangeText.Text = "Change Text";
            btnChangeText.UseVisualStyleBackColor = true;
            btnChangeText.Click += btnChangeText_Click;
            // 
            // cmbColors
            // 
            cmbColors.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbColors.FormattingEnabled = true;
            cmbColors.Items.AddRange(new object[] { "Red", "Green", "Blue" });
            cmbColors.Location = new Point(351, 129);
            cmbColors.Name = "cmbColors";
            cmbColors.Size = new Size(151, 28);
            cmbColors.TabIndex = 3;
            // 
            // EventPlayground
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(870, 417);
            Controls.Add(cmbColors);
            Controls.Add(btnChangeText);
            Controls.Add(btnChangeColor);
            Controls.Add(lblStatus);
            Name = "EventPlayground";
            Text = "EventPlayground";
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblStatus;
        private Button btnChangeColor;
        private Button btnChangeText;
        private ComboBox cmbColors;
    }
}
