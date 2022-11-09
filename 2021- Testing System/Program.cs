using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO.Ports;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _2021__Testing_System
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());


        }
        //public static  SqlConnection sqlConnection = new SqlConnection("Data Source=LAPTOP-NUR8T6G0;Initial Catalog=SqlTestingSystem;Integrated Security=True");
        public static SqlConnection sqlConnection = new SqlConnection("Data Source=LAPTOP-NUR8T6G0;Initial Catalog=SqlTestingSystem;Integrated Security=True");


    }
}
