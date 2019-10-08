using System;
namespace GameServer.Models
{
    public class Player
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public Player(string id)
        {
            Id = id;
        }
    }
}
