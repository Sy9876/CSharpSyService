using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SyService
{
    public partial class Service1 : ServiceBase
    {
        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            FileStream fs = new FileStream(@"e:\syService.log", FileMode.OpenOrCreate, FileAccess.Write);
            StreamWriter sw = new StreamWriter(fs);
            sw.BaseStream.Seek(0, SeekOrigin.End);
            sw.WriteLine("SyService: Started " + DateTime.Now.ToString() + "\n");

            sw.WriteLine("SyService: takeScreenShotMain " + "\n");
            takeScreenShotMain();
            sw.WriteLine("SyService: takeScreenShotMain done" + "\n");

            sw.Flush();
            sw.Close();
            fs.Close();
        }

        protected override void OnStop()
        {
            FileStream fs = new FileStream(@"e:\syService.log", FileMode.OpenOrCreate, FileAccess.Write);
            StreamWriter sw = new StreamWriter(fs);
            sw.BaseStream.Seek(0, SeekOrigin.End);
            sw.WriteLine("SyService: Stopped " + DateTime.Now.ToString() + "\n");
            sw.Flush();
            sw.Close();
            fs.Close();

        }

        public void takeScreenShotMain()
        {
            string dateStr = DateTime.Now.ToString("yyyyMMdd_HHmmss");
            string filePath = @"E:\SyServiceScreenShot_" + dateStr + ".png";
            takeScreenShot(filePath);
        }
        public void takeScreenShot(string filePath)
        {
            Rectangle screenBounds = SystemInformation.VirtualScreen;
            Bitmap screenshotObject = new Bitmap(screenBounds.Width, screenBounds.Height);
            Graphics drawingGraphics = Graphics.FromImage(screenshotObject);
            drawingGraphics.CopyFromScreen(screenBounds.Location, Point.Empty, screenBounds.Size);
            screenshotObject.Save(filePath);

            drawingGraphics.Dispose();
            screenshotObject.Dispose();

        }

    }
}
