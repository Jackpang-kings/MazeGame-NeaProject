using System;
using System.Diagnostics;
using System.Globalization;
using Microsoft.CSharp;

namespace MazeGame{
    class MazeGrid {
    public Cell[,] _mazeGrid;
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
    public void InitialiseMaze(){
        for (int i = 0;i<width;i++){
            for (int j = 0;j<height;j++){
                _mazeGrid[i,j] = new Cell(i,j,false);
            }
        }
        // Add neighbour cells to cell
        for (int i = 0;i<width;i++){
            for (int j = 0;j<height;j++){
                if (i+1<width){
                    _mazeGrid[i,j].neighbourCells.Add(_mazeGrid[i+1,j]);
                }
                if (i-1>=0){
                    _mazeGrid[i,j].neighbourCells.Add(_mazeGrid[i-1,j]);
                }
                if (j+1<height){
                    _mazeGrid[i,j].neighbourCells.Add(_mazeGrid[i,j+1]);
                }
                if (j-1>=0){
                    _mazeGrid[i,j].neighbourCells.Add(_mazeGrid[i,j-1]);
                }
            }
        }
        
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
    public void DFSGenerateMaze(Cell prevCell,Cell currCell) {
        currCell.Visited(true);
        ClearWall(prevCell,currCell);
        Cell nextcell;
        do{
            nextcell = NeighbourCell(currCell);
            if (nextcell!=null){
                DFSGenerateMaze(currCell,nextcell);
            }
        }while (nextcell != null);
    } 
    public void SetAllCellNotVisited(){
        foreach (Cell cell in _mazeGrid){
            cell.Visited(false);
        }
    }
    public void ClearWall(Cell prevCell,Cell currCell) {
        if (prevCell!=null){
            if (prevCell.X() > currCell.X()){
                prevCell.LeftWall = false;
                currCell.RightWall = false;
            }else if (prevCell.X() < currCell.X()){
                prevCell.RightWall = false;
                currCell.LeftWall = false;
            }else if (prevCell.Y() > currCell.Y()){
                prevCell.FrontWall = false;
                currCell.BackWall = false;
            }else if (prevCell.Y() < currCell.Y()){
                prevCell.BackWall = false;
                currCell.FrontWall = false;
            }
        }
    }
    public List<Cell> RetrieveAdjacentNodes(List<Cell> cells){
        List<Cell> adjcells = new List<Cell>();
        foreach (Cell cell in cells){
            if (!cell.IsVisited()){
                adjcells.Add(cell);
            }
        }
        return adjcells; 
    }
    List<Cell> FYShuffleList(List<Cell> rndList){
        Random rnd = new Random();
        for(int i = rndList.Count-1;i>0;i--){
            var k = rnd.Next(i+1);
            Cell tempCell = rndList[k];
            rndList[k] = rndList[i];
            rndList[i] = tempCell;
        }
        return rndList;
    }
    public Cell NeighbourCell(Cell currCell){
        List<Cell> AdjCell = RetrieveAdjacentNodes(currCell.neighbourCells);
        AdjCell = FYShuffleList(AdjCell);
        return AdjCell.FirstOrDefault();
    }
    public string PrintConnectedNeighbours(){
        string message = "";
        foreach (Cell cell in _mazeGrid){
            message+=$"{cell.X()},{cell.Y()}:";
            for (int i = 0;i<cell.connectedCells.Count;i++){
                message+=$"({cell.connectedCells[i].X()},{cell.connectedCells[i].Y()}) | ";
            }
            message+=$"\n";
        }
        return message;
    }
    public void SetConnectedCells(){
        foreach (Cell currcell in _mazeGrid){
            if (!currcell.FrontWall && currcell.Y()-1 >= 0){
                currcell.AddConnectedCell(GetMazeCell(currcell.X(), currcell.Y()-1));
            }
            if (!currcell.BackWall && currcell.Y()+1 < height){
                currcell.AddConnectedCell(GetMazeCell(currcell.X(), currcell.Y()+1));
            }
            if (!currcell.LeftWall && currcell.X()-1 >= 0){
                currcell.AddConnectedCell(GetMazeCell(currcell.X()-1, currcell.Y()));
            }
            if (!currcell.RightWall && currcell.X()+1 < width){
                currcell.AddConnectedCell(GetMazeCell(currcell.X()+1, currcell.Y()));
            }
        }
        }
    public void CreateMaze(MazeGrid maze){
        maze.InitialiseMaze();
        maze.GetMazeCell(0,0).FrontWall = false;
        maze.GetMazeCell(maze.Width()-1,maze.Height()-1).BackWall = false;
        maze.DFSGenerateMaze(null,maze.GetMazeCell(0,0));
        SetConnectedCells();
    }
}
}