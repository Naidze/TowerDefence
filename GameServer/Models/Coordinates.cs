﻿using System;
namespace GameServer.Models
{
    public class Coordinates
    {
        public long Id { get; set; }
        public long PosX { get; set; }
        public long PosY { get; set; }

        public Coordinates()
        {
        }

        public Coordinates(long PosX, long PosY)
        {
            this.PosX = PosX;
            this.PosY = PosY;
        }

    }
}
