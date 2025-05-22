using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Security.Cryptography;
using System.IO;
using System.Security.Cryptography;

namespace Encryption
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Encry.Enc();
        }

        private void encryptButton_Click(object sender, EventArgs e)
        {
            Encry.text = richTextBox1.Text;
            Encry.form1 = this;
            Encry.Enc();
        }

        private void decryptButton_Click(object sender, EventArgs e)
        {
            Encry.form1 = this;
            Encry.Dec(Encoding.Default.GetBytes(textBox1.Text));
        }
    }
}
