using PsdMaskDataViewer.Core.Interfaces;
using PsdMaskDataViewer.Core.Adaptors;
using PsdMaskDataViewer.Core.Docker;
using PsdMaskDataViewer.Core.PowerShell;
using PsdMaskDataViewer.Core.VSCode;
using System;
using System.Collections.Generic;
using System.Text;

namespace PsdMaskDataViewer.Core.Factories
{
    public static class Psd2XmlConverterFactory
    {
        /// <summary>
        /// IPsd2XmlConverterのインスタンスを生成します。
        /// </summary>
        /// <returns>初期化済みのIPsd2XmlConverterインスタンス</returns>
        public static IPsd2XmlConverter Create(string? adaptorTypeName = null)
        {
            if (!string.IsNullOrEmpty(adaptorTypeName))
            {
                // 指定されたアダプタの型名からインスタンスを生成します。
                var adaptorType = Type.GetType(adaptorTypeName);
                if (adaptorType != null && typeof(IPsd2XmlConverter).IsAssignableFrom(adaptorType))
                {
                    try
                    {
                        return (IPsd2XmlConverter)Activator.CreateInstance(adaptorType)!;
                    }
                    catch (Exception)
                    {
                        // インスタンスの生成に失敗した場合はデフォルトのアダプタを返します。
                    }
                }
            }

            // デフォルトのアダプタを生成します。
            // 依存するコントローラのインスタンスを生成します。
            var dockerController = new DefaultDockerController();
            var powerShellController = new DefaultPowerShellController();
            var vsCodeController = new DefaultVSCodeController();
            // アダプタのインスタンスを生成して返します。
            return new DefaultPsd2XmlConverterAdaptor(dockerController, powerShellController, vsCodeController);
        }
    }
}
