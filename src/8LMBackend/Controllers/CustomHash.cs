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
        public string GetHashedString(string Price, string OrderID)
        {
            string LoginID = "9c6x9RG4Pt";
            string SignatureKey = "6495FCD5132C75E7AFB8EA01033E2542921B809E97297D692A977C1F4AAB47C046D00DA2E0FC0FE31A683FCEE95FC60402ACC83851BB8A0744D5F4C72B3050B7";
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
