using System;
using System.Windows.Forms;
using AudioTaggerCore;

namespace AudioTagger
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
            var test = new TaggerTest();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void openFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // open dialog to select folder
        }
    }
}