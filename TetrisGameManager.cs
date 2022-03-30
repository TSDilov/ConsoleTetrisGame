using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MyPersonalTetris
{
    public class TetrisGameManager
    {
        private ITetrisGame game;
        private IInputHandler inputHandler;
        private TetrisConsoleWriter tetrisConsoleWriter;
        private ScoreManager scoreManager;
        public TetrisGameManager(ITetrisGame game, IInputHandler inputHandler, TetrisConsoleWriter tetrisConsoleWriter, ScoreManager scoreManager)
        {
            this.game = game;
            this.inputHandler = inputHandler;
            this.tetrisConsoleWriter = tetrisConsoleWriter;
            this.scoreManager = scoreManager;
        }
        public void MainLoop()
        {
            while (true)
            {
                this.tetrisConsoleWriter.Frame++;
                this.game.UpdateLevel(scoreManager.Score);
                var input = this.inputHandler.GetInput();
                switch (input)
                {
                    case TetrisGameInput.Left:
                        if (this.game.CanMoveToLeft())
                        {
                            this.game.CurrentFigureCol--;
                        }
                        break;
                    case TetrisGameInput.Right:
                        if (this.game.CanMoveToRight())
                        {
                            this.game.CurrentFigureCol++;
                        }
                        break;
                    case TetrisGameInput.Down:
                        this.tetrisConsoleWriter.Frame = 1;
                        this.scoreManager.AddToScore(game.Level, 0);
                        this.game.CurrentFigureRow++;
                        break;
                    case TetrisGameInput.Rotate:
                        var newFigure = this.game.CurrentFigure.GetRotate();
                        if (!this.game.Collision(newFigure))
                        {
                            this.game.CurrentFigure = newFigure;
                        }
                        break;
                    case TetrisGameInput.Exit:
                        return;
                }

                if (this.tetrisConsoleWriter.Frame % (this.tetrisConsoleWriter.FramesToMoveFigure - this.game.Level) == 0)
                {
                    this.game.CurrentFigureRow++;
                    this.tetrisConsoleWriter.Frame = 0;
                }
                if (this.game.Collision(this.game.CurrentFigure))
                {
                    this.game.AddCurrentFigureTotetrisField();
                    int lines = this.game.CheckForFullLines();
                    this.scoreManager.AddToScore(this.game.Level, lines);
                    this.game.NewRandomFigure();
                    if (this.game.Collision(this.game.CurrentFigure))
                    {
                        this.scoreManager.AddToHighScore();
                        this.tetrisConsoleWriter.DrawAll(game, scoreManager);
                        this.tetrisConsoleWriter.WriteGameOver(scoreManager.Score);
                        Thread.Sleep(100000);
                        return;
                    }
                }

                this.tetrisConsoleWriter.DrawAll(game, scoreManager);

                Thread.Sleep(40);
            }
        }
    }
}
