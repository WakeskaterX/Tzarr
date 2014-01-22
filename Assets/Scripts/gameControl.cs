using UnityEngine;
using System.Collections;

public class gameControl : MonoBehaviour {
	
	public int				player_turn		= 1;						//1 = White Player,  2 = Black Player
	
	public GameObject 		game_board;
	public boardControl 	board_script;
	public GameObject[,]	bsq_grid 		= new GameObject[9,9];		//locations of board squares for highlighting
	
	public int[,]			unit_grid		= new int[9,9];				//0 - empty, 1 - center, 2 - normal move, 3 - tzarr move 
	
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

	  ClearUnitGrid();
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
			
			ClearUnitGrid();
		}
		
		//if the player left clicks, and there is nothing selected, send out a ray cast to select the first hit unit, or else the first board square hit
		//and select that square and update the board grids
		if (Input.GetMouseButtonDown(0) && clicked_unit == null){
			SelectUnit();
		}
		else
		if (Input.GetMouseButtonDown(0) && clicked_unit != null){
			MoveUnit();
		}
	}
	
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
				
				//Debug.Log (clicked_unit.name);
				//Debug.Log (clicked_unit.tag);
				UpdateUnitGrid();
			}	
			else if (ray_hit.transform.tag == "BoardSquare"){
			
				if (ray_hit.transform.gameObject.GetComponent<boardSquare>().linked_unit != null){
					clicked_unit = ray_hit.transform.gameObject.GetComponent<boardSquare>().linked_unit;
					clicked_unit.GetComponent<unitControl>().selected = true;
					grid_x = clicked_unit.GetComponent<unitControl>().x_loc;
					grid_y = clicked_unit.GetComponent<unitControl>().y_loc;
					
					UpdateUnitGrid();
				}
			}
		}
		//Debug.Log (clicked_unit.name);	
	}//End of Select Unit
	
	void MoveUnit(){
		RaycastHit ray_hit;
		Ray temp_ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		int layerMask = 1 << 9;
		
		if (Physics.Raycast(temp_ray,out ray_hit,1000,layerMask)){
			if (ray_hit.transform.tag == "BoardSquare"){
				if (ray_hit.transform.gameObject.GetComponent<boardSquare>().square_state == 1){
					MoveToLocation(ray_hit.transform.gameObject.GetComponent<boardSquare>().x_loc,ray_hit.transform.gameObject.GetComponent<boardSquare>().y_loc);
				} else if (ray_hit.transform.gameObject.GetComponent<boardSquare>().square_state == 2){
					AttackLocation(ray_hit.transform.gameObject.GetComponent<boardSquare>().x_loc,ray_hit.transform.gameObject.GetComponent<boardSquare>().y_loc);
				}
			}
		}
		
		//Clear Selection
		if (clicked_unit != null){
			clicked_unit.GetComponent<unitControl>().selected = false;
		}
		clicked_unit 	= null;
		grid_x			= -1;
		grid_y			= -1;
		
		ClearUnitGrid();
	}//End of Move Unit
	
	void MoveToLocation(int xx, int yy){
		clicked_unit.transform.position = bsq_grid[xx,yy].transform.position;
		clicked_unit.GetComponent<unitControl>().x_loc = xx;
		clicked_unit.GetComponent<unitControl>().y_loc = yy;
		bsq_grid[xx,yy].GetComponent<boardSquare>().linked_unit = clicked_unit;
		bsq_grid[grid_x,grid_y].GetComponent<boardSquare>().linked_unit = null;
		
		//changes the board grid so it can be saved later as a string array
		board_script.board_grid[xx,yy] = clicked_unit.GetComponent<unitControl>().unit_name;
		board_script.board_grid[grid_x,grid_y] = "O";
		
		PlayerTurnSwap();
	}
	
	void AttackLocation(int xx, int yy){
		
		Destroy(bsq_grid[xx,yy].GetComponent<boardSquare>().linked_unit);
		clicked_unit.transform.position = bsq_grid[xx,yy].transform.position;
		clicked_unit.GetComponent<unitControl>().x_loc = xx;
		clicked_unit.GetComponent<unitControl>().y_loc = yy;
		bsq_grid[xx,yy].GetComponent<boardSquare>().linked_unit = clicked_unit;
		bsq_grid[grid_x,grid_y].GetComponent<boardSquare>().linked_unit = null;
		
		//changes the board grid so it can be saved for later as a string array
		board_script.board_grid[xx,yy] = clicked_unit.GetComponent<unitControl>().unit_name;
		board_script.board_grid[grid_x,grid_y] = "O";
		
		PlayerTurnSwap();
	}
	
	void UpdateUnitGrid(){
		ClearUnitGrid();
	
		unitControl unit_scr = clicked_unit.GetComponent<unitControl>();
		int x_shift = unit_scr.x_loc - 3;
		int y_shift = unit_scr.y_loc - 3;
		
		Debug.Log (unit_scr.gameObject.name);

		for (int i = 0; i < 7; i ++){
		for (int j = 0; j < 7; j ++){
			
			if ((i+x_shift >= 0) && (j+y_shift >= 0) && (i + x_shift < 9) && (j + y_shift < 9)){
				unit_grid[i+x_shift,j+y_shift] = unit_scr.move_grid[i,j];
			}
		}}
		
		UpdateSquareTex();
	}
	
	void ClearUnitGrid(){
		for (int i = 0; i < 9; i ++){
		for (int j = 0; j < 9; j ++){
			unit_grid[i,j] = 0;
			bsq_grid[i,j].GetComponent<boardSquare>().square_state = 0;
		}}
		
		UpdateSquareTex();
	}
	
	void UpdateSquareTex(){
		for (int i = 0; i < 9; i ++){
		for (int j = 0; j < 9; j ++){
			switch(unit_grid[i,j]){
				case 0:
					bsq_grid[i,j].GetComponent<boardSquare>().square_state = 0;
					break;
				case 1:
					//SELECTION TILE
					if (UnitIsOurs()) {bsq_grid[i,j].GetComponent<boardSquare>().square_state = 3;}
					else {bsq_grid[i,j].GetComponent<boardSquare>().square_state = 6;}
					break;
				case 2:
					//MOVEMENT TILE
					MovementTile(i,j);
					break;
				case 3:
					//TZARR TILE (Same As Movement When Tzarr is 1 away)
					if (TzarrIsNear())
					{
						MovementTile(i,j);
					} else {bsq_grid[i,j].GetComponent<boardSquare>().square_state = 0;}
					break;
				default:
					bsq_grid[i,j].GetComponent<boardSquare>().square_state = 0;
					break;
			}
		}}
	}
	
	void PlayerTurnSwap(){
		if (player_turn == 1) player_turn = 2;
		else if (player_turn == 2) player_turn = 1;
	}
 	
	//Checks if the Unit matches the player turn
	bool UnitIsOurs(){
	
		Debug.Log (clicked_unit.tag);
		if ((player_turn == 1 && clicked_unit.tag == "WhiteUnit") || (player_turn == 2 && clicked_unit.tag == "BlackUnit")) {return true;}
		else {return false;}
	}
	
	bool UnitIsOurTeam(int kk, int ll)
	{
		string plTag = bsq_grid[kk,ll].GetComponent<boardSquare>().linked_unit.tag;
		string turnTag;
		
		if (player_turn == 1) turnTag = "WhiteUnit";
		else turnTag = "BlackUnit";
		
		if (plTag == turnTag) return true;
		else return false;
	}
	
	//Checks if the Tzarr is within 1 space of the unit
	bool TzarrIsNear(){
		for (int i = grid_x - 1; i <= grid_x + 1; i++){
		for (int j = grid_y - 1; j <= grid_y + 1; j++){
			if (i >= 0 && j >= 0 && i < 9 && j < 9)
			{
				if (bsq_grid[i,j].GetComponent<boardSquare>().linked_unit != null){
				if ((player_turn == 1 && bsq_grid[i,j].GetComponent<boardSquare>().linked_unit.GetComponent<unitControl>().unit_name == "T1") || (player_turn == 2 && bsq_grid[i,j].GetComponent<boardSquare>().linked_unit.GetComponent<unitControl>().unit_name == "T2")){
					return true;	
				}}
			}
		
		}}
		
		return false;	
	}
	
	void MovementTile(int k, int l){
	
		if (bsq_grid[k,l].GetComponent<boardSquare>().linked_unit != null){
			if (UnitIsOurs()) {
				if (UnitIsOurTeam(k,l)){
					bsq_grid[k,l].GetComponent<boardSquare>().square_state = 4;
				} else {
					bsq_grid[k,l].GetComponent<boardSquare>().square_state = 2;
				}
			}
			else {
				if (!UnitIsOurTeam(k,l)){
					bsq_grid[k,l].GetComponent<boardSquare>().square_state = 4;
				} else {
					bsq_grid[k,l].GetComponent<boardSquare>().square_state = 5;
				}
		}} else {
			if (UnitIsOurs()) {
				bsq_grid[k,l].GetComponent<boardSquare>().square_state = 1;
			}
			else{
				bsq_grid[k,l].GetComponent<boardSquare>().square_state = 4;
			}
		}
	}
	
	
	
	
	//DEBUG Methods
	void DebugIntArray(int[,] array){
		for (int i = 0; i < 9; i ++){
		for (int j = 0; j < 9; j ++){
			Debug.Log ("X: " + i + ", Y: " + j + ", Value: " + array[i,j]);
		}}
	}
	
	void DebugInt7Array(int[,] array){
		for (int i = 0; i < 7; i ++){
		for (int j = 0; j < 7; j ++){
			Debug.Log ("X: " + i + ", Y: " + j + ", Value: " + array[i,j]);
		}}
	}
	
	void DebugGameObjArray(GameObject[,] array){
		for (int i = 0; i < 9; i ++){
		for (int j = 0; j < 9; j ++){
			Debug.Log ("X: " + i + ", Y: " + j + ", Value: " + array[i,j]);	
		}}
	}
	
}//End of Class
