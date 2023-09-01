using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Oracle.ManagedDataAccess.Client;

namespace MiniUber
{
    public partial class AcceptRide : Form
    {
        string ordb = "Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=localhost)(PORT=1521)))(CONNECT_DATA=(SERVICE_NAME=orcl)));User Id=scott;Password=tiger;";
        OracleConnection conn;
        int Id;
        string Username;
        Ride ride;
        string Password;
        OracleCommand c = new OracleCommand();
        public AcceptRide(string username, string password, int id)
        {
            InitializeComponent();
            Username = username;
            Password = password;
            Id = id;
        }


        private void AcceptRide_Load(object sender, EventArgs e)
        {
            conn = new OracleConnection(ordb);
            conn.Open();
            c = new OracleCommand();
            c.Connection = conn;
            c.CommandText = "select trip_request from drivers where username=:username";
            c.CommandType = CommandType.Text;
            c.Parameters.Add(new OracleParameter(":username", Username));
            OracleDataReader dataReader = c.ExecuteReader();
            if (dataReader.Read())
            {
                string trip_req = dataReader["trip_request"].ToString();
                textBox1.Text = trip_req;
            }
            dataReader.Close();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            c.CommandText = "update drivers set trip_request = 'no trip request',status = 'unavailable' where username=:username";
            c.CommandType = CommandType.Text;
            c.ExecuteNonQuery();
            ride = new Ride("yes");
            ride.getDriverInfo(Id);

        }

        private void button2_Click(object sender, EventArgs e)
        {
            c.CommandText = "update drivers set trip_request = 'no trip request' where username=:username";
            c.CommandType = CommandType.Text;
            c.ExecuteNonQuery();
            ride = new Ride("no");
        }
        
    }
}
