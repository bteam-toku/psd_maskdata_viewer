using PsdMaskDataViewer.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using System.Diagnostics;

namespace PsdMaskDataViewer.App.Dialogs
{
	public partial class InitialSetupDialog : Form
	{
		private readonly IPsd2XmlConverter _converter;  // PSD→XML変換インスタンス

		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="converter"></param>
		public InitialSetupDialog(IPsd2XmlConverter converter)
		{
			// コンポーネント初期化
			InitializeComponent();

			// IPsd2XmlConverterのインスタンスを保存
			_converter = converter;
		}

		/// <summary>
		/// ロードイベント
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		public void InitialSetupDialog_Load(object sender, EventArgs e)
		{
			// アセンブリ情報参照
			var path = Process.GetCurrentProcess().MainModule?.FileName;
			var versionInfo = FileVersionInfo.GetVersionInfo(path!);
			// Version
			if (versionInfo != null)
			{
				// Title
				this.Text = versionInfo.FileDescription + " - 初期セットアップ";
			}
			// 処理中メッセージ設定
			textProcess.Text = "初期セットアップを実行しています。しばらくお待ちください...";
		}

		/// <summary>
		/// Shownイベント
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		public async void InitialSetupDialog_Shown(object sender, EventArgs e)
		{
			// 初期セットアップ処理実行
			bool execResult = _converter.InitialSetup();
			if (execResult)
			{
				this.DialogResult = DialogResult.OK;
			}
			else
			{
				this.DialogResult = DialogResult.Cancel;
			}
			// ダイアログを閉じる
			this.Close();
		}
	}
}
