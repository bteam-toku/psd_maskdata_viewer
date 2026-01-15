using PsdMaskDataViewer.Core.Interfaces;
using PsdMaskDataViewer.Core.Docker;
using PsdMaskDataViewer.Core.PowerShell;
using PsdMaskDataViewer.Core.VSCode;
using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography.X509Certificates;

namespace PsdMaskDataViewer.Core.Adaptors
{
    internal class DefaultPsd2XmlConverterAdaptor : Psd2XmlConverterAdaptorBase
    {
        public DefaultPsd2XmlConverterAdaptor(DefaultDockerController dockerController, DefaultPowerShellController powerShellController, DefaultVSCodeController vsCodeController)
            : base(dockerController, powerShellController, vsCodeController)
        {
        }

        public override bool Convert(string inputPsdPath, string outputXmlPath)
        {
            // 依存するコントローラが設定されていない場合は実行不可
            if (_powerShellController == null)
            {
                return false;
            }

            // outputXmlPathがネットワークドライブの場合、変換を実行しない
            if (outputXmlPath.StartsWith(@"\\") || outputXmlPath.StartsWith("//"))
            {
                return false;
            }

            // inputPsdPathがネットワークドライブの場合、input/tempに全てのPSDファイルをコピーしてから変換を実行する
            if (inputPsdPath.StartsWith(@"\\") || inputPsdPath.StartsWith("//"))
            {
                // コピー元とコピー先のパスを設定
                string copySrcPath = inputPsdPath.TrimEnd(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);
                // アプリケーションの実行ディレクトリ内にinput/tempフォルダを作成
                string copyDstPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "input", "temp");
                // コピー先のフォルダを作成
                if (Directory.Exists(copyDstPath))
                {
                    try
                    {
                        Directory.Delete(copyDstPath, true);
                    }
                    catch (Exception)
                    {
                        // 削除に失敗した場合、変換を実行しない
                        return false;
                    }
                }
                // フォルダが存在しない場合は作成
                Directory.CreateDirectory(copyDstPath);

                // PSDファイルをコピー
                string Command = $@"robocopy --% ""{copySrcPath}"" ""{copyDstPath}"" *.psd /E /R:3 /W:5 /NDL /NJH /NJS /NC /NS /NP";
                _powerShellController.Execute(Command);

                // コピー先のパスを変換元パスとして設定
                inputPsdPath = copyDstPath;
            }

            // 基底クラスの変換処理を呼び出す
            return base.Convert(inputPsdPath, outputXmlPath);
        }
    }
}