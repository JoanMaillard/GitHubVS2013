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
        Boolean zzAnalyzerKickedIn = false;
        String[] temp;
        Boolean DEBUG = false;
        String string1 = String.Empty;
        //Boolean operationPressed = false;
       // String operand1 = String.Empty;
        //String operand2 = String.Empty;
        //Char operation;
        //Double result = 0.0;
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

     
        private void ansButton_click(object sender, EventArgs e)
        {
            if (lblPreviousCalc.Text != "")
            {
                lblResult.Text = "";
                lblResult.Text += lblPreviousCalc.Text;
            }
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
           /* operationPressed = false;
            Double num1, num2;
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
            lblResult.Text = Convert.ToString(result);*/
            Analyzer((String)lblResult.Text);

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
          /*  if (operationPressed == false)
            {
                operand1 = operand1 + ButtonText;
            }
            else
            {
                operand2 = operand2 + ButtonText;
            }*/
        }

        /*private void OperationProcessor(String OperationText)
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
        }*/

        


        // Syntax analyzer (todo)
    private void Analyzer (String Calculus)
        {
            zzAnalyzerKickedIn = false;
            //int n;    
            char[] operators = {'+', '-', '*', '/', 's', 'c', 't', '(', ')'};
            char[] numbers = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', '.' };
            //auto-generating array on this line of code

   
        foreach (char c in Calculus)
                    //add numbers to the current string, then use double.TryParse to make it a "double"
                    temp = Calculus.Split('+');
                    foreach (String WHATEVER in temp)
                    {
                        string1 = ZzAnalyzer(WHATEVER, 0);
                       
                    }
                    MessageBox.Show(string1);
                    temp = string1.Split('-');
                    foreach (String WHATEVER in temp)
                    {
                         string1 = ZzAnalyzer(WHATEVER, 1);

                     
                    }
                    temp = string1.Split('/');
                    foreach (String WHATEVER in temp)
                    {
                         string1 = ZzAnalyzer(WHATEVER, 2);
                       
                    }
                    temp = string1.Split('*');
                    foreach (String WHATEVER in temp)
                    {
                         string1 = ZzAnalyzer(WHATEVER, 3);
                     
                    }
                    foreach (String WHATEVER in temp)
                    {
                    if (zzAnalyzerKickedIn == false)
                        ZzAnalyzer(WHATEVER, 4);
                    }

                
                /*else if (operators.Contains(c))
                {
          

                }
                else //error case
                {
                    lblPower.Text = "Error, please press the CA button";
                }*/
        }

    private Boolean MultipleOperators(String OPS) {
        int x = 0;
        foreach (char c in OPS) { if (c == '+' || c == '-' || c == '*' || c == '/') x++; }
        if ( x>1 )
        {
            return true;
        }
        else
        return false; 
    } // These 2 missing 'n' needed
    private Boolean MultipleOperators(char[] OPS) {
        int x = 0;
        foreach (char c in OPS) { if (c == '+' || c == '-' || c == '*' || c == '/') x++; }
        if (x > 1)
        {
            return true;
        }
        else
            return false;  
    }

    private String ZzAnalyzer(String ZzCalculus, int Type) 
    {
        Boolean error = false;
        String ReturnValue = String.Empty;
        Double num_a = 0.0, num_b = 0.0;
        switch (Type)
        {
            
            case 0:
                
                    
                    zzAnalyzerKickedIn = true;
                    try
                    {
                        num_a = Double.Parse(temp[0]);
                        num_b = Double.Parse(temp[1]);
                    }
                    catch (Exception ex) { error = true; if (DEBUG) { MessageBox.Show(ex.Message); }; }
                    if (error == false)
                    {
                        num_a = num_a + num_b;
                        Math.Round(num_a, 10);
                        lblResult.Text = (num_a.ToString());
                       
                    }
                    else
                        ReturnValue = ZzCalculus;
                
                    break;
           
            case 1:
                    zzAnalyzerKickedIn = true;
                try
                    {
                        num_a = Double.Parse(temp[0]);
                        num_b = Double.Parse(temp[1]);
                    }
                    catch (Exception ex) { error = true; if (DEBUG) { MessageBox.Show(ex.Message); }; }
                if (error == false)
                {
                    num_a = num_a - num_b;
                    Math.Round(num_a, 10);
                    lblResult.Text = (num_a.ToString());
                }
                else
                    ReturnValue = string1;
                    break;
           
            case 2:
                  
                    zzAnalyzerKickedIn = true;
                try
                    {
                        num_a = Double.Parse(temp[0]);
                        num_b = Double.Parse(temp[1]);
                    }
                    catch (Exception ex) { error = true; if (DEBUG) { MessageBox.Show(ex.Message); }; }
                if (error == false)
                {
                    num_a = num_a / num_b;
                    Math.Round(num_a, 10);
                    lblResult.Text = (num_a.ToString());
                }
                else
                    ReturnValue = string1;
                    break;
            
            case 3:
                  
                    zzAnalyzerKickedIn = true;
                try 
                    {
                        num_a = Double.Parse(temp[0]);
                        num_b = Double.Parse(temp[1]);
                    }
                    catch (Exception ex) { error = true; if (DEBUG) { MessageBox.Show(ex.Message); }; }
                if (error == false)
                {
                    num_a = num_a * num_b;
                    Math.Round(num_a, 10);
                    lblResult.Text = (num_a.ToString());
                }
                    break;
            default:
                    lblPower.Text = "Error - please rewrite your calculus without any disturbing characters";
                    break;
        }
            

        
        return ReturnValue;
    }

    private void cmd_ClearAll(object sender, EventArgs e) // this event reinitializes all the actions ever made in the calculator.
    {
        equalPressed = false;
        lblPower.Text = "";
        lblPreviousCalc.Text = "";
        lblResult.Text = "";
    }

       

        
    }
}
/*
private void asdéfkkj(
int num1, num2;
num1 'op' num2

num2 = num1 op num2

*/