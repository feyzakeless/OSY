using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSY.Service.Extensions
{
    public class Extension
    {
        public static string GenerateRandomPassword(int passLength)
        {
            const string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            StringBuilder buildPass = new StringBuilder();
            Random randomVal = new Random();
            while (0 < passLength--)
            {
                buildPass.Append(chars[randomVal.Next(chars.Length)]);
            }
            return buildPass.ToString();
        }
    }
}
