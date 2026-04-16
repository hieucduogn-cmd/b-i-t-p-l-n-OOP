# 📚 HỆ THỐNG QUẢN LÝ CỬA HÀNG SÁCH - ỨNG DỤNG OOP

Một hệ thống quản lý cửa hàng sách được xây dựng từ đầu sử dụng C# Windows Forms và lập trình hướng đối tượng (OOP). Hệ thống hỗ trợ quản lý sách, bán hàng, lưu trữ hóa đơn và thống kê doanh thu. Trong quá trình kiểm thử, hệ thống hoạt động ổn định với giao diện thân thiện, dễ sử dụng.

## 📑 Mục Lục
- Giới thiệu
- Tính năng
- Công nghệ ứng dụng
- Cấu trúc dự án
- Cài đặt & Vận hành
- Kiến trúc hệ thống
- Cơ sở dữ liệu (Schema)
- Khắc phục sự cố

---

## 📖 Giới thiệu

Dự án này là giải pháp quản lý cửa hàng sách với giao diện trực quan, áp dụng đầy đủ 4 tính chất của lập trình hướng đối tượng (OOP): **Đóng gói, Kế thừa, Đa hình, Trừu tượng**.

Hệ thống hỗ trợ đầy đủ các nghiệp vụ: **Thêm sách -> Xóa sách -> Tìm kiếm -> Bán hàng -> Tạo hóa đơn -> Thống kê doanh thu**.

---

## ✨ Tính năng nổi bật

| Tính năng | Mô tả |
|-----------|-------|
| ⚡ **Xử lý nhanh** | Thao tác thêm, xóa, tìm kiếm sách được xử lý tức thì, không bị gián đoạn |
| 🖼️ **Quản lý đa loại sách** | Hỗ trợ 3 loại: Sách Giáo Khoa, Sách Tham Khảo, Sách Văn Học |
| 👥 **Quản lý khách hàng** | Lưu trữ thông tin khách hàng, dễ dàng tra cứu |
| 🔐 **Bảo mật dữ liệu** | Dữ liệu được lưu trữ cục bộ, không qua mạng, đảm bảo an toàn |
| 💾 **Lưu trữ vĩnh viễn** | Tự động lưu sách, hóa đơn vào danh sách, hỗ trợ xuất xem chi tiết |
| 🎨 **Giao diện thân thiện** | Thiết kế đơn giản, dễ sử dụng, hoạt động tốt trên Windows |

---

## 🛠 Công nghệ sử dụng

| Thành phần | Công nghệ | Chi tiết |
|------------|-----------|----------|
| Phần mềm | C# | Ngôn ngữ lập trình chính |
| Giao diện | Windows Forms | Xây dựng giao diện đồ họa |
| Kiến trúc | OOP | Lập trình hướng đối tượng |
| Lưu trữ | List / Dictionary | Lưu trữ dữ liệu trong bộ nhớ |
| IDE | Visual Studio 2022 | Môi trường phát triển |

---

## 📂 Cấu trúc dự án

Để đảm bảo hệ thống vận hành chính xác, cấu trúc thư mục được tổ chức như sau:
## 📂 Cấu trúc dự án
📁 QuanLySach/
├── 📁 Models/
│ ├── 📁 Entities/ (Sach, KhachHang, HoaDon...)
│ └── 📁 Services/ (QuanLyCuaHang)
├── 📁 Forms/ (Form chính, Thêm, Bán, Hóa đơn)
└── 📄 Program.cs (Khởi chạy)


## 🚀 Cài đặt & Vận hành

### 1. Yêu cầu tiên quyết
- Máy tính cài đặt **Windows 10** trở lên
- Đã cài đặt **Visual Studio 2022** (hoặc 2019)
- **.NET Framework 4.8** hoặc cao hơn

### 2. Mở project
- Mở Visual Studio → **File** → **Open** → **Project/Solution**
- Chọn file `QuanLySach.sln`

### 3. Build và chạy
- Nhấn `Ctrl + Shift + B` để Build project
- Nhấn `F5` để chạy chương trình

**Dấu hiệu thành công:** Giao diện chính hiển thị danh sách sách có sẵn.

### 4. Sử dụng
- Xem danh sách sách hiển thị sẵn
- Nhấn **"Thêm sách"** để thêm sách mới
- Chọn sách → nhấn **"Xóa sách"** để xóa
- Nhấn **"Bán hàng"** để tạo hóa đơn
- Nhấn **"Hóa đơn"** để xem lịch sử giao dịch

---

## 🧠 Kiến trúc hệ thống

