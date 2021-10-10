using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameOfLife
{
    static class clsHelper
    {
       
        public static void Display(clsGrid grid)
        {
            foreach (clsRowProcess row in grid.GridObj)
            {
                foreach (clsCell cell in row.Cells)
                {
                    Console.Write(cell.ToString());
                }
                Console.WriteLine();
            }
        }

      
        public static void Copy(clsGrid sourceGrid, clsGrid targetGrid)
        {
            MatchSchema(sourceGrid, targetGrid);
            targetGrid.ReInitialize();
            AssignCellValues(sourceGrid, targetGrid);
        }

      
        private static void MatchSchema(clsGrid sourceGrid, clsGrid targetGrid)
        {
            while (targetGrid.RowCount < sourceGrid.RowCount)
            {
                clsRowProcess newRow = new clsRowProcess();
                for (int k = 0; k < targetGrid.ColumnCount; k++)
                {
                    clsCell newCell = new clsCell(false);
                    newRow.AddCell(newCell);
                }
                targetGrid.AddRow(newRow);
            }
            while (targetGrid.ColumnCount < sourceGrid.ColumnCount)
            {
                clsCell cell = new clsCell(false);
                for (int k = 0; k < targetGrid.RowCount; k++)
                {
                    targetGrid[k].AddCell(cell);
                }
                targetGrid.ColumnCount += 1;
            }

        }

        
        private static void AssignCellValues(clsGrid sourceGrid, clsGrid targetGrid)
        {
            for (int i = 0; i < sourceGrid.RowCount; i++)
            {
                for (int j = 0; j < sourceGrid.ColumnCount; j++)
                {
                    targetGrid[i, j].IsAlive = sourceGrid[i, j].IsAlive;
                }
            }
        }
        
    }
}
