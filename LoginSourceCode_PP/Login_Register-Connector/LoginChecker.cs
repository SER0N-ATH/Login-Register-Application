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

namespace LoginSourceCode_PP
{
    public partial class LoginChecker : Form
    {
        public LoginChecker()
        {

            //Sets the background image of various object to transparent,
            //and disables IlleagalCrossThread Calls, and return button

            InitializeComponent();
            label1.BackColor = Color.Transparent;
            pictureBox1.BackColor = Color.Transparent;
            pictureBox2.BackColor = Color.Transparent;
            this.Shown += new System.EventHandler(this.LoginChecker_Shown);
            CheckForIllegalCrossThreadCalls = false; 
            button1.Enabled = false;
        }

        // Function that returns a randomly generated GUID without the "-".  Contains 32 
        //random digits which is used for hashes and other encripitons
        private static string GuidGeneration()
        {
            string RandomGuid = System.Guid.NewGuid().ToString();
            string CompletedGUID = RandomGuid.Replace("-", "");
            return CompletedGUID;
        }

        //This function Generates a 256 Hash From a given string.
        private static string SHA256Hash(string pswrd) //Input Password Entered by user.
        {
            var hidden = new System.Security.Cryptography.SHA256Managed();
            var hash = new System.Text.StringBuilder();
            byte[] crypto = hidden.ComputeHash(Encoding.UTF8.GetBytes(pswrd));
            foreach (byte theByte in crypto)
            {
                hash.Append(theByte.ToString("x2"));
            }
            return hash.ToString();
        }

        //This function Creates a "Salted" password from a given hashed password, and a salt obtained by the servers"
        //It is used to prevent dictionary and rainbow attacks.

        private static string PswrdHashedGeneration(string pwd, string salt)
        {
            var provider = MD5.Create();
            byte[] bytes = provider.ComputeHash(Encoding.ASCII.GetBytes(salt + pwd));
            string computedHash = BitConverter.ToString(bytes);
            string ConverteredHash = computedHash.Replace("-", "");
            return ConverteredHash;
        }



        // We check if the user has missed any of the inputs. If so we tell them to enter the missing input. 
        // We then have our variables where we store our Salt values, connection/query strings/usernames.
        // The first using statement extracts the salt value for the particular user account.
        // The second using statement hashes and salts the password given by the user, and compares it with the 
        // Hashed value in the MYSQL DATABASE

        // Make sure before you complie this program, that you setup a custom Connection 

        public int TokenSucess = 0;

        string ConnectionString = "SERVER=remotemysql.com;" +
                                  "PORT=3306;" +
                                  "DATABASE=;" +
                                  "UID=;" +
                                  "PASSWORD=;" +
                                  "SslMode=none;";


        // This login function will redirect the user to the Success Page [Form3] if the valid credentials are given.

