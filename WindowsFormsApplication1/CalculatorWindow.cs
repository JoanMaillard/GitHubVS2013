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
        String[] temp;
        Boolean DEBUG = false;
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
            Processor(pressedButton.Text);
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
           // CommaChanger(lblResult.Text); (Todo)
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
            
                //Notation "0.xx" for numbers below 1, instead of ".xx"
                if (lblResult.Text == "0")
                    if (ButtonText != "." && ButtonText != "+" && ButtonText != "-" && ButtonText != "*" && ButtonText != "/")
                        lblResult.Text = "";
                lblResult.Text = lblResult.Text + ButtonText;
                if (ButtonText == "+" || ButtonText == "-" || ButtonText == "*" || ButtonText == "/")
                    lblResult.Text = lblResult.Text + "(double)"; //Will be removed, is necessary to make decimals in numbers where the compiler would consider integers if not precised            
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
                    sb.AppendLine(String.Format("Error ({0}): {1}"/*, error.ErrorNumber /*(facultatif)*/, error.ErrorText));
                }

                MessageBox.Show(sb.ToString());
                return 0;
            }

            // If the compilation is sucessfull the execute the function
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

        private void cmdSin_click(object sender, EventArgs e)
        {
            eraser();
            lblResult.Text = lblResult.Text + "Math.Sin(";
        }

        private void cmdCos_click(object sender, EventArgs e)
        {
            eraser();
            lblResult.Text = lblResult.Text + "Math.Cos(";
        }

        private void cmdTan_click(object sender, EventArgs e)
        {
            eraser();
            lblResult.Text = lblResult.Text + "Math.Tan(";
        }

        private void cmdPower_click(object sender, EventArgs e)
        {
            eraser();
            lblResult.Text = lblResult.Text + "Math.Pow(";
        }

        private void cmdSquareRoot_click(object sender, EventArgs e)
        {
            eraser();
            lblResult.Text = lblResult.Text + "Math.Sqrt(";
        }

        private void eraser()
        {
            if (lblResult.Text == "0")
                lblResult.Text = String.Empty;
        }


       /* private void Doublize (string Analyzable)
        {
            StringBuilder sb = new StringBuilder();
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




        


        // Syntax analyzer (todo)
  /*  private void Analyzer (String Calculus)
        {
            //zzAnalyzerKickedIn = false;
            //int n;    
            char[] operators = {'+', '-', '*', '/', 's', 'c', 't', '(', ')'};
            //auto-generating array on this line of code

   
        foreach (char c in Calculus)
                    //add numbers to the current string, then use double.TryParse to make it a "double"
                    temp = Calculus.Split('+');
                    foreach (String WHATEVER in temp)
                    {
                        string1 = ZzAnalyzer(WHATEVER, 0);
                       
                    }
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
                    /*foreach (String WHATEVER in temp)
                    {
                    if (zzAnalyzerKickedIn == false)
                        ZzAnalyzer(WHATEVER, 4);
                    }

                

        }
// the next 2 methods will be used for nultiple operators.
    private Boolean MultipleOperators(String OPS) {
        int x = 0;
        foreach (char c in OPS) { if (c == '+' || c == '-' || c == '*' || c == '/') x++; }
        if (x > 1)
        {
            return true;
        }
        else
        return false; 
    }
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

    private String ZzAnalyzer(String ZzCalculus, int Type) //this part of the code is where everything is calculated
    {
        Boolean error = false;
        String ReturnValue = String.Empty;
        Double num_a = 0.0, num_b = 0.0;
        switch (Type)
        {
            case 0:
                
                    
                    //zzAnalyzerKickedIn = true;
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
                        lblResult.Text = "0";
                        lblPreviousCalc.Text = (num_a.ToString());
                    }
                    else
                        ReturnValue = ZzCalculus;
                
                    break;
           
            case 1:
                    //zzAnalyzerKickedIn = true;
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
                    lblResult.Text = "0";
                    lblPreviousCalc.Text = (num_a.ToString());
                }
                else
                    ReturnValue = string1;
                    break;
           
            case 2:
                  
                  //  zzAnalyzerKickedIn = true;
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
                    lblResult.Text = "0";
                    lblPreviousCalc.Text = (num_a.ToString());
                }
                else
                    ReturnValue = string1;
                    break;
            
            case 3:
                  
                  //  zzAnalyzerKickedIn = true;
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
                    lblResult.Text = "0";
                    lblPreviousCalc.Text = (num_a.ToString());
                }
                    break;
            default:
                    lblPower.Text = "Error - please rewrite your calculus without any disturbing characters";
                    break;
        }
            

        
        return ReturnValue;
    }
    */
   



    }
}

