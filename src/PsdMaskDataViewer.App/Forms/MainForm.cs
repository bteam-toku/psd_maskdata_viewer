using PsdMaskDataViewer.Core.Interfaces;
using PsdMaskDataViewer.App.Dialogs;
using System.IO;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;
using System.Runtime.CompilerServices;
using System.Text;
using System.Reflection;
using System.Diagnostics;

namespace PsdMaskDataViewer.App.Forms
{
    public partial class MainForm : Form
    {
        // メンバー変数
        private readonly IPsd2XmlConverter _converter;  // PSD→XML変換オブジェクト
        private string? _importXmlPath = null;  // 取り込みXMLフォルダパス
        private string? _targetXmlFilename;     // 分析対象XMLファイル名
        private TreeNode? _currentTreeNode;     // TreeViewのカレントノード
        private List<TreeNode>? _searchResults;         // キーワード検索結果ノードリスト

        //
        // コンストラクタ
        //
        /// <summary>
        /// MainFormコンストラクタ
        /// </summary>
        public MainForm(IPsd2XmlConverter converter)
        {
            // コンポーネント初期化
            InitializeComponent();

            // PSD→XML変換オブジェクト保存
            _converter = converter;

            // 設定ファイルから最後に使用したXMLフォルダパスを取得
            _importXmlPath = Properties.Settings.Default.LastImportPath;
            // ComboboxにXMLファイル名リストを設定
            SetListToCombobox();
            // Comboboxの先頭要素を選択
            if (comboBox.Items.Count > 0)
            {
                comboBox.SelectedIndex = 0;
            }

            // コントロール状態を更新
            UpdateControlEnabled();

            // 検索パネルを非表示
            panelSearch.Visible = false;

            // PSD→XML変換メニューの有効/無効を設定
            MenuItemPSDConvert.Enabled = _converter.CanExecute();
        }

        //
        // 業務ロジック実装
        //
        /// <summary>
        /// コントロールのEnabled状態を更新する
        /// </summary>
        /// <returns>なし</returns>
        private void UpdateControlEnabled()
        {
            // インポートディレクトリーが選択されている場合はComboBoxを有効化
            bool hasImportDir = !String.IsNullOrEmpty(_importXmlPath) && Directory.Exists(_importXmlPath);
            comboBox.Enabled = hasImportDir;
            if (!hasImportDir)
            {
                txtSeleceNodeName.Text = "XMLフォルダが未選択です。メニューの[ファイル]-[XMLフォルダを開く]からXMLフォルダを選択してください。";
            }

            // インポートディレクトリーが選択されていて、VSCodeが使用可能な場合はVSCodeで開くメニューを有効化
            MenuItemOpenVSCode.Enabled = hasImportDir && _converter.OpenEditor(null);   //OPenEditor(null)でCanExecuteを呼び出す

            // TreeViewノードがある場合はTreeViewを制御するメニューを有効化
            bool hasNodes = treeView.Nodes.Count > 0;
            MenuItemSearch.Enabled = hasNodes;
            MenuItemTreeViewExpand.Enabled = hasNodes;
            MenuItemTreeViewFold.Enabled = hasNodes;

            // 検索結果がある場合は検索ナビゲーションメニューを有効化
            bool isSearchble = panelSearch.Visible == true;
            MenuItemSearchNext.Enabled = isSearchble;
            MenuItemSearchPrev.Enabled = isSearchble;
        }

        /// <summary>
        /// ComboboxにXMLファイル名リストを設定する
        /// </summary>
        /// <returns>なし</returns>
        private void SetListToCombobox()
        {
            // インポートXMLフォルダが設定されていない場合は処理を抜ける
            if (String.IsNullOrEmpty(_importXmlPath) || !Directory.Exists(_importXmlPath))
            {
                return;
            }
            // フォルダ内のXMLファイル名リストを取得
            var fnames = Directory.GetFiles(_importXmlPath, "*.xml")
                                  .Select(Path.GetFileNameWithoutExtension)
                                  .OfType<string>()
                                  .ToArray();
            // Comboboxにリストを設定
            if (fnames.Any())
            {
                comboBox.BeginUpdate();             // 更新開始
                comboBox.Items.Clear();             // 既存アイテムクリア
                comboBox.Items.AddRange(fnames);    // アイテム追加
                comboBox.EndUpdate();               // 更新終了
            }
        }

