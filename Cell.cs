using System;
namespace MazeGame{
class Cell{
    int x,y;
    public bool leftWall{get;set;}
    public bool rightWall{get;set;}
    public bool frontWall{get;set;}
    public bool backWall{get;set;}
    bool visited, goal;
    
    public Cell(int X,int Y,bool g = false){
        x = X;
        y = Y;
        goal = g;
        leftWall = true;
        rightWall = true;
        frontWall = true;
        backWall = true;
        visited = false;
    }

    public void RmLeftWall(){
        leftWall = false;
    }
    public void RmRightWall(){
        rightWall = false;
    }
    public void RmFrontWall(){
        frontWall = false;
    }
    public void RmBackWall(){
        backWall = false;
    }
    public bool IsVisited(){
        return visited;
    }
    public void Visited(){
        visited = true;
    }
    public bool IsGoal(){
        return goal;
    }
    public void Goal(){
        goal = true;
    }
    public int X(){
        return x;
    }
    public int Y(){
        return y;
    }
}
}