# Övn. 3 - Exceptions-övning

```cs
Övn. 3 - Exceptions-övning
--------------------------

Ni ska granska ExceptionDemo-exemplet som ligger i klassens github noga!
https://github.com/Lexicon-NET-2026-HT/ExceptionsDemo-master

Förbättra koden!
Se till att exceptions fungerar.
Framkalla alla olika typer av catch-undantag.

Lämna in en fungerande kod med en github-länk som vanligt,
i inlämningschatten.

Deadline imorgon kl. 10.00

Lycka till!
```

# Source Repo

https://github.com/Lexicon-NET-2026-HT/ExceptionsDemo-master

# List of exceptions

## Main Exceptions

```cs
FileNotFoundException
FormatException
DivideByZeroException
Exception
```

## ProcessFile() Exceptions

```cs
ArgumentException
FormatException
Exception
FileNotFoundException
DivideByZeroException
InvalidOperationException
```

# Test 0 | works perfectly | number.txt | 5

```cs
=== Start av programmet ===
Försöker läsa fil och räkna...
finally i ProcessFile: StreamReader stängd.

Resultat: 20✅
Cleanup: Logging avslutat anrop.
Programmet avslutas normalt.

D:\User\dotnet\03-ExceptionsDemo\bin\Debug\net10.0\ExceptionsDemo.exe (process 37032) exited with code 0 (0x0).
To automatically close the console when debugging stops, enable Tools->Options->Debugging->Automatically close the console when debugging stops.
Press any key to close this window . . .
```

# 🧪Test 1 | change name to 00numbers.txt | file does not exist

The test failed.

```cs
=== Start av programmet ===
Försöker läsa fil och räkna...
finally i ProcessFile: StreamReader stängd.
Okänt fel: Det gick inte att processa filen.❌
Cleanup: Logging avslutat anrop.
Programmet avslutas normalt.

D:\User\dotnet\03-ExceptionsDemo\bin\Debug\net10.0\ExceptionsDemo.exe (process 9096) exited with code 0 (0x0).
To automatically close the console when debugging stops, enable Tools->Options->Debugging->Automatically close the console when debugging stops.
Press any key to close this window . . .
```

## How to fix it

Without the second catch of ProcessFile()

```
ProcessFile()
     ↓
StreamReader tries to open numbers.txt
     ↓
💥 FileNotFoundException
     ↓
ProcessFile() does not transform the exception, catch(FileNotFoundException ex) deleted❌
     ↓
exception goes up to Main()
     ↓
catch (FileNotFoundException ex) in Main
     ↓
"Filen hittades inte: Could not find file 'F:\User\03-ExceptionsDemo\bin\Debug\net10.0\numbers.txt'"
```

OUTPUT:

```cs
=== Start av programmet ===
Försöker läsa fil och räkna...
finally i ProcessFile: StreamReader stängd.
Filen hittades inte: Could not find file 'F:\User\03-ExceptionsDemo\bin\Debug\net10.0\numbers.txt'.✅
Cleanup: Logging avslutat anrop.
Programmet avslutas normalt.

D:\User\dotnet\03-ExceptionsDemo\bin\Debug\net10.0\ExceptionsDemo.exe (process 10392) exited with code 0 (0x0).
To automatically close the console when debugging stops, enable Tools->Options->Debugging->Automatically close the console when debugging stops.
Press any key to close this window . . .
```

# 🧪Test 2 | in numbers.txt | 0

The test failed.

```cs
=== Start av programmet ===
Försöker läsa fil och räkna...
finally i ProcessFile: StreamReader stängd.
Okänt fel: Det gick inte att processa filen.❌
Cleanup: Logging avslutat anrop.
Programmet avslutas normalt.

D:\User\dotnet\03-ExceptionsDemo\bin\Debug\net10.0\ExceptionsDemo.exe (process 7480) exited with code 0 (0x0).
To automatically close the console when debugging stops, enable Tools->Options->Debugging->Automatically close the console when debugging stops.
Press any key to close this window . . .
```

## How to fix it

Floating-point division does not throw `DivideByZeroException`; it returns positive infinity.
To throw `DivideByZeroException` it is necessary `return 100 / number;` instead.

```
ProcessFile() now uses `100 / 0`
     ↓
StreamReader tries to open numbers.txt
     ↓
💥 `DivideByZeroException`
     ↓
ProcessFile() does not transform the exception
     ↓
catch (DivideByZeroException ex) in Main
     ↓
"Kan inte dividera med noll: Attempted to divide by zero."
```

OUTPUT:

```cs
=== Start av programmet ===
Försöker läsa fil och räkna...
finally i ProcessFile: StreamReader stängd.
Kan inte dividera med noll: Attempted to divide by zero.✅
Cleanup: Logging avslutat anrop.
Programmet avslutas normalt.

D:\User\dotnet\03-ExceptionsDemo\bin\Debug\net10.0\ExceptionsDemo.exe (process 52308) exited with code 0 (0x0).
To automatically close the console when debugging stops, enable Tools->Options->Debugging->Automatically close the console when debugging stops.
Press any key to close this window . . .
```

