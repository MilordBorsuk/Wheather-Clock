using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.IO;
using System.Net;
using System.Xml.Linq;

namespace WheatheroClock
{
    public partial class Form1 : Form
    {
        Timer t = new Timer();
        public Form1()
        {
            InitializeComponent();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            string City;
            City = textBoxCity.Text;
            string uri = string.Format("http://api.apixu.com/v1/current.xml?key=8e79d8c92b3f49b8a9d64504192705&q=", City);
            XDocument doc = XDocument.Load(uri);

            string iconUrl = (string)doc.Descendants("icon").FirstOrDefault();
            WebClient client = new WebClient();

            byte[] image = client.DownloadData("http:" + iconUrl );
            MemoryStream stream = new MemoryStream(image);

            Bitmap newBitmap = new Bitmap(stream);
            string makstemp = (string)doc.Descendants("maxtemp_c").FirstOrDefault();
            string mintemp = (string)doc.Descendants("mintemp_c").FirstOrDefault();
           
            string wind = (string)doc.Descendants("maxwind_mph").FirstOrDefault();
            
            string humidity = (string)doc.Descendants("avghumidity").FirstOrDefault();
            Bitmap Icon = newBitmap;
            textBoxHumidity.Text = humidity;
            textBoxMaxTemp.Text = makstemp;
            textBoxMinTemp.Text = mintemp;
            textBoxWindspeed.Text = wind;
           // kraj = "Poland";
            pictureBox1.Image = Icon;
            


        }

        private void buttonEnd_Click(object sender, EventArgs e)
        {
            this.Close();

        }

        private void label5_Click(object sender, EventArgs e)
        {
            

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //label5.Text = DateTime.Now.ToLongTimeString();
            t.Interval = 1000;
            t.Tick += new EventHandler(this.t_Tick);
            label8.Text = DateTime.Now.ToLongDateString();
            t.Start();
        }
        private void t_Tick(object sender, EventArgs e)
        {
            int gg = DateTime.Now.Hour;
            int mm = DateTime.Now.Minute;
            int ss = DateTime.Now.Second;
            string time = "";

            if (gg < 10)
            {
                time += "0" + gg;
            }
            else
            {
                time += gg;
            }
            time += ":";
            if(mm<10)
            {
                time += "0" + mm;
            } else
            {
                time += mm;

            }
            time += ":";
            if(ss < 10)
            {
                time += "0" + ss;
            }
            else
            {
                time += ss;
            }
            time += ":";
            label5.Text = time;
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }
    }
}
