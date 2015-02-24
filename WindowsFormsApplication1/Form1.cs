using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Calculatorr : Form
    {
        Boolean flag1 = false;
        public Calculatorr()
        {
            InitializeComponent();
        }

        private void Calculatorr_Load(object sender, EventArgs e)
        {

        }

        private void button_click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            //display from lblResult to lblPreviousCalc after an operation 
            //when another button is pressed, and after erase if needed
            if (flag1 == true)
            {
                if(lblPreviousCalc.Text!="")
                { lblPreviousCalc.Text = ""; }
                lblPreviousCalc.Text = lblResult.Text;
                lblResult.Text = "0";
                flag1 = false;
            }
            //Notation "0.xx" for numbers below 1, instead of ".xx"
            if (lblResult.Text == "0")
                if (button.Text!=".")
                    lblResult.Text="";
            lblResult.Text = lblResult.Text + button.Text;
        }

        private void cmdClear_Click(object sender, EventArgs e)
        {
            lblResult.Text = "0";
        }

        private void cmdEqual_click(object sender, EventArgs e)
        {
            flag1 = true;
        }
    
    }
}
