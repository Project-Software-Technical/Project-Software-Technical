# AIDIMS - AI-Powered DICOM Imaging Management System

Hệ thống quản lý hình ảnh y tế DICOM tích hợp AI.

## Tổng quan

AIDIMS là một hệ thống quản lý hình ảnh y tế với khả năng phân tích hỗ trợ bởi trí tuệ nhân tạo, giúp các bác sĩ trong việc chẩn đoán và lưu trữ hình ảnh DICOM.

## Cấu trúc dự án

- **AIDIMS.Core**: Chứa các models, interfaces và data core
- **AIDIMS.Repositories**: Chứa các repository thao tác với database
- **AIDIMS.Services**: Chứa các service xử lý logic nghiệp vụ
- **AIDIMS.App**: Web API và ứng dụng

## Công nghệ sử dụng

- ASP.NET Core
- Entity Framework Core
- PostgreSQL
- DICOM

## Database

Cấu trúc cơ sở dữ liệu bao gồm các bảng:

- Role - Vai trò người dùng
- HospitalStaff - Nhân viên bệnh viện
- User - Người dùng hệ thống
- Patient - Bệnh nhân
- MedicalRecord - Hồ sơ bệnh án
- Service - Dịch vụ
- ImagingRequest - Yêu cầu chụp
- DiagnosisResult - Kết quả chẩn đoán
- DicomImage - Hình ảnh DICOM

## Cài đặt và chạy dự án

1. Clone repository:

```
git clone https://github.com/Project-Software-Technical/Project-Software-Technical.git
```

2. Cài đặt PostgreSQL và tạo database aidims_db.

3. Cập nhật connection string trong appsettings.json nếu cần.

4. Chạy migration để tạo database:

```
cd AIDIMS.Core
dotnet ef database update
```

5. Chạy ứng dụng:

```
cd AIDIMS.App
dotnet run
```
