# Product SOAP Integration API

## Overview

This solution is a .NET 9 Web API that provides secure access to product information through SOAP service integration. It implements JWT-based authentication, follows clean architecture principles with dependency injection, and provides comprehensive API documentation through Swagger/OpenAPI.

## Architecture Principles

### Clean Architecture Implementation

The solution follows clean architecture patterns with clear separation of concerns:

```
┌─────────────────────────────────────────────────────────────┐
│                     ProductApi (Web Layer)                  │
│  ┌─────────────────┐  ┌─────────────────┐  ┌─────────────── │
│  │  Controllers    │  │  Authentication │  │  Configuration │
│  │  - Products     │  │  - JWT Bearer   │  │  - Swagger     │
│  │  - Auth         │  │  - Claims       │  │  - DI Setup    │
│  └─────────────────┘  └─────────────────┘  └─────────────── │
└─────────────────────────────────────────────────────────────┘
                              │
                              ▼
┌─────────────────────────────────────────────────────────────┐
│                 Product.IoC (IoC Container)                 │
│  ┌─────────────────────────────────────────────────────────┐│
│  │         Dependency Injection Configuration              ││
│  │         - Service Registration                          ││
│  │         - Interface Binding                             ││
│  └─────────────────────────────────────────────────────────┘│
└─────────────────────────────────────────────────────────────┘
                              │
                              ▼
┌─────────────────────────────────────────────────────────────┐
│               Product.Domain (Business Layer)               │
│  ┌─────────────────┐              ┌─────────────────────────┐│
│  │   Interfaces    │              │      Services          ││
│  │ - IProductDomain│              │ - ProductDomainService ││
│  │   Service       │              │   (Business Logic)     ││
│  └─────────────────┘              └─────────────────────────┘│
└─────────────────────────────────────────────────────────────┘
                              │
                              ▼
┌─────────────────────────────────────────────────────────────┐
│            Product.Soap.Adapter (External Layer)           │
│  ┌─────────────────┐  ┌─────────────────┐  ┌─────────────── │
│  │   Interfaces    │  │    Adapters     │  │   Models      │
│  │ - IProductDetails│  │ - ProductSoap   │  │ - ProductDetails│
│  │   Interface     │  │   Adapter       │  │ - SoapClient  │
│  └─────────────────┘  └─────────────────┘  └─────────────── │
└─────────────────────────────────────────────────────────────┘
```

### Design Principles

1. **Separation of Concerns**: Each layer has a specific responsibility
2. **Dependency Inversion**: Higher-level modules depend on abstractions, not concretions
3. **Interface Segregation**: Small, focused interfaces rather than large monolithic ones
4. **Single Responsibility**: Each class has one reason to change
5. **Open/Closed**: Open for extension, closed for modification

### Key Architectural Components

#### 1. **API Layer (`ProductApi`)**
- **Controllers**: Handle HTTP requests and responses
- **Authentication**: JWT-based security implementation
- **Configuration**: Swagger/OpenAPI documentation setup
- **Dependency Registration**: IoC container configuration

#### 2. **Domain Layer (`Product.Domain`)**
- **Business Logic**: Core application rules and validation
- **Domain Services**: Orchestrate business operations
- **Interfaces**: Define contracts for external dependencies

#### 3. **Adapter Layer (`Product.Soap.Adapter`)**
- **External Integration**: SOAP service communication
- **Data Transformation**: Convert between external and internal models
- **Protocol Handling**: SOAP envelope creation and parsing

#### 4. **IoC Layer (`Product.IoC`)**
- **Dependency Management**: Service registration and lifetime management
- **Interface Binding**: Map interfaces to concrete implementations

## Technology Stack

- **.NET 9.0**: Latest .NET framework
- **ASP.NET Core Web API**: RESTful API framework
- **JWT Bearer Authentication**: Secure token-based authentication
- **Swagger/OpenAPI**: API documentation and testing
- **Dependency Injection**: Built-in .NET DI container
- **HTTP Client**: For SOAP service communication
- **XML Processing**: SOAP envelope creation and parsing

## System Requirements

### Development Environment
- **Visual Studio 2022** (version 17.8 or later) or **Visual Studio Code**
- **.NET 9.0 SDK** or later
- **Windows 10/11**, **macOS**, or **Linux**

### Runtime Requirements
- **.NET 9.0 Runtime**
- **HTTPS Certificate** (for production)
- **Network Access** to target SOAP services

### Optional Tools
- **Postman** or similar HTTP client for API testing
- **Git** for version control
- **Docker** (for containerized deployment)

