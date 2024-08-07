using System;
namespace MazeGame { 
class Program { 
    static void Main() {
        string ch = "";
        Console.WriteLine("T)test script G)Start Game Q)Quit");
        ch = Console.ReadLine().ToUpper();
        switch (ch) {
            case "T":{
                testScript();
                break;
            }
            case "G":{
                Game();
                break;
            }
            case "Q":{
                break;
            }
        }
    }
    static void Game() {
        bool valid = false;
        int[] intcommand = new int[2];
        do {
            try{
                Console.Write("Maze size (width,depth): ");
                string size = Console.ReadLine();
                string[] strcommand = size.Split(",");
                for (int i = 0; i < strcommand.Length;i++){
                    intcommand[i] = int.Parse(strcommand[i]);
                }
                valid = true;
            }catch{
                Console.WriteLine("Invalid choice");
            }
        }while(valid==false);
        MazeGrid gameboard = new MazeGrid(intcommand[0],intcommand[1]);
        // Initialising cells in the maze
        gameboard.InitialiseMaze();
        Console.WriteLine("Not traverse maze");
        Console.WriteLine(MazePrint(gameboard));
        // Open start cell wall
        gameboard.GetMazeCell(0,0).RmFrontWall();
        // Open goal cell wall
        gameboard.GetMazeCell(intcommand[0]-1,intcommand[1]-1).RmBackWall();
        // Generating maze with depth-first search
        gameboard.DFSGenerateMaze(null,gameboard.GetMazeCell(0,0));
        Console.WriteLine("Generated maze");
        Console.WriteLine(MazePrint(gameboard));
    }
    static void testScript(){
        Cell cell1= new Cell(1,1,false);
        Cell cell2= new Cell(9,0,true);
        Cell[] cells = new Cell[]{cell1,cell2};
        cell1.RmRightWall();
        cell2.RmLeftWall();
        cell2.Visited();
        Console.WriteLine(CellStatus(cell1));
        Console.WriteLine(CellStatus(cell2));
        Console.WriteLine();
        Console.Write(PrintCellFrontWall(cells));
        Console.Write(PrintCellLeftRightWall(cells));
        Console.Write(PrintCellBackWall(cells));
        Console.WriteLine();

        MazeGrid mazeBoard = new MazeGrid(2,2);
        mazeBoard.SetMazeCell(0,0,new Cell(0,0,false));
        mazeBoard.GetMazeCell(0,0).RmFrontWall();
        mazeBoard.SetMazeCell(1,0,new Cell(1,0,false));
        mazeBoard.ClearWall(mazeBoard.GetMazeCell(0,0),mazeBoard.GetMazeCell(1,0));
        mazeBoard.SetMazeCell(0,1,new Cell(0,1,false));
        mazeBoard.SetMazeCell(1,1,new Cell(1,1,true));
        mazeBoard.ClearWall(mazeBoard.GetMazeCell(1,0),mazeBoard.GetMazeCell(1,1));
        mazeBoard.ClearWall(mazeBoard.GetMazeCell(0,1),mazeBoard.GetMazeCell(1,1));
        mazeBoard.GetMazeCell(1,1).RmBackWall();
        Console.WriteLine(MazePrint(mazeBoard));

        MazeGrid testMaze = new MazeGrid(5,5);
        testMaze.InitialiseMaze();
        testMaze.GetMazeCell(0,0).RmFrontWall();
        testMaze.GetMazeCell(4,4).RmBackWall();
        testMaze.DFSGenerateMaze(null,testMaze.GetMazeCell(0,0));
        Console.WriteLine(MazePrint(testMaze));
    }
    static string CellStatus(Cell cell){
        string message = "";
        message += $"x location: "+cell.X().ToString()+"\n";
        message += $"y location: "+cell.Y().ToString()+"\n";
        message += $"leftWall: "+cell.leftWall+"\n";
        message += $"rightWall: "+cell.rightWall+"\n";
        message += $"frontWall: "+cell.frontWall+"\n";
        message += $"backWall: "+cell.backWall+"\n";
        message += $"visited: "+cell.IsVisited()+"\n";
        message += "goal: "+cell.IsGoal();
        return message;
    }
    static string PrintCellFrontWall(Cell[] cells){
        string message = "";
        foreach (Cell cell in cells){
            if (cell.frontWall){
            message += "+--+";
            }else{
            message += "+  +";
            }
        }
        message +="\n";
        return message;
    }
    static string PrintCellLeftRightWall(Cell[] cells){ 
        string message = "";
        foreach (Cell cell in cells){
            if (cell.leftWall){
                message += "| ";
            }else{
                message += "  ";
            }
            if (cell.rightWall){
                message += " |";
            }else{
                message += "  ";
            }
        }
        message+="\n";
        return message;
    }
    static string PrintCellBackWall(Cell[] cells){
        string message = "";
        foreach (Cell cell in cells){
            if (cell.backWall){
                message += "+--+";
            }else{
                message += "+  +";
            }
        }
        message+="\n";
        return message;
        
    }
    static string MazePrint(MazeGrid _maze) {
        string mazeprintmessage = "  ";
        for (int i = 0;i<_maze.Width();i++ ){
            mazeprintmessage += $"{Convert.ToString(i+1).PadLeft(3)} ";
        }
        mazeprintmessage += "\n  ";
        mazeprintmessage += PrintCellFrontWall(_maze.GetMazeRows(0));
        for (int j = 0;j<_maze.Height();j++){
            Cell[] rowOfCells = _maze.GetMazeRows(j);
            mazeprintmessage += $"{j+1} ";
            mazeprintmessage += PrintCellLeftRightWall(rowOfCells);
            mazeprintmessage += "  ";
            mazeprintmessage += PrintCellBackWall(rowOfCells);
        }
        return mazeprintmessage;
    }
} 
}