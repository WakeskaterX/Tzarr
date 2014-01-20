using UnityEngine;
using System.Collections;

public class unitControl : MonoBehaviour {

	//Moveset is a 2d array containing possible moves from the center for this piece.  1 = Middle Point, 2 = Normal Move locations, 3 = Tzarr Move Locations
	public int[,] move_grid = new int[7,7] {{0,0,0,0,0,0,0},
							  				{0,0,0,0,0,0,0},
							  				{0,0,0,0,0,0,0},
							  				{0,0,0,1,0,0,0},
							  				{0,0,0,0,0,0,0},
							  				{0,0,0,0,0,0,0},
							  				{0,0,0,0,0,0,0}};
	public string unit_name		= "O";												//Unit Name is used in the board information
	public bool selected 		= false;
	
	//Location of the Unit
	public int		x_loc		= -1;
	public int		y_loc		= -1;
	
								  				
		
	//Set the grids in Awake so they can be used in Start						  				
	void Awake () {
		SetMovementGrid();
	}
	
	void Start(){
	
	}
	
	void Update () {
	
	}
	
	void SetMovementGrid(){
		switch(name)
		{
			case "P1_Soldier":
				unit_name = "S1";
				move_grid = new int[7,7]   {{0,0,0,0,0,0,0},
											{0,0,0,0,0,0,0},
											{0,0,0,0,0,0,0},
											{0,0,0,1,0,0,0},
											{0,0,3,2,3,0,0},
											{0,0,0,0,0,0,0},
											{0,0,0,0,0,0,0}};
			break;
			case "P1_Lance":
				unit_name = "L1";
				move_grid = new int[7,7]   {{0,0,0,0,0,0,0},
											{0,0,0,0,0,0,0},
											{0,0,2,3,2,0,0},
											{0,0,0,1,0,0,0},
											{0,0,0,2,0,0,0},
											{0,0,0,2,0,0,0},
											{0,0,0,3,0,0,0}};
			break;
			case "P1_Jester":
				unit_name = "J1";
				move_grid = new int[7,7]   {{3,0,0,0,0,0,3},
											{0,2,0,0,0,2,0},
											{0,0,0,2,0,0,0},
											{0,0,3,1,3,0,0},
											{0,0,0,2,0,0,0},
											{0,2,0,0,0,2,0},
											{3,0,0,0,0,0,3}};
			break;
			case "P1_Phalanx":
				unit_name = "P1";
				move_grid = new int[7,7]   {{0,0,0,0,0,0,0},
											{0,0,0,0,0,0,0},
											{0,0,0,2,0,0,0},
											{0,0,3,1,3,0,0},
											{0,0,2,2,2,0,0},
											{0,0,0,0,0,0,0},
											{0,0,0,0,0,0,0}};
			break;
			case "P1_Guard":
				unit_name = "G1";	
				move_grid = new int[7,7]   {{0,0,0,0,0,0,0},
											{0,3,0,3,0,3,0},
											{0,0,2,2,2,0,0},
											{0,3,2,1,2,3,0},
											{0,0,0,2,0,0,0},
											{0,0,0,0,0,0,0},
											{0,0,0,0,0,0,0}};
			break;
			case "P1_Tzarr":
				unit_name = "T1";
				move_grid = new int[7,7]   {{0,0,0,0,0,0,0},
											{0,0,0,0,0,0,0},
											{0,0,2,2,2,0,0},
											{0,0,2,1,2,0,0},
											{0,0,2,2,2,0,0},
											{0,0,0,0,0,0,0},
											{0,0,0,0,0,0,0}};
			break;
			case "P2_Soldier":
				unit_name = "S2";
				move_grid = new int[7,7]   {{0,0,0,0,0,0,0},
											{0,0,0,0,0,0,0},
											{0,0,3,2,3,0,0},
											{0,0,0,1,0,0,0},
											{0,0,0,0,0,0,0},
											{0,0,0,0,0,0,0},
											{0,0,0,0,0,0,0}};
			break;
			case "P2_Lance":
				unit_name = "L2";
				move_grid = new int[7,7]   {{0,0,0,3,0,0,0},
											{0,0,0,2,0,0,0},
											{0,0,0,2,0,0,0},
											{0,0,0,1,0,0,0},
											{0,0,2,3,2,0,0},
											{0,0,0,0,0,0,0},
											{0,0,0,0,0,0,0}};
			break;
			case "P2_Jester":
				unit_name = "J2";
				move_grid = new int[7,7]   {{3,0,0,0,0,0,3},
											{0,2,0,0,0,2,0},
											{0,0,0,2,0,0,0},
											{0,0,3,1,3,0,0},
											{0,0,0,2,0,0,0},
											{0,2,0,0,0,2,0},
											{3,0,0,0,0,0,3}};
			break;
			case "P2_Phalanx":
				unit_name = "P2";
				move_grid = new int[7,7]   {{0,0,0,0,0,0,0},
											{0,0,0,0,0,0,0},
											{0,0,2,2,2,0,0},
											{0,0,3,1,3,0,0},
											{0,0,0,2,0,0,0},
											{0,0,0,0,0,0,0},
											{0,0,0,0,0,0,0}};
			break;
			case "P2_Guard":
				unit_name = "G2";
				move_grid = new int[7,7]   {{0,0,0,0,0,0,0},
											{0,0,0,0,0,0,0},
											{0,0,0,2,0,0,0},
											{0,3,2,1,2,3,0},
											{0,0,2,2,2,0,0},
											{0,3,0,3,0,3,0},
											{0,0,0,0,0,0,0}};
			break;
			case "P2_Tzarr":
				unit_name = "T2";
				move_grid = new int[7,7]   {{0,0,0,0,0,0,0},
											{0,0,0,0,0,0,0},
											{0,0,2,2,2,0,0},
											{0,0,2,1,2,0,0},
											{0,0,2,2,2,0,0},
											{0,0,0,0,0,0,0},
											{0,0,0,0,0,0,0}};
			break;
		}
	}
}
	