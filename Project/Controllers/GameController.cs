using System;
using System.Collections.Generic;
using ConsoleAdventure.Project.Interfaces;
using ConsoleAdventure.Project.Models;

namespace ConsoleAdventure.Project.Controllers
{

    public class GameController : IGameController
    {
        private GameService _gameService { get; set; } = new GameService();
        private bool _playing = true;
        //NOTE Makes sure everything is called to finish Setup and Starts the Game loop
        public void Run()
        {
            Console.Clear();
            Console.WriteLine("Why Hello! And What might your name be?");
            Console.Write("> ");
            string playerName = Console.ReadLine();
            _gameService.Setup(playerName);
    
            while(_playing == true)
            {
                Print();
                GetUserInput();
            }
            
        }

        //NOTE Gets the user input, calls the appropriate command, and passes on the option if needed.
        public void GetUserInput()
        {
            Console.WriteLine("What would you like to do?");
            string input = Console.ReadLine().ToLower() + " ";
            string command = input.Substring(0, input.IndexOf(" "));
            string option = input.Substring(input.IndexOf(" ") + 1).Trim();
            //NOTE this will take the user input and parse it into a command and option.
            //IE: take silver key => command = "take" option = "silver key"
            switch (command)
            {
                case "quit":
                    _playing = false;
                    break;
                case "look":
                    _gameService.Look();
                    break;
                case "help":
                    _gameService.Help();
                    break;
                case "reset":
                    Run();
                    break;
                case "inventory":
                    _gameService.Inventory();
                    break;
                case "go":
                    _gameService.Go(option);
                    break;
                case "take":
                    _gameService.TakeItem(option);
                    break;
                case "use":
                    _gameService.UseItem(option);
                    break;
                
            }

        }

        //NOTE this should print your messages for the game.
        private void Print()
        {
            Console.Clear();
            foreach(string message in _gameService.Messages)
            {
                Console.WriteLine(message);
            }
            _gameService.Messages.Clear();
        }

    }
}