using System;
using System.Formats.Asn1;
namespace MazeGame{
    class Graph{
        public List<Node> nodes;
        public Dictionary<(Node,Node), int> NodeMap;
        public Dictionary<Node,List<(Node,int)>> NodeList;
        public int size;

        public Graph(int n){
            nodes = new List<Node>(n);
            NodeMap = new Dictionary<(Node n1,Node n2), int>();
            NodeList = new Dictionary<Node, List<(Node, int)>>();
            size = n;
        }
        public Node GetNode(int i){
            return nodes[i];
        }
        public void AddNode(Node n) {
            nodes.Add(n); 
        }
        public void AddNodesInNodeMap(Node n1,Node n2,int val){
            NodeMap[(n1,n2)]=val;
        }
        public void AddNodesInNodeList(Node n1,List<(Node,int)> values){
            NodeList.Add(n1,values);
        }
        public string PrintNodeMap(){
            string message="";
            foreach(var node in NodeMap){
                (Node n1,Node n2) n = node.Key;
                int val = node.Value;
                if (val>6){
                    message+=$"({n.n1.X()},{n.n1.Y()})".PadRight(9);
                    message+=$"({n.n2.X()},{n.n2.Y()})".PadRight(9);
                    message+=$"{val}";
                    message+="\n";
                }
                
            }
            return message;
        }
        public string PrintNodeList(){
            string message="";
            foreach(var nodepair in NodeList){
                Node n = nodepair.Key;
                List<(Node n,int val)> nodeNcosts = nodepair.Value;
                message+=$"({n.X()},{n.Y()})".PadRight(8);
                message+=":";
                foreach(var nNc in nodeNcosts){
                    message+=$"({nNc.n.X()},{nNc.n.Y()}),{nNc.val}|".PadLeft(11);
                }
                message+="\n";
            }
            return message;
        }
        public int GetDistanceBetweenNodes(Node node1,Node node2){
            int distance;
            if (node1.X()==node2.X()){// in the same row
                //find the distance vertically
                distance = Math.Abs(node1.Y()-node2.Y());
            }else if (node1.Y()==node2.Y()){
                distance = Math.Abs(node1.X()-node2.X());
            }else{
                return -1;
            }
            return distance;
        }
}
}
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
