using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

/* ******************************************************
* CIS 123: Introduction to Object-Oriented Programming  *
* Murach C# 7th Edition                                 *
* Chapter 8: How to use arrays and collection           *
* Exercise 8-2 Use a rectangular array                  *
*       Base code and form design provided by Murach    *
*       Exercise Instructions: pg. 268                  *
* Dominique Tepper, 25MAY2022                           *
* ******************************************************/

namespace FutureValue
{
    public partial class frmFutureValue : Form
    {
        public frmFutureValue()
        {
            InitializeComponent();
        }

/* ******************************************************************
*   2. Declare 2 class variables
*       A. a row counter
*       B. a 10 x 4 rectangular array of strings
* ********************************************************* Tepper */

        int rowCounter = 0;                       // 2-A
        string [,] fvCalcs = new string [10, 4];    // 2-B

        private void btnCalculate_Click(object sender, EventArgs e)
        {
            try
            {
                if (IsValidData())
                {
                    decimal monthlyInvestment =
                        Convert.ToDecimal(txtMonthlyInvestment.Text);
                    decimal interestRateYearly =
                        Convert.ToDecimal(txtInterestRate.Text);
                    int years = Convert.ToInt32(txtYears.Text);

                    int months = years * 12;
                    decimal interestRateMonthly = interestRateYearly / 12 / 100;

                    decimal futureValue = CalculateFutureValue(
                        monthlyInvestment, interestRateMonthly, months);
                    txtFutureValue.Text = futureValue.ToString("c");
                    txtMonthlyInvestment.Focus();

/* ******************************************************************
*   3. Add code that stores the values for each calculation in the
*      next row of the array when the user clicks Calculate. Format
*      the following:
*      
*           A. monthlyInvestment           currency
*           B. futureValue                 currency
*           C. interestRatePercent         percent
* ********************************************************* Tepper */

                    decimal interestRatePercent = interestRateYearly / 100;
                    fvCalcs[rowCounter, 0] = monthlyInvestment.ToString("c");       // 3A
                    fvCalcs[rowCounter, 1] = interestRatePercent.ToString("p");     // 3C
                    fvCalcs[rowCounter, 2] = years.ToString();
                    fvCalcs[rowCounter, 3] = futureValue.ToString("c");             // 3B
                    rowCounter++;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n\n" +
                    ex.GetType().ToString() + "\n" +
                    ex.StackTrace, "Exception");
            }
        }

        public bool IsValidData()
        {
            bool success = true;
            string errorMessage = "";

            // Validate the Monthly Investment text box
            errorMessage += IsDecimal(txtMonthlyInvestment.Text, txtMonthlyInvestment.Tag.ToString());
            errorMessage += IsWithinRange(txtMonthlyInvestment.Text, txtMonthlyInvestment.Tag.ToString(), 1, 1000);

            // Validate the Yearly Interest Rate text box
            errorMessage += IsDecimal(txtInterestRate.Text, txtInterestRate.Tag.ToString());
            errorMessage += IsWithinRange(txtInterestRate.Text, txtInterestRate.Tag.ToString(), 1, 20);

            // Validate the Number of Years text box
            errorMessage += IsInt32(txtYears.Text, txtYears.Tag.ToString());
            errorMessage += IsWithinRange(txtYears.Text, txtYears.Tag.ToString(), 1, 40);

            if (errorMessage != "")
            {
                success = false;
                MessageBox.Show(errorMessage, "Entry Error");
            }
            return success;
        }

        public static string IsPresent(string value, string name)
        {
            string msg = "";
            if (value == "")
            {
                msg += name + " is a required field.\n";
            }
            return msg;
        }

        public static string IsDecimal(string value, string name)
        {
            string msg = "";
            if (!Decimal.TryParse(value, out _))
            {
                msg += name + " must be a valid decimal value.\n";
            }
            return msg;
        }

        public static string IsInt32(string value, string name)
        {
            string msg = "";
            if (!Int32.TryParse(value, out _))
            {
                msg += name + " must be a valid integer value.\n";
            }
            return msg;
        }

        public static string IsWithinRange(string value, string name, decimal min, decimal max)
        {
            string msg = "";
            if (Decimal.TryParse(value, out decimal number))
            {
                if (number < min || number > max)
                {
                    msg += name + " must be between " + min + " and " + max + ".\n";
                }
            }
            return msg;
        }
        private decimal CalculateFutureValue(decimal monthlyInvestment,
           decimal monthlyInterestRate, int months)
        {
            decimal futureValue = 0m;
            for (int i = 0; i < months; i++)
            {
                futureValue = (futureValue + monthlyInvestment)
                    * (1 + monthlyInterestRate);
            }

            return futureValue;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            // TODO: Display the rectangular array in a dialog box here

            this.Close();
        }

    }
}
