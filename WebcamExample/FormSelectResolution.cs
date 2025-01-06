using AForge.Video.DirectShow;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WebcamExample
{
    public partial class FormSelectResolution : Form
    {
        public VideoCapabilitiesHolder Result { get; set; }

        public FormSelectResolution(VideoCapabilities[] videoCapabilities)
        {
            InitializeComponent();
            bool first = true;
            foreach (var v in videoCapabilities)
            {
                VideoCapabilitiesHolder holder = new VideoCapabilitiesHolder(v);
                listBox1.Items.Add(holder);
                if (first)
                {
                    first = false;
                    listBox1.SelectedIndex = 0;
                }
            }
        }

        private void IDOK_Click(object sender, EventArgs e)
        {
            OK();
        }

        private void OK()
        {
            Result = listBox1.SelectedItem as VideoCapabilitiesHolder;
            DialogResult = DialogResult.OK;
            Close();

        }
        private void IDCANCEL_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void listBox1_DoubleClick(object sender, EventArgs e)
        {
            OK();
        }
    }
}
