using System;
namespace MazeGame { 
class Program { 
    static void Main() {
        string ch = "";
        Console.WriteLine("T)test script G)Start Game Q)Quit");
        testScript();
        //ch = Console.ReadLine().ToUpper();
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
        CreateMaze(gameboard);
        // Generating maze with depth-first search
        Console.WriteLine("Generated maze");
        Console.WriteLine(MazePrint(gameboard));
        
        int graphsize = CheckNumOfNodes(gameboard);
        PathFinder pf = new PathFinder(new Graph(graphsize),gameboard);
        pf.BreadthFirstTraversal(gameboard.GetMazeCell(0,0));
        Console.WriteLine(pf.graph.PrintNodeMap());
    }
    static void testScript(){
        //testCell();
        //testMazeGrid();
        testCreateRandomGraph(10,10);
    }
    static void testCell(){
        Console.WriteLine("Class cell test:");
        Cell cell1= new Cell(1,1,false);
        Cell cell2= new Cell(9,0,true);
        Cell[] cells = new Cell[]{cell1,cell2};
        cell1.RightWall = false;
        cell2.LeftWall = false;
        cell2.Visited(true);
        Console.WriteLine(CellStatus(cell1));
        Console.WriteLine(CellStatus(cell2));
        Console.WriteLine();
        Console.Write(PrintCellFrontWall(cells));
        Console.Write(PrintCellLeftRightWall(cells));
        Console.Write(PrintCellBackWall(cells));
        Console.WriteLine();
    }
    static void testMazeGrid(){
                MazeGrid mazeBoard = new MazeGrid(3,3);
        mazeBoard.InitialiseMaze();
        mazeBoard.GetMazeCell(0,0).FrontWall = false;
        mazeBoard.GetMazeCell(2,2).BackWall = false;
        mazeBoard.GetMazeCell(2,2).Goal(true);
        mazeBoard.ClearWall(mazeBoard.GetMazeCell(0,0),mazeBoard.GetMazeCell(1,0));
        mazeBoard.ClearWall(mazeBoard.GetMazeCell(1,0),mazeBoard.GetMazeCell(2,0));
        mazeBoard.ClearWall(mazeBoard.GetMazeCell(2,0),mazeBoard.GetMazeCell(2,1));
        mazeBoard.ClearWall(mazeBoard.GetMazeCell(2,1),mazeBoard.GetMazeCell(2,2));
        mazeBoard.ClearWall(mazeBoard.GetMazeCell(1,0),mazeBoard.GetMazeCell(1,1));
        mazeBoard.ClearWall(mazeBoard.GetMazeCell(0,1),mazeBoard.GetMazeCell(1,1));
        mazeBoard.ClearWall(mazeBoard.GetMazeCell(0,1),mazeBoard.GetMazeCell(0,2));
        mazeBoard.ClearWall(mazeBoard.GetMazeCell(0,2),mazeBoard.GetMazeCell(1,2));
        mazeBoard.ClearWall(mazeBoard.GetMazeCell(1,2),mazeBoard.GetMazeCell(2,2));
        Console.WriteLine(CellStatus(mazeBoard.GetMazeCell(2,2)));
        Console.WriteLine(MazePrint(mazeBoard));
        mazeBoard.SetAllCellNotVisited();
        //Give cells their neighbours
        mazeBoard.SetConnectedCells();
        //Print their neighbour out
        Console.WriteLine(mazeBoard.PrintConnectedNeighbours());
        int size = CheckNumOfNodes(mazeBoard);
        Console.WriteLine($"There are {size} nodes");
        Graph testgraph = new Graph(size);
        PathFinder pathFinder = new PathFinder(testgraph,mazeBoard);
        testgraph = pathFinder.BreadthFirstTraversal(mazeBoard.GetMazeCell(0,0));
        Console.WriteLine(testgraph.PrintNodeMap());
        Console.WriteLine(testgraph.PrintNodeList());
    }
    static void testCreateRandomGraph(int width,int height){
        Console.WriteLine("Class Pathfinder test using random maze and graph structure");
        MazeGrid mazeBoard = new MazeGrid(width,height);
        CreateMaze(mazeBoard);
        Console.WriteLine("This is maze");
        Console.WriteLine(MazePrint(mazeBoard));
        int size = CheckNumOfNodes(mazeBoard);
        Console.WriteLine($"There are {size} nodes");
        Graph testgraph = new Graph(size);
        PathFinder pathFinder = new PathFinder(testgraph,mazeBoard);
        testgraph = pathFinder.BreadthFirstTraversal(mazeBoard.GetMazeCell(0,0));
        Console.WriteLine(testgraph.PrintNodeMap());
        Console.WriteLine(testgraph.PrintNodeList());
    }
    static void CreateMaze(MazeGrid maze){
        maze.InitialiseMaze();
        maze.GetMazeCell(0,0).FrontWall = false;
        maze.GetMazeCell(maze.Width()-1,maze.Height()-1).BackWall = false;
        maze.DFSGenerateMaze(null,maze.GetMazeCell(0,0));
        maze.SetAllCellNotVisited();
        maze.SetConnectedCells();
    }
    static string CellStatus(Cell cell){
        string message = "";
        message += $"x location: "+cell.X().ToString()+"\n";
        message += $"y location: "+cell.Y().ToString()+"\n";
        message += $"LeftWall: "+cell.LeftWall+"\n";
        message += $"RightWall: "+cell.RightWall+"\n";
        message += $"FrontWall: "+cell.FrontWall+"\n";
        message += $"BackWall: "+cell.BackWall+"\n";
        message += $"visited: "+cell.IsVisited()+"\n";
        message += "goal: "+cell.IsGoal();
        return message;
    }
    static string PrintCellFrontWall(Cell[] cells){
        string message = "";
        foreach (Cell cell in cells){
            if (cell.FrontWall){
            message += "+---+";
            }else{
            message += "+   +";
            }
        }
        message +="\n";
        return message;
    }
    static string PrintCellLeftRightWall(Cell[] cells){ 
        string message = "";
        foreach (Cell cell in cells){
            if (cell.LeftWall){
                message += "| ";
            }else{
                message += "  ";
            }
            message += " ";
            if (cell.RightWall){
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
            if (cell.BackWall){
                message += "+---+";
            }else{
                message += "+   +";
            }
        }
        return message;
        
    }
    static string MazePrint(MazeGrid _maze) {
        string mazeprintmessage ="".PadLeft(3);
        for (int i = 0;i<_maze.Width();i++){
            mazeprintmessage +="".PadLeft(2);
            mazeprintmessage += $"{Convert.ToString(i)}";
            mazeprintmessage +="".PadLeft(2);
        }
        mazeprintmessage += "\n".PadRight(4);
        mazeprintmessage += PrintCellFrontWall(_maze.GetMazeRows(0));
        for (int j = 0;j<_maze.Height();j++){
            Cell[] rowOfCells = _maze.GetMazeRows(j);
            mazeprintmessage += $"{j}".PadRight(3);
            mazeprintmessage += PrintCellLeftRightWall(rowOfCells);
            mazeprintmessage += "".PadLeft(3);
            mazeprintmessage += PrintCellBackWall(rowOfCells);
            mazeprintmessage+="\n";
        }
        return mazeprintmessage;
    }
    static int CheckNumOfNodes(MazeGrid _maze){
        int size = 0;
        int w = _maze.Width();
        int h = _maze.Height();
        foreach(Cell cell in _maze._mazeGrid){
            if (cell != null){
                //check if its a junction or a turning point or a dead end or a starting point or a end point
                if (IsNode(cell,w,h)) {
                    size++;
                }
            }
        }
        return size;
    }
    static bool IsNode(Cell cell,int w,int h){
            if((cell.X()==0&&cell.Y()==0)|(cell.X()==w-1&&cell.Y()==h-1)){
                return true;
            }else if (cell.FrontWall&&cell.BackWall&&!cell.RightWall&&!cell.LeftWall){
                return false;
            }else if (!cell.FrontWall&&!cell.BackWall&&cell.RightWall&&cell.LeftWall){
                return false;
            }else{
                return true;
            }
        }
}
}