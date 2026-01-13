using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace PsdMaskDataViewer.App.Dialogs
{
    public partial class VersionDialog : Form
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public VersionDialog()
        {
            InitializeComponent();
        }

        /// <summary>
        /// ロードイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void VersionDialog_Load(object sender, EventArgs e)
        {
            // アセンブリ情報参照
            var path = Process.GetCurrentProcess().MainModule?.FileName;
            var versionInfo = FileVersionInfo.GetVersionInfo(path!);

            // Version
            if (versionInfo != null)
            {
                // Title
                this.Text = versionInfo.FileDescription + " - バージョン情報";
                // Product
                textProduct.Text = versionInfo.FileDescription;
                // Version
                textVersion.Text = "Version " + versionInfo.ProductVersion + "    (Build " + versionInfo.FileVersion + ")";
                // Copyright
                textCopyright.Text = versionInfo.LegalCopyright;
                // Description
                textDescription.Text = versionInfo.Comments;
            }
            else
            {
                textProduct.Text = "Product information not available.";
                textVersion.Text = "Version information not available.";
                textCopyright.Text = "Copyright information not available.";
                textDescription.Text = "Description information not available.";
            }
        }
    }
}
