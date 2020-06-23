# Wykorzystane biblioteki (wstęp)

W projekcie zostały zasadniczo wykorzystane dwie biblioteki:
- `System.Data.SQLite` - do obsługi bazy danych
- `System.Xml` - do parsowania pliku XML

# System.Data.SQLite

## Źródła:

> [Strona System.Data.SQLite](https://system.data.sqlite.org/index.html/doc/trunk/www/index.wiki) <br/>
> [Strona Microsoft](https://docs.microsoft.com/pl-pl/dotnet/standard/data/sqlite/compare)

## Omówienie:

Biblioteka System.Data.SqLite wykorzystwana jest w naszym projekcie do korzystania z bazy danych SQLite. Wykorzystywana jest ona w projekcie do utworzenia pliku z bazą danych `"archive.db"` wraz z tabelą, dodawania rekordów oraz wykonywania zapytań SQLite.

---

## Otwieranie połączenia z bazą danych:

Aby skorzystać z bazy danych trzeba najpierw ustanowić z nią połączenie:

```csharp
using (var connection = new SQLiteConnection("Data Source=" + dbFilePath))
{
    //...
}
```

W stringu podawanym do konstruktora można opcjonalnie dodać wyrażenie `Mode=ReadWriteCreate`, które spowoduje utworzenie bazy danych, jeżeli takowa nie istnieje.

W powyrzszym przypadku wartość argumentu przekazywana do konstruktora klasy `SQLiteConnection` będzie wyglądała tak:

```csharp
"Data Source=" + dbFilePath + ";Mode=ReadWriteCreate;Version=3"
```

> `Version=3` jest opcjonalne, my użylismy go tak dla pewności

Po utworzeniu obiektu połączenia trzeba je otworzyć:

```csharp
connection.Open();
```

Po wykonaniu tych czynności można przejść do wykonywania poleceń na bazie danych

---

## Załadowanie polecenia:

Przed wykonaniem polecenia, czy ładowaniem argumentów do polecenia, trzeba wczytać jego treść do obiektu `command`

```csharp
query = "CREATE TABLE transactions .....";
...
command.CommandText = query;
```

Aby móc wykonywać polecenia trzeba uzystkać dostęp do obiektu typu `SQLiteCommand`. Aby to osiągnąć mozna skorzystać z metody `CreateCommand()` klasy `SQLiteConnection`. Robi się to tak:

```csharp
var command = connection.CreateCommand();
```

> obiekt `connection` został utworzony w poprzednim punkcie

Sama biblioteka pozwala na to, żeby w samej składni polecenie SQLite umieścić specjalne znaczniki (zazwyczaj oznaczone z przodu symbolem `@`), które mozna wypełnić wartościami przekazanymi jako argument do polecenia. Oznacza to, że mozna dodać wartości do polecenia SQLite ze zmiennych

> Ważne jest to, że nie działa to jeżeli chodzi o nazwę tabeli, która musi być z góry znana (dowiedzielismy sie o tym "w praniu")

Tak wygląda **string** zawierający przygotowane polecenie SQLite wykorzystujące **"uzupełnianie"**:

```SQL
INSERT INTO transactions (fromName, fromID, fromType, fromCardID, fromCardType, fromBankName, fromBankID, toName, toID, toType, toCardID, toCardType, toBankName, toBankID, amount, bankActionResult) VALUES (@fromName, @fromID, @fromType, @fromCardID, @fromCardType, @fromBankName, @fromBankID, @toName, @toID, @toType, @toCardID, @toCardType, @toBankName, @toBankID, @amount, @bankActionResult);
```

Ja można zauważyć wykorzystana została notacja ze znakiem `@`

Aby załadować wartości oznaczone znakiem `@` trzeba skorzystać z metody `AddWithValue(key, value)`

```csharp
command.Parameters.AddWithValue("@fromName", fronName.ToString());
```

Podobnie należy postępować dla pozostałych wartości oznaczonych `@`

---

## Wykonywanie poleceń (non-query):

Aby wykonać komendę (która nie jest query) należy skorzystać z metody `ExecuteNonQuery()` klasy `SQLiteCommand`

```csharp
command.ExecuteNonQuery();
```

---

## Wykonywanie poleceń (query):

Wykonywanie poleceń typu query do poleceń non-query różni się tym, że żeby skorzystać z komendy korzysta się z metody `ExecuteReader()`, która zwraca obiekt typu `SQLiteDataReader`. Pozwala on na czytanie rekord po rekordzie tych rekordów, które pasują do zapytania.

Przykładowe czytanie rekordów:

```csharp
using (var reader = command.ExecuteReader())
{
    while (reader.Read())
    {
        reader.GetString(1);
    }
}
```

Metoda `Read()` czyta kolejny rekord, a metoda `GetString(index)` pozwala odczytać wartość typu `string` z kolumny o podanym indeksie

# System.Xml

## Źródła:

> [Strona System.Xml z przestrzenią nazw](https://docs.microsoft.com/pl-pl/dotnet/api/system.xml?view=netcore-3.1) <br/>
> [XmlReader - strona Microsoft](https://docs.microsoft.com/pl-pl/dotnet/api/system.xml.xmlreader?view=netcore-3.1) <br/>
> [XmlWriter - strona Microsoft](https://docs.microsoft.com/pl-pl/dotnet/api/system.xml.xmlwriter?view=netcore-3.1)

## Omówienie:

Biblioteka System.Xml wykorzystana została w naszym projekcie do odczytywania i zapisywania stanu systemu do pliku `system_state.xml`. Pozwala ona na łatwe kostruowanie plików `xml` oraz ich czytanie/parsowanie

---

## Czytanie z pliku XML:

Aby móc czytać z pliku `xml` trzeba utworzyć obiekt typu `XmlReader`, który zawiera potrzebne metody. Mozna zrobic to w następujący sposób:

```csharp
using (XmlReader reader = XmlReader.Create(fileStream))
{
    while (reader.Read())
    {
        //...
    }
}
```

> Aby `XmlReader` mógł działać trzeba w metodzie `Create(input)` dać obiekt typu `Stream` do czytania z pliku

Obiekt `reader` klasy `XmlReader` zapewnia wiele przydatnych funkcjonalności:

- `reader.NodeType` zawiera informację o tym w jakiego typu elemencie obecnie znajduje się reader dla np. 
    ```xml
    <element>
    ```
    jest to `XmlNodeType.Element`, a dla
    ```xml
    </element>
    ```
    jest to `XmlNodeType.EndElement`

- `reader.HasAttributes` mówi o tym, czy w obecnym obecnym momencie są dostępne jakies atrybuty do czytania np. dla
    ```xml
    <bank name="Bank1" id="0">
    ```
    wartość `reader.HasAttributes` będzie równa `true`

- `reader.GetAttribute(name)` pozwala pobrać wartość atrubutu o danej nazwie. Jeżeli trybut nie istnieje to metoda zwraca `null`. <br/> <br/> Przykład: <br/>
    ```xml
    XML:
    <bank name="Bank1" id="0">
    ```
    ```csharp
    C#:
    var bankName = reader.GetAttribute("name");
    ```

---

## Zapisywanie do pliku XML:

Aby móc zapisywać do pliku `xml` trzba utworzyć obiekt typu `XmlWriter`. Metoda `Create(stringBuilder, settings)` tworząca ten obiekt z klasy `XmlWriter` może opcjonalnie przyjąć argument typu `XmlWriterSettings`. Pozwala on dkoładniej zdefiniować zachowanie obiektu.

Przykład utworzenia obiektu typu `XmlWriterSettings`:

```csharp
XmlWriterSettings settings = new XmlWriterSettings
{
    Indent = true,
    Encoding = new UTF8Encoding(false),
    OmitXmlDeclaration = true
};
```

W powyższym przykładzie (wziętym z naszego kodu) ustawiane są pewne informacje o tym, jak ma być generowany kod `xml`. 

Omównienie powyższych ustawień:

- `Indent` mówi czy w wyjściowym kodzie `xml` mają być wcięcia, czy kod ma być generowany "ciurkiem"

- `Encoding` ustala w jakim kodowaniu będzie kod `xml`. W naszym przypadku jest to UTF-8

- `OmitXmlDeclaration` mówi, żeby `XmlWriter` nie dodawał na początku pliku kodu
    ```xml
    <?xml version="1.0" encoding="utf-8">
    ```

Obiekt typu `XmlWriter` można utworzyć w następujący sposób:

```csharp
using (XmlWriter writer = XmlWriter.Create(xmlContent, settings))
{
    //...
}
```

> Gdzie obiekt `xmlContent` jest obiektem typu `StringBuilder`, a `settings` jest typu `XmlWriterSettings`

Obiekt `writer` oferuje wiele przydatnych metod, np.

- `WriterStartElement(name)`, która tworzy element początkowy o podanej nazwie np.
    ```xml
    <bank>
    ```

- `WriteAttributeString(name, value)`, które dodaje atrybut o podanej nazwie i wartości do obecnego elementu. Np. dla elementu
    ```xml
    <bank>
    ```
    Wywołanie metody `WriteAttributeString("id", "1")` (będąc ciągle w elemencie `bank`) spowoduje powstanie takiego kodu `xml`
    ```xml
    <bank id="1">
    ```
- `WriteEndElement()` powoduje dodanie tagu koncowego do obecnego elementu. Np. jeżeli `writer` obecnie znajduje się w elemencie `bank`, wywołanie metody `WriteEndElement()` spowoduje dodanie kodu:
    ```xml
    </bank>
    ```

Przykład utworzenia pliku `xml`:

```csharp
using (XmlWriter writer = XmlWriter.Create(xmlContent, settings))
{
    writer.WriteStartElement("system");
    
    writer.WriteStartElement("bank");
    writer.WriteAttributeString("name", "Bank1");
    writer.WriteAttributeString("id", "11");

    writer.WriteStartElement("card");
    writer.WriteAttributeString("number", "1");
    writer.WriteAttributeString("ownerName", "Jan Kowalski");

    writer.WriteEndElement(); // </card>
    writer.WriteEndElement(); // </bank>
    writer.WriteEndElement(); // </system>
}
```

Spowoduje powstanie pliku `xml` o treści:

```xml
<system>
    <bank name="Bank1" id="11">
        <card number="1" ownerName="Jan Kowlaski"/>
    </bank>
</system>
```

> Warto zauważyć, że `writer` jest na tyle "inteligentny", że wykrywa zbedne tagi `</element>` i zamiast tworzyć parę `<element></element>` od razu zwija ją do `<element/>`