# Proiect Baze de Date – Editura

Aplicație web **ASP.NET Core Razor Pages (.NET 8)** conectată la o bază de date **SQL Server** pentru gestiunea unei edituri: edituri, autori, cărți, genuri și cataloage.

## Funcționalități
- CRUD (adăugare, vizualizare, modificare, ștergere) pentru **Autori** și **Edituri**
- Pagină cu **interogări simple** (`Pages/InterogariSimple`)
- Pagină cu **interogări complexe** (`Pages/InterogariComplexe`)

## Structura bazei de date
Baza de date `Editura` conține tabelele pentru edituri, cataloage, cărți, autori și genuri, legate prin tabele de legătură (`Legatura_Carti_Cataloage`, `Legatura_Carti_Gen`, `Legatura_Carti_Autor`).
Documentația completă (diagrame, descriere, interogări) se află în [`Docs/Document_BD_Visan_Robert.docx`](Docs/Document_BD_Visan_Robert.docx).

## Cum rulezi proiectul

### Cerințe
- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- SQL Server sau SQL Server Express (instanța `.\SQLEXPRESS`)
- SQL Server Management Studio (SSMS) pentru restaurarea bazei de date

### 1. Restaurarea bazei de date
1. Deschide SSMS și conectează-te la `.\SQLEXPRESS`.
2. Click dreapta pe **Databases → Restore Database…**
3. Alege **Device** și selectează fișierul `Database/Editura.bak`.
4. Apasă **OK**. Baza de date se va numi `Editura`.

### 2. Pornirea aplicației
```bash
dotnet restore
dotnet run
```
Apoi deschide în browser adresa afișată în terminal (implicit `http://localhost:5198`).

### Conexiunea la baza de date
Aplicația folosește autentificare Windows:
```
Data Source=.\SQLEXPRESS;Initial Catalog=Editura;Integrated Security=True;Encrypt=True;TrustServerCertificate=True
```
Dacă instanța ta SQL Server are alt nume, modifică acest șir de conexiune în fișierele din `Pages/`.

## Tehnologii
C# · ASP.NET Core Razor Pages · .NET 8 · SQL Server · System.Data.SqlClient · Bootstrap

## Autor
Vișan Robert
