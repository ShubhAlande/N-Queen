using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NQueenusingHillClimbing
{

    public sealed class RandomRestart : NqueenBoard
    {
        private int _totalNumberOfStepsSucceeds = 0;
        private int _numberOfSuccessfulIterations = 0;
        private Boolean _boardChanged = true;
        private int _consecutiveSidewaysMoves = 0;
        private readonly int LIMIT_CONSECUTIVE_SIDEWAYS_MOVES = 100;

        public RandomRestart(int numOfQueens) : base(numOfQueens)
        {
            
        }

        /// <summary>
        /// restarting the game board (by calling resetboard()) any time the board is 'stuck' at h > 0
        /// </summary>
        /// <returns>success/ failure analysis</returns>
        public int[] RunAlgorithmWithoutSideways()
        {
            this.StartGameBoard();
            Boolean boardChanged = true;
            int currentStateHeuristic = this.CalculateHeuristic(this._gameBoard);
            List<int[,]> possibleMoves = new List<int[,]>();
            this.ResetRegularMoves();

            while (this.CalculateHeuristic(this._gameBoard) != 0 && boardChanged == true)
            {
                int columnNumber = 0;
                int[,] possibleMove = new int[this.GetBoardSize(),this.GetBoardSize()];
                for (int i = 0; i < this.GetBoardSize(); i++)
                {
                    for (int j = 0; j < this.GetBoardSize(); j++)
                    {
                        possibleMove[i,j] = this._gameBoard[i,j];
                    }
                }

                currentStateHeuristic = this.CalculateHeuristic(this._gameBoard);
                this._boardChanged = false;

                while (columnNumber < this.GetBoardSize())
                {
                    int currentColumnQueenPosition = -1;

                    for (int i = 0; i < this.GetBoardSize(); i++)
                    {
                        if (possibleMove[i,columnNumber] == this._QUEEN)
                            currentColumnQueenPosition = i;
                        possibleMove[i,columnNumber] = this._NOT_QUEEN;
                    }

                    for (int i = 0; i < this.GetBoardSize(); i++)
                    {
                        possibleMove[i,columnNumber] = this._QUEEN;

                        int[,] newMove = new int[this.GetBoardSize(),this.GetBoardSize()];
                        for (int k = 0; k < this.GetBoardSize(); k++)
                        {
                            for (int j = 0; j < this.GetBoardSize(); j++)
                            {
                                newMove[k,j] = possibleMove[k,j];
                            }
                        }
                        if (this.CalculateHeuristic(this._gameBoard) > this.CalculateHeuristic(newMove))
                            possibleMoves.Add(newMove);
                        possibleMove[i,columnNumber] = this._NOT_QUEEN;
                    }

                    possibleMove[currentColumnQueenPosition,columnNumber] = this._QUEEN;

                    columnNumber += 1;
                }

                Random rand = new Random();

                if (possibleMoves.Count != 0)
                {
                    int pick = rand.Next(possibleMoves.Count);

                    int minHeuristic = currentStateHeuristic;

                    if (minHeuristic > this.CalculateHeuristic(possibleMoves[pick]))
                    {
                        minHeuristic = this.CalculateHeuristic(possibleMoves[pick]);
                        this._gameBoard = this.CopyBoard(possibleMoves[pick]);
                        this._boardChanged = true;

                        this._regularMoves += 1;
                        Console.WriteLine("\nStep No." + _regularMoves);
                        this.PrintGameBoard();
                        possibleMoves.Clear();
                    }
                }
                else
                {
                    this.ResetBoard();
                    this.SetGameBoard();
                    this._boardChanged = true;
                    possibleMoves.Clear();
                    Console.WriteLine("Random Restart!");
                    this.StartGameBoard();
                }
            }

            if (this.CalculateHeuristic(this._gameBoard) == 0)
            {
                Console.WriteLine("\nSuccess!");
                this._totalNumberOfStepsSucceeds += this._regularMoves;
                this._numberOfSuccessfulIterations += 1;
                Console.WriteLine("Total Steps Taken: " + this._regularMoves);
            }

            return new int[] { _totalNumberOfStepsSucceeds, _numberOfSuccessfulIterations, this.GetResets() };
        }

        /// <summary>
        /// restarting the game board (by calling resetboard()) any time the board is 'stuck' at h > 0 with side way moves allowed
        /// </summary>
        /// <returns>success/ failure analysis</returns>
        public int[] RunAlgorithmWithSideways()
        {
            this.StartGameBoard();
            Boolean boardChanged = true;
            int currentStateHeuristic = this.CalculateHeuristic(this._gameBoard);
            List<int[,]> possibleMoves = new List<int[,]>();
            this.ResetRegularMoves();

            while (this.CalculateHeuristic(this._gameBoard) != 0 && boardChanged == true)
            {
                int columnNumber = 0;
                int[,] possibleMove = new int[this.GetBoardSize(),this.GetBoardSize()];
                for (int i = 0; i < this.GetBoardSize(); i++)
                {
                    for (int j = 0; j < this.GetBoardSize(); j++)
                    {
                        possibleMove[i,j] = this._gameBoard[i,j];
                    }
                }

                currentStateHeuristic = this.CalculateHeuristic(this._gameBoard);
                this._boardChanged = false;

                while (columnNumber < this.GetBoardSize())
                {
                    int currentColumnQueenPosition = -1;

                    for (int i = 0; i < this.GetBoardSize(); i++)
                    {
                        if (possibleMove[i,columnNumber] == this._QUEEN)
                            currentColumnQueenPosition = i;
                        possibleMove[i,columnNumber] = this._NOT_QUEEN;
                    }

                    for (int i = 0; i < this.GetBoardSize(); i++)
                    { 
                        possibleMove[i,columnNumber] = this._QUEEN; 

                        int[,] newMove = new int[this.GetBoardSize(),this.GetBoardSize()];
                        for (int k = 0; k < this.GetBoardSize(); k++)
                        {
                            for (int j = 0; j < this.GetBoardSize(); j++)
                            {
                                newMove[k,j] = possibleMove[k,j];
                            }
                        }
                        if (this.CalculateHeuristic(this._gameBoard) >= this.CalculateHeuristic(newMove) && this.BoardsAreEqual(this._gameBoard, newMove) == false)
                            possibleMoves.Add(newMove);
                        possibleMove[i,columnNumber] = this._NOT_QUEEN;
                    }

                    possibleMove[currentColumnQueenPosition,columnNumber] = this._QUEEN;

                    columnNumber += 1;
                }

                Random rand = new Random();
                int minHeuristic = currentStateHeuristic;

                if (possibleMoves.Count != 0)
                {
                    int pick = rand.Next(possibleMoves.Count);

                    if (minHeuristic > this.CalculateHeuristic(possibleMoves[pick]))
                    {
                        minHeuristic = this.CalculateHeuristic(possibleMoves[pick]);
                        this._gameBoard = this.CopyBoard(possibleMoves[pick]);
                        this._boardChanged = true;
                        this._consecutiveSidewaysMoves = 0; 
                        this._regularMoves += 1;
                        Console.WriteLine("\nStep No." + _regularMoves);
                        this.PrintGameBoard();
                        possibleMoves.Clear();
                    }
                    else if (minHeuristic == this.CalculateHeuristic(possibleMoves[pick]) && 
                          this._consecutiveSidewaysMoves < this.LIMIT_CONSECUTIVE_SIDEWAYS_MOVES)
                    { 
                        minHeuristic = this.CalculateHeuristic(possibleMoves[pick]);
                        this._gameBoard = this.CopyBoard(possibleMoves[pick]);
                        this._boardChanged = true;
                        this._consecutiveSidewaysMoves += 1; 
                        this._regularMoves += 1;
                        Console.WriteLine("\nStep No." + _regularMoves);
                        this.PrintGameBoard();
                        Console.WriteLine("Sideways Move!");
                        possibleMoves.Clear();
                    }
                    else
                    {
                        this.ResetBoard(); 
                        this.SetGameBoard();
                        this._boardChanged = true;
                        possibleMoves.Clear();
                        Console.WriteLine("Random Restart!");
                        this.StartGameBoard();
                    }
                }
                else
                {
                    this.ResetBoard(); 
                    this.SetGameBoard();
                    this._boardChanged = true;
                    possibleMoves.Clear();
                    Console.WriteLine("Random Restart!");
                    this.StartGameBoard();
                }
            }

            if (this.CalculateHeuristic(this._gameBoard) == 0)
            {
                Console.WriteLine("\nSuccess!");
                this._totalNumberOfStepsSucceeds += this._regularMoves;
                this._numberOfSuccessfulIterations += 1;
                Console.WriteLine("Total Steps Taken: " + this._regularMoves);
            }

            return new int[] { _totalNumberOfStepsSucceeds, _numberOfSuccessfulIterations, this.GetResets() };
        }
    }
}
