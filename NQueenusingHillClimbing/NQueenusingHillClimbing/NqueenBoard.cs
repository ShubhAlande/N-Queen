using System;

namespace NQueenusingHillClimbing
{
    public abstract class NqueenBoard
    {
        Random rand = new Random();
        private int[,] _gameBoardOne;
        private int[,] _gameBoardTwo;
        private int[,] _gameBoardThree;
        public int[,] _gameBoard;
        public int _numOfResets = -1;
        public int _numberOfQueens;
        public int _regularMoves = 0;
        public int _sidewaysMoves = 0;
        public readonly int _QUEEN = 1;
        public readonly int _NOT_QUEEN = 0;

        public NqueenBoard(int numOfQueens)
        {
            this._numberOfQueens = numOfQueens;
            this._gameBoardOne = new int[numOfQueens,numOfQueens];
            this._gameBoardTwo = new int[numOfQueens,numOfQueens];
            this._gameBoardThree = new int[numOfQueens,numOfQueens];
            this._gameBoard = new int[numOfQueens,numOfQueens];
            this.ResetBoard();
            this.SetGameBoard();
          
        }

        public int GetBoardSize()
        {
            return this._numberOfQueens;
        }

        public int[,] GetGameBoardOne()
        {
            return this._gameBoardOne;
        }

        public int[,] GetGameBoardTwo()
        {
            return this._gameBoardTwo;
        }

        public int[,] GetGameBoardThree()
        {
            return this._gameBoardThree;
        }

        public void SetGameBoard()
        { 
            int n = rand.Next(2);
            if (n == 0)
                this._gameBoard = this.CopyBoard(_gameBoardOne);
            else if (n == 1)
                this._gameBoard = this.CopyBoard(_gameBoardTwo);
            else
                this._gameBoard = this.CopyBoard(_gameBoardThree);
        }

        public int[,] GetGameBoard()
        { 
            return this._gameBoard;
        }

        /// <summary>
        /// calculates the heuristic of the board sent as  parameter. that is no of pairs of queens attacking each other.
        /// </summary>
        /// <param name="tempBoard"></param>
        /// <returns>integer heuristic value</returns>
        public int CalculateHeuristic(int[,] tempBoard)
        {
            int[,] board = tempBoard;
            int heuristic = 0;

           
            for (int i = 0; i < this._numberOfQueens; i++)
            {
                int noQueens = 0;
                for (int j = 0; j < this._numberOfQueens; j++)
                {
                    if (board[i, j] == _QUEEN)
                        noQueens += 1;
                }             

                if (noQueens > 1)
                {
                    for (int queens = noQueens; queens > 1; --queens)
                    {
                        heuristic += queens - 1;
                    }
                }
            }

           
            for (int i = 0; i < this._numberOfQueens; i++)
            {
                int noQueens = 0;
                for (int j = 0; j < this._numberOfQueens; j++)
                {
                    if (board[j,i] == _QUEEN)
                        noQueens += 1;
                }

                if (noQueens > 1)
                {
                    for (int queens = noQueens; queens > 1; --queens)
                    {
                        heuristic += queens - 1;
                    }
                }
            }

           
            int numOfQueens = 0;
            int iteration = 0;
            while (iteration < (this._numberOfQueens - 1))
            {
                numOfQueens = 0;
                int xValue = 0;
                int yValue = iteration;

                while (xValue <= iteration)
                {
                    if (board[xValue,yValue] == _QUEEN)
                        numOfQueens += 1;

                    yValue -= 1;
                    xValue += 1;
                }

                if (numOfQueens > 1)
                {
                    for (int queens = numOfQueens; queens > 1; --queens)
                    {
                        heuristic += queens - 1;
                    }
                }

                iteration += 1;
            }

            
            numOfQueens = 0;
            int diagonal = this._numberOfQueens - 1;
            for (int i = 0; i < this._numberOfQueens; i++)
            {
                if (board[i,diagonal] == _QUEEN)
                    numOfQueens += 1;
                diagonal -= 1;
            }

            if (numOfQueens > 1)
            {
                for (int queens = numOfQueens; queens > 1; --queens)
                {
                    heuristic += queens - 1;
                }
            }

            
            numOfQueens = 0;
            iteration = 1;
            while (iteration < (this._numberOfQueens - 1))
            {
                numOfQueens = 0;
                int xValue = iteration;
                int yValue = this._numberOfQueens - 1;

                while (xValue < this._numberOfQueens)
                {
                    if (board[xValue,yValue] == _QUEEN)
                        numOfQueens += 1;

                    yValue -= 1;
                    xValue += 1;
                }
                if (numOfQueens > 1)
                {
                    for (int queens = numOfQueens; queens > 1; --queens)
                    {
                        heuristic += queens - 1;
                    }
                }

                iteration += 1;
            }

           
            numOfQueens = 0;
            iteration = this._numberOfQueens - 2;
            while (iteration > 0)
            {
                numOfQueens = 0;
                int xValue = 0;
                int yValue = iteration;

                while (yValue < this._numberOfQueens)
                {
                    if (board[xValue,yValue] == _QUEEN)
                        numOfQueens += 1;

                    yValue += 1;
                    xValue += 1;
                }

                if (numOfQueens > 1)
                {
                    for (int queens = numOfQueens; queens > 1; --queens)
                    {
                        heuristic += queens - 1;
                    }
                }

                iteration -= 1;
            }

           
            numOfQueens = 0;
            diagonal = 0;
            for (int i = 0; i < this._numberOfQueens; i++)
            {
                if (board[i,diagonal] == _QUEEN)
                    numOfQueens += 1;
                diagonal += 1;
            }

            if (numOfQueens > 1)
            {
                for (int queens = numOfQueens; queens > 1; --queens)
                {
                    heuristic += queens - 1;
                }
            }

          
            numOfQueens = 0;
            iteration = 1;
            while (iteration < this._numberOfQueens)
            {
                numOfQueens = 0;
                int xValue = iteration;
                int yValue = 0;

                while (xValue < this._numberOfQueens)
                {
                    if (board[xValue,yValue] == _QUEEN)
                        numOfQueens += 1;

                    yValue += 1;
                    xValue += 1;
                }
                if (numOfQueens > 1)
                {
                    for (int queens = numOfQueens; queens > 1; --queens)
                    {
                        heuristic += queens - 1;
                    }
                }

                iteration += 1;
            }

            return heuristic;
        }

