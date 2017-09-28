namespace WFTabExplorer
{
	partial class WFTabExplorerForm
	{
		/// <summary>
		/// 必需的设计器变量。
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// 清理所有正在使用的资源。
		/// </summary>
		/// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows 窗体设计器生成的代码

		/// <summary>
		/// 设计器支持所需的方法 - 不要修改
		/// 使用代码编辑器修改此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WFTabExplorerForm));
			this.tabExplorers = new System.Windows.Forms.TabControl();
			this.btnBack = new System.Windows.Forms.Button();
			this.btnForward = new System.Windows.Forms.Button();
			this.outputURL = new System.Windows.Forms.TextBox();
			this.btnGo = new System.Windows.Forms.Button();
			this.btnAddPage = new System.Windows.Forms.Button();
			this.btnUp = new System.Windows.Forms.Button();
			this.lDrivers = new System.Windows.Forms.ListBox();
			this.btnClose = new System.Windows.Forms.Button();
			this.splitContainer = new System.Windows.Forms.SplitContainer();
			this.btnNewDir = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
			this.splitContainer.Panel1.SuspendLayout();
			this.splitContainer.Panel2.SuspendLayout();
			this.splitContainer.SuspendLayout();
			this.SuspendLayout();
			// 
			// tabExplorers
			// 
			this.tabExplorers.AllowDrop = true;
			this.tabExplorers.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tabExplorers.Appearance = System.Windows.Forms.TabAppearance.FlatButtons;
			this.tabExplorers.Location = new System.Drawing.Point(3, 4);
			this.tabExplorers.Name = "tabExplorers";
			this.tabExplorers.SelectedIndex = 0;
			this.tabExplorers.Size = new System.Drawing.Size(375, 390);
			this.tabExplorers.TabIndex = 0;
			this.tabExplorers.SelectedIndexChanged += new System.EventHandler(this.tabExplorers_SelectedIndexChanged);
			// 
			// btnBack
			// 
			this.btnBack.Location = new System.Drawing.Point(11, 1);
			this.btnBack.Name = "btnBack";
			this.btnBack.Size = new System.Drawing.Size(21, 23);
			this.btnBack.TabIndex = 1;
			this.btnBack.Text = "←";
			this.btnBack.UseVisualStyleBackColor = true;
			this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
			// 
			// btnForward
			// 
			this.btnForward.Location = new System.Drawing.Point(55, 1);
			this.btnForward.Name = "btnForward";
			this.btnForward.Size = new System.Drawing.Size(21, 23);
			this.btnForward.TabIndex = 2;
			this.btnForward.Text = "→";
			this.btnForward.UseVisualStyleBackColor = true;
			this.btnForward.Click += new System.EventHandler(this.btnForward_Click);
			// 
			// outputURL
			// 
			this.outputURL.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.outputURL.ForeColor = System.Drawing.SystemColors.InfoText;
			this.outputURL.Location = new System.Drawing.Point(82, 1);
			this.outputURL.Name = "outputURL";
			this.outputURL.Size = new System.Drawing.Size(376, 21);
			this.outputURL.TabIndex = 3;
			this.outputURL.KeyDown += new System.Windows.Forms.KeyEventHandler(this.outputURL_KeyDown);
			// 
			// btnGo
			// 
			this.btnGo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnGo.Location = new System.Drawing.Point(464, 1);
			this.btnGo.Name = "btnGo";
			this.btnGo.Size = new System.Drawing.Size(29, 23);
			this.btnGo.TabIndex = 4;
			this.btnGo.Text = "Go";
			this.btnGo.UseVisualStyleBackColor = true;
			this.btnGo.Click += new System.EventHandler(this.btnGo_Click);
			// 
			// btnAddPage
			// 
			this.btnAddPage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnAddPage.Location = new System.Drawing.Point(494, 1);
			this.btnAddPage.Name = "btnAddPage";
			this.btnAddPage.Size = new System.Drawing.Size(29, 23);
			this.btnAddPage.TabIndex = 5;
			this.btnAddPage.Text = "+";
			this.btnAddPage.UseVisualStyleBackColor = true;
			this.btnAddPage.Click += new System.EventHandler(this.btnAddPage_Click);
			// 
			// btnUp
			// 
			this.btnUp.Location = new System.Drawing.Point(33, 1);
			this.btnUp.Name = "btnUp";
			this.btnUp.Size = new System.Drawing.Size(21, 23);
			this.btnUp.TabIndex = 6;
			this.btnUp.Text = "↑";
			this.btnUp.UseVisualStyleBackColor = true;
			this.btnUp.Click += new System.EventHandler(this.btnUp_Click);
			// 
			// lDrivers
			// 
			this.lDrivers.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.lDrivers.FormattingEnabled = true;
			this.lDrivers.ItemHeight = 12;
			this.lDrivers.Location = new System.Drawing.Point(3, 3);
			this.lDrivers.Name = "lDrivers";
			this.lDrivers.Size = new System.Drawing.Size(150, 388);
			this.lDrivers.TabIndex = 7;
			this.lDrivers.SelectedIndexChanged += new System.EventHandler(this.lDrivers_SelectedIndexChanged);
			// 
			// btnClose
			// 
			this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnClose.Location = new System.Drawing.Point(524, 1);
			this.btnClose.Name = "btnClose";
			this.btnClose.Size = new System.Drawing.Size(29, 23);
			this.btnClose.TabIndex = 8;
			this.btnClose.Text = "×";
			this.btnClose.UseVisualStyleBackColor = true;
			this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
			// 
			// splitContainer
			// 
			this.splitContainer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.splitContainer.Location = new System.Drawing.Point(11, 51);
			this.splitContainer.Name = "splitContainer";
			// 
			// splitContainer.Panel1
			// 
			this.splitContainer.Panel1.Controls.Add(this.lDrivers);
			// 
			// splitContainer.Panel2
			// 
			this.splitContainer.Panel2.Controls.Add(this.tabExplorers);
			this.splitContainer.Size = new System.Drawing.Size(542, 402);
			this.splitContainer.SplitterDistance = 156;
			this.splitContainer.TabIndex = 9;
			// 
			// btnNewDir
			// 
			this.btnNewDir.Location = new System.Drawing.Point(11, 25);
			this.btnNewDir.Name = "btnNewDir";
			this.btnNewDir.Size = new System.Drawing.Size(101, 23);
			this.btnNewDir.TabIndex = 10;
			this.btnNewDir.Text = "新建文件夹(&N)";
			this.btnNewDir.UseVisualStyleBackColor = true;
			this.btnNewDir.Click += new System.EventHandler(this.btnNewDir_Click);
			// 
			// WFTabExplorerForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(561, 465);
			this.Controls.Add(this.btnNewDir);
			this.Controls.Add(this.splitContainer);
			this.Controls.Add(this.btnClose);
			this.Controls.Add(this.btnUp);
			this.Controls.Add(this.btnAddPage);
			this.Controls.Add(this.btnGo);
			this.Controls.Add(this.outputURL);
			this.Controls.Add(this.btnForward);
			this.Controls.Add(this.btnBack);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "WFTabExplorerForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "TabExplorer";
			this.Load += new System.EventHandler(this.WFTabExplorerForm_Load);
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.WFTabExplorerForm_KeyDown);
			this.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.WFTabExplorerForm_MouseDoubleClick);
			this.splitContainer.Panel1.ResumeLayout(false);
			this.splitContainer.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
			this.splitContainer.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		private void LDrivers_MouseDoubleClick(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			throw new System.NotImplementedException();
		}

		#endregion

		private System.Windows.Forms.TabControl tabExplorers;
		private System.Windows.Forms.Button btnBack;
		private System.Windows.Forms.Button btnForward;
		private System.Windows.Forms.TextBox outputURL;
		private System.Windows.Forms.Button btnGo;
		private System.Windows.Forms.Button btnAddPage;
		private System.Windows.Forms.Button btnUp;
		private System.Windows.Forms.ListBox lDrivers;
		private System.Windows.Forms.Button btnClose;
		private System.Windows.Forms.SplitContainer splitContainer;
		private System.Windows.Forms.Button btnNewDir;
	}
}