# 🧪Test 3 | in numbers.txt | abc

The test works but the code can be improved.

```cs
=== Start av programmet ===
Försöker läsa fil och räkna...
Formatfel i ProcessFile: The input string 'abc' was not in a correct format.✅
finally i ProcessFile: StreamReader stängd.
Formatfel: The input string 'abc' was not in a correct format.✅
Cleanup: Logging avslutat anrop.
Programmet avslutas normalt.

D:\User\dotnet\03-ExceptionsDemo\bin\Debug\net10.0\ExceptionsDemo.exe (process 41548) exited with code 0 (0x0).
To automatically close the console when debugging stops, enable Tools->Options->Debugging->Automatically close the console when debugging stops.
Press any key to close this window . . .
```

## No need to fix but... can be improved

The `catch (FormatException ex){throw;}` in ProcessFile() can be deleted since it only has throw which will be handled by the `catch (FormatException ex)` in Main.

This throws `FormatException`

```
ProcessFile()
     ↓
StreamReader tries to open numbers.txt
     ↓
💥 FormatException
     ↓
ProcessFile() does not transform the exception, catch(FormatException ex) with throw; and can be deleted (exception propagation)
     ↓
exception goes up to Main()
     ↓
catch (FormatException ex) in Main
     ↓
"Formatfel: The input string 'abc' was not in a correct format."
```

IMPROVED OUTPUT:

```cs
=== Start av programmet ===
Försöker läsa fil och räkna...
finally i ProcessFile: StreamReader stängd.
Formatfel: The input string 'abc' was not in a correct format.✅
Cleanup: Logging avslutat anrop.
Programmet avslutas normalt.
```

# 🧪Test 4 | in numbers.txt | empty string `''`

The test works but the code can be improved.

```cs
=== Start av programmet ===
Försöker läsa fil och räkna...
Formatfel i ProcessFile: The input string '' was not in a correct format.✅
finally i ProcessFile: StreamReader stängd.
Formatfel: The input string '' was not in a correct format.✅
Cleanup: Logging avslutat anrop.
Programmet avslutas normalt.

D:\User\dotnet\03-ExceptionsDemo\bin\Debug\net10.0\ExceptionsDemo.exe (process 43920) exited with code 0 (0x0).
To automatically close the console when debugging stops, enable Tools->Options->Debugging->Automatically close the console when debugging stops.
Press any key to close this window . . .
```

## How to fix it

There is a duplicated message:

```cs
Formatfel i ProcessFile: The input string '' was not in a correct format.
finally i ProcessFile: StreamReader stängd.
Formatfel: The input string '' was not in a correct format.
```

It can be handled by Main so that the `Console.WriteLine($"Formatfel i ProcessFile: {ex.Message}");` is not necessary

ProcessFile()
↓
StreamReader tries to open numbers.txt
↓
💥 FormatException
↓
ProcessFile() catch(FormatException ex) has throw
↓
exception goes up to Main()
↓
catch (FormatException ex) in Main
↓
"Formatfel: The input string '' was not in a correct format."

IMPROVED OUTPUT:

```cs
=== Start av programmet ===
Försöker läsa fil och räkna...
finally i ProcessFile: StreamReader stängd.
Formatfel: The input string '' was not in a correct format.✅
Cleanup: Logging avslutat anrop.
Programmet avslutas normalt.

D:\User\dotnet\03-ExceptionsDemo\bin\Debug\net10.0\ExceptionsDemo.exe (process 43508) exited with code 0 (0x0).
To automatically close the console when debugging stops, enable Tools->Options->Debugging->Automatically close the console when debugging stops.
Press any key to close this window . . .
```

# 🧪Test 5 | numbers.txt | empty file

The test succeeded but the message can be improved.

```cs
=== Start av programmet ===
Försöker läsa fil och räkna...
finally i ProcessFile: StreamReader stängd.
Ett fel uppstod: Filen är tom.✅
Cleanup: Logging avslutat anrop.
Programmet avslutas normalt.
```

## It does not need to be fixed but...can be improved

numbers.txt is empty: line = null, a new exception `InvalidOperationException` is created with the message ("Filen är tom."), main receives it in `catch(Exception ex)` and the console shows "Ett fel uppstod: Filen är tom." which is correct.
The only improvement can be the message in Main. It can be:

```cs
Console.WriteLine($"Ett fel uppstod: {ex.Message}");
```

That way there is no contradiction between unknown error and the error message:

ProcessFile()
↓
StreamReader tries to open numbers.txt
↓
💥 InvalidOperationException
↓
ProcessFile() -> line = null `throw new InvalidOperationException("Filen är tom.")`
↓
exception goes up to Main()
↓
catch (Exception ex) in Main
↓
"Ett fel uppstod: Filen är tom."

# END
