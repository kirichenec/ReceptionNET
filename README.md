# ReceptionNET
[![Build](https://github.com/kirichenec/ReceptionNET/actions/workflows/dotnet.yml/badge.svg)](https://github.com/kirichenec/ReceptionNET/actions/workflows/dotnet.yml)
[![Quality Gate Status](https://sonarcloud.io/api/project_badges/measure?project=kirichenec_ReceptionNET&metric=alert_status)](https://sonarcloud.io/summary/new_code?id=kirichenec_ReceptionNET)
[![Lines of Code](https://sonarcloud.io/api/project_badges/measure?project=kirichenec_ReceptionNET&metric=ncloc)](https://sonarcloud.io/summary/new_code?id=kirichenec_ReceptionNET)
[![Coverage](https://sonarcloud.io/api/project_badges/measure?project=kirichenec_ReceptionNET&metric=coverage)](https://sonarcloud.io/summary/new_code?id=kirichenec_ReceptionNET)

## Description
A project for notifying the boss about visitors.
Uses .NET, Avalonia, SignalR

## Roadmap
- [X] Boss logic
- [ ] Boss welcome message to settings
- [X] Visitor guid/order number
- [ ] Visitors history
- [ ] One-to-one instead of one-to-many for Hub boss-subordinate connections
- [ ] Rework ShowError by MainWindowVM link to MessageBus or something looks like di
- [ ] Up data after reconnection
- [ ] Unit-tests

## Known issues
- [ ] Client reconnection
- [ ] Localization for default controls (passwordbox without it now)
- [ ] Scroll for search result

## Project status
Prealphabetagamma :)
