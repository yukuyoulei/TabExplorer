using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Web;
using System.IO;
using System.Management;
using System.Diagnostics;

namespace WFTabExplorer
{
	public partial class WFTabExplorerForm : Form
	{
		AIniLoader ConfigurationLoader;
		public WFTabExplorerForm()
		{
			InitializeComponent();

			if (!Directory.Exists("Config"))
			{
				Directory.CreateDirectory("Config");

				if (File.Exists("Config.ini"))
				{
					File.Move("Config.ini", "Config/Config.ini");
				}
				if (File.Exists("Save.ini"))
				{
					File.Move("Save.ini", "Config/Save.ini");
				}
				if (File.Exists("Favorite.ini"))
				{
					File.Move("Favorite.ini", "Config/Favorite.ini");
				}
			}
			ConfigurationLoader = new AIniLoader();
			ConfigurationLoader.LoadIniFile("Config/Config.ini");
			ConfigurationLoader.OnSetDefaultValue("w", 1024);
			ConfigurationLoader.OnSetDefaultValue("h", 768);

			if (ConfigurationLoader.OnGetIntValue("w") <= 0)
			{
				LoadDir(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory));
			}
			else
			{
				int count = pathLoader.OnGetIntValue("Count");
				if (count > 0)
				{
					for (int i = 0; i < count; i++)
					{
						string surl = pathLoader.OnGetValue("U" + i);
						LoadDir(surl, i);
						if (!dCurPath.ContainsKey(i))
						{
							dCurPath.Add(i, surl);
						}
					}
					int cur = pathLoader.OnGetIntValue("Cur");
					if (cur >= 0 && cur < tabExplorers.TabCount)
					{
						tabExplorers.SelectedIndex = cur;
					}
				}
				else
				{
					LoadDir(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory));
				}
			}

			LoadDevices();

