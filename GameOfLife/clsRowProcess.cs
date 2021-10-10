using System.Collections.Generic;
using System;

namespace GameOfLife
{
    public class clsRowProcess
    {
        //list of cells
        public List<clsCell> Cells { get; set; }

        public clsCell this[int y]
        {
            get { if (Cells.Count >= y) throw new ArgumentOutOfRangeException("Argument out of bound"); return Cells[y]; }
            set { if (Cells.Count >= y) throw new ArgumentOutOfRangeException("Argument out of bound"); Cells[y] = value; }
        }
     
        public clsRowProcess()
        {
            Cells = new List<clsCell>();
        }
       
        public void AddCell(clsCell cell)
        {
            Cells.Add(cell);
        }
     
        public void InsertCell(int index, clsCell cell, int ColumnCount)
        {
            if (index < 0 || index >= ColumnCount);
            Cells.Insert(index, cell);
        }

    }
}
