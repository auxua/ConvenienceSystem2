using System;
using System.Linq;

namespace Toolbox
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine(menu);
                var a = Console.ReadKey();
                char selection = a.KeyChar;
                Console.WriteLine();

                switch (selection)
                {
                    case '1':
                        // Random String
                        string random = GenerateRandomString();
                        Console.WriteLine(random);
                        System.Windows.Forms.Clipboard.SetText(random);
                        Console.WriteLine("The string has been copied to yout clipboard");
                        break;
                    case '2':
                        // Create hash from input
                        Console.WriteLine("Please type your Password:");
                        string pass = Console.ReadLine();
                        var Crypto = System.Security.Cryptography.SHA512.Create();

                        byte[] passwordBytes = GetBytes(pass);
                        var passwordHash = Crypto.ComputeHash(passwordBytes);
                        string passwordHash64 = Convert.ToBase64String(passwordHash);

                        Console.WriteLine(passwordHash64);
                        System.Windows.Forms.Clipboard.SetText(passwordHash64);
                        Console.WriteLine("The string has been copied to yout clipboard");

                        break;
                    case '3':
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Please insert valid slection");
                        break;
                }

                Console.WriteLine();
            }
        }

        readonly static string menu = "Please select one option from below: " + Environment.NewLine + Environment.NewLine 
            + "\t (1) Generate Random String (e.g. Device Token)" + Environment.NewLine 
            + "\t (2) Generate password-hash" + Environment.NewLine 
            + "\t (3) Quit "
             +Environment.NewLine + "  Please select Option: ";

        internal static String GenerateRandomString()
        {
            String chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            Random random = new Random();
            String result = new string(
                Enumerable.Repeat(chars, 64)
                          .Select(s => s[random.Next(s.Length)])
                          .ToArray());
            return result;
        }

        static byte[] GetBytes(string str)
        {
            /*byte[] bytes = new byte[str.Length * sizeof(char)];
            System.Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
            return bytes;*/
            return System.Text.Encoding.UTF8.GetBytes(str);
        }

        static string GetString(byte[] bytes)
        {
            /*char[] chars = new char[bytes.Length / sizeof(char)];
            System.Buffer.BlockCopy(bytes, 0, chars, 0, bytes.Length);
            return new string(chars);*/
            return System.Text.Encoding.UTF8.GetString(bytes);
        }
    }
}
