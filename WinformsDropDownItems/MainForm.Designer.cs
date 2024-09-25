namespace WinformsDropDownItems
{
    partial class MainForm
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
            components = new System.ComponentModel.Container();
            label = new Label();
            RightClickWindow = new ContextMenuStrip(components);
            SuspendLayout();
            // 
            // label
            // 
            label.AutoSize = true;
            label.Font = new Font("Segoe UI", 15F);
            label.Location = new Point(87, 87);
            label.Name = "label";
            label.Size = new Size(302, 41);
            label.TabIndex = 0;
            label.Text = "Right-Click Anywhere";
            label.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // RightClickWindow
            // 
            RightClickWindow.ImageScalingSize = new Size(24, 24);
            RightClickWindow.Name = "contextMenuStrip";
            RightClickWindow.Size = new Size(61, 4);
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(478, 244);
            Controls.Add(label);
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Main Form";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label;
        private ContextMenuStrip RightClickWindow;
    }
}
