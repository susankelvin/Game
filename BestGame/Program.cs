﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BestGame
{
    class Program
    {
        static void Main()
        {
            Engine engine = new Engine(80, 50);

            engine.Run();
        }
    }
}
