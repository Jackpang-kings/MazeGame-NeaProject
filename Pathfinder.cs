
using System;
using System.Diagnostics;
using System.Globalization;
using Microsoft.CSharp;
namespace MazeGame{
    class PathFinder {
        public Graph graph;
        public MazeGrid _maze;
        Dictionary<Cell, Node> cellNodeMap;
        public PathFinder(Graph g, MazeGrid m) {
            graph = g;
            _maze = m;
            cellNodeMap = new Dictionary<Cell, Node>();
            int nodecounter = 0;
            for(int i = 0;i<_maze.Width();i++){
                for(int j = 0;j<_maze.Height();j++){
                    Cell c = _maze._mazeGrid[i,j];
                    if (IsNode(c)){
                        Node newNode = new Node(nodecounter, c.X(), c.Y());
                        nodecounter++;
                        graph.AddNode(newNode);
                        cellNodeMap[c] = newNode;
                    }
                } 
            }
        }
        public Graph BreadthFirstTraversal(Cell startCell) {
            _maze.SetAllCellNotVisited();
            Queue<Cell> queue = new Queue<Cell>();
            // Setup all nodes
            queue.Enqueue(startCell);
            startCell.Visited(true);
            while (queue.Count > 0) {
                Cell c = queue.Dequeue();
                // Connect this node with other nodes found so far
                if (IsNode(c)){
                    CheckNodesRelationship(c);
                }
                // Enqueue unvisited connected neighbors
                foreach (var neighbor in c.connectedCells){
                    if (!neighbor.IsVisited()) {
                        neighbor.Visited(true);
                        queue.Enqueue(neighbor);
                    }
                }
            }
            return graph;
        }
        public bool IsNode(Cell cell){
            if(cell.X()==0&&cell.Y()==0){
                return true;
            }else if (cell.X()==_maze.Width()-1&&cell.Y()==_maze.Height()-1){
                return true;
            }
            else if (cell.FrontWall&&cell.BackWall){
                if (cell.RightWall^cell.LeftWall){
                    return true;
                }
            }else if (cell.RightWall&&cell.LeftWall){
                if (cell.FrontWall^cell.BackWall){
                    return true;
                }
            }else if (!((cell.FrontWall&&cell.BackWall)|(cell.LeftWall&&cell.RightWall))){
                return true;
            }
            return false;
        }
        public void CheckNodesRelationship(Cell c){
            Node prevNode;
            Node newNode = cellNodeMap[c];
            List<(Node,int)> values = new List<(Node,int)>();
            bool found = false;
            int x = c.X();
            int y = c.Y();
            if (!c.LeftWall){
                while(!found&&x>=1){
                    x--;
                    if (IsNode(_maze._mazeGrid[x,c.Y()])&&_maze._mazeGrid[x,c.Y()]!=c){
                        prevNode = cellNodeMap[_maze._mazeGrid[x,c.Y()]];
                        int val = graph.GetDistanceBetweenNodes(newNode, prevNode);
                        graph.AddNodesInNodeMap(prevNode, newNode,val);
                        values.Add((prevNode,val));
                        found=true;
                    }
                }
                found=false;
            }
            if (!c.RightWall){
                while(!found&&x<_maze.Width()-1){
                    x++;
                    if (IsNode(_maze._mazeGrid[x,c.Y()])&&_maze._mazeGrid[x,c.Y()]!=c){
                        prevNode = cellNodeMap[_maze._mazeGrid[x,c.Y()]];
                        int val = graph.GetDistanceBetweenNodes(newNode, prevNode);
                        graph.AddNodesInNodeMap(prevNode, newNode,val);
                        values.Add((prevNode,val));
                        found=true;
                    }  
                }
                found=false;
            }
            if (!c.FrontWall){
                while(!found&&y>=1){
                    y--;
                    if (IsNode(_maze._mazeGrid[c.X(),y])&&_maze._mazeGrid[c.X(),y]!=c){
                        prevNode = cellNodeMap[_maze._mazeGrid[c.X(),y]];
                        int val = graph.GetDistanceBetweenNodes(newNode, prevNode);
                        graph.AddNodesInNodeMap(prevNode, newNode,val);
                        values.Add((prevNode,val));
                        found=true;
                    }
                }
                found=false;
            }
            if (!c.BackWall){
                while(!found&&y<_maze.Height()-1){
                    y++;
                    if (IsNode(_maze._mazeGrid[c.X(),y])&&_maze._mazeGrid[c.X(),y]!=c){
                        prevNode = cellNodeMap[_maze._mazeGrid[c.X(),y]];
                        int val = graph.GetDistanceBetweenNodes(newNode, prevNode);
                        graph.AddNodesInNodeMap(prevNode, newNode,val);
                        values.Add((prevNode,val));
                        found=true;
                    }
                }
            }
            graph.AddNodesInNodeList(newNode,values);
        }
    }
}