# A .NET Project Template

[![.NET](https://github.com/ovation22/Aspire.ProjectTemplate/actions/workflows/dotnet.yml/badge.svg)](https://github.com/ovation22/Aspire.ProjectTemplate/actions/workflows/dotnet.yml)

Template repository for a .NET Web Api project.

## Getting Started

1. Clone this Repository
1. Install Project Template
1. Create Project with `dotnet new`

### Clone this Repository

Clone or download this repository.

```powershell
git clone https://github.com/ovation22/Aspire.ProjectTemplate.git
```

### Install Project Template

Then run the following command to install this project template:

```powershell
dotnet new install ./Aspire.ProjectTemplate
```

You should now see the `api-project` template installed successfully.

### Install using `dotnet new`

Run the following command to create the solution structure in a subfolder named `Your.ProjectName`:

```powershell
dotnet new api-project -o Your.ProjectName
```

Optionally, you may choose to included additional parameters to better fill out your new project:

```powershell
dotnet new aspire-project -o Your.ProjectName
```

## Solution Structure

Included in the solution:

- [x] .NET Aspire
- [x] Blazor Web App
- [x] .NET 9 Web Api
