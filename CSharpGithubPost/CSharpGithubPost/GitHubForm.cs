using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security;
using System.Text;
using System.Windows.Forms;

namespace CSharpGithubPost
{
    public partial class GithubPostForm : Form
    {
        public GithubPostForm()
        {
            InitializeComponent();
            lblShowInfo.Text = string.Empty;
        }

        Form messbox;
        public void MessageBoxShow(string content)
        {
            Label lblInfo = new Label();
            lblInfo.AutoSize = true;
            lblInfo.Size = new Size(50, 20);
            lblInfo.Location = new Point(90, 50);
            lblInfo.Font = new Font("Microsoft Sans Serif", 11.25F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
            lblInfo.Text = content;

            Button okButton = new Button();
            okButton.Size = new Size(80, 50);
            okButton.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
            okButton.Location = new Point(340, 150);
            okButton.Text = "OK";
            okButton.Click += new EventHandler(button1_Click);

            messbox = new Form();
            messbox.MaximizeBox = false;
            messbox.MinimizeBox = false;
            messbox.ClientSize = new Size(460, 260);
            messbox.MaximumSize = messbox.ClientSize;
            messbox.MinimumSize = messbox.ClientSize;
            messbox.AutoSize = true;
            messbox.Name = "InformationMessBox";
            messbox.Text = "Notification";
            messbox.Controls.Add(lblInfo);
            messbox.Controls.Add(okButton);
            messbox.TopMost = true;
            messbox.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            messbox.Close();
            messbox.Dispose();
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            //string fileContent = string.Empty;
            //string filePath = string.Empty;
            //string fileNameOnly = string.Empty;
            

            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
                openFileDialog.Filter = "All files (*.*)|*.*";
                //"txt files (*.txt)|*.txt|Images (*.BMP;*.JPG;*.GIF)|*.BMP;*.JPG;*.GIF|All files (*.*)|*.*";
                //openFileDialog.FilterIndex = 1;
                openFileDialog.RestoreDirectory = false;
                openFileDialog.Multiselect = true;
                openFileDialog.Title = "Select your file";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    foreach (string file in openFileDialog.FileNames)
                    {
                        try
                        {
                            if (!rtxbFilePath.Text.Contains(file))
                            {
                                //Get the path of specified file
                                rtxbFilePath.Text += file + "\r\n";
                                rtxbFileName.Text += file.Split('\\').Last().Split('.').First() + "\r\n";

                                //Read the contents of the file into a stream
                                //var fileStream = openFileDialog.OpenFile();

                                //using (StreamReader reader = new StreamReader(fileStream))
                                //{
                                //    fileContent = reader.ReadToEnd();
                                //}
                            }
                            else
                            {
                                MessageBoxShow(string.Format("File \"{0}\" is added!", file.Split('\\').Last().Split('.').First()));
                            }

                        }
                        catch (SecurityException ex)
                        {
                            // The user lacks appropriate permissions to read files, discover paths, etc.
                            MessageBox.Show("Security error. Please contact your administrator for details.\n\n" +
                                "Error message: " + ex.Message + "\n\n" +
                                "Details (send to Support):\n\n" + ex.StackTrace
                            );
                        }
                        catch (Exception ex)
                        {
                            // Could not load the image - probably related to Windows file system permissions.
                            MessageBox.Show("Cannot display the image: " + file.Substring(file.LastIndexOf('\\'))
                                + ". You may not have permission to read the file, or " +
                                "it may be corrupt.\n\nReported error: " + ex.Message);
                        }
                    }

                }
            }
        }

        private void btnPost_Click(object sender, EventArgs e)
        {
            using (System.Diagnostics.Process process = new System.Diagnostics.Process())
            {
                //System.Diagnostics.Process process = new System.Diagnostics.Process();
                System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
                startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                startInfo.FileName = "cmd.exe";
                startInfo.RedirectStandardInput = true;
                startInfo.RedirectStandardOutput = true;
                startInfo.UseShellExecute = false;
                startInfo.CreateNoWindow = true;
                process.StartInfo = startInfo;
                //startInfo.Arguments = "/C copy " + pathFile + " ";
                process.Start();

                foreach (string pathFile in rtxbFilePath.Lines)
                {
                    //spcace or not "\\?\"
                    process.StandardInput.WriteLine("copy \\\\?\\" + pathFile + " " + Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory).ToString() + "\\Test");
                }

                process.StandardInput.WriteLine("exit");
                process.WaitForExit();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            rtxbFilePath.Text = string.Empty;
            rtxbFileName.Text = string.Empty;
        }
    }
}
