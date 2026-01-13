using Microsoft.Extensions.Configuration;
using PsdMaskDataViewer.App.Dialogs;
using PsdMaskDataViewer.App.Forms;
using PsdMaskDataViewer.Core.Factories;
using PsdMaskDataViewer.Core.VSCode;

namespace PsdMaskDataViewer.App
{
	internal static class Program
	{
		/// <summary>
		///  The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			ApplicationConfiguration.Initialize();

			// 設定ファイルの読み込み
			var config = new ConfigurationBuilder()
				.SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
				.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
				.Build();
			// クラス名の取得
			string? adapterClass = config.GetSection("AdapterSettings")["AdapterClass"];
			// Factoryを使用してIPsd2XmlConverterのインスタンスを取得
			var converter = Psd2XmlConverterFactory.Create(adapterClass);

			// 初期設定が必要か確認	
			if (converter.NeedInitialSetup())
			{
				// 初期設定ダイアログを表示
				using (var setup = new InitialSetupDialog(converter))
				{
					// 初期設定ダイアログを表示
					var result = setup.ShowDialog();
					// 異常終了の場合はアプリケーションを終了
					if (result != DialogResult.OK)
					{
						MessageBox.Show("初期設定が完了しなかったため、アプリケーションを終了します。", "初期設定未完了", MessageBoxButtons.OK, MessageBoxIcon.Warning);
						return;
					}
				}
			}

			// MainFormにconverterを渡す
			Application.Run(new MainForm(converter));
		}
	}
}