## Getting Started

### 1. Clone and Setup

```powershell
# Clone the repository
git clone <repository-url>
cd ProductSoapIntegration

# Restore NuGet packages
dotnet restore
```

### 2. Configuration

#### JWT Settings (appsettings.json)
```json
{
  "JwtSettings": {
    "SecretKey": "YourSecretKeyHere",
    "Issuer": "ProductAPI",
    "Audience": "ProductAPIClients",
    "ExpirationInMinutes": 60
  }
}
```

#### Development Environment (appsettings.Development.json)
```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  }
}
```

### 3. Build and Run

#### Using Visual Studio
1. Open `ProductsSoapIntegration.sln`
2. Set `ProductApi` as startup project
3. Press `F5` to run with debugging

#### Using Command Line
```powershell
# Navigate to API project
cd ProductApi

# Run the application
dotnet run

# Or run with specific profile
dotnet run --launch-profile "https"
```

#### Using VS Code Tasks
```powershell
# Use predefined tasks
# Ctrl+Shift+P -> "Tasks: Run Task"
# - build: Build the project
# - watch: Run with hot reload
# - clean: Clean build artifacts
# - restore: Restore NuGet packages
```

### 4. Access the Application

- **API Base URL**: `https://localhost:7000` or `http://localhost:5000`
- **Swagger UI**: `https://localhost:7000` (root path)
- **OpenAPI Spec**: `https://localhost:7000/swagger/v1/swagger.json`

## API Usage Guide

### Authentication

#### 1. Obtain JWT Token

**Endpoint**: `POST /api/auth/login`

**Request**:
```json
{
  "username": "admin",
  "password": "password123"
}
```

**Response**:
```json
{
  "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...",
  "expiresAt": "2025-10-15T15:30:00Z",
  "tokenType": "Bearer",
  "username": "admin"
}
```

#### 2. Validate Token

**Endpoint**: `GET /api/auth/validate`

**Headers**:
```
Authorization: Bearer <your-jwt-token>
```

**Response**:
```json
{
  "valid": true,
  "username": "admin",
  "expiresAt": "2025-10-15T15:30:00Z"
}
```

### Product Operations

#### Get Product Details

**Endpoint**: `GET /api/products/{productId}`

**Headers**:
```
Authorization: Bearer <your-jwt-token>
```

**Example Request**:
```
GET /api/products/123
Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...
```

**Response**:
```json
{
  "productId": "123",
  "name": "Sample Product",
  "description": "This is a sample product description.",
  "price": 19.99
}
```

### Error Responses

#### 400 Bad Request
```json
{
  "message": "Product ID cannot be empty"
}
```

#### 401 Unauthorized
```json
{
  "message": "Invalid username or password"
}
```

#### 404 Not Found
```json
{
  "message": "Product not found"
}
```

## Testing the API

### Using HTTP Files

The project includes `.http` files for testing:

#### Authentication Testing (`Auth.http`)
```http
### 1. Login
POST https://localhost:7000/api/auth/login
Content-Type: application/json

{
  "username": "admin",
  "password": "password123"
}

### 2. Get Product (with auth)
GET https://localhost:7000/api/products/123
Authorization: Bearer YOUR_TOKEN_HERE
```

### Using Swagger UI

1. Navigate to `https://localhost:7000`
2. Click **"Authorize"** button
3. Enter `Bearer <your-token>` in the authorization field
4. Test endpoints through the interactive interface

### Using Postman

1. **Create Collection**: Import the API endpoints
2. **Set Authorization**: Configure Bearer Token at collection level
3. **Environment Variables**: Set base URL as `{{baseUrl}}`
4. **Pre-request Scripts**: Auto-generate tokens if needed

### Using cURL

```bash
# Get token
curl -X POST "https://localhost:7000/api/auth/login" \
  -H "Content-Type: application/json" \
  -d '{"username":"admin","password":"password123"}'

# Use token to get product
curl -X GET "https://localhost:7000/api/products/123" \
  -H "Authorization: Bearer <your-token>"
```

## SOAP Integration Details

### Current Implementation

The SOAP adapter currently uses a **mock implementation** for development:

```csharp
public ProductDetails GetProductDetails(string productId)
{
    // Mock implementation - returns sample data
    return new ProductDetails
    {
        ProductId = productId,
        Name = "Sample Product",
        Description = "This is a sample product description.",
        Price = 19.99m
    };
}
```

### Production Implementation

To connect to a real SOAP service, uncomment and configure the `SoapClient`:

