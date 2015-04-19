// This page contains the program for the startup window
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
        // Bool used for last calculus control
        Boolean equalPressed = false;
        Boolean operationPressed = false;
        String operand1 = String.Empty;
        String operand2 = String.Empty;
        Char operation;
        public Calculatorr()
        {
            InitializeComponent();
        }
        // Function called at startup. Does nothing in this case.
        private void Calculatorr_Load(object sender, EventArgs e)
        {}
        // When any number or operation is pressed
        private void button_click(object sender, EventArgs e)
        {
            Button pressedButton = (Button)sender; // Declaration of the "pressedButton" object
        // Generic function for the number and signs to be displayed when clicked
            Processor(pressedButton.Text);
        }

        private void operator_click(object sender, EventArgs e)
        {
            Button pressedOperator = (Button)sender;
            OperationProcessor(pressedOperator.Text);
        }
        private void ansButton_click(object sender, EventArgs e)
        {
            lblResult.Text = "";
            lblResult.Text += lblPreviousCalc.Text;
        }
        // Operations when Clear is pressed
        private void cmdClear_Click(object sender, EventArgs e)
        {
            lblPreviousCalc.Text = lblResult.Text;
            lblResult.Text = "0";
        }
        // Operations when "=" is pressed
        private void cmdEqual_click(object sender, EventArgs e)
        {
            equalPressed = true;
            operationPressed = false;
            Double num1, num2;
            Double result = 0.0;
            Double.TryParse(operand1, out num1);
            Double.TryParse(operand2, out num2);
            this.operand1 = String.Empty;
            this.operand2 = String.Empty;
            if (operation == '+')
            { result = num1 + num2; }
            else if (operation == '-')
            { result = num1 - num2; }
            else if (operation == '*')
            {result = num1 * num2;}
            else if (operation == '/')
            {
                if (num2 != 0.0)
                    result = num1 / num2;
                else
                    result = 0;
            }
            lblResult.Text = Convert.ToString(result);


        }
       // Repeating the previous functions on the labels for the 3 special calculus in the additional list 
        private void ToolStripMenuItemSpecial_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem clickedMenu = (ToolStripMenuItem)sender;
            Processor((String)clickedMenu.Tag);
        }
        // Method used when any button is pressed:
        private void Processor(String ButtonText) // String = type of variant wanted; ButtonText: name of the variant 
        {
            if (equalPressed == true)
            {
                if (lblPreviousCalc.Text != "")
                { lblPreviousCalc.Text = ""; }
                lblPreviousCalc.Text = lblResult.Text;
                lblResult.Text = "0";
                equalPressed = false;
            }
            //Notation "0.xx" for numbers below 1, instead of ".xx"
            if (lblResult.Text == "0")
                if (ButtonText != ".")
                    lblResult.Text = "";
            lblResult.Text = lblResult.Text + ButtonText;
            if (operationPressed == false)
            {
                operand1 = operand1 + ButtonText;
            }
            else
            {
                operand2 = operand2 + ButtonText;
            }
        }

        private void OperationProcessor(String OperationText)
        {
            if (equalPressed == true)
            {
                if (lblPreviousCalc.Text != "")
                { lblPreviousCalc.Text = ""; }
                lblPreviousCalc.Text = lblResult.Text;
                lblResult.Text = "0";
                equalPressed = false;
                operationPressed = true;
                operand1 = lblPreviousCalc.Text;
            }
            if (lblResult.Text == "0")
            {
                lblResult.Text = "";
                lblResult.Text = lblPreviousCalc.Text + OperationText;
            }
            else
            {
                lblResult.Text += OperationText;
                operationPressed = true;
            }
            operation = (Convert.ToChar(OperationText));
        }




        // Syntax analyzer (todo)
    /*private void Analyzer (Char Category)
        {

        }*/

       

        
    }
}
