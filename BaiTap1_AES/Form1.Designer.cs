using System.Drawing;
using System.Windows.Forms;

namespace BaiTap1_AES
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
            this.groupBoxKey = new System.Windows.Forms.GroupBox();
            this.btnGenKey = new System.Windows.Forms.Button();
            this.btnSaveKey = new System.Windows.Forms.Button();
            this.btnLoadKey = new System.Windows.Forms.Button();
            this.lblKey = new System.Windows.Forms.Label();
            this.txtKey = new System.Windows.Forms.TextBox();
            this.lblIV = new System.Windows.Forms.Label();
            this.txtIV = new System.Windows.Forms.TextBox();
            this.picDoraemon = new System.Windows.Forms.PictureBox();

            this.groupBoxText = new System.Windows.Forms.GroupBox();
            this.lblPlain = new System.Windows.Forms.Label();
            this.txtPlainText = new System.Windows.Forms.TextBox();
            this.btnEncryptText = new System.Windows.Forms.Button();
            this.btnDecryptText = new System.Windows.Forms.Button();
            this.lblCipher = new System.Windows.Forms.Label();
            this.txtCipherText = new System.Windows.Forms.TextBox();
            this.lblDecrypted = new System.Windows.Forms.Label();
            this.txtDecryptedText = new System.Windows.Forms.TextBox();

            this.groupBoxFile = new System.Windows.Forms.GroupBox();
            this.lblSourceFile = new System.Windows.Forms.Label();
            this.txtSourceFile = new System.Windows.Forms.TextBox();
            this.btnBrowseSource = new System.Windows.Forms.Button();
            this.btnEncryptFile = new System.Windows.Forms.Button();
            this.btnDecryptFile = new System.Windows.Forms.Button();
            this.btnCompare = new System.Windows.Forms.Button();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.lblStatus = new System.Windows.Forms.Label();
            this.txtLog = new System.Windows.Forms.TextBox();

            this.groupBoxKey.SuspendLayout();
            this.groupBoxText.SuspendLayout();
            this.groupBoxFile.SuspendLayout();
            this.SuspendLayout();

            Font fontBold = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            Font fontRegular = new Font("Segoe UI", 9F, FontStyle.Regular);
            Font fontTitle = new Font("Segoe UI", 10F, FontStyle.Bold);
            Color doremonBlue = Color.FromArgb(0, 150, 236);
            Color doremonDarkBlue = Color.FromArgb(2, 119, 189);
            Color doremonYellow = Color.FromArgb(255, 179, 0);
            Color doremonRed = Color.FromArgb(229, 57, 53);
            Color whiteCard = Color.White;

            // 1. GroupBox Key
            this.groupBoxKey.Text = "🔑 1. QUẢN LÝ KHÓA AES-256";
            this.groupBoxKey.Font = fontTitle;
            this.groupBoxKey.ForeColor = doremonDarkBlue;
            this.groupBoxKey.BackColor = whiteCard;
            this.groupBoxKey.Location = new System.Drawing.Point(16, 12);
            this.groupBoxKey.Size = new System.Drawing.Size(865, 115);

            this.btnGenKey.Text = "✨ Sinh Khóa Mới";
            this.btnGenKey.Font = fontBold;
            this.btnGenKey.BackColor = doremonYellow;
            this.btnGenKey.ForeColor = Color.Black;
            this.btnGenKey.FlatStyle = FlatStyle.Flat;
            this.btnGenKey.FlatAppearance.BorderSize = 0;
            this.btnGenKey.Cursor = Cursors.Hand;
            this.btnGenKey.Location = new System.Drawing.Point(18, 28);
            this.btnGenKey.Size = new System.Drawing.Size(140, 34);
            this.btnGenKey.Click += new System.EventHandler(this.btnGenKey_Click);

            this.btnSaveKey.Text = "💾 Lưu Khóa File";
            this.btnSaveKey.Font = fontBold;
            this.btnSaveKey.BackColor = doremonBlue;
            this.btnSaveKey.ForeColor = Color.White;
            this.btnSaveKey.FlatStyle = FlatStyle.Flat;
            this.btnSaveKey.FlatAppearance.BorderSize = 0;
            this.btnSaveKey.Cursor = Cursors.Hand;
            this.btnSaveKey.Location = new System.Drawing.Point(168, 28);
            this.btnSaveKey.Size = new System.Drawing.Size(130, 34);
            this.btnSaveKey.Click += new System.EventHandler(this.btnSaveKey_Click);

            this.btnLoadKey.Text = "📂 Nạp Khóa File";
            this.btnLoadKey.Font = fontBold;
            this.btnLoadKey.BackColor = doremonBlue;
            this.btnLoadKey.ForeColor = Color.White;
            this.btnLoadKey.FlatStyle = FlatStyle.Flat;
            this.btnLoadKey.FlatAppearance.BorderSize = 0;
            this.btnLoadKey.Cursor = Cursors.Hand;
            this.btnLoadKey.Location = new System.Drawing.Point(308, 28);
            this.btnLoadKey.Size = new System.Drawing.Size(130, 34);
            this.btnLoadKey.Click += new System.EventHandler(this.btnLoadKey_Click);

            this.lblKey.Text = "Khóa:";
            this.lblKey.Font = fontRegular;
            this.lblKey.ForeColor = Color.FromArgb(50, 50, 50);
            this.lblKey.Location = new System.Drawing.Point(18, 75);
            this.lblKey.Size = new System.Drawing.Size(45, 23);

            this.txtKey.Font = fontRegular;
            this.txtKey.Location = new System.Drawing.Point(65, 72);
            this.txtKey.Size = new System.Drawing.Size(260, 25);

            this.lblIV.Text = "IV:";
            this.lblIV.Font = fontRegular;
            this.lblIV.ForeColor = Color.FromArgb(50, 50, 50);
            this.lblIV.Location = new System.Drawing.Point(335, 75);
            this.lblIV.Size = new System.Drawing.Size(30, 23);

            this.txtIV.Font = fontRegular;
            this.txtIV.Location = new System.Drawing.Point(370, 72);
            this.txtIV.Size = new System.Drawing.Size(220, 25);

            // Ảnh Doraemon & Nobita
            this.picDoraemon.Location = new System.Drawing.Point(610, 14);
            this.picDoraemon.Size = new System.Drawing.Size(240, 93);
            this.picDoraemon.SizeMode = PictureBoxSizeMode.Zoom;
            this.picDoraemon.BackColor = Color.White;
            try
            {
                string imgFile = System.IO.Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "doraemon_nobita.jpg");
                if (!System.IO.File.Exists(imgFile)) imgFile = "doraemon_nobita.jpg";
                if (System.IO.File.Exists(imgFile))
                    this.picDoraemon.Image = System.Drawing.Image.FromFile(imgFile);
            }
            catch { }

            this.groupBoxKey.Controls.AddRange(new System.Windows.Forms.Control[] {
                this.btnGenKey, this.btnSaveKey, this.btnLoadKey,
                this.lblKey, this.txtKey, this.lblIV, this.txtIV, this.picDoraemon
            });

            // 2. GroupBox Text
            this.groupBoxText.Text = "📝 2. MÃ HÓA & GIẢI MÃ VĂN BẢN";
            this.groupBoxText.Font = fontTitle;
            this.groupBoxText.ForeColor = doremonDarkBlue;
            this.groupBoxText.BackColor = whiteCard;
            this.groupBoxText.Location = new System.Drawing.Point(16, 137);
            this.groupBoxText.Size = new System.Drawing.Size(865, 175);

            this.lblPlain.Text = "Văn bản gốc:";
            this.lblPlain.Font = fontBold;
            this.lblPlain.ForeColor = Color.FromArgb(70, 70, 70);
            this.lblPlain.Location = new System.Drawing.Point(18, 25);
            this.lblPlain.Size = new System.Drawing.Size(120, 20);

            this.txtPlainText.Font = fontRegular;
            this.txtPlainText.Location = new System.Drawing.Point(18, 48);
            this.txtPlainText.Size = new System.Drawing.Size(245, 105);
            this.txtPlainText.Multiline = true;
            this.txtPlainText.Text = "Họ và tên: Hoàng Đình Hiếu - Môn An toàn và Bảo mật thông tin!";

            this.btnEncryptText.Text = "Mã hóa ➔";
            this.btnEncryptText.Font = fontBold;
            this.btnEncryptText.BackColor = doremonBlue;
            this.btnEncryptText.ForeColor = Color.White;
            this.btnEncryptText.FlatStyle = FlatStyle.Flat;
            this.btnEncryptText.FlatAppearance.BorderSize = 0;
            this.btnEncryptText.Cursor = Cursors.Hand;
            this.btnEncryptText.Location = new System.Drawing.Point(272, 70);
            this.btnEncryptText.Size = new System.Drawing.Size(100, 36);
            this.btnEncryptText.Click += new System.EventHandler(this.btnEncryptText_Click);

            this.lblCipher.Text = "Bản mã (Base64):";
            this.lblCipher.Font = fontBold;
            this.lblCipher.ForeColor = Color.FromArgb(70, 70, 70);
            this.lblCipher.Location = new System.Drawing.Point(380, 25);
            this.lblCipher.Size = new System.Drawing.Size(130, 20);

            this.txtCipherText.Font = fontRegular;
            this.txtCipherText.Location = new System.Drawing.Point(380, 48);
            this.txtCipherText.Size = new System.Drawing.Size(185, 105);
            this.txtCipherText.Multiline = true;

            this.btnDecryptText.Text = "Giải mã ➔";
            this.btnDecryptText.Font = fontBold;
            this.btnDecryptText.BackColor = Color.FromArgb(76, 175, 80);
            this.btnDecryptText.ForeColor = Color.White;
            this.btnDecryptText.FlatStyle = FlatStyle.Flat;
            this.btnDecryptText.FlatAppearance.BorderSize = 0;
            this.btnDecryptText.Cursor = Cursors.Hand;
            this.btnDecryptText.Location = new System.Drawing.Point(573, 70);
            this.btnDecryptText.Size = new System.Drawing.Size(100, 36);
            this.btnDecryptText.Click += new System.EventHandler(this.btnDecryptText_Click);

            this.lblDecrypted.Text = "Sau giải mã:";
            this.lblDecrypted.Font = fontBold;
            this.lblDecrypted.ForeColor = Color.FromArgb(70, 70, 70);
            this.lblDecrypted.Location = new System.Drawing.Point(680, 25);
            this.lblDecrypted.Size = new System.Drawing.Size(120, 20);

            this.txtDecryptedText.Font = fontRegular;
            this.txtDecryptedText.Location = new System.Drawing.Point(680, 48);
            this.txtDecryptedText.Size = new System.Drawing.Size(165, 105);
            this.txtDecryptedText.Multiline = true;

            this.groupBoxText.Controls.AddRange(new System.Windows.Forms.Control[] {
                this.lblPlain, this.txtPlainText, this.btnEncryptText,
                this.lblCipher, this.txtCipherText, this.btnDecryptText,
                this.lblDecrypted, this.txtDecryptedText
            });

            // 3. GroupBox File
            this.groupBoxFile.Text = "⚡ 3. THỬ NGHIỆM FILE LỚN (HÀNG GB) & TÍNH TỐC ĐỘ";
            this.groupBoxFile.Font = fontTitle;
            this.groupBoxFile.ForeColor = doremonDarkBlue;
            this.groupBoxFile.BackColor = whiteCard;
            this.groupBoxFile.Location = new System.Drawing.Point(16, 322);
            this.groupBoxFile.Size = new System.Drawing.Size(865, 305);

            this.lblSourceFile.Text = "File nguồn:";
            this.lblSourceFile.Font = fontRegular;
            this.lblSourceFile.ForeColor = Color.FromArgb(50, 50, 50);
            this.lblSourceFile.Location = new System.Drawing.Point(18, 28);
            this.lblSourceFile.Size = new System.Drawing.Size(75, 23);

            this.txtSourceFile.Font = fontRegular;
            this.txtSourceFile.Location = new System.Drawing.Point(95, 25);
            this.txtSourceFile.Size = new System.Drawing.Size(635, 25);

            this.btnBrowseSource.Text = "Chọn File...";
            this.btnBrowseSource.Font = fontBold;
            this.btnBrowseSource.BackColor = doremonBlue;
            this.btnBrowseSource.ForeColor = Color.White;
            this.btnBrowseSource.FlatStyle = FlatStyle.Flat;
            this.btnBrowseSource.FlatAppearance.BorderSize = 0;
            this.btnBrowseSource.Cursor = Cursors.Hand;
            this.btnBrowseSource.Location = new System.Drawing.Point(740, 23);
            this.btnBrowseSource.Size = new System.Drawing.Size(105, 28);
            this.btnBrowseSource.Click += new System.EventHandler(this.btnBrowseSource_Click);

            this.btnEncryptFile.Text = "🔒 Mã hóa File";
            this.btnEncryptFile.Font = fontBold;
            this.btnEncryptFile.BackColor = doremonBlue;
            this.btnEncryptFile.ForeColor = Color.White;
            this.btnEncryptFile.FlatStyle = FlatStyle.Flat;
            this.btnEncryptFile.FlatAppearance.BorderSize = 0;
            this.btnEncryptFile.Cursor = Cursors.Hand;
            this.btnEncryptFile.Location = new System.Drawing.Point(95, 62);
            this.btnEncryptFile.Size = new System.Drawing.Size(145, 36);
            this.btnEncryptFile.Click += new System.EventHandler(this.btnEncryptFile_Click);

            this.btnDecryptFile.Text = "🔓 Giải mã File";
            this.btnDecryptFile.Font = fontBold;
            this.btnDecryptFile.BackColor = Color.FromArgb(0, 137, 123);
            this.btnDecryptFile.ForeColor = Color.White;
            this.btnDecryptFile.FlatStyle = FlatStyle.Flat;
            this.btnDecryptFile.FlatAppearance.BorderSize = 0;
            this.btnDecryptFile.Cursor = Cursors.Hand;
            this.btnDecryptFile.Location = new System.Drawing.Point(255, 62);
            this.btnDecryptFile.Size = new System.Drawing.Size(145, 36);
            this.btnDecryptFile.Click += new System.EventHandler(this.btnDecryptFile_Click);

            this.btnCompare.Text = "⚖️ So sánh Byte (SHA-256)";
            this.btnCompare.Font = fontBold;
            this.btnCompare.BackColor = doremonRed;
            this.btnCompare.ForeColor = Color.White;
            this.btnCompare.FlatStyle = FlatStyle.Flat;
            this.btnCompare.FlatAppearance.BorderSize = 0;
            this.btnCompare.Cursor = Cursors.Hand;
            this.btnCompare.Location = new System.Drawing.Point(415, 62);
            this.btnCompare.Size = new System.Drawing.Size(220, 36);
            this.btnCompare.Click += new System.EventHandler(this.btnCompare_Click);

            this.progressBar.Location = new System.Drawing.Point(18, 110);
            this.progressBar.Size = new System.Drawing.Size(827, 20);

            this.lblStatus.Text = "Trạng thái: Sẵn sàng.";
            this.lblStatus.Font = fontBold;
            this.lblStatus.ForeColor = doremonDarkBlue;
            this.lblStatus.Location = new System.Drawing.Point(18, 137);
            this.lblStatus.Size = new System.Drawing.Size(827, 23);

            this.txtLog.Font = new Font("Consolas", 9F, FontStyle.Regular);
            this.txtLog.BackColor = Color.FromArgb(240, 248, 255);
            this.txtLog.ForeColor = Color.FromArgb(10, 50, 100);
            this.txtLog.Location = new System.Drawing.Point(18, 163);
            this.txtLog.Size = new System.Drawing.Size(827, 125);
            this.txtLog.Multiline = true;
            this.txtLog.ScrollBars = ScrollBars.Vertical;
            this.txtLog.ReadOnly = true;

            this.groupBoxFile.Controls.AddRange(new System.Windows.Forms.Control[] {
                this.lblSourceFile, this.txtSourceFile, this.btnBrowseSource,
                this.btnEncryptFile, this.btnDecryptFile, this.btnCompare,
                this.progressBar, this.lblStatus, this.txtLog
            });

            // Form Chính
            this.ClientSize = new System.Drawing.Size(898, 642);
            this.BackColor = Color.FromArgb(225, 245, 254);
            this.Controls.AddRange(new System.Windows.Forms.Control[] {
                this.groupBoxKey, this.groupBoxText, this.groupBoxFile
            });
            this.Text = "🐱 BÀI 1: MÃ HÓA ĐỐI XỨNG AES - HOÀNG ĐÌNH HIẾU";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;

            this.groupBoxKey.ResumeLayout(false);
            this.groupBoxKey.PerformLayout();
            this.groupBoxText.ResumeLayout(false);
            this.groupBoxText.PerformLayout();
            this.groupBoxFile.ResumeLayout(false);
            this.groupBoxFile.PerformLayout();
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.GroupBox groupBoxKey;
        private System.Windows.Forms.Button btnGenKey;
        private System.Windows.Forms.Button btnSaveKey;
        private System.Windows.Forms.Button btnLoadKey;
        private System.Windows.Forms.Label lblKey;
        private System.Windows.Forms.TextBox txtKey;
        private System.Windows.Forms.Label lblIV;
        private System.Windows.Forms.TextBox txtIV;
        private System.Windows.Forms.PictureBox picDoraemon;

        private System.Windows.Forms.GroupBox groupBoxText;
        private System.Windows.Forms.Label lblPlain;
        private System.Windows.Forms.TextBox txtPlainText;
        private System.Windows.Forms.Button btnEncryptText;
        private System.Windows.Forms.Button btnDecryptText;
        private System.Windows.Forms.Label lblCipher;
        private System.Windows.Forms.TextBox txtCipherText;
        private System.Windows.Forms.Label lblDecrypted;
        private System.Windows.Forms.TextBox txtDecryptedText;

        private System.Windows.Forms.GroupBox groupBoxFile;
        private System.Windows.Forms.Label lblSourceFile;
        private System.Windows.Forms.TextBox txtSourceFile;
        private System.Windows.Forms.Button btnBrowseSource;
        private System.Windows.Forms.Button btnEncryptFile;
        private System.Windows.Forms.Button btnDecryptFile;
        private System.Windows.Forms.Button btnCompare;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.TextBox txtLog;
    }
}
