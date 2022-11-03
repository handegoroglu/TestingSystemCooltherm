using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.IO;

namespace _2021__Testing_System
{
    public partial class Form1 : Form
    {
        //Port Numaralarını ports isimli diziye atıyoruz.
        string[] ports = SerialPort.GetPortNames();

        public Form1()
        {
            InitializeComponent();
            serialPort2.DataReceived += serialPort2_DataReceived;
            serialPort1.ReadTimeout = 2000;//veri okuma timeout'u 100ms
            serialPort2.ReadTimeout = 2000;//veri okuma timeout'u 100ms
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            foreach (string port in ports)
            {
                comboBox1.Items.Add(port); // Port isimlerini combobox1'de gösteriyoruz.
                comboBox1.SelectedIndex = 0;
                comboBox3.Items.Add(port); // Port isimlerini combobox1'de gösteriyoruz.
                comboBox3.SelectedIndex = 0;
            }
            

            // Baudrate'leri kendimiz combobox2'ye giriyoruz.
            comboBox2.Items.Add("9600");
            comboBox2.SelectedIndex = 0;
            // Baudrate'leri kendimiz combobox4'ye giriyoruz.
            comboBox4.Items.Add("9600");
            comboBox4.SelectedIndex = 0;


            label3.Text = "Bağlantı Kapalı";   //Bu esnada bağlantı yok.



        }
        private void button1_Click(object sender, EventArgs e)
        {


            if (serialPort1.IsOpen == false)
            {
                serialPort1.PortName = comboBox1.Text;  // combobox1'e zaten port isimlerini aktarmıştık.
                serialPort1.BaudRate = Convert.ToInt16(comboBox2.Text); //Seri Haberleşme baudrate'i combobox2 'de seçilene göre belirliyoruz.

                serialPort2.PortName = comboBox3.Text;  // combobox1'e zaten port isimlerini aktarmıştık.
                serialPort2.BaudRate = Convert.ToInt16(comboBox4.Text); //Seri Haberleşme baudrate'i combobox2 'de seçilene göre belirliyoruz.
                try
                {
                    serialPort1.Open(); //Haberleşme için port açılıyor
                    serialPort2.Open();
                    label3.ForeColor = Color.Green;
                    label3.Text = "Bağlantı Açık";
                    button1.Text = "bağlantı kes";
                    if (serialPort1.IsOpen && serialPort2.IsOpen)
                    {
                        comboBox1.Enabled = false;
                        comboBox2.Enabled = false;
                        comboBox3.Enabled = false;
                        comboBox4.Enabled = false;
                    }


                }
                catch (Exception hata)
                {
                    MessageBox.Show("Hata:" + hata.Message);
                }
            }
            else
            {
                label3.Text = "Bağlantı kesildi!";
                label3.ForeColor = Color.Red;
                serialPort1.Close();
                serialPort2.Close();
                if (serialPort1.IsOpen == false && serialPort2.IsOpen == false)
                {
                    comboBox1.Enabled = true;
                    comboBox2.Enabled = true;
                    comboBox3.Enabled = true;
                    comboBox4.Enabled = true;
                }

                button1.Text = "bağlantı aç";
            }

        }

        private void button2_Click(object sender, EventArgs e)
        { 
            if (serialPort1.IsOpen)
            {
                string okeymessage = "Veri gönderildi / alındı.";
                serialPort1.Write(okeymessage);

                
            }
            else
            {
                richTextBox1.Text = "Başarısız";
            }

        }
        public delegate void veriGoster(string s);
        public void textBoxYaz(string s)
        {
            richTextBox1.Text = s;
        }
        private void serialPort2_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            //Her data geldiğinde program buraya geliyor
            string gelenveri = serialPort2.ReadExisting();
            richTextBox1.Invoke(new veriGoster(textBoxYaz), gelenveri);
            

        }
    }
}