        /// <summary>
        /// creates random game boards
        /// </summary>
        public void ResetBoard()
        { 
            int numOfQueens = this._numberOfQueens;
           
            for (int i = 0; i < numOfQueens; i++)
            {
                // RANDOM QUEEN GENERATION
                int queenPositionY = rand.Next(numOfQueens - 1);

                for (int j = 0; j < numOfQueens; j++)
                {
                    if (j == queenPositionY)
                        this._gameBoardOne[j,i] = _QUEEN;
                    else
                        this._gameBoardOne[j,i] = _NOT_QUEEN;
                }
            }

          
            for (int i = 0; i < numOfQueens; i++)
            {
                // RANDOM QUEEN GENERATION
                int queenPositionY = rand.Next(numOfQueens - 1);

                for (int j = 0; j < numOfQueens; j++)
                {
                    if (j == queenPositionY)
                        this._gameBoardTwo[j,i] = _QUEEN;
                    else
                        this._gameBoardTwo[j,i] = _NOT_QUEEN;
                }
            }

           
            for (int i = 0; i < numOfQueens; i++)
            {
                // RANDOM QUEEN GENERATION
                int queenPositionY = rand.Next(numOfQueens - 1);

                for (int j = 0; j < numOfQueens; j++)
                {
                    if (j == queenPositionY)
                        this._gameBoardThree[j,i] = _QUEEN;
                    else
                        this._gameBoardThree[j,i] = _NOT_QUEEN;
                }
            }

            this._numOfResets += 1; 
        }

        public int GetResets()
        {
            return this._numOfResets;
        }

        public void ResetRegularMoves()
        {
            this._regularMoves = 0;
        }

        public void ResetSidewaysMoves()
        {

            this._sidewaysMoves = 0;
        }
        
        public void StartGameBoard()
        {
            Console.WriteLine("\n\n------------------------------------");
            Console.WriteLine("Starting Board");
            Console.WriteLine("Current State (Heuristic): " + this.CalculateHeuristic(this._gameBoard));

            for (int x = 0; x < this._gameBoard.GetLength(0); x++)
            {
                for (int y = 0; y < this._gameBoard.GetLength(1); y++)
                {
                    Console.Write(this._gameBoard[x,y] + " ");
                }
                Console.WriteLine();
            }
        }

        public void PrintGameBoard()
        {
            Console.WriteLine("Current State (Heuristic): " + this.CalculateHeuristic(this._gameBoard));

            for (int x = 0; x < this._gameBoard.GetLength(0); x++)
            {
                for (int y = 0; y < this._gameBoard.GetLength(1); y++)
                {
                    Console.Write(this._gameBoard[x,y] + " ");
                }
                Console.WriteLine();
            }
        }

        public int[,] CopyBoard(int[,] oldBoard)
        {
            int[,] newBoard = new int[this.GetBoardSize(),this.GetBoardSize()];

            for (int i = 0; i < oldBoard.GetLength(0); i++)
            {
                for (int j = 0; j < oldBoard.GetLength(1); j++)
                {
                    newBoard[i,j] = oldBoard[i,j];
                }
            }
            return newBoard;
        }

        public Boolean BoardsAreEqual(int[,] boardOne, int[,] boardTwo)
        { 
            Boolean areEqual = true;

            for (int i = 0; i < this.GetBoardSize(); i++)
            {
                for (int j = 0; j < this.GetBoardSize(); j++)
                {
                    if (boardOne[i,j] != boardTwo[i,j])
                        areEqual = false;
                }
            }
            return areEqual;
        }
    }
}
