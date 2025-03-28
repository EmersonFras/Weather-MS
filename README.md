# Weather Microservice

## Overview
The **Weather Microservice** is a .NET Core Web API that periodically fetches weather data, caches it in a SQLite database, and publishes updates via RabbitMQ.  

It is designed to integrate with a Raspberry Pi project that connects multiple microservices, including an LED matrix display and a backend API for a web front end.

## Features
- Runs a **hosted service** that fetches weather data every **10 minutes**.
- Caches the **temperature** and **weather code** in a **SQLite database** using **Entity Framework (EF Core)**.
- Publishes updates to a **RabbitMQ topic exchange** for other microservices to consume.  
- Exposes a **single GET HTTP endpoint** to fetch the current temperature and weather code.



