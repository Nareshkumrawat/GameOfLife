using System;
using System.Collections.Generic;

namespace GameOfLife
{
    class clsAllRule
    {
     
        private clsAllRule()
        {

        }

       
        public static void ChangeCellsState(clsGrid inputGrid, clsGrid outputGrid, CoOrdinates coOrdinates)
        {
            int liveNeighbourCount = CountAliveNeighbours(inputGrid, coOrdinates);
            lock (outputGrid)
            {
                if (IsAliveInNextState(inputGrid[coOrdinates.X, coOrdinates.Y], liveNeighbourCount))
                {
                    outputGrid[coOrdinates.X, coOrdinates.Y].IsAlive = true;
                }

            }

        }

      
        private static int CountAliveNeighbours(clsGrid grid, CoOrdinates coOrdinates)
        {
            int liveNeighbours = 0;
            CellTypeEnum enumInnerCell = clsCellLogic.GetCellType(grid, coOrdinates);
            List<CoOrdinates> reachableCells = new List<CoOrdinates>();
            clsCellLogic.ReachableCells.TryGetValue(enumInnerCell, out reachableCells);
            if (reachableCells.Count == 0) throw new ArgumentNullException("Cannot find reachable co-ordinates");
            foreach (CoOrdinates coOrds in reachableCells)
            {
                liveNeighbours += IsAliveNeighbour(grid, coOrdinates, coOrds);
            }
            return liveNeighbours;
        }


       
        private static int IsAliveNeighbour(clsGrid grid, CoOrdinates baseCoOrdinates, CoOrdinates offSetCoOrdinates)
        {
            int live = 0; // set default as 0
            int x = baseCoOrdinates.X + offSetCoOrdinates.X; 
            int y = baseCoOrdinates.Y + offSetCoOrdinates.Y; 
            if ((x >= 0 && x < grid.RowCount) && y >= 0 && y < grid.ColumnCount)
            {
                live = grid[x, y].IsAlive ? 1 : 0;
            }

            return live;
        }

    
        private static Boolean IsAliveInNextState(clsCell cell, int liveNeighbourCount)
        {
            Boolean alive = false;
            if (cell.IsAlive)
            {
                if (liveNeighbourCount == 2 || liveNeighbourCount == 3)
                {
                    alive = true;
                }
            }
            else if (liveNeighbourCount == 3)
            {
                alive = true;
            }
            return alive;
        }

      
        public static void ChangeGridState(clsGrid inputGrid, clsGrid outputGrid)
        {
            CheckRowGrowth(inputGrid, outputGrid, -1);
            CheckRowGrowth(inputGrid, outputGrid, inputGrid.RowCount);
            CheckColumnGrowth(inputGrid, outputGrid, -1);
            CheckColumnGrowth(inputGrid, outputGrid, inputGrid.ColumnCount);
        }

      
        private static void CheckColumnGrowth(clsGrid inputGrid, clsGrid outputGrid, int colId)
        {
            Boolean columnCreatedFlag = false;
           for (int i = 1; i < inputGrid.RowCount - 1; i++)
            {
                if (clsAllRule.CountAliveNeighbours(inputGrid, new CoOrdinates(i, colId)) == 3)
                {
                    if (columnCreatedFlag == false)
                    {
                       for (int k = 0; k < outputGrid.RowCount; k++)
                        {
                            clsCell newDeadCell = new clsCell(false);
                            if (colId == -1)
                            {
                                outputGrid[k].InsertCell(0, newDeadCell, outputGrid.ColumnCount);
                            }
                            else
                            {
                                outputGrid[k].AddCell(newDeadCell);
                            }
                        }
                        outputGrid.ColumnCount += 1;
                        columnCreatedFlag = true;
                    }
                    int yAxis = (colId == -1) ? 0 : outputGrid.ColumnCount - 1;
                    outputGrid[i, yAxis].IsAlive = true;
                }
            }
        }
        private static void CheckRowGrowth(clsGrid inputGrid, clsGrid outputGrid, int rowId)
        {
            Boolean rowCreatedFlag = false;
          for (int j = 1; j < inputGrid.ColumnCount - 1; j++)
            {
                if (clsAllRule.CountAliveNeighbours(inputGrid, new CoOrdinates(rowId, j)) == 3)
                {
                    if (rowCreatedFlag == false)
                    {
                        clsRowProcess newRow = new clsRowProcess();
                       for (int k = 0; k < outputGrid.ColumnCount; k++)
                        {
                            clsCell newDeadCell = new clsCell(false);
                            newRow.AddCell(newDeadCell);
                        }
                        if (rowId == -1)
                        {
                            outputGrid.InsertRow(0, newRow);
                        }
                        else
                        {
                            outputGrid.AddRow(newRow);
                        }
                        rowCreatedFlag = true;
                    }
                    int XAxis = (rowId == -1) ? 0 : outputGrid.RowCount - 1;
                    outputGrid[XAxis, j].IsAlive = true;
                }
            }
        }

    }

}
