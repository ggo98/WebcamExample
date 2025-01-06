using AForge.Video.DirectShow;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace WebcamExample
{
    public class VideoCapabilitiesHolder
    {
        private VideoCapabilities _videoCapabilities;

        public static implicit operator VideoCapabilities(VideoCapabilitiesHolder videoCapabilitiesHolder) => videoCapabilitiesHolder._videoCapabilities;

        public VideoCapabilitiesHolder(VideoCapabilities videoCapabilities)
        {
            _videoCapabilities = videoCapabilities;
        }

        public override string ToString()
        {
            return $"{_videoCapabilities.FrameSize}\t{_videoCapabilities.AverageFrameRate}\t{_videoCapabilities.MaximumFrameRate}\t{_videoCapabilities.BitCount}";
        }
    }
}
