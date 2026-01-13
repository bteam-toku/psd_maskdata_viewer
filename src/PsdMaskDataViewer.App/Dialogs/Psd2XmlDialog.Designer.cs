namespace PsdMaskDataViewer.App.Dialogs
{
    partial class Psd2XmlDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Psd2XmlDialog));
            btnPsdPath = new Button();
            txtPsdPath = new TextBox();
            btnXmlPath = new Button();
            txtXmlPath = new TextBox();
            btnConvert = new Button();
            labelPsd = new Label();
            label1 = new Label();
            rtextConsole = new RichTextBox();
            SuspendLayout();
            // 
            // btnPsdPath
            // 
            btnPsdPath.BackgroundImage = (Image)resources.GetObject("btnPsdPath.BackgroundImage");
            btnPsdPath.BackgroundImageLayout = ImageLayout.Stretch;
            btnPsdPath.Font = new Font("Meiryo UI", 9.75F);
            btnPsdPath.Location = new Point(16, 80);
            btnPsdPath.Margin = new Padding(6);
            btnPsdPath.Name = "btnPsdPath";
            btnPsdPath.Size = new Size(70, 70);
            btnPsdPath.TabIndex = 0;
            btnPsdPath.UseVisualStyleBackColor = true;
            btnPsdPath.Click += BtnPsdPath_Click;
            // 
            // txtPsdPath
            // 
            txtPsdPath.AllowDrop = true;
            txtPsdPath.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtPsdPath.Font = new Font("Yu Gothic UI", 9.75F);
            txtPsdPath.Location = new Point(96, 104);
            txtPsdPath.Margin = new Padding(6);
            txtPsdPath.Name = "txtPsdPath";
            txtPsdPath.Size = new Size(1229, 42);
            txtPsdPath.TabIndex = 1;
            txtPsdPath.DragDrop += TxtPsdPath_DragDrop;
            txtPsdPath.DragEnter += TxtPsdPath_DragEnter;
            // 
            // btnXmlPath
            // 
            btnXmlPath.BackgroundImage = Properties.Resources.folder;
            btnXmlPath.BackgroundImageLayout = ImageLayout.Stretch;
            btnXmlPath.Font = new Font("Meiryo UI", 9.75F);
            btnXmlPath.Location = new Point(16, 240);
            btnXmlPath.Margin = new Padding(6);
            btnXmlPath.Name = "btnXmlPath";
            btnXmlPath.Size = new Size(70, 70);
            btnXmlPath.TabIndex = 2;
            btnXmlPath.UseVisualStyleBackColor = true;
            btnXmlPath.Click += BtnXmlPath_Click;
            // 
            // txtXmlPath
            // 
            txtXmlPath.AllowDrop = true;
            txtXmlPath.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtXmlPath.Font = new Font("Yu Gothic UI", 9.75F);
            txtXmlPath.Location = new Point(96, 264);
            txtXmlPath.Margin = new Padding(6);
            txtXmlPath.Name = "txtXmlPath";
            txtXmlPath.Size = new Size(1229, 42);
            txtXmlPath.TabIndex = 3;
            txtXmlPath.DragDrop += TxtXmlPath_DragDrop;
            txtXmlPath.DragEnter += TxtXmlPath_DragEnter;
            // 
            // btnConvert
            // 
            btnConvert.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            btnConvert.Font = new Font("Meiryo UI", 9.75F);
            btnConvert.Location = new Point(16, 340);
            btnConvert.Margin = new Padding(6);
            btnConvert.Name = "btnConvert";
            btnConvert.Size = new Size(1309, 84);
            btnConvert.TabIndex = 4;
            btnConvert.Text = "変換実行（PSD⇒XML）";
            btnConvert.UseVisualStyleBackColor = true;
            btnConvert.Click += BtnConvert_Click;
            // 
            // labelPsd
            // 
            labelPsd.AutoSize = true;
            labelPsd.Font = new Font("Yu Gothic UI", 10.875F);
            labelPsd.Location = new Point(16, 32);
            labelPsd.Name = "labelPsd";
            labelPsd.Size = new Size(396, 40);
            labelPsd.TabIndex = 5;
            labelPsd.Text = "インプットのフォルダ選択（PSD）";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Yu Gothic UI", 10.875F);
            label1.Location = new Point(16, 192);
            label1.Name = "label1";
            label1.Size = new Size(424, 40);
            label1.TabIndex = 6;
            label1.Text = "アウトプットのフォルダ選択（XML）";
            // 
            // rtextConsole
            // 
            rtextConsole.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            rtextConsole.BackColor = SystemColors.ControlText;
            rtextConsole.Font = new Font("ＭＳ ゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point, 128);
            rtextConsole.ForeColor = SystemColors.HighlightText;
            rtextConsole.Location = new Point(16, 463);
            rtextConsole.Name = "rtextConsole";
            rtextConsole.ReadOnly = true;
            rtextConsole.Size = new Size(1309, 491);
            rtextConsole.TabIndex = 7;
            rtextConsole.Text = "";
            // 
            // Psd2XmlDialog
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1343, 981);
            Controls.Add(rtextConsole);
            Controls.Add(label1);
            Controls.Add(labelPsd);
            Controls.Add(btnConvert);
            Controls.Add(txtXmlPath);
            Controls.Add(btnXmlPath);
            Controls.Add(txtPsdPath);
            Controls.Add(btnPsdPath);
            Margin = new Padding(6);
            MinimumSize = new Size(1100, 530);
            Name = "Psd2XmlDialog";
            Text = "Psd2Xml";
            Load += Psd2XmlDialog_Load;
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion

        private Button btnPsdPath;
        private TextBox txtPsdPath;
        private Button btnXmlPath;
        private TextBox txtXmlPath;
        private Button btnConvert;
        private Label labelPsd;
        private Label label1;
        private RichTextBox rtextConsole;
    }
}