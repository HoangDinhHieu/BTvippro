using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using libSecurity;

namespace BaiTap2_RSA
{
    public partial class Form1 : Form
    {
        private MyRSA rsaService = new MyRSA();
        private byte[] signature1 = null;
        private byte[] signature2 = null;

        public Form1()
        {
            InitializeComponent();
            btnGenKeys_Click(this, EventArgs.Empty);
        }

        // ==================== TAB 1: KHÓA RSA & VIEW PEM ====================
        private void btnGenKeys_Click(object sender, EventArgs e)
        {
            rsaService.GenerateKeys(2048);
            txtPemContent.Text = rsaService.ExportPublicKeyPem();
            btnViewPem_Click(this, EventArgs.Empty);
        }

        private void btnSavePubPem_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog { Filter = "PEM File (*.pem)|*.pem|Text File (*.txt)|*.txt", FileName = "public_key.pem" };
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                File.WriteAllText(sfd.FileName, rsaService.ExportPublicKeyPem());
                MessageBox.Show("Đã lưu Public Key PEM thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnSavePrivPem_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog { Filter = "PEM File (*.pem)|*.pem|Text File (*.txt)|*.txt", FileName = "private_key.pem" };
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                File.WriteAllText(sfd.FileName, rsaService.ExportPrivateKeyPem());
                MessageBox.Show("Đã lưu Private Key PEM thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnImportPem_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog { Filter = "PEM File (*.pem)|*.pem|All Files (*.*)|*.*" };
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                string content = File.ReadAllText(ofd.FileName);
                txtPemContent.Text = content;

                if (content.Contains("PRIVATE"))
                {
                    rsaService.ImportPrivateKeyFromPem(content);
                    MessageBox.Show("Nạp PRIVATE KEY thành công!\nTrích xuất đầy đủ n, e, d, p, q,...", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    rsaService.ImportPublicKeyFromPem(content);
                    MessageBox.Show("Nạp PUBLIC KEY thành công!\nTrích xuất được tham số n và e.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                btnViewPem_Click(this, EventArgs.Empty);
            }
        }

        private void btnViewPem_Click(object sender, EventArgs e)
        {
            try
            {
                string pem = txtPemContent.Text;
                bool isPriv = pem.Contains("PRIVATE");
                string analysis = MyRSA.AnalyzePemBytes(pem, isPriv);
                txtAsn1Log.Text = analysis;
            }
            catch (Exception ex)
            {
                txtAsn1Log.Text = "Lỗi phân tích PEM: " + ex.Message;
            }
        }

        // ==================== TAB 2: MÃ HÓA & GIẢI MÃ ====================
        private void btnGenRandomKey_Click(object sender, EventArgs e)
        {
            byte[] randKey = new byte[32]; // 32 bytes AES-256
            RandomNumberGenerator.Fill(randKey);
            txtDataPlain.Text = Convert.ToBase64String(randKey);
        }

        private void btnEncryptData_Click(object sender, EventArgs e)
        {
            try
            {
                byte[] plainBytes = Encoding.UTF8.GetBytes(txtDataPlain.Text);
                if (plainBytes.Length > 190)
                {
                    MessageBox.Show("Dữ liệu vượt quá 190 bytes!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                byte[] cipherBytes = rsaService.EncryptData(plainBytes);
                string cipherBase64 = Convert.ToBase64String(cipherBytes);
                txtDataCipher.Text = cipherBase64;

                SaveFileDialog sfd = new SaveFileDialog { Filter = "Encrypted File (*.enc)|*.enc", FileName = "encrypted_aes_key.enc" };
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    File.WriteAllText(sfd.FileName, cipherBase64);
                }

                lblCompareResult.Text = "Mã hóa thành công! Kích thước khối mã hóa RSA-2048: 256 bytes (344 ký tự Base64).";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi mã hóa: " + ex.Message);
            }
        }

        private void btnDecryptData_Click(object sender, EventArgs e)
        {
            try
            {
                byte[] cipherBytes = Convert.FromBase64String(txtDataCipher.Text.Trim());
                byte[] decryptedBytes = rsaService.DecryptData(cipherBytes);
                string decryptedText = Encoding.UTF8.GetString(decryptedBytes);
                txtDataDecrypted.Text = decryptedText;

                if (decryptedText == txtDataPlain.Text)
                {
                    lblCompareResult.Text = "🎉 KẾT QUẢ: KHỚP 100% VỚI DỮ LIỆU GỐC! Dữ liệu được bảo toàn nguyên vẹn.";
                    MessageBox.Show("GIẢI MÃ THÀNH CÔNG!\n\nDữ liệu sau giải mã trùng khớp 100% với bản rõ ban đầu.",
                                    "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    lblCompareResult.Text = "CẢNH BÁO: Dữ liệu sau giải mã KHÔNG khớp với dữ liệu gốc!";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi giải mã: " + ex.Message);
            }
        }

        // ==================== TAB 3: BENCHMARK TỐC ĐỘ ====================
        private async void btnRunBenchmark_Click(object sender, EventArgs e)
        {
            btnRunBenchmark.Enabled = false;
            progressBenchmark.Style = ProgressBarStyle.Marquee;
            txtSpeedLog.Text = "Đang thực hiện đo đạc so sánh tốc độ lũy thừa modulo giữa Public Key và Private Key...\r\n";

            int iters = int.Parse(cboIterations.SelectedItem.ToString());

            var (pubMs, privMs) = await Task.Run(() => rsaService.BenchmarkSpeed(iters));

            double ratio = privMs / (pubMs > 0 ? pubMs : 0.0001);

            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"=========================================================================");
            sb.AppendLine($"📊 KẾT QUẢ ĐO ĐẠC TỐC ĐỘ RSA (Số lần lặp: {iters} lần)");
            sb.AppendLine($"=========================================================================");
            sb.AppendLine($"• Thời gian mã hóa trung bình (Public Key e = 65537):  {pubMs:F4} ms / phép tính");
            sb.AppendLine($"• Thời gian giải mã trung bình (Private Key d 2048-bit): {privMs:F4} ms / phép tính");
            sb.AppendLine($"-------------------------------------------------------------------------");
            sb.AppendLine($"👉 TỶ LỆ CHÊNH LỆCH: Private Key chậm hơn Public Key gấp {ratio:F1} LẦN!");
            sb.AppendLine($"=========================================================================\r\n");
            sb.AppendLine("💡 GIẢI THÍCH NGUYÊN LÝ TOÁN HỌC & MẬT MÃ:");
            sb.AppendLine("1. Public Key (n, e): Số mũ e = 65537 (dạng nhị phân: 2^16 + 1 = 17 bit, chỉ có 2 bit '1').");
            sb.AppendLine("   Khi tính m^e mod n bằng thuật toán 'Bình phương và Nhân' (Square-and-Multiply),");
            sb.AppendLine("   CPU chỉ mất đúng 16 phép bình phương và 1 phép nhân số lớn.");
            sb.AppendLine("2. Private Key (n, d): Số mũ bí mật d là số nguyên ngẫu nhiên cực lớn có kích thước 2048-bit.");
            sb.AppendLine("   Khi tính c^d mod n, có xấp xỉ 1024 bit '1', đòi hỏi ~2048 phép bình phương và ~1024 phép nhân số lớn.");
            sb.AppendLine("=> Do đó, thao tác tính toán với Private Key luôn tốn tài nguyên và chậm hơn rất nhiều lần!");

            txtSpeedLog.Text = sb.ToString();
            progressBenchmark.Style = ProgressBarStyle.Blocks;
            btnRunBenchmark.Enabled = true;
        }

        // ==================== TAB 4: SHA-256 AVALANCHE ====================
        private void btnCompareSha_Click(object sender, EventArgs e)
        {
            var (hash1, raw1) = MyRSA.ComputeSHA256(txtShaInput1.Text);
            var (hash2, raw2) = MyRSA.ComputeSHA256(txtShaInput2.Text);
            int diffBits = MyRSA.CountBitDifferences(raw1, raw2);
            double percent = (diffBits / 256.0) * 100.0;

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("❄️ ĐÁNH GIÁ HIỆU ỨNG TUYẾT LỞ (AVALANCHE EFFECT) TRONG SHA-256:");
            sb.AppendLine("=========================================================================");
            sb.AppendLine($"• Bản tin 1: \"{txtShaInput1.Text}\"");
            sb.AppendLine($"  => SHA-256: {hash1}\r\n");
            sb.AppendLine($"• Bản tin 2: \"{txtShaInput2.Text}\"");
            sb.AppendLine($"  => SHA-256: {hash2}\r\n");
            sb.AppendLine("-------------------------------------------------------------------------");
            sb.AppendLine($"📊 KẾT QUẢ ĐO LƯỜNG SỰ THAY ĐỔI:");
            sb.AppendLine($"• Số bit khác biệt: {diffBits} / 256 bits");
            sb.AppendLine($"• Tỷ lệ thay đổi:   {percent:F2}% (Tiêu chuẩn mật mã lý tưởng là ~50%)");
            sb.AppendLine("-------------------------------------------------------------------------");
            sb.AppendLine("💡 KẾT LUẬN:");
            sb.AppendLine("Dù chỉ thay đổi cực nhỏ (1 ký tự cuối), giá trị băm đầu ra hoàn toàn thay đổi");
            sb.AppendLine("ngẫu nhiên xấp xỉ 50% số bit. Điều này chứng minh SHA-256 đáp ứng hoàn hảo");
            sb.AppendLine("tính chất khuếch tán (Diffusion) và chống tìm tiền ảnh (Preimage Resistance).");

            txtShaLog.Text = sb.ToString();
        }

        // ==================== TAB 5: CHỮ KÝ SỐ RSA-PSS ====================
        private void btnSign1_Click(object sender, EventArgs e)
        {
            byte[] data = Encoding.UTF8.GetBytes(txtMsgToSign.Text);
            signature1 = rsaService.SignDataWithPSS(data);
            txtSig1.Text = Convert.ToBase64String(signature1);
            AppendSignLog("Đã ký số LẦN 1 bằng cơ chế RSA-PSS (tự động gắn Salt ngẫu nhiên).");
        }

        private void btnSign2_Click(object sender, EventArgs e)
        {
            byte[] data = Encoding.UTF8.GetBytes(txtMsgToSign.Text);
            signature2 = rsaService.SignDataWithPSS(data);
            txtSig2.Text = Convert.ToBase64String(signature2);
            AppendSignLog("Đã ký số LẦN 2 bằng cơ chế RSA-PSS (tự động gắn Salt ngẫu nhiên).");

            if (signature1 != null && signature2 != null)
            {
                if (txtSig1.Text != txtSig2.Text)
                {
                    AppendSignLog("🎉 KIỂM CHỨNG: Cùng dữ liệu, cùng Private Key nhưng 2 chữ ký hoàn toàn KHÁC NHAU!");
                }
            }
        }

        private void btnVerify1_Click(object sender, EventArgs e)
        {
            if (signature1 == null)
            {
                MessageBox.Show("Chưa có Chữ ký 1 để xác minh!");
                return;
            }
            byte[] data = Encoding.UTF8.GetBytes(txtMsgToSign.Text);
            bool isValid = rsaService.VerifyDataWithPSS(data, signature1);
            AppendSignLog($"Kết quả xác minh Chữ ký 1: {(isValid ? "✅ HỢP LỆ (Valid)" : "❌ KHÔNG HỢP LỆ")}");
            MessageBox.Show(isValid ? "Xác minh Chữ ký 1: HỢP LỆ!" : "Chữ ký không hợp lệ!", "Kết quả");
        }

        private void btnVerify2_Click(object sender, EventArgs e)
        {
            if (signature2 == null)
            {
                MessageBox.Show("Chưa có Chữ ký 2 để xác minh!");
                return;
            }
            byte[] data = Encoding.UTF8.GetBytes(txtMsgToSign.Text);
            bool isValid = rsaService.VerifyDataWithPSS(data, signature2);
            AppendSignLog($"Kết quả xác minh Chữ ký 2: {(isValid ? "✅ HỢP LỆ (Valid)" : "❌ KHÔNG HỢP LỆ")}");
            MessageBox.Show(isValid ? "Xác minh Chữ ký 2: HỢP LỆ!" : "Chữ ký không hợp lệ!", "Kết quả");
        }

        private void AppendSignLog(string msg)
        {
            txtSignLog.AppendText($"[{DateTime.Now:HH:mm:ss}] {msg}\r\n");
        }
    }
}
