using UnityEngine;
using System.Collections.Generic;
using System.Collections;

using System;

public class ChainJam : MonoBehaviour {
	
	/*
		These are the things you need to implement:
		
		GAME:
		
		void GameEnd():
			end the game before time. If this is never called, your game will automatically finish after 1 minute.
		
		void ChainJam.AddFunctionBeforeExit(Action function, int s)
			will execute the “function”, “s” seconds before the 1 minute timeframe is over.
			Use this if you want to end your game 'nicely', with a fade or an animation.
			Example: 
				if you have a void Fadeout() function:
				ChainJam.AddFunctionBeforeExit(Fadeout,5);  // this will execute fadeout 5 seconds before the game ends.
			If your function has parameters, use () => function
			Example:
				if you have a void Fadeout(int length) function:
				ChainJam.AddFunctionBeforeExit(() => Fadeout(5),5);  
							
		ENUMS:
		
		enum ChainJam.PLAYER: 
			you will need this enum to get controller inputs and add points. 
			
		enum ChainJam.BUTTON: 
			you will need this enum to specify a button.
			
			
		SCORE:	
			
		void ChainJam.AddPoints(ChainJam.PLAYER player, int p): 
			use this to add “p” points to “player”.
			
		int ChainJam.GetTotalPoints():
			returns the total of points distributed during this (will never be more than 10).
			
		int ChainJam.GetPoints(PLAYER player):
			returns the points of "player" distributed during this game.
			
			
		CONTROLLERS: 	
			
		bool ChainJam.GetButtonPressed(ChainJam.PLAYER player, ChainJam.BUTTON button): 
			will return true or false depending on whether the “button” is pressed for “player”.
			
		bool ChainJam.GetButtonJustPressed(ChainJam.PLAYER player, ChainJam.BUTTON button): 
			will return true only in the frame where the “button”is pressed for “player”. 
			
		bool ChainJam.GetButtonJustReleased(ChainJam.PLAYER player, ChainJam.BUTTON button): 
			will return true only in the frame where the “button”is released for “player”. 
			
	*/
	
	
	// Controller enums
	public enum PLAYER {PLAYER1, PLAYER2, PLAYER3, PLAYER4};
	public enum BUTTON {LEFT,RIGHT,UP,DOWN,A,B};
	
	// Private vars
	private static int _player1Points = 0;
	private static int _player2Points = 0;
	private static int _player3Points = 0;
	private static int _player4Points = 0;
	private static float _timePassed = 0;
	private static float _timePassedLast = -1;

	private Dictionary<int,List<Action>> _actions;
	
	
	void Awake () {
		DontDestroyOnLoad(this.gameObject);
		_actions = new Dictionary<int, List<Action>>();
		GameStart();
	}
	
	public void AddFunctionBeforeExit(Action function, int s)
	{
		List<Action> list = new List<Action>();
		list.Add(function);
		
		if(!_actions.ContainsKey(s))
		{
			_actions.Add(s,list);
		}
		else
		{
			_actions[s].AddRange(list);
		}
	}
		
	void Update() {
		_timePassed += Time.deltaTime;
		
		int index = (int)Mathf.Round(60-_timePassed);
		int lastIindex = (int)Mathf.Round(60-_timePassedLast);
		if(index != lastIindex)
		{
			_timePassedLast = _timePassed;
			if (_actions.ContainsKey(index))
			{
				foreach (Action function in _actions[index]) {
					function();
				}
			}
		}
	}

	public static bool GetButtonPressed(PLAYER player, BUTTON button)
	{
		return Input.GetKey(GetKeycode(player, button));
	}
	
	public static bool GetButtonJustPressed(PLAYER player, BUTTON button)
	{
		return Input.GetKeyDown(GetKeycode(player, button));
	}
	
	public static bool GetButtonJustReleased(PLAYER player, BUTTON button)
	{
		return Input.GetKeyUp(GetKeycode(player, button));
	}	
	
