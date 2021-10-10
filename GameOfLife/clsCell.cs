using System;

namespace GameOfLife
{
    public class clsCell
    {

        public Boolean IsAlive { get; set; }

        public clsCell(Boolean isAlive)
        {
            IsAlive = isAlive;
        }
       
        public override string ToString()
        {
            return (IsAlive ? " X " : " - ");
        }
    }
}
