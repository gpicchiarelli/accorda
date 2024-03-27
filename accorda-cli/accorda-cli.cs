using System.Reflection;
using Spectre.Console;

internal class Program
{
    private static void Main(string[] args)
    {
        while (true)
        {
            AnsiConsole.Clear();
            PrintHeader();
            PrintMenu();
            string choice = PromptInput();
            ProcessChoice(choice);
        }
    }

    private static void PrintHeader()
    {
        AnsiConsole.WriteLine();
        AnsiConsole.WriteLine("[bold underline green]Guitar Tuner[/]");
        AnsiConsole.WriteLine();
    }

    private static void PrintMenu()
    {
        AnsiConsole.WriteLine("[yellow]Select an option:[/]");
        AnsiConsole.WriteLine("[yellow]1[/] - Tune Guitar");
        AnsiConsole.WriteLine("[yellow]2[/] - Read License");
        AnsiConsole.WriteLine("[yellow]3[/] - Exit");
        AnsiConsole.WriteLine();
    }

    private static string PromptInput()
    {
        return AnsiConsole.Prompt(new TextPrompt<string>("Enter your choice:")
            .InvalidChoiceMessage("[red]Invalid choice[/]")
            .DefaultValue("1"));
    }

    private static void ProcessChoice(string choice)
    {
        switch (choice.ToLower())
        {
            case "1":
                TuneGuitar();
                break;
            case "2":
                ReadLicense();
                break;
            case "3":
                Environment.Exit(0);
                break;
            default:
                AnsiConsole.WriteLine("[red]Invalid choice[/]");
                break;
        }
    }

    private static void TuneGuitar()
    {
        // Implementare la logica per l'accordatura della chitarra
        AnsiConsole.WriteLine("[yellow]Tuning guitar...[/]");
        // Simulazione di attesa per 2 secondi
        System.Threading.Thread.Sleep(2000);
        AnsiConsole.WriteLine("[green]Guitar tuned successfully![/]");
        AnsiConsole.WriteLine();
        AnsiConsole.WriteLine("[yellow]Press any key to continue...[/]");
        _ = Console.ReadKey();
    }

    private static void ReadLicense()
    {
        string resourceName = "LICENSE"; // Sostituisci con il nome corretto

        // Ottieni l'assembly corrente
        var assembly = Assembly.GetExecutingAssembly();

        // Leggi il contenuto del file di licenza
        string licenseText = string.Empty;
        using (Stream stream = assembly.GetManifestResourceStream(resourceName))
        {
            if (stream == null)
            {
                Console.WriteLine("File di licenza non trovato.");
                return;
            }

            using (StreamReader reader = new StreamReader(stream))
            {
                licenseText = reader.ReadToEnd();
            }
        }


        // Leggere il testo della licenza dal file o da una risorsa
        AnsiConsole.WriteLine();
        AnsiConsole.WriteLine("[bold underline]Licenza[/]");
        AnsiConsole.WriteLine(licenseText);
        AnsiConsole.WriteLine();
        AnsiConsole.WriteLine("[yellow]Premere per chiudere la licenza.[/]");
        _ = Console.ReadKey();
    }
}

