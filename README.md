# BÀI TẬP VỀ NHÀ: AN TOÀN VÀ BẢO MẬT THÔNG TIN

## 1. Thông tin sinh viên
- **Họ và tên**: Hoàng Đình Hiếu
- **MSSV**: K235480106025
- **LỚP**: K59.KTP.K01
- **GitHub**: [HoangDinhHieu](https://github.com/HoangDinhHieu)
- **Repository**: [BTvippro](https://github.com/HoangDinhHieu/BTvippro)


---

## 2. Công nghệ & Kiến trúc dự án
- **Nền tảng**: .NET 10 (C#) / Windows Forms App
- **Kiến trúc giải pháp**:
  - `libSecurity`: Thư viện xử lý thuật toán mật mã dùng chung (`MyAES`, `MyRSA`, hàm băm).
  - `BaiTap1_AES`: Giao diện ứng dụng mã hóa đối xứng AES.
  - `BaiTap2_RSA`: Giao diện ứng dụng mã hóa bất đối xứng RSA & Chữ ký số.

---

## 3. Báo cáo kết quả Bài 1 - Mã hóa đối xứng AES

### 3.1. Các tính năng đã hoàn thành
- [x] Sinh cặp khóa ngẫu nhiên AES 256-bit và Vector khởi tạo IV 128-bit.
- [x] Xuất khóa ra file `.key` và nạp khóa trực tiếp từ file.
- [x] Mã hóa / giải mã văn bản chuỗi (kết quả hiển thị Base64 trực quan).
- [x] Mã hóa / giải mã file kích thước lớn (hàng GB) theo luồng đệm (Stream Buffer 1MB), không gây tràn bộ nhớ RAM.
- [x] Đo lường thời gian xử lý bằng `Stopwatch`, tính tốc độ truyền tải trung bình (`MB/s`).
- [x] So sánh tính toàn vẹn dữ liệu từng byte bằng hàm băm SHA-256 giữa file nguồn và file giải mã.

### 3.2. Bảng số liệu thực nghiệm đo đạc hiệu năng file lớn
| Tiêu chí đánh giá | Số liệu thực nghiệm |
| :--- | :--- |
| **Tên file thử nghiệm** | *SERVER_EVAL_x64FRE_en-us.iso* |
| **Kích thước file** | *4810.42 MB* |
| **Thời gian mã hóa** | *Thời gian: 14.876 giây* |
| **Tốc độ mã hóa** | *Tốc độ: 323.38 MB/s* |
| **Thời gian giải mã** | *Thời gian: 6.408 giây* |
| **Tốc độ giải mã** | *Tốc độ: 750.70 MB/s* |
| **Mã SHA-256 file nguồn** | *3e4fa6d8507b554856fc9ca6079cc402df11a8b79344871669f0251535255325* |
| **Mã SHA-256 file sau giải mã** | *3e4fa6d8507b554856fc9ca6079cc402df11a8b79344871669f0251535255325* |
| **Kết luận toàn vẹn** | **Khớp 100% từng byte (Trùng khớp tuyệt đối SHA-256)** |

### 3.3. Ảnh minh chứng kết quả chạy thực tế
<img width="1920" height="1080" alt="image" src="https://github.com/user-attachments/assets/f1905c5c-5694-48b4-98c4-a60e6ffa1845" />


---

## 4. Tiến độ tổng thể bài tập
- [x] Khởi tạo Repository, .gitignore & cấu trúc Solution 3 tầng
- [x] **Bài 1**: Hoàn thiện mã hóa đối xứng AES (chuỗi + file lớn hàng GB + đo tốc độ + so sánh byte)
- [ ] **Bài 2**: Mã hóa bất đối xứng RSA (cặp khóa, View PEM, mã hóa/giải mã, so sánh tốc độ Public vs Private Key)
- [ ] **Bài 2 mở rộng**: Hiệu ứng tuyết lở SHA-256 & Chữ ký số RSA-PSS ngẫu nhiên hóa
