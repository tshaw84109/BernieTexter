using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BernieTexter
{
    class ErrorHandler
    {
        public static void HandleError(string className, string method, string message)
        {
            try
            {
                Console.WriteLine($"{className}.{method}->{message}");
            }
            catch (Exception ex)
            {

                System.IO.File.AppendAllText(@"C:\Users\Public\error.txt",
                    $"{Environment.NewLine} HandleError Exception: {ex.Message}");
            }
        }
    }
}
