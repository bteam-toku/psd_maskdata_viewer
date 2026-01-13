using PsdMaskDataViewer.Core.Interfaces;
using PsdMaskDataViewer.Core.Docker;
using PsdMaskDataViewer.Core.PowerShell;
using PsdMaskDataViewer.Core.VSCode;
using System;
using System.IO;
using System.Runtime.CompilerServices;

namespace PsdMaskDataViewer.Core.Adaptors
{
    public class Psd2XmlConverterAdaptorBase : IPsd2XmlConverter
    {
        // 依存するコントローラのインスタンス
        protected readonly DockerControllerBase? _dockerController;
        protected readonly PowerShellControllerBase? _powerShellController;
        protected readonly VSCodeControllerBase? _vsCodeController;

        /// <summary>
        /// 出力受信時のイベント
        /// </summary>
        public event Action<string>? OnOutputReceived;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="dockerController">依存するDockerコントローラのインスタンス</param>
        /// <param name="powerShellController">依存するPowerShellコントローラのインスタンス</param>
        public Psd2XmlConverterAdaptorBase(DockerControllerBase dockerController, PowerShellControllerBase powerShellController, VSCodeControllerBase vsCodeController)
        {
            // コンストラクタで依存関係を注入
            _dockerController = dockerController;
            _powerShellController = powerShellController;
            _vsCodeController = vsCodeController;
            // 出力受信イベントの中継設定
            _powerShellController.OnOutputReceived += (msg) =>
            {
                OnOutputReceived?.Invoke(msg);
            };
        }

        public Psd2XmlConverterAdaptorBase()
        {
            // カスタムのAdaptorを作成する場合は、こちらのデフォルトコンストラクタを使用して、
            // 依存するコントローラのインスタンスを自分で生成してください。
        }

        /// <summary>
        /// 変換処理が実行可能かどうかを判定します
        /// </summary>
        /// <returns><see langword="true"/>:dockerが実行可能; <see langword="false"/>:実行不可。</returns>
        public virtual bool CanExecute()
        {
            if (_dockerController == null || _powerShellController == null)
            {
                // 依存するコントローラが設定されていない場合は実行不可
                return false;
            }
            // DockerとPowerShellの両方が実行可能かどうかを判定します。
            return _dockerController.CanExecute() && _powerShellController.CanExecute();
        }

        /// <summary>
        /// PSDファイルをXMLファイルに変換します。
        /// </summary>
        /// <param name="inputPsdPath">変換元のPSDファイルのパス</param>
        /// <param name="outputXmlPath">変換先のXMLファイルのパス</param>
        /// <returns><see langword="true"/>:変換成功、<see langword="false"/>:変換失敗</returns>
        public virtual bool Convert(string inputPsdPath, string outputXmlPath)
        {
            if (_dockerController == null || _powerShellController == null)
            {
                // 依存するコントローラが設定されていない場合は変換失敗
                return false;
            }
            // 変換実行結果
            bool execResult;

            // 絶対パスに変換
            string absInputPsdPath = System.IO.Path.GetFullPath(inputPsdPath);
            string absOutputXmlPath = System.IO.Path.GetFullPath(outputXmlPath);

            // Dockerのrunコマンドを実行する
            if (!CanExecute()) return false; // 実行不可の場合はfalseを返します。
            execResult = _powerShellController.Execute(_dockerController.BuildRunCommand(absInputPsdPath, absOutputXmlPath));
            if (!execResult) return false; // runコマンドの実行に失敗した場合はfalseを返します。

            return true;  // 変換成功
        }

        /// <summary>
        /// 初期セットアップが必要かどうかを判定します。
        /// </summary>
        /// <returns><see langword="true"/>:初期セットアップが必要、<see langword="false"/>:不要</returns>
        public virtual bool NeedInitialSetup()
        {
            if (_dockerController == null)
            {
                // 依存するコントローラが設定されていない場合はセットアップ不要
                return false;
            }
            // Dockerイメージがない場合は初期セットアップが必要
            return !_dockerController.IsImageAvailable();
        }

        /// <summary>
        /// 初期セットアップを実行します。
        /// </summary>
        /// <returns><see langword="true"/>:セットアップ成功、<see langword="false"/>:セットアップ失敗</returns>
        public virtual bool InitialSetup()
        {
            if (_dockerController == null || _powerShellController == null)
            {
                // 依存するコントローラが設定されていない場合はセットアップ失敗
                return false;
            }
            // Dockerイメージがない場合にPullする
            if (!_dockerController.IsImageAvailable())
            {
                if (!_dockerController.IsDockerInstalled() || !_powerShellController.CanExecute()) return false; // 実行不可の場合はfalseを返します。
                bool execResult = _powerShellController.Execute(_dockerController.BuildPullCommand());
                if (!execResult) return false; // 初期セットアップの実行に失敗した場合はfalseを返します。
            }

            return true;  // 初期セットアップ成功
        }

        /// <summary>
        /// 指定フォルダをエディタで開きます。
        /// </summary>
        /// <param name="folderPath">開くフォルダのパス</param>
        /// <returns><see langword="true"/>:エディタが正常に開かれた場合; <see langword="false"/>:エラーが発生した場合。</returns>
        /// <remarks> このメソッドは、VSCodeコントローラを使用して、指定されたフォルダをVisual Studio Codeで開きます。フォルダ指定がnullまたは空文字の場合、VSCodeがコマンドラインから使用可能かどうかを確認します。</remarks>
        public virtual bool OpenEditor(string? folderPath)
        {

            if (_vsCodeController == null)
            {
                // 依存するコントローラが設定されていない場合は失敗
                return false;
            }
            try
            {
                // folderPathがnullまたは空文字の場合、CanExecuteを呼び出して結果を返す
                if (string.IsNullOrEmpty(folderPath))
                {
                    return _vsCodeController.CanExecute();
                }
                // 指定フォルダが存在するか確認
                if (!Directory.Exists(folderPath))
                {
                    // 指定フォルダが存在しない場合は失敗
                    return false;
                }
                // VSCodeが実行可能か確認
                if (!_vsCodeController.CanExecute())
                {
                    // VSCodeが実行不可の場合は失敗
                    return false;
                }
                // 指定フォルダをVSCodeで開く
                return _vsCodeController.Open(folderPath);
            }
            catch
            {
                return false; // エラーが発生した場合はfalseを返します。
            }
        }
    }
}