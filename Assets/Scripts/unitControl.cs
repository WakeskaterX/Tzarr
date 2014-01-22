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
	public bool	can_jump		= false;
	
	//Location of the Unit
	public int		x_loc		= -1;
	public int		y_loc		= -1;
	
								  				
		
	void Start(){
	}
	
	void Update () {
	
	}
	
	public void SetMovementGrid(){
		//Debug.Log (this.gameObject.name);
	
		switch(this.gameObject.name)
		{
			case "P1_Soldier(Clone)":
				unit_name = "S1";
				move_grid = new int[7,7]   {{0,0,0,0,0,0,0},
											{0,0,0,0,0,0,0},
											{0,0,0,0,0,0,0},
											{0,0,0,1,0,0,0},
											{0,0,3,2,3,0,0},
											{0,0,0,0,0,0,0},
											{0,0,0,0,0,0,0}};
				//Debug.Log ("Soldier Set!");
			break;
			case "P1_Lance(Clone)":
				unit_name = "L1";
				move_grid = new int[7,7]   {{0,0,0,0,0,0,0},
											{0,0,0,0,0,0,0},
											{0,0,2,3,2,0,0},
											{0,0,0,1,0,0,0},
											{0,0,0,2,0,0,0},
											{0,0,0,2,0,0,0},
											{0,0,0,3,0,0,0}};
			break;
			case "P1_Jester(Clone)":
				unit_name 	= "J1";
				can_jump	= true;
				move_grid = new int[7,7]   {{3,0,0,0,0,0,3},
											{0,2,0,0,0,2,0},
											{0,0,0,2,0,0,0},
											{0,0,3,1,3,0,0},
											{0,0,0,2,0,0,0},
											{0,2,0,0,0,2,0},
											{3,0,0,0,0,0,3}};
			break;
			case "P1_Phalanx(Clone)":
				unit_name = "P1";
				move_grid = new int[7,7]   {{0,0,0,0,0,0,0},
											{0,0,0,0,0,0,0},
											{0,0,0,2,0,0,0},
											{0,0,3,1,3,0,0},
											{0,0,2,2,2,0,0},
											{0,0,0,0,0,0,0},
											{0,0,0,0,0,0,0}};
			break;
			case "P1_Guard(Clone)":
				unit_name = "G1";	
				move_grid = new int[7,7]   {{0,0,0,0,0,0,0},
											{0,3,0,3,0,3,0},
											{0,0,2,2,2,0,0},
											{0,3,2,1,2,3,0},
											{0,0,0,2,0,0,0},
											{0,0,0,0,0,0,0},
											{0,0,0,0,0,0,0}};
			break;
			case "P1_Tzarr(Clone)":
				unit_name = "T1";
				move_grid = new int[7,7]   {{0,0,0,0,0,0,0},
											{0,0,0,0,0,0,0},
											{0,0,2,2,2,0,0},
											{0,0,2,1,2,0,0},
											{0,0,2,2,2,0,0},
											{0,0,0,0,0,0,0},
											{0,0,0,0,0,0,0}};
			break;
			case "P2_Soldier(Clone)":
				unit_name = "S2";
				move_grid = new int[7,7]   {{0,0,0,0,0,0,0},
											{0,0,0,0,0,0,0},
											{0,0,3,2,3,0,0},
											{0,0,0,1,0,0,0},
											{0,0,0,0,0,0,0},
											{0,0,0,0,0,0,0},
											{0,0,0,0,0,0,0}};
			break;
			case "P2_Lance(Clone)":
				unit_name = "L2";
				move_grid = new int[7,7]   {{0,0,0,3,0,0,0},
											{0,0,0,2,0,0,0},
											{0,0,0,2,0,0,0},
											{0,0,0,1,0,0,0},
											{0,0,2,3,2,0,0},
											{0,0,0,0,0,0,0},
											{0,0,0,0,0,0,0}};
			break;
			case "P2_Jester(Clone)":
				unit_name 	= "J2";
				can_jump 	= true;
				move_grid = new int[7,7]   {{3,0,0,0,0,0,3},
											{0,2,0,0,0,2,0},
											{0,0,0,2,0,0,0},
											{0,0,3,1,3,0,0},
											{0,0,0,2,0,0,0},
											{0,2,0,0,0,2,0},
											{3,0,0,0,0,0,3}};
			break;
			case "P2_Phalanx(Clone)":
				unit_name = "P2";
				move_grid = new int[7,7]   {{0,0,0,0,0,0,0},
											{0,0,0,0,0,0,0},
											{0,0,2,2,2,0,0},
											{0,0,3,1,3,0,0},
											{0,0,0,2,0,0,0},
											{0,0,0,0,0,0,0},
											{0,0,0,0,0,0,0}};
			break;
			case "P2_Guard(Clone)":
				unit_name = "G2";
				move_grid = new int[7,7]   {{0,0,0,0,0,0,0},
											{0,0,0,0,0,0,0},
											{0,0,0,2,0,0,0},
											{0,3,2,1,2,3,0},
											{0,0,2,2,2,0,0},
											{0,3,0,3,0,3,0},
											{0,0,0,0,0,0,0}};
			break;
			case "P2_Tzarr(Clone)":
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
	