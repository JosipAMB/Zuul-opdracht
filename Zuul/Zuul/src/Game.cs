using System;

class Game
	{
	private Parser parser;
	private Player player;
	public Game ()
	{
		parser = new Parser();
		player = new Player();
		CreateRooms();
	}
	// methods
// methods
private void Take(Command command)
{
    if (!command.HasSecondWord())
    {
        Console.WriteLine("Take what?");
        return;
    }

    string itemName = command.SecondWord;
    player.TakeFromChest(itemName);
}

private void Drop(Command command)
{
    if (!command.HasSecondWord())
    {
        Console.WriteLine("Drop what?");
        return;
    }

    string itemName = command.SecondWord;
    player.DropToChest(itemName);
}


	// Initialise the Rooms (and the Items)
		private void CreateRooms()
	{
		Room outside = new Room("outside the main entrance of the university");
		Room theatre = new Room("in a lecture theatre");
		Room pub = new Room("in the campus pub");
		Room lab = new Room("in a computing lab");
		Room office = new Room("in the computing admin office");
		// Extra kamers voor verdiepingen
		Room labUp = new Room("in the upper floor of the computing lab");
		Room officeUp = new Room("in the upper floor of the admin office");

		outside.AddExit("east", theatre);
		outside.AddExit("south", lab);
		outside.AddExit("west", pub);

		theatre.AddExit("west", outside);
		pub.AddExit("east", outside);

		lab.AddExit("north", outside);
		lab.AddExit("east", office);
		lab.AddExit("up", labUp);      // naar boven
		labUp.AddExit("down", lab);    // naar beneden

		office.AddExit("west", lab);
		office.AddExit("up", officeUp);    // naar boven
		officeUp.AddExit("down", office);  // naar beneden
		// Maak items
		Item sword = new Item(5, "sword");
		Item book = new Item(2, "book");
		Item key = new Item(1, "key");
		// Voeg items toe aan kamers
		outside.AddItem(book);
		lab.AddItem(sword);
		officeUp.AddItem(key);
		// Start buiten
		player.CurrentRoom = outside;
	}
	//  Main play routine. Loops until end of play.
	public void Play()
	{
		PrintWelcome();
		// Enter the main command loop. Here we repeatedly read commands and
		// execute them until the player wants to quit.
		bool finished = false;
		while (!finished)
		{
			Command command = parser.GetCommand();
			finished = ProcessCommand(command);
		}
		Console.WriteLine("Thank you for playing.");
		Console.WriteLine("Press [Enter] to continue.");
		Console.ReadLine();
	}
	// Print out the opening message for the player.
	private void PrintWelcome()
	{
		Console.WriteLine();
		Console.WriteLine("Welcome to Zuul!");
		Console.WriteLine("Zuul is a new, incredibly boring adventure game.");
		Console.WriteLine("Type 'help' if you need help.");
		Console.WriteLine();
		Console.WriteLine(player.CurrentRoom.GetLongDescription());
	}
	// Given a command, process (that is: execute) the command.
	// If this command ends the game, it returns true.
	// Otherwise false is returned.
	private bool ProcessCommand(Command command)
	{
		bool wantToQuit = false;

		if(command.IsUnknown())
		{
			Console.WriteLine("I don't know what you mean...");
			return wantToQuit; // false
		}

		else if (command.CommandWord == "look")
		{
    		Console.WriteLine(player.CurrentRoom.GetLongDescription());
		}

		switch (command.CommandWord)
		{
			case "help":
				PrintHelp();
				break;
			case "go":
				GoRoom(command);
				break;
			case "take":
				Take(command);
				break;
			case "drop":
				Drop(command);
				break;
			case "quit":
				wantToQuit = true;
				break;
		}
		return wantToQuit;
	}
	// ######################################
	// implementations of user commands:
	// ######################################
	
	// Print out some help information.
	// Here we print the mission and a list of the command words.
	private void PrintHelp()
	{
		Console.WriteLine("You are lost. You are alone.");
		Console.WriteLine("You wander around at the university.");
		Console.WriteLine();
		// let the parser print the commands
		parser.PrintValidCommands();
	}

	// Try to go to one direction. If there is an exit, enter the new
	// room, otherwise print an error message.
	private void GoRoom(Command command)
	{
		if (!command.HasSecondWord())
		{
			Console.WriteLine("Go where?");
			return;
		}

		string direction = command.SecondWord;
		Room nextRoom = player.CurrentRoom.GetExit(direction);

		if (nextRoom == null)
		{
			Console.WriteLine("There is no door to " + direction + "!");
			return;
		}

		// beweeg speler
		player.CurrentRoom = nextRoom;

		// verlies health
		player.Damage(5);
		Console.WriteLine(player.CurrentRoom.GetLongDescription());
		Console.WriteLine("Your health: " + player.Health);

		// check dood
		if (!player.IsAlive())
		{
			Console.WriteLine("You have died! Game over.");
			Environment.Exit(0);
		}

		// check win: gebruik de description van de kamer
		if (player.CurrentRoom.GetShortDescription() == "in the upper floor of the admin office")
		{
			Console.WriteLine("Congratulations! You won!");
			Environment.Exit(0);
		}
	}
}
