namespace PsdMaskDataViewer.App.Forms
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            ListViewItem listViewItem5 = new ListViewItem(new string[] { "X座標", "" }, -1, Color.Empty, Color.Empty, new Font("Yu Gothic UI", 9F));
            ListViewItem listViewItem6 = new ListViewItem(new string[] { "Y座標", "" }, -1, Color.Empty, Color.Empty, new Font("Yu Gothic UI", 9F));
            ListViewItem listViewItem7 = new ListViewItem(new string[] { "幅", "" }, -1, Color.Empty, Color.Empty, new Font("Yu Gothic UI", 9F));
            ListViewItem listViewItem8 = new ListViewItem(new string[] { "高さ", "" }, -1, Color.Empty, Color.Empty, new Font("Yu Gothic UI", 9F));
            treeView = new TreeView();
            txtSearchKey = new TextBox();
            btnSearch = new Button();
            btnSearchPrev = new Button();
            btnSearchNext = new Button();
            txtSeleceNodeName = new TextBox();
            listView = new ListView();
            ColumnHeader1 = new ColumnHeader();
            ColumnHeader2 = new ColumnHeader();
            pictureBox = new PictureBox();
            menuStrip = new MenuStrip();
            MenuItemFile = new ToolStripMenuItem();
            MenuItemXMLOpen = new ToolStripMenuItem();
            toolStripSeparator1 = new ToolStripSeparator();
            MenuItemExit = new ToolStripMenuItem();
            MenuItemEdit = new ToolStripMenuItem();
            MenuItemSearch = new ToolStripMenuItem();
            MenuItemSearchNext = new ToolStripMenuItem();
            MenuItemSearchPrev = new ToolStripMenuItem();
            toolStripSeparator2 = new ToolStripSeparator();
            MenuItemPSDConvert = new ToolStripMenuItem();
            MenuItemView = new ToolStripMenuItem();
            MenuItemTreeViewExpand = new ToolStripMenuItem();
            MenuItemTreeViewFold = new ToolStripMenuItem();
            MenuItemHelp = new ToolStripMenuItem();
            MenuItemVersion = new ToolStripMenuItem();
            comboBox = new ComboBox();
            panelSearch = new Panel();
            btnSerachExif = new Button();
            MenuItemOpenVSCode = new ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)pictureBox).BeginInit();
            menuStrip.SuspendLayout();
            panelSearch.SuspendLayout();
            SuspendLayout();
            // 
            // treeView
            // 
            treeView.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            treeView.Location = new Point(544, 192);
            treeView.Margin = new Padding(6);
            treeView.Name = "treeView";
            treeView.Size = new Size(1017, 1000);
            treeView.TabIndex = 0;
            treeView.AfterSelect += TreeView_AfterSelect;
            // 
            // txtSearchKey
            // 
            txtSearchKey.Font = new Font("Yu Gothic UI", 10.875F);
            txtSearchKey.Location = new Point(16, 40);
            txtSearchKey.Margin = new Padding(4, 2, 4, 2);
            txtSearchKey.Name = "txtSearchKey";
            txtSearchKey.Size = new Size(336, 46);
            txtSearchKey.TabIndex = 6;
            txtSearchKey.KeyDown += TxtSearchKey_KeyDown;
            // 
            // btnSearch
            // 
            btnSearch.BackgroundImage = (Image)resources.GetObject("btnSearch.BackgroundImage");
            btnSearch.BackgroundImageLayout = ImageLayout.Stretch;
            btnSearch.Font = new Font("Meiryo UI", 10.125F);
            btnSearch.Location = new Point(368, 40);
            btnSearch.Margin = new Padding(4, 2, 4, 2);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(48, 48);
            btnSearch.TabIndex = 7;
            btnSearch.UseVisualStyleBackColor = true;
            btnSearch.Click += BtnSearch_Click;
            // 
            // btnSearchPrev
            // 
            btnSearchPrev.Font = new Font("Meiryo UI", 10.875F);
            btnSearchPrev.Location = new Point(16, 144);
            btnSearchPrev.Margin = new Padding(4, 2, 4, 2);
            btnSearchPrev.Name = "btnSearchPrev";
            btnSearchPrev.Size = new Size(464, 48);
            btnSearchPrev.TabIndex = 8;
            btnSearchPrev.Text = "▲　前候補（Shist+F3）";
            btnSearchPrev.TextAlign = ContentAlignment.MiddleLeft;
            btnSearchPrev.UseVisualStyleBackColor = true;
            btnSearchPrev.Click += BtnSearchPrev_Click;
            // 
            // btnSearchNext
            // 
            btnSearchNext.Font = new Font("Meiryo UI", 10.875F);
            btnSearchNext.Location = new Point(16, 224);
            btnSearchNext.Margin = new Padding(4, 2, 4, 2);
            btnSearchNext.Name = "btnSearchNext";
            btnSearchNext.Size = new Size(464, 48);
            btnSearchNext.TabIndex = 9;
            btnSearchNext.Text = "▼　次候補（F3）";
            btnSearchNext.TextAlign = ContentAlignment.MiddleLeft;
            btnSearchNext.UseVisualStyleBackColor = true;
            btnSearchNext.Click += BtnSearchNext_Click;
            // 
            // txtSeleceNodeName
            // 
            txtSeleceNodeName.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtSeleceNodeName.BorderStyle = BorderStyle.FixedSingle;
            txtSeleceNodeName.Location = new Point(16, 136);
            txtSeleceNodeName.Margin = new Padding(6);
            txtSeleceNodeName.Name = "txtSeleceNodeName";
            txtSeleceNodeName.ReadOnly = true;
            txtSeleceNodeName.Size = new Size(1552, 39);
            txtSeleceNodeName.TabIndex = 10;
            // 
            // listView
            // 
            listView.Columns.AddRange(new ColumnHeader[] { ColumnHeader1, ColumnHeader2 });
            listView.Font = new Font("Yu Gothic UI", 10.125F);
            listView.Items.AddRange(new ListViewItem[] { listViewItem5, listViewItem6, listViewItem7, listViewItem8 });
            listView.Location = new Point(16, 192);
            listView.Margin = new Padding(6);
            listView.Name = "listView";
            listView.Size = new Size(502, 220);
            listView.TabIndex = 11;
            listView.UseCompatibleStateImageBehavior = false;
            listView.View = View.Details;
            // 
            // ColumnHeader1
            // 
            ColumnHeader1.Text = "項目";
            ColumnHeader1.Width = 134;
            // 
            // ColumnHeader2
            // 
            ColumnHeader2.Text = "値";
            ColumnHeader2.Width = 134;
            // 
            // pictureBox
            // 
            pictureBox.BackColor = Color.AliceBlue;
            pictureBox.BorderStyle = BorderStyle.FixedSingle;
            pictureBox.Location = new Point(16, 432);
            pictureBox.Margin = new Padding(6);
            pictureBox.Name = "pictureBox";
            pictureBox.Size = new Size(503, 433);
            pictureBox.TabIndex = 12;
            pictureBox.TabStop = false;
            // 
            // menuStrip
            // 
            menuStrip.ImageScalingSize = new Size(32, 32);
            menuStrip.Items.AddRange(new ToolStripItem[] { MenuItemFile, MenuItemEdit, MenuItemView, MenuItemHelp });
            menuStrip.Location = new Point(0, 0);
            menuStrip.Name = "menuStrip";
            menuStrip.Size = new Size(1574, 42);
            menuStrip.TabIndex = 13;
            menuStrip.Text = "menuStrip1";
            // 
            // MenuItemFile
            // 
            MenuItemFile.DropDownItems.AddRange(new ToolStripItem[] { MenuItemXMLOpen, MenuItemOpenVSCode, toolStripSeparator1, MenuItemExit });
            MenuItemFile.Name = "MenuItemFile";
            MenuItemFile.Size = new Size(128, 38);
            MenuItemFile.Text = "ファイル(&F)";
            // 
            // MenuItemXMLOpen
            // 
            MenuItemXMLOpen.Name = "MenuItemXMLOpen";
            MenuItemXMLOpen.ShortcutKeys = Keys.Control | Keys.O;
            MenuItemXMLOpen.Size = new Size(438, 44);
            MenuItemXMLOpen.Text = "XMLフォルダを開く(&O)";
            MenuItemXMLOpen.Click += MenuItemXMLOpen_Click;
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new Size(435, 6);
            // 
            // MenuItemExit
            // 
            MenuItemExit.Name = "MenuItemExit";
            MenuItemExit.Size = new Size(438, 44);
            MenuItemExit.Text = "終了(&X)";
            MenuItemExit.Click += MenuItemExit_Click;
            // 
            // MenuItemEdit
            // 
            MenuItemEdit.DropDownItems.AddRange(new ToolStripItem[] { MenuItemSearch, MenuItemSearchNext, MenuItemSearchPrev, toolStripSeparator2, MenuItemPSDConvert });
            MenuItemEdit.Name = "MenuItemEdit";
            MenuItemEdit.Size = new Size(101, 38);
            MenuItemEdit.Text = "編集&E)";
            // 
            // MenuItemSearch
            // 
            MenuItemSearch.Name = "MenuItemSearch";
            MenuItemSearch.ShortcutKeys = Keys.Control | Keys.F;
            MenuItemSearch.Size = new Size(390, 44);
            MenuItemSearch.Text = "検索(&F)";
            MenuItemSearch.Click += MenuItemSearch_Click;
            // 
            // MenuItemSearchNext
            // 
            MenuItemSearchNext.Name = "MenuItemSearchNext";
            MenuItemSearchNext.ShortcutKeys = Keys.F3;
            MenuItemSearchNext.Size = new Size(390, 44);
            MenuItemSearchNext.Text = "次候補に移動";
            MenuItemSearchNext.Click += MenuItemSearchNext_Click;
            // 
            // MenuItemSearchPrev
            // 
            MenuItemSearchPrev.Name = "MenuItemSearchPrev";
            MenuItemSearchPrev.ShortcutKeys = Keys.Shift | Keys.F3;
            MenuItemSearchPrev.Size = new Size(390, 44);
            MenuItemSearchPrev.Text = "前候補に移動";
            MenuItemSearchPrev.Click += MenuItemSearchPrev_Click;
            // 
            // toolStripSeparator2
            // 
            toolStripSeparator2.Name = "toolStripSeparator2";
            toolStripSeparator2.Size = new Size(387, 6);
            // 
            // MenuItemPSDConvert
            // 
            MenuItemPSDConvert.Name = "MenuItemPSDConvert";
            MenuItemPSDConvert.Size = new Size(390, 44);
            MenuItemPSDConvert.Text = "PSDをXMLに変換(&C)";
            MenuItemPSDConvert.Click += MenuItemPSDConvert_Click;
            // 
            // MenuItemView
            // 
            MenuItemView.DropDownItems.AddRange(new ToolStripItem[] { MenuItemTreeViewExpand, MenuItemTreeViewFold });
            MenuItemView.Name = "MenuItemView";
            MenuItemView.Size = new Size(111, 38);
            MenuItemView.Text = "表示(&V)";
            // 
            // MenuItemTreeViewExpand
            // 
            MenuItemTreeViewExpand.Name = "MenuItemTreeViewExpand";
            MenuItemTreeViewExpand.Size = new Size(373, 44);
            MenuItemTreeViewExpand.Text = "ツリー表示を展開(&E)";
            MenuItemTreeViewExpand.Click += MenuItemTreeViewExpand_Click;
            // 
            // MenuItemTreeViewFold
            // 
            MenuItemTreeViewFold.Name = "MenuItemTreeViewFold";
            MenuItemTreeViewFold.Size = new Size(373, 44);
            MenuItemTreeViewFold.Text = "ツリー表示を折り畳む(&F)";
            MenuItemTreeViewFold.Click += MenuItemTreeViewFold_Click;
            // 
            // MenuItemHelp
            // 
            MenuItemHelp.DropDownItems.AddRange(new ToolStripItem[] { MenuItemVersion });
            MenuItemHelp.Name = "MenuItemHelp";
            MenuItemHelp.Size = new Size(123, 38);
            MenuItemHelp.Text = "ヘルプ(&H)";
            // 
            // MenuItemVersion
            // 
            MenuItemVersion.Name = "MenuItemVersion";
            MenuItemVersion.Size = new Size(506, 44);
            MenuItemVersion.Text = "psd mask viewerのバージョン情報(&A)";
            MenuItemVersion.Click += MenuItemVersion_Click;
            // 
            // comboBox
            // 
            comboBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            comboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox.Font = new Font("Yu Gothic UI", 10.125F);
            comboBox.FormattingEnabled = true;
            comboBox.Location = new Point(16, 56);
            comboBox.Name = "comboBox";
            comboBox.Size = new Size(1552, 45);
            comboBox.TabIndex = 14;
            comboBox.SelectedIndexChanged += ComboBox_SelectedIndexChanged;
            // 
            // panelSearch
            // 
            panelSearch.BackColor = Color.DimGray;
            panelSearch.Controls.Add(btnSerachExif);
            panelSearch.Controls.Add(txtSearchKey);
            panelSearch.Controls.Add(btnSearch);
            panelSearch.Controls.Add(btnSearchPrev);
            panelSearch.Controls.Add(btnSearchNext);
            panelSearch.Location = new Point(16, 888);
            panelSearch.Name = "panelSearch";
            panelSearch.Size = new Size(496, 304);
            panelSearch.TabIndex = 15;
            // 
            // btnSerachExif
            // 
            btnSerachExif.BackgroundImage = (Image)resources.GetObject("btnSerachExif.BackgroundImage");
            btnSerachExif.BackgroundImageLayout = ImageLayout.Stretch;
            btnSerachExif.Font = new Font("Yu Gothic UI", 10.125F);
            btnSerachExif.Location = new Point(432, 40);
            btnSerachExif.Name = "btnSerachExif";
            btnSerachExif.Size = new Size(48, 48);
            btnSerachExif.TabIndex = 7;
            btnSerachExif.UseVisualStyleBackColor = true;
            btnSerachExif.Click += BtnSerachExif_Click;
            // 
            // MenuItemOpenVSCode
            // 
            MenuItemOpenVSCode.Name = "MenuItemOpenVSCode";
            MenuItemOpenVSCode.Size = new Size(438, 44);
            MenuItemOpenVSCode.Text = "Visual Studio Codeで開く";
            MenuItemOpenVSCode.Click += MenuItemOpenVSCode_Click;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1574, 1209);
            Controls.Add(panelSearch);
            Controls.Add(comboBox);
            Controls.Add(pictureBox);
            Controls.Add(txtSeleceNodeName);
            Controls.Add(treeView);
            Controls.Add(listView);
            Controls.Add(menuStrip);
            MainMenuStrip = menuStrip;
            Margin = new Padding(6);
            MinimumSize = new Size(1600, 1280);
            Name = "MainForm";
            Text = "Main Form";
            Load += MainForm_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBox).EndInit();
            menuStrip.ResumeLayout(false);
            menuStrip.PerformLayout();
            panelSearch.ResumeLayout(false);
            panelSearch.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TreeView treeView;
		private TextBox txtSearchKey;
		private Button btnSearch;
		private Button btnSearchPrev;
		private Button btnSearchNext;
		private TextBox txtSeleceNodeName;
		private ListView listView;
		private ColumnHeader ColumnHeader1;
		private ColumnHeader ColumnHeader2;
		private PictureBox pictureBox;
		private MenuStrip menuStrip;
		private ToolStripMenuItem MenuItemFile;
		private ToolStripMenuItem MenuItemEdit;
		private ToolStripMenuItem MenuItemView;
		private ToolStripMenuItem MenuItemHelp;
		private ToolStripMenuItem MenuItemXMLOpen;
		private ToolStripSeparator toolStripSeparator1;
		private ToolStripMenuItem MenuItemExit;
		private ToolStripMenuItem MenuItemTreeViewExpand;
		private ToolStripMenuItem MenuItemTreeViewFold;
		private ComboBox comboBox;
		private Panel panelSearch;
		private Button btnSerachExif;
		private ToolStripMenuItem MenuItemSearch;
		private ToolStripSeparator toolStripSeparator2;
		private ToolStripMenuItem MenuItemPSDConvert;
		private ToolStripMenuItem MenuItemSearchNext;
		private ToolStripMenuItem MenuItemSearchPrev;
		private ToolStripMenuItem MenuItemVersion;
        private ToolStripMenuItem MenuItemOpenVSCode;
    }
}