/*
 * Please leave this Copyright notice in your code if you use it
 * Written by Decebal Mihailescu [http://www.codeproject.com/script/articles/list_articles.asp?userid=634640]
 */
using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;
namespace AboutUtil
{
    using System.Text.RegularExpressions;
    using Microsoft.Win32;
    /// <summary>
    /// Summary description for About.
    /// </summary>
    public class About : System.Windows.Forms.Form
    {
        internal class MsgHookWindow : NativeWindow, IDisposable
        {
            IWin32Window _owner;
            internal MsgHookWindow(IWin32Window owner)
            {
                _owner = owner;
                AssignHandle(_owner.Handle);
            }
            protected override void WndProc(ref Message m)
            {
                switch (m.Msg)
                {
                    case About.WM_SYSCOMMAND:

                        if (m.WParam.ToInt32() == About.IDM_ABOUT)
                        {
                            About dlg = new About(_owner);
                            dlg.ShowDialog();
                            dlg.Dispose();
                        }
                        break;
                }
                base.WndProc(ref m);
            }

            #region IDisposable Members

            public void Dispose()
            {
                ReleaseHandle();
            }

            #endregion
        }
        static MsgHookWindow _wndHook;
        private RichTextBox richTextBox1;
        static List<KeyValuePair<string, string>> s_products;
        static internal void InitSysMenu(System.Windows.Forms.IWin32Window wnd)
        {
            IntPtr sysMenuHandle = About.GetSystemMenu(wnd.Handle, false);
            About.AppendMenu(sysMenuHandle, About.MF_SEPARATOR, new IntPtr(0), string.Empty);
            Assembly crt = Assembly.GetExecutingAssembly();
            object[] attr = crt.GetCustomAttributes(typeof(AssemblyDescriptionAttribute), true);
            AssemblyDescriptionAttribute desc = attr[0] as AssemblyDescriptionAttribute;
            About.AppendMenu(sysMenuHandle, About.MF_STRING, new IntPtr(About.IDM_ABOUT), "About " + desc.Description);
            if (_wndHook == null)
                _wndHook = new MsgHookWindow(wnd);
        }
        static internal readonly Int32 IDM_ABOUT = (int)RegisterWindowMessage("About Message");
        //5000;
        //internal delegate void AboutHandler();
        internal const Int32 WM_SYSCOMMAND = 0x112;
        internal const Int32 MF_SEPARATOR = 0x800;
        private TextBox _txtkey;
        private ComboBox _cbxProduct;
        internal const Int32 MF_STRING = 0x0;
        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        static extern uint RegisterWindowMessage(string lpString);
        [DllImport("user32.dll")]
        internal static extern IntPtr GetSystemMenu(IntPtr hWnd, [MarshalAs(UnmanagedType.Bool)]bool bRevert);
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool AppendMenu(IntPtr hMenu, Int32 wFlags, IntPtr wIDNewItem, string lpNewItem);
        private System.Windows.Forms.LinkLabel _linkLabelAbout;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label _lblFromReflection;
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.Container components = null;
        static About()
        {
            s_products = new List<KeyValuePair<string, string>>(1);
            s_products.Add(new KeyValuePair<string, string>("Windows XP key", GetXPCDKey()));
            //Console.WriteLine("XP key:      {0}\n**************************", GetXPCDKey());
            for (int i = 7; i < 15; i++)
            {
                List<KeyValuePair<string, string>> lst = GetOfficeCDKeys(i);
                if (lst == null)
                    continue;
                //list.Add(new KeyValuePair<string, string>("Windows XP key", GetXPCDKey()));
                lst.ForEach(delegate(KeyValuePair<string, string> prod)
                {
                    string name = string.Format("{0} version {1}", prod.Key, i);
                    s_products.Add(new KeyValuePair<string, string>(name, prod.Value));

                });
            }
            //s_products.ForEach(delegate(KeyValuePair<string, string> prod)
            //{
            //    Console.WriteLine("{0} key: {1}\n**************************",
            //        prod.Key, prod.Value);
            //});
        }
        public About(IWin32Window owner)
        {
            Owner = owner as Form;
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();

            //
            // TODO: Add any constructor code after InitializeComponent call
            //
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            if (!disposing)
            {
                if (_wndHook != null)
                    _wndHook.Dispose();
                _wndHook = null;
            }
            base.Dispose(disposing);
        }
        ~About()
        {
            Dispose(false);
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #region Windows Form Designer generated code
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.Label label2;
            System.Windows.Forms.Label label3;
            System.Windows.Forms.Label label4;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(About));
            this._linkLabelAbout = new System.Windows.Forms.LinkLabel();
            this.button1 = new System.Windows.Forms.Button();
            this._lblFromReflection = new System.Windows.Forms.Label();
            this._txtkey = new System.Windows.Forms.TextBox();
            this._cbxProduct = new System.Windows.Forms.ComboBox();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            label2 = new System.Windows.Forms.Label();
            label3 = new System.Windows.Forms.Label();
            label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(15, 198);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(119, 13);
            label2.TabIndex = 4;
            label2.Text = "Microsoft License Keys:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new System.Drawing.Point(12, 262);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(46, 13);
            label3.TabIndex = 4;
            label3.Text = "CD Key:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new System.Drawing.Point(12, 198);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(119, 13);
            label4.TabIndex = 4;
            label4.Text = "Microsoft License Keys:";
            // 
            // _linkLabelAbout
            // 
            this._linkLabelAbout.LinkArea = new System.Windows.Forms.LinkArea(36, 0);
            this._linkLabelAbout.Location = new System.Drawing.Point(15, 327);
            this._linkLabelAbout.Name = "_linkLabelAbout";
            this._linkLabelAbout.Size = new System.Drawing.Size(246, 24);
            this._linkLabelAbout.TabIndex = 0;
            this._linkLabelAbout.Text = "Copyright ?2008  Decebal Mihailescu";
            this._linkLabelAbout.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this._linkLabelAbout.UseCompatibleTextRendering = true;
            this._linkLabelAbout.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelAbout_LinkClicked);
            // 
            // button1
            // 
            this.button1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button1.Location = new System.Drawing.Point(212, 354);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "&OK";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // _lblFromReflection
            // 
            this._lblFromReflection.AutoSize = true;
            this._lblFromReflection.Location = new System.Drawing.Point(12, 9);
            this._lblFromReflection.Name = "_lblFromReflection";
            this._lblFromReflection.Size = new System.Drawing.Size(93, 13);
            this._lblFromReflection.TabIndex = 3;
            this._lblFromReflection.Text = "text from reflection";
            // 
            // _txtkey
            // 
            this._txtkey.Location = new System.Drawing.Point(15, 288);
            this._txtkey.Name = "_txtkey";
            this._txtkey.ReadOnly = true;
            this._txtkey.Size = new System.Drawing.Size(474, 20);
            this._txtkey.TabIndex = 5;
            // 
            // _cbxProduct
            // 
            this._cbxProduct.DisplayMember = "Key";
            this._cbxProduct.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._cbxProduct.FormattingEnabled = true;
            this._cbxProduct.Location = new System.Drawing.Point(15, 227);
            this._cbxProduct.Name = "_cbxProduct";
            this._cbxProduct.Size = new System.Drawing.Size(474, 21);
            this._cbxProduct.TabIndex = 6;
            this._cbxProduct.ValueMember = "Value";
            this._cbxProduct.SelectedIndexChanged += new System.EventHandler(this._cbxProduct_SelectedIndexChanged);
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(15, 35);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.ReadOnly = true;
            this.richTextBox1.Size = new System.Drawing.Size(474, 160);
            this.richTextBox1.TabIndex = 7;
            this.richTextBox1.Text = resources.GetString("richTextBox1.Text");
            // 
            // About
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(501, 385);
            this.ControlBox = false;
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this._cbxProduct);
            this.Controls.Add(this._txtkey);
            this.Controls.Add(label3);
            this.Controls.Add(label4);
            this.Controls.Add(label2);
            this.Controls.Add(this._lblFromReflection);
            this.Controls.Add(this.button1);
            this.Controls.Add(this._linkLabelAbout);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "About";
            this.ShowInTaskbar = false;
            this.Text = "About";
            this.Load += new System.EventHandler(this.About_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion

        private void linkLabelAbout_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
        {
            System.Windows.Forms.LinkLabel llbl = sender as System.Windows.Forms.LinkLabel;
            if (llbl != null)
            {
                llbl.Links[llbl.Links.IndexOf(e.Link)].Visited = true;
                string target = e.Link.LinkData as string;
                if (target != null && target.Length > 0)
                    System.Diagnostics.Process.Start(target);
            }
        }

        private void About_Load(object sender, System.EventArgs e)
        {


            Assembly crt = Assembly.GetExecutingAssembly();
            object[] attr = crt.GetCustomAttributes(typeof(AssemblyDescriptionAttribute), true);
            AssemblyDescriptionAttribute desc = attr[0] as AssemblyDescriptionAttribute;
            attr = crt.GetCustomAttributes(typeof(AssemblyFileVersionAttribute), true);
            AssemblyFileVersionAttribute ver = attr[0] as AssemblyFileVersionAttribute;
            Version v = crt.GetName().Version;
            _lblFromReflection.Text = string.Format("{0};  File Version: {1};  Assembly Version: {2}",
                desc.Description, ver.Version, v.ToString());
            attr = crt.GetCustomAttributes(typeof(AssemblyTitleAttribute), true);
            AssemblyTitleAttribute ta = attr[0] as AssemblyTitleAttribute;
            Text = string.Format("About  {0} {1}.{2}", ta.Title, v.Major, v.Minor); //Owner.Text     

            attr = crt.GetCustomAttributes(typeof(AssemblyCopyrightAttribute), true);
            AssemblyCopyrightAttribute copyright = attr[0] as AssemblyCopyrightAttribute;
            string strcpy = copyright.Copyright;

            _linkLabelAbout.Text = strcpy;
            attr = crt.GetCustomAttributes(typeof(AssemblyCompanyAttribute), true);
            AssemblyCompanyAttribute company = attr[0] as AssemblyCompanyAttribute;
            // look for the name in copyright string to activate if found
            int start = strcpy.IndexOf(company.Company, 0, StringComparison.InvariantCultureIgnoreCase);
            if (start != -1)
            {
                this._linkLabelAbout.LinkArea = new System.Windows.Forms.LinkArea(start, company.Company.Length);
            }
            else
            {// company not found in copyright string, so check for Copyright ?
                int size = "Copyright ?".Length;
                start = strcpy.IndexOf("Copyright ?, 0, StringComparison.InvariantCultureIgnoreCase");
                if (start != -1)
                {
                    this._linkLabelAbout.LinkArea = new System.Windows.Forms.LinkArea(size, strcpy.Length - size);
                }
                else
                {// no Copyright ?string, activate the whole area
                    this._linkLabelAbout.LinkArea = new System.Windows.Forms.LinkArea(0, strcpy.Length);
                }
            }
            if (_linkLabelAbout.LinkArea != null && !_linkLabelAbout.LinkArea.IsEmpty)
                this._linkLabelAbout.Links[0].LinkData = @"www.codeproject.com/script/Articles/list_articles.asp?userid=634640";
            _cbxProduct.DataSource = s_products;
        }

        /// <summary>
        /// gets the CD Key for Windows XP in the format XXXXX-XXXXX-XXXXX-XXXXX-XXXXX
        /// </summary>
        /// <returns>the CD key or nothing upon failure</returns>
        public static string GetXPCDKey()
        {
            RegistryKey regKey = null;
            try
            {
                regKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\MICROSOFT\\Windows NT\\CurrentVersion", false);
                if (regKey == null)
                    return string.Empty;

                //Read the value of DigitalProductID
                byte[] DigitalProductID = regKey.GetValue("DigitalProductId", null, RegistryValueOptions.None) as byte[];
                if (DigitalProductID == null)
                    return string.Empty;
                return ExtractCDKey(DigitalProductID);

            }
            catch
            {
                return string.Empty;
            }
            finally
            {
                if (regKey != null)
                    regKey.Close();

            }
        }


        static readonly Regex _guidRegEx = new Regex(@"^(\{{0,1}([0-9a-fA-F]){8}-([0-9a-fA-F]){4}-([0-9a-fA-F]){4}-([0-9a-fA-F]){4}-([0-9a-fA-F]){12}\}{0,1})$");
        /// <summary>
        /// gets a list of product name and their respective keys
        /// </summary>
        /// <param name="ver"> the office version</param>
        /// <returns> a list of product types or nothing upon failure</returns>
        private static List<KeyValuePair<string, string>> GetOfficeCDKeys(int ver)
        {
            RegistryKey regKeyBase = null;

            try
            {
                //open the office registration registry key
                string baseRegName = string.Format(@"SOFTWARE\Microsoft\Office\{0}.0\Registration\", ver);
                regKeyBase = Registry.LocalMachine.OpenSubKey(baseRegName, false);
                if (regKeyBase == null)
                    return null;
                //get all subkey names
                string[] sknames = regKeyBase.GetSubKeyNames();
                //List<product> lst = new List<product>(sknames.Length);
                List<KeyValuePair<string, string>> list = new List<KeyValuePair<string, string>>(1);

                foreach (string skname in sknames)
                {
                    //get all the GUID like reg subkeys
                    if (!_guidRegEx.IsMatch(skname))
                        continue;
                    RegistryKey regSubKey = regKeyBase.OpenSubKey(skname, false);
                    try
                    {
                        regSubKey = regKeyBase.OpenSubKey(skname, false);
                        if (regSubKey == null)
                            continue;

                        //Read the value of DigitalProductID
                        byte[] DigitalProductID = regSubKey.GetValue("DigitalProductId", null, RegistryValueOptions.None) as byte[];
                        if (DigitalProductID == null)
                            continue;
                        string key = ExtractCDKey(DigitalProductID);
                        if (string.IsNullOrEmpty(key))
                            continue;
                        string prodname = regSubKey.GetValue("ProductName", null, RegistryValueOptions.None) as string;
                        if (string.IsNullOrEmpty(prodname))
                            continue;
                        //create the list of pairs
                        //product prd = new product(prodname, key);
                        KeyValuePair<string, string> kv = new KeyValuePair<string, string>(prodname, key);
                        list.Add(kv);
                        //lst.Add(prd);
                    }
                    catch
                    {
                        continue;
                    }
                    finally
                    {
                        if (regSubKey != null)
                            regSubKey.Close();
                    }
                }
                return list;
            }
            catch
            {
                return null;
            }
            finally
            {
                if (regKeyBase != null)
                    regKeyBase.Close();

            }
        }
        private static string ExtractCDKey(byte[] DigitalProductID)
        {
            StringBuilder sCDKey = null;
            try
            {
                sCDKey = new StringBuilder(29);
                const string keyset = "BCDFGHJKMPQRTVWXY2346789";
                for (long ilByte = 24; ilByte >= 0; ilByte--)
                {
                    int nCur = 0;
                    for (long ilKeyByte = 66; ilKeyByte >= 52; ilKeyByte--)
                    {
                        //use some  bytes from the DigitalProductId
                        nCur = nCur * 256 ^ DigitalProductID[ilKeyByte];
                        DigitalProductID[ilKeyByte] = Convert.ToByte(nCur / 24);
                        nCur = nCur % 24;
                    }
                    sCDKey.Insert(0, keyset[nCur]);
                    if (ilByte % 5 == 0 & ilByte != 0)
                        sCDKey = sCDKey.Insert(0, '-');
                }
                return sCDKey.ToString();
            }
            finally
            {
                if (sCDKey != null)
                    sCDKey.Length = 0;
            }
        }

        private void _cbxProduct_SelectedIndexChanged(object sender, EventArgs e)
        {
            _txtkey.Text = _cbxProduct.SelectedValue as string;
        }


    }
}
