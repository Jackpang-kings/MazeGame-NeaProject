using System;
namespace MazeGame{
class Cell{
    int x,y;
    public bool LeftWall { get; set; } = true;
    public bool RightWall { get; set; } = true;
    public bool FrontWall { get; set; } = true;
    public bool BackWall { get; set; } = true;

    public List<Cell> neighbourCells;
    public List<Cell> connectedCells;
    bool visited, goal;
    
    public Cell(int X,int Y,bool g = false){
        x = X;
        y = Y;
        goal = g;
        visited = false;
        neighbourCells = new List<Cell>();
        connectedCells = new List<Cell>();
    }
    public int X(){
        return x;
    }
    public int Y(){
        return y;
    }
    public bool IsVisited(){
        return visited;
    }
    public void Visited(bool b){
        visited = b;
    }
    public bool IsGoal(){
        return goal;
    }
    public void Goal(bool g){
        goal = g;
    }
    public void AddConnectedCell(Cell cell){
        connectedCells.Add(cell);
    }
    public List<Cell> GetConnectedCells(){
        return connectedCells;
    }
    public string CellStatus(Cell cell){
        string message = "";
        message += $"x location: "+cell.X().ToString()+"\n";
        message += $"y location: "+cell.Y().ToString()+"\n";
        message += $"leftWall: "+cell.LeftWall+"\n";
        message += $"rightWall: "+cell.RightWall+"\n";
        message += $"frontWall: "+cell.FrontWall+"\n";
        message += $"backWall: "+cell.BackWall+"\n";
        message += $"visited: "+cell.IsVisited()+"\n";
        message += "goal: "+cell.IsGoal();
        return message;
    }
}
}