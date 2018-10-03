using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StateManager : object {

	public static float levelStartTimer;

	[Range(1,5)]
	public static int curState = 1;
	/*
	 * 1. Main Menu
	 * 2. Transition
	 * 3. Gameplay
	 * 4. End Game
	 * 5. Pause Menu
	*/

	public enum gameStates
	{
		main = 1,
		transition = 2,
		gameplay = 3,
		endgame = 4,
		pause = 5
	}


}
