# Claim Orchestrator

A modern, professional web application for streamlined claims management and processing with comprehensive SEO optimization and enhanced user experience.

## 🚀 Features

### Core Functionality

- **Claims Management**: View, upload, and process claims with real-time status tracking
- **Automated Processing**: Validation, deduplication, and eligibility checks
- **Processing Logs**: Detailed audit trail for all claim processing steps
- **CSV Import**: Drag-and-drop file upload with validation
- **Demo Mode**: Quick setup with sample data for testing

### Modern UI/UX

- **Glossy Design**: Glassmorphism effects and modern styling
- **Responsive Layout**: Optimized for desktop, tablet, and mobile
- **Interactive Elements**: Animations, loading states, and smooth transitions
- **Professional Dashboard**: Statistics cards and comprehensive overview
- **Timeline View**: Visual processing logs with status indicators

### SEO & Performance

- **Comprehensive SEO**: Meta tags, Open Graph, Twitter Cards, structured data
- **PWA Support**: Web manifest for mobile app-like experience
- **Performance Optimized**: Efficient loading and caching strategies
- **Accessibility**: WCAG compliant design and navigation

### Technical Excellence

- **API Documentation**: Swagger/OpenAPI with XML comments
- **Modular Architecture**: Clean separation of concerns
- **Error Handling**: Comprehensive logging and user-friendly error messages
- **Security**: Security headers, CORS, and input validation

## 🛠 Technology Stack

- **Backend**: .NET 8, ASP.NET Core MVC
- **Database**: Entity Framework Core with SQLite
- **Frontend**: Bootstrap 5, FontAwesome, Custom CSS/JS
- **Documentation**: Swagger/OpenAPI, XML comments
- **Validation**: FluentValidation, Data Annotations
- **Mapping**: AutoMapper for object transformations

## 📋 Requirements

- .NET 8.0 SDK or later
- Modern web browser (Chrome, Firefox, Safari, Edge)
- 100MB disk space for application and database

## 🚀 Quick Start

### 1. Clone and Setup

```bash
git clone <repository-url>
cd claim-orchestrator
```

### 2. Run the Application

```bash
dotnet run
```

### 3. Access the Application

- **Main Application**: http://localhost:5000
- **API Documentation**: http://localhost:5000/api-docs
- **Health Check**: http://localhost:5000/health

## 📖 Usage Guide

### Dashboard Overview

The main dashboard provides:

- **Statistics Cards**: Total claims, pending, processed, and total amount
- **Claims Table**: Sortable list with actions and status indicators
- **Quick Actions**: Upload, export, and refresh functionality

### Uploading Claims

1. **CSV Upload**: Drag and drop or browse for CSV files
2. **Demo Data**: Create sample claims for testing
3. **Validation**: Automatic format and size validation

#### CSV Format

```csv
ClaimNumber,ClaimantName,Address,Amount
CLM-001,John Smith,123 Main St Anytown CA 90210,2500.00
CLM-002,Jane Doe,456 Oak Ave Somewhere NY 10001,1500.00
```

### Processing Claims

Each claim can be processed through:

- **Validation**: Check required fields and data integrity
- **Deduplication**: Identify and flag duplicate claims
- **Eligibility**: Verify claim meets criteria
- **Full Processing**: Run all steps automatically

### Viewing Details

- **Claim Information**: Complete claim details with timestamps
- **Processing Logs**: Timeline view of all processing steps
- **Actions**: Individual processing steps or full automation

## 🔧 Configuration

### Environment Variables

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Data Source=claims.db"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  }
}
```

### API Endpoints

- `GET /claims` - List all claims
- `GET /claims/{id}` - Get claim details
- `POST /claims/upload` - Upload claims
- `POST /claims/{id}/process` - Process claim
- `GET /health` - Health check

## 📊 SEO Features

### Meta Tags

- Dynamic title and description for each page
- Open Graph tags for social media sharing
- Twitter Card support
- Canonical URLs

### Structured Data

- JSON-LD schema markup
- Web application structured data
- Organization and contact information

### Performance

- Optimized images and assets
- Efficient CSS and JavaScript loading
- Browser caching strategies

## 🔒 Security

- **Input Validation**: Comprehensive validation on all inputs
- **Security Headers**: XSS protection, content type options
- **CORS Configuration**: Controlled cross-origin requests
- **Error Handling**: Secure error messages without information leakage

## 📱 Mobile Support

- **Responsive Design**: Optimized for all screen sizes
- **Touch-Friendly**: Large touch targets and gestures
- **PWA Features**: Installable as mobile app
- **Offline Capability**: Basic functionality without internet

## 🧪 Testing

### Manual Testing

1. Upload sample claims
2. Process claims through all steps
3. Verify processing logs
4. Test responsive design
5. Check SEO meta tags

### Automated Testing

```bash
dotnet test
```

## 📈 Performance

- **Page Load**: < 2 seconds average
- **Database Queries**: Optimized with Entity Framework
- **Asset Loading**: Compressed and cached resources
- **API Response**: < 500ms average

## 🤝 Contributing

1. Fork the repository
2. Create a feature branch
3. Make your changes
4. Add tests if applicable
5. Submit a pull request

## 📄 License

This project is licensed under the MIT License - see the LICENSE file for details.

## 🆘 Support

- **Documentation**: Check the API docs at `/api-docs`
- **Issues**: Report bugs via GitHub issues
- **Email**: support@claimorchestrator.com

## 🔄 Version History

- **v1.1.0**: Enhanced UI, SEO optimization, modular architecture
- **v1.0.0**: Initial release with basic functionality

---

**Claim Orchestrator** - Streamlining claims management for the modern world.

## GenericDataAccess Library

A reusable .NET 8 class library for generic data access using Dapper and Microsoft.Data.SqlClient. Supports MSSQL, async CRUD, filtering, and cursor-based pagination.

### Features

- Generic repository pattern for any entity implementing `IEntity`
- Async methods for GetById, GetAll, Post, Filter, CursorPagination, Update, Delete, Count, Exists
- Clean code, extensible, and safe (parameterized queries)
- Sample console app included

### Usage

1. **Install dependencies:**

   - Dapper
   - Microsoft.Data.SqlClient

2. **Implement your entity:**

```csharp
public class Person : IEntity
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public int Age { get; set; }
    public DateTime CreatedAt { get; set; }
}
```

3. **Use the repository:**

```csharp
var connectionString = "Server=localhost;Database=SampleDb;User Id=sa;Password=your_password;TrustServerCertificate=True;";
var repository = new GenericRepository<Person>(connectionString);

// Insert
var newPerson = new Person { Name = "Alice", Age = 30, CreatedAt = DateTime.UtcNow };
var inserted = await repository.PostAsync(newPerson);

// Get all
var allPeople = await repository.GetAllAsync();

// Filter
var filtered = await repository.FilterAsync("Age > @MinAge", new { MinAge = 25 });

// Cursor pagination
var paged = await repository.CursorPaginationAsync(2);
```

### Sample App

See `GenericDataAccess.SampleApp/Program.cs` for a runnable example.

---

## Project Structure

- `GenericDataAccess/Interfaces/` — IEntity, IGenericRepository
- `GenericDataAccess/Repositories/` — GenericRepository implementation
- `GenericDataAccess.SampleApp/` — Console demo

---

## Changelog

See [CHANGELOG.md](CHANGELOG.md)