```csharp
public ProductDetails GetProductDetails(string productId)
{
    SoapClient soapClient = new SoapClient("https://your-soap-endpoint.com/service");
    return soapClient.GetProductDetails(productId);
}
```

### SOAP Client Configuration

The `SoapClient` supports:
- **Custom SOAP Envelopes**: Flexible XML structure
- **HTTP Headers**: SOAPAction and custom headers
- **Error Handling**: Comprehensive exception management
- **Response Parsing**: XML to object mapping

## Configuration Options

### JWT Settings

```json
{
  "JwtSettings": {
    "SecretKey": "YourSecretKey",           // Minimum 32 characters
    "Issuer": "ProductAPI",                // Token issuer
    "Audience": "ProductAPIClients",       // Target audience
    "ExpirationInMinutes": 60              // Token lifetime
  }
}
```

### Logging Configuration

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning",
      "Product.Soap.Adapter": "Debug"
    }
  }
}
```

### CORS Configuration (if needed)

Add to `Program.cs`:
```csharp
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        policy => policy.WithOrigins("https://localhost:3000")
                        .AllowAnyHeader()
                        .AllowAnyMethod());
});
```

## Security Considerations

### Authentication
- **JWT Tokens**: Use strong secret keys (minimum 256 bits)
- **Token Expiration**: Configure appropriate lifetime
- **HTTPS Only**: Enforce HTTPS in production
- **Secret Management**: Use Azure Key Vault or similar

### Authorization
- **Role-based Access**: Implement role claims
- **Resource-based**: Add resource-specific permissions
- **API Rate Limiting**: Implement throttling

### Production Hardening
```csharp
// In production
options.RequireHttpsMetadata = true;
options.SaveToken = false;
options.TokenValidationParameters.ClockSkew = TimeSpan.Zero;
```

## Deployment Guide

### IIS Deployment

1. **Publish Application**:
   ```powershell
   dotnet publish -c Release -o ./publish
   ```

2. **Configure IIS**:
   - Create Application Pool (.NET CLR Version: No Managed Code)
   - Deploy files to `wwwroot`
   - Configure HTTPS bindings

### Docker Deployment

```dockerfile
# Dockerfile
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY . .
RUN dotnet restore
RUN dotnet build -c Release -o /app/build

FROM build AS publish
RUN dotnet publish -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ProductApi.dll"]
```

### Cloud Deployment (Azure)

1. **App Service**: Deploy directly from Visual Studio
2. **Container Instances**: Use Docker image
3. **Kubernetes**: Deploy with Helm charts

## Troubleshooting

### Common Issues

#### JWT Token Issues
- **Problem**: "Invalid token" errors
- **Solution**: Check secret key configuration and token expiration

#### SOAP Connection Issues
- **Problem**: SOAP service unreachable
- **Solution**: Verify network connectivity and endpoint URLs

#### Build Errors
- **Problem**: Missing references
- **Solution**: Run `dotnet restore` and check project references

### Debugging Tips

1. **Enable Detailed Logging**:
   ```json
   {
     "Logging": {
       "LogLevel": {
         "Default": "Debug"
       }
     }
   }
   ```

2. **Use Developer Exception Page**:
   ```csharp
   if (app.Environment.IsDevelopment())
   {
       app.UseDeveloperExceptionPage();
   }
   ```

3. **Inspect JWT Claims**:
   ```csharp
   var claims = HttpContext.User.Claims;
   ```

## Contributing

### Development Setup
1. Fork the repository
2. Create feature branch
3. Follow coding standards
4. Add unit tests
5. Submit pull request

### Code Standards
- **Naming**: PascalCase for public members, camelCase for private
- **Documentation**: XML comments for public APIs
- **Testing**: Minimum 80% code coverage
- **Security**: Follow OWASP guidelines

## License

This project is licensed under the MIT License - see the LICENSE file for details.

## Support

For support and questions:
- **Issues**: Create GitHub issues for bugs
- **Discussions**: Use GitHub discussions for questions
- **Documentation**: Refer to inline code comments and this README

---

## Quick Reference

### Default Credentials
- **Username**: Any username
- **Password**: `password123`

### Key Endpoints
- **Login**: `POST /api/auth/login`
- **Validate**: `GET /api/auth/validate`
- **Products**: `GET /api/products/{id}`
- **Swagger**: `GET /` (root)

### Project Structure
```
ProductApi/           # Web API layer
Product.Domain/       # Business logic layer
Product.Soap.Adapter/ # External integration layer
Product.IoC/          # Dependency injection configuration
```