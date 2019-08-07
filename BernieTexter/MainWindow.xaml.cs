using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BernieTexter
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Texter myLogic = new Texter();
        string fromNumber;
        string message;
        string[] toNumbers;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void SendButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                feedbackTextBlock.Text = "";

                if (VerifyAndAssignFromNumber() && VerifyAndAssignMessage() && VerifyAndAssignToNumbers())
                {
                    foreach (string toNumber in toNumbers)
                    {
                        myLogic.SendMessage(message, fromNumber, toNumber);
                    }
                }
            }
            catch (Exception ex) // exception type you want to catch
            {
                string className = MethodInfo.GetCurrentMethod().DeclaringType.Name;
                string methodName = MethodInfo.GetCurrentMethod().Name;
                string message = ex.Message;
                ErrorHandler.HandleError(className, methodName, message);;
            }
        }

        private bool VerifyAndAssignMessage()
        {
            try
            {
                message = messageTextBox.Text;

                if (HasContent() && IsCorrectLength())
                {
                    return true;
                }
                else
                {
                    return false;
                }


                bool HasContent()
                {
                    if (string.IsNullOrWhiteSpace(message))
                    {
                        feedbackTextBlock.Text = "Message was blank. Please fix and retry.";
                        return false;
                    }
                    else return true;
                }

                bool IsCorrectLength()
                {
                    if (message.Length <= 160)
                    {
                        return true;
                    }
                    else
                    {
                        feedbackTextBlock.Text = "Message length exceeded 160 characters. Please fix and retry.";
                        return false;
                    }
                }
            }
            catch (Exception ex) // exception type you want to catch
            {
                string className = MethodInfo.GetCurrentMethod().DeclaringType.Name;
                string methodName = MethodInfo.GetCurrentMethod().Name;
                string message = ex.Message;
                ErrorHandler.HandleError(className, methodName, message);
                return false;
            }
        }

        private bool VerifyAndAssignFromNumber()
        {
            try
            {
                fromNumber = fromNumberTextBox.Text;
                var chars = fromNumber.ToList();

                if (HasContent() && ContainsOnlyPlusOrDigits() && IsCorrectLength())
                {
                    fromNumber = String.Join("", chars);
                    return true;
                }
                else
                {
                    return false;
                }

                bool HasContent()
                {
                    if (string.IsNullOrWhiteSpace(fromNumber))
                    {
                        feedbackTextBlock.Text = "'From' phone number was blank. Please fix and retry.";
                        return false;
                    }
                    else
                    {
                        chars = AddPlusAnd1IfNeeded(chars);
                        return true;
                    }
                }

                bool ContainsOnlyPlusOrDigits()
                {
                    foreach (char c in chars)
                    {
                        if (!(c.Equals('+') || Char.IsDigit(c)))
                        {
                            feedbackTextBlock.Text = "'From' phone number had an incorrect letter or symbol. Please fix and retry.";
                            return false;
                        }
                    }
                    return true;
                }

                bool IsCorrectLength()
                {
                    if (chars.Count != 12)
                    {
                        feedbackTextBlock.Text = "'From' phone number had incorrect length. Please fix and retry.";
                        return false;
                    }
                    else return true;
                }
            }
            catch (Exception ex) // exception type you want to catch
            {
                string className = MethodInfo.GetCurrentMethod().DeclaringType.Name;
                string methodName = MethodInfo.GetCurrentMethod().Name;
                string message = ex.Message;
                ErrorHandler.HandleError(className, methodName, message);
                return false;
            }
        }

        private bool VerifyAndAssignToNumbers()
        {
            try
            {
                //parse in a list of phone numbers and validate each
                string tempToNumbers = toNumbersTextBox.Text;


                //comma separated
                if (HasContent()) // ContainsOnlyPlusOrDigits() && IsCorrectLength()
                {
                    if (tempToNumbers.Contains(","))
                    {
                        toNumbers = tempToNumbers.Split(',');
                    }
                    CleanStrings();
                }    
                
                bool HasContent()
                {
                    if (string.IsNullOrWhiteSpace(fromNumber))
                    {
                        feedbackTextBlock.Text = "'To' phone numbers were blank. Please fix and retry.";
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }

                void CleanStrings()
                {
                    foreach (string s in toNumbers)
                    {
                        s.Trim();
                        //remove dashes, spaces,  ( and )
                    }
                }

                return true;
            }
            catch (Exception ex) // exception type you want to catch
            {
                string className = MethodInfo.GetCurrentMethod().DeclaringType.Name;
                string methodName = MethodInfo.GetCurrentMethod().Name;
                string message = ex.Message;
                ErrorHandler.HandleError(className, methodName, message);
                return false;
            }
        }

        List<char> AddPlusAnd1IfNeeded(List<char> chars)
        {
            try
            {
                //check that starts with +, if not add
                if (chars[0] != '+')
                {
                    chars.Insert(0, '+');
                }

                //check that next char is 1, if not add
                if (chars[1] != '1')
                {
                    chars.Insert(1, '1');
                }

                return chars;
            }
            catch (Exception ex) // exception type you want to catch
            {
                string className = MethodInfo.GetCurrentMethod().DeclaringType.Name;
                string methodName = MethodInfo.GetCurrentMethod().Name;
                string message = ex.Message;
                ErrorHandler.HandleError(className, methodName, message);
                return null;
            }
        }
    }
}
