using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace cryptoFatality
{
    public partial class Form1 : Form
    {
        private string inputData;
        public Form1()
        {
            InitializeComponent();
            foreach (string method in Enum.GetNames(typeof(CryptoMethods)))
            {
                comboBox1.Items.Add(method);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                var fileStream = openFileDialog.OpenFile();

                StreamReader reader = new StreamReader(fileStream);
                
                this.inputData = reader.ReadToEnd();
                
            }
            button2.Enabled = true;
            button3.Enabled = true;
        }

        public void save_file(string filePath, string data)
        {
            File.WriteAllText(filePath, data);
        }

        public string caesarEncode()
        {
            StringBuilder sb = new StringBuilder();
            foreach (char ch in this.inputData)
            {
                if (!char.IsLetter(ch)) {
                    sb.Append(ch);
                    continue;
                } 
                char offset;
                offset = char.IsUpper(ch) ? 'A' : 'a';
                sb.Append((char)((((ch + Int32.Parse(textBox1.Text)) - offset) % 26) + offset));
            }
            return sb.ToString();
        }

        public string caesarDecode()
        {
            StringBuilder sb = new StringBuilder();
            foreach (char ch in this.inputData)
            {
                if (!char.IsLetter(ch))
                {
                    sb.Append(ch);
                    continue;
                }
                char offset;
                offset = char.IsUpper(ch) ? 'A' : 'a';
                sb.Append((char)((((ch - Int32.Parse(textBox1.Text)) - offset) % 26) + offset));
            }
            return sb.ToString();
        }

        public string affineEncode()
        {
            char y, offset;
            StringBuilder sb = new StringBuilder();
            foreach (char ch in this.inputData)
            {
                if (!char.IsLetter(ch))
                {
                    sb.Append(ch);
                    continue;
                }
                offset = char.IsUpper(ch) ? 'A' : 'a';
                y = (char)(ch - offset);
                y = (char)((Int32.Parse(textBox2.Text) * y + Int32.Parse(textBox3.Text)) % 26);
                sb.Append((char)( y + offset));
            }
            return sb.ToString();
        }
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            button1.Enabled = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 0)
                this.save_file("C:\\Users\\user\\Desktop\\CaesarEncrypt.txt", this.caesarEncode());
            if (comboBox1.SelectedIndex == 1)
                this.save_file("C:\\Users\\user\\Desktop\\AffineEncrypt.txt", this.affineEncode());
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 0 || comboBox1.SelectedIndex == 2) { 
                label1.Visible = true;
                textBox1.Visible = true;
                label4.Visible = false;
                textBox2.Visible = false;
                label3.Visible = false;
                textBox3.Visible = false;
            }

            if (comboBox1.SelectedIndex == 1)
            {
                label4.Visible = true;
                textBox2.Visible = true;
                label3.Visible = true;
                textBox3.Visible = true;
                label1.Visible = false;
                textBox1.Visible = false;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 0)
                this.save_file("C:\\Users\\user\\Desktop\\Decrypt.txt", this.caesarDecode());
            if (comboBox1.SelectedIndex == 1)
                this.save_file("C:\\Users\\user\\Desktop\\AffineDecrypt.txt", this.affineEncode());

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if(textBox2.TextLength > 0 || textBox3.TextLength > 0)
            {
                button1.Enabled = true;
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            if (textBox2.TextLength > 0 || textBox3.TextLength > 0)
            {
                button1.Enabled = true;
            }
        }
    }
}
