using System;
using System.Collections.Generic;

namespace GameOfLife
{
    public class clsGrid
    {

        public List<clsRowProcess> GridObj { set; get; }

        public clsGrid(int rows, int columns)
        {            
            Setup(rows, columns);
        }

        public clsCell this[int x, int y]
        {
            get { if (GridObj.Count <= x || ColumnCount <= y) throw new ArgumentOutOfRangeException("Out of bound"); return GridObj[x].Cells[y]; }
            set { if (GridObj.Count <= x || ColumnCount <= y) throw new ArgumentOutOfRangeException("Out of bound"); GridObj[x].Cells[y] = value; }
        }
        public clsRowProcess this[int x]
        {
            get { if (GridObj.Count <= x) throw new ArgumentOutOfRangeException("Out of bound"); return GridObj[x]; }
            set { if (GridObj.Count <= x) throw new ArgumentOutOfRangeException("Out of bound"); GridObj[x] = value; }
        }
        public int RowCount { get { return GridObj.Count; } }
        public int ColumnCount { set; get; }

        public void ReInitialize()
        {
            Setup(RowCount, ColumnCount);
        }
        private void Setup(int rows, int columns)
        {
            if (rows <= 0 || columns <= 0) throw new ArgumentOutOfRangeException("Row and Column size must be greater than zero");
            GridObj = new List<clsRowProcess>();
            for (int i = 0; i < rows; i++)
            {
                clsRowProcess row = new clsRowProcess();
                for (int j = 0; j < columns; j++)
                {
                    clsCell cell = new clsCell(false);
                    row.AddCell(cell);
                }
                GridObj.Add(row);
            }
            ColumnCount = columns;
        }
        public void ToggleCell(int x, int y)
        {
            if (GridObj.Count <= x || ColumnCount <= y) throw new ArgumentNullException("Cell doesn't have data");
            this[x, y].IsAlive = !this[x, y].IsAlive;
        }
        public void InsertRow(int index, clsRowProcess row)
        {
            if (index < 0 || index >= RowCount) throw new ArgumentOutOfRangeException("Invalid Index value");
            GridObj.Insert(index, row);
        }

        public void AddRow(clsRowProcess row)
        {
            GridObj.Add(row);
        }

    }

}