### Luồng xử lý bán hàng (The Non-blocking Flow)

Hệ thống được thiết kế để thao tác nhanh, mượt mà:
👤 Chọn khách hàng
│
▼
📖 Chọn sách + nhập số lượng
│
▼
🛒 Thêm vào giỏ hàng (có thể lặp lại)
│
▼
💳 Thanh toán
│
├────────────────────────────────────┐
│ │
▼ ▼
📄 Tạo hóa đơn 🔄 Cập nhật tồn kho
│ │
└────────────────┬───────────────────┘
│
▼
✅ Hoàn tất

**Chi tiết:**
- **Bước 1 (Chọn khách):** Chọn khách hàng từ danh sách hoặc để mặc định "Khách lẻ"
- **Bước 2 (Chọn sách):** Chọn sách, nhập số lượng → Thêm vào giỏ
- **Bước 3 (Thanh toán):** Xác nhận giỏ hàng → Hệ thống tạo hóa đơn và trừ tồn kho
- **Kết quả:** Người dùng thấy hóa đơn ngay lập tức, dữ liệu được cập nhật

---

---

## 🗄 Cơ sở dữ liệu (Data Structure)

Dữ liệu được lưu trữ trong bộ nhớ bằng **List<T>**:

### 1. Danh sách sách (List<Sach>)

| Thuộc tính | Kiểu | Mô tả |
|------------|------|-------|
| MaSach | string | Mã sách (Khóa chính) |
| TenSach | string | Tên sách |
| TacGia | string | Tác giả |
| NamXuatBan | int | Năm xuất bản |
| GiaBan | double | Giá bán |
| SoLuongTon | int | Số lượng tồn kho |

### 2. Danh sách khách hàng (List<KhachHang>)

| Thuộc tính | Kiểu | Mô tả |
|------------|------|-------|
| MaKhach | string | Mã khách hàng |
| TenKhach | string | Tên khách hàng |
| SoDienThoai | string | Số điện thoại |

### 3. Danh sách hóa đơn (List<HoaDon>)

| Thuộc tính | Kiểu | Mô tả |
|------------|------|-------|
| MaHoaDon | string | Mã hóa đơn (Tự động) |
| KhachHang | KhachHang | Thông tin khách hàng |
| NgayLap | DateTime | Thời gian lập hóa đơn |
| ChiTiet | List<ChiTietHoaDon> | Danh sách sách đã mua |
| TongTien | double | Tổng tiền thanh toán |

---

## 🔧 Khắc phục sự cố

### 1. Lỗi: "Không build được project"
**Nguyên nhân:** Thiếu .NET Framework hoặc lỗi cú pháp
**Cách sửa:**
- Kiểm tra lại code đã copy đúng chưa
- Vào **Tools** → **Get Tools and Features** → Cài đặt .NET Framework 4.8

### 2. Lỗi: "Không thấy giao diện"
**Nguyên nhân:** Chưa build project hoặc chưy sai file
**Cách sửa:**
- Nhấn `Ctrl + Shift + B` để Build
- Đảm bảo file `Program.cs` chạy đúng form: `Application.Run(new FormQuanLy())`

### 3. Lỗi: "Thêm sách không thấy hiển thị"
**Nguyên nhân:** DataGridView chưa refresh
**Cách sửa:**
- Gọi lại `LoadData()` sau khi thêm sách (code đã có sẵn)

### 4. Lỗi: "Bán hàng báo lỗi"
**Nguyên nhân:** Chưa chọn khách hàng hoặc sách
**Cách sửa:**
- Luôn chọn khách hàng (hoặc để mặc định "Khách lẻ")
- Đảm bảo sách được chọn có số lượng > 0

---

## 📊 Bảng tổng kết OOP

| Tính chất | Thể hiện trong code | File minh họa |
|-----------|---------------------|---------------|
| 🔒 **Đóng gói** | Thuộc tính private + public get/set | `Sach.cs` |
| 🌳 **Kế thừa** | `SachGiaoKhoa : Sach` | `SachGiaoKhoa.cs` |
| 🎭 **Đa hình** | Override phương thức `TinhGiaSauChietKhau()` | Các file Sach*.cs |
| 📦 **Trừu tượng** | Lớp `abstract Sach` | `Sach.cs` |

---

## 👨‍💻 Tác giả

Dự án được phát triển bởi **Nhóm [Tên nhóm]** - Môn Lập trình hướng đối tượng

---

**© 2024 - Hệ thống Quản Lý Cửa Hàng Sách | OOP Project**


