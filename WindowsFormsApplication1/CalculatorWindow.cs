// This page contains the program for the startup window
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Microsoft.CSharp;
using System.CodeDom.Compiler;
using System.Text.RegularExpressions;

namespace WindowsFormsApplication1
{
    public partial class Calculatorr : Form
    {
        Boolean calculated = false; // situational bool used in a display funtion, which is to know whether or not clear the input field or not
        Boolean OperatorPressed = false; // used to avoid double operators
        Boolean periodPressed = false; // The name says it all.
        Boolean antiOperatorPeriod = false; // Needed to avoid having an operator, and then directly a period behind it.
        Boolean num1OrNum2 = false; // Needed to swap the two modes for the power function
        Boolean inPower = false; // Needed to know where to put num1 and num2 (see further)
        String Selector = String.Empty; // Needed to know what function we're talking about
        String CorrectedString = String.Empty; // Calculable string
        String PeriodTester = "."; // Needed for a ".Contains" function
        String num1, num2 = String.Empty; //numbers used for special funtions  
        String NumberTester = "1234567890"; // Needed for a ".Contains" function
        
        public Calculatorr()
        {
            InitializeComponent();
        }
        // Function called at startup. Just says hello in this case.
        private void Calculatorr_Load(object sender, EventArgs e)
        {
            MessageBox.Show("Hello, thanks for using my crappy beta calculator instead of the basic Windows'! :D");
        }
        // When any number or operation is pressed
        private void button_click(object sender, EventArgs e)
        {
            Button pressedButton = (Button)sender; // Declaration of the "pressedButton" object
        // Generic function for the number and signs to be displayed when clicked
                
                Processor(pressedButton.Text);
        }
       
        private void Processor(String ButtonText) // String = type of variant wanted; ButtonText: name of the variant 
        {


            String OperatorTester = "+-*/^";
          
            if (calculated == true && NumberTester.Contains(ButtonText))
            {
                lblPreviousCalc.Text = lblResult.Text;
                lblResult.Text = String.Empty;
            }
            calculated = false;
            //Notation "0.xx" for numbers below 1, instead of ".xx"
            if (lblResult.Text == "0")
                if (ButtonText != "." && ButtonText != "+" && ButtonText != "-" && ButtonText != "*" && ButtonText != "/")
                    lblResult.Text = String.Empty;
            if (PeriodTester.Contains(ButtonText) && periodPressed == false && antiOperatorPeriod == false)
            {
                lblResult.Text = lblResult.Text + ButtonText;
                periodPressed = true;
            }
            else if (NumberTester.Contains(ButtonText))
            {
                lblResult.Text = lblResult.Text + ButtonText;
                antiOperatorPeriod = false;
                OperatorPressed = false;
            }
            else if (OperatorTester.Contains(ButtonText) &&  antiOperatorPeriod == false && OperatorPressed == false)
            {
                lblResult.Text = lblResult.Text + ButtonText;
                periodPressed = false;
                antiOperatorPeriod = true;
                OperatorPressed = true;
            }
            else
                lblResult.Text = lblResult.Text + ButtonText;
           
            }
        private void memButton_click(object sender, EventArgs e)
        {
            if (lblPreviousCalc.Text != "")
            {
                if (lblResult.Text == "0")
                lblResult.Text = "";
                lblResult.Text = lblResult.Text + lblPreviousCalc.Text;
            }
        }
        // Operations when Clear is pressed
        private void cmdSave_Click(object sender, EventArgs e)
        {
            lblPreviousCalc.Text = lblResult.Text;
            lblResult.Text = "0";
            CorrectedString = String.Empty;
            calculated = false;
            periodPressed = false; 
            antiOperatorPeriod = false; 
        }
        // Operations when "=" is pressed
        private void cmdEqual_click(object sender, EventArgs e)
        {
            int equationInPower = 0;
            lblResult.Text = lblResult.Text + " "; // Ending char used to know where the end of the calculus is.
            foreach (char c in lblResult.Text) // prepares the calculable string, adding some precisions.
            {
                if (c == '^')
                {
                    Selector = "Pow";
                    num1OrNum2 = true;
                    inPower = true;
                }
                 if (inPower == true && c == '(')
                    equationInPower++;
                 if (inPower == true && c == ')')
                    equationInPower--;
                    
                 if (NumberTester.Contains(c) || PeriodTester.Contains(c) || c == '(' || c == ')')
                {
                    if (num1OrNum2 == false)
                        num1 = num1 + c;
                    else
                        num2 = num2 + c;
                }
                 if (inPower == false)
                {
                    if (c == '+' || c == '-' || c == '*' || c == '/')
                    {
                        CorrectedString = CorrectedString + num1 + c + "(double)";
                        num1 = String.Empty;
                    }
                    else if (c == ' ')
                    {
                        CorrectedString = CorrectedString + num1;
                        num1 = String.Empty;
                    }
                }
                 if (inPower == true)
                {
                    if (c == '+' || c == '-' || c == '*' || c == '/')
                    {
                        if (equationInPower == 0)
                        {
                            inPower = false;
                            CorrectedString = CorrectedString + "Math." + Selector + "(" + num1 + ", " + num2 + ")" + c + "(double)";
                            num1 = String.Empty;
                            num2 = String.Empty;
                            num1OrNum2 = false;
                        }
                        else 
                        {
                            num2 = num2 + c + "(double)";
                        }
                    }
                    else if (c == ' ')
                    {
                        inPower = false;
                        CorrectedString = CorrectedString + "Math." + Selector + "(" + num1 + ", " + num2 + ")";
                        num1 = String.Empty;
                        num2 = String.Empty;
                        num1OrNum2 = false;
                    }
                }
            }
            lblResult.Text = getridofcommas(DoStupideCalc(CorrectedString).ToString()); //launching the calculation procedure
            CorrectedString = String.Empty; // emptying the calculable string so it's ready for the next calculus
            calculated = true;
            if (lblResult.Text.Contains(PeriodTester))
                periodPressed = true;

        }
        // Repeating the previous functions on the labels for the 3 special calculus in the additional list 
        private void ToolStripMenuItemSpecial_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem clickedMenu = (ToolStripMenuItem)sender;
           Processor(clickedMenu.Text);
        }
        private void cmdClear_click(object sender, EventArgs e) // Clears the user input.
        {
            lblResult.Text = "0";
            CorrectedString = String.Empty;
        }

