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
													
	public float 			socket_width	= 3.333f;
	public GameObject[]		pieces			= new GameObject[12];   //0 - S1, 1- L1, 2- J1, 3 - P1, 4 - G1, 5 - T1, 6 - S2, 7 - L2, 8 - J2, 9 - P2, 10 - G2, 11 - T2
			
	private Vector3[,]		sockets			= new Vector3[9,9];

													
												
	void Start () {
		for (int i = 0; i < 9; i++)
		{
			for (int j = 0; j < 9; j++)
			{
				sockets[i,j] = new Vector3(13.333f - j*socket_width,0,13.333f - i*socket_width);
			}
		}
		
		GameSetup();
		
	}

	void Update () {
	
	}
	
	void GameSetup()
	{
		for (int i = 0; i < 9; i ++)
		{
			for (int j = 0; j < 9; j++)
			{
				switch(board_grid[i,j])
				{
					case "O":
						//Do nothing this is an Open Block
					break;
					
					case "S1":
						GameObject.Instantiate (pieces[0],sockets[i,j],Quaternion.Euler(-90,-90,0));
					break;
					
					case "L1":
						GameObject.Instantiate (pieces[1],sockets[i,j],Quaternion.Euler(-90,-90,0));
					break;
					
					case "J1":
						GameObject.Instantiate (pieces[2],sockets[i,j],Quaternion.Euler(-90,-90,0));
					break;
					
					case "P1":
					    GameObject.Instantiate (pieces[3],sockets[i,j],Quaternion.Euler(-90,-90,0));
					break;
					
					case "G1":
						GameObject.Instantiate (pieces[4],sockets[i,j],Quaternion.Euler(-90,-90,0));
					break;
					
					case "T1":
						GameObject.Instantiate (pieces[5],sockets[i,j],Quaternion.Euler(-90,-90,0));
					break;
					
					case "S2":
						GameObject.Instantiate (pieces[6],sockets[i,j],Quaternion.Euler(-90,90,0));
					break;
					
					case "L2":
						GameObject.Instantiate (pieces[7],sockets[i,j],Quaternion.Euler(-90,90,0));
					break;
					
					case "J2":
						GameObject.Instantiate (pieces[8],sockets[i,j],Quaternion.Euler(-90,90,0));
					break;
						
					case "P2":
						GameObject.Instantiate (pieces[9],sockets[i,j],Quaternion.Euler(-90,90,0));
					break;
					
					case "G2":
						GameObject.Instantiate (pieces[10],sockets[i,j],Quaternion.Euler(-90,90,0));
					break;
					
					case "T2":
						GameObject.Instantiate (pieces[11],sockets[i,j],Quaternion.Euler(-90,90,0));
					break;
				}
			}
		}
	
	}
}
