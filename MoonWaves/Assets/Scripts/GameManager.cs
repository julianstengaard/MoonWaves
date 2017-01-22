using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	private static GameManager _instance;
	public delegate void StateChangeHandler(LevelStates oldState, LevelStates newState);
	public static event StateChangeHandler OnStateChange;

	public enum LevelStates
	{
		MainMenu,
		IntroMovie,
		Countdown,
		Battle,
		Menu
	}
	private LevelStates _currentState;
	public static LevelStates currentState { get { return _instance._currentState; } }

	void Awake()
	{
		if (_instance != null)
		{
			DestroyImmediate(gameObject);
			return;
		}
		_instance = this;
		DontDestroyOnLoad(this);
	}
	
	public static void SetState(LevelStates newState) { _instance._currentState = newState; }
	public void ChangeState(LevelStates newState)
	{
		OnLeaveState(_currentState);

		OnStateChange(_currentState, newState);
		_currentState = newState;

		OnEnterState(newState);
	}

	public void OnLeaveState(LevelStates state)
	{
		Debug.Log("Leaving state - " + state);

		switch (state)
		{
			case LevelStates.MainMenu:

				break;


			case LevelStates.IntroMovie:

				break;


			case LevelStates.Countdown:
				
				break;


			case LevelStates.Battle:
				
				break;


			case LevelStates.Menu:
				
				break;
		}
	}
	public void OnEnterState(LevelStates state)
	{
		Debug.Log("Enter state - " + state);

		switch (state)
		{
			case LevelStates.MainMenu:

				break;


			case LevelStates.IntroMovie:

				break;


			case LevelStates.Countdown:

				break;


			case LevelStates.Battle:

				break;


			case LevelStates.Menu:

				break;
		}
	}
}
