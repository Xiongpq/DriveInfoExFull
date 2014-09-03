using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using IOEx;
using AboutUtil;

namespace UIClient
{
    public partial class MainForm : Form
    {
        DriveListEx m_list;
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            m_list = new DriveListEx();
            var count = m_list.Load();

            m_dataGridView.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.DisplayedCellsExceptHeader);
            m_dataGridView.DataSource = m_list;
            About.InitSysMenu(this);
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (m_list != null)
            {
                m_list.Dispose();
            }
        }

        private void btnAbout_Click(object sender, EventArgs e)
        {
            About aboutfrm = new About(this);
            aboutfrm.ShowDialog(this);
            aboutfrm.Dispose();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnRefersh_Click(object sender, EventArgs e)
        {
            m_list.Load();
            m_dataGridView.DataSource = m_list;
        }
    }
}