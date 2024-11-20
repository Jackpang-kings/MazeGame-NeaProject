using System;
using System.Diagnostics;
using System.Globalization;
using Microsoft.CSharp;
namespace MazeGame{
    class Node{
        int nodeid;
        int x,y;
        bool visited;
        public Node(int id,int a,int b){
            nodeid = id;
            x = a;
            y = b;
            visited = false;
        }
        public int X() {
            return x;
        }
        public int Y() {
            return y;
        }
        public int getNodeID(){
            return nodeid;
        }
        public bool isVisited(){
            return visited;
        }
        public void Visited(){
            visited = true;
        }
        public void NotVisited(){
            visited = false;
        }
    }

}