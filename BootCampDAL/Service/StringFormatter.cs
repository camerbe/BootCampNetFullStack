using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BootCampDAL.Service
{
    public static class StringFormatter
    {
        public static string ToTitleCase(string input)
        {
            if (string.IsNullOrWhiteSpace(input)) { 
                return input;
            }
            
            return string.Join(" ", input.Split(' ')
                .Select(word => char.ToUpper(word[0]) + word.Substring(1).ToLower()));

        }
    }
}
