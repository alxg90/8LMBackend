using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace _8LMCore.Controllers
{
    public class CustomHash
    {
        public string GetHashedString(string Price, string OrderID, string LoginID, string SignatureKey)
        {
            string body = LoginID + "^" + OrderID + "^" + ConvertToUnixTimestamp(DateTime.UtcNow).ToString() + "^" + Price + "^";
            return GenerateSHA512String(body, SignatureKey);
        }

        static byte[] HexToByte(string hex)
        {
            return Enumerable.Range(0, hex.Length)
                             .Where(x => x % 2 == 0)
                             .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
                             .ToArray();
        }

        static string ByteArrayToHexString(byte[] data)
        {
            return BitConverter.ToString(data).Replace("-", string.Empty);
        }

        public double ConvertToUnixTimestamp(DateTime date)
        {
            DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            TimeSpan diff = date.ToUniversalTime() - origin;
            return Math.Floor(diff.TotalSeconds);
        }

        public string GenerateSHA512String(string message, string key)
        {
            byte[] hash = new HMACSHA512(HexToByte(key)).ComputeHash(Encoding.UTF8.GetBytes(message));
            return ByteArrayToHexString(hash);
        }
    }
}
