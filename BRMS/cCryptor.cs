using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace BRMS
{
    class cCryptor
    {
        private readonly byte[] key;
        private readonly byte[] iv;

        public cCryptor(string passphrase)
        {
            using (var sha256 = SHA256.Create())
            {
                key = sha256.ComputeHash(Encoding.UTF8.GetBytes(passphrase)); // AES 키를 생성
                iv = key.Take(16).ToArray(); // IV는 128비트 (16바이트)로 설정
            }
        }

        public string Encrypt(string plainText)
        {
            using (var aes = new AesManaged())
            {
                aes.Key = key;
                aes.IV = iv;
                aes.Mode = CipherMode.CBC;
                aes.Padding = PaddingMode.PKCS7;

                using (var encryptor = aes.CreateEncryptor(aes.Key, aes.IV))
                using (var ms = new MemoryStream())
                using (var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                using (var sw = new StreamWriter(cs))
                {
                    sw.Write(plainText);
                    sw.Flush();
                    cs.FlushFinalBlock();
                    return Convert.ToBase64String(ms.ToArray());
                }
            }
        }

        public string Decrypt(string cipherText)
        {
            using (var aes = new AesManaged())
            {
                aes.Key = key;
                aes.IV = iv;
                aes.Mode = CipherMode.CBC;
                aes.Padding = PaddingMode.PKCS7;

                using (var decryptor = aes.CreateDecryptor(aes.Key, aes.IV))
                using (var ms = new MemoryStream(Convert.FromBase64String(cipherText)))
                using (var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                using (var sr = new StreamReader(cs))
                {
                    return sr.ReadToEnd();
                }
            }
        }

       
        public (string MaskedPhone, string keyValue) NumberEncrypt(string number)
        {
            string keyValue;
            int charIndex = number.Replace("-", "").Length - 6;
            string cryptorNumber = number.Replace("-", "").Substring(charIndex, 2);
            keyValue = Encrypt(cryptorNumber);
            string replaceNumber = ReplaceNumber(number);
            string maskedPhone = RestoreNumber(number, replaceNumber);
            return (maskedPhone, keyValue);
        }
        
        public string NumberDecrypt(string number,string key)
        {
            
            string decryptNumber = Decrypt(key);
            string result = number.Replace("-", "");
            result = result.Replace("**", decryptNumber);
            result = RestoreNumber(number, result);
            return result;
        }
       

        private string ReplaceNumber(string Number)
        {
            //string replaceNumber;
            // 숫자만 추출 (하이픈 제외)
            string plainNumber = new string(Number.Where(char.IsDigit).ToArray());

            // 뒤에서 2, 3번째 숫자 추출
            int secondLastIndex = plainNumber.Length - 6;
            int thirdLastIndex = plainNumber.Length - 5;

            char secondLastChar = plainNumber[secondLastIndex];
            char thirdLastChar = plainNumber[thirdLastIndex];
            char[] maskedArray = plainNumber.ToCharArray();
            maskedArray[thirdLastIndex] = '*';
            maskedArray[secondLastIndex] = '*';

            return new string(maskedArray);
        }
        private string RestoreNumber(string origenalNumber, string cryptorNumber)
        {
            string result = "";
            if (origenalNumber.Length == cryptorNumber.Length)
            {
                return result = cryptorNumber;
            }
            else if (!origenalNumber.Contains("-"))
            {
                return result = cryptorNumber;
            }
            else
            {
                int cryptorIndex = 0;  // 암호화된 번호의 인덱스
                for (int i = 0; i < origenalNumber.Length; i++)
                {
                    // 원본 번호의 문자와 하이픈을 유지하며 result에 추가
                    string word = origenalNumber.Substring(i, 1);
                    if (word == "-")
                    {
                        result += "-";  // 원본 번호에서 하이픈을 그대로 추가
                    }
                    else
                    {
                        result += cryptorNumber[cryptorIndex];  // 암호화된 번호에서 해당 위치 문자 추가
                        cryptorIndex++;
                    }
                }
                return result;  // 복원된 전화번호 반환
            }
        }
        private string ReplaceDigitsWithMask(string original, char[] maskedDigits)
        {
            StringBuilder result = new StringBuilder();
            int digitIndex = 0;

            foreach (char ch in original)
            {
                if (char.IsDigit(ch))
                {
                    result.Append(maskedDigits[digitIndex++]);
                }
                else
                {
                    result.Append(ch); // 하이픈 또는 다른 문자는 그대로 유지
                }
            }

            return result.ToString();
        }
    }
}

