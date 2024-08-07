using System;
namespace MazeGame{
    class MazeGrid {
    Cell[,] _mazeGrid;
    int width, height;
    public MazeGrid(int w,int h){
        width = w;
        height = h;
        _mazeGrid = new Cell[width,height];
    }
    public Cell[] GetMazeRows(int j){
        Cell[] rowOfCells = new Cell[width];
        for (int i = 0;i < width;i++){
            rowOfCells[i] = _mazeGrid[i,j];
        }
        return rowOfCells;
    }
    public int Width(){
        return width;
    }
    public int Height(){
        return height;
    }
    public void SetMazeCell(int i, int j,Cell cell){
        _mazeGrid[i,j] = cell;
    }
    public Cell GetMazeCell(int i, int j){
        return _mazeGrid[i,j];
    }
    public void InitialiseMaze(){
        for (int i = 0;i<width;i++){
            for (int j = 0;j<height;j++){
                _mazeGrid[i,j] = new Cell(i,j,false);
                if (i==width-1 && j==height-1){
                    _mazeGrid[i,j].Goal();
                }
            }
        }
    }
    public void DFSGenerateMaze(Cell prevCell,Cell currCell) {
        currCell.Visited();
        ClearWall(prevCell,currCell);
        Cell nextcell;
        do{
            nextcell = NeighbourNode(currCell);
            if (nextcell!=null){
                DFSGenerateMaze(currCell,nextcell);
            }
        }while (nextcell != null);
    }
    public void ClearWall(Cell prevCell,Cell currCell) {
        if (prevCell!=null){
            if (prevCell.X() > currCell.X()){
                prevCell.RmLeftWall();
                currCell.RmRightWall();
            }else if (prevCell.X() < currCell.X()){
                prevCell.RmRightWall();
                currCell.RmLeftWall();
            }else if (prevCell.Y() > currCell.Y()){
                prevCell.RmFrontWall();
                currCell.RmBackWall();
            }else if (prevCell.Y() < currCell.Y()){
                prevCell.RmBackWall();
                currCell.RmFrontWall();
            }
        }
    }
    List<Cell> RetrieveAdjacentNodes(Cell currCell){
        List<Cell> AdjacentCells = new List<Cell>();
        int x = currCell.X();
	    int y = currCell.Y();
        if (currCell.X()+1 < width){
            if (!_mazeGrid[currCell.X()+1,y].IsVisited()){
                AdjacentCells.Add(_mazeGrid[currCell.X()+1,y]);
            }
        }
        if (currCell.X()-1 >= 0){
            if (!_mazeGrid[currCell.X()-1,y].IsVisited()){
                AdjacentCells.Add(_mazeGrid[currCell.X()-1,y]);
            }
        }
        if (currCell.Y()+1 < height){
            if (!_mazeGrid[x,currCell.Y()+1].IsVisited()){
                AdjacentCells.Add(_mazeGrid[x,currCell.Y()+1]);
            }
            
        }
        if (currCell.Y()-1 >= 0){
            if (!_mazeGrid[x,currCell.Y()-1].IsVisited()){
                AdjacentCells.Add(_mazeGrid[x,currCell.Y()-1]);
            }
        }
        return AdjacentCells;
    } 
    List<Cell> RandomSortList(List<Cell> cells){
        Random rnd = new Random();
        List<Cell> rndList = new List<Cell>();
        rndList = cells.OrderBy(x => rnd.Next()).ToList();
        return rndList;
    }
    public Cell NeighbourNode(Cell currCell){
        List<Cell> AdjCell = RetrieveAdjacentNodes(currCell);
        AdjCell = RandomSortList(AdjCell);
        return AdjCell.FirstOrDefault();
    }
}
}