        /// <summary>
        /// XMLファイルを解析してTreeViewノードを生成する
        /// </summary>
        /// <param name="fname">XMLファイル名</param>
        /// <returns>なし</returns>
        private void ParseXmlToTreeview(string fname)
        {
            try
            {
                treeView.BeginUpdate(); // 更新開始
                treeView.Nodes.Clear(); // 既存ノードクリア

                // XMLファイルオープン
                using XmlReader reader = XmlReader.Create(fname);

                // Elementの頭出し
                TreeNode? root = null;
                while (reader.Read())
                {
                    if (reader.NodeType == XmlNodeType.Element)
                    {
                        root = new TreeNode(reader.GetAttribute("name") ?? "no_name");
                        treeView.Nodes.Add(root);
                        break;
                    }
                }

                // ルートノードが存在する場合
                if (root != null)
                {
                    // 再帰処理でXMLを解析してTreeViewノードを作成
                    ParserXmlToTreeviewSub(reader, root);

                    // 後処理
                    treeView.ExpandAll();               // 全展開
                    treeView.Nodes[0].EnsureVisible();  // 先頭ノードに移動
                    treeView.SelectedNode = root;       // 先頭ノードを選択
                    _currentTreeNode = root;            // カレントノードを保存
                    treeView.Focus();                   // フォーカス移動
                }

                // XMLファイルクローズ
            }
            catch
            {
                // 初期化
                treeView.Nodes.Clear();
            }
            finally
            {
                treeView.EndUpdate();   // 更新終了
            }
        }

        /// <summary>
        /// XMLファイルを解析してTreeViewノードを生成する再帰処理
        /// </summary>
        /// <param name="reader">XMLリーダー</param>
        /// <param name="node">親TreeNode</param>
        /// <returns>なし</returns>
        private void ParserXmlToTreeviewSub(XmlReader reader, TreeNode node)
        {
            while (reader.Read())
            {
                // ファイル終端またはEND-Elementの場合は処理終了
                if (reader.EOF || reader.NodeType == XmlNodeType.EndElement)
                {
                    break;
                }

                // Elementノードの場合はTreeNodeを追加
                if (reader.NodeType == XmlNodeType.Element)
                {
                    // name属性を取得
                    string name = reader.GetAttribute("name") ?? "no_name";
                    // Element名で処理分岐
                    switch (reader.Name)
                    {
                        case "group":    // Elementが"group"の場合
                                         // TreeViewにノード追加
                            TreeNode tn = new(name);
                            node.Nodes.Add(tn);
                            // 再帰処理で段下げ
                            ParserXmlToTreeviewSub(reader, tn);
                            break;
                        case "node":  // Elementが"node"の場合
                                      // TreeViewにノード追加
                            node.Nodes.Add(new TreeNode(name)
                            {
                                // Tag情報にマスク情報を設定
                                Tag = new Dictionary<string, string>
                                {
                                    ["x"] = reader.GetAttribute("x") ?? "",
                                    ["y"] = reader.GetAttribute("y") ?? "",
                                    ["w"] = reader.GetAttribute("w") ?? "",
                                    ["h"] = reader.GetAttribute("h") ?? ""
                                }
                            });
                            break;
                    }
                }
            }
        }

        /// <summary>
        /// キーワードでTreeViewのノードを検索して結果をTreeNodeリストとして出力する
        /// </summary>
        /// <param name="key">検索キーワード</param>
        /// <param name="node">検索対象のTreeNode</param>
        /// <returns>なし</returns>
        private void SearchTreeView(string key, TreeNode node)
        {
            // 初期化
            _searchResults = null;
            _searchResults = new List<TreeNode>();

            // 検索処理を呼び出し
            SearchTreeViewSub(key, node);
        }

        /// <summary>
        /// キーワードでTreeViewのノードを検索する再帰処理
        /// </summary>
        /// <param name="key">検索キーワード</param>
        /// <param name="node">検索対象のTreeNode</param>
        /// <returns>なし</returns>
        private void SearchTreeViewSub(string key, TreeNode node)
        {
            // 検索結果リストが初期化されていない場合は処理を抜ける
            if (_searchResults == null)
            {
                return;
            }
            // キーワードで検索する
            foreach (TreeNode tn in node.Nodes)
            {
                if (tn.Text.CompareTo(key) == 0)    // キーワードと一致
                {
                    _searchResults.Add(tn);    // 検索結果リストに追加
                    tn.BackColor = Color.Yellow;    // 強調色付与
                }
                else if (tn.Text.Contains(key))
                {
                    _searchResults.Add(tn);    // 検索結果リストに追加
                    tn.BackColor = Color.LightYellow;    // 強調色付与
                }
                else
                {
                    tn.BackColor = Color.White; // 強調色解除
                }

                // 再帰処理
                if (tn.Nodes.Count > 0)
                {
                    SearchTreeViewSub(key, tn); // 検索再帰処理
                }
            }
        }

