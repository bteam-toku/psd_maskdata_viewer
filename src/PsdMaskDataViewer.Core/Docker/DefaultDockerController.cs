using System;

namespace PsdMaskDataViewer.Core.Docker
{
    internal class DefaultDockerController : DockerControllerBase
    {
        // protectedメンバのオーバーライド
        protected override string RegistryUrl => "ghcr.io/bteam-toku";  // dockerレジストリのURL
        protected override string ImageName => "psd_maskdata";          // dockerイメージ名

        //
        // publicメンバのオーバーライド
        //
        /// <summary>
        /// docker runコマンドを構築します。
        /// </summary>
        /// <param name="inputPath">変換元データのフォルダパス</param>
        /// <param name="outputPath">変換先データのフォルダパス</param>
        /// <returns>docker runコマンドを含む文字列。この文字列をShellで実行してください。</returns>
        public override string BuildRunCommand(string inputPath, string outputPath)
        {
            // カレントパスを取得
            var currentPath = Environment.CurrentDirectory;
            // docker runコマンドを構築
            var sb = new System.Text.StringBuilder();
            sb.Append("docker run --rm ");
            sb.Append($"-v '{inputPath}:/app/input' ");
            sb.Append($"-v '{outputPath}:/app/output' ");
            sb.Append($"-v '{currentPath}:/data' ");
            sb.Append($"{ImageName}:latest --input /app/input --output /app/output");
            return sb.ToString();
        }
    }
}