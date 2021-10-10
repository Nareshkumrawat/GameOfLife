using System;
using System.Threading.Tasks;

namespace GameOfLife
{
    public class clsGameOLife
    {
        private clsGrid _inputGrid;
        private clsGrid _outputGrid;
        public clsGrid InputGrid { get { return _inputGrid; } }  
        public clsGrid OutputGrid { get { return _outputGrid; } } 
        
        private Task EvaluateCellTask;
        private Task EvaluateGridGrowthTask;
        public int MaxGenerations = 1; //set deafult as 1

        public int RowCount { get { return InputGrid.RowCount; } }
        public int ColumnCount { get { return InputGrid.ColumnCount; } }

       
        public clsGameOLife(int rows, int columns)
        {
            if (rows <= 0 || columns <= 0) throw new ArgumentOutOfRangeException("Row and Column size must be greater than zero");
            _inputGrid = new clsGrid(rows, columns);
            _outputGrid = new clsGrid(rows, columns);
            clsCellLogic.InitializeReachableCells();
        }
               
        public void ToggleGridCell(int x, int y)
        {            
            if (_inputGrid.RowCount <= x || _inputGrid.ColumnCount <= y) throw new ArgumentOutOfRangeException("Argument out of bound");
            _inputGrid.ToggleCell(x, y);

        }

       public void Init()
        {
            Start();
        }
        /// <summary>
        /// Start Game of Life
        /// </summary>
        private void Start()
        {
            int currentGeneration = 0;
            clsHelper.Display(_inputGrid);
            do
            {
                currentGeneration++;
                ProcessGeneration();

                Console.WriteLine("Generation: "+currentGeneration);                
                clsHelper.Display(_inputGrid);
            } while (currentGeneration < MaxGenerations);
        }
        
        private void ProcessGeneration()
        {            
            SetNextGeneration();
            Tick();
            FlipGridState();
        }

        private void SetNextGeneration()
        {
            if ((EvaluateCellTask == null) || (EvaluateCellTask != null && EvaluateCellTask.IsCompleted))
            {
                EvaluateCellTask = ChangeCellsState();
                EvaluateCellTask.Wait();  
            }
            if ((EvaluateGridGrowthTask == null) || (EvaluateGridGrowthTask != null && EvaluateGridGrowthTask.IsCompleted))
            {
                EvaluateGridGrowthTask = ChangeGridState();
            }
        }
        
        private void Tick()
        {            
            if (EvaluateGridGrowthTask != null)
            {
                EvaluateGridGrowthTask.Wait();
            }
        }

        private void FlipGridState()
        {
            clsHelper.Copy(_outputGrid, _inputGrid);
            _outputGrid.ReInitialize();
        }

        
        private Task ChangeCellsState()
        {
            return Task.Factory.StartNew(() =>
            Parallel.For(0, _inputGrid.RowCount, x =>
            {
                Parallel.For(0, _inputGrid.ColumnCount, y =>
                {
                    clsAllRule.ChangeCellsState(_inputGrid, _outputGrid, new CoOrdinates(x, y));
                });
            }));
        }
 
        private Task ChangeGridState()
        {
            return Task.Factory.StartNew(delegate()
                {
                    clsAllRule.ChangeGridState(_inputGrid, _outputGrid);
                }
            );
        }
    }
}
