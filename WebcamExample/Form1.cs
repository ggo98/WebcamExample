using AForge.Video;
using AForge.Video.DirectShow;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace WebcamExample
{
    public partial class Form1 : Form
    {
        private FilterInfoCollection _videoDevices;
        private VideoCaptureDevice _videoSource;

        public Form1()
        {
            InitializeComponent();

            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom;

            foreach (Control v in this.Controls)
            {
                v.BackColor = Color.Black;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Stop();

            var videoDevice = GetVideoDevice();
            if (videoDevice == null)
                return;
            _videoSource = new VideoCaptureDevice(videoDevice.MonikerString);

            _videoSource.VideoResolution = _videoSource.VideoCapabilities[0];
            FormSelectResolution F = new FormSelectResolution(_videoSource.VideoCapabilities);
            if (F.ShowDialog() == DialogResult.OK
                && null != F.Result)
            {
                //MessageBox.Show(F.Result.ToString());
                _videoSource.VideoResolution = F.Result;
            }


            _videoSource.NewFrame += VideoSource_NewFrame;
            _videoSource.Start();

            pictureBox1.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Stop();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Stop();
        }

        private void VideoSource_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            pictureBox1.Image = (Bitmap)eventArgs.Frame.Clone();
        }

        private void Stop()
        {
            if (!IsControlDown())
                pictureBox1.Hide();
            if (_videoSource != null && _videoSource.IsRunning)
            {
                _videoSource.SignalToStop();
                _videoSource.WaitForStop();
                _videoSource = null;
            }
        }

        private FilterInfo GetVideoDevice()
        {
            FilterInfoCollection videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);

            if (videoDevices.Count == 0)
            {
                _videoDevices = null;
                MessageBox.Show("No video devices found.");
                return null;
            }
            _videoDevices = videoDevices;

            // note: device:pnp = 'real' device, device:sw = software-emulated or virtual devices
            foreach (FilterInfo v in videoDevices)
                Console.WriteLine($"{v.GetType().FullName}\t{v.Name}\t{v.MonikerString}");
            //var ret = (from data in videoDevices
            //           where  data)
            return videoDevices[0];
        }

        private void button3_Click(object sender, EventArgs e)
        {
            GetVideoDevice();
            GetVideoCaptureCapabilities();
        }

        private VideoCapabilities GetVideoCaptureCapabilities()
        {
            var videoDevice = GetVideoDevice();
            if (videoDevice == null)
                return null;
            VideoCaptureDevice videoSource = new VideoCaptureDevice(videoDevice.MonikerString);
            VideoCapabilities[] videoCapabilities = videoSource.VideoCapabilities;

            int index = 0;
            foreach (VideoCapabilities v in videoCapabilities)
            {
                //Console.WriteLine();
                Console.WriteLine($"{index++}\t{v.FrameSize}\t{v.AverageFrameRate}\t{v.MaximumFrameRate}\t{v.BitCount}");
            }
            return videoCapabilities[0];
        }

        private void button4_Click(object sender, EventArgs e)
        {
            GC.Collect(0, GCCollectionMode.Forced);
        }

        public static bool IsControlDown()
        {
            return (Control.ModifierKeys & Keys.Control) == Keys.Control;
        }
    }
}
