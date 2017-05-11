using System;
using System.Windows.Forms;
using CefSharp;
using CefSharp.WinForms;
using System.Reflection;
using System.IO;

namespace SampleCsChromium
{
  [System.Runtime.InteropServices.Guid("A687BDD9-F74C-4BB2-88E0-E2AEC95A9FCE")]
  public partial class SampleCsChromiumPanelControl : UserControl
  {
    private ChromiumWebBrowser m_browser;

    /// <summary>
    /// Returns the ID of this panel.
    /// </summary>
    public static Guid PanelId
    {
      get
      {
        return typeof(SampleCsChromiumPanelControl).GUID;
      }
    }

    public SampleCsChromiumPanelControl()
    {
      InitializeComponent();
      InitializeBrowser();
      SampleCsChromiumPlugIn.Instance.UserControl = this;
      this.Disposed += new EventHandler(OnDisposed);
    }

    private void InitializeBrowser()
    {
      Cef.EnableHighDPISupport();

      string assemblyLocation = Assembly.GetExecutingAssembly().Location;
      string assemblyPath = Path.GetDirectoryName(assemblyLocation);
      string pathSubprocess = Path.Combine(assemblyPath, "CefSharp.BrowserSubprocess.exe");

      var settings = new CefSettings();
      settings.BrowserSubprocessPath = pathSubprocess;
      Cef.Initialize(settings);

      m_browser = new ChromiumWebBrowser("http://www.rhino3d.com/");
      Controls.Add(m_browser);
      m_browser.Dock = DockStyle.Fill;
      m_browser.Enabled = true;
      m_browser.Show();
    }

    /// <summary>
    /// Occurs when the component is disposed by a call to the
    /// System.ComponentModel.Component.Dispose() method.
    /// </summary>
    private void OnDisposed(object sender, EventArgs e)
    {
      m_browser.Dispose();
      Cef.Shutdown();
      SampleCsChromiumPlugIn.Instance.UserControl = null;
    }
  }
}
