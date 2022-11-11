# MitfahrerDB

MitfahrerDB, Oberstufenprojekt FI007.

- **Fahrten anbieten** und **Fahrer finden**
- **Weniger Parkplätze** und Sprit verschwenden.
- **Interaktive Karte** um Fahrer in der Nähe zu finden und zu kontaktieren.

## Tech-Stack

### Frontend

- [React](https://github.com/facebook/react) with Context for state management
- [Material-UI](https://github.com/callemall/material-ui) Component Library
- [LeafletReact](https://github.com/PaulLeCam/react-leaflet) for interactive Map
- [ESLint](https://github.com/eslint/eslint) for linting.
- [Prettier](https://github.com/prettier/prettier) for formatting
- [Axios](https://github.com/axios/axios) for API fetching

### Backend

- Dotnet Webapi

### Datenbank
- EF Core
- Die Klassen sind in der Datei "DataBaseContext.cs" Definiert.
- Datenbank generieren:
1. (Falls noch nicht installiert)
```sh
dotnet tool install --global dotnet-ef
```
2. Migrationen anwenden
```sh
dotnet ef database update
```
## Start working

```
$ yarn start
```
