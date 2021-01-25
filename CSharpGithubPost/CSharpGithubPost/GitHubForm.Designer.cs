
namespace CSharpGithubPost
{
    partial class GithubPostForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.grpBoxFilePath = new System.Windows.Forms.GroupBox();
            this.rtxbFilePath = new System.Windows.Forms.RichTextBox();
            this.rtxbFileName = new System.Windows.Forms.RichTextBox();
            this.grpBoxClickButton = new System.Windows.Forms.GroupBox();
            this.btnSaveToFolder = new System.Windows.Forms.Button();
            this.ckbOverwrite = new System.Windows.Forms.CheckBox();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnSelect = new System.Windows.Forms.Button();
            this.btnPost = new System.Windows.Forms.Button();
            this.lblShowInfo = new System.Windows.Forms.Label();
            this.lblDestinationDirectory = new System.Windows.Forms.Label();
            this.grpBoxFilePath.SuspendLayout();
            this.grpBoxClickButton.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpBoxFilePath
            // 
            this.grpBoxFilePath.Controls.Add(this.rtxbFilePath);
            this.grpBoxFilePath.Controls.Add(this.rtxbFileName);
            this.grpBoxFilePath.Location = new System.Drawing.Point(18, 18);
            this.grpBoxFilePath.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.grpBoxFilePath.Name = "grpBoxFilePath";
            this.grpBoxFilePath.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.grpBoxFilePath.Size = new System.Drawing.Size(1012, 451);
            this.grpBoxFilePath.TabIndex = 0;
            this.grpBoxFilePath.TabStop = false;
            this.grpBoxFilePath.Text = "File";
            // 
            // rtxbFilePath
            // 
            this.rtxbFilePath.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtxbFilePath.Location = new System.Drawing.Point(294, 29);
            this.rtxbFilePath.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.rtxbFilePath.Name = "rtxbFilePath";
            this.rtxbFilePath.ReadOnly = true;
            this.rtxbFilePath.Size = new System.Drawing.Size(708, 410);
            this.rtxbFilePath.TabIndex = 0;
            this.rtxbFilePath.Text = "";
            // 
            // rtxbFileName
            // 
            this.rtxbFileName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtxbFileName.Location = new System.Drawing.Point(9, 29);
            this.rtxbFileName.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.rtxbFileName.Name = "rtxbFileName";
            this.rtxbFileName.ReadOnly = true;
            this.rtxbFileName.Size = new System.Drawing.Size(274, 410);
            this.rtxbFileName.TabIndex = 0;
            this.rtxbFileName.Text = "";
            // 
            // grpBoxClickButton
            // 
            this.grpBoxClickButton.Controls.Add(this.btnSaveToFolder);
            this.grpBoxClickButton.Controls.Add(this.ckbOverwrite);
            this.grpBoxClickButton.Controls.Add(this.btnDelete);
            this.grpBoxClickButton.Controls.Add(this.btnSelect);
            this.grpBoxClickButton.Controls.Add(this.btnPost);
            this.grpBoxClickButton.Location = new System.Drawing.Point(1040, 18);
            this.grpBoxClickButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.grpBoxClickButton.Name = "grpBoxClickButton";
            this.grpBoxClickButton.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.grpBoxClickButton.Size = new System.Drawing.Size(142, 239);
            this.grpBoxClickButton.TabIndex = 0;
            this.grpBoxClickButton.TabStop = false;
            this.grpBoxClickButton.Text = "Click Button";
            // 
            // btnSaveToFolder
            // 
            this.btnSaveToFolder.Location = new System.Drawing.Point(9, 29);
            this.btnSaveToFolder.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnSaveToFolder.Name = "btnSaveToFolder";
            this.btnSaveToFolder.Size = new System.Drawing.Size(124, 35);
            this.btnSaveToFolder.TabIndex = 2;
            this.btnSaveToFolder.Text = "Save To";
            this.btnSaveToFolder.UseVisualStyleBackColor = true;
            this.btnSaveToFolder.Click += new System.EventHandler(this.btnSaveToFolder_Click);
            // 
            // ckbOverwrite
            // 
            this.ckbOverwrite.AutoSize = true;
            this.ckbOverwrite.Location = new System.Drawing.Point(21, 117);
            this.ckbOverwrite.Name = "ckbOverwrite";
            this.ckbOverwrite.Size = new System.Drawing.Size(94, 24);
            this.ckbOverwrite.TabIndex = 1;
            this.ckbOverwrite.Text = "Overwrite";
            this.ckbOverwrite.UseVisualStyleBackColor = true;
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(9, 194);
            this.btnDelete.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(124, 35);
            this.btnDelete.TabIndex = 0;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnSelect
            // 
            this.btnSelect.Location = new System.Drawing.Point(9, 74);
            this.btnSelect.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new System.Drawing.Size(124, 35);
            this.btnSelect.TabIndex = 0;
            this.btnSelect.Text = "Select";
            this.btnSelect.UseVisualStyleBackColor = true;
            this.btnSelect.Click += new System.EventHandler(this.btnSelect_Click);
            // 
            // btnPost
            // 
            this.btnPost.Location = new System.Drawing.Point(10, 149);
            this.btnPost.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnPost.Name = "btnPost";
            this.btnPost.Size = new System.Drawing.Size(124, 35);
            this.btnPost.TabIndex = 0;
            this.btnPost.Text = "Post";
            this.btnPost.UseVisualStyleBackColor = true;
            this.btnPost.Click += new System.EventHandler(this.btnPost_Click);
            // 
            // lblShowInfo
            // 
            this.lblShowInfo.AutoSize = true;
            this.lblShowInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblShowInfo.Location = new System.Drawing.Point(1045, 439);
            this.lblShowInfo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblShowInfo.Name = "lblShowInfo";
            this.lblShowInfo.Size = new System.Drawing.Size(32, 18);
            this.lblShowInfo.TabIndex = 1;
            this.lblShowInfo.Text = "Info";
            // 
            // lblDestinationDirectory
            // 
            this.lblDestinationDirectory.AutoSize = true;
            this.lblDestinationDirectory.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDestinationDirectory.Location = new System.Drawing.Point(24, 475);
            this.lblDestinationDirectory.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDestinationDirectory.Name = "lblDestinationDirectory";
            this.lblDestinationDirectory.Size = new System.Drawing.Size(142, 18);
            this.lblDestinationDirectory.TabIndex = 2;
            this.lblDestinationDirectory.Text = "DestinationDirectory";
            // 
            // GithubPostForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1200, 502);
            this.Controls.Add(this.lblDestinationDirectory);
            this.Controls.Add(this.lblShowInfo);
            this.Controls.Add(this.grpBoxClickButton);
            this.Controls.Add(this.grpBoxFilePath);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "GithubPostForm";
            this.Text = "Github Post";
            this.grpBoxFilePath.ResumeLayout(false);
            this.grpBoxClickButton.ResumeLayout(false);
            this.grpBoxClickButton.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox grpBoxFilePath;
        private System.Windows.Forms.RichTextBox rtxbFilePath;
        private System.Windows.Forms.RichTextBox rtxbFileName;
        private System.Windows.Forms.GroupBox grpBoxClickButton;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnSelect;
        private System.Windows.Forms.Button btnPost;
        private System.Windows.Forms.Label lblShowInfo;
        private System.Windows.Forms.CheckBox ckbOverwrite;
        private System.Windows.Forms.Button btnSaveToFolder;
        private System.Windows.Forms.Label lblDestinationDirectory;
    }
}

