using System;
using System.Diagnostics;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Net;
using System.IO;
using System.Windows.Forms;
using System.Data;
using System.Threading;
using HtmlAgilityPack;
using Varan.Text;
using Varan.Internet;

namespace StarWarsFigureImages {
	public class Form1 : System.Windows.Forms.Form {
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.TextBox tbURL;
		private System.Windows.Forms.TextBox tbUsername;
		private System.Windows.Forms.TextBox tbPassword;
		private System.Windows.Forms.TextBox tbDomain;
		private System.Windows.Forms.CheckBox checkBox1;
		private System.Windows.Forms.TextBox tbProxy;
		private System.ComponentModel.Container components = null;

		public Form1() {
			InitializeComponent();
			tbURL.Text = "http://www.rebelscum.com/ch-vint.asp" + Environment.NewLine +
							"http://www.rebelscum.com/CH-POTF2.ASP" + Environment.NewLine +
							"http://www.rebelscum.com/CH-sote.ASP" + Environment.NewLine +
							"http://www.rebelscum.com/ch-e1.asp" + Environment.NewLine +
							"http://www.rebelscum.com/ch-potje.asp" + Environment.NewLine +
							"http://www.rebelscum.com/ch-saga.asp" + Environment.NewLine +
							"http://www.rebelscum.com/ch-CW.asp" + Environment.NewLine +
							"http://www.rebelscum.com/ch-OTC.asp" + Environment.NewLine +
							"http://www.rebelscum.com/ch-ROTS.asp";
		}

		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.button1 = new System.Windows.Forms.Button();
			this.tbURL = new System.Windows.Forms.TextBox();
			this.tbUsername = new System.Windows.Forms.TextBox();
			this.tbPassword = new System.Windows.Forms.TextBox();
			this.tbDomain = new System.Windows.Forms.TextBox();
			this.checkBox1 = new System.Windows.Forms.CheckBox();
			this.tbProxy = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(72, 296);
			this.button1.Name = "button1";
			this.button1.TabIndex = 0;
			this.button1.Text = "Go!";
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// tbURL
			// 
			this.tbURL.AcceptsReturn = true;
			this.tbURL.Location = new System.Drawing.Point(24, 16);
			this.tbURL.Multiline = true;
			this.tbURL.Name = "tbURL";
			this.tbURL.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.tbURL.Size = new System.Drawing.Size(352, 144);
			this.tbURL.TabIndex = 1;
			this.tbURL.Text = "";
			this.tbURL.WordWrap = false;
			// 
			// tbUsername
			// 
			this.tbUsername.Location = new System.Drawing.Point(392, 232);
			this.tbUsername.Name = "tbUsername";
			this.tbUsername.Size = new System.Drawing.Size(168, 20);
			this.tbUsername.TabIndex = 2;
			this.tbUsername.Text = "John_Varan";
			// 
			// tbPassword
			// 
			this.tbPassword.Location = new System.Drawing.Point(392, 264);
			this.tbPassword.Name = "tbPassword";
			this.tbPassword.PasswordChar = '*';
			this.tbPassword.Size = new System.Drawing.Size(168, 20);
			this.tbPassword.TabIndex = 3;
			this.tbPassword.Text = "Cl0nePilot";
			// 
			// tbDomain
			// 
			this.tbDomain.Location = new System.Drawing.Point(392, 296);
			this.tbDomain.Name = "tbDomain";
			this.tbDomain.Size = new System.Drawing.Size(168, 20);
			this.tbDomain.TabIndex = 4;
			this.tbDomain.Text = "AMERICAS";
			// 
			// checkBox1
			// 
			this.checkBox1.Checked = true;
			this.checkBox1.CheckState = System.Windows.Forms.CheckState.Checked;
			this.checkBox1.Location = new System.Drawing.Point(360, 168);
			this.checkBox1.Name = "checkBox1";
			this.checkBox1.Size = new System.Drawing.Size(88, 24);
			this.checkBox1.TabIndex = 5;
			this.checkBox1.Text = "Use Proxy";
			this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
			// 
			// tbProxy
			// 
			this.tbProxy.Location = new System.Drawing.Point(392, 200);
			this.tbProxy.Name = "tbProxy";
			this.tbProxy.Size = new System.Drawing.Size(168, 20);
			this.tbProxy.TabIndex = 6;
			this.tbProxy.Text = "http://proxy";
			// 
			// Form1
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(616, 350);
			this.Controls.Add(this.tbProxy);
			this.Controls.Add(this.tbDomain);
			this.Controls.Add(this.tbPassword);
			this.Controls.Add(this.tbUsername);
			this.Controls.Add(this.tbURL);
			this.Controls.Add(this.checkBox1);
			this.Controls.Add(this.button1);
			this.Name = "Form1";
			this.Text = "Form1";
			this.ResumeLayout(false);

		}
		#endregion