        /// <summary>
        /// TreeView検索前候補取得
        /// </summary>
        /// <returns>なし</returns>
        private void SearchTreeViewPrevNode()
        {
            // 検索結果が無い場合は処理を抜ける
            if (_searchResults is not { Count: > 0 } || _currentTreeNode == null)
            {
                return;
            }
            // 検索候補にカレントノードがあるか判定
            int index = _searchResults.IndexOf(_currentTreeNode);
            // 前の検索候補を指定、範囲外の場合は最後の候補を指定
            index = (index <= 0) ? _searchResults.Count - 1 : index - 1;
            // 指定した検索候補ノードにフォーカス
            _currentTreeNode = _searchResults[index];
        }
        /// <summary>
        /// TreeView検索次候補取得
        /// </summary>
        /// <returns>なし</returns>
        private void SearchTreeViewNextNode()
        {
            if (_searchResults is not { Count: > 0 } || _currentTreeNode == null)
            {
                return;
            }

            // 検索候補にカレントノードがあるか判定
            int index = _searchResults.IndexOf(_currentTreeNode);
            // 次の検索候補を指定、範囲外の場合は最初の候補を指定
            index = (index < 0 || index >= _searchResults.Count - 1) ? 0 : index + 1;
            // 指定した検索候補ノードにフォーカス
            _currentTreeNode = _searchResults[index];
        }
        /// <summary>
        /// 検索解除処理
        /// </summary>
        /// <param name="node"></param>
        private void SearchTreeViewDispose(TreeNode node)
        {
            // 検索結果とキーワード初期化
            _searchResults = null;
            txtSearchKey.Text = null;

            // 自身の強調色クリア
            node.BackColor = Color.White;

            // 強調色クリア再帰処理呼び出し
            SearchTreeViewDisposeSub(node);
        }
        /// <summary>
        /// 検索解除処理の再帰処理
        /// </summary>
        /// <param name="node"></param>
        private void SearchTreeViewDisposeSub(TreeNode node)
        {
            // 強調色クリア
            foreach (TreeNode tn in node.Nodes)
            {
                tn.BackColor = Color.White; // 強調色解除

                if (tn.Nodes.Count > 0)
                {
                    SearchTreeViewDisposeSub(tn); // 再帰処理
                }
            }
        }
        /// <summary>
        /// マスクのサンプル画像（赤い矩形）を描画する
        /// </summary>
        /// <param name="x">X座標</param>
        /// <param name="y">Y座標</param>
        /// <param name="w">幅</param>
        /// <param name="h">高さ</param>
        /// <returns>なし</returns>
        /// <remarks>pictureBoxのサイズ(Width: 800, Height: 600)に合わせてスケーリングして描画します。</remarks>
        private void DrawSamplePicture(int x, int y, int w, int h)
        {
            // 既存のイメージがある場合は破棄
            pictureBox.Image?.Dispose();

            // Graphicsオブジェクトを生成
            Bitmap canv = new(pictureBox.Width, pictureBox.Height);

            // Graphicsオブジェクトで矩形を描画
            using (Graphics graph = Graphics.FromImage(canv))
            {
                double scaleX = (double)pictureBox.Width / 800;
                double scaleY = (double)pictureBox.Height / 600;
                // 座標系変換して矩形を描画
                int xx = (int)(x * scaleX);
                int yy = (int)(y * scaleY);
                int ww = (int)(w * scaleX);
                int hh = (int)(h * scaleY);
                graph.FillRectangle(Brushes.Red, xx, yy, ww, hh);
            }

            // pictureBoxにイメージ表示
            pictureBox.Image = canv;
        }

        //
        // イベントハンドラ実装
        //
        /// <summary>
        /// ロードイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void MainForm_Load(object sender, EventArgs e)
        {
            // アセンブリ情報参照
            var path = Process.GetCurrentProcess().MainModule?.FileName;
            var versionInfo = FileVersionInfo.GetVersionInfo(path!);
            // Version
            if (versionInfo != null)
            {
                // Title
                this.Text = versionInfo.FileDescription + " - メイン";
            }
        }

