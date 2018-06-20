using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Entities.Security
{
    public static class Cryptor
    {
        #region Key
        const string KEY = "ksljhndfjkosfkl8werut89eut89eDSD";
        const string IV = "34345(*(*&";
        #endregion

        public static dynamic Hash(string RawData,byte[] Salt)
        {
            int MaxSaltlength=16;
            int MinSaltlength=4;
            byte[] SaltBytes = null;
            if (Salt != null)
            {
                SaltBytes = Salt;
            }
            else
            {
                Random random = new Random();
                int Saltlength = random.Next(MinSaltlength, MaxSaltlength);
                SaltBytes = new byte[Saltlength];
                RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
                rng.GetNonZeroBytes(SaltBytes);
                rng.Dispose();

            }
            byte[] PlainData = ASCIIEncoding.UTF8.GetBytes(RawData);
            byte[] DataAndSalt = new byte[PlainData.Length + SaltBytes.Length];
            for (int i = 0; i < PlainData.Length; i++)
            {
                DataAndSalt[i] = PlainData[i];
            }
            for (int j = 0; j < SaltBytes.Length; j++)
            {
                DataAndSalt[PlainData.Length+j] = SaltBytes[j];
            }
            SHA256Managed sha256 = new SHA256Managed();
            byte[] HashValue = sha256.ComputeHash(DataAndSalt);
            dynamic result = new ExpandoObject();
            result.Hash = Convert.ToBase64String(HashValue);
            result.Salt= Convert.ToBase64String(SaltBytes);
            return result;
        }

       
    }
}
