using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using CefSharp;
using CefSharp.WinForms;

using HC.SelfServiceTerminals.Win.ExternalUnit;

namespace HC.SelfServiceTerminals.Win
{
    public partial class Form1 : Form
    {
        ChromiumWebBrowser browser;
        public Form1()
        {
            InitializeComponent();
            

            //var settings = new CefSettings();
            //settings.Locale = "zh-CN";
            //settings.CefCommandLineArgs.Add("disable-gpu", "1");//去掉gpu，否则chrome显示有问题
            //Cef.Initialize(settings);


            //Monitor parent process exit and close subprocesses if parent process exits first
            //This will at some point in the future becomes the default
            CefSharpSettings.SubprocessExitIfParentProcessClosed = true;
            CefSharpSettings.LegacyJavascriptBindingEnabled = true;
            //For Windows 7 and above, best to include relevant app.manifest entries as well
            Cef.EnableHighDPISupport();

            var settings = new CefSettings()
            {
                //By default CefSharp will use an in-memory cache, you need to specify a Cache Folder to persist data
                CachePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "CefSharp\\Cache")
            };

            //Example of setting a command line argument
            //Enables WebRTC
            settings.CefCommandLineArgs.Add("enable-media-stream", "1");

            //Perform dependency check to make sure all relevant resources are in our output directory.
            Cef.Initialize(settings, performDependencyCheck: true, browserProcessHandler: null);


            //WindowState = FormWindowState.Maximized;

             browser = new ChromiumWebBrowser("http://localhost:88")
            {
                Dock = DockStyle.Fill,
            };
            contentPanel.Controls.Add(browser);
           
            browser.ContextMenuStrip = this.contextMenuStrip1;

            this.contentPanel.Size = new System.Drawing.Size(1280, 1024);
            this.Size = new System.Drawing.Size(1305, 1080);

            //新注册方式，可支持多页面，需要在前端调用前注册
            browser.JavascriptObjectRepository.ResolveObject += (s, eve) =>
            {
                var repo = eve.ObjectRepository;
                if (eve.ObjectName == "sdk_demo") //这个名字对应页面上 CefSharp.BindObjectAsync 部分
                {
                    repo.Register("sdk_demo", new SDK_Demo(), isAsync: true, options: BindingOptions.DefaultBinder);
                }
            };
        }

        private void toolStripMenuItem_devtool_Click(object sender, EventArgs e)
        {
            browser.ShowDevTools();
        }
    }
}
