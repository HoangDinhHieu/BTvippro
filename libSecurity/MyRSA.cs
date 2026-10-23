using System;
using System.Diagnostics;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace libSecurity
{
    public class MyRSA
    {
        public RSA RsaProvider { get; private set; }
        public RSAParameters CurrentParameters { get; private set; }

        public MyRSA()
        {
            RsaProvider = RSA.Create(2048);
            CurrentParameters = RsaProvider.ExportParameters(true);
        }

        // 1. Sinh cặp khóa mới (2048-bit)
        public void GenerateKeys(int keySize = 2048)
        {
            RsaProvider?.Dispose();
            RsaProvider = RSA.Create(keySize);
            CurrentParameters = RsaProvider.ExportParameters(true);
        }

        // 2. Xuất khóa ra chuỗi định dạng PEM
        public string ExportPublicKeyPem() => RsaProvider.ExportRSAPublicKeyPem();
        public string ExportPrivateKeyPem() => RsaProvider.ExportRSAPrivateKeyPem();

        public void SaveKeysToPemFiles(string pubPath, string privPath)
        {
            File.WriteAllText(pubPath, ExportPublicKeyPem());
            File.WriteAllText(privPath, ExportPrivateKeyPem());
        }

        // 3. Nạp khóa từ file PEM
        public void ImportPublicKeyFromPem(string pemContent)
        {
            RsaProvider.ImportFromPem(pemContent);
            CurrentParameters = RsaProvider.ExportParameters(false);
        }

        public void ImportPrivateKeyFromPem(string pemContent)
        {
            RsaProvider.ImportFromPem(pemContent);
            CurrentParameters = RsaProvider.ExportParameters(true);
        }

        // 4. Bóc tách và hiển thị ý nghĩa các byte ASN.1 / DER từ PEM
        public static string AnalyzePemBytes(string pemContent, bool isPrivate)
        {
            StringBuilder sb = new StringBuilder();
            string base64 = pemContent
                .Replace("-----BEGIN RSA PUBLIC KEY-----", "")
                .Replace("-----END RSA PUBLIC KEY-----", "")
                .Replace("-----BEGIN PUBLIC KEY-----", "")
                .Replace("-----END PUBLIC KEY-----", "")
                .Replace("-----BEGIN RSA PRIVATE KEY-----", "")
                .Replace("-----END RSA PRIVATE KEY-----", "")
                .Replace("-----BEGIN PRIVATE KEY-----", "")
                .Replace("-----END PRIVATE KEY-----", "")
                .Replace("\r", "").Replace("\n", "").Trim();

            byte[] bytes = Convert.FromBase64String(base64);
            sb.AppendLine($"📊 Tổng kích thước phần thân (Body): {bytes.Length} bytes\r\n");
            sb.AppendLine("🔍 BẢNG HIỂN THỊ DỮ LIỆU HEX (16 byte/dòng):");
            sb.AppendLine("----------------------------------------------------------------");

            for (int i = 0; i < bytes.Length; i++)
            {
                sb.Append($"{bytes[i]:X2} ");
                if ((i + 1) % 16 == 0) sb.AppendLine();
            }
            if (bytes.Length % 16 != 0) sb.AppendLine();

            sb.AppendLine("----------------------------------------------------------------");
            sb.AppendLine("\r\n💡 GIẢI THÍCH Ý NGHĨA CÁC BYTE ĐỊNH DẠNG ASN.1 / DER:");

            if (isPrivate)
            {
                sb.AppendLine("• 0x30 ... : Thẻ SEQUENCE (cấu trúc danh sách các phần tử ASN.1)");
                sb.AppendLine("• 0x02 0x01 0x00 : INTEGER (Version = 0)");
                sb.AppendLine("• 0x02 ... : Modulus n (khóa công khai chung, thường 256 bytes cho RSA 2048-bit)");
                sb.AppendLine("• 0x02 0x03 0x01 0x00 0x01 : Public Exponent e (mũ công khai e = 65537 - 3 bytes)");
                sb.AppendLine("• 0x02 ... : Private Exponent d (mũ bí mật d, kích thước ~256 bytes)");
                sb.AppendLine("• 0x02 ... : Prime 1 (Số nguyên tố p, ~128 bytes)");
                sb.AppendLine("• 0x02 ... : Prime 2 (Số nguyên tố q, ~128 bytes)");
                sb.AppendLine("• 0x02 ... : Exponent 1 (dp = d mod (p-1))");
                sb.AppendLine("• 0x02 ... : Exponent 2 (dq = d mod (q-1))");
                sb.AppendLine("• 0x02 ... : Coefficient (iq = q^-1 mod p - tối ưu thuật toán phần dư Trung Hoa CRT)");
            }
            else
            {
                sb.AppendLine("• 0x30 ... : Thẻ SEQUENCE (danh sách)");
                sb.AppendLine("• 0x02 ... : Modulus n (256 bytes cho RSA 2048-bit)");
                sb.AppendLine("• 0x02 0x03 0x01 0x00 0x01 : Public Exponent e (giá trị 65537 / 0x010001)");
            }

            return sb.ToString();
        }

        // 5. Mã hóa dữ liệu nhỏ (<= 190 byte) bằng Public Key
        public byte[] EncryptData(byte[] plainData)
        {
            if (plainData.Length > 190)
                throw new ArgumentException("Dữ liệu RSA 2048-bit với đệm OAEP không được vượt quá 190 bytes!");

            return RsaProvider.Encrypt(plainData, RSAEncryptionPadding.OaepSHA256);
        }

        // 6. Giải mã dữ liệu bằng Private Key
        public byte[] DecryptData(byte[] cipherData)
        {
            return RsaProvider.Decrypt(cipherData, RSAEncryptionPadding.OaepSHA256);
        }

        // 7. Đo và so sánh tốc độ xử lý giữa Public Key và Private Key
        public (double pubMs, double privMs) BenchmarkSpeed(int iterations = 200)
        {
            byte[] testData = new byte[32]; // Dữ liệu 32 bytes (chuẩn khóa AES-256)
            RandomNumberGenerator.Fill(testData);

            // Đo mã hóa bằng Public Key (m^e mod n, e nhỏ = 65537)
            Stopwatch swPub = Stopwatch.StartNew();
            for (int i = 0; i < iterations; i++)
            {
                _ = RsaProvider.Encrypt(testData, RSAEncryptionPadding.OaepSHA256);
            }
            swPub.Stop();

            byte[] cipher = RsaProvider.Encrypt(testData, RSAEncryptionPadding.OaepSHA256);

            // Đo giải mã bằng Private Key (c^d mod n, d cực lớn ~2048-bit)
            Stopwatch swPriv = Stopwatch.StartNew();
            for (int i = 0; i < iterations; i++)
            {
                _ = RsaProvider.Decrypt(cipher, RSAEncryptionPadding.OaepSHA256);
            }
            swPriv.Stop();

            return (swPub.Elapsed.TotalMilliseconds / iterations, swPriv.Elapsed.TotalMilliseconds / iterations);
        }

        // 8. Băm SHA-256
        public static (string hexHash, byte[] rawBytes) ComputeSHA256(string input)
        {
            using (SHA256 sha = SHA256.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(input);
                byte[] hash = sha.ComputeHash(bytes);
                return (BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant(), hash);
            }
        }

        // Đếm số bit khác nhau giữa 2 chuỗi băm (Đo hiệu ứng Tuyết lở)
        public static int CountBitDifferences(byte[] hash1, byte[] hash2)
        {
            int diffBits = 0;
            for (int i = 0; i < hash1.Length; i++)
            {
                byte xor = (byte)(hash1[i] ^ hash2[i]);
                for (int b = 0; b < 8; b++)
                {
                    if ((xor & (1 << b)) != 0) diffBits++;
                }
            }
            return diffBits;
        }

        // 9. Chữ ký số ngẫu nhiên hóa bằng RSA-PSS (Đáp ứng yêu cầu MONG MUỐN)
        public byte[] SignDataWithPSS(byte[] data)
        {
            // RSASignaturePadding.Pss tự động thêm muối ngẫu nhiên (Salt) cho mỗi lần ký
            return RsaProvider.SignData(data, HashAlgorithmName.SHA256, RSASignaturePadding.Pss);
        }

        public bool VerifyDataWithPSS(byte[] data, byte[] signature)
        {
            return RsaProvider.VerifyData(data, signature, HashAlgorithmName.SHA256, RSASignaturePadding.Pss);
        }
    }
}
