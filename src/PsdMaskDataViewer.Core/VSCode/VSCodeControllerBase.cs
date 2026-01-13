using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace PsdMaskDataViewer.Core.VSCode
{
    public abstract class VSCodeControllerBase
    {
        /// <summary>
        /// 指定フォルダをVSCodeで開きます。
        /// </summary>
        /// <param name="folderPath">開くフォルダのパス</param>
        /// <returns><see langword="true"/>: 正常に開かれた場合; <see langword="false"/>: エラーが発生した場合。</returns>
        /// <remarks> このメソッドは、"code"コマンドを使用して、指定されたフォルダをVisual Studio Codeで開きます。</remarks>
        public virtual bool Open(string folderPath)
        {
			// VSCodeを起動して指定フォルダを開く
			var startInfo = new ProcessStartInfo
            {
                FileName = "code",
				Arguments = $"-n \"{folderPath}\"",
				UseShellExecute = false,
                CreateNoWindow = true,
				//WindowStyle = ProcessWindowStyle.Hidden
            };


			try
            {
                using (var process = new Process { StartInfo = startInfo })
                {
                    process.Start();
                }
            }
            catch
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// VSCodeがコマンドラインから使用可能か確認します。
        /// </summary>
        /// <returns><see langword="true"/>: 使用可能; <see langword="false"/>: 使用不可。</returns>
        /// <remarks> このメソッドは、"code"コマンドがシステムで使用可能かどうかを確認します。</remarks>
        public virtual bool CanExecute()
        {
            // "code"コマンドが使用可能か確認
            try
            {
				var pathEnv = Environment.GetEnvironmentVariable("PATH");
				if (string.IsNullOrEmpty(pathEnv)) return false;

				var paths = pathEnv.Split(Path.PathSeparator);
				foreach (var path in paths)
				{
					if (path.Contains("Microsoft VS Code") ||
						File.Exists(Path.Combine(path, "code")) ||
						File.Exists(Path.Combine(path, "code.cmd")))
					{
						return true;
					}
				}
                return true;
			}
            catch
            {
                return false;
            }
        }
    }
}
