#  InnoSoft Inventory Management System

[![.NET 9](https://img.shields.io/badge/.NET-9.0-blue.svg)](https://dotnet.microsoft.com/download/dotnet/9.0)
[![Entity Framework Core](https://img.shields.io/badge/EF%20Core-9.0-green.svg)](https://docs.microsoft.com/en-us/ef/)
[![Angular](https://img.shields.io/badge/Angular-Ready-red.svg)](https://angular.io/)
[![License](https://img.shields.io/badge/License-MIT-yellow.svg)](LICENSE)

A modern, scalable inventory management system built with **Clean Architecture**, **CQRS**, and **multi-language support**. Perfect for businesses needing real-time inventory tracking with enterprise-grade features.

##  Quick Start

### Prerequisites
- [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) (LocalDB, Express, or Full)

### 🔧 Setup & Run

1. **Clone the repository**
   ```bash
   git clone https://github.com/your-org/InnoSoft.InventoryManagementSystem.git
   cd InnoSoft.InventoryManagementSystem
   ```

2. **Configure Database Connection**
   
   Update `src/InnoSoft.InventorySystem.Api/appsettings.json`:
   ```json
   {
     "ConnectionStrings": {
       "Application": "Server=YOUR_SERVER;Database=InventorySystem;User Id=YOUR_USER;Password=YOUR_PASSWORD;TrustServerCertificate=True;"
     }
   }
   ```
**ERD:**
   <img width="400" height="400" alt="image" src="https://github.com/user-attachments/assets/9bd3c639-ce4a-44c4-88a8-17d45d6da3e5" />


3. **Run the Application**
   ```bash
   cd src/InnoSoft.InventorySystem.Api
   dotnet run
   ```

**That's it!** 🎉 The application will:
- Automatically create the database
- Run Entity Framework migrations
- Seed initial data (languages, categories, products)
- Start the API on `https://localhost:7000`

---

##  Architecture Overview (High-Level Digram)

This system follows **Clean Architecture** principles with a modern, scalable design:

```
┌─────────────────────────────────────────────────────────┐
│                     Frontend Layer                      │
│  Angular SPA • Material Design • Real-time Updates      │
└─────────────────────────────────────────────────────────┘
                              │
┌─────────────────────────────────────────────────────────┐
│                 API Gateway & Security                  │
│      Rate Limiting • CORS • JWT Authentication          │
└─────────────────────────────────────────────────────────┘
                              │
┌─────────────────────────────────────────────────────────┐
│                  Presentation Layer                     │
│    Controllers • SignalR Hubs • Swagger • Middleware    │
└─────────────────────────────────────────────────────────┘
                              │
┌─────────────────────────────────────────────────────────┐
│                   Application Layer                     │
│   CQRS • MediatR • AutoMapper • FluentValidation        │
└─────────────────────────────────────────────────────────┘
                              │
┌─────────────────────────────────────────────────────────┐
│                 Infrastructure Layer                    │
│     Repository Pattern • EF Core • Background Jobs      │
└─────────────────────────────────────────────────────────┘
                              │
┌─────────────────────────────────────────────────────────┐
│                     Core Layer                          │
│        Domain Entities • Business Rules • DTOs          │
└─────────────────────────────────────────────────────────┘
```

###  Key Architectural Patterns

#### ** CQRS (Command Query Responsibility Segregation)**
The system separates read and write operations for optimal performance and maintainability:

```csharp
// Commands (Write Operations)
public class CreateProductCommand : Command<Guid>
{
    public required IEnumerable<ProductTranslationDto> Translations { get; set; }
    public decimal PricePerPiece { get; set; }
    public double Quantity { get; set; }
    // ... other properties
}

// Queries (Read Operations)  
public class GetProductsQuery : Query<PagedResult<ProductDto>>
{
    public Guid? CategoryId { get; set; }
    public double? MaxQuantity { get; set; }
    public PagedQuery Pagination { get; set; }
}
```

**Benefits:**
-  **Performance**: Optimized read/write models
-  **Maintainability**: Clear separation of concerns  
-  **Scalability**: Independent scaling of read/write operations
-  **Testability**: Easy to unit test individual operations

#### ** MediatR Integration**
All business operations flow through MediatR for loose coupling:

```csharp
// In Controller
[HttpPost]
public async Task<ActionResult<Guid>> Create(CreateProductCommand command)
{
    return Ok(await _mediator.Send(command));
}

// Handler automatically called
public class CreateProductCommandHandler : ICommandHandler<CreateProductCommand, Guid>
{
    public async Task<Guid> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        // Business logic here
    }
}
```

---

##  Multi-Language Support

###  **Lightning-Fast Language Addition**

Adding a new language is incredibly easy thanks to our translation architecture:

#### **1. Add Language to Database (1 minute)**
```sql
INSERT INTO Languages (Id, Name, Abbreviation) 
VALUES (NEWID(), 'Français', 'fr');
```

#### **2. Update Localization (2 minutes)**
```csharp
// In appsettings.json
var supportedCultures = new[]
{
    new CultureInfo("en"),  // English
    new CultureInfo("ar"),  // Arabic  
    new CultureInfo("fr")   // French (NEW!)
};
```

#### **3. Add Translations (5 minutes)**
```csharp
// Products and Categories automatically support new language
var frenchTranslation = new ProductTranslation
{
    LanguageId = frenchLanguageId,
    Name = "iPhone 15 Pro",
    Description = "Le dernier smartphone phare d'Apple.",
    TranslationRootId = productId
};
```

**That's it!** Your entire application now supports French:
-  **API endpoints** automatically serve French content
-  **Frontend UI** switches to French
-  **Database queries** return French translations
-  **Validation messages** appear in French

### ** How Translation Works**

#### **Entity-Level Translation**
Every translatable entity has a translations collection:

```csharp
public class Product : Entity
{
    public ICollection<ProductTranslation> Translations { get; set; }
    public decimal PricePerPiece { get; set; }
    public double Quantity { get; set; }
    // ... other properties
}

public class ProductTranslation : TranslationBase
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public Guid LanguageId { get; set; }
    public Guid TranslationRootId { get; set; }
}
```

#### **Automatic Language Resolution**
Our custom AutoMapper resolvers automatically select the correct language:

```csharp
public class CurrentTranslationResolver<TTranslation, TResult> 
    : IValueResolver<object, object, TResult>
{
    public TResult Resolve(object source, object destination, 
                          TResult destMember, ResolutionContext context)
    {
        var currentLanguage = _languageService.GetCurrentLanguage();
        var translations = GetTranslations(source);
        
        // Return translation for current language, fallback to default
        return translations
            .FirstOrDefault(t => t.LanguageId == currentLanguage.Id)
            ?.GetValue(propertyName) ?? GetDefaultTranslation();
    }
}
```

#### **HTTP Header Language Detection**
Users can switch languages via HTTP headers:

```http
GET /api/products
x-Accept-Language: fr
Authorization: Bearer your-jwt-token
```

---

##  Testing & Quality Assurance

### ** Comprehensive Testing Strategy**

#### ** Unit Tests**
- **32+ Test Methods** covering core entities
- **MSTest Framework** with parallel execution
- **FluentAssertions** for readable assertions
- **Moq** for dependency mocking

```csharp
[TestMethod]
public void Product_Should_Handle_Multiple_Translations()
{
    // Arrange
    var product = new Product { /* ... */ };
    var translations = new List<ProductTranslation>
    {
        new() { LanguageId = englishId, Name = "iPhone 15 Pro" },
        new() { LanguageId = arabicId, Name = "آيفون 15 برو" }
    };

    // Act
    product.Translations = translations;

    // Assert
    product.Translations.Should().HaveCount(2);
    product.Translations.Should().Contain(t => t.Name == "iPhone 15 Pro");
    product.Translations.Should().Contain(t => t.Name == "آيفون 15 برو");
}
```

#### ** Test Coverage Areas**
-  **Entity Logic**: Core domain entities and business rules
-  **CQRS Handlers**: Command and query processing
-  **AutoMapper**: Object-to-object mapping
-  **Validation**: FluentValidation rules
-  **Authentication**: JWT token handling
-  **Multi-language**: Translation functionality

#### ** Test Data Builders**
Simplified test object creation:

```csharp
public static class TestDataBuilder
{
    public static Product CreateValidProduct(Guid? id = null)
    {
        return new Product
        {
            Id = id ?? Guid.NewGuid(),
            PricePerPiece = 100.50m,
            Quantity = 50,
            AlertThresholdQuantity = 5,
            Translations = CreateMultiLanguageTranslations()
        };
    }
}
```

### ** Code Quality & Linting**

#### ** Built-in Quality Measures**
- ** Clean Architecture**: Enforced layer separation
- ** SOLID Principles**: Dependency inversion, single responsibility
- ** Nullable Reference Types**: Enhanced null safety (.NET 9)
- ** Performance**: Async/await throughout, efficient queries
- ** Logging**: Structured logging with Serilog

#### ** Validation Pipeline**
```csharp
public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
        RuleFor(x => x.Translations).NotEmpty();
        RuleFor(x => x.PricePerPiece).GreaterThanOrEqualTo(0);
        RuleFor(x => x.Quantity).GreaterThanOrEqualTo(0);
        RuleFor(x => x.AlertThresholdQuantity).GreaterThanOrEqualTo(0);
        RuleForEach(x => x.Translations).SetValidator(new ProductTranslationDtoValidator());
    }
}
```

---

##  Key Features

### ** Inventory Management**
-  **Product CRUD**: Create, read, update, delete products
-  **Category Management**: Hierarchical product organization
-  **Stock Tracking**: Real-time quantity monitoring
-  **Low Stock Alerts**: Automated threshold notifications
-  **Multi-language Products**: Localized product information

### ** Security & Authentication**
-  **JWT Authentication**: Secure token-based auth
-  **Role-based Authorization**: Admin/User permissions
-  **Rate Limiting**: DDoS protection and usage throttling
-  **CORS Support**: Secure cross-origin requests

### ** Real-time Features**
-  **SignalR Integration**: Live inventory updates
-  **Background Services**: Automated stock monitoring
-  **Push Notifications**: Instant low-stock alerts

### ** Internationalization**
-  **Multi-language Support**: English, Arabic (+ easy to add more)
-  **RTL Support**: Right-to-left language support
-  **Cultural Formatting**: Dates, numbers, currencies
-  **Automatic Language Detection**: HTTP header based

### ** Performance & Scalability**
-  **EF Core Optimization**: Split queries, async operations
-  **Repository Pattern**: Efficient data access
-  **AutoMapper**: Object mapping optimization
-  **Rate Limiting**: Request throttling
-  **Background Processing**: Non-blocking operations

---

##  API Documentation

### ** Interactive API Explorer**
Access Swagger UI at: `https://localhost:7000/swagger`

### ** Key Endpoints**

#### ** Authentication**
```http
POST /api/auth/login
Content-Type: application/json

{
  "username": "admin",
  "password": "admin123"
}
```

#### ** Products**
```http
# Get all products with pagination
GET /api/products?pageNumber=1&pageSize=10
x-Accept-Language: en
Authorization: Bearer your-jwt-token

# Create new product
POST /api/products
Content-Type: application/json
x-Accept-Language: en
Authorization: Bearer your-jwt-token

{
  "translations": [
    {
      "name": "New Product",
      "description": "Product description",
      "language": "en"
    }
  ],
  "pricePerPiece": 99.99,
  "quantity": 100,
  "alertThresholdQuantity": 10,
  "categoryId": "category-guid"
}
```

#### ** Categories**
```http
# Get all categories
GET /api/categories
x-Accept-Language: ar
Authorization: Bearer your-jwt-token
```

---



##  Configuration

### ** Application Settings**
Key configuration options in `appsettings.json`:

```json
{
  "ConnectionStrings": {
    "Application": "Your SQL Server connection string"
  },
  "JwtSettings": {
    "Key": "Your-Secret-Key-Here",
    "Issuer": "InventorySystem", 
    "Audience": "InventorySystem_Angular",
    "DurationInMinutes": 6000
  },
  "Cors": {
    "AllowedOrigins": "http://localhost:4200"
  }
}
```


##  Acknowledgments

- **Clean/Onion Architecture** principles
- **CQRS** pattern implementation with MediatR
- **Entity Framework Core** for data access
- **AutoMapper** for object mapping
- **FluentValidation** for input validation
- **SignalR** for real-time communication

---
