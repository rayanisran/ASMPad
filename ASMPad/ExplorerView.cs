using System.Drawing;
using WeifenLuo.WinFormsUI.Docking;
using System.Windows.Forms;
using ASMPad.Properties;

namespace ASMPad
{
    public class ExplorerView : DockContent
    {
        private FileBrowser.Browser browser1;
        string currfold = string.Empty;
        #region Globals
        #endregion

        #region Constructor

        public ExplorerView()
        {
            InitializeComponent();
            this.TabText = "File Explorer";
            browser1.StartUpDirectoryOther = Settings.Default.fppath;
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ExplorerView));
            this.browser1 = new FileBrowser.Browser();
            this.SuspendLayout();
            // 
            // browser1
            // 
            this.browser1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.browser1.ListViewMode = System.Windows.Forms.View.List;
            this.browser1.Location = new System.Drawing.Point(0, 0);
            this.browser1.Name = "browser1";
            this.browser1.SelectedNode = null;
            this.browser1.Size = new System.Drawing.Size(893, 169);
            this.browser1.SplitterDistance = 158;
            this.browser1.StartUpDirectory = FileBrowser.SpecialFolders.Other;
            this.browser1.TabIndex = 0;
            this.browser1.SelectedFolderChanged += new FileBrowser.SelectedFolderChangedEventHandler(this.browser1_SelectedFolderChanged_1);
            // 
            // ExplorerView
            // 
            this.ClientSize = new System.Drawing.Size(893, 169);
            this.Controls.Add(this.browser1);
            this.DockAreas = WeifenLuo.WinFormsUI.Docking.DockAreas.DockBottom;
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ExplorerView";
            this.ShowIcon = false;
            this.TabText = "File Explorer";
            this.Text = "File Explorer";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ExplorerView_FormClosing);
            this.ResumeLayout(false);

        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets an Image from the InternalImages resource file...
        /// </summary>
        /// <param name="imageName">Name of Image</param>
        /// <returns>Image</returns>
        public Image GetInternalImage(string imageName)
        {
            System.Resources.ResourceManager mngr = new System.Resources.ResourceManager("CodingEditor.InternalImages", this.GetType().Assembly);
            return (Image)mngr.GetObject(imageName);
        }

        #endregion

        private void browser1_SelectedFolderChanged_1(object sender, FileBrowser.SelectedFolderChangedEventArgs e)
        {
            currfold = e.Path + "\\";
            Settings.Default.fppath = currfold;
            Settings.Default.Save();
        }

        private void ExplorerView_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                Hide();
            }
        }
    }
}
