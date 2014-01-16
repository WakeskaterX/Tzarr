using UnityEngine;
using System.Collections;

public class playerController : MonoBehaviour {

	//Moveset is a 2d array containing possible moves from the center for this piece.  1 = Middle Point, 2 = Normal Move locations, 3 = Tzarr Move Locations
	public int[,] move_grid = new int[7,7] {{0,0,0,0,0,0,0},
							  				{0,0,0,0,0,0,0},
							  				{0,0,0,0,0,0,0},
							  				{0,0,0,1,0,0,0},
							  				{0,0,0,0,0,0,0},
							  				{0,0,0,0,0,0,0},
							  				{0,0,0,0,0,0,0}};
	
	private bool selected = false;
							  				
		
							  				
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
