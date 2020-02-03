using System;
using System.Collections.Generic;
using ConsoleAdventure.Project.Interfaces;
using ConsoleAdventure.Project.Models;

namespace ConsoleAdventure.Project
{
    public class GameService : IGameService
    {
        private IGame _game { get; set; }
        public List<string> Messages { get; set; }
        public GameService()
        {
            _game = new Game();
            Messages = new List<string>();
        }
        public void Go(string direction)
        {
            if(_game.CurrentRoom.Exits.ContainsKey(direction))
            {
                _game.CurrentRoom = _game.CurrentRoom.Exits[direction];
                GetRoomDescription();
                return;
            }
            Messages.Add("There is no exit that way my man");
            return;
        }

        private void GetRoomDescription()
        {
            if(_game.CurrentRoom.Name == "room 5")
            {
                Console.WriteLine($"{_game.CurrentPlayer.Name} the floor gave out underneath you! Game over!");
                Reset();
            }
                Messages.Add(_game.CurrentRoom.Description);
            
        }

        public void Help()
        {
            Messages.Add("--8-8-8-8--Command List --8-8-8-8--");
            Messages.Add("'quit'-------------********--------------------'Quits The Game'");
            Messages.Add("'look'------********---------'looks around the room for clues!'");
            Messages.Add("'help'------*************------'shows you the list of commands'");
            Messages.Add("'reset'-------****--------'Resets the game and starts you over'");
            Messages.Add("'inventory'---------********----------'Displays your inventory'");
            Messages.Add("'go + direction(N, S, E, W)'--**--'Moves you in that direction'");
        }

        public void Inventory()
        {
            if(_game.CurrentPlayer.Inventory.Count == 0)
            {
                Messages.Add("you aint got shit my man");
                return;
            }
            Messages.Add("---****---Inventory---****---");
            foreach(var item in _game.CurrentPlayer.Inventory)
            {
                Messages.Add($"({item.Name})---***---({item.Description})");
            }
        }

        public void Look()
        {
            if(_game.CurrentRoom.Items.Count > 0)
            {
                Messages.Add($"stumbling in this room you find {_game.CurrentRoom.Items[0].Name}");
            }
            Messages.Add(_game.CurrentRoom.Description);
        }
        ///<summary>
        ///Restarts the game 
        ///</summary>
        public void Reset()
        {
            _game.CurrentPlayer.Inventory.Clear();
            Console.WriteLine("Why Hello! And What might your name be?");
            Setup(Console.ReadLine());
        }

        public void Setup(string playerName)
        {
            _game.CurrentPlayer.Name = playerName;
            _game.Setup();
            Messages.Add($"welcome to the thunderdome {_game.CurrentPlayer.Name}");
            Messages.Add($"{_game.CurrentRoom.Description}");
        }
        ///<summary>When taking an item be sure the item is in the current room before adding it to the player inventory, Also don't forget to remove the item from the room it was picked up in</summary>
        public void TakeItem(string itemName)
        {
            var item = _game.CurrentRoom.Items.Find(i => i.Name.ToLower() == itemName);
            _game.CurrentPlayer.Inventory.Add(item);
            _game.CurrentRoom.Items.Remove(item);
            Messages.Add($"You grabbed {item.Name} player");
        }
        ///<summary>
        ///No need to Pass a room since Items can only be used in the CurrentRoom
        ///Make sure you validate the item is in the room or player inventory before
        ///being able to use the item
        ///</summary>
        public void UseItem(string itemName)
        {

            if (_game.CurrentPlayer.Inventory.Exists(i => i.Name.ToLower() == itemName))
            {
                if(_game.CurrentRoom.Name == "room 8" &&  itemName == "das key")
                {
                    Messages.Add($"you win {_game.CurrentPlayer.Name}");
                    Reset();
                    return;
                }
                Messages.Add($"{_game.CurrentPlayer.Name} you cant use this here!");
            }
            Messages.Add("There is nothing in your inventory that matches.");
            return;
        }
    }
}