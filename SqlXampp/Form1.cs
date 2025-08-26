using MySql.Data.MySqlClient;
using Org.BouncyCastle.Pqc.Crypto.Lms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SqlXampp
{
    public partial class Form1 : Form
    {

        MySqlConnection mySqlConnection = new MySqlConnection("server=127.0.0.1;user=root;database=sample; password=");
        
        public Form1()
        {
            InitializeComponent();
            this.KeyPreview = true;
            this.KeyDown += new KeyEventHandler(Form1_KeyDown);
        }
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                InsertStudent();
                e.Handled = true;
                e.SuppressKeyPress = true; // prevent beep sound
            }
        }

        private void InsertStudent()
        {
            try
            {
                string query = "INSERT INTO students " +
                               "(student_no, last_name, first_name, middle_name, gender, birthday, birthplace, course_year, department) " +
                               "VALUES (@student_no, @last_name, @first_name, @middle_name, @gender, @birthday, @birthplace, @course_year, @department)";

                MySqlCommand cmd = new MySqlCommand(query, mySqlConnection);

                cmd.Parameters.AddWithValue("@student_no", txtStudentNo.Text);
                cmd.Parameters.AddWithValue("@last_name", txtLastName.Text);
                cmd.Parameters.AddWithValue("@first_name", txtFirstName.Text);
                cmd.Parameters.AddWithValue("@middle_name", txtMiddleName.Text);
                cmd.Parameters.AddWithValue("@gender", txtMiddleName.Text);  // ComboBox for Gender
                cmd.Parameters.AddWithValue("@birthday", dtpBirthday.Value); // DateTimePicker
                cmd.Parameters.AddWithValue("@birthplace", txtBirthPlace.Text);
                cmd.Parameters.AddWithValue("@course_year", txtCourseYear.Text);
                cmd.Parameters.AddWithValue("@department", cmbDepartment.Text); // ComboBox for Department

                mySqlConnection.Open();
                cmd.ExecuteNonQuery();
                mySqlConnection.Close();

                MessageBox.Show("Student info added successfully!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
                if (mySqlConnection.State == System.Data.ConnectionState.Open)
                    mySqlConnection.Close();
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
