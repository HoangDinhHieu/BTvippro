using System.Drawing;
using System.Windows.Forms;

namespace BaiTap2_RSA
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();

            // Colors Hello Kitty Theme
            Color hkPinkLight = Color.FromArgb(255, 240, 245); // Nền hồng nhạt LavenderBlush
            Color hkPinkCard = Color.FromArgb(255, 245, 248);  // Hồng phấn dịu
            Color hkPinkPrimary = Color.FromArgb(233, 30, 99); // Hồng đậm Hello Kitty
            Color hkPinkDark = Color.FromArgb(173, 20, 87);    // Hồng viền đậm
            Color hkRedRibbon = Color.FromArgb(229, 57, 53);   // Đỏ nơ Hello Kitty
            Font fontTitle = new Font("Segoe UI", 12F, FontStyle.Bold);
            Font fontHeader = new Font("Segoe UI", 10F, FontStyle.Bold);
            Font fontBold = new Font("Segoe UI", 9F, FontStyle.Bold);
            Font fontRegular = new Font("Segoe UI", 9F, FontStyle.Regular);
            Font fontCode = new Font("Consolas", 8.5F, FontStyle.Regular);

            // Controls Header
            this.panelHeader = new Panel();
            this.lblHeaderTitle = new Label();
            this.lblHeaderSubtitle = new Label();
            this.picHelloKitty = new PictureBox();

            // TabControl
            this.tabControlMain = new TabControl();
            this.tabKeys = new TabPage();
            this.tabEncrypt = new TabPage();
            this.tabSpeed = new TabPage();
            this.tabSha = new TabPage();
            this.tabSign = new TabPage();

            // Tab 1: Keys & PEM
            this.btnGenKeys = new Button();
            this.btnSavePubPem = new Button();
            this.btnSavePrivPem = new Button();
            this.btnImportPem = new Button();
            this.btnViewPem = new Button();
            this.lblPem = new Label();
            this.txtPemContent = new TextBox();
            this.lblAsn1 = new Label();
            this.txtAsn1Log = new TextBox();

            // Tab 2: Encrypt / Decrypt AES Key
            this.lblDataPlain = new Label();
            this.txtDataPlain = new TextBox();
            this.btnGenRandomKey = new Button();
            this.btnEncryptData = new Button();
            this.btnDecryptData = new Button();
            this.lblCipher = new Label();
            this.txtDataCipher = new TextBox();
            this.lblDecrypted = new Label();
            this.txtDataDecrypted = new TextBox();
            this.lblCompareResult = new Label();

            // Tab 3: Benchmark Speed
            this.lblIter = new Label();
            this.cboIterations = new ComboBox();
            this.btnRunBenchmark = new Button();
            this.progressBenchmark = new ProgressBar();
            this.txtSpeedLog = new TextBox();

            // Tab 4: SHA-256 Avalanche
            this.lblSha1 = new Label();
            this.txtShaInput1 = new TextBox();
            this.lblSha2 = new Label();
            this.txtShaInput2 = new TextBox();
            this.btnCompareSha = new Button();
            this.txtShaLog = new TextBox();

            // Tab 5: Digital Signature PSS
            this.lblMsg = new Label();
            this.txtMsgToSign = new TextBox();
            this.btnSign1 = new Button();
            this.btnSign2 = new Button();
            this.btnVerify1 = new Button();
            this.btnVerify2 = new Button();
            this.lblSig1 = new Label();
            this.txtSig1 = new TextBox();
            this.lblSig2 = new Label();
            this.txtSig2 = new TextBox();
            this.txtSignLog = new TextBox();

            this.SuspendLayout();

            // ==================== PANEL HEADER ====================
            this.panelHeader.BackColor = Color.White;
            this.panelHeader.Location = new Point(0, 0);
            this.panelHeader.Size = new Size(960, 85);

            this.lblHeaderTitle.Text = "🎀 BÀI 2: MÃ HÓA BẤT ĐỐI XỨNG RSA & CHỮ KÝ SỐ";
            this.lblHeaderTitle.Font = fontTitle;
            this.lblHeaderTitle.ForeColor = hkPinkDark;
            this.lblHeaderTitle.Location = new Point(20, 15);
            this.lblHeaderTitle.Size = new Size(750, 30);

            this.lblHeaderSubtitle.Text = "Sinh viên: Hoàng Đình Hiếu | Giao diện Hello Kitty Pastel";
            this.lblHeaderSubtitle.Font = fontRegular;
            this.lblHeaderSubtitle.ForeColor = Color.FromArgb(120, 120, 120);
            this.lblHeaderSubtitle.Location = new Point(22, 45);
            this.lblHeaderSubtitle.Size = new Size(600, 25);

            this.picHelloKitty.Location = new Point(840, 5);
            this.picHelloKitty.Size = new Size(95, 75);
            this.picHelloKitty.SizeMode = PictureBoxSizeMode.Zoom;
            this.picHelloKitty.BackColor = Color.Transparent;
            try
            {
                string imgPath = System.IO.Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "hello_kitty.jpg");
                if (!System.IO.File.Exists(imgPath)) imgPath = "hello_kitty.jpg";
                if (System.IO.File.Exists(imgPath))
                    this.picHelloKitty.Image = Image.FromFile(imgPath);
            }
            catch { }

            this.panelHeader.Controls.AddRange(new Control[] {
                this.lblHeaderTitle, this.lblHeaderSubtitle, this.picHelloKitty
            });

            // ==================== TAB CONTROL ====================
            this.tabControlMain.Location = new Point(15, 95);
            this.tabControlMain.Size = new Size(930, 570);
            this.tabControlMain.Font = fontBold;
            this.tabControlMain.Controls.AddRange(new Control[] {
                this.tabKeys, this.tabEncrypt, this.tabSpeed, this.tabSha, this.tabSign
            });

            // ---------------- TAB 1: KEYS & PEM ----------------
            this.tabKeys.Text = "🎀 1. Khóa RSA & View PEM";
            this.tabKeys.BackColor = hkPinkCard;

            this.btnGenKeys.Text = "✨ Sinh Cặp Khóa 2048-bit";
            this.btnGenKeys.Location = new Point(15, 15);
            this.btnGenKeys.Size = new Size(180, 34);
            this.btnGenKeys.BackColor = hkPinkPrimary;
            this.btnGenKeys.ForeColor = Color.White;
            this.btnGenKeys.FlatStyle = FlatStyle.Flat;
            this.btnGenKeys.FlatAppearance.BorderSize = 0;
            this.btnGenKeys.Click += new System.EventHandler(this.btnGenKeys_Click);

            this.btnSavePubPem.Text = "💾 Lưu Public (.pem)";
            this.btnSavePubPem.Location = new Point(205, 15);
            this.btnSavePubPem.Size = new Size(160, 34);
            this.btnSavePubPem.BackColor = hkPinkDark;
            this.btnSavePubPem.ForeColor = Color.White;
            this.btnSavePubPem.FlatStyle = FlatStyle.Flat;
            this.btnSavePubPem.FlatAppearance.BorderSize = 0;
            this.btnSavePubPem.Click += new System.EventHandler(this.btnSavePubPem_Click);

            this.btnSavePrivPem.Text = "💾 Lưu Private (.pem)";
            this.btnSavePrivPem.Location = new Point(375, 15);
            this.btnSavePrivPem.Size = new Size(160, 34);
            this.btnSavePrivPem.BackColor = hkPinkDark;
            this.btnSavePrivPem.ForeColor = Color.White;
            this.btnSavePrivPem.FlatStyle = FlatStyle.Flat;
            this.btnSavePrivPem.FlatAppearance.BorderSize = 0;
            this.btnSavePrivPem.Click += new System.EventHandler(this.btnSavePrivPem_Click);

            this.btnImportPem.Text = "📂 Nạp File PEM...";
            this.btnImportPem.Location = new Point(545, 15);
            this.btnImportPem.Size = new Size(150, 34);
            this.btnImportPem.BackColor = Color.FromArgb(0, 150, 236);
            this.btnImportPem.ForeColor = Color.White;
            this.btnImportPem.FlatStyle = FlatStyle.Flat;
            this.btnImportPem.FlatAppearance.BorderSize = 0;
            this.btnImportPem.Click += new System.EventHandler(this.btnImportPem_Click);

            this.btnViewPem.Text = "🔍 Phân Tích Byte ASN.1";
            this.btnViewPem.Location = new Point(705, 15);
            this.btnViewPem.Size = new Size(195, 34);
            this.btnViewPem.BackColor = hkRedRibbon;
            this.btnViewPem.ForeColor = Color.White;
            this.btnViewPem.FlatStyle = FlatStyle.Flat;
            this.btnViewPem.FlatAppearance.BorderSize = 0;
            this.btnViewPem.Click += new System.EventHandler(this.btnViewPem_Click);

            this.lblPem.Text = "Nội dung Khóa PEM hiện tại:";
            this.lblPem.Location = new Point(15, 60);
            this.lblPem.Size = new Size(250, 20);

            this.txtPemContent.Location = new Point(15, 85);
            this.txtPemContent.Size = new Size(420, 435);
            this.txtPemContent.Multiline = true;
            this.txtPemContent.Font = fontCode;
            this.txtPemContent.ScrollBars = ScrollBars.Vertical;

            this.lblAsn1.Text = "Bóc tách từng byte & Ý nghĩa cấu trúc ASN.1 / DER (n, e, d, p, q,...):";
            this.lblAsn1.Location = new Point(450, 60);
            this.lblAsn1.Size = new Size(450, 20);

            this.txtAsn1Log.Location = new Point(450, 85);
            this.txtAsn1Log.Size = new Size(455, 435);
            this.txtAsn1Log.Multiline = true;
            this.txtAsn1Log.Font = fontCode;
            this.txtAsn1Log.ScrollBars = ScrollBars.Vertical;
            this.txtAsn1Log.ReadOnly = true;

            this.tabKeys.Controls.AddRange(new Control[] {
                this.btnGenKeys, this.btnSavePubPem, this.btnSavePrivPem, this.btnImportPem, this.btnViewPem,
                this.lblPem, this.txtPemContent, this.lblAsn1, this.txtAsn1Log
            });

            // ---------------- TAB 2: ENCRYPT / DECRYPT AES KEY ----------------
            this.tabEncrypt.Text = "🔐 2. Mã Hóa & Giải Mã Khóa AES";
            this.tabEncrypt.BackColor = hkPinkCard;

            this.lblDataPlain.Text = "Dữ liệu cần mã hóa (Ví dụ: Khóa AES-256 = 32 bytes Base64, tối đa 190 bytes):";
            this.lblDataPlain.Location = new Point(20, 20);
            this.lblDataPlain.Size = new Size(600, 20);

            this.txtDataPlain.Location = new Point(20, 45);
            this.txtDataPlain.Size = new Size(700, 25);
            this.txtDataPlain.Font = fontRegular;
            this.txtDataPlain.Text = "qG3X9kL1pW7vY0zC8mN4tE6bA2sD5fH=";

            this.btnGenRandomKey.Text = "🎲 Sinh Khóa AES 32B";
            this.btnGenRandomKey.Location = new Point(735, 42);
            this.btnGenRandomKey.Size = new Size(165, 30);
            this.btnGenRandomKey.BackColor = hkPinkPrimary;
            this.btnGenRandomKey.ForeColor = Color.White;
            this.btnGenRandomKey.FlatStyle = FlatStyle.Flat;
            this.btnGenRandomKey.FlatAppearance.BorderSize = 0;
            this.btnGenRandomKey.Click += new System.EventHandler(this.btnGenRandomKey_Click);

            this.btnEncryptData.Text = "🔒 Mã Hóa bằng Public Key ➔";
            this.btnEncryptData.Location = new Point(20, 85);
            this.btnEncryptData.Size = new Size(240, 36);
            this.btnEncryptData.BackColor = hkPinkDark;
            this.btnEncryptData.ForeColor = Color.White;
            this.btnEncryptData.FlatStyle = FlatStyle.Flat;
            this.btnEncryptData.FlatAppearance.BorderSize = 0;
            this.btnEncryptData.Click += new System.EventHandler(this.btnEncryptData_Click);

            this.btnDecryptData.Text = "🔓 Giải Mã bằng Private Key ➔";
            this.btnDecryptData.Location = new Point(280, 85);
            this.btnDecryptData.Size = new Size(240, 36);
            this.btnDecryptData.BackColor = Color.FromArgb(0, 137, 123);
            this.btnDecryptData.ForeColor = Color.White;
            this.btnDecryptData.FlatStyle = FlatStyle.Flat;
            this.btnDecryptData.FlatAppearance.BorderSize = 0;
            this.btnDecryptData.Click += new System.EventHandler(this.btnDecryptData_Click);

            this.lblCipher.Text = "Kết quả Mã Hóa (Bản mã Base64 lưu file .enc):";
            this.lblCipher.Location = new Point(20, 135);
            this.lblCipher.Size = new Size(400, 20);

            this.txtDataCipher.Location = new Point(20, 160);
            this.txtDataCipher.Size = new Size(880, 120);
            this.txtDataCipher.Multiline = true;
            this.txtDataCipher.Font = fontCode;
            this.txtDataCipher.ScrollBars = ScrollBars.Vertical;

            this.lblDecrypted.Text = "Kết quả Giải Mã được (Đối chứng với dữ liệu ban đầu):";
            this.lblDecrypted.Location = new Point(20, 295);
            this.lblDecrypted.Size = new Size(400, 20);

            this.txtDataDecrypted.Location = new Point(20, 320);
            this.txtDataDecrypted.Size = new Size(880, 60);
            this.txtDataDecrypted.Multiline = true;
            this.txtDataDecrypted.Font = fontRegular;

            this.lblCompareResult.Text = "Trạng thái đối chứng: Sẵn sàng.";
            this.lblCompareResult.Font = fontHeader;
            this.lblCompareResult.ForeColor = hkPinkDark;
            this.lblCompareResult.Location = new Point(20, 395);
            this.lblCompareResult.Size = new Size(880, 30);

            this.tabEncrypt.Controls.AddRange(new Control[] {
                this.lblDataPlain, this.txtDataPlain, this.btnGenRandomKey,
                this.btnEncryptData, this.btnDecryptData,
                this.lblCipher, this.txtDataCipher,
                this.lblDecrypted, this.txtDataDecrypted,
                this.lblCompareResult
            });

            // ---------------- TAB 3: SPEED BENCHMARK ----------------
            this.tabSpeed.Text = "⏱️ 3. So Sánh Tốc Độ (Public vs Private)";
            this.tabSpeed.BackColor = hkPinkCard;

            this.lblIter.Text = "Số lần lặp thử nghiệm:";
            this.lblIter.Location = new Point(20, 25);
            this.lblIter.Size = new Size(160, 25);

            this.cboIterations.Items.AddRange(new object[] { "100", "200", "500", "1000" });
            this.cboIterations.SelectedIndex = 1;
            this.cboIterations.Location = new Point(180, 22);
            this.cboIterations.Size = new Size(100, 25);

            this.btnRunBenchmark.Text = "🚀 Bắt Đầu Đo Tốc Độ";
            this.btnRunBenchmark.Location = new Point(300, 18);
            this.btnRunBenchmark.Size = new Size(180, 32);
            this.btnRunBenchmark.BackColor = hkPinkPrimary;
            this.btnRunBenchmark.ForeColor = Color.White;
            this.btnRunBenchmark.FlatStyle = FlatStyle.Flat;
            this.btnRunBenchmark.FlatAppearance.BorderSize = 0;
            this.btnRunBenchmark.Click += new System.EventHandler(this.btnRunBenchmark_Click);

            this.progressBenchmark.Location = new Point(20, 65);
            this.progressBenchmark.Size = new Size(880, 18);

            this.txtSpeedLog.Location = new Point(20, 95);
            this.txtSpeedLog.Size = new Size(880, 420);
            this.txtSpeedLog.Multiline = true;
            this.txtSpeedLog.Font = fontCode;
            this.txtSpeedLog.ScrollBars = ScrollBars.Vertical;
            this.txtSpeedLog.ReadOnly = true;

            this.tabSpeed.Controls.AddRange(new Control[] {
                this.lblIter, this.cboIterations, this.btnRunBenchmark,
                this.progressBenchmark, this.txtSpeedLog
            });

            // ---------------- TAB 4: SHA-256 AVALANCHE ----------------
            this.tabSha.Text = "❄️ 4. SHA-256 & Hiệu Ứng Tuyết Lở";
            this.tabSha.BackColor = hkPinkCard;

            this.lblSha1.Text = "Chuỗi đầu vào 1:";
            this.lblSha1.Location = new Point(20, 15);
            this.lblSha1.Size = new Size(200, 20);

            this.txtShaInput1.Location = new Point(20, 38);
            this.txtShaInput1.Size = new Size(880, 25);
            this.txtShaInput1.Font = fontRegular;
            this.txtShaInput1.Text = "HoangDinhHieu_ATBMTT_HelloKitty1";

            this.lblSha2.Text = "Chuỗi đầu vào 2 (Chỉ khác 1 ký tự cuối cùng):";
            this.lblSha2.Location = new Point(20, 75);
            this.lblSha2.Size = new Size(400, 20);

            this.txtShaInput2.Location = new Point(20, 98);
            this.txtShaInput2.Size = new Size(880, 25);
            this.txtShaInput2.Font = fontRegular;
            this.txtShaInput2.Text = "HoangDinhHieu_ATBMTT_HelloKitty2";

            this.btnCompareSha.Text = "⚡ So Sánh Kết Quả Băm (Đo Hiệu Ứng Tuyết Lở)";
            this.btnCompareSha.Location = new Point(20, 135);
            this.btnCompareSha.Size = new Size(350, 36);
            this.btnCompareSha.BackColor = hkPinkPrimary;
            this.btnCompareSha.ForeColor = Color.White;
            this.btnCompareSha.FlatStyle = FlatStyle.Flat;
            this.btnCompareSha.FlatAppearance.BorderSize = 0;
            this.btnCompareSha.Click += new System.EventHandler(this.btnCompareSha_Click);

            this.txtShaLog.Location = new Point(20, 185);
            this.txtShaLog.Size = new Size(880, 335);
            this.txtShaLog.Multiline = true;
            this.txtShaLog.Font = fontCode;
            this.txtShaLog.ScrollBars = ScrollBars.Vertical;
            this.txtShaLog.ReadOnly = true;

            this.tabSha.Controls.AddRange(new Control[] {
                this.lblSha1, this.txtShaInput1, this.lblSha2, this.txtShaInput2,
                this.btnCompareSha, this.txtShaLog
            });

            // ---------------- TAB 5: SIGNATURE PSS ----------------
            this.tabSign.Text = "✍️ 5. Chữ Ký Số RSA-PSS (Yêu Cầu Đặc Biệt)";
            this.tabSign.BackColor = hkPinkCard;

            this.lblMsg.Text = "Dữ liệu cần ký số:";
            this.lblMsg.Location = new Point(20, 15);
            this.lblMsg.Size = new Size(200, 20);

            this.txtMsgToSign.Location = new Point(20, 38);
            this.txtMsgToSign.Size = new Size(880, 25);
            this.txtMsgToSign.Font = fontRegular;
            this.txtMsgToSign.Text = "Hợp đồng chuyển giao công nghệ mật mã - Sinh viên Hoàng Đình Hiếu";

            this.btnSign1.Text = "✍️ Bấm Ký Lần 1";
            this.btnSign1.Location = new Point(20, 75);
            this.btnSign1.Size = new Size(160, 34);
            this.btnSign1.BackColor = hkPinkPrimary;
            this.btnSign1.ForeColor = Color.White;
            this.btnSign1.FlatStyle = FlatStyle.Flat;
            this.btnSign1.FlatAppearance.BorderSize = 0;
            this.btnSign1.Click += new System.EventHandler(this.btnSign1_Click);

            this.btnVerify1.Text = "✅ Xác Minh Chữ Ký 1";
            this.btnVerify1.Location = new Point(190, 75);
            this.btnVerify1.Size = new Size(180, 34);
            this.btnVerify1.BackColor = Color.FromArgb(76, 175, 80);
            this.btnVerify1.ForeColor = Color.White;
            this.btnVerify1.FlatStyle = FlatStyle.Flat;
            this.btnVerify1.FlatAppearance.BorderSize = 0;
            this.btnVerify1.Click += new System.EventHandler(this.btnVerify1_Click);

            this.btnSign2.Text = "✍️ Bấm Ký Lần 2";
            this.btnSign2.Location = new Point(480, 75);
            this.btnSign2.Size = new Size(160, 34);
            this.btnSign2.BackColor = hkPinkDark;
            this.btnSign2.ForeColor = Color.White;
            this.btnSign2.FlatStyle = FlatStyle.Flat;
            this.btnSign2.FlatAppearance.BorderSize = 0;
            this.btnSign2.Click += new System.EventHandler(this.btnSign2_Click);

            this.btnVerify2.Text = "✅ Xác Minh Chữ Ký 2";
            this.btnVerify2.Location = new Point(650, 75);
            this.btnVerify2.Size = new Size(180, 34);
            this.btnVerify2.BackColor = Color.FromArgb(76, 175, 80);
            this.btnVerify2.ForeColor = Color.White;
            this.btnVerify2.FlatStyle = FlatStyle.Flat;
            this.btnVerify2.FlatAppearance.BorderSize = 0;
            this.btnVerify2.Click += new System.EventHandler(this.btnVerify2_Click);

            this.lblSig1.Text = "Chữ ký số lần 1 (Base64):";
            this.lblSig1.Location = new Point(20, 118);
            this.lblSig1.Size = new Size(200, 20);

            this.txtSig1.Location = new Point(20, 140);
            this.txtSig1.Size = new Size(420, 80);
            this.txtSig1.Multiline = true;
            this.txtSig1.Font = fontCode;
            this.txtSig1.ScrollBars = ScrollBars.Vertical;

            this.lblSig2.Text = "Chữ ký số lần 2 (Base64):";
            this.lblSig2.Location = new Point(480, 118);
            this.lblSig2.Size = new Size(200, 20);

            this.txtSig2.Location = new Point(480, 140);
            this.txtSig2.Size = new Size(420, 80);
            this.txtSig2.Multiline = true;
            this.txtSig2.Font = fontCode;
            this.txtSig2.ScrollBars = ScrollBars.Vertical;

            this.txtSignLog.Location = new Point(20, 235);
            this.txtSignLog.Size = new Size(880, 280);
            this.txtSignLog.Multiline = true;
            this.txtSignLog.Font = fontCode;
            this.txtSignLog.ScrollBars = ScrollBars.Vertical;
            this.txtSignLog.ReadOnly = true;

            this.tabSign.Controls.AddRange(new Control[] {
                this.lblMsg, this.txtMsgToSign,
                this.btnSign1, this.btnVerify1, this.lblSig1, this.txtSig1,
                this.btnSign2, this.btnVerify2, this.lblSig2, this.txtSig2,
                this.txtSignLog
            });

            // ==================== MAIN FORM ====================
            this.ClientSize = new Size(960, 680);
            this.BackColor = hkPinkLight;
            this.Controls.AddRange(new Control[] {
                this.panelHeader, this.tabControlMain
            });
            this.Text = "🎀 BÀI 2: MÃ HÓA BẤT ĐỐI XỨNG RSA & CHỮ KÝ SỐ - HOÀNG ĐÌNH HIẾU";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;

            this.panelHeader.ResumeLayout(false);
            this.tabControlMain.ResumeLayout(false);
            this.tabKeys.ResumeLayout(false);
            this.tabKeys.PerformLayout();
            this.tabEncrypt.ResumeLayout(false);
            this.tabEncrypt.PerformLayout();
            this.tabSpeed.ResumeLayout(false);
            this.tabSpeed.PerformLayout();
            this.tabSha.ResumeLayout(false);
            this.tabSha.PerformLayout();
            this.tabSign.ResumeLayout(false);
            this.tabSign.PerformLayout();
            this.ResumeLayout(false);
        }

        private Panel panelHeader;
        private Label lblHeaderTitle;
        private Label lblHeaderSubtitle;
        private PictureBox picHelloKitty;

        private TabControl tabControlMain;
        private TabPage tabKeys;
        private TabPage tabEncrypt;
        private TabPage tabSpeed;
        private TabPage tabSha;
        private TabPage tabSign;

        // Tab 1
        private Button btnGenKeys;
        private Button btnSavePubPem;
        private Button btnSavePrivPem;
        private Button btnImportPem;
        private Button btnViewPem;
        private Label lblPem;
        private TextBox txtPemContent;
        private Label lblAsn1;
        private TextBox txtAsn1Log;

        // Tab 2
        private Label lblDataPlain;
        private TextBox txtDataPlain;
        private Button btnGenRandomKey;
        private Button btnEncryptData;
        private Button btnDecryptData;
        private Label lblCipher;
        private TextBox txtDataCipher;
        private Label lblDecrypted;
        private TextBox txtDataDecrypted;
        private Label lblCompareResult;

        // Tab 3
        private Label lblIter;
        private ComboBox cboIterations;
        private Button btnRunBenchmark;
        private ProgressBar progressBenchmark;
        private TextBox txtSpeedLog;

        // Tab 4
        private Label lblSha1;
        private TextBox txtShaInput1;
        private Label lblSha2;
        private TextBox txtShaInput2;
        private Button btnCompareSha;
        private TextBox txtShaLog;

        // Tab 5
        private Label lblMsg;
        private TextBox txtMsgToSign;
        private Button btnSign1;
        private Button btnSign2;
        private Button btnVerify1;
        private Button btnVerify2;
        private Label lblSig1;
        private TextBox txtSig1;
        private Label lblSig2;
        private TextBox txtSig2;
        private TextBox txtSignLog;
    }
}
