﻿using System;
using System.IO;
using System.Data;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Drawing;

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

        public static Image ImageFromBase64(string encoded)
        {
            byte[] imgbytes = Convert.FromBase64String(encoded);

            using(var ms = new MemoryStream(imgbytes, 0, imgbytes.Length))
            {
                Image img = Image.FromStream(ms, true);
                return img;
            }
        }

        /// <summary>
        /// Convert datetime to the format: 1 Hora 2 Minutos
        /// </summary>
        /// <param name="time">Datetime to convert</param>
        /// <returns>Output string</returns>
        public static string generateStringEstimatedTime(DateTime time)
        {
            string hour = "", minute = "", second = "";
            
            if (time.Hour != 0)
            {
                hour = string.Format(
                    "{0} {1}",
                    time.Hour,
                    time.Hour > 1 ? "Horas" : "Hora"
                );
            }

            if (time.Minute != 0)
            {
                minute = string.Format(
                    "{0} {1}",
                    time.Minute,
                    time.Minute > 1 ? "Minutos" : "Minuto"
                );
            }

            if (time.Second != 0)
            {
                second = string.Format(
                    "{0} {1}",
                    time.Second,
                    time.Second > 1 ? "Segundos" : "Segundo"
                );
            }

            return string.Format("{0} {1} {2}", hour, minute, second).Trim();

        }

        /// <summary>
        /// Function to parse the estimated time with the format
        /// 1 Hora 2 Minutos
        /// </summary>
        /// <param name="estimated">Input text</param>
        /// <returns>Returns DateTime object with the parsed time</returns>
        public static DateTime parseTime(string estimated)
        {
            DateTime time = new DateTime(2000, 11, 13);
            MatchCollection matches = new Regex(
                @"\d+\s\w+",
                RegexOptions.IgnoreCase | RegexOptions.Compiled
            ).Matches(estimated);

            foreach (Match match in matches)
            {
                string value = match.Value.ToLower();
                int unit = int.Parse(Regex.Replace(value, "\\D+", ""));
                if (value.Contains("hora"))
                {
                    time = time.AddHours(unit);
                } else if (value.Contains("minuto"))
                {
                    time = time.AddMinutes(unit);
                } else if (value.Contains("segundo"))
                {
                    time = time.AddSeconds(unit);
                }
            }

            return time;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public static Stream GenerateStreamFromString(string content)
        {
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);

            writer.Write(content);
            writer.Flush();
            stream.Position = 0;

            return stream;
        }

        public static string CreateCSVDataTable(DataTable table)
        {
            StringBuilder sb = new StringBuilder();

            string[] columnNames = table.Columns.Cast<DataColumn>()
                .Select(column => column.ColumnName)
                .ToArray();

            sb.AppendLine(string.Join(",", columnNames));

            foreach(DataRow row in table.Rows)
            {
                string[] fields = row.ItemArray.Select(
                    field => field.ToString()
                ).ToArray();

                sb.AppendLine(string.Join(",", fields));
            }

            return sb.ToString();
        }
    
    
        public static string GenerateUUID()
        {
            Guid guid = Guid.NewGuid();
            string uuid = guid.ToString();
            return uuid;
        }
    }
}