using PsdMaskDataViewer.Core.Interfaces;
using System;
using System.Diagnostics;
using System.Windows.Forms;
using System.Reflection;

namespace PsdMaskDataViewer.App.Dialogs
{
    public partial class Psd2XmlDialog : Form
    {
        // private変数
        private readonly IPsd2XmlConverter _converter;  // PSD→XML変換インスタンス
        private string? _inputPsdPath;                  // 変換元PSDファイルのフォルダ
        private string? _outputXmlPath;                  // 出力先XMLファイルのフォルダ

        //
        // コンストラクタ
        //
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="converter">IPsd2XmlConverterのインスタンス</param>
        public Psd2XmlDialog(IPsd2XmlConverter converter)
        {
            InitializeComponent();

            // IPsd2XmlConverterのインスタンスを保存
            _converter = converter;

            // 初期値設定
            _inputPsdPath = Properties.Settings.Default.LastInputPath;
            _outputXmlPath = Properties.Settings.Default.LastOutputPath;
            txtPsdPath.Text = _inputPsdPath;
            txtXmlPath.Text = _outputXmlPath;

            // コントロール状態の更新
            UpdateControlEnabled();

            // 出力受信イベントハンドラ登録
            _converter.OnOutputReceived += (msg) =>
            {
                if (rtextConsole.IsHandleCreated)
                {
                    // 出力メッセージを表示
                    rtextConsole.BeginInvoke(new Action(() =>
                    {
                        // メッセージを追加して最新行にスクロール
                        rtextConsole.AppendText(msg + Environment.NewLine);
                        rtextConsole.ScrollToCaret();
                    }));
                }
            };
        }

        //
        // publicメソッド
        //
        /// <summary>
        /// 出力先XMLフォルダ取得
        /// </summary>
        /// <returns>出力先XMLフォルダ</returns>
        public string? GetOutputXmlPath()
        {
            return _outputXmlPath;
        }

        //
        // privateメソッド
        //
        /// <summary>
        /// コントロール状態更新
        /// </summary>
        private void UpdateControlEnabled()
        {
            // 変換実行ボタンの有効/無効設定
            btnConvert.Enabled = Directory.Exists(_inputPsdPath) && Directory.Exists(_outputXmlPath) && _converter.CanExecute();
        }

        //
        // イベントハンドラ
        //
        /// <summary>
        /// ロードイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Psd2XmlDialog_Load(object sender, EventArgs e)
        {
            // アセンブリ情報参照
            var path = Process.GetCurrentProcess().MainModule?.FileName;
            var versionInfo = FileVersionInfo.GetVersionInfo(path!);
            // Version
            if (versionInfo != null)
            {
                // Title
                this.Text = versionInfo.FileDescription + " - PSD→XML変換";
            }
        }
        /// <summary>
        /// 入力フォルダ選択ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnPsdPath_Click(object sender, EventArgs e)
        {
            // 初期パス設定
            _inputPsdPath = txtPsdPath.Text;
            var tempPath = string.IsNullOrEmpty(_inputPsdPath) ? Path.GetDirectoryName(Application.ExecutablePath) + @"./" : _inputPsdPath;

            // フォルダ選択ダイアログ表示
            FolderBrowserDialog? fbdialog = new()
            {
                Description = @"入力フォルダの選択",
                SelectedPath = tempPath,
                ShowNewFolderButton = false
            };
            // 選択されたフォルダを設定
            if (fbdialog.ShowDialog() == DialogResult.OK)
            {
                // 選択されたフォルダを設定
                _inputPsdPath = fbdialog.SelectedPath;
                txtPsdPath.Text = _inputPsdPath;
                // 設定ファイルへ保存
                Properties.Settings.Default.LastInputPath = _inputPsdPath;
                Properties.Settings.Default.Save();
            }
            // ダイアログ破棄
            fbdialog.Dispose();
            // コントロール状態の更新
            UpdateControlEnabled();
        }
        /// <summary>
        /// 出力先フォルダ選択ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnXmlPath_Click(object sender, EventArgs e)
        {
            // 初期パス設定
            _outputXmlPath = txtXmlPath.Text;
            var tempPath = string.IsNullOrEmpty(_outputXmlPath) ? Path.GetDirectoryName(Application.ExecutablePath) + @"./" : _outputXmlPath;

            // フォルダ選択ダイアログ表示
            FolderBrowserDialog? fbdialog = new()
            {
                Description = @"出力先フォルダの選択",
                SelectedPath = tempPath,
                ShowNewFolderButton = true
            };
            // 選択されたフォルダを設定
            if (fbdialog.ShowDialog() == DialogResult.OK)
            {
                // 選択されたフォルダを設定
                _outputXmlPath = fbdialog.SelectedPath;
                txtXmlPath.Text = _outputXmlPath;
                // 設定ファイルへ保存
                Properties.Settings.Default.LastOutputPath = _outputXmlPath;
                Properties.Settings.Default.Save();
            }
            fbdialog.Dispose();
            // コントロール状態の更新
            UpdateControlEnabled();
        }
        /// <summary>
        /// 変換実行ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void BtnConvert_Click(object sender, EventArgs e)
        {
            if (_converter.CanExecute())
            {
                // 連打防止
                btnConvert.Enabled = false; // 連打防止のため無効化
                // コンソールをクリアして開始メッセージ表示
                rtextConsole.Clear();   // コンソールクリア
                rtextConsole.AppendText("PSDファイルのXML変換を開始します..." + Environment.NewLine);

                // 変換実行（バックグラウンド）
                bool execResult = await Task.Run(() => _converter.Convert(_inputPsdPath!, _outputXmlPath!));

                // 変換結果表示
                if (execResult)
                {
                    // 完了メッセージ表示
                    MessageBox.Show("PSDファイルのXML変換が正常に完了しました。", "変換完了", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    // ダイアログ終了
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    // エラーメッセージ表示
                    MessageBox.Show("PSDファイルのXML変換中にエラーが発生しました。出力コンソールを確認してください。", "変換エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    // コントロール状態の更新
                    UpdateControlEnabled();
                }
            }
        }
        /// <summary>
        /// 入力フォルダテキストボックス_ドラッグ&ドロップ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TxtPsdPath_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data != null && e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                // 文字列取得
                string[]? input = (string[]?)e.Data.GetData(DataFormats.FileDrop, false);
                if (input != null && input.Length > 0)
                {
                    _inputPsdPath = input[0];
                    txtPsdPath.Text = _inputPsdPath;
                    // コントロール状態の更新
                    UpdateControlEnabled();
                }
            }
        }
        /// <summary>
        /// 入力フォルダテキストボックス_ドラッグ&ドロップ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TxtPsdPath_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.All;
        }
        /// <summary>
        /// 出力先フォルダテキストボックス_ドラッグ&ドロップ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TxtXmlPath_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data != null && e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                // 文字列取得
                string[]? input = (string[]?)e.Data.GetData(DataFormats.FileDrop, false);
                if (input != null && input.Length > 0)
                {
                    _outputXmlPath = input[0];
                    txtXmlPath.Text = _outputXmlPath;
                    // コントロール状態の更新
                    UpdateControlEnabled();
                }
            }
        }
        /// <summary>
        /// 出力先フォルダテキストボックス_ドラッグ&ドロップ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TxtXmlPath_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.All;
        }
    }
}