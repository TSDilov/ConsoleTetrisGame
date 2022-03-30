using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPersonalTetris
{
    public class ConsoleInputHandler : IInputHandler
    {
        public TetrisGameInput GetInput()
        {
            if (Console.KeyAvailable)
            {
                ConsoleKeyInfo key = Console.ReadKey();
                if (key.Key == ConsoleKey.Escape)
                {
                    return TetrisGameInput.Exit;
                }
                if (key.Key == ConsoleKey.LeftArrow || key.Key == ConsoleKey.A)
                {
                    return TetrisGameInput.Left;
                }
                if (key.Key == ConsoleKey.RightArrow || key.Key == ConsoleKey.D)
                {
                    return TetrisGameInput.Right;
                }
                if (key.Key == ConsoleKey.DownArrow || key.Key == ConsoleKey.S)
                {
                    return TetrisGameInput.Down;
                }
                if (key.Key == ConsoleKey.Spacebar || key.Key == ConsoleKey.UpArrow || key.Key == ConsoleKey.W)
                {
                    return TetrisGameInput.Rotate;
                }
            }

            return TetrisGameInput.None;
        }
    }

    public enum TetrisGameInput
    { 
        None = 0,
        Left = 1,
        Right = 2,
        Down = 3,
        Rotate = 4,
        Exit = 9,
    }
}
