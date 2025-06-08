# Đề Xuất Cấu Trúc Thư Mục Dự Án AIDIMS.BACKEND

## Tổng Quan

Dự án được tổ chức theo mô hình phân lớp (N-Layer) kết hợp với mô hình MVC, tập trung vào việc phát triển Backend API. Cấu trúc này giúp tách biệt các thành phần, dễ bảo trì và mở rộng.

## Cấu Trúc Solution

```
AIDIMS.BACKEND/
│
├── AIDIMS.BACKEND.API/                # Project chính chứa API endpoints và controllers
│   ├── Controllers/                   # API controllers
│   ├── Middlewares/                   # Custom middlewares
│   ├── Filters/                       # Action filters
│   ├── Extensions/                    # Extension methods
│   ├── Program.cs                     # Điểm khởi đầu ứng dụng
│   └── appsettings.json               # Cấu hình ứng dụng
│
├── AIDIMS.BACKEND.Core/               # Chứa các entities, interfaces và models chung
│   ├── Entities/                      # Domain entities
│   ├── Interfaces/                    # Interfaces cho services và repositories
│   ├── DTOs/                          # Data Transfer Objects
│   ├── Enums/                         # Enum definitions
│   └── Constants/                     # Các hằng số
│
├── AIDIMS.BACKEND.Service/            # Chứa business logic
│   ├── Services/                      # Implementations của các service
│   ├── Validators/                    # Validators cho các model
│   └── Mapping/                       # Mapper profiles (AutoMapper)
│
├── AIDIMS.BACKEND.Repository/         # Chứa repository pattern implementations
│   ├── Repositories/                  # Implementations của các repository
│   └── UnitOfWork/                    # UnitOfWork pattern implementation
│
├── AIDIMS.BACKEND.DataAccess/         # Chứa database access logic
│   ├── Context/                       # DbContext và cấu hình
│   ├── Configurations/                # Entity type configurations
│   ├── Migrations/                    # Database migrations
│   └── Seeder/                        # Dữ liệu khởi tạo
│
└── AIDIMS.BACKEND.Tests/              # Unit tests và integration tests
    ├── UnitTests/                     # Unit tests cho các component
    └── IntegrationTests/              # Integration tests
```

## Luồng Dữ Liệu

1. **Client Request** → **API Controller** (AIDIMS.BACKEND.API)
2. **API Controller** → **Service Layer** (AIDIMS.BACKEND.Service)
3. **Service Layer** → **Repository Layer** (AIDIMS.BACKEND.Repository)
4. **Repository Layer** → **Data Access Layer** (AIDIMS.BACKEND.DataAccess)
5. **Data Access Layer** → **Database**

## Công Nghệ Đề Xuất

- **ASP.NET Core** - Framework chính
- **Entity Framework Core** - ORM cho data access
- **AutoMapper** - Object mapping
- **FluentValidation** - Model validation
- **Swagger/OpenAPI** - API documentation
- **xUnit** - Testing framework

## Các Nguyên Tắc Thiết Kế

- **SOLID Principles** - Single Responsibility, Open/Closed, Liskov Substitution, Interface Segregation, Dependency Inversion
- **DRY (Don't Repeat Yourself)** - Tránh lặp code
- **Dependency Injection** - Sử dụng DI container của ASP.NET Core
- **Repository Pattern** - Tách biệt data access logic
- **Unit of Work Pattern** - Đảm bảo tính nhất quán của các transaction
