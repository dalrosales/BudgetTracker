# BudgetTracker

BudgetTracker is a web application built with ASP.NET 8 MVC to manage budgets and track expenses effectively. The project integrates Webpack for front-end asset bundling. This is a personal project in the beginning stages of development.

## Prerequisites

- Node.js and NPM - Required for Webpack bundling
- .NET SDK 8 - Required to build and run the application

## Installation

1) Clone the repository: 

	`git clone https://github.com/yourusername/BudgetTracker.git`

2) Navigate to the project directory:

	`cd BudgetTracker`

3) Install npm dependencies:

	`npm install`

4) Restore .NET dependencies:

	`dotnet restore`

## Development

1) Start Webpack Dev Server

	To start the Webpack Dev Server with hot-reloading:

	`npm run dev`

	By default, it runs on [http://localhost:3000](http://localhost:3000).

2) Run the ASP.NET Application

	In Visual Studio, run the project on HTTPS to access the application at [https://localhost:7056](https://localhost:7056). 

	Ensure Webpack Dev Server is running to see front-end changes live.

## Production Build

To build for production, generating optimized assets:

`npm run build`

This outputs bundled files to wwwroot/dist.

## License
This project is licensed under the MIT License.
