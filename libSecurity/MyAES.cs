using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace libSecurity
{
    public class MyAES
    {
        // Khóa AES (256-bit = 32 bytes) và Vector khởi tạo IV (128-bit = 16 bytes)
        public byte[] Key { get; private set; }
        public byte[] IV { get; private set; }

        // 1. Sinh khóa ngẫu nhiên AES-256
        public void GenerateKey()
        {
            using (Aes aes = Aes.Create())
            {
                aes.KeySize = 256;
                aes.GenerateKey();
                aes.GenerateIV();

                Key = aes.Key;
                IV = aes.IV;
            }
        }

        // Lấy khóa và IV dạng chuỗi Base64 để hiển thị
        public string GetKeyBase64() => Key != null ? Convert.ToBase64String(Key) : "";
        public string GetIVBase64() => IV != null ? Convert.ToBase64String(IV) : "";

        // 2. Lưu khóa ra file (định dạng text lưu Key và IV)
        public void SaveKeyToFile(string filePath)
        {
            if (Key == null || IV == null)
                throw new InvalidOperationException("Chưa có khóa để lưu!");

            string content = $"{Convert.ToBase64String(Key)}\n{Convert.ToBase64String(IV)}";
            File.WriteAllText(filePath, content);
        }

        // 3. Import khóa từ file
        public void LoadKeyFromFile(string filePath)
        {
            string[] lines = File.ReadAllLines(filePath);
            if (lines.Length < 2)
                throw new FormatException("File khóa không hợp lệ (phải gồm 2 dòng: Key và IV)!");

            Key = Convert.FromBase64String(lines[0].Trim());
            IV = Convert.FromBase64String(lines[1].Trim());
        }

        // Nạp trực tiếp từ chuỗi Base64
        public void SetKeyAndIV(string keyBase64, string ivBase64)
        {
            Key = Convert.FromBase64String(keyBase64.Trim());
            IV = Convert.FromBase64String(ivBase64.Trim());
        }

        // 4. Mã hóa đoạn văn bản -> Chuỗi Base64
        public string EncryptText(string plainText)
        {
            if (Key == null || IV == null) throw new InvalidOperationException("Chưa thiết lập khóa AES!");

            using (Aes aes = Aes.Create())
            {
                aes.Key = Key;
                aes.IV = IV;
                using (ICryptoTransform encryptor = aes.CreateEncryptor())
                {
                    byte[] plainBytes = Encoding.UTF8.GetBytes(plainText);
                    byte[] cipherBytes = encryptor.TransformFinalBlock(plainBytes, 0, plainBytes.Length);
                    return Convert.ToBase64String(cipherBytes);
                }
            }
        }

        // 5. Giải mã chuỗi Base64 -> Văn bản gốc
        public string DecryptText(string cipherBase64)
        {
            if (Key == null || IV == null) throw new InvalidOperationException("Chưa thiết lập khóa AES!");

            using (Aes aes = Aes.Create())
            {
                aes.Key = Key;
                aes.IV = IV;
                using (ICryptoTransform decryptor = aes.CreateDecryptor())
                {
                    byte[] cipherBytes = Convert.FromBase64String(cipherBase64);
                    byte[] plainBytes = decryptor.TransformFinalBlock(cipherBytes, 0, cipherBytes.Length);
                    return Encoding.UTF8.GetString(plainBytes);
                }
            }
        }

        // 6. Mã hóa File lớn (hàng GB) theo luồng Buffer không tràn RAM
        public void EncryptFile(string inputFilePath, string outputFilePath, Action<int> progressCallback = null)
        {
            if (Key == null || IV == null) throw new InvalidOperationException("Chưa thiết lập khóa AES!");

            long totalBytes = new FileInfo(inputFilePath).Length;
            long processedBytes = 0;

            using (Aes aes = Aes.Create())
            {
                aes.Key = Key;
                aes.IV = IV;

                using (FileStream fsIn = new FileStream(inputFilePath, FileMode.Open, FileAccess.Read, FileShare.Read))
                using (FileStream fsOut = new FileStream(outputFilePath, FileMode.Create, FileAccess.Write))
                using (ICryptoTransform encryptor = aes.CreateEncryptor())
                using (CryptoStream cs = new CryptoStream(fsOut, encryptor, CryptoStreamMode.Write))
                {
                    byte[] buffer = new byte[1024 * 1024]; // 1MB buffer tối ưu tốc độ đọc ghi
                    int bytesRead;
                    while ((bytesRead = fsIn.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        cs.Write(buffer, 0, bytesRead);
                        processedBytes += bytesRead;
                        progressCallback?.Invoke((int)((processedBytes * 100) / totalBytes));
                    }
                }
            }
        }

        // 7. Giải mã File lớn theo luồng Buffer
        public void DecryptFile(string inputFilePath, string outputFilePath, Action<int> progressCallback = null)
        {
            if (Key == null || IV == null) throw new InvalidOperationException("Chưa thiết lập khóa AES!");

            long totalBytes = new FileInfo(inputFilePath).Length;
            long processedBytes = 0;

            using (Aes aes = Aes.Create())
            {
                aes.Key = Key;
                aes.IV = IV;

                using (FileStream fsIn = new FileStream(inputFilePath, FileMode.Open, FileAccess.Read, FileShare.Read))
                using (FileStream fsOut = new FileStream(outputFilePath, FileMode.Create, FileAccess.Write))
                using (ICryptoTransform decryptor = aes.CreateDecryptor())
                using (CryptoStream cs = new CryptoStream(fsIn, decryptor, CryptoStreamMode.Read))
                {
                    byte[] buffer = new byte[1024 * 1024]; // 1MB buffer
                    int bytesRead;
                    while ((bytesRead = cs.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        fsOut.Write(buffer, 0, bytesRead);
                        processedBytes += bytesRead;
                        if (totalBytes > 0)
                            progressCallback?.Invoke((int)Math.Min(100, (processedBytes * 100) / totalBytes));
                    }
                }
            }
        }

        // 8. Hàm băm SHA-256 để so sánh kiểm tra tính toàn vẹn 100% giữa 2 file (theo byte)
        public static string ComputeFileHash(string filePath)
        {
            using (SHA256 sha = SHA256.Create())
            using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                byte[] hash = sha.ComputeHash(fs);
                return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
            }
        }
    }
}
