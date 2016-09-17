using AudioTaggerCore;
using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.IO;
using System.Windows.Forms;
using TagLib;

namespace AudioTagger
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
            lsvCurrentFolder.View = View.Details;
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void openFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // open dialog to select folder
            var folderDialog = new CommonOpenFileDialog()
            {
                IsFolderPicker = true
            };

            var folderResult = folderDialog.ShowDialog();
            if (folderResult == CommonFileDialogResult.Ok)
            {
                var folder = folderDialog.FileName;
                string[] files = Directory.GetFiles(folder);
                if (files.IsValid())
                {
                    foreach (var file in files)
                    {
                        var tagFile = TagLib.File.Create(file);
                        if (tagFile.Properties.MediaTypes == MediaTypes.Audio)
                        {
                            var listViewItem = new ListViewItem(new string[]
                            {
                                tagFile.Tag.Track.ToString(),
                                tagFile.Tag.Title,
                                tagFile.Tag.FirstPerformer,
                                tagFile.Tag.Album,
                                tagFile.Tag.Year.ToString(),
                                tagFile.Tag.FirstGenre,
                                tagFile.Tag.Disc.ToString()
                            });

                            lsvCurrentFolder.Items.Add(listViewItem);
                        }   
                    }
                }
            }
        }
    }
}