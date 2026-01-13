namespace PsdMaskDataViewer.App.Dialogs
{
    partial class VersionDialog
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
            textProduct = new TextBox();
            btnOK = new Button();
            textVersion = new TextBox();
            textCopyright = new TextBox();
            textDescription = new TextBox();
            SuspendLayout();
            // 
            // textProduct
            // 
            textProduct.BorderStyle = BorderStyle.None;
            textProduct.Location = new Point(32, 88);
            textProduct.Name = "textProduct";
            textProduct.ReadOnly = true;
            textProduct.Size = new Size(736, 32);
            textProduct.TabIndex = 0;
            textProduct.TabStop = false;
            textProduct.Text = "アプリケーション名";
            // 
            // btnOK
            // 
            btnOK.DialogResult = DialogResult.OK;
            btnOK.Location = new Point(336, 376);
            btnOK.Name = "btnOK";
            btnOK.Size = new Size(150, 46);
            btnOK.TabIndex = 1;
            btnOK.Text = "OK";
            btnOK.UseVisualStyleBackColor = true;
            // 
            // textVersion
            // 
            textVersion.BorderStyle = BorderStyle.None;
            textVersion.Location = new Point(32, 152);
            textVersion.Name = "textVersion";
            textVersion.ReadOnly = true;
            textVersion.Size = new Size(736, 32);
            textVersion.TabIndex = 2;
            textVersion.Text = "バージョン";
            // 
            // textCopyright
            // 
            textCopyright.BorderStyle = BorderStyle.None;
            textCopyright.Location = new Point(32, 216);
            textCopyright.Name = "textCopyright";
            textCopyright.ReadOnly = true;
            textCopyright.Size = new Size(736, 32);
            textCopyright.TabIndex = 3;
            textCopyright.Text = "著作権";
            // 
            // textDescription
            // 
            textDescription.BorderStyle = BorderStyle.None;
            textDescription.Location = new Point(32, 280);
            textDescription.Multiline = true;
            textDescription.Name = "textDescription";
            textDescription.ReadOnly = true;
            textDescription.Size = new Size(736, 72);
            textDescription.TabIndex = 4;
            textDescription.Text = "説明";
            // 
            // VersionDialog
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(textDescription);
            Controls.Add(textCopyright);
            Controls.Add(textVersion);
            Controls.Add(btnOK);
            Controls.Add(textProduct);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "VersionDialog";
            Text = "Version";
            Load += VersionDialog_Load;
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion

        private TextBox textProduct;
        private Button btnOK;
        private TextBox textVersion;
        private TextBox textCopyright;
        private TextBox textDescription;
    }
}