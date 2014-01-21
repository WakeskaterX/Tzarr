using UnityEngine;
using System.Collections;

public class boardControl : MonoBehaviour {

	//board grid keeps track of all pieces on the board as a double digit string
	/*	O  - Open			*	P  - Phalanx
	 *  S  - Soldier		*	G  - Guard
	 *  L  - Lance			*	T  - Tzarr/King
	 *  J  - Jester			*   1/2  - Player 1/2  (White/Black)
	 */
	public string[,] board_grid = new string[9,9]  {{"L1",	"J1",	"P1",	"G1",	"T1",	"G1",	"P1",	"J1",	"L1"},
													{"S1",	"S1",	"S1",	"S1",	"S1",	"S1",	"S1",	"S1",	"S1"},
													{"O",	"O",	"O",	"O",	"O",	"O",	"O",	"O",	"O"},
													{"O",	"O",	"O",	"O",	"O",	"O",	"O",	"O",	"O"},
													{"O",	"O",	"O",	"O",	"O",	"O",	"O",	"O",	"O"},
													{"O",	"O",	"O",	"O",	"O",	"O",	"O",	"O",	"O"},
													{"O",	"O",	"O",	"O",	"O",	"O",	"O",	"O",	"O"},
													{"S2",	"S2",	"S2",	"S2",	"S2",	"S2",	"S2",	"S2",	"S2"},
													{"L2",	"J2",	"P2",	"G2",	"T2",	"G2",	"P2",	"J2",	"L2"}};
													
	public GameObject[,] board_sqs 	= new GameObject[9,9];
	public GameObject bsq;
													
	public float 			socket_width	= 3.333f;
	public GameObject[]		pieces			= new GameObject[12];   //0 - S1, 1- L1, 2- J1, 3 - P1, 4 - G1, 5 - T1, 6 - S2, 7 - L2, 8 - J2, 9 - P2, 10 - G2, 11 - T2
			
	private Vector3[,]		sockets			= new Vector3[9,9];

													
	//Use Awake to create the sockets, board squares and etc so they can be called in other Start functions										
	void Awake () {
		for (int i = 0; i < 9; i++){
		for (int j = 0; j < 9; j++){
			
			sockets[i,j] = new Vector3(13.333f - j*socket_width,-.23f,13.333f - i*socket_width);
			board_sqs[i,j] = Instantiate(bsq, sockets[i,j],Quaternion.identity) as GameObject;
			board_sqs[i,j].GetComponent<boardSquare>().square_state = 0;
			board_sqs[i,j].GetComponent<boardSquare>().game_board = this.gameObject;
			board_sqs[i,j].GetComponent<boardSquare>().x_loc = i;
			board_sqs[i,j].GetComponent<boardSquare>().y_loc = j; 
		}}
		
		GameSetup();
	}

	//Start is unused atm
	void Start(){}

	void Update () {
	
	}
	
	void GameSetup()
	{
		for (int i = 0; i < 9; i++){
		for (int j = 0; j < 9; j++){
		
			GameObject temp_obj = null;
					
			switch(board_grid[i,j])
			{
				case "O":
							//Do nothing this is an Open Block
					break;
						
				case "S1":
					temp_obj = GameObject.Instantiate (pieces[0],sockets[i,j],Quaternion.Euler(-90,-90,0)) as GameObject;
					break;
				
				case "L1":
					temp_obj = GameObject.Instantiate (pieces[1],sockets[i,j],Quaternion.Euler(-90,-90,0)) as GameObject;
					break;
				
				case "J1":
					temp_obj = GameObject.Instantiate (pieces[2],sockets[i,j],Quaternion.Euler(-90,-90,0)) as GameObject;
					break;
						
				case "P1":
					temp_obj = GameObject.Instantiate (pieces[3],sockets[i,j],Quaternion.Euler(-90,-90,0)) as GameObject;
					break;
						
				case "G1":
					temp_obj = GameObject.Instantiate (pieces[4],sockets[i,j],Quaternion.Euler(-90,-90,0)) as GameObject;
					break;
						
				case "T1":
					temp_obj = GameObject.Instantiate (pieces[5],sockets[i,j],Quaternion.Euler(-90,-90,0)) as GameObject;
					break;
						
				case "S2":
					temp_obj = GameObject.Instantiate (pieces[6],sockets[i,j],Quaternion.Euler(-90,90,0)) as GameObject;
					break;
						
				case "L2":
					temp_obj = GameObject.Instantiate (pieces[7],sockets[i,j],Quaternion.Euler(-90,90,0)) as GameObject;
					break;
						
				case "J2":
					temp_obj = GameObject.Instantiate (pieces[8],sockets[i,j],Quaternion.Euler(-90,90,0)) as GameObject;
					break;
					
				case "P2":
					temp_obj = GameObject.Instantiate (pieces[9],sockets[i,j],Quaternion.Euler(-90,90,0)) as GameObject;
					break;
						
				case "G2":
					temp_obj = GameObject.Instantiate (pieces[10],sockets[i,j],Quaternion.Euler(-90,90,0)) as GameObject;
					break;
						
				case "T2":
					temp_obj = GameObject.Instantiate (pieces[11],sockets[i,j],Quaternion.Euler(-90,90,0)) as GameObject;
					break;
			}
					
			if (temp_obj != null){
				temp_obj.GetComponent<unitControl>().x_loc = i;
				temp_obj.GetComponent<unitControl>().y_loc = j;
				
				temp_obj.GetComponent<unitControl>().SetMovementGrid();
			}
			
			//Stores the unit in the board sqs to keep track of what items are where
			board_sqs[i,j].GetComponent<boardSquare>().linked_unit = temp_obj;
			
		}}//end of For Loops
	
	}
}
