using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Arcanoid_Shelkynov
{
    static class Program
    {

        static void Main(string[] args)
        {
            using (Arcanoid game = new Arcanoid())
            {
                game.Run();
            }
        }
    }
}