        private void cmdClearAll_click(object sender, EventArgs e) //Get the calculator back to startup state.
        {
            lblResult.Text = "0";
            lblPreviousCalc.Text = String.Empty;
            lblPower.Text = String.Empty;
            CorrectedString = String.Empty;
        }

        public double DoStupideCalc(string stupidFormula)
        {
            return CompiledCalc(stupidFormula);
        }

        // Genertate the dyanamic compiled assembly and do the calc
        public double CompiledCalc(string stupidFormula)
        {
            
            // Generate the compiler object and the parameter object
            CSharpCodeProvider provider = new CSharpCodeProvider();
            CompilerParameters parameters = new CompilerParameters();

            //Set some basic parameters
            // Reference to System.Drawing library
            parameters.ReferencedAssemblies.Add("System.Drawing.dll");
            // True - memory generation, false - external file generation
            parameters.GenerateInMemory = true;
            // True - exe file generation, false - dll file generation
            parameters.GenerateExecutable = false;
            
            
            // Compile the assembly
            CompilerResults results = provider.CompileAssemblyFromSource(parameters, GetStupideCalcSourceCode(stupidFormula));

            // Check the compilation result
            if (results.Errors.HasErrors)
            {
                StringBuilder ErrorBuilder = new StringBuilder();

                foreach (CompilerError error in results.Errors)
                {
                    ErrorBuilder.AppendLine(String.Format("Error ({0}): {1}", error.ErrorNumber /*(facultatif)*/, error.ErrorText));
                }

                MessageBox.Show(ErrorBuilder.ToString());
                return 42;
            }

            // If the compilation is sucessfull, execute the function
            Type binaryFunction = results.CompiledAssembly.GetType("StupidCompiledCalculator.CompiledFunctions");
            return (double) binaryFunction.GetMethod("CompiledCalc").Invoke(null,null);
        }

        // Source code for the operation to be compiled and then calculated
        public string GetStupideCalcSourceCode(string stupidFormula)
        {
            string sourceCode;
            string yolo = String.Empty;

            sourceCode = @"
                    using System;
                    namespace StupidCompiledCalculator
                    {
                        public class CompiledFunctions
                        {
                                public static double CompiledCalc()
                                {
                                    return " + stupidFormula + @";
                                }
                        }
                    }";
            return sourceCode;


        }
        private string getridofcommas(string iHaveNoIdeaHowToNameYou)
        {
            String yolo = String.Empty;
            foreach (char c in iHaveNoIdeaHowToNameYou)
            {
                if (c == ',')
                    yolo = yolo + '.';
                else
                    yolo = yolo + c;
            }

            return yolo;
        }

// Displays a goodbye message and closes the application.
        private void cmdQuitApplication_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Thanks for using, feel free to send feedback to joanjoanjostorm@gmail.com :D");
            Application.Exit();
        }
// Minimizes the application.
        private void cmdMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

    }
}

