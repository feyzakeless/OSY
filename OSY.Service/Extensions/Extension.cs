using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSY.Service.Extensions
{
    public static class Extension
    {
       //Encode İslemi
        public static string EncodeBase64(this string value)
        {
            var valueBytes = Encoding.UTF8.GetBytes(value);
            return Convert.ToBase64String(valueBytes);
        }

        //Decode İslemi
        public static string DecodeBase64(this string value)
        {
            var valueBytes = System.Convert.FromBase64String(value);
            return Encoding.UTF8.GetString(valueBytes);
        }

        //Daire basına düsen faturayı bulma islemi
        public static decimal DivideTotalBill(int apartments, decimal totalPrice)
        {
            return totalPrice / apartments;
        }

        //Random parola olusturma islemi
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
