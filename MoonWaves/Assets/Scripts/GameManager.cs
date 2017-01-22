using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	private static GameManager _instance;
	public delegate void StateChangeHandler(LevelStates oldState, LevelStates newState);
	public static event StateChangeHandler OnStateChange;

	public LevelStates startState;

	public enum LevelStates
	{
		None,
		MainMenu,
		IntroMovie,
		Countdown,
		Battle,
		Menu
	}
	private LevelStates _currentState = LevelStates.None;
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

		ChangeState(startState);
	}
	
	public static void SetState(LevelStates newState) { _instance._currentState = newState; }
	public void ChangeState(LevelStates newState)
	{
		OnLeaveState(_currentState);

		if (OnStateChange != null) OnStateChange(_currentState, newState);
		_currentState = newState;

		OnEnterState(newState);
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
				Time.timeScale = 0;
				break;
		}
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
				Time.timeScale = 1;
				break;
		}
	}
}
