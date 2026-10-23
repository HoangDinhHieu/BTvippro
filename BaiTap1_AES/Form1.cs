using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using libSecurity;

namespace BaiTap1_AES
{
    public partial class Form1 : Form
    {
        private MyAES aesService = new MyAES();
        private string encryptedFilePath = "";
        private string decryptedFilePath = "";

        public Form1()
        {
            InitializeComponent();
        }

        // 1. Sinh khóa mới
        private void btnGenKey_Click(object sender, EventArgs e)
        {
            aesService.GenerateKey();
            txtKey.Text = aesService.GetKeyBase64();
            txtIV.Text = aesService.GetIVBase64();
            AppendLog("Đã sinh khóa AES 256-bit và IV 128-bit mới ngẫu nhiên thành công!");
        }

        // 2. Lưu khóa ra file
        private void btnSaveKey_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog sfd = new SaveFileDialog
                {
                    Filter = "Key File (*.key)|*.key|Text File (*.txt)|*.txt",
                    FileName = "aes_key.key"
                };
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    aesService.SaveKeyToFile(sfd.FileName);
                    MessageBox.Show("Lưu khóa thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    AppendLog($"Lưu khóa tại: {sfd.FileName}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi lưu khóa: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // 3. Nạp khóa từ file
        private void btnLoadKey_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog ofd = new OpenFileDialog
                {
                    Filter = "Key File (*.key)|*.key|Text File (*.txt)|*.txt|All Files (*.*)|*.*"
                };
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    aesService.LoadKeyFromFile(ofd.FileName);
                    txtKey.Text = aesService.GetKeyBase64();
                    txtIV.Text = aesService.GetIVBase64();
                    MessageBox.Show("Nạp khóa thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    AppendLog($"Nạp khóa từ: {ofd.FileName}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi nạp khóa: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SyncKeyFromInputs()
        {
            if (string.IsNullOrWhiteSpace(txtKey.Text) || string.IsNullOrWhiteSpace(txtIV.Text))
            {
                btnGenKey_Click(this, EventArgs.Empty);
            }
            else
            {
                aesService.SetKeyAndIV(txtKey.Text, txtIV.Text);
            }
        }

        // 4. Mã hóa văn bản
        private void btnEncryptText_Click(object sender, EventArgs e)
        {
            try
            {
                SyncKeyFromInputs();
                string plain = txtPlainText.Text;
                txtCipherText.Text = aesService.EncryptText(plain);
                AppendLog("Mã hóa văn bản thành công -> Kết quả Base64 hiển thị trên màn hình.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi mã hóa văn bản: " + ex.Message);
            }
        }

        // 5. Giải mã văn bản
        private void btnDecryptText_Click(object sender, EventArgs e)
        {
            try
            {
                SyncKeyFromInputs();
                string cipher = txtCipherText.Text;
                txtDecryptedText.Text = aesService.DecryptText(cipher);
                AppendLog("Giải mã văn bản thành công!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi giải mã văn bản: " + ex.Message);
            }
        }

        // 6. Chọn file nguồn
        private void btnBrowseSource_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog { Filter = "All Files (*.*)|*.*" };
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                txtSourceFile.Text = ofd.FileName;
                FileInfo fi = new FileInfo(ofd.FileName);
                AppendLog($"Đã chọn file: {fi.FullName} | Kích thước: {fi.Length / (1024.0 * 1024.0):F2} MB ({fi.Length:N0} bytes)");
            }
        }

        // 7. Mã hóa File (hỗ trợ hàng GB, đo thời gian & tốc độ)
        private async void btnEncryptFile_Click(object sender, EventArgs e)
        {
            if (!File.Exists(txtSourceFile.Text))
            {
                MessageBox.Show("Vui lòng chọn file nguồn hợp lệ!");
                return;
            }

            SyncKeyFromInputs();
            string sourceFile = txtSourceFile.Text;
            encryptedFilePath = sourceFile + ".aes";

            btnEncryptFile.Enabled = false;
            btnDecryptFile.Enabled = false;
            progressBar.Value = 0;
            lblStatus.Text = "Đang mã hóa file...";

            Stopwatch sw = Stopwatch.StartNew();
            long fileBytes = new FileInfo(sourceFile).Length;

            await Task.Run(() =>
            {
                aesService.EncryptFile(sourceFile, encryptedFilePath, percent =>
                {
                    this.Invoke(new Action(() => progressBar.Value = Math.Min(100, percent)));
                });
            });

            sw.Stop();
            double seconds = sw.Elapsed.TotalSeconds;
            double speedMB = (fileBytes / (1024.0 * 1024.0)) / (seconds > 0 ? seconds : 0.001);

            lblStatus.Text = $"Mã hóa hoàn tất trong {seconds:F2}s ({speedMB:F2} MB/s)";
            AppendLog($"[MÃ HÓA FILE] -> File đích: {encryptedFilePath}");
            AppendLog($"  - Thời gian: {seconds:F3} giây | Tốc độ: {speedMB:F2} MB/s");

            btnEncryptFile.Enabled = true;
            btnDecryptFile.Enabled = true;
        }