        /// <summary>
        /// XMLフォルダを開く_メニュー選択
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItemXMLOpen_Click(object sender, EventArgs e)
        {
            DialogResult result;

            // フォルダ選択ダイアログ表示
            using FolderBrowserDialog fddialog = new()
            {
                Description = "XMLのフォルダを指定",
                ShowNewFolderButton = false,
                SelectedPath = _importXmlPath ?? AppDomain.CurrentDomain.BaseDirectory
            };
            // ダイアログ表示
            result = fddialog.ShowDialog();
            // OKの場合
            if (result == DialogResult.OK)
            {
                // ディレクトリーを更新
                _importXmlPath = fddialog.SelectedPath;
                // 設定ファイルへ保存
                Properties.Settings.Default.LastImportPath = _importXmlPath;
                Properties.Settings.Default.Save();
                // Comboboxにリストを設定
                SetListToCombobox();
                // Comboboxの先頭要素を選択
                if (comboBox.Items.Count > 0)
                {
                    comboBox.SelectedIndex = 0;
                }
            }
            // 状態更新
            UpdateControlEnabled();
        }
        /// <summary>
        /// VSCodeで開く_メニュー選択
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
		private void MenuItemOpenVSCode_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(_importXmlPath) || !Directory.Exists(_importXmlPath))
            {
                return;
            }
            // VSCodeコントローラを使用してVSCodeを起動
            _converter.OpenEditor(_importXmlPath);
            // 状態更新
            UpdateControlEnabled();
        }
        /// <summary>
        /// コンボボックスでアイテム(ファイル名)選択
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox.SelectedItem == null || String.IsNullOrEmpty(_importXmlPath) || !Directory.Exists(_importXmlPath))
            {
                return;
            }
            // TreeView生成
            _targetXmlFilename = Path.Combine(_importXmlPath, comboBox.SelectedItem.ToString() + ".xml");
            if (File.Exists(_targetXmlFilename))
            {
                ParseXmlToTreeview(_targetXmlFilename);
            }
            // 状態更新
            UpdateControlEnabled();
        }
        /// <summary>
        /// TreeViewのノード選択
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            // カレントNodeを更新
            TreeNode? tn = e.Node ?? null;
            if (tn == null) return;

            // カレントノードを保存
            _currentTreeNode = tn;

            // 更新処理
            txtSeleceNodeName.Text = tn.FullPath;
            txtSeleceNodeName.Select(txtSeleceNodeName.Text.Length, 0);
            Clipboard.Clear();
            // サンプル画像破棄
            pictureBox.Image?.Dispose();
            pictureBox.Image = null;

            // ノードのTag情報を取得
            if (tn.Tag is IDictionary<string, string> map)
            {
                // クリップボードに設定
                Clipboard.SetText(map["x"] + "\t" + map["y"] + "\t" + map["w"] + "\t" + map["h"] + "\t" + tn.FullPath);
                // リストビューに設定
                listView.Items[0].SubItems[1].Text = map["x"];
                listView.Items[1].SubItems[1].Text = map["y"];
                listView.Items[2].SubItems[1].Text = map["w"];
                listView.Items[3].SubItems[1].Text = map["h"];

                // サンプル画像描画
                _ = int.TryParse(map["x"], out int x);
                _ = int.TryParse(map["y"], out int y);
                _ = int.TryParse(map["w"], out int w);
                _ = int.TryParse(map["h"], out int h);
                DrawSamplePicture(x, y, w, h);
            }
            else
            {
                // クリップボードに設定
                Clipboard.SetText(tn.FullPath);
                // リストビューに設定
                listView.Items[0].SubItems[1].Text = "";
                listView.Items[1].SubItems[1].Text = "";
                listView.Items[2].SubItems[1].Text = "";
                listView.Items[3].SubItems[1].Text = "";
            }
            // コントロール状態の更新
            UpdateControlEnabled();
        }
        /// <summary>
        /// ツリー表示を展開_メニュー選択
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItemTreeViewExpand_Click(object sender, EventArgs e)
        {
            // ツリー全展開
            treeView.BeginUpdate();
            treeView.ExpandAll();
            treeView.SelectedNode = _currentTreeNode;
            _currentTreeNode?.EnsureVisible();
            treeView.Focus();       // カレントノードにフォーカス移動
            treeView.EndUpdate();

            // コントロール状態の更新
            UpdateControlEnabled();
        }
        /// <summary>
        /// ツリー表示を折り畳む_メニュー選択
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItemTreeViewFold_Click(object sender, EventArgs e)
        {
            // ツリー全折り畳み
            treeView.BeginUpdate();
            treeView.CollapseAll();
            treeView.Nodes[0].EnsureVisible();
            treeView.EndUpdate();

            // コントロール状態の更新
            UpdateControlEnabled();
        }
        /// <summary>
        /// 検索_メニュー選択
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItemSearch_Click(object sender, EventArgs e)
        {
            // 検索パネル表示
            panelSearch.Visible = true;
            // 検索文字列にフォーカス
            txtSearchKey.Focus();
            // コントロール状態の更新
            UpdateControlEnabled();
        }
        /// <summary>
        /// 検索終了ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSerachExif_Click(object sender, EventArgs e)
        {
            // 検索解除
            treeView.BeginUpdate();
            SearchTreeViewDispose(treeView.Nodes[0]);
            treeView.EndUpdate();
            // 検索パネル非表示
            panelSearch.Visible = false;
            // コントロール状態の更新
            UpdateControlEnabled();
        }
        /// <summary>
        /// 検索実行ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSearch_Click(object sender, EventArgs e)
        {
            if (treeView.Nodes.Count == 0)
            {
                return;
            }

            // 検索文字列が未入力の場合は検索解除する
            string searchKey = txtSearchKey.Text;
            if (string.IsNullOrEmpty(searchKey))
            {
                treeView.BeginUpdate();
                SearchTreeViewDispose(treeView.Nodes[0]);
                treeView.EndUpdate();
            }
            else
            {
                // 検索実行
                treeView.BeginUpdate();
                SearchTreeView(searchKey, treeView.Nodes[0]);
                treeView.EndUpdate();

                if (_searchResults is { Count: > 0 })
                {
                    // 検索結果の先頭ノードにフォーカス
                    _currentTreeNode = _searchResults[0];
                    treeView.SelectedNode = _currentTreeNode;
                    _currentTreeNode.EnsureVisible();
                    treeView.Focus();   // カレントノードにフォーカス移動
                }
            }
            // コントロール状態の更新
            UpdateControlEnabled();
        }
        /// <summary>
        /// 検索文字列テキストボックスキー入力
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TxtSearchKey_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    // 検索実行
                    BtnSearch_Click(sender, e);
                    e.SuppressKeyPress = true;
                    break;
                default:
                    break;
            }
        }
        /// <summary>
        /// 検索前候補ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSearchPrev_Click(object sender, EventArgs e)
        {
            // 検索未実施の場合は無処理
            if (_searchResults is not { Count: > 0 })
            {
                return;
            }

            // 前候補検索
            SearchTreeViewPrevNode();
            // フォーカス設定
            treeView.SelectedNode = _currentTreeNode;
            _currentTreeNode?.EnsureVisible();
            treeView.Focus();   // カレントノードにフォーカス移動

            // コントロール状態の更新
            UpdateControlEnabled();
        }
        /// <summary>
        /// 検索次候補ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSearchNext_Click(object sender, EventArgs e)
        {
            // 検索未実施の場合は無処理
            if (_searchResults is not { Count: > 0 })
            {
                return;
            }

            // 次候補検索
            SearchTreeViewNextNode();
            // フォーカス設定
            treeView.SelectedNode = _currentTreeNode;
            _currentTreeNode?.EnsureVisible();
            treeView.Focus();   // カレントノードにフォーカス移動

            // コントロール状態の更新
            UpdateControlEnabled();
        }
        /// <summary>
        /// 次候補へ移動_メニュー選択
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItemSearchNext_Click(object sender, EventArgs e)
        {
            BtnSearchNext_Click(sender, e);
        }
        /// <summary>
        /// 前候補へ移動_メニュー選択
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItemSearchPrev_Click(object sender, EventArgs e)
        {
            BtnSearchPrev_Click(sender, e);
        }
        /// <summary>
        /// アプリ終了
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItemExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        /// <summary>
        /// バージョン情報_メニュー選択
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItemVersion_Click(object sender, EventArgs e)
        {
            using VersionDialog vd = new()
            {
                StartPosition = FormStartPosition.CenterParent
            };
            vd.ShowDialog();
        }
        /// <summary>
        /// PSDをXMLに変換_メニュー選択
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItemPSDConvert_Click(object sender, EventArgs e)
        {
            // 変換実行処理
            using Psd2XmlDialog psd2XmlDialog = new(_converter)
            {
                StartPosition = FormStartPosition.CenterParent
            };
            psd2XmlDialog.ShowDialog();
            // 変換実行されていたら出力先を設定
            string? outputPath = psd2XmlDialog.GetOutputXmlPath();
            if (!string.IsNullOrEmpty(outputPath))
            {
                // インポートXMLフォルダパスを設定
                _importXmlPath = outputPath;
                // 設定ファイルへ保存
                Properties.Settings.Default.LastImportPath = _importXmlPath;
                Properties.Settings.Default.Save();
                // Comboboxにリストを設定
                SetListToCombobox();
                // Comboboxの先頭要素を選択
                if (comboBox.Items.Count > 0)
                {
                    comboBox.SelectedIndex = 0;
                }
                // 状態更新
                UpdateControlEnabled();
            }
        }
    }
}