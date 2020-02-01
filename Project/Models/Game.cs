using ConsoleAdventure.Project.Interfaces;

namespace ConsoleAdventure.Project.Models
{
    public class Game : IGame
    {
        public IRoom CurrentRoom { get; set; }
        public IPlayer CurrentPlayer { get; set; }

        //NOTE Make yo rooms here...
        public void Setup()
        {
            Room Room1 = new Room("Starting Room", "starting room");
            Room Room2 = new Room("Ball Room", "Ball room");
            Room Room3 = new Room("Dining Room", "this is the dining room");
            Room Room4 = new Room("Kitchen", "this is the kitchen");
            Room Room5 = new Room("room 5", "this is the room 5 ");
            Room Room6 = new Room("room 6", "this is the room 6 ");
            Room Room7 = new Room("room 7", "this is the room 7");
            Room Room8 = new Room("room 8", "this is the room 8");

            Room1.Exits.Add("east", Room2);
            Room2.Exits.Add("west", Room1);
            Room2.Exits.Add("east", Room3);
            Room2.Exits.Add("north", Room6);
            Room2.Exits.Add("south", Room5);
            Room3.Exits.Add("east", Room4);
            Room3.Exits.Add("west", Room2);
            Room4.Exits.Add("east", Room3);
            Room4.Exits.Add("north", Room8);
            Room5.Exits.Add("north", Room2);
            Room6.Exits.Add("south", Room2);
            Room6.Exits.Add("east", Room7);
            Room7.Exits.Add("west", Room6);
            Room8.Exits.Add("south", Room4);

            Item key = new Item("Das Key", "you can unlock the damn door");
            Room3.Items.Add(key);
            Item lighter = new Item("Lighter", "Used to light the torch");
            Room1.Items.Add(lighter);
            Item torch = new Item("torch", "Use This to Light the room up!");
            Room2.Items.Add(torch);

            Player One = new Player("");
            CurrentPlayer = One;

            CurrentRoom = Room1;
        }
        public Game()
        {
            Setup();
        }
    }
}