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
using System.Threading;
using System.Net.Mail;

namespace WindowsFormsApp1
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
            pictureBox1.BackColor = Color.Transparent;

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        public static bool registerMode = false;
        public static bool ValidResponse = true;
        public static bool ReturnToHomeScreen = false;
        public static string name;
        public static string password;
        public static string email;
        public static string userid;
        

        bool UniqueName; 

        private void UserNameCheck()
        {
            string ConnectionString = "SERVER=remotemysql.com;" +
                                "PORT=3306;" +
                                "DATABASE=;" +
                                "UID=;" +
                                "PASSWORD=;" +
                                "SslMode=none;";

            try
            {
                string ObtainNameQuery = "SELECT count(*) FROM Login WHERE UserName = @username";
                using (MySqlConnection ServCon = new MySqlConnection(ConnectionString))
                {
                    ServCon.Open();

                    using (MySqlCommand UsernameExist = new MySqlCommand(ObtainNameQuery, ServCon))
                    {
                        UsernameExist.Parameters.AddWithValue("@username", UIDBox.Text);
                        int UserExist = int.Parse(UsernameExist.ExecuteScalar().ToString());
                        if (UserExist > 0)
                        {
                            UniqueName = false; //Username
                        }
                        else
                        {
                            UniqueName = true;
                        }
                    }
                }
            }
            finally
            {
                
            }

        }

        private void InfoValidator()
        {
            ValidResponse = true;
            if (string.IsNullOrEmpty(UIDBox.Text) || string.IsNullOrEmpty(UIDBox.Text) || string.IsNullOrEmpty(PassBox.Text) || string.IsNullOrEmpty(EmailBox.Text))
            {
                MessageBox.Show("Please enter all fields");
                ValidResponse = false;
            }
            else
            {
                try
                {
                    MailAddress m = new MailAddress(EmailBox.Text);
                }
                catch (FormatException)
                {
                    MessageBox.Show("Please enter a valid email");
                    ValidResponse = false;
                }
            }


            UserNameCheck();
            if (UniqueName == false)
            {
                MessageBox.Show("Username already exist");
                label6.Text = "Username already exists.";
            }
            if (ValidResponse && UniqueName)
            {
                name = UIDBox.Text;
                password = PassBox.Text;
                email = EmailBox.Text;
                userid = UIDBox.Text;


                Hide();
                using (TermsAndConditionscs form2 = new TermsAndConditionscs())
                {
                    form2.ShowDialog();
                }
                if (registerMode)
                {
                    ReturnToHomeScreen = true;
                    using (LoginChecker formx = new LoginChecker())
                    {
                        formx.ShowDialog();

                    }
                }
                else
                {
                    MessageBox.Show("You must accept to the terms and conduct, before you register");
                }
                Show();
            }

        }
     

        private void NameBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void UIDBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void PassBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void EmailBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void LoginButton_Click_1(object sender, EventArgs e)
        {
            InfoValidator();
            if (ReturnToHomeScreen == true) {
                this.Close();
            }


        }

        private void EmailBox_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void UIDBox_TextChanged_1(object sender, EventArgs e)
        {
            
        }




        // Animation For Focus/LostFocus


        private void UIDBox_GotFocus(object sender, EventArgs e)
        {
            if (UIDBox.Text == "Username")
            { UIDBox.Clear(); }
        
        }

        private void NameBox_GotFocus(object sender, EventArgs e)
        {
            if (NameBox.Text == "Name")
            { NameBox.Clear(); }
        }

        private void PassBox_GotFocus(object sender, EventArgs e)
        {
            if (PassBox.Text == "Password")
            { PassBox.Clear(); }
        }

        private void PassBox1_GotFocus(object sender, EventArgs e)
        {
            if (PassBox1.Text == "Password")
            { PassBox1.Clear(); }
        }


        private void UIDBox_LostFocus(object sender, EventArgs e)
        {
            if (UIDBox.Text == "")
            {
                UIDBox.Text = "Username";
            }
        }

        private void NameBox_LostFocus(object sender, EventArgs e)
        {
            if (NameBox.Text == "")
            {
                NameBox.Text = "Name";
            }
        }

        private void PassBox_LostFocus(object sender, EventArgs e)
        {
            if (PassBox.Text == "")
            {
                PassBox.Text = "Password";
            }
        }

        private void PassBox1_LostFocus(object sender, EventArgs e)
        {
            if (PassBox1.Text == "")
            {
                PassBox1.Text = "Password";
            }
        }

        private void EmailBox_LostFocus(object sender, EventArgs e)
        {
            if (EmailBox.Text == "")
            {
                EmailBox.Text = "Email";
            }
        }

        private void EmailBox_GotFocus(object sender, EventArgs e)
        {
            if (EmailBox.Text == "Email")
            { EmailBox.Clear(); }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
