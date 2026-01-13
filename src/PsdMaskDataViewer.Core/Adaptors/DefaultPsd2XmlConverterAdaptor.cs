using PsdMaskDataViewer.Core.Interfaces;
using PsdMaskDataViewer.Core.Docker;
using PsdMaskDataViewer.Core.PowerShell;
using PsdMaskDataViewer.Core.VSCode;
using System;
using System.Collections.Generic;
using System.Text;

namespace PsdMaskDataViewer.Core.Adaptors
{
    internal class DefaultPsd2XmlConverterAdaptor : Psd2XmlConverterAdaptorBase
    {
        public DefaultPsd2XmlConverterAdaptor(DockerControllerBase dockerController, PowerShellControllerBase powerShellController, VSCodeControllerBase vsCodeController)
            : base(dockerController, powerShellController, vsCodeController)
        {
        }
    }
}