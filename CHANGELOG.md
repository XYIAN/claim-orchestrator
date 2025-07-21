# Changelog

## [1.1.2] - Custom Favicon Addition

### Added

- Custom favicon.ico for the application
- Updated favicon configuration in layout and web manifest
- Simplified favicon references for better browser compatibility

## [1.1.1] - Code Quality and Warning Fixes

### Fixed

- Resolved all async method warnings by properly implementing Task-based returns
- Fixed CS1998 warnings in ValidationService and EligibilityService
- Improved code quality and maintainability
- Ensured clean build with zero warnings

## [1.1.0] - Enhanced UI and SEO Optimization

### Added

- Comprehensive SEO optimization with meta tags, Open Graph, and Twitter Cards
- Enhanced UI with glossy design, glassmorphism effects, and animations
- Modular architecture with separate validation and eligibility services
- XML documentation for all models, controllers, and services
- Swagger/OpenAPI documentation with comprehensive API docs
- Drag-and-drop file upload functionality
- Enhanced error handling and logging
- Security headers and CORS configuration
- PWA support with web manifest
- Robots.txt for SEO optimization
- AutoMapper integration for object mapping
- FluentValidation for enhanced model validation
- Health check endpoint
- Print functionality for claim details
- CSV export functionality
- Enhanced timeline view for processing logs
- Statistics cards and dashboard improvements
- Breadcrumb navigation
- Loading states and interactive feedback
- Comprehensive privacy policy page

### Enhanced

- All views with modern UI components and better UX
- Controllers with proper error handling and logging
- Models with comprehensive validation attributes
- Layout with structured data and SEO meta tags
- JavaScript functionality with animations and interactions
- CSS with advanced styling and responsive design
- Project configuration with XML documentation generation

### Fixed

- Improved error handling throughout the application
- Better responsive design for mobile devices
- Enhanced accessibility features
- Optimized performance with proper caching

## [1.0.0] - Initial Release

### Added

- .NET 8 MVC project structure
- Entity Framework Core with SQLite database
- Models: Claim, ProcessingLog
- Database migrations and seeding with sample claims
- ClaimProcessingService for validation, deduplication, eligibility
- ClaimsController with routes for:
  - Listing all claims
  - Viewing claim details and logs
  - Uploading CSV or demo claims
  - Manually triggering processing steps
- Razor Views with Bootstrap and FontAwesome icons
- Warm neutral color theme for professional admin UI
- User-focused README and basic sitemap.xml

## [Unreleased]

### Added

- GenericDataAccess class library: Implements a generic repository pattern using Dapper and Microsoft.Data.SqlClient for MSSQL.
  - Supports async CRUD, filter, and cursor-based pagination.
  - Includes IEntity and IGenericRepository<T> interfaces.
  - Clean code, separation of concerns, and extensible design.
- GenericDataAccess.SampleApp: Console app demonstrating usage with a sample Person entity.
- Enhanced animations and transitions throughout the application:
  - Page transition effects with smooth loading states
  - Skeleton loading screens for better perceived performance
  - Enhanced button interactions with ripple effects and loading spinners
  - Form animations with focus states and validation feedback
  - Table row animations with staggered entrance effects
  - Card animations with intersection observer for scroll-triggered effects
  - Parallax scrolling effects for background elements
  - Enhanced alert system with auto-dismiss and smooth animations
  - Progress bar animations and enhanced pagination
  - Improved accessibility with focus states and smooth scrolling
