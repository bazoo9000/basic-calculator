using System.Globalization;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CalculatorWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private enum Operation
        {
            NONE = 0, 
            ADD, SUB, MUL, DIV, MOD
        }

        private const NumberStyles PARSER_FLAGS = NumberStyles.AllowExponent | NumberStyles.AllowDecimalPoint;
        private const int MAX_TEXTBLOCK_SIZE = 21;
        private const int MAX_TEXTBLOCK_SIZE_INT = 19;
        private Operation operation = Operation.NONE;
        private decimal firstNum;

        public MainWindow()
        {
            InitializeComponent();
            CalculationsTextBlock.Text = "0";
            InitOperationButtons();
            InitNumberButtons();
            InitSpecialButtons();
        }

        #region INITS

        private void InitOperationButtons()
        {
            Button_Add.Click += ExtraOperationButtonEvents;
            Button_Subtract.Click += ExtraOperationButtonEvents;
            Button_Multiply.Click += ExtraOperationButtonEvents;
            Button_Divide.Click += ExtraOperationButtonEvents;
        }

        private void InitNumberButtons()
        {
            Button_0.Click += ExtraNumberButtonEvents;
            Button_1.Click += ExtraNumberButtonEvents;
            Button_2.Click += ExtraNumberButtonEvents;
            Button_3.Click += ExtraNumberButtonEvents;
            Button_4.Click += ExtraNumberButtonEvents;
            Button_5.Click += ExtraNumberButtonEvents;
            Button_6.Click += ExtraNumberButtonEvents;
            Button_7.Click += ExtraNumberButtonEvents;
            Button_8.Click += ExtraNumberButtonEvents;
            Button_9.Click += ExtraNumberButtonEvents;
        }

        private void InitSpecialButtons()
        {
            Button_SquareRoot.Content = "\u221A" + "x";
            Button_Power2.Content = "x" + "\u00B2";
        }

        #endregion

        #region NUMBER BUTTONS

        private void Button_0_Click(object sender, RoutedEventArgs e)
        {
            CalculationsTextBlock.Text += "0";
        }

        private void Button_1_Click(object sender, RoutedEventArgs e)
        {
            CalculationsTextBlock.Text += "1";
        }

        private void Button_2_Click(object sender, RoutedEventArgs e)
        {
            CalculationsTextBlock.Text += "2";
        }

        private void Button_3_Click(object sender, RoutedEventArgs e)
        {
            CalculationsTextBlock.Text += "3";
        }

        private void Button_4_Click(object sender, RoutedEventArgs e)
        {
            CalculationsTextBlock.Text += "4";
        }

        private void Button_5_Click(object sender, RoutedEventArgs e)
        {
            CalculationsTextBlock.Text += "5";
        }

        private void Button_6_Click(object sender, RoutedEventArgs e)
        {
            CalculationsTextBlock.Text += "6";
        }

        private void Button_7_Click(object sender, RoutedEventArgs e)
        {
            CalculationsTextBlock.Text += "7";
        }

        private void Button_8_Click(object sender, RoutedEventArgs e)
        {
            CalculationsTextBlock.Text += "8";
        }

        private void Button_9_Click(object sender, RoutedEventArgs e)
        {
            CalculationsTextBlock.Text += "9";
        }

        #endregion

        #region OPERATION BUTTONS

        private void Button_Add_Click(object sender, RoutedEventArgs e)
        {
            operation = Operation.ADD;
            OperationLabel.Content = "+";
        }

        private void Button_Sub_Click(object sender, RoutedEventArgs e)
        {
            operation = Operation.SUB;
            OperationLabel.Content = "-";
        }

        private void Button_Mul_Click(object sender, RoutedEventArgs e)
        {
            operation = Operation.MUL;
            OperationLabel.Content = "*";
        }

        private void Button_Div_Click(object sender, RoutedEventArgs e)
        {
            operation = Operation.DIV;
            OperationLabel.Content = "/";
        }

        #endregion

        #region SPECIAL BUTTONS

        private void Button_Inverse_Click(object sender, RoutedEventArgs e)
        {
            decimal num = decimal.Parse(CalculationsTextBlock.Text, PARSER_FLAGS);
            if (num == 0.0m)
            {
                MessageBox.Show("ERROR! Division by zero!");
                return;
            }
            decimal result = 1.0m / num;
            CalculationsTextBlock.Text = result.ToString(GetNumberFormat(result));
        }

        private void Button_Power2_Click(object sender, RoutedEventArgs e)
        {
            decimal num = decimal.Parse(CalculationsTextBlock.Text, PARSER_FLAGS);

            try
            {
                decimal result = (decimal)Math.Pow((double)num, 2);
                CalculationsTextBlock.Text = result.ToString(GetNumberFormat(result));
            }
            catch (Exception)
            {
                MessageBox.Show("ERROR! Number is too big!");
            }
        }

        private void Button_Sqrt_Click(object sender, RoutedEventArgs e)
        {
            decimal num = decimal.Parse(CalculationsTextBlock.Text, PARSER_FLAGS);
            if (num < 0.0m)
            {
                MessageBox.Show("ERROR! You can't square root a negative number!");
                return;
            }
            decimal result = (decimal)Math.Sqrt((double)num);
            CalculationsTextBlock.Text = result.ToString(GetNumberFormat(result));
        }

        #endregion

        #region EQUALS BUTTON LOGIC

        private void Button_Equals_Click(object sender, RoutedEventArgs e)
        {
            decimal secondNum = decimal.Parse(CalculationsTextBlock.Text, PARSER_FLAGS);
            decimal result = 0.0m;
           
            switch (operation)
            {
                case Operation.NONE: break;
                case Operation.ADD:
                    result = firstNum + secondNum;
                    break;
                case Operation.SUB:
                    result = firstNum - secondNum;
                    break;
                case Operation.MUL:
                    result = firstNum * secondNum;
                    break;
                case Operation.DIV:
                    if(secondNum == 0)
                    {
                        MessageBox.Show("ERROR! Trying to divide by 0!");
                        return;
                    }
                    result = firstNum / secondNum;
                    break;
            }

            MemoryTextBlock.Text = "";
            CalculationsTextBlock.Text = result.ToString(GetNumberFormat(result));
        }

        #endregion

        #region DELETE BUTTONS

        private void Button_Delete_Click(object sender, RoutedEventArgs e)
        {
            String temp = CalculationsTextBlock.Text;

            if (temp.Length <= 1)
            {
                CalculationsTextBlock.Text = "0";
                return;
            }

            if(temp[temp.Length - 1] == '.')
            {
                return;
            }

            CalculationsTextBlock.Text = temp.Remove(temp.Length - 1);
        }

        private void Button_Clear_Click(object sender, RoutedEventArgs e)
        {
            CalculationsTextBlock.Text = "0";
            MemoryTextBlock.Text = "";
            OperationLabel.Content = "";
        }

        #endregion

        #region MODIFY NUMBER PROPERTIES BUTTONS

        private void Button_Sign_Click(object sender, RoutedEventArgs e)
        {
            if(!CalculationsTextBlock.Text.Contains('-'))
            {
                CalculationsTextBlock.Text = "-" + CalculationsTextBlock.Text;
            }
            else
            {
                CalculationsTextBlock.Text = CalculationsTextBlock.Text.Substring(1);
            }
        }

        private void Button_Float_Click(object sender, RoutedEventArgs e)
        {
            if(!CalculationsTextBlock.Text.Contains('.'))
            {
                CalculationsTextBlock.Text += ".";
            }
            else
            {
                String temp = CalculationsTextBlock.Text;
                CalculationsTextBlock.Text = temp.Remove(temp.IndexOf('.'));
            }
        }

        #endregion

        #region EXTRA EVENTS FOR BUTTONS

        private void ExtraOperationButtonEvents(object sender, RoutedEventArgs e)
        {
            Memorize();
            SetZero();
        }

        private void ExtraNumberButtonEvents(object sender, RoutedEventArgs e)
        {
            RemoveFirstZero();
            CheckOverMaxSize();
        }

        #region METHODS FOR SAID EVENTS

        private void RemoveFirstZero()
        {
            bool beginsWithZero = CalculationsTextBlock.Text.StartsWith('0');
            bool hasNoDecimal = !CalculationsTextBlock.Text.Contains('.');
            if (beginsWithZero && hasNoDecimal)
            {
                CalculationsTextBlock.Text = CalculationsTextBlock.Text.Substring(1);
            }
        }

        private void Memorize()
        {
            if (MemoryTextBlock.Text.Length > 0)
            {
                return;
            }

            firstNum = decimal.Parse(CalculationsTextBlock.Text, PARSER_FLAGS);

            MemorizeNumber(firstNum);
        }

        private void SetZero()
        {
            CalculationsTextBlock.Text = "0";
        }

        private void CheckOverMaxSize()
        {
            bool isBiggerInt = CalculationsTextBlock.Text.Length > MAX_TEXTBLOCK_SIZE_INT;
            bool isBigger = CalculationsTextBlock.Text.Length > MAX_TEXTBLOCK_SIZE;
            bool isDecimal = CalculationsTextBlock.Text.Contains('.');

            if (!isBiggerInt && !isDecimal)
            {
                return;
            }

            if (!isBigger && isDecimal)
            {
                return;
            }

            CalculationsTextBlock.Text = CalculationsTextBlock.Text.Remove(CalculationsTextBlock.Text.Length - 1);
        }

        #endregion

        #endregion

        #region USEFULL FUNCTIONS

        private string GetNumberFormat(decimal num)
        {
            int numOfDigits = GetNumOfInts(num);
            string fmt;
            if (numOfDigits > MAX_TEXTBLOCK_SIZE_INT)
            {
                fmt = "e" + (Math.Clamp(num.ToString().Length, 0, 14)).ToString();
                return fmt;
            }

            if(IsNumTooSmall(num))
            {
                fmt = "e" + (Math.Clamp(num.ToString().Length, 0, 10)).ToString();
                return fmt;
            }

            fmt = "0.";
            for (int i = numOfDigits + 1; i < MAX_TEXTBLOCK_SIZE; i++)
            {
                fmt += "#";
            }

            return fmt;
        }

        private int GetNumOfInts(decimal num)
        {
            String temp = num.ToString();
            if (temp.IndexOf('.') == -1)
            {
                int numOfInts = 0;
                while (num > 1.0m)
                {
                    num /= 10;
                    numOfInts++;
                }

                return numOfInts;
            }

            return temp.IndexOf('.');
        }

        private void MemorizeNumber(decimal num)
        {
            MemoryTextBlock.Text = num.ToString(GetNumberFormat(num));
            CalculationsTextBlock.Text = "";
        }

        private bool IsNumTooSmall(decimal num)
        {
            int count = 0;
            while(num <= 1.0m)
            {
                count++;
                num *= 10;
            }

            return count > 18;
        }

        #endregion
    }
}