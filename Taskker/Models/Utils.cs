using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Security.Cryptography;

namespace Taskker.Models
{
    public class Utils
    {
        public static string Capitalize(string src)
        {
            return string.Join(" ", src.Split(' ').ToList()
                    .ConvertAll(palabra =>
                        palabra.Substring(0, 1).ToUpper() + palabra.Substring(1)
                    )
                );
        }

        public static byte[] HashPassword(string password)
        {
            byte[] result = new SHA512Managed().ComputeHash(
                UTF8Encoding.UTF8.GetBytes(password)
            );

            return result;
        }

        public static string EncodePicture(string path)
        {
            try
            {
                byte[] encoded = File.ReadAllBytes(path);
                return $"data:image/jpg;base64,{Convert.ToBase64String(encoded, 0, encoded.Length)}";
            }
            catch (IOException)
            {
                return string.Empty;
            }
        }

    }
}