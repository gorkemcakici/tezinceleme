
namespace TezInceleme
{
	partial class frmMain
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.txt_thesispath = new System.Windows.Forms.TextBox();
			this.bn_openThesis = new System.Windows.Forms.Button();
			this.listResults = new System.Windows.Forms.ListBox();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.bn_start = new System.Windows.Forms.Button();
			this.thesisProcess = new System.ComponentModel.BackgroundWorker();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.label1 = new System.Windows.Forms.Label();
			this.lblErrorMessage = new System.Windows.Forms.Label();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			this.SuspendLayout();
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.lblErrorMessage);
			this.groupBox1.Controls.Add(this.txt_thesispath);
			this.groupBox1.Controls.Add(this.bn_start);
			this.groupBox1.Controls.Add(this.bn_openThesis);
			this.groupBox1.ForeColor = System.Drawing.Color.White;
			this.groupBox1.Location = new System.Drawing.Point(12, 91);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(774, 63);
			this.groupBox1.TabIndex = 0;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Tez Dosyası";
			// 
			// txt_thesispath
			// 
			this.txt_thesispath.Location = new System.Drawing.Point(6, 28);
			this.txt_thesispath.Name = "txt_thesispath";
			this.txt_thesispath.Size = new System.Drawing.Size(583, 20);
			this.txt_thesispath.TabIndex = 1;
			// 
			// bn_openThesis
			// 
			this.bn_openThesis.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
			this.bn_openThesis.ForeColor = System.Drawing.Color.White;
			this.bn_openThesis.Location = new System.Drawing.Point(595, 18);
			this.bn_openThesis.Name = "bn_openThesis";
			this.bn_openThesis.Size = new System.Drawing.Size(75, 40);
			this.bn_openThesis.TabIndex = 0;
			this.bn_openThesis.Text = "Dosya yükle";
			this.bn_openThesis.UseVisualStyleBackColor = false;
			this.bn_openThesis.Click += new System.EventHandler(this.bn_openThesis_Click);
			// 
			// listResults
			// 
			this.listResults.FormattingEnabled = true;
			this.listResults.Location = new System.Drawing.Point(6, 19);
			this.listResults.Name = "listResults";
			this.listResults.Size = new System.Drawing.Size(762, 238);
			this.listResults.TabIndex = 1;
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.listResults);
			this.groupBox2.ForeColor = System.Drawing.Color.White;
			this.groupBox2.Location = new System.Drawing.Point(12, 160);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(774, 263);
			this.groupBox2.TabIndex = 2;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Hatalar";
			// 
			// bn_start
			// 
			this.bn_start.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
			this.bn_start.Location = new System.Drawing.Point(676, 18);
			this.bn_start.Name = "bn_start";
			this.bn_start.Size = new System.Drawing.Size(75, 40);
			this.bn_start.TabIndex = 3;
			this.bn_start.Text = "Başla";
			this.bn_start.UseVisualStyleBackColor = false;
			this.bn_start.Click += new System.EventHandler(this.bn_start_Click);
			// 
			// thesisProcess
			// 
			this.thesisProcess.WorkerReportsProgress = true;
			this.thesisProcess.DoWork += new System.ComponentModel.DoWorkEventHandler(this.thesisProcess_DoWork);
			this.thesisProcess.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.thesisProcess_ProgressChanged);
			this.thesisProcess.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.thesisProcess_RunWorkerCompleted);
			// 
			// pictureBox1
			// 
			this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
			this.pictureBox1.Location = new System.Drawing.Point(12, 1);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(100, 85);
			this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.pictureBox1.TabIndex = 4;
			this.pictureBox1.TabStop = false;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label1.ForeColor = System.Drawing.Color.White;
			this.label1.Location = new System.Drawing.Point(149, 29);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(546, 37);
			this.label1.TabIndex = 5;
			this.label1.Text = "Fırat Üniversitesi Tez Kontrol Aracı";
			// 
			// lblErrorMessage
			// 
			this.lblErrorMessage.AutoSize = true;
			this.lblErrorMessage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
			this.lblErrorMessage.ForeColor = System.Drawing.Color.Red;
			this.lblErrorMessage.Location = new System.Drawing.Point(235, 12);
			this.lblErrorMessage.Name = "lblErrorMessage";
			this.lblErrorMessage.Size = new System.Drawing.Size(155, 13);
			this.lblErrorMessage.TabIndex = 4;
			this.lblErrorMessage.Text = "*Lütfen tez dosyanızı yükleyiniz.";
			// 
			// frmMain
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(146)))), ((int)(((byte)(33)))), ((int)(((byte)(71)))));
			this.ClientSize = new System.Drawing.Size(798, 434);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.pictureBox1);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.groupBox1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximumSize = new System.Drawing.Size(814, 473);
			this.MinimumSize = new System.Drawing.Size(814, 473);
			this.Name = "frmMain";
			this.Text = "Fırat Üniversitesi Tez Kontrol Aracı";
			this.Load += new System.EventHandler(this.frmMain_Load);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.groupBox2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.TextBox txt_thesispath;
		private System.Windows.Forms.Button bn_openThesis;
		private System.Windows.Forms.ListBox listResults;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.Button bn_start;
		private System.ComponentModel.BackgroundWorker thesisProcess;
		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label lblErrorMessage;
	}
}

