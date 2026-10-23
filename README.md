# BÀI TẬP VỀ NHÀ: AN TOÀN VÀ BẢO MẬT THÔNG TIN

## 1. Thông tin sinh viên
- **Họ và tên**: Hoàng Đình Hiếu
- **MSSV**: K235480106025
- **LỚP**: K59.KTP.K01
- **GitHub**: [HoangDinhHieu](https://github.com/HoangDinhHieu)
- **Repository**: [BTvippro](https://github.com/HoangDinhHieu/BTvippro)

## 2. Mục tiêu & Nội dung bài tập
- **Bài 1 - Mã hóa đối xứng AES**:
  - Sinh khóa AES-256 bit & IV 128 bit.
  - Lưu và nạp khóa từ file.
  - Mã hóa / Giải mã văn bản (hiển thị Base64/Hex).
  - Mã hóa / Giải mã file kích thước lớn (hàng GB) theo luồng (Stream buffer).
  - Đo thời gian, tính tốc độ (MB/s) và so sánh toàn vẹn dữ liệu file gốc vs file giải mã.
- **Bài 2 - Mã hóa bất đối xứng RSA & Chữ ký số**:
  - Sinh cặp khóa RSA 2048-bit, xuất ra định dạng PEM chuẩn.
  - View PEM: Bóc tách cấu trúc ASN.1 / DER và giải thích ý nghĩa các byte.
  - Import file PEM bóc tách các tham số ($n, e, d, ...$).
  - Mã hóa / Giải mã dữ liệu $\le 190$ byte (khóa AES 32 byte).
  - Đánh giá tốc độ giữa Public Key và Private Key.
  - Băm SHA-256 và kiểm chứng hiệu ứng tuyết lở (Avalanche Effect).
  - Chữ ký số: Ứng dụng RSA-PSS để cùng 1 dữ liệu, cùng 1 khóa tạo ra các chữ ký khác nhau qua mỗi lần ký.

## 3. Công nghệ sử dụng
- Ngôn ngữ: C# (.NET)
- Giao diện: Windows Forms / Desktop Application
- Quản lý phiên bản: Git & GitHub

## 4. Tiến độ thực hiện
- [x] Khởi tạo Repository & Cấu trúc dự án
- [ ] Xây dựng thư viện mã hóa AES (Bài 1)
- [ ] Giao diện thử nghiệm AES chuỗi & file lớn hàng GB
- [ ] Xây dựng thư viện & Giao diện RSA, View PEM (Bài 2)
- [ ] Module kiểm chứng SHA-256 & Chữ ký số RSA-PSS
- [ ] Đo đạc số liệu, chụp ảnh minh chứng & hoàn thiện báo cáo
