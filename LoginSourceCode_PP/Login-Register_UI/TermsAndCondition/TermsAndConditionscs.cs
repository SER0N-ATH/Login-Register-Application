using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LoginSourceCode_PP
{
    public partial class TermsAndConditionscs : Form
    {
        public TermsAndConditionscs()
        {
            InitializeComponent();
            label1.BackColor = Color.Transparent;
            richTextBox1.Text = "Lorem ipsum dolor sit amet, at magna inani consulatu quo. Qui ut erat habeo." +
                " Tritani dolorum eu qui, integre eligendi dissentiet at mel, vim cu utamur dissentiet. Ne reque erroribus necessitatibus ius," +
                " nominavi pertinax gubergren vis ad. No elit intellegebat consectetuer usu.";
                
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void LoginButton_Click(object sender, EventArgs e)
        {
            //accept
            WindowsFormsApp1.Form2.registerMode = true;
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //decline 
            this.Close();
        }
    }
}
