using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.IO;
using System.IO.Ports;
using System.Data.SqlClient;

namespace _2021__Testing_System
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
            serialPort1.ReadTimeout = 100;//veri okuma timeout'u 100ms
            serialPort2.ReadTimeout = 100;//veri okuma timeout'u 100ms
        }
        SqlConnection baglan = new SqlConnection("Data Source=LAPTOP-NUR8T6G0;Initial Catalog=SqlTestingSystem;Integrated Security=True");
        private void VerileriGöster()
        {
            baglan.Open();
            SqlCommand komut = new SqlCommand("Select *From TestingSystems", baglan);
            SqlDataReader oku = komut.ExecuteReader();
            while (oku.Read())
            {
                ListViewItem ekle = new ListViewItem();
                ekle.Text = oku["DeviceID"].ToString();
                ekle.SubItems.Add(oku["TestSonuc"].ToString());
                listView1.Items.Add(ekle);
            }
            baglan.Close();
        }

        private void VerileriKaydet()
        {
            bool okeyTest = true;
            baglan.Open();
            SqlCommand komut = new SqlCommand("Insert Into TestingSystems (DeviceID,TestSonuc)" + " values ('" + textBox1.Text + "','" + okeyTest.ToString() + "')", baglan);
            komut.ExecuteNonQuery();
            baglan.Close();

        }


        private void Form1_Load(object sender, EventArgs e)
        {


            // Baudrate'leri kendimiz combobox2'ye giriyoruz.
            comboBox2.Items.Add("9600");
            comboBox2.SelectedIndex = 0;
            // Baudrate'leri kendimiz combobox4'ye giriyoruz.
            comboBox4.Items.Add("9600");
            comboBox4.SelectedIndex = 0;


            label3.Text = "Bağlantı Kapalı";   //Bu esnada bağlantı yok.
            label3.ForeColor = Color.Red;
            button2.Enabled = false;

            bool okeyTest = false;

        }



        bool portOpen(bool state)

        {
            if (state)
            {

                serialPort1.PortName = comboBox1.Text;  // combobox1'e zaten port isimlerini aktarmıştık.
                serialPort1.BaudRate = Convert.ToInt16(comboBox2.Text); //Seri Haberleşme baudrate'i combobox2 'de seçilene göre belirliyoruz.

                serialPort2.PortName = comboBox3.Text;  // combobox1'e zaten port isimlerini aktarmıştık.
                serialPort2.BaudRate = Convert.ToInt16(comboBox4.Text); //Seri Haberleşme baudrate'i combobox2 'de seçilene göre belirliyoruz.


                try
                {
                    serialPort1.Open(); //Haberleşme için port açılıyor

                }
                catch (Exception)
                {
                }

                try
                {
                    serialPort2.Open(); //Haberleşme için port açılıyor
                }
                catch (Exception)
                {
                }

                if (serialPort1.IsOpen && serialPort2.IsOpen)
                {
                    label3.ForeColor = Color.GreenYellow;
                    label3.Text = "Bağlantı Açık";
                }
                else
                {
                    return false;
                }

            }
            else
            {

                serialPort1.Close();
                serialPort2.Close();
            }

            return true;

        }

        private void button2_Click(object sender, EventArgs e)
        {

            if (portOpen(true))
            {

                string okeymessage = "HANDE&FATIH\n";
                string receiveData = "";

                serialPort1.Write(okeymessage);
                try
                {
                    receiveData = serialPort2.ReadLine();
                }
                catch (Exception)
                {
                }

                if (receiveData.Equals(okeymessage.Replace("\n", "")) == true)
                {

                    receiveData = "";
                    serialPort2.Write(okeymessage);
                    try
                    {
                        receiveData = serialPort1.ReadLine();
                    }
                    catch (Exception)
                    {
                    }

                    if (receiveData.Equals(okeymessage.Replace("\n", "")) == true)
                    {
                        label4.ForeColor = Color.GreenYellow;
                        label4.Text = ("Test Başarılı!");
                        VerileriKaydet();

                    }
                    else
                    {
                        label4.ForeColor = Color.Red;
                        label4.Text = ($"{serialPort2.PortName}'den {serialPort1.PortName}'e data gönderilemedi.");
                    }

                }
                else
                {
                    label4.ForeColor = Color.Red;
                    label4.Text = ($"{serialPort1.PortName}'den {serialPort2.PortName}'e data gönderilemedi.");
                }

            }
            else
            {
                if (!serialPort2.IsOpen && !serialPort1.IsOpen)
                {
                    label4.ForeColor = Color.Red;
                    label4.Text = ($"{serialPort1.PortName} ve {serialPort2.PortName} açılamadı");
                }
                else if (!serialPort1.IsOpen)
                {
                    label4.ForeColor = Color.Red;
                    label4.Text = ($"{serialPort1.PortName} açılamadı");
                }
                else if (!serialPort2.IsOpen)
                {
                    label4.ForeColor = Color.Red;
                    label4.Text = ($"{serialPort2.PortName} açılamadı");
                    // MessageBox.Show($"{serialPort2.PortName} açılamadı");
                }

            }

            portOpen(false);

        }

        private void comboBox3_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem != " " && comboBox2.SelectedItem != " ")
            {
                button2.Enabled = true;
            }
            comboBox3.Items.Clear();

            //Port Numaralarını ports isimli diziye atıyoruz.
            foreach (string port in SerialPort.GetPortNames())
            {
                comboBox3.Items.Add(port); // Port isimlerini combobox1'de gösteriyoruz.
                comboBox3.SelectedIndex = 0;
            }
        }

        private void comboBox1_Click(object sender, EventArgs e)
        {

            comboBox1.Items.Clear();

            //Port Numaralarını ports isimli diziye atıyoruz.
            foreach (string port in SerialPort.GetPortNames())
            {
                comboBox1.Items.Add(port); // Port isimlerini combobox1'de gösteriyoruz.
                comboBox1.SelectedIndex = 0;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            VerileriGöster();
        }
    }
}
