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
	
	private static int EstimateDistance(VecInt a,VecInt b){
		int deltaX = Mathf.Abs(a.x - b.x);	
		int deltaY = Mathf.Abs(a.y - b.y);	
		return (deltaX + deltaY);
	}
	
	private static VecInt[] SurroundingArea(VecInt a,int width,int height,bool[,] colisionArray){
		int i;
		int j;
		List<VecInt> suroundingArea = new List<VecInt>();
		//add if on map
		if(a.x < width-1){
			suroundingArea.Add(new VecInt(a.x+1,a.y));
		}
		if(a.y < height-1){
			suroundingArea.Add(new VecInt(a.x,a.y+1));
		}
		if(a.x > 0){
			suroundingArea.Add(new VecInt(a.x-1,a.y));
		}
		if(a.y > 0){
			suroundingArea.Add(new VecInt(a.x,a.y-1));
		}
		//remove if in collision
		for(i = suroundingArea.Count-1; i > -1; i--){
			if(colisionArray[suroundingArea[i].x,suroundingArea[i].y] == true){ 
				suroundingArea.RemoveAt(i);
			}
		}
		//remove if already in open or closed
		for(i = suroundingArea.Count-1; i > -1; i--){
			for(j = 0; j < open.Count; j++){
				if((suroundingArea[i].x == open[j].x )&&(suroundingArea[i].y == open[j].y )){ 
					suroundingArea.RemoveAt(i);
					break;
				}
			}
		}
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
	
	private class SortIntDescending : IComparer<Node>
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
			open.Sort(new SortIntDescending());
			for(i = open.Count-1; i > -1; i--){
				Node currentN = open[i];
				//print ("[PathFind] open count"+open.Count);
				VecInt[] newOpenList = SurroundingArea(open[i],width,height,collisionArray);
				//print ("[PathFind] new open count"+newOpenList.Length);
				for(j = 0; j <newOpenList.Length;j++){
					//print (open.Count+" - "+i);
					float newG = open[i].G+1;
					float newH = EstimateDistance(newOpenList[j],B); 
					Node newN = new Node(newG,newH,newG+newH,currentN,newOpenList[j].x,newOpenList[j].y);
					//end found
					if(newN.x==B.x&&newN.y==B.y){
						print ("[PathFind] end Foound!!!!!!!!!!!!!!!!!!!!\n");
						endFound = true;
						closed.Add(currentN);
						open.Remove(currentN);
						closed.Add(newN);
						break;
					}
					open.Add(newN);
				}
				if(endFound){
					break;
				}
				closed.Add(currentN);
				open.Remove(currentN);
			}
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
