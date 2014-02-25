using System;

namespace ASCIIInvaders
{
    class ASCIIInvaders
    {
        static void Main()
        {
            Engine engine = new Engine(80, 50);

            engine.Run();
        }
    }
}
