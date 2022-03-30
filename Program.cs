using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;

namespace MyPersonalTetris
{
    public static class Program
    {
        public static void Main()
        {
            new MusicPlayer().Play();

            int TetrisRows = 20;
            int TetrisColumns = 10;
            ScoreManager scoreManager = new ScoreManager("scores.txt");            
            var tetrisConsoleWriter = new TetrisConsoleWriter(TetrisRows, TetrisColumns, '0');            
            var game = new TetrisGame(TetrisRows, TetrisColumns);
            IInputHandler inputHandler = new ConsoleInputHandler();
            var gameManager = new TetrisGameManager(game, inputHandler, tetrisConsoleWriter, scoreManager);
            gameManager.MainLoop();
        }
    }
}
