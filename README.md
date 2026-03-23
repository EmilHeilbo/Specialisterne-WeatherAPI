# Specialisterne WeatherAPI

![ASP.NET Core 10](https://img.shields.io/badge/ASP.NET_Core_10-682A7B?style=for-the-badge&logo=.net&logoColor=white)
[![SvelteKit](https://img.shields.io/badge/sveltekit-%23f1413d.svg?style=for-the-badge&logo=svelte&logoColor=white)](https://svelte.dev)

[![License: AGPL v3](https://img.shields.io/badge/License-AGPL_v3-blue?style=for-the-badge)](https://www.gnu.org/licenses/agpl-3.0)
[![Conventional Commits](https://img.shields.io/badge/Conventional%20Commits-1.0.0-%23FE5196?style=for-the-badge&logo=conventionalcommits&logoColor=white)](https://conventionalcommits.org)

[![Coded By A Human, Not By AI](https://notbyai.fyi/img/developed-by-human-not-by-ai-white.svg)](https://notbyai.fyi)

This is a read-only weather API created during my 8th week at [Specialisterne Academy](https://specialisterne.com).

It's built to integrate with [Specialisterne-Case---ETL-Pipeline](https://github.com/NervousPapaya/Specialisterne-Case---ETL-Pipeline.git), which should be added as a submodule to this repository.

The final project will be runnable in Docker using the `compose.yaml` file.

## Build / Run

_Instructions to be added at a later date._

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