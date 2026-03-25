# Specialisterne WeatherAPI

[![Conventional Commits](https://img.shields.io/badge/Conventional%20Commits-1.0.0-%23FE5196?style=for-the-badge&logo=conventionalcommits&logoColor=white)](https://conventionalcommits.org)
[![License: AGPL v3](https://img.shields.io/badge/License-AGPL_v3-blue?style=for-the-badge)](https://www.gnu.org/licenses/agpl-3.0)

![ASP.NET Core 10](https://img.shields.io/badge/ASP.NET_Core_10-682A7B?style=for-the-badge&logo=.net&logoColor=white)
[![SvelteKit](https://img.shields.io/badge/sveltekit-%23f1413d.svg?style=for-the-badge&logo=svelte&logoColor=white)](https://svelte.dev)

This is a read-only weather API created during my 8th week at [Specialisterne Academy](https://specialisterne.com).

It's built to integrate with [ETL-Pipeline](https://github.com/NervousPapaya/Specialisterne-Case---ETL-Pipeline.git), which has been added as a submodule to this repository.

The final project will be runnable in Docker using the `compose.yaml` file.

## Build / Run

### ⚠️ _Project is currently in development, and may not function_ ⚠️

1. Clone this project and pull the submodules as well.
2. Configure `ETL-Pipeline` according to the README found within.
   1. Copy `ETL-Pipeline/.env.template` to `ETL-Pipeline/.env`
   2. Register an account and get an API key for the services listed in the README
   3. Paste your API keys into `ETL-Pipeline/.env`
   4. Update the DB_USER, DB_PASSWORD and DB_NAME variables for slightly improved security
3. Install Docker (or another OCI Runtime) on the PC/Server you plan on using.
   1. You can check if it's installed by running `docker-compose -v`
4. Start the project with `docker-compose up -d`; this'll daemonize the process, meaning it runs in the background instead of being dependant on the active terminal session.
   1. To close the application, simply run `docker-compose down`


## Goals

- Learning about ETL and API integrations
- Display my ability to implement new API endpoints for an existing database schema and/or data application
- Show I can implement a frontend for an OpenAPI application

## To Do

- [ ] Examine and integrate database schema as DTOs
- [ ] Map endpoints to fetch the following
  - List of weather stations
  - Get measurements from a specific station
  - Get and compare measurements between stations
- [ ] Add filters to endpoints using query parameters
  - From date
  - To date
  - Data type
- [ ] Add Swagger, Scalar or similar to provide API documentation
- [ ] Thorough unit testing of the API backend
- [ ] Containerize and test using a CI/CD pipeline
- [ ] Add API Authentication/Authorization, rate-limiting, and caching
- [ ] Add a nice, simple frontend using SvelteKit

[![Developed By A Human, Not By AI](https://notbyai.fyi/img/developed-by-human-not-by-ai-white.svg)](https://notbyai.fyi)