using System;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Drawing;

namespace VideoManagement.Common
{
    public class ThumbNail
    {
        public static string Generate(string video, string thumbnail)
        {
            var cmd = $"ffmpeg  -itsoffset -1  -i {video}  -vcodec mjpeg -vframes 1 -an -f rawvideo -s 320x240  {thumbnail}";

            var startInfo = new ProcessStartInfo
            {
                WindowStyle = ProcessWindowStyle.Hidden,
                FileName = "cmd.exe",
                Arguments = "/C " + cmd
            };

            var process = new Process
            {
                StartInfo = startInfo
            };

            process.Start();
            process.WaitForExit(5000);

            return thumbnail;
        }
    }
}
