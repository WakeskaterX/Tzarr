using UnityEngine;
using System.Collections;

public class gameControl : MonoBehaviour {
	
	public int				player_turn		= 1;						//1 = White Player,  2 = Black Player
	
	public GameObject 		game_board;
	public boardControl 	board_script;
	public GameObject[,]	bsq_grid 		= new GameObject[9,9];		//locations of board squares for highlighting
	
	public int[,]			unit_grid		= new int[9,9];				//0 - cannot move, 1 - center, 2 - normal move, 3 - tzarr move 
	public int[,]			board_grid		= new int[9,9];				//0 - open, 1 - P1 Unit, 2 - P2 Unit
	
	public GameObject  		clicked_unit;
	
	//Currently Selected Grid Location (-1 for nothing)
	private int				grid_x			= -1;
	private int				grid_y			= -1; 

	void Start () {
	  //Set up game_board and board_script
	  if (game_board == null)
	  {
	  	game_board = GameObject.Find ("Board");
	  }
	  board_script = game_board.GetComponent<boardControl>();
	  
	  //Link to the board squares game object arrays
	  bsq_grid = board_script.board_sqs;
	  
	  //Set the board grid integers to locate where friendly and enemy players are
	  BoardGridSet();
	  
	}

	void Update () {
		//if we have a unit selected and the player right clicks, get rid of the selection
		if (Input.GetMouseButtonDown(1)){
			if (clicked_unit != null){
				clicked_unit.GetComponent<unitControl>().selected = false;
			}
			clicked_unit 	= null;
			grid_x			= -1;
			grid_y			= -1;
		}
		
		//if the player left clicks, and there is nothing selected, send out a ray cast to select the first hit unit, or else the first board square hit
		//and select that square and update the board grids
		if (Input.GetMouseButtonDown(0) && clicked_unit == null){
			SelectUnit();
		}
	}
	
	void BoardGridSet(){
		//get info from the String Array telling which units are where and turn that into integer data for friendly and enemy locations
		for (int i = 0; i < 9; i++){
		for (int j = 0; j < 9; j++){
			
			switch(board_script.board_grid[i,j])
			{
				case "O":
					board_grid[i,j] = 0;
				break;
				
				case "S1":
				case "L1":
				case "J1":
				case "P1":
				case "G1":
				case "T1":
					board_grid[i,j] = 1;
				break;
				
				case "S2":
				case "L2":
				case "J2":
				case "P2":
				case "G2":
				case "T2":
					board_grid[i,j] = 2;
				break;
			}
		}}
	}//end of BoardGridSet
	
	void SelectUnit(){
		//send out a raycast and check for colliders with tag WhitePlayer or BlackPlayer
		RaycastHit ray_hit;
		Ray temp_ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		
		if (Physics.Raycast(temp_ray,out ray_hit,1000)){
			if (ray_hit.transform.tag == "WhiteUnit" || ray_hit.transform.tag == "BlackUnit"){
				clicked_unit = ray_hit.collider.gameObject;
				clicked_unit.GetComponent<unitControl>().selected = true;
				grid_x = clicked_unit.GetComponent<unitControl>().x_loc;
				grid_y = clicked_unit.GetComponent<unitControl>().y_loc;
			}	
			else if (ray_hit.transform.tag == "BoardSquare"){
			
				if (ray_hit.transform.gameObject.GetComponent<boardSquare>().linked_unit != null){
					clicked_unit = ray_hit.transform.gameObject.GetComponent<boardSquare>().linked_unit;
					clicked_unit.GetComponent<unitControl>().selected = true;
					grid_x = clicked_unit.GetComponent<unitControl>().x_loc;
					grid_y = clicked_unit.GetComponent<unitControl>().y_loc;
				}
			}
		}
		
		UpdateUnitGrid();
	}
	
	void UpdateUnitGrid(){
		
	}
	
	
}//End of Class
