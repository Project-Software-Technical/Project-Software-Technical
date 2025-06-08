# Tài liệu cấu trúc cơ sở dữ liệu MedicalSystem

## 1. Bảng Vai trò (Role)

- Mã vai trò (RoleID)
- Tên vai trò (RoleName)
- Mô tả (Description)

## 2. Bảng Nhân viên bệnh viện (HospitalStaff)

- Mã nhân viên (StaffID)
- Họ tên (FullName)
- Ngày sinh (BirthDate)
- Giới tính (Gender)
- Địa chỉ (Address)
- Số điện thoại (Phone)
- Email (Email)
- Chức vụ (Position)
- Trạng thái (Status)
- Ngày tạo (CreatedDate)

## 3. Bảng Người dùng (User)

- Mã người dùng (UserID)
- Mã nhân viên (StaffID)
- Mã vai trò (RoleID)
- Tên đăng nhập (Username)
- Mật khẩu (Password)
- Ngày tạo (CreatedDate)
- Trạng thái (Status)

## 4. Bảng Bệnh nhân (Patient)

- Mã bệnh nhân (PatientID)
- Họ tên (FullName)
- Ngày sinh (BirthDate)
- Giới tính (Gender)
- Địa chỉ (Address)
- Số điện thoại (Phone)
- Email (Email)
- Ngày tạo (CreatedDate)

## 5. Bảng Hồ sơ bệnh án (MedicalRecord)

- Mã hồ sơ (RecordID)
- Mã bệnh nhân (PatientID)
- Mã bác sĩ (StaffID)
- Triệu chứng (Symptoms)
- Ngày khám (ExaminationDate)
- Chẩn đoán (Diagnosis)
- Kết quả (Result)
- Trạng thái (Status)
- Ngày tạo (CreatedDate)

## 6. Bảng Dịch vụ (Service)

- Mã dịch vụ (ServiceID)
- Tên dịch vụ (ServiceName)
- Mô tả (Description)
- Giá (Price)
- Trạng thái (Status)
- Ngày tạo (CreatedDate)

## 7. Bảng Yêu cầu chụp (ImagingRequest)

- Mã yêu cầu (RequestID)
- Mã hồ sơ (RecordID)
- Mã dịch vụ (ServiceID)
- Ngày yêu cầu (RequestDate)
- Ngày thực hiện (ExecutionDate)
- Ghi chú (Notes)
- Trạng thái (Status)

## 8. Bảng Kết quả chẩn đoán (DiagnosisResult)

- Mã kết quả (DiagnosisResultID)
- Mã hồ sơ (RecordID)
- Kết luận bác sĩ (DoctorConclusion)
- Ngày chẩn đoán (DiagnosisDate)
- Mô tả kết quả (ResultDescription)
- Ghi chú (Notes)
- Trạng thái (Status)

## 9. Bảng Hình ảnh DICOM (DicomImage)

- Mã hình ảnh (ImageID)
- Mã hồ sơ (RecordID)
- Mã kỹ thuật viên (TechnicianID)
- Ghi chú Bác sĩ (DoctorNotes)
- Ghi chú AI (AIFeedback)
- Đã duyệt? (IsApproved)
- Đường dẫn file (FilePath)
- Loại hình ảnh (ImageType)
- Kích thước file (FileSize)
- Độ phân giải (Resolution)
- Ngày tạo (CreatedDate)
