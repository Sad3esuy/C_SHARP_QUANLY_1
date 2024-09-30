using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BAI2_CAU1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                double number1 = double.Parse(txtNumber1.Text);
                double number2 = double.Parse(txtNumber2.Text);
                double result = number1 + number2;
                txtAnswer.Text = result.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnSub_Click(object sender, EventArgs e)
        {
            try
            {
                double number1 = double.Parse(txtNumber1.Text);
                double number2 = double.Parse(txtNumber2.Text);
                double result = number1 - number2;
                txtAnswer.Text = result.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnMul_Click(object sender, EventArgs e)
        {
            try
            {
                double number1 = double.Parse(txtNumber1.Text);
                double number2 = double.Parse(txtNumber2.Text);
                double result = number1 * number2;
                txtAnswer.Text = result.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnDiv_Click(object sender, EventArgs e)
        {
            try
            {
                double number1 = double.Parse(txtNumber1.Text);
                double number2 = double.Parse(txtNumber2.Text);
                double result = number1 / number2;
                txtAnswer.Text = result.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