        public int TextSanitizerLogin()
        {

            string username = WindowsFormsApp1.Form1.username;
            string password = WindowsFormsApp1.Form1.password;

                string SaltValue = "";
               
                string ConnectionQuery = "Select * from Login where UserName=@UserID and PassHash=@Paswrd";
                string ObtainSaltQuery = "Select * from Login where UserName=@UserID";
               

            //Makes everthing within disposiable, and allows us to catch exceptions or errors that may arrise
            //During the execution of this program. 
            try
            {
                label1.Text = "Connecting to Server";
                using (MySqlConnection ServCon = new MySqlConnection(ConnectionString))
                {
                    ServCon.Open();
                    
                    using (MySqlCommand SaltRetriever = new MySqlCommand(ObtainSaltQuery, ServCon))
                    {
                        SaltRetriever.Parameters.AddWithValue("@UserID", username);
                        MySqlDataAdapter adapt = new MySqlDataAdapter(SaltRetriever);
                        MySqlDataReader DataView = SaltRetriever.ExecuteReader();
                        if (DataView.Read())
                        {
                            SaltValue = DataView["SALT"].ToString();
                        }
                    }
                    label1.Text = "Authenicating";
                    string pswrdhash = PswrdHashedGeneration(SHA256Hash(password), SaltValue);
                    using (MySqlCommand Login = new MySqlCommand(ConnectionQuery, ServCon))
                    {
                        Login.Parameters.AddWithValue("@UserID", username);
                        Login.Parameters.AddWithValue("@Paswrd", pswrdhash);
                        MySqlDataAdapter adapt = new MySqlDataAdapter(Login);
                        DataSet ds = new DataSet();
                        adapt.Fill(ds);
                        TokenSucess = 0;
                        TokenSucess = ds.Tables[0].Rows.Count;
                    }
                    ServCon.Close();
                    
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                label1.Text = "Connectiion to server failed";
            }

            // This is the on decision whether or not the user shall be given access to the program.

            finally
            {
                if (TokenSucess == 1)
                {
                    WindowsFormsApp1.Form1.successtoken = true;
                    MessageBox.Show("Login Sucess");
                    label1.Text = "Login Success";
                    this.Hide(); 
                
                }
                else
                {
                    WindowsFormsApp1.Form1.successtoken = false;
                    MessageBox.Show("Failure");
                    label1.Text = "Login Failure";
                    button1.Enabled = true;
                }

            }
                //A simple if statement is used to check if the TokenSucess is one, which means if the Query detected that 
                //The hash value matches the database values.

  
                return 0;
            }



        // This is a function which registers the user to a MySQL database. The following data must be given in order
        // For the login to be successful the user must enter the following details. Username/Password/Name/Email. 
        // This is used to identifiy and differientate from other users.
        
        // The following will be sent to the database. --> username/password hash/salt/name/emailAddress
        
        private int UserRegerstration()
        {
           string Username = WindowsFormsApp1.Form2.userid;
           string incompletePassword = WindowsFormsApp1.Form2.password;
           string Email = WindowsFormsApp1.Form2.email;
           string Name = WindowsFormsApp1.Form2.name;
           string SaltGeneration = GuidGeneration();
           string Password =PswrdHashedGeneration(SHA256Hash(incompletePassword), SaltGeneration);
           int Success = 0;
           string RegQuery = "INSERT INTO Login (UserName, PassHash,SALT,Name,Email) VALUES (@Username, @Password,@SaltGeneration,@Name,@Email)";
           string ObtainSaltQuery = "SELECT COUNT(*) FROM Login WHERE UserName=@UserID";
      
            try
            {
                label1.Text = "Connecting to Server ";
                using (MySqlConnection ServCon = new MySqlConnection(ConnectionString))
                {
                    ServCon.Open();
                    using (MySqlCommand Login = new MySqlCommand(RegQuery, ServCon))
                    {
                        MessageBox.Show("Connection has begun!");
                        Login.Parameters.AddWithValue("@Username", Username);
                        Login.Parameters.AddWithValue("@Password", Password);
                        Login.Parameters.AddWithValue("@SaltGeneration", SaltGeneration);
                        Login.Parameters.AddWithValue("@Name", Name);
                        Login.Parameters.AddWithValue("@Email", Email);
                        label1.Text = "Registering";
                        Success = Login.ExecuteNonQuery();
                    }
                    ServCon.Close();
                }
            }
            catch
            {
                MessageBox.Show("An error has occured.");
                label1.Text = "Connectiion to server failed";
            }
            finally
            {
                if (Success == 1)
                {
                    MessageBox.Show("Registrations has been succesfull");
                    label1.Text = "You have been registered";
                    button1.Enabled = true;
                }
                else
                {
                    MessageBox.Show("Registrations failed. ");
                    button1.Enabled = true;
                }
            }
            return 0;

        }
  

        private void label1_Click(object sender, EventArgs e)
        {
    
        }



        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void LoginChecker_Load(object sender, EventArgs e)
        {

        }


        private void LoginChecker_Shown(object sender, EventArgs e)
        {
            if (WindowsFormsApp1.Form1.loginMode==true)
            {
                Task<int> Execution = new Task<int>(TextSanitizerLogin);
                Execution.Start();
                WindowsFormsApp1.Form1.loginMode = false;
            }
            else if (WindowsFormsApp1.Form2.registerMode==true)
            {
                MessageBox.Show("ReGMode");
                Task<int> Execution = new Task<int>(UserRegerstration);
                Execution.Start();
                WindowsFormsApp1.Form2.registerMode = false;
            }
          

        }
    }
}
