# Changelog

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
