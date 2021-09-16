using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace PM
{
    public partial class WaitForm : Form
    {
        public MainForm mainForm { get; set; }
        public WaitForm()
        {
            InitializeComponent();
            this.ControlBox = false;
        }
        
        private void WaitForm_Load(object sender, EventArgs e)
        {
            mainForm.Enabled = false;
        }

        public void freshProgress(Int32 value)
        {
            WaitForm_ProgressBar.Value = value;
        }

        private void WaitForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            mainForm.Enabled = true;
            WaitForm_ProgressBar.Value = 0;
        }
    }
}
