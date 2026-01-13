namespace PsdMaskDataViewer.App.Dialogs
{
    partial class InitialSetupDialog
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
            textProcess = new Label();
            SuspendLayout();
            // 
            // textProcess
            // 
            textProcess.AutoSize = true;
            textProcess.Location = new Point(49, 63);
            textProcess.Name = "textProcess";
            textProcess.Size = new Size(302, 32);
            textProcess.TabIndex = 0;
            textProcess.Text = "アプリケーションを初期化中・・・";
            // 
            // InitialSetupDialog
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(702, 165);
            ControlBox = false;
            Controls.Add(textProcess);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "InitialSetupDialog";
            Text = "初期化";
            Load += InitialSetupDialog_Load;
            Shown += InitialSetupDialog_Shown;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label textProcess;
    }
}