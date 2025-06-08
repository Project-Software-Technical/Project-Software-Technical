# Các Framework và Thư Viện Sử Dụng trong Dự Án AIDIMS.BACKEND

## Core Frameworks

- **ASP.NET Core 9.0** - Framework chính để xây dựng Web API
- **Entity Framework Core 9.0 (Preview)** - ORM (Object-Relational Mapping) framework

## Database

- **PostgreSQL** - Hệ quản trị cơ sở dữ liệu
- **Npgsql.EntityFrameworkCore.PostgreSQL** - Provider cho Entity Framework Core để làm việc với PostgreSQL

## API Documentation

- **Swagger/OpenAPI (Swashbuckle.AspNetCore)** - Công cụ tạo tài liệu API tự động

## Các Thư Viện Bổ Sung (Dự Kiến)

- **AutoMapper** - Mapping giữa các đối tượng
- **FluentValidation** - Kiểm tra tính hợp lệ của dữ liệu
- **Serilog** - Ghi log ứng dụng
- **Microsoft.AspNetCore.Authentication.JwtBearer** - Xác thực JWT
- **IdentityServer4** - Framework xác thực và ủy quyền

## Công Cụ Development

- **Microsoft.EntityFrameworkCore.Design** - Công cụ thiết kế EF Core
- **Microsoft.EntityFrameworkCore.Tools** - Công cụ migration, scaffolding, v.v.

## Thông Tin Kết Nối PostgreSQL

- **Database**: db_aidims
- **Username**: postgres
- **Password**: 1234
- **Port**: 5433
- **Host**: localhost
- **Connection string**: "Host=localhost;Port=5433;Database=db_aidims;Username=postgres;Password=1234"

## Cấu Trúc Solution

- **AIDIMS.BACKEND.API** - Chứa controllers và endpoint API
- **AIDIMS.BACKEND.Core** - Chứa entities, interfaces và DTOs
- **AIDIMS.BACKEND.Service** - Chứa business logic
- **AIDIMS.BACKEND.Repository** - Triển khai repository pattern
- **AIDIMS.BACKEND.DataAccess** - Chứa DbContext và cấu hình cơ sở dữ liệu
- **AIDIMS.BACKEND.Tests** - Chứa các unit tests và integration tests
