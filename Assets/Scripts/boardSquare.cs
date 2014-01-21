using UnityEngine;
using System.Collections;

public class boardSquare : MonoBehaviour {

	   /*				About:
		*	Board Squares are clickable squares that are normally invisible on the board.  Players can click them instead of a unit to select the unit on that square.
		*   When the unit on that square is selected board squares light up blue for places that the player can move that piece to and red for pieces the player can attack.
		*   Players then select a board square to move the piece to that square.
		*	Board Squares also store the data of the piece above them in them
	 	*/
	 	
	
	public GameObject 		game_board;						//this is the main game board / drop in inspector to link scripts / set to the board when creating squares];
	
	public Material[]		board_mat = new Material[6];
	
	public GameObject		linked_unit = null;
	
	private boardControl 	board_script;					//this is the script for the game board, set in START
	public int				square_state	= 0;			//square state:  0 - invisible, not selected,  1 - light up teal for movement,  2 - light up red for attack an enemy unit, 3 - Selected Tile
															//				4 - enemy move / not possible but in movement squares, 5 - enemy attacks  6 - enemy select
	//These are used to store the data of the location of the square
	public int				x_loc			= -1;
	public int				y_loc			= -1;
	
	void Start () {	
		board_script = game_board.GetComponent <boardControl>();
	}

	void Update () {
		if (square_state == 0){
			renderer.enabled = false;
			
		}
		else{
			renderer.enabled = true;
			renderer.material = board_mat[square_state - 1];
		}
	 }//end of Update
	
	
}
