namespace ExceptionsDemo
{
  internal class Program
  {
    static void Main(string[] args)
    {
      {
        Console.WriteLine("=== Start av programmet ===");

        // Exempel 1: try-catch-finally
        try
        {
          Console.WriteLine("Försöker läsa fil och räkna...");
          var path = Path.Combine(AppContext.BaseDirectory, "numbers.txt");
          var result = ProcessFile(path);

          Console.WriteLine($"\nResultat: {result}");
        }
        catch (FileNotFoundException ex)
        {
          // Specifikt fel om filen inte finns
          Console.WriteLine($"Filen hittades inte: {ex.Message}");
        }
        catch (FormatException ex)
        {
          // Specifikt fel om texten inte kan tolkas som tal
          Console.WriteLine($"Formatfel: {ex.Message}");
        }
        catch (DivideByZeroException ex)
        {
          // Specifikt fel om nolldivision
          Console.WriteLine($"Kan inte dividera med noll: {ex.Message}");
        }
        catch (Exception ex)
        {
          // Fallback för alla övriga obekanta fel
          // Console.WriteLine($"Okänt fel: {ex.Message}");
          Console.WriteLine($"Ett fel uppstod: {ex.Message}"); // Juliett-05: If numbers.txt is empty (line = null) ProcessFile() creates a new exception with the message: Filen är tom.". Since the error message is known, "Ett fel uppstod:" can improve precision.
        }
        finally
        {
          // Körs ALLTID, även om det blev undantag
          Console.WriteLine("Cleanup: Logging avslutat anrop.");
        }

        Console.WriteLine("Programmet avslutas normalt.");
      }



      // Exempel på metod som själv kastar ett undantag (throw)
      static double ProcessFile(string fileName)
      {
        // Om filnamnet är tomt: logiskt fel vi vill signalera
        if (string.IsNullOrWhiteSpace(fileName))
        {
          throw new ArgumentException("Filnamn får inte vara tomt eller null.", nameof(fileName));
        }

        StreamReader? reader = null;
        try
        {
          reader = new StreamReader(fileName);

          string? line = reader.ReadLine();


          if (line == null)
            throw new InvalidOperationException("Filen är tom.");

          // Försöker omvandla text till tal
          int number = int.Parse(line); // Kan ge FormatException

          // Division: kan ge DivideByZeroException
          //return 100.0 / number;
          return 100 / number; // Juliett-02: double division, produce Infinity if number is 0. Instead, integer division would throw DivideByZeroException in the method ProcessFile().
        }

        // Juliett-03: This catch can be commented because it only uses throw which makes the exception be handled by the catch()FormatException) in Main.
        // catch (FormatException ex)
        // {
        // Vi kan logga eller omformulera felet
        //Console.WriteLine($"Formatfel i ProcessFile: {ex.Message}");
        // Vi kan välja att låta metoden "kasta upp" felet
        // throw; // När du i `catch` bara vill logga/analysera,
        // men låta anroparen (t.ex. en högre nivå i applikationen)
        // bestämma hur man ska återhämta sig. 
        // }

        //Juliett-01: This catch block is commented out because the FileNotFoundException is already implemented in Main.
        //catch (Exception ex)
        //{
        //  // Om vi vill ge en mer meningsfull feltyp till anroparen
        //  throw new InvalidOperationException(
        //  "Det gick inte att processa filen.",
        //  ex); // InnerException = ursprunglig fel
        //}

        //Juliett-04: This catch block is commented out because the DivideByZeroException is already implemented in Main.
        //catch (DivideByZeroException ex)
        //{
        //  throw;
        //}
        finally
        {
          // Garanterad stängning av resurs
          reader?.Close();
          Console.WriteLine("finally i ProcessFile: StreamReader stängd.");
        }
      }
    }
  }
}

