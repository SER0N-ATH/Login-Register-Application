using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Cryptography;
using MySql.Data.MySqlClient;
using LoginSourceCode_PP;

// Login System ASP.NET/C#/ or win32 applications.
// Put together By Seron.Athavan(www.ProjectProgrammer.ca)

// Thanks to Nicoleta Ciauşu
//https://medium.com/@mehanix/lets-talk-security-salted-password-hashing-in-c-5460be5c3aae

// Thanks to christosmatskas 
//https://cmatskas.com/-net-password-hashing-using-pbkdf2/

// If you find any errors please message me


namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            pictureBox1.BackColor = Color.Transparent;
           
        }

        public static string username;
        public static string password;

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }




       public static bool successtoken = false;
       public static bool loginMode = false; 

        private void LoginButton_Click(object sender, EventArgs e)
        {
            username = textBox1.Text;
            password = textBox2.Text;
            loginMode = true;
            Hide();
         
            using (LoginChecker form2 = new LoginChecker())
            {
                form2.ShowDialog();
            }
            loginMode = false;

            if (successtoken)
            {
                using (Form3 form2 = new Form3())
                {
                    form2.ShowDialog();
                }
                    Show();
            }
            else
            {
                Show();
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
          
        }


        private void button1_Click(object sender, EventArgs e)
        {
            using (Form2 formx = new Form2())
            {
                this.Hide();
                formx.ShowDialog();
            }
            this.Show();
        }

        private void textBox2_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void textBox1_GotFocus(object sender, EventArgs e)
        {
            if (textBox1.Text == "Username")
            { textBox1.Clear(); }
        }

        private void textBox1_LostFocus(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                textBox1.Text = "Username";
            }
        }

        private void textBox2_GotFocus(object sender, EventArgs e)
        {
            if (textBox2.Text == "Password")
            { textBox2.Clear(); }

        }

        private void textBox2_LostFocus(object sender, EventArgs e)
        {
            if (textBox2.Text == "")
            {
                textBox2.Text = "Password";
            }
        }

    }
}
