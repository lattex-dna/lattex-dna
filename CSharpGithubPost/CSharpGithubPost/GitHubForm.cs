﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
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

        string destinationDirectory = string.Empty;

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
            okButton.Click += new EventHandler(OKButton_Click);

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

        private void OKButton_Click(object sender, EventArgs e)
        {
            messbox.Close();
            messbox.Dispose();
        }

        private void btnSaveToFolder_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog openFolderDialog = new FolderBrowserDialog())
            {
                lblShowInfo.Text = "Selecting...";
                if (openFolderDialog.ShowDialog() == DialogResult.OK && !string.IsNullOrEmpty(openFolderDialog.SelectedPath))
                {
                    destinationDirectory = openFolderDialog.SelectedPath;
                    lblDestinationDirectory.Text = "Github saved path: " + destinationDirectory;
                    lblShowInfo.Text = "Path is saved!";
                }
                else
                {
                    lblDestinationDirectory.Text = "Click Save To Button and Choose Destination Directory!";
                    lblShowInfo.Text = "Path is not saved!";
                }
            }
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                lblShowInfo.Text = "Selecting...";
                //openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
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
                                lblShowInfo.Text = "Selected!";

                                //Read the contents of the file into a stream
                                //var fileStream = openFileDialog.OpenFile();

                                //using (StreamReader reader = new StreamReader(fileStream))
                                //{
                                //    fileContent = reader.ReadToEnd();
                                //}
                            }
                            else
                            {
                                lblShowInfo.Text = "Selected!";
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

        public static void WriteLine(string s, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(s);
        }

        private string Copy(string note)
        {
            if (!string.IsNullOrEmpty(destinationDirectory))
            {
                using (System.Diagnostics.Process process = new System.Diagnostics.Process())
                {
                    lblShowInfo.Text = "Posting...";
                    //System.Diagnostics.Process process = new System.Diagnostics.Process();
                    System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
                    startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden; //Hidden
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
                        //copy with/without Overwrite
                        if (ckbOverwrite.Checked && !string.IsNullOrEmpty(destinationDirectory))
                        {
                            string renamedFile = pathFile.Replace(' ', '_').Split('\\').Last();
                            process.StandardInput.WriteLine("COPY " + "\"" + pathFile + "\"" + " \"" + destinationDirectory + "\\" + renamedFile + "\" /Y");
                        }
                        else
                        {
                            if (File.Exists(destinationDirectory + "\\" + pathFile.Replace(' ', '_').Split('\\').Last()))
                            {
                                string renamedFile = pathFile.Insert(pathFile.LastIndexOf('.'), DateTime.Now.ToString(" HH-mm-ss dd-MM-yyyy")).Replace(' ', '_').Split('\\').Last();
                                process.StandardInput.WriteLine("COPY " + "\"" + pathFile + "\"" + " \"" + destinationDirectory + "\\" + renamedFile + "\" /Y");
                            }
                            else
                            {
                                string renamedFile = pathFile.Replace(' ', '_').Split('\\').Last();
                                process.StandardInput.WriteLine("COPY " + "\"" + pathFile + "\"" + " \"" + destinationDirectory + "\\" + renamedFile + "\" /Y");
                            }
                        }
                    }

                    process.StandardInput.WriteLine("cd " + destinationDirectory);
                    process.StandardInput.WriteLine("git init");
                    process.StandardInput.WriteLine("git add *");
                    process.StandardInput.WriteLine("git commit -m \'" + note.Replace(' ', '_') + "\'");
                    process.StandardInput.WriteLine("git push");
                    process.StandardInput.WriteLine("exit");
                    process.WaitForExit();
                    lblShowInfo.Text = "Posted";
                }

                return "All done!";
            }
            else
                MessageBox.Show("Select save folder first please!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
            return "Can not be done!";
        }

        #region---Async Task---
        //private async Task<string> PostToGithub(string note)
        //{
        //    //Func<object, string> myfunc = (object thamso) =>
        //    //{
        //    //    // Đọc tham số (dùng kiểu động - xem kiểu động để biết chi tiết)
        //    //    dynamic ts = thamso;
        //    //    for (int i = 1; i <= 10; i++)
        //    //    {
        //    //        //  Thread.CurrentThread.ManagedThreadId  trả về ID của thread đạng chạy
        //    //        WriteLine($"{i,5} {Thread.CurrentThread.ManagedThreadId, 3} Tham số {ts.x} {ts.y}",
        //    //            ConsoleColor.Green);
        //    //        Thread.Sleep(500);
        //    //    }
        //    //    return $"Kết thúc Async1! {ts.x}";
        //    //};

        //    Func<object, string> postFunc = (object obj) =>
        //    {
        //        dynamic ob = obj;

        //        return "string";
        //    };

        //    Task<string> task = new Task<string>(postFunc, new { x = note });
        //    task.Start();
        //    await task;
        //    string result = task.Result;
        //    return result;

        //}

        //private async Task VoidReturn()
        //{
        //    Action myaction = () => {
        //        for (int i = 1; i <= 10; i++)
        //        {
        //            WriteLine($"{i, 5} {Thread.CurrentThread.ManagedThreadId, 3}", ConsoleColor.Yellow);
        //            Thread.Sleep(2000);
        //        }
        //    };
        //    Task task = new Task(myaction);
        //    task.Start();

        //    await task;
        //}

        //private async Task Process()
        //{
        //    try
        //    {
        //        Task<string> t1 = PostToGithub("initial commit");
        //        //Task t2 = VoidReturn();
        //        await t1;
        //        //await t2;
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message + "\r\n" + ex.StackTrace);
        //    }
        //}
        #endregion---End Async Task---

        private void btnPost_Click(object sender, EventArgs e)
        {
            string dateTimeNow = DateTime.Now.ToString("HH:mm:ss_yyyy-MM-dd-dddd");
            Copy(dateTimeNow.Insert(dateTimeNow.LastIndexOf(':'), "m").Insert(dateTimeNow.IndexOf(':'), "h").Replace(":", ""));
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            rtxbFilePath.Text = string.Empty;
            rtxbFileName.Text = string.Empty;
        }

        
    }
}
