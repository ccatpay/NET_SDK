using System;
using System.Security.Cryptography;
using System.Text;

namespace CCatPay_Net
{
    public class SecurityProcess
    {
        #region MD5編碼方法
        /// <summary>
        /// MD5編碼方法
        /// </summary>
        /// <param name="input"></param>
        /// <param name="isTolower"></param>
        /// <returns></returns>
        public string GetMd5Hash(string input, bool isTolower = true)
        {
            //比照舊系統，先轉小寫再編碼
            if (isTolower)
                input = input.ToLower();

            MD5 md5Hash = MD5.Create();

            // Convert the input string to a byte array and compute the hash.
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data 
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Return the hexadecimal string.
            return sBuilder.ToString();
        }
        #endregion

        #region 產生隨機數字
        /// <summary>
        /// 產生隨機數字
        /// </summary>
        /// <param name="Length"></param>
        /// <returns></returns>
        public string CreateRandomNumber(int Length)
        {
            string newRandomNumber = "";
            string number = "0123456789";
            Random r = new Random(DateTime.UtcNow.AddHours(8).Millisecond);
            for (int i = 0; i < Length; i++)
            {
                newRandomNumber += number[r.Next(0, number.Length)];
            }
            return newRandomNumber;
        }
        #endregion
    }
}