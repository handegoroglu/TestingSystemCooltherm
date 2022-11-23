using _2021__Testing_System.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _2021__Testing_System
{
    public partial class ServerAyarlari : Form
    {
        public ServerAyarlari()
        {
            InitializeComponent();
            label3.Text = " ";
        }
        /*
         sqlSorgu.Database.cnxn = pyodbc.connect("Driver={SQL Server Native Client 11.0};"
                                                        f"Server={generalJsonFile.settings['Server']};"
                                                        "Database=Recowa;"
                                                        "Trusted_Connection=yes;"
                                                        f"UID={generalJsonFile.settings['userName']};"
                                                        f"PWD={generalJsonFile.settings['password']};")
         */
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                
                Program.sqlConnection.Close();
                string connectionString = "Server=" + txtServerName.Text + "; Database=" + txtDatabaseName.Text + "; Trusted_Connection=" + (checkBox1.Checked == true ? "no" : "yes") + "; UID=" + txtUserName.Text + ";PWD=" + txtPassword.Text + ";";
                Program.sqlConnection = new SqlConnection(connectionString);
                //Program.sqlConnection = new SqlConnection("Data Source=" + txtServerName.Text + ";Initial Catalog=" + txtDatabaseName.Text + "User ID = " + txtUserName.Text + "Password =" + txtPassword.Text + ";");
                Program.sqlConnection.Open();
                label3.Text = "Bağlandı!";
                label3.ForeColor = Color.Green;

                Settings.Default.localhost = checkBox1.Checked;
                Settings.Default.ServerName= txtServerName.Text;
                Settings.Default.UserName= txtUserName.Text;
                Settings.Default.DatabasePassword= txtPassword.Text;
                Settings.Default.DatabaseName = txtDatabaseName.Text;
                Settings.Default.Save();


                Task t = Task.Run(() =>
                {
                    Thread.Sleep(1000);
                    this.Invoke((MethodInvoker)delegate ()
                    {
                        this.Close();
                    });
                });
            }
            catch (Exception)
            {
                label3.Text = "Bağlanamadı!";
                label3.ForeColor = Color.Red;
            }


        }

        private void ServerAyarlari_Load(object sender, EventArgs e)
        {
            checkBox1.Checked = Settings.Default.localhost;
            txtServerName.Text = Settings.Default.ServerName;
            txtUserName.Text = Settings.Default.UserName;
            txtPassword.Text = Settings.Default.DatabasePassword;
            txtDatabaseName.Text = Settings.Default.DatabaseName;
        }
    }
}