		[STAThread]
		static void Main() 
		{
			Application.Run(new Form1());
		}

		private void button1_Click(object sender, System.EventArgs e) {

			bool debug = false;
			string[] urlLines = tbURL.Text.Split('\n');
			string filenameChecklist = "temp.html";
			string filenameDebug = "debug.txt";
			string filenameFigure = "temp_figure.html";

			File.Delete(filenameDebug);
			File.Delete(filenameChecklist);
			File.Delete(filenameFigure);

			for(int i=0; i < urlLines.Length; i++) { 
				StreamWriter swChecklist = new StreamWriter(filenameChecklist, false);
				string[] ignore = {""};
				string url = urlLines[i].ToString();
				swChecklist.Write(Web.GetHtmlForPage(url, Web.GetDefaultProxy(tbUsername.Text, tbPassword.Text, tbDomain.Text)));
				swChecklist.Close();

				//parsing html sample
				StreamWriter swDebug = null;
				if(debug) {
					swDebug = new StreamWriter(filenameDebug, true);
				}
				HtmlDocument h = new HtmlDocument();
				h.Load(filenameChecklist);

				bool inValidSection = false;
				string title = "";
				foreach(HtmlNode n in h.DocumentNode.SelectNodes("//*")) {
					if(n.Name.ToLower() == "title") {
						title = CaseConvertor.ToTitleCase(n.InnerText.ToLower().Replace("rebelscum.com:","").Replace("star wars checklist","").Trim());
						if(debug) {
							swDebug.WriteLine("TITLE: " + title);
						}
						Directory.CreateDirectory(title);
					}
					
					if(n.Name.ToLower() == "b") {
						if(SectionValid(n.InnerText)) {
							inValidSection = true;
							if(debug) {
								swDebug.WriteLine(n.InnerText);
							}
						} else {
							inValidSection = false;
						}
					}

					if(inValidSection && n.Name.ToLower() == "a") {
						string link = n.Attributes["href"].Value;
						if(link.IndexOf("*") < 0) {
							if(debug) {
								swDebug.WriteLine("     " + link);
							}
							WebRequest figureReq = HttpWebRequest.Create(link);
							figureReq.Proxy = Web.GetDefaultProxy();
							StreamReader srFigure = new StreamReader(figureReq.GetResponse().GetResponseStream());
							StreamWriter swFigure = new StreamWriter(filenameFigure, false);
							swFigure.Write(srFigure.ReadToEnd());
							swFigure.Close();
							srFigure.Close();

							HtmlDocument hFigure = new HtmlDocument();
							hFigure.Load(filenameFigure);
							bool doneWithHeader = false;
							bool doneWithFigure = false;
							bool doneWithImage = false;
							string finalFigureName = "";
							foreach(HtmlNode figNode in hFigure.DocumentNode.SelectNodes("//*")) {
								string name = figNode.Name.ToLower();

								if(name == "img" && !doneWithFigure) {
									string src = figNode.Attributes["src"].Value;
									if(src == "images/archivesheader-top.gif") {
										doneWithHeader = true;
									}
								}

								if(name == "font" && doneWithHeader && !doneWithFigure) {
									string figureName = "";
									int idx = figNode.InnerText.IndexOf(Environment.NewLine);
									if(idx >= 0) {
										if(idx == 0) {
											figureName = figNode.InnerText.Substring(2, figNode.InnerText.Length - 2);
										} else {
											figureName = figNode.InnerText.Substring(0, idx + 1);
										}
									} else {
										figureName = figNode.InnerText;
									}
									finalFigureName = figNode.InnerText.Replace(Environment.NewLine, "");
									finalFigureName = CaseConvertor.ToTitleCase(finalFigureName);
									finalFigureName = finalFigureName.Replace("&", "and");
									if(finalFigureName.IndexOf("/") >= 0) {
										finalFigureName = finalFigureName.Substring(0, finalFigureName.IndexOf("/")).Trim();
									}
									if(debug) {
										swDebug.WriteLine("          " + finalFigureName);
									}
									doneWithFigure = true;
								}

								if(!doneWithImage && doneWithFigure && name == "img") {
									string imgSrc = figNode.Attributes["src"].Value;
									if(imgSrc.IndexOf("http://") < 0) {
										Uri u = new Uri(urlLines[i]);
										string rootUrl = u.AbsoluteUri.Substring(0, u.AbsoluteUri.Length - u.AbsolutePath.Length);
									
										if(imgSrc.StartsWith("/")) {
											imgSrc = rootUrl + imgSrc;
										} else {
											imgSrc = rootUrl + "/" + imgSrc;
										}
									}
									if(imgSrc.IndexOf("archivesheader") < 0 && doneWithFigure) {
										if(debug) {
											swDebug.WriteLine("              " + imgSrc);
										}
										
										string targetImg = Path.GetDirectoryName(Application.ExecutablePath) + "\\" + title + "\\" + finalFigureName + Path.GetExtension(imgSrc);
										if(!File.Exists(targetImg)) {
											Web.DownloadFile(imgSrc, targetImg); 
										}
										doneWithImage = true;
									}
								}
							}
						}
					}
				}

				if(debug) {
					swDebug.Close();
				}
			}
			MessageBox.Show(this, "Done.");
		}

