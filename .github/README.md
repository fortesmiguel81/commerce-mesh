# CI/CD Pipeline Setup Guide

## Overview
This repository includes a complete CI/CD pipeline using GitHub Actions with support for:
- .NET 9.0 services (Order Service)
- Java Spring Boot services (Payment Service - future)
- Node.js services (Notification Service - future)
- Aspire orchestration and manifest generation
- Security scanning and dependency management
- Multi-environment deployment (Staging → Production)

## Required Repository Secrets

### Azure Deployment Secrets
```
AZURE_CREDENTIALS           # Service Principal for staging environment
AZURE_CREDENTIALS_PROD      # Service Principal for production environment
AZURE_RESOURCE_GROUP        # Resource group for staging
AZURE_RESOURCE_GROUP_PROD   # Resource group for production
AZURE_REGION               # Azure region (e.g., eastus)
```

### Application Secrets
```
STAGING_URL                # URL of staging environment for integration tests
GITHUB_TOKEN              # Automatically provided by GitHub Actions
```

## Setting Up Azure Service Principal

1. Create Service Principal for staging:
```bash
az ad sp create-for-rbac --name "CommerceMesh-Staging" \
  --role contributor \
  --scopes /subscriptions/{subscription-id}/resourceGroups/{resource-group} \
  --sdk-auth
```

2. Create Service Principal for production:
```bash
az ad sp create-for-rbac --name "CommerceMesh-Production" \
  --role contributor \
  --scopes /subscriptions/{subscription-id}/resourceGroups/{resource-group-prod} \
  --sdk-auth
```

3. Add the JSON output as repository secrets.

## Workflow Files

### Main CI/CD Pipeline (`.github/workflows/ci-cd.yml`)
- **Triggers**: Push/PR to main/develop branches
- **Jobs**:
  - Build and test .NET services
  - Build Java services (when present)
  - Build Node.js services (when present)
  - Security scanning
  - Generate Aspire deployment manifests
  - Build and push container images
  - Deploy to staging environment
  - Run integration tests
  - Deploy to production (manual approval)

### Security Scanning (`.github/workflows/security.yml`)
- **Triggers**: Daily schedule, push to main, manual
- **Features**:
  - CodeQL analysis for C# and JavaScript
  - OWASP dependency checking
  - Container vulnerability scanning
  - .NET security analysis

### Dependency Management (`.github/workflows/dependencies.yml`)
- **Triggers**: Weekly schedule, manual
- **Features**:
  - Automated dependency updates for .NET, Java, Node.js
  - Creates pull requests for review
  - Separate workflows for each technology stack

## Container Images

Container images are built and pushed to GitHub Container Registry:
- `ghcr.io/{owner}/commercemesh/order-service-api`
- `ghcr.io/{owner}/commercemesh/payment-service`
- `ghcr.io/{owner}/commercemesh/notification-service`

## Environment Configuration

### Staging Environment
- Automatic deployment on main branch
- Integration tests run post-deployment
- Uses Azure Container Apps for hosting

### Production Environment
- Manual approval required
- Deploys after successful staging deployment and tests
- Blue-green deployment strategy

## Local Development

### Using Aspire (Recommended)
```bash
cd src/AppHost
dotnet run
```

### Using Docker Compose (Fallback)
```bash
# All services
docker-compose up

# Only .NET services
docker-compose up postgres redis order-service

# Include Java services
docker-compose --profile java up

# Include Node.js services
docker-compose --profile nodejs up
```

## Branch Protection Rules

Recommended branch protection for `main`:
- Require status checks: `dotnet-build-test`, `security-scan`
- Require pull request reviews: 1 reviewer minimum
- Dismiss stale reviews when new commits are pushed
- Require branches to be up to date before merging

## Monitoring and Alerts

The pipeline includes:
- Test result uploads and reporting
- Code coverage tracking with Codecov
- Security vulnerability alerts
- Dependency update notifications

## Next Steps

1. Set up repository secrets in GitHub
2. Configure Azure resources for staging/production
3. Enable branch protection rules
4. Review and customize environment-specific configurations
5. Add service-specific health checks and monitoring
