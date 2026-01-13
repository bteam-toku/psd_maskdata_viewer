using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Text;

namespace PsdMaskDataViewer.Core.Docker
{
	public abstract class DockerControllerBase
	{
		// protectedメンバ
		protected abstract string RegistryUrl { get; }      // dockerレジストリのURL
		protected abstract string ImageName { get; }        // dockerイメージ名

		//
		// コンストラクタ
		//
		public DockerControllerBase()
		{
			// プロパティの設定確認
			if (string.IsNullOrEmpty(RegistryUrl) || string.IsNullOrEmpty(ImageName))
			{
				throw new InvalidOperationException("プロパティが設定されていません。");
			}
		}

		//
		// publicメンバ
		//
		/// <summary>
		/// docker pullコマンドを構築します。
		/// </summary>
		/// <remarks>
		/// このメソッドは、RegistryUrlとImageNameプロパティを使用してdocker pullコマンドを構築します。
		/// 実行前に、RegistryUrlとImageNameプロパティが正しく設定されていることを確認してください。
		/// このメソッドの呼び出し前に、CanExecuteメソッドを使用してdockerの実行可能性を確認することをお勧めします。
		/// </remarks>
		/// <returns>docker pullコマンドを含む文字列。この文字列をShellで実行してください。</returns>
		public virtual string BuildPullCommand()
		{
			var sb = new StringBuilder();
			// docker pullのコマンドを構築します。
			sb.Append($"docker pull {RegistryUrl}/{ImageName}:latest\n");
			// docker tagのコマンドを構築します。
			sb.Append($"docker tag {RegistryUrl}/{ImageName}:latest {ImageName}");
			return sb.ToString();
		}

		/// <summary>
		/// docker runコマンドを構築します。
		/// </summary>
		/// <param name="inputPath">変換元データのフォルダパス</param>
		/// <param name="outputPath">変換先データのフォルダパス</param>
		/// <returns>docker runコマンドを含む文字列。この文字列をShellで実行してください。</returns>
		public abstract string BuildRunCommand(string inputPath, string outputPath);

		/// <summary>
		/// dockerが実行可能かどうかを判定します。
		/// </summary>
		/// <remarks>
		/// このメソッドは、Dockerがインストールされているかどうかと、指定されたDockerイメージが利用可能かどうかを確認します。
		/// </remarks>
		/// <returns><see langword="true"/>:dockerが実行可能; <see langword="false"/>:実行不可。</returns>
		public virtual bool CanExecute()
		{
			// Dockerがインストールされているかどうかと、指定されたDockerイメージが利用可能かどうかを確認します。
			return IsDockerInstalled() && IsImageAvailable();
		}

		/// <summary>
		/// Dockerがインストールされているかどうかを判定します。
		/// </summary>
		/// <remarks>
		/// このメソッドは、docker --versionコマンドを実行して、Dockerの存在を確認します。
		/// </remarks>
		/// <returns><see langword="true"/>:Dockerがインストールされている; <see langword="false"/>:インストールされていない。</returns>
		public virtual bool IsDockerInstalled()
		{
			// docker --versionコマンドを実行して、Dockerの存在を確認します。 
			try
			{
				var process = new System.Diagnostics.Process();
				process.StartInfo.FileName = "docker";
				process.StartInfo.Arguments = "--version";
				process.StartInfo.RedirectStandardOutput = true;
				process.StartInfo.UseShellExecute = false;
				process.StartInfo.CreateNoWindow = true;
				process.Start();
				string output = process.StandardOutput.ReadToEnd();
				process.WaitForExit();
				return output.StartsWith("Docker version");
			}
			catch
			{
				return false;
			}
		}

		/// <summary>
		/// 指定されたDockerイメージが利用可能かどうかを判定します。
		/// </summary>
		/// <remarks>
		/// このメソッドは、docker images -q <ImageName>コマンドを実行して、イメージの存在を確認します。
		/// </remarks>
		/// <returns><see langword="true"/>:指定されたDockerイメージが利用可能; <see langword="false"/>:利用不可。</returns>
		public virtual bool IsImageAvailable()
		{
			// docker images -q <ImageName>コマンドを実行して、イメージの存在を確認します。
			try
			{
				var process = new System.Diagnostics.Process();
				process.StartInfo.FileName = "docker";
				process.StartInfo.Arguments = $"images -q {ImageName}";
				process.StartInfo.RedirectStandardOutput = true;
				process.StartInfo.UseShellExecute = false;
				process.StartInfo.CreateNoWindow = true;
				process.Start();
				string output = process.StandardOutput.ReadToEnd();
				process.WaitForExit();
				return !string.IsNullOrWhiteSpace(output);
			}
			catch
			{
				return false;
			}
		}
	}
}
