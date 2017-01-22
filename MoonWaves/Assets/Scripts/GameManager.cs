﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

	public List<SceneAssociation> stateSceneAssociations;
	[System.Serializable]
	public class SceneAssociation
	{
		public string sceneName;
		public LevelStates associatedState;
	}

	void Awake()
	{
		if (_instance != null)
		{
			DestroyImmediate(gameObject);
			return;
		}

		_instance = this;
		DontDestroyOnLoad(this);

		SetState(startState);
	}
	
	public static void SetState(LevelStates newState) { _instance.StartCoroutine(_instance.ChangeState(newState)); }
	private IEnumerator ChangeState(LevelStates newState)
	{
		Debug.Log("Leaving state - " + _currentState);
		OnLeaveState(_currentState);

		SceneAssociation newScene = stateSceneAssociations.Find(x => x.associatedState == newState);
		if (newScene != null && SceneManager.GetActiveScene().name != newScene.sceneName)
		{
			SceneManager.LoadScene(newScene.sceneName);
		}
		
		yield return null;

		if (OnStateChange != null) OnStateChange(_currentState, newState);
		_currentState = newState;

		Debug.Log("Enter state - " + newState);
		OnEnterState(newState);
	}

	public void OnEnterState(LevelStates state)
	{
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