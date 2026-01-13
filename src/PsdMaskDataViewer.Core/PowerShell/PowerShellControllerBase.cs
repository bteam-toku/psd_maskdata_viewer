using System;
using System.Diagnostics;
using System.Text;

namespace PsdMaskDataViewer.Core.PowerShell
{
    public abstract class PowerShellControllerBase
    {
        /// <summary>
        /// 出力受信時のイベント
        /// </summary>
        public event Action<string>? OnOutputReceived;

        /// <summary>
        /// PowerShellコマンドを実行します。
        /// </summary>
        /// <param name="command">実行するPowerShellコマンド</param>
        /// <returns><see langword="true"/>:コマンドが正常に実行された場合; <see langword="false"/>:エラーが発生した場合。</returns>
        /// <remarks>
        /// このメソッドは、指定されたPowerShellコマンドを新しいPowerShellプロセスで実行します。
        /// 実行中に標準出力および標準エラー出力を非同期で受信し、OnOutputReceivedイベントを通じて通知します。
        /// </remarks>
        public virtual bool Execute(string command)
        {
            // PowerShellの起動設定
            var startInfo = new ProcessStartInfo
            {
                FileName = "powershell.exe",
				Arguments = $"-NoProfile -ExecutionPolicy Bypass -Command \"[Console]::OutputEncoding = [System.Text.Encoding]::UTF8; {command}\"",
				RedirectStandardInput = true,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true,
                StandardOutputEncoding = Encoding.UTF8,
                StandardErrorEncoding = Encoding.UTF8,
            };

            try
            {
                using (var process = new Process { StartInfo = startInfo })
                {
                    // 出力データ受信時のイベントハンドラを設定
                    process.OutputDataReceived += (sender, e) =>
                    {
                        if (e.Data != null)
                        {
                            OnOutputReceived?.Invoke(e.Data);
                        }
                    };
                    // エラーデータ受信時のイベントハンドラを設定
                    process.ErrorDataReceived += (sender, e) =>
                    {
                        if (e.Data != null)
                        {
                            OnOutputReceived?.Invoke($"ERROR: {e.Data}");
                        }
                    };
                    // プロセス開始
                    process.Start();
                    // 非同期で出力データの読み取りを開始
                    process.BeginOutputReadLine();
                    process.BeginErrorReadLine();
                    // プロセスの終了を待機
                    process.WaitForExit();
                    // 終了コードを確認
                    return process.ExitCode == 0;
                }
            }
            catch (Exception ex)
            {
                OnOutputReceived?.Invoke($"EXCEPTION: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// PowerShellが実行可能かどうかを判定します。
        /// </summary>
        /// <returns><see langword="true"/>:PowerShellが実行可能; <see langword="false"/>:実行不可。</returns>
        /// <remarks>
        /// このメソッドは、PowerShellがインストールされているかどうかを確認します。
        /// </remarks>
        public virtual bool CanExecute()
        {
            // PowerShellがインストールされているかどうかを確認します。
            try
            {
                var process = new Process();
                process.StartInfo.FileName = "powershell.exe";
                process.StartInfo.Arguments = "-NoProfile -Command \"$PSVersionTable.PSVersion\"";
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.CreateNoWindow = true;
                process.Start();
                string output = process.StandardOutput.ReadToEnd();
                process.WaitForExit();
                return !string.IsNullOrEmpty(output);
            }
            catch
            {
                return false;
            }
        }
    }
}