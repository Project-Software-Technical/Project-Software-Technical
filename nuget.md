# Các lệnh Migration đã chạy

## Cài đặt các package cần thiết

```bash
# Entity Framework Core
dotnet add AIDIMS.Core package Microsoft.EntityFrameworkCore
dotnet add AIDIMS.Core package Microsoft.EntityFrameworkCore.SqlServer
dotnet add AIDIMS.Core package Microsoft.EntityFrameworkCore.Design
dotnet add AIDIMS.Core package Microsoft.EntityFrameworkCore.Tools
dotnet add AIDIMS.Core package Microsoft.EntityFrameworkCore.Relational
```

## Tạo migration ban đầu

```bash
dotnet ef migrations add InitialCreate --project AIDIMS.Core
```

## Cập nhật database

```bash
dotnet ef database update --project AIDIMS.Core
```

## Các lệnh hữu ích khác

```bash
# Xóa migration cuối cùng
dotnet ef migrations remove

# Liệt kê tất cả migrations
dotnet ef migrations list

# Tạo script SQL từ migrations
dotnet ef migrations script

# Xóa database
dotnet ef database drop
```

## Lưu ý

- Đảm bảo đã cài đặt Entity Framework Core Tools:

```bash
dotnet tool install --global dotnet-ef
```

- Nếu đã cài đặt, có thể cập nhật lên phiên bản mới nhất:

```bash
dotnet tool update --global dotnet-ef
```