			bInited = true;
		}
		private bool bInited;

		protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
		{
			const int WM_KEYDOWN = 0x100;
			const int WM_SYSKEYDOWN = 0x104;
			if ((msg.Msg == WM_KEYDOWN) || (msg.Msg == WM_SYSKEYDOWN))
			{
				KeysConverter kc = new KeysConverter();
				string sKC = kc.ConvertToString(keyData);
				if (kc.ConvertToString(keyData).IndexOf("Alt", 0) != -1 && kc.ConvertToString(keyData).IndexOf("D", 0) != -1)
				{
					outputURL.Focus();
				}
				else if (sKC == "BackSpace")
				{
					if (!outputURL.Focused)
					{
						btnBack_Click(null, null);
					}
				}
				else if (sKC == "F5")
				{
					if (CurWebBrowser != null)
					{
						CurWebBrowser.Refresh();
					}
				}
				else if (sKC.Contains("Alt+Oemtilde"))
				{
					btnClose_Click(null, null);
				}
				else if ((sKC.Contains("Alt") || sKC.Contains("Ctrl")) && sKC.Contains("Down"))
				{
					btnBack_Click(null, null);
				}
				else if ((sKC.Contains("Alt") || sKC.Contains("Ctrl")) && sKC.Contains("Up"))
				{
					btnUp_Click(null, null);
				}
				else if (sKC.Contains("Ctrl+M"))
				{
					btnAddPage_Click(null, null);
				}
			}
			return base.ProcessCmdKey(ref msg, keyData);
		}
		private void LoadDevices()
		{
			string[] adevices = Directory.GetLogicalDrives();
			foreach (string sdevice in adevices)
			{
				DriveInfo di = new DriveInfo(sdevice);
				if (!di.IsReady)
				{
					continue;
				}
				lDrivers.Items.Add(sdevice + "\r(" + di.VolumeLabel + ")");
			}

			if (File.Exists("Config/Favorite.ini"))
			{
				string stext = File.ReadAllText("Config/Favorite.ini");
				string[] astr = stext.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
				foreach (string str in astr)
				{
					lDrivers.Items.Add(str.Replace("\\r", "\r"));
				}
			}
		}

		Dictionary<int, List<string>> dHistories = new Dictionary<int, List<string>>();
		Dictionary<int, string> dCurPath = new Dictionary<int, string>();
		Dictionary<int, WebBrowser> dWebBrowsers = new Dictionary<int, WebBrowser>();
		private void LoadDir(string url, int curIndex = -1)
		{
			if (curIndex == -1)
			{
				curIndex = tabExplorers.SelectedIndex;
			}

			outputURL.Text = url;

			WebBrowser wb = null;
			if (tabExplorers.TabPages.Count > curIndex)
			{
				tabExplorers.SelectedIndex = curIndex;

				wb = dWebBrowsers[curIndex];
			}
			else
			{
				System.IO.DirectoryInfo di = new System.IO.DirectoryInfo(url);
				TabPage page = new TabPage(di.Name);
				page.VisibleChanged += Page_VisibleChanged;
				tabExplorers.TabPages.Add(page);
				wb = new WebBrowser();
				wb.AllowWebBrowserDrop = true;
				wb.Parent = page;
				wb.Dock = DockStyle.Fill;

				wb.Navigated += Wb_Navigated;

				curIndex = tabExplorers.TabPages.IndexOf(page);
				dWebBrowsers.Add(curIndex, wb);
			}
			wb.Navigate(url);
		}

		void AddHistory(int index, string url)
		{
			if (url == backingURL)
			{
				return;
			}
			if (!dHistories.ContainsKey(index))
			{
				dHistories.Add(index, new List<string>());
			}
			dHistories[index].Add(url);
		}
		AIniLoader _pathLoader;
		AIniLoader pathLoader
		{
			get
			{
				if (_pathLoader == null)
				{
					_pathLoader = new AIniLoader();
					_pathLoader.LoadIniFile("Config/Save.ini");
				}
				return _pathLoader;
			}
		}

		public WebBrowser CurWebBrowser
		{
			get
			{
				if (!dWebBrowsers.ContainsKey(tabExplorers.SelectedIndex))
				{
					return null;
				}
				return dWebBrowsers[tabExplorers.SelectedIndex];
			}
		}

		void AddCurPath(string url, int curIndex)
		{
			if (!bInited)
			{
				return;
			}
			if (dCurPath.ContainsKey(curIndex))
			{
				dCurPath[curIndex] = url;
			}
			else
			{
				dCurPath.Add(curIndex, url);
			}
			SaveCurPath();
		}
		void SaveCurPath()
		{
			pathLoader.OnSetValue("Count", dCurPath.Count.ToString());
			foreach (var d in dCurPath)
			{
				pathLoader.OnSetValue("U" + d.Key, d.Value);
			}
			pathLoader.OnSaveBack();
		}
		private void Page_VisibleChanged(object sender, EventArgs e)
		{
			TabPage page = sender as TabPage;
			if (!page.Visible)
			{
				return;
			}
			int index = tabExplorers.TabPages.IndexOf(page);
			if (dCurPath.ContainsKey(index))
			{
				SetCurPath(dCurPath[index]);
			}
			TestBtns(index);
		}
		void SetCurPath(string path)
		{
			outputURL.Text = path;
			this.Text = outputURL.Text;
		}
		private void Wb_Navigated(object sender, WebBrowserNavigatedEventArgs e)
		{
			TabPage tp = (sender as WebBrowser).Parent as TabPage;
			int index = tabExplorers.TabPages.IndexOf(tp);

			outputURL.Text = System.Web.HttpUtility.UrlDecode(e.Url.LocalPath);
			this.Text = outputURL.Text;

			System.IO.DirectoryInfo di = new System.IO.DirectoryInfo(outputURL.Text);
			tabExplorers.TabPages[index].Text = string.Format("{0}> {1}", di.Root.FullName.Replace(":\\", ""), di.Name);

			if (dCurPath.ContainsKey(index))
			{
				AddHistory(index, dCurPath[index]);
			}

			AddCurPath(outputURL.Text, index);

			TestBtns(index);
		}
		void TestBtns(int index)
		{
			btnBack.Enabled = dHistories.ContainsKey(index) && dHistories[index].Count > 0;
			btnForward.Enabled = dForwards.ContainsKey(index) && dForwards[index].Count > 0;
			SetSrcButtons();
		}

		private void WFTabExplorerForm_Resize(object sender, EventArgs e)
		{
			DoSaveSizeAndLocation();
		}
		void DoSaveSizeAndLocation()
		{
			ConfigurationLoader.OnSetValue("w", Width.ToString());
			ConfigurationLoader.OnSetValue("h", Height.ToString());
			ConfigurationLoader.OnSetValue("t", Top.ToString());
			ConfigurationLoader.OnSetValue("l", Left.ToString());
			ConfigurationLoader.OnSetValue("split", splitContainer.SplitterDistance.ToString());
			ConfigurationLoader.OnSaveBack();
		}

		private void btnGo_Click(object sender, EventArgs e)
		{
			if (string.IsNullOrEmpty(outputURL.Text))
			{
				return;
			}
			LoadDir(outputURL.Text.Split('\r')[0]);
		}

		private void btnAddPage_Click(object sender, EventArgs e)
		{
			LoadDir(dCurPath[tabExplorers.SelectedIndex], tabExplorers.TabPages.Count);

			tabExplorers.SelectedIndex = tabExplorers.TabCount - 1;
		}

		private string backingURL;
		private void btnBack_Click(object sender, EventArgs e)
		{
			if (dHistories.ContainsKey(tabExplorers.SelectedIndex) && dHistories[tabExplorers.SelectedIndex].Count > 0)
			{
				string url = dHistories[tabExplorers.SelectedIndex][dHistories[tabExplorers.SelectedIndex].Count - 1];
				if (!dForwards.ContainsKey(tabExplorers.SelectedIndex))
				{
					dForwards.Add(tabExplorers.SelectedIndex, new List<string>());
				}
				dForwards[tabExplorers.SelectedIndex].Add(dCurPath[tabExplorers.SelectedIndex]);

				backingURL = dCurPath[tabExplorers.SelectedIndex];

				LoadDir(url, tabExplorers.SelectedIndex);

				dHistories[tabExplorers.SelectedIndex].RemoveAt(dHistories[tabExplorers.SelectedIndex].Count - 1);

				tabExplorers.Focus();
			}
		}

		Dictionary<int, List<string>> dForwards = new Dictionary<int, List<string>>();
		private string forwardURL;
		private void btnForward_Click(object sender, EventArgs e)
		{
			if (dForwards.ContainsKey(tabExplorers.SelectedIndex) && dForwards[tabExplorers.SelectedIndex].Count > 0)
			{
				string url = dForwards[tabExplorers.SelectedIndex][dForwards[tabExplorers.SelectedIndex].Count - 1];
				forwardURL = dCurPath[tabExplorers.SelectedIndex];

				LoadDir(url, tabExplorers.SelectedIndex);

				dForwards[tabExplorers.SelectedIndex].RemoveAt(dForwards[tabExplorers.SelectedIndex].Count - 1);

				tabExplorers.Focus();
			}
		}

		private void btnUp_Click(object sender, EventArgs e)
		{
			string url = dCurPath[tabExplorers.SelectedIndex];
			DirectoryInfo di = new DirectoryInfo(url);
			if (di.Parent == null)
			{
				return;
			}
			outputURL.Text = di.Parent.FullName;
			btnGo_Click(null, null);
			tabExplorers.TabPages[tabExplorers.SelectedIndex].Focus();
		}

		private void lDrivers_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			if (lDrivers.Items.Count <= lDrivers.SelectedIndex || lDrivers.SelectedIndex == -1)
			{
				return;
			}
			string url = lDrivers.Items[lDrivers.SelectedIndex].ToString();
			outputURL.Text = url;
			btnGo_Click(null, null);
		}

		private void outputURL_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter)
			{
				btnGo_Click(null, null);
			}
		}

		private void WFTabExplorerForm_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.D && e.Alt)
			{
				outputURL.Focus();
			}
		}

		private void btnClose_Click(object sender, EventArgs e)
		{
			if (MessageBox.Show("确定要移除" + dCurPath[tabExplorers.SelectedIndex] + "的tab页吗？", "提示", MessageBoxButtons.OKCancel) == DialogResult.OK)
			{
				TabPage tp = tabExplorers.TabPages[tabExplorers.SelectedIndex];
				List<int> keys = dCurPath.Keys.ToList();
				Dictionary<int, string> temp1 = new Dictionary<int, string>();
				foreach (var k in keys)
				{
					if (k == tabExplorers.SelectedIndex)
					{
						continue;
					}
					if (k < tabExplorers.SelectedIndex)
					{
						temp1.Add(k, dCurPath[k]);
					}
					else
					{
						temp1.Add(k - 1, dCurPath[k]);
					}
				}
				dCurPath = temp1;

				keys = dForwards.Keys.ToList();
				Dictionary<int, List<string>> temp2 = new Dictionary<int, List<string>>();
				foreach (var k in keys)
				{
					if (k == tabExplorers.SelectedIndex)
					{
						continue;
					}
					if (k < tabExplorers.SelectedIndex)
					{
						temp2.Add(k, dForwards[k]);
					}
					else
					{
						temp2.Add(k - 1, dForwards[k]);
					}
				}
				dForwards = temp2;

				keys = dHistories.Keys.ToList();
				temp2 = new Dictionary<int, List<string>>();
				foreach (var k in keys)
				{
					if (k == tabExplorers.SelectedIndex)
					{
						continue;
					}
					if (k < tabExplorers.SelectedIndex)
					{
						temp2.Add(k, dHistories[k]);
					}
					else
					{
						temp2.Add(k - 1, dHistories[k]);
					}
				}
				dHistories = temp2;

				keys = dWebBrowsers.Keys.ToList();
				Dictionary<int, WebBrowser> temp3 = new Dictionary<int, WebBrowser>();
				foreach (var k in keys)
				{
					if (k == tabExplorers.SelectedIndex)
					{
						continue;
					}
					if (k < tabExplorers.SelectedIndex)
					{
						temp3.Add(k, dWebBrowsers[k]);
					}
					else
					{
						temp3.Add(k - 1, dWebBrowsers[k]);
					}
				}
				dWebBrowsers = temp3;

				tabExplorers.TabPages.Remove(tp);
			}

			SaveCurPath();
		}

		private void WFTabExplorerForm_Load(object sender, EventArgs e)
		{
			ConfigurationLoader.OnSetDefaultValue("split", 200);

			Width = ConfigurationLoader.OnGetIntValue("w");
			Height = ConfigurationLoader.OnGetIntValue("h");
			splitContainer.SplitterDistance = ConfigurationLoader.OnGetIntValue("split");
			Location = new Point(ConfigurationLoader.OnGetIntValue("l"), ConfigurationLoader.OnGetIntValue("t"));

			if (Width < 200 || Height < 100)
			{
				Width = 200;
				Height = 100;
				Location = new Point(0, 0);
				DoSaveSizeAndLocation();
			}
			this.Resize += new System.EventHandler(this.WFTabExplorerForm_Resize);
			this.Move += new System.EventHandler(this.WFTabExplorerForm_Move);
			this.splitContainer.SplitterMoved += new System.Windows.Forms.SplitterEventHandler(this.splitContainer_SplitterMoved);
			this.splitContainer.MouseDoubleClick += WFTabExplorerForm_MouseDoubleClick;
			this.splitContainer.Panel2.MouseDoubleClick += WFTabExplorerForm_MouseDoubleClick;

			this.outputURL.GotFocus += OutputURL_GotFocus;
			this.outputURL.LostFocus += OutputURL_LostFocus;
		}

		private void OutputURL_GotFocus(object sender, EventArgs e)
		{
			outputURL.ForeColor = Color.Black;
			DisvisibleButtons();
		}
		void DisvisibleButtons()
		{
			foreach (var b in lButtons)
			{
				b.Visible = false;
			}
		}
		List<Button> lButtons = new List<Button>();
		private void SetSrcButtons()
		{
			DirectoryInfo outputDir = new DirectoryInfo(outputURL.Text);
			if (!outputDir.Exists)
			{
				return;
			}
			Button curBtn = null;
			DirectoryInfo curDir = outputDir;
			int idx = 0;
			DisvisibleButtons();
			while (true)
			{
				if (curDir == null)
				{
					break;
				}
				if (curDir == outputDir.Root)
				{
					break;
				}
				curBtn = null;
				if (idx >= lButtons.Count)
				{
					curBtn = new Button();
					curBtn.AutoSizeMode = AutoSizeMode.GrowAndShrink;
					this.Controls.Add(curBtn);
					curBtn.BackColor = Color.WhiteSmoke;
					curBtn.ForeColor = Color.Black;
					curBtn.Click += CurBtn_Click;
					lButtons.Add(curBtn);
				}
				else
				{
					curBtn = lButtons[idx];
				}
				curBtn.BringToFront();
				idx++;
				curBtn.Text = curDir.Name;
				curBtn.Name = curDir.FullName;
				curBtn.Visible = true;
				curBtn.Enabled = true;
				curBtn.Width = curBtn.PreferredSize.Width;
				curDir = curDir.Parent;
			}
			idx = 0;
			curBtn = null;
			for (int i = lButtons.Count - 1; i >= 0; i--)
			{
				if (!lButtons[i].Visible)
				{
					continue;
				}
				if (curBtn == null)
				{
					lButtons[i].Location = outputURL.Location;
				}
				else
				{
					lButtons[i].Location = new Point(curBtn.Location.X + curBtn.Width, curBtn.Location.Y);
				}
				curBtn = lButtons[i];
			}
		}
		private void OutputURL_LostFocus(object sender, EventArgs e)
		{
			SetSrcButtons();
			outputURL.ForeColor = Color.White;
		}

		private void CurBtn_Click(object sender, EventArgs e)
		{
			LoadDir((sender as Button).Name);
			tabExplorers.Select();
		}

		private void WFTabExplorerForm_Move(object sender, EventArgs e)
		{
			DoSaveSizeAndLocation();
		}

		private void WFTabExplorerForm_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			btnAddPage_Click(null, null);
		}

		private void lDrivers_SelectedIndexChanged(object sender, EventArgs e)
		{
			lDrivers_MouseDoubleClick(null, null);
			tabExplorers.TabPages[tabExplorers.SelectedIndex].Focus();
		}

		protected override void OnMouseUp(MouseEventArgs e)
		{
			base.OnMouseUp(e);

			if (e.Button == MouseButtons.Right)
			{
				Process.Start(Environment.CurrentDirectory);
			}
		}

		private void tabExplorers_SelectedIndexChanged(object sender, EventArgs e)
		{
			pathLoader.OnSetValue("Cur", tabExplorers.SelectedIndex.ToString());
			pathLoader.OnSaveBack();
		}

		private void splitContainer_SplitterMoved(object sender, SplitterEventArgs e)
		{
			DoSaveSizeAndLocation();
		}

		string CurPath
		{
			get
			{
				if (dCurPath.ContainsKey(tabExplorers.SelectedIndex))
				{
					return dCurPath[tabExplorers.SelectedIndex];
				}
				return "";
			}
		}
		private void btnNewDir_Click(object sender, EventArgs e)
		{
			if (string.IsNullOrEmpty(CurPath))
			{
				return;
			}
			string sdir = CurPath + "/新文件夹";

			if (!Directory.Exists(sdir))
			{
				Directory.CreateDirectory(sdir);
			}
			else
			{
				int idx = 1;
				while (Directory.Exists(sdir + idx))
				{
					idx++;
				}
				Directory.CreateDirectory(sdir + idx);
			}

			tabExplorers.TabPages[tabExplorers.SelectedIndex].Focus();
		}
	}
}