		private bool SectionValid(string sectionName) {
			bool valid = false;

			sectionName = sectionName.ToLower();
			int i = sectionName.IndexOf("multipacks");
			if(sectionName.IndexOf("figures") >= 0 || sectionName.IndexOf("creature") >= 0 || 
				sectionName.IndexOf("vehicle") >= 0 || sectionName.IndexOf("playset") >= 0 ||
				sectionName.IndexOf("scenes") >= 0 || sectionName.IndexOf("sets") >= 0 ||
				sectionName.IndexOf("multipacks") >= 0 || sectionName.IndexOf("exclusive") >= 0 ||
				sectionName.IndexOf("sy snootles") >= 0 || sectionName.IndexOf("droids") >= 0 ||
				sectionName.IndexOf("ewoks") >= 0 || sectionName.IndexOf("rigs") >= 0 ||
				sectionName.IndexOf("band") >= 0 || sectionName.IndexOf("princess leia") >= 0 ||
				sectionName.IndexOf("sneak") >= 0 || sectionName.IndexOf("evolution") >= 0 ||
				sectionName.IndexOf("electronic power") >= 0) {
				valid = true;
			}
			
			if(sectionName.IndexOf("12\"") >= 0 || sectionName.IndexOf("cups") >= 0 ||
				sectionName.IndexOf("micro") >= 0 || sectionName.IndexOf("miscellaneous") >= 0 ||
				sectionName.IndexOf("battle arena") >= 0) {
				valid = false;
			}
			return valid;
		}

		private void checkBox1_CheckedChanged(object sender, System.EventArgs e) {
			if(((CheckBox)sender).Checked) {
				tbProxy.Enabled = true;
				tbUsername.Enabled = true;
				tbPassword.Enabled = true;
				tbDomain.Enabled = true;
			} else {
				tbProxy.Enabled = false;
				tbUsername.Enabled = false;
				tbPassword.Enabled = false;
				tbDomain.Enabled = false;
			}
		}
	}
}
