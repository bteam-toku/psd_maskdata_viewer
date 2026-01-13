using System;
using System.Collections.Generic;
using System.Text;

namespace PsdMaskDataViewer.Core.Interfaces
{
	public interface IPsd2XmlConverter
	{
		/// <summary>
		/// 出力受信時のイベント
		/// </summary>
		event Action<string>? OnOutputReceived;

		/// <summary>
		/// 変換処理が実行可能かどうかを判定します
		/// </summary>
		/// <returns><see langword="true"/>:実行可能、<see langword="false"/>:実行不可</returns>
		/// <remarks>例えば、必要なライブラリが存在しない場合などに<see langword="false"/>を返します</remarks>
		bool CanExecute();

		/// <summary>
		/// PSDファイルをXMLファイルに変換します。
		/// </summary>
		/// <param name="inputPsdPath">変換元のPSDファイルのパス</param>
		/// <param name="outputXmlPath">変換先のXMLファイルのパス</param>
		/// <returns><see langword="true"/>:変換成功、<see langword="false"/>:変換失敗</returns>
		bool Convert(string inputPsdPath, string outputXmlPath);

		/// <summary>
		/// 初期設定が必要かどうかを判定します。
		/// </summary>
		/// <returns><see langword="true"/>:初期設定が必要、<see langword="false"/>:初期設定は不要</returns>
		bool NeedInitialSetup();

		/// <summary>
		/// 初期設定を実行します。
		/// </summary>
		/// <returns><see langword="true"/>:初期設定成功、<see langword="false"/>:初期設定失敗</returns>
		bool InitialSetup();

		/// <summary>
		/// 指定フォルダをエディタで開きます。
		/// </summary>
		/// <param name="folderPath">開くフォルダのパス</param>
		/// <returns><see langword="true"/>:エディタが正常に開かれた場合; <see langword="false"/>:エラーが発生した場合。</returns>
		/// <remarks> このメソッドは、VSCodeコントローラを使用して、指定されたフォルダをVisual Studio Codeで開きます。フォルダ指定がnullまたは空文字の場合、VSCodeがコマンドラインから使用可能かどうかを確認します。</remarks>
		bool OpenEditor(string? folderPath);
	}
}
