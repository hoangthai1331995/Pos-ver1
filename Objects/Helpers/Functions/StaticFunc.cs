using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Helpers.Functions
{
    public static class StaticFunc
    {
        public static string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }

        public static string Base64Decode(string base64EncodedData)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }

        public static string CowndownTime(DateTime inputDay)
        {
            TimeSpan Time = inputDay - DateTime.Now;
            decimal totalDate = Time.Days;
            if (totalDate >= 365)
            {
                decimal year = Math.Floor(totalDate / 365);
                return "Còn " + year + " năm";
            }
            else
            {
                decimal month = Math.Floor(totalDate / 30);
                if (month >= 1)
                    return "Còn " + month + " tháng";
                else
                {
                    if (totalDate > 0)
                        return "Còn " + totalDate + " ngày";
                    else
                    {
                        return "Đã quá hạn";
                    }
                }
            }
        }

        public static string KillChars(string strInput)
        {
            if (!String.IsNullOrEmpty(strInput))
            {
                string[] arrBadChars = new string[] { "select", "drop", ";", "--", "insert", "delete", "xp_", "sysobjects", "syscolumns", "update", "/>", "cookie", "session", ">", "script", "</" };
                strInput = strInput.Trim().Replace("'", "''");
                strInput = strInput.Replace("%20", " ");
                for (int i = 0; i < arrBadChars.Length; i++)
                {
                    strInput = strInput.Replace(arrBadChars[i].ToString(), "");
                }
            }
            return strInput;
        }

        public static Bitmap ResizeImage(Stream stream, int? width, int? height)
        {
            System.Drawing.Bitmap bmpOut = null;
            const int defaultWidth = 800;
            const int defaultHeight = 600;
            int lnWidth = width == null ? defaultWidth : (int)width;
            int lnHeight = height == null ? defaultHeight : (int)height;
            try
            {
                Bitmap loBMP = new Bitmap(stream);
                ImageFormat loFormat = loBMP.RawFormat;

                decimal lnRatio;
                int lnNewWidth = 0;
                int lnNewHeight = 0;

                //*** If the image is smaller than a thumbnail just return it
                if (loBMP.Width < lnWidth && loBMP.Height < lnHeight)
                {
                    return loBMP;
                }

                if (loBMP.Width > loBMP.Height)
                {
                    lnRatio = (decimal)lnWidth / loBMP.Width;
                    lnNewWidth = lnWidth;
                    decimal lnTemp = loBMP.Height * lnRatio;
                    lnNewHeight = (int)lnTemp;
                }
                else
                {
                    lnRatio = (decimal)lnHeight / loBMP.Height;
                    lnNewHeight = lnHeight;
                    decimal lnTemp = loBMP.Width * lnRatio;
                    lnNewWidth = (int)lnTemp;
                }

                bmpOut = new Bitmap(lnNewWidth, lnNewHeight);
                Graphics g = Graphics.FromImage(bmpOut);
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                g.FillRectangle(Brushes.White, 0, 0, lnNewWidth, lnNewHeight);
                g.DrawImage(loBMP, 0, 0, lnNewWidth, lnNewHeight);
                loBMP.Dispose();
            }
            catch
            {
                return null;
            }
            return bmpOut;
        }

        public static ImageFormat ConvertImageToByteArray(string extension)
        {
            ImageFormat format;
            switch (extension)
            {
                case ".png":
                case ".PNG":
                    format = ImageFormat.Png;
                    break;
                case ".gif":
                case ".GIF":
                    format = ImageFormat.Gif;
                    break;
                case ".bmp":
                case ".BMP":
                    format = ImageFormat.Bmp;
                    break;
                case ".icon":
                    format = ImageFormat.Icon;
                    break;
                case ".jpg":
                case ".JPG":
                case ".jpeg":
                case ".JPEG":
                    format = ImageFormat.Jpeg;
                    break;
                default:
                    format = ImageFormat.Jpeg;
                    break;
            }
            return format;
        }

        #region Encrypt - Decrypt Functions
        public static string DecryptIntranet(string encrypted)
        {
            return Decrypt(encrypted, "1qaz2wsx0okm9ijn");
        }
        public static string EncryptIntranet(string original)
        {
            return Encrypt(original, "1qaz2wsx0okm9ijn");
        }
        public static string Encrypt(string original)
        {
            return Encrypt(original, "!@#$%^&*()~_+|");
        }
        public static string EncryptSimple_Link(string original)
        {
            return Encrypt(original, "!@#$");
        }
        public static string DecryptSimple_Link(string encrypted)
        {
            return Decrypt(encrypted, "!@#$");
        }
        public static string Decrypt(string encrypted)
        {
            return Decrypt(encrypted, "!@#$%^&*()~_+|");
        }
        
        public static string Encrypt(string original, string key)
        {
            TripleDESCryptoServiceProvider objDESProvider;
            MD5CryptoServiceProvider objHashMD5Provider;
            byte[] keyhash;
            byte[] buffer;
            try
            {
                objHashMD5Provider = new MD5CryptoServiceProvider();
                keyhash = objHashMD5Provider.ComputeHash(UnicodeEncoding.Unicode.GetBytes(key));
                objHashMD5Provider = null;

                objDESProvider = new TripleDESCryptoServiceProvider();
                objDESProvider.Key = keyhash;
                objDESProvider.Mode = CipherMode.ECB;

                buffer = UnicodeEncoding.Unicode.GetBytes(original);
                return Convert.ToBase64String(objDESProvider.CreateEncryptor().TransformFinalBlock(buffer, 0, buffer.Length));
            }
            catch
            {
                return string.Empty;
            }
        }

        public static string Decrypt(string encrypted, string key)
        {
            TripleDESCryptoServiceProvider objDESProvider;
            MD5CryptoServiceProvider objHashMD5Provider;
            byte[] keyhash;
            byte[] buffer;

            try
            {
                objHashMD5Provider = new MD5CryptoServiceProvider();
                keyhash = objHashMD5Provider.ComputeHash(UnicodeEncoding.Unicode.GetBytes(key));
                objHashMD5Provider = null;

                objDESProvider = new TripleDESCryptoServiceProvider();
                objDESProvider.Key = keyhash;
                objDESProvider.Mode = CipherMode.ECB;

                buffer = Convert.FromBase64String(encrypted);
                return UnicodeEncoding.Unicode.GetString(objDESProvider.CreateDecryptor().TransformFinalBlock(buffer, 0, buffer.Length));
            }
            catch (Exception ex)
            {
                return string.Empty;
            }
        }

        #endregion

        #region DateTime
        public static DateTime ConvertFormDateTime(string inputDate = "")
        {
            DateTime updateFromDate = DateTime.Now;
            string[] FromDatePostArr = !String.IsNullOrEmpty(inputDate) ? inputDate.Split(' ') : new string[0];
            if (FromDatePostArr.Count() == 2)
            {
                string[] dateOfFrom = FromDatePostArr[1].Split('/');
                if (dateOfFrom.Count() == 3)
                {
                    string[] timeOfFrom = FromDatePostArr[0].Split(':');
                    updateFromDate = new DateTime(Convert.ToInt32(dateOfFrom[2]), Convert.ToInt32(dateOfFrom[1]), Convert.ToInt32(dateOfFrom[0]), Convert.ToInt32(timeOfFrom[0]), Convert.ToInt32(timeOfFrom[1]), 0);
                }
            }
            return updateFromDate;
        }
        public static DateTime ConvertFormDate(string inputDate = "")
        {
            DateTime updateFromDate = DateTime.Now;
            string[] dateOfFrom = !String.IsNullOrEmpty(inputDate) ? inputDate.Split('/') : new string[0];
            if (dateOfFrom.Count() == 3)
            {
                updateFromDate = new DateTime(Convert.ToInt32(dateOfFrom[2]), Convert.ToInt32(dateOfFrom[1]), Convert.ToInt32(dateOfFrom[0]), 0, 0, 0);
            }
            return updateFromDate;
        }
        #endregion
    }
}