        // 8. Giải mã File
        private async void btnDecryptFile_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(encryptedFilePath) || !File.Exists(encryptedFilePath))
            {
                OpenFileDialog ofd = new OpenFileDialog { Filter = "Encrypted Files (*.aes)|*.aes|All Files (*.*)|*.*" };
                if (ofd.ShowDialog() == DialogResult.OK)
                    encryptedFilePath = ofd.FileName;
                else
                    return;
            }

            SyncKeyFromInputs();
            decryptedFilePath = encryptedFilePath.EndsWith(".aes")
                ? encryptedFilePath.Substring(0, encryptedFilePath.Length - 4) + "_decrypted" + Path.GetExtension(txtSourceFile.Text)
                : encryptedFilePath + "_decrypted";

            btnEncryptFile.Enabled = false;
            btnDecryptFile.Enabled = false;
            progressBar.Value = 0;
            lblStatus.Text = "Đang giải mã file...";

            Stopwatch sw = Stopwatch.StartNew();
            long fileBytes = new FileInfo(encryptedFilePath).Length;

            await Task.Run(() =>
            {
                aesService.DecryptFile(encryptedFilePath, decryptedFilePath, percent =>
                {
                    this.Invoke(new Action(() => progressBar.Value = Math.Min(100, percent)));
                });
            });

            sw.Stop();
            double seconds = sw.Elapsed.TotalSeconds;
            double speedMB = (fileBytes / (1024.0 * 1024.0)) / (seconds > 0 ? seconds : 0.001);

            lblStatus.Text = $"Giải mã hoàn tất trong {seconds:F2}s ({speedMB:F2} MB/s)";
            AppendLog($"[GIẢI MÃ FILE] -> File đích: {decryptedFilePath}");
            AppendLog($"  - Thời gian: {seconds:F3} giây | Tốc độ: {speedMB:F2} MB/s");

            btnEncryptFile.Enabled = true;
            btnDecryptFile.Enabled = true;
        }

        // 9. So sánh theo byte giữa file nguồn và file giải mã
        private async void btnCompare_Click(object sender, EventArgs e)
        {
            string source = txtSourceFile.Text;
            if (!File.Exists(source) || !File.Exists(decryptedFilePath))
            {
                MessageBox.Show("Chưa có đủ file nguồn hoặc file sau giải mã để so sánh!", "Thông báo");
                return;
            }

            lblStatus.Text = "Đang tính mã băm SHA-256 so sánh theo byte giữa 2 file...";
            AppendLog("Bắt đầu kiểm tra tính toàn vẹn dữ liệu (Byte-by-Byte via SHA-256)...");

            string hashSource = "";
            string hashDecrypted = "";

            await Task.Run(() =>
            {
                hashSource = MyAES.ComputeFileHash(source);
                hashDecrypted = MyAES.ComputeFileHash(decryptedFilePath);
            });

            long size1 = new FileInfo(source).Length;
            long size2 = new FileInfo(decryptedFilePath).Length;

            AppendLog($"File nguồn ({size1:N0} bytes) SHA-256: {hashSource}");
            AppendLog($"File giải mã ({size2:N0} bytes) SHA-256: {hashDecrypted}");

            if (hashSource == hashDecrypted && size1 == size2)
            {
                lblStatus.Text = "KẾT LUẬN: Khớp 100% từng byte! Dữ liệu được bảo toàn toàn vẹn.";
                MessageBox.Show("DỮ LIỆU KHỚP 100% TỪNG BYTE!\n\nFile nguồn và file giải mã hoàn toàn trùng khớp.",
                                "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                lblStatus.Text = "CẢNH BÁO: File nguồn và file giải mã khác nhau!";
                MessageBox.Show("Có sự sai lệch dữ liệu!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void AppendLog(string message)
        {
            txtLog.AppendText($"[{DateTime.Now:HH:mm:ss}] {message}\r\n");
        }
    }
}
