#region Using Directives
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Reflection;
using System.IO;
using System.Diagnostics;
using Microsoft.VisualBasic.ApplicationServices;
#endregion Using Directives

namespace ASMPad
{
    public static class Program
    {
        #region Fields
        public static string _in = string.Empty;
        public static Main MainF = null;
        #endregion Fields

        #region Properties
        public static DocForm ActiveDocument
        {
            get
            {
                return MainF.ActiveDocument;
            }
        }

        public static string Title
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
                if (attributes.Length > 0)
                {
                    AssemblyTitleAttribute titleAttribute = (AssemblyTitleAttribute)attributes[0];
                    if (!String.IsNullOrEmpty(titleAttribute.Title))
                        return titleAttribute.Title;
                }
                //If there was no title attribute, or if the title attribute was the empty string, return the .exe name.
                return Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().CodeBase);
            }
        }
        #endregion Properties

        #region Methods
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            string[] args = Environment.GetCommandLineArgs();
            if (args.Length > 1)
                _in = args[1];
            SingleInstanceController controller = new SingleInstanceController();
            controller.Run(args);
        }
        #endregion Methods
    }

    public class SingleInstanceController : WindowsFormsApplicationBase
    {
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern void SwitchToThisWindow(IntPtr hWnd, bool fAltTab);
        public SingleInstanceController()
        {
            IsSingleInstance = true;
            StartupNextInstance += this_StartupNextInstance;
        }

        void this_StartupNextInstance(object sender, StartupNextInstanceEventArgs e)
        {
            Main form = MainForm as Main;
            form.CheckIfAlreadyOpen(e.CommandLine[1]);
            form.LG.cs.AppendText("\r\nLoaded file " + e.CommandLine[1] + " from the command line.");

            System.Diagnostics.Process me = System.Diagnostics.Process.GetCurrentProcess();
            System.Diagnostics.Process[] myProcesses = System.Diagnostics.Process.GetProcessesByName(me.ProcessName);
            foreach (System.Diagnostics.Process p in myProcesses)
            {
                if (p.Id != me.Id)
                {
                    SwitchToThisWindow(p.MainWindowHandle, true);
                    return;
                }
            }
        }

        protected override void OnCreateMainForm()
        {
            MainForm = new Main();
            Main form = MainForm as Main;
            string _l = Program._in;
            if (_l != string.Empty)
            {
                form.OpenFile(_l);
                form.LG.cs.AppendText("\r\nLoaded file " + _l + " from command line.");
            }
            else
                form.NewDocument();
        }
    }
}