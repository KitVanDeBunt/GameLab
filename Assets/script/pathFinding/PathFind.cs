using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Node :VecInt
{
	public float G; // movement from start
	public float H; // estimated Cost til end
	public float F; // weight
	public Node parentNode;
	public Node(float g, float h,float f, Node parent, int _x,int _y) :base(_x,_y)
	{
		G = g;
		H = h;
		F = f;
		parentNode = parent;
	}
}

public class NewNode :VecInt
{
	public float K; // estimated Cost til end
	public NewNode(float k, int _x,int _y) :base(_x,_y)
	{
		K = k;
	}
}

public class PathFind : MonoBehaviour {
	private static int i;
	private static int j;
	private static List<Node> open;
	private static List<Node> closed;
	
	private static VecInt[] drawPath;
	public static void Update(){
		DrawLast();
	}
	
	private static void DrawLast(){
		
		if(drawPath!=null){
			//print ("L "+drawPath.Length);
			for (int i = 0; i <drawPath.Length-1; i++){
				Debug.DrawLine(IsoMath.tileToWorld(drawPath[i].x,drawPath[i].y)
				               ,IsoMath.tileToWorld(drawPath[i+1].x,drawPath[i+1].y));
			}
		}
	}
	
	private static float EstimateDistance(VecInt a,VecInt b){
		float deltaX = Mathf.Abs(a.x - b.x);	
		float deltaY = Mathf.Abs(a.y - b.y);	
		float deltaXY = Mathf.Abs (deltaX - deltaY);
		deltaX -= deltaXY;
		deltaY -= deltaXY;
		if(deltaXY!=0){
			deltaXY = Mathf.Sqrt((deltaXY*deltaXY)+(deltaXY*deltaXY));
		}
		return (deltaX + deltaY + deltaXY);
	}
	
	private static NewNode[] SurroundingArea(VecInt a,int width,int height,bool[,] colisionArray){
		int i;
		int j;
		List<NewNode> suroundingArea = new List<NewNode>();


		//add if on map
		if((a.x < width-1)&&(a.y < height-1)){
			if(!(colisionArray[a.x+1,a.y])&&
			   !(colisionArray[a.x,a.y+1])){ 
				suroundingArea.Add(new NewNode(1.4142f,a.x+1,a.y+1));//right down
			}
		}
		if((a.y < height-1)&&(a.x > 0)){
			if(!(colisionArray[a.x-1,a.y])&&
			   !(colisionArray[a.x,a.y+1])){ 
				suroundingArea.Add(new NewNode(1.4142f,a.x-1,a.y+1));//down left
			}
		}
		if((a.x > 0)&&(a.y > 0)){
			if(!(colisionArray[a.x-1,a.y])&&
			   !(colisionArray[a.x,a.y-1])){ 
				suroundingArea.Add(new NewNode(1.4142f,a.x-1,a.y-1));//left up
			}
		}
		if((a.y > 0)&&(a.x < width-1)){
			if(!(colisionArray[a.x+1,a.y])&&
			   !(colisionArray[a.x,a.y-1])){ 
				suroundingArea.Add(new NewNode(1.4142f,a.x+1,a.y-1));//up right
			}
		}

		//add if on map
		if(a.x < width-1){
			suroundingArea.Add(new NewNode(1,a.x+1,a.y));//right
		}
		if(a.y < height-1){
			suroundingArea.Add(new NewNode(1,a.x,a.y+1));//down
		}
		if(a.x > 0){
			suroundingArea.Add(new NewNode(1,a.x-1,a.y));//left
		}
		if(a.y > 0){
			suroundingArea.Add(new NewNode(1,a.x,a.y-1));//up
		}

		//remove if in collision
		for(i = suroundingArea.Count-1; i > -1; i--){
			if(colisionArray[suroundingArea[i].x,suroundingArea[i].y]){ 
				suroundingArea.RemoveAt(i);
			}
		}

		//remove if already in open
		/*for(i = suroundingArea.Count-1; i > -1; i--){
			for(j = 0; j < open.Count; j++){
				if((suroundingArea[i].x == open[j].x )&&(suroundingArea[i].y == open[j].y )){ 
					suroundingArea.RemoveAt(i);
					break;
				}
			}
		}*/
		//remove if already in closed
		for(i = suroundingArea.Count-1; i > -1; i--){
			for(j = 0; j < closed.Count; j++){
				if((suroundingArea[i].x == closed[j].x )&&(suroundingArea[i].y == closed[j].y )){ 
					suroundingArea.RemoveAt(i);
					break;
				}
			}
		}
		return suroundingArea.ToArray();
	}
	
