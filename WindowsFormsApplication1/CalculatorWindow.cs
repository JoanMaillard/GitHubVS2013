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
        //Boolean zzAnalyzerKickedIn = false;
        String string1 = String.Empty;
          

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
            if (pressedButton.Tag == "Special")
                SpecButton_click(pressedButton.Text);
            else
                Processor(pressedButton.Text);
        }
        private void SpecButton_click(String SpecButtonText)
        {
            eraser();
            lblResult.Text = lblResult.Text + "Math." + SpecButtonText;
        }
        private void Processor(String ButtonText) // String = type of variant wanted; ButtonText: name of the variant 
        {

            //Notation "0.xx" for numbers below 1, instead of ".xx"
            if (lblResult.Text == "0")
                if (ButtonText != "." && ButtonText != "+" && ButtonText != "-" && ButtonText != "*" && ButtonText != "/")
                    lblResult.Text = "";
            lblResult.Text = lblResult.Text + ButtonText;
            if (ButtonText == "+" || ButtonText == "-" || ButtonText == "*" || ButtonText == "/")
                lblResult.Text = lblResult.Text + "(double)"; //Will be removed, is necessary to make decimals in numbers where the compiler would consider integers if not precised            
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
        }
        // Operations when "=" is pressed
        private void cmdEqual_click(object sender, EventArgs e)
        {
           // Doublize(lblResult.Text); (doublize pas encore au point)
            lblResult.Text = (DoStupideCalc(lblResult.Text).ToString());
            /*foreach (char c in lblResult.Text)
            {
            if (c == ',')
                c = '.';
            }*/ //alternative Regex à trouver
           // CommaChanger(lblResult.Text); (Todo)
        }
       // Repeating the previous functions on the labels for the 3 special calculus in the additional list 
        private void ToolStripMenuItemSpecial_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem clickedMenu = (ToolStripMenuItem)sender;
            Processor((String)clickedMenu.Tag);
        }
        private void cmdClear_click(object sender, EventArgs e)
        {
            lblResult.Text = "0";
        }

        private void cmdClearAll_click(object sender, EventArgs e)
        {
            lblResult.Text = "0";
            lblPreviousCalc.Text = "0";
            lblPower.Text = String.Empty;
        }
        private void eraser()
        {
            if (lblResult.Text == "0")
                lblResult.Text = String.Empty;
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
                StringBuilder sb = new StringBuilder();

                foreach (CompilerError error in results.Errors)
                {
                    sb.AppendLine(String.Format("Error ({0}): {1}", error.ErrorNumber /*(facultatif)*/, error.ErrorText));
                }

                MessageBox.Show(sb.ToString());
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

       /* private void Doublize (string Analyzable)
        {
            StringBuilder l = new StringBuilder();
            Boolean banane = false;
            String  Number = "1234567890.";
            foreach (var c in Analyzable)
            {
                if (Number.Contains(c) && banane == false)
                {
                    sb.Append("(double)").Append(c);
                    banane = true;
                }
                else if (!Number.Contains(c))
                    banane = false;
            }
            MessageBox.Show (Analyzable);

        }*/

    }
}

