using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NQueenusingHillClimbing
{
    public class SteepestAscent: NqueenBoard
    {
        private int _totalNumberOfStepsSucceeds = 0;
        private int _numberOfSuccessfulIterations = 0;
        private int _totalNumberOfStepsFails = 0;
        private int _numberOfFailedIterations = 0;
        private Boolean _boardChanged = true;

        public SteepestAscent(int numOfQueens) : base(numOfQueens)
        {
            
        }

        /// <summary>
        /// calls until the heuristic is 0 and neighbours with a lower heuristic value is found. Fails if h=0 not found.
        /// </summary>
        /// <returns>Success / Failure analysis</returns>
        public int[] RunAlgorithm()
        {
            this.StartGameBoard();
            this._boardChanged = true; 
            int currentStateHeuristic = this.CalculateHeuristic(this._gameBoard);
            List<int[,]> possibleMoves = new List<int[,]>();
            this.ResetRegularMoves();

            while (this.CalculateHeuristic(this._gameBoard) != 0 && (this._boardChanged == true))
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
                    if (possibleMoves.Count == 0)
                    {
                        this._boardChanged = false;
                        possibleMoves.Clear();
                    }
                }
            }

            if (this.CalculateHeuristic(this._gameBoard) == 0)
            {
                Console.WriteLine("\nSuccess!");
                this._totalNumberOfStepsSucceeds += this._regularMoves;
                this._numberOfSuccessfulIterations += 1;
                Console.WriteLine("Total Steps Taken: " + this._regularMoves);
            }
            else
            {
                Console.WriteLine("\nFailure!");
                this.PrintGameBoard();
                this._totalNumberOfStepsFails += this._regularMoves;
                this._numberOfFailedIterations += 1;
                Console.WriteLine("Total Steps Taken: " + this._regularMoves);
            }

            return new int[] { _totalNumberOfStepsSucceeds, _numberOfSuccessfulIterations, _totalNumberOfStepsFails,
                _numberOfFailedIterations };
        }
    }
}
