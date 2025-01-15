# Testowanie i Jakość Oprogramowania

## Autor

Jakub Piekielniak

## Temat projektu

Testowanie aplikacji umożliwiającej zarządzanie pojazdami

## Opis projektu

Implementacja aplikacji webowej, która umożliwia użytkownikom dodawanie i zarządzanie informacjami o swoich pojazdach.

Aplikacja umożliwia:

- Planowanie przeglądów technicznych,
- Śledzenie historii serwisowej i kosztów utrzymania,
- Wysyłanie powiadomień dotyczących ważnych terminów, takich jak przeglądy czy ubezpieczenia,
- Zarządzanie pojazdami.

Projekt obejmuje również implementację testów jednostkowych i integracyjnych, aby zapewnić wysoką jakość i niezawodność oprogramowania.

## Uruchomienie projektu

### Backend

1. Przejdź do katalogu:

    ```bash
    cd VehicleManager/src/VehicleManager.api
    ```

2. Wykonaj polecenie:

    ```bash
    dotnet run
    ```
    
Backend uruchomi się domyślnie pod adresem localhost:5189

### Frontend

1. Przejdź do katalogu:

    ```bash
    cd VehicleManagerClient/src
    ```

2. Wykonaj polecenie:

    ```bash
    ng serve
    ```

3. W przeglądarce wpisz:

    ```bash
    localhost:4200
    ```

Frontend uruchomi się domyślnie pod tym adresem.

## Testy

### Testy jednostkowe

Testy jednostkowe znajdują się w lokalizacji:

- **VehicleManager/tests/VehicleManager.Tests.Unit**

Podzielone na foldery w zależności od kategorii:

- **Admin**
- **ServiceBook**
- **Users**
- **Vehicles**
- **Common/Emails**

### Testy integracyjne

Testy integracyjne znajdują się w lokalizacji:

- **VehicleManager/tests/VehicleManager.Tests.Integration**

Podzielone na foldery w zależności od kategorii:

- **ServiceBooks**
- **Users**
- **Vehicles**

## Dokumentacja API

Po uruchomieniu projektu (backendu), dokumentacja API dostępna jest pod adresem:

- [http://localhost:5189/swagger/index.html](http://localhost:5189/swagger/index.html)

## Przypadki testowe dla testera manualnego (TestCase)

### Zawartość

| ID    | Tytuł                               | Warunki początkowe                                       | Kroki testowe                                                              | Oczekiwany rezultat                                 |
| ----- | ----------------------------------- | -------------------------------------------------------- | -------------------------------------------------------------------------- | --------------------------------------------------- |
| TC001 | Dodawanie pojazdu                   | Użytkownik jest zalogowany, otwarte menu "Moje pojazdy"       | 1. Kliknij "Dodaj pojazd". 2. Wprowadź dane pojazdu. 3. Zapisz.            | Pojazd zostaje dodany i widoczny na liście          |
| TC002 | Usuwanie pojazdu                    | Użytkownik jest zalogowany, lista pojazdów posiada wpisy | 1. Wybierz pojazd. 2. Kliknij "Usuń". 3. Potwierdź usunięcie.              | Pojazd zostaje usunięty z listy                     |
| TC003 | Wyświetlanie szczegółów użytkownika | Użytkownik jest zalogowany       | 1. Otwórz menu "Moje konto". 2. Przejrzyj informacje.                   | Wyświetlane są szczegółowe informacje o użytkowniku |
| TC004 | Edycja informacji o pojeździe       | Użytkownik jest zalogowany, otwarte szczegóły pojazdu    | 1. Kliknij "Edytuj". 2. Zmień dane. 3. Zapisz zmiany.                      | Dane pojazdu zostają zaktualizowane                 |
| TC005 | Usuwanie konta                      | Użytkownik jest zalogowany, otwarte menu "Moje konto"        | 1. Kliknij "Usuń konto". 2. Potwierdź decyzję.                             | Konto zostaje usunięte                              |
| TC006 | Wyświetlanie historii serwisowej    | Użytkownik jest zalogowany, otwarte szczegóły pojazdu    | 1. Kliknij "Historia serwisowa".                                           | Wyświetlana jest lista przeglądów i napraw          |
| TC007 | Uzupełnianie danych opcjonalnych użytkownika    | Użytkownik jest zalogowany, otwarte szczegóły konta    | 1. Kliknij "Edytuj". 2. Dodaj brakujące dane opcjonalne. 3. Zapisz zmiany. | Dane użytkownika zostają uzupełnione                    |
| TC008 | Rejestracja użytkownika             | Strona rejestracji jest otwarta                          | 1. Wypełnij formularz rejestracji. 2. Kliknij "Zarejestruj się".               | Konto użytkownika zostaje utworzone                 |
| TC009 | Logowanie użytkownika               | Strona logowania jest otwarta                            | 1. Wprowadź login i hasło. 2. Kliknij "Zaloguj się".                           | Użytkownik zostaje zalogowany                       |
| TC010 | Resetowanie hasła                   | Strona resetowania hasła jest otwarta                    | 1. Wprowadź adres e-mail. 2. Kliknij "Wyślij link".                      | Link do resetowania hasła zostaje wysłany na e-mail |

## Technologie użyte w projekcie

- **Backend**: C# .NET
- **Frontend**: Angular
- **Baza danych**: PostgreSQL
- **Przechowywanie zdjęć**: Azure Blob Storage
- **Dokumentacja API**: Swagger

