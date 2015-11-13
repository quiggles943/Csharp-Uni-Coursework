using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security;
using System.Runtime.InteropServices;

namespace Menu_Program
{
    internal static partial class encryption
    {
        internal static string encrypt(string password)
        {
            var seed = 12584;
            char[] pass = password.ToCharArray();
            Random r = new Random(seed);
            int length = password.Length-1;
            for(int i=0; i<= length;i++)
            {
                int x = Convert.ToInt32(pass[i]);
                x = x+r.Next(1,20);
                pass[i] = Convert.ToChar(x) ;
            }
            string secure = new string(pass);
            return secure;


        }

        private static string decrypt(string password)
        {
            var seed = 12584;
            char[] pass = password.ToCharArray();
            Random r = new Random(seed);
            int length = password.Length - 1;
            for (int i = 0; i <= length; i++)
            {
                int x = Convert.ToInt32(pass[i]);
                x = x - r.Next(1, 20);
                pass[i] = Convert.ToChar(x);
            }
            string insecure = new string(pass);
            return insecure;
        }
    }
}
