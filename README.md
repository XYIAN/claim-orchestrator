# Claim Orchestrator

A professional, user-friendly dashboard for managing and processing class action claims.

## What is Claim Orchestrator?

Claim Orchestrator is a web-based dashboard for class action administrators to:

- View and manage all claims
- Upload new claims (CSV or demo data)
- Process claims through validation, deduplication, and eligibility steps
- View detailed processing logs for each claim

## How to Use

### 1. Launch the Dashboard

- Start the app (your admin will provide the link or local address)
- Log in if required (no login by default)

### 2. View Claims

- The home page lists all claims with their status, amount, and claimant info
- Click the eye icon to view details and logs for any claim

### 3. Upload Claims

- Click 'Upload Claims' in the top menu
- Upload a CSV file (see format below), or use the demo button to create sample claims

#### CSV Format Example

| ClaimNumber | ClaimantName | Address                          | Amount  |
| ----------- | ------------ | -------------------------------- | ------- |
| CLM-001     | John Smith   | 123 Main St, Anytown, CA 90210   | 2500.00 |
| CLM-002     | Jane Doe     | 456 Oak Ave, Somewhere, NY 10001 | 1500.00 |

### 4. Process Claims

- On the claim details page, use the buttons to:
  - Validate (checks required fields)
  - Check Duplicates (ensures no duplicate claim numbers)
  - Check Eligibility (amount between $100 and $10,000)
  - Or use 'Process All Steps' to run all at once
- Each step logs its result for auditability

### 5. View Processing Logs

- Every claim has a log of all processing steps, with status and messages
- Icons indicate success, failure, or warnings

## Features

- Minimal, professional Bootstrap UI with warm neutral colors
- FontAwesome icons for status and actions
- Fully functional on first run with sample data
- SQLite database (local file)

## Sitemap

- `/Claims` — List all claims
- `/Claims/Details/{id}` — View claim details and logs
- `/Claims/Upload` — Upload or demo claims

---

For questions or support, contact your system administrator.