	private class SortFDescending : IComparer<Node>
	{
		int IComparer<Node>.Compare(Node a, Node b) //implement Compare
		{              
			if (a.F > b.F)
				return -1; //normally greater than = 1
			if (a.F < b.F)
				return 1; // normally smaller than = -1
			else
				return 0; // equal
		}
	}
	
	
	public static VecInt[] FindPath (VecInt A,VecInt B, bool[,] collisionArray){
		int i;
		int j;
		List<VecInt> returnPath = new List<VecInt>();
		open = new List<Node>();
		closed = new List<Node>();
		int width = collisionArray.GetLength(0);
		int height = collisionArray.GetLength(1);
		//returnPath.Add(A);
		print ("[PathFind] new path!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!\n");
		
		//add start node
		Node startN = new Node(0,99999999,99999999,null,A.x,A.y);
		open.Add(startN);
		
		bool pathOpen = true;
		bool endFound = false;
		int whileLooped = 0;
		while (pathOpen) {
			whileLooped++;
			//sort
			open.Sort(new SortFDescending());
			//for(i = open.Count-1; i > -1; i--){
			
			
			Node currentCheckNode = open[open.Count-1];
			//print ("[PathFind] open count"+open.Count);
			NewNode[] newOpenList = SurroundingArea(currentCheckNode,width,height,collisionArray);
			//print ("[PathFind] new open count"+newOpenList.Length);
			for(j = 0; j <newOpenList.Length;j++){
				//draw
				Debug.DrawLine(IsoMath.tileToWorld(currentCheckNode.x,currentCheckNode.y)
				               ,IsoMath.tileToWorld(newOpenList[j].x,newOpenList[j].y),Color.magenta,3.0f);
				               
				bool inOPenList = false;
				float newG = currentCheckNode.G+newOpenList[j].K;
				float newH = EstimateDistance(newOpenList[j],B); 
				float newF = newG+newH;
				//check if new open node is already in open list
				for(i = 0; i < open.Count; i++){
					if((newOpenList[j].x == open[i].x )&&(newOpenList[j].y == open[i].y )){
						inOPenList =true;
						if(currentCheckNode.parentNode.F > open[i].parentNode.F ){
							open[i].G = newG;
							open[i].H = newH;
							open[i].F = newF;
							open[i].parentNode = currentCheckNode;
						}
						break;
					}
				}
				if(!inOPenList){
					//print (open.Count+" - "+i);
					Node newN = new Node(newG,newH,newF,currentCheckNode,newOpenList[j].x,newOpenList[j].y);
					//end found
					if(newN.x==B.x&&newN.y==B.y){
						print ("[PathFind] end Foound!!!!!!!!!!!!!!!!!!!!\n");
						endFound = true;
						closed.Add(currentCheckNode);
						open.Remove(currentCheckNode);
						closed.Add(newN);
						break;
					}
					open.Add(newN);
				}
				
			}
			if(endFound){
				break;
			}
			closed.Add(currentCheckNode);
			open.Remove(currentCheckNode);
			
			
			//}
			if(endFound){
				break;
			}
			if(open.Count==0){
				print ("[PathFind] zero open!!!!!!!!!!!!!!!!!!!!\n");
				pathOpen = false;
			}
		}
		print ("[PathFind] whileLooped: "+whileLooped+"\n");
		print ("[PathFind] Start: "+A.print+"\n");
		print ("[PathFind] Start: "+A.print+"\n");
		print ("[PathFind] Start Surounding: "+SurroundingArea(A,width,height,collisionArray).Length+"\n");
		print ("[PathFind] Destination: "+B.print+"\n");
		print ("[PathFind] Dist: "+EstimateDistance(A,B)+"\n");
		//returnPath.Add(B);
		List<Node> tempReturnPath = new List<Node>();
		tempReturnPath.Add(closed[closed.Count-1]);
		Node privious = tempReturnPath[tempReturnPath.Count-1].parentNode;
		tempReturnPath.Add(privious);
		while(true){
			privious = tempReturnPath[tempReturnPath.Count-1].parentNode;
			if(privious!=null){
				tempReturnPath.Add(privious);
			}else{
				break;
			}
		}
		for(i = tempReturnPath.Count-1;i > -1;i--){
			returnPath.Add (new VecInt(tempReturnPath[i].x,tempReturnPath[i].y));
		}
		
		if (endFound) {
			drawPath = returnPath.ToArray();
			return returnPath.ToArray();
		} else {
			return null;
		}
	}
	

}