	private static KeyCode GetKeycode(PLAYER player, BUTTON button)
	{
		KeyCode key = KeyCode.Space;
		switch(player)
		{
		case PLAYER.PLAYER1:
			switch(button)
			{
			case BUTTON.A:
				key = KeyCode.Z;
				break;
			case BUTTON.B:
				key = KeyCode.X;
				break;
			case BUTTON.LEFT:
				key = KeyCode.LeftArrow;
				break;
			case BUTTON.RIGHT:
				key = KeyCode.RightArrow;
				break;
			case BUTTON.UP:
				key = KeyCode.UpArrow;
				break;
			case BUTTON.DOWN:
				key = KeyCode.DownArrow;
				break;
			}	
			break;
		case PLAYER.PLAYER2:
			switch(button)
			{
			case BUTTON.A:
				key = KeyCode.N;
				break;
			case BUTTON.B:
				key = KeyCode.M;
				break;
			case BUTTON.LEFT:
				key = KeyCode.J;
				break;
			case BUTTON.RIGHT:
				key = KeyCode.L;
				break;
			case BUTTON.UP:
				key = KeyCode.I;
				break;
			case BUTTON.DOWN:
				key = KeyCode.K;
				break;
			}	
			break;
		case PLAYER.PLAYER3:
			switch(button)
			{
			case BUTTON.A:
				key = KeyCode.Q;
				break;
			case BUTTON.B:
				key = KeyCode.E;
				break;
			case BUTTON.LEFT:
				key = KeyCode.A;
				break;
			case BUTTON.RIGHT:
				key = KeyCode.D;
				break;
			case BUTTON.UP:
				key = KeyCode.W;
				break;
			case BUTTON.DOWN:
				key = KeyCode.S;
				break;
			}	
			break;
		case PLAYER.PLAYER4:
			switch(button)
			{
			case BUTTON.A:
				key = KeyCode.R;
				break;
			case BUTTON.B:
				key = KeyCode.Y;
				break;
			case BUTTON.LEFT:
				key = KeyCode.F;
				break;
			case BUTTON.RIGHT:
				key = KeyCode.H;
				break;
			case BUTTON.UP:
				key = KeyCode.T;
				break;
			case BUTTON.DOWN:
				key = KeyCode.G;
				break;
			}	
			break;
		}
		return key;
	}
	
	
	// call to tell the Mini Game Machine that the game has ended
	public static void GameEnd()
	{
		Application.ExternalCall("GameEnd", "");
	}
	
	private static void GameStart()
	{
		Application.ExternalCall("GameStart", "");
	}
	
	// adds points to a given player
	public static void AddPoints(PLAYER player, int points)
	{
		switch (player)
		{
		case PLAYER.PLAYER1:
			Player1Points += points;
			break;
		case PLAYER.PLAYER2:
			Player2Points += points;
			break;
		case PLAYER.PLAYER3:
			Player3Points += points;
			break;
		case PLAYER.PLAYER4:
			Player4Points += points;
			break;
		}
	}
	
	public static void AddPointsPlayerOne(int points) {Player1Points += points;}
	public static void AddPointsPlayerTwo(int points) {Player2Points += points;}
	public static void AddPointsPlayerThree(int points) {Player3Points += points;}
	public static void AddPointsPlayerFour(int points) {Player4Points += points;}
	
	
	public static int GetTotalPoints() {
		return Player1Points + Player2Points + Player3Points + Player4Points;
	}
	
	public static int GetPoints(PLAYER player)
	{
		switch (player)
		{
		case PLAYER.PLAYER1:
			return Player1Points;
		case PLAYER.PLAYER2:
			return Player2Points;
		case PLAYER.PLAYER3:
			return Player3Points;
		case PLAYER.PLAYER4:
			return Player4Points;
		}
		return 0;
	}
	
	private static int Player1Points
	{
		get {return _player1Points;}
		set 
		{
			int points = value - _player1Points;
			if (points > 0) 
			{
				Application.ExternalCall("PlayerOnePoints", points);
				_player1Points = value;
			}
		}
	}
	
	private static int Player2Points
	{
		get {return _player2Points;}
		set 
		{
			int points = value - _player2Points;
			if (points > 0) 
			{
				Application.ExternalCall("PlayerTwoPoints", points);
				_player2Points = value;
			}
		}
	}
	
	private static int Player3Points
	{
		get {return _player3Points;}
		set 
		{
			int points = value - _player3Points;
			if (points > 0) 
			{
				Application.ExternalCall("PlayerThreePoints", points);
				_player3Points = value;
			}
		}
	}
	
	private static int Player4Points
	{
		get {return _player4Points;}
		set 
		{
			int points = value - _player4Points;
			if (points > 0) 
			{
				Application.ExternalCall("PlayerFourPoints", points);
				_player4Points = value;
			}
		}
	}
}
