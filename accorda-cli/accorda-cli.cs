using System;
using Spectre.Console;

class Program
{
    static void Main(string[] args)
    {
        while (true)
        {
            AnsiConsole.Clear();
            PrintHeader();
            PrintMenu();
            var choice = PromptInput();
            ProcessChoice(choice);
        }
    }

    static void PrintHeader()
    {
        AnsiConsole.WriteLine();
        AnsiConsole.WriteLine("[bold underline green]Guitar Tuner[/]");
        AnsiConsole.WriteLine();
    }

    static void PrintMenu()
    {
        AnsiConsole.WriteLine("[yellow]Select an option:[/]");
        AnsiConsole.WriteLine("  [yellow]1[/] - Tune Guitar");
        AnsiConsole.WriteLine("  [yellow]2[/] - Read License");
        AnsiConsole.WriteLine("  [yellow]3[/] - Exit");
        AnsiConsole.WriteLine();
    }

    static string PromptInput()
    {
        return AnsiConsole.Prompt(new TextPrompt<string>("Enter your choice:")
            .InvalidChoiceMessage("[red]Invalid choice[/]")
            .DefaultValue("1"));
    }

    static void ProcessChoice(string choice)
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

    static void TuneGuitar()
    {
        // Implementare la logica per l'accordatura della chitarra
        AnsiConsole.WriteLine("[yellow]Tuning guitar...[/]");
        // Simulazione di attesa per 2 secondi
        System.Threading.Thread.Sleep(2000);
        AnsiConsole.WriteLine("[green]Guitar tuned successfully![/]");
        AnsiConsole.WriteLine();
        AnsiConsole.WriteLine("[yellow]Press any key to continue...[/]");
        Console.ReadKey();
    }

    static void ReadLicense()
    {
        // Leggere il testo della licenza dal file o da una risorsa
        string licenseText = "License text goes here...";
        AnsiConsole.WriteLine();
        AnsiConsole.WriteLine("[bold underline]License[/]");
        AnsiConsole.WriteLine(licenseText);
        AnsiConsole.WriteLine();
        AnsiConsole.WriteLine("[yellow]Press any key to continue...[/]");
        Console.ReadKey();
    }
}

