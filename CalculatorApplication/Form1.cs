using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CalculatorApplication
{
    public partial class FrmCalculator : Form
    {
        private CalculatorClass cal = new CalculatorClass();

        public FrmCalculator()
        {
            InitializeComponent();
            cal = new CalculatorClass();

            cbOperator.Items.Add("+");
            cbOperator.Items.Add("-");
            cbOperator.Items.Add("*");
            cbOperator.Items.Add("/");
        }

        private void btnEqual_Click(object sender, EventArgs e)
        {
            try
            {
                double n1 = Convert.ToDouble(txtBoxInput1.Text);
                double n2 = Convert.ToDouble(txtBoxInput2.Text);
                string op = cbOperator.SelectedItem?.ToString();

                if (op == null)
                {
                    MessageBox.Show("Error!! Please select a valid operator.");
                    return;
                }

                double result = 0;
                switch (op)
                {
                    case "+":
                        result = cal.GetSum(n1, n2);
                        break;

                    case "-":
                        result = cal.GetDifference(n1, n2);
                        break;

                    case "*":
                        result = cal.GetProduct(n1, n2);
                        break;

                    case "/":
                        if (n2 == 0)
                        {
                            MessageBox.Show("Error!! Division by zero.");
                            return;
                        }
                        result = cal.GetQuotient(n1, n2);
                        break;

                    default:
                        MessageBox.Show("Error!! Please select a valid operator.");
                        return;
                }

                lblDisplayTotal.Text = result.ToString();
            }
            catch (FormatException)
            {
                MessageBox.Show("Error!! Please enter valid numbers.");
            }
        }
    }

    // Delegate and event
    public delegate T Formula<T>(T arg1, T arg2);

    public class CalculatorClass
    {
        public Formula<double> CalculateEvent;

        public double GetSum(double n1, double n2) => n1 + n2;
        public double GetDifference(double n1, double n2) => n1 - n2;
        public double GetProduct(double n1, double n2) => n1 * n2;
        public double GetQuotient(double n1, double n2) => n2 != 0 ? n1 / n2 : 0;

        public void AddCalculateEvent(Formula<double> method)
        {
            CalculateEvent += method;
            Console.WriteLine("Added the Delegate");
        }

        public void RemoveCalculateEvent(Formula<double> method)
        {
            CalculateEvent -= method;
            Console.WriteLine("Removed the Delegate");
        }
    }
}