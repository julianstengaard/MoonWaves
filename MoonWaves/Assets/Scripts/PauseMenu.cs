using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour {
    public GameObject Background;

    public TextMesh[] MenuOptions;

    public Font Unselected;
    public Font Selected;

    private bool _paused;
    private bool _pauseButtonReady;

    private bool _selectButtonReady;
    private bool _clickButtonReady;

    private int _currentMenuOption;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        HandlePause();

	    if (!_paused) {
	        return;
	    }
        HandleSelect();
	    HandlePress();
	}

    private void HandlePress() {
        if (_clickButtonReady && (Input.GetAxisRaw("Fire1") > 0.7f || Input.GetAxisRaw("Fire2") > 0.7f)) {
            _clickButtonReady = false;
            if (_currentMenuOption == 0) {
                SetPauseState(false);
                print("Resume");
                GameManager.SetState(GameManager.LevelStates.Battle);
            }
            if (_currentMenuOption == 1) {
                //Restart
                print("Restart");
                GameManager.SetState(GameManager.LevelStates.Countdown, true);
            }
            if (_currentMenuOption == 2) {
                //Main menu
                print("Main");
                GameManager.SetState(GameManager.LevelStates.MainMenu);
            }
            if (_currentMenuOption == 3) {
                //Exit
                print("Exit");
                Application.Quit();
            }
        }
        if (!_clickButtonReady && Input.GetAxisRaw("Fire1") <= 0.2f && Input.GetAxisRaw("Fire2") <= 0.2f) {
            _clickButtonReady = true;
        }
    }

    private void HandleSelect() {
        if (_selectButtonReady && Input.GetAxisRaw("MenuVertical") < -0.999999f) {
            _selectButtonReady = false;
            SetFont(_currentMenuOption, false);
            _currentMenuOption = (_currentMenuOption + 1) % 4;
            SetFont(_currentMenuOption, true);
        } else if (_selectButtonReady && Input.GetAxisRaw("MenuVertical") > 0.999999f) {
            _selectButtonReady = false;
            SetFont(_currentMenuOption, false);
            _currentMenuOption = (_currentMenuOption == 0) ? 4 : _currentMenuOption;
            _currentMenuOption = (_currentMenuOption - 1);
            SetFont(_currentMenuOption, true);
        }

        if (!_selectButtonReady && Mathf.Abs(Input.GetAxisRaw("MenuVertical")) < 1f) {
            _selectButtonReady = true;
        }
    }

    private void HandlePause() {
        if (_pauseButtonReady && Input.GetAxisRaw("Pause") > 0.7f) {
            _pauseButtonReady = false;
            SetPauseState(!_paused);
        }

        if (!_pauseButtonReady && Input.GetAxisRaw("Pause") <= 0.2f) {
            _pauseButtonReady = true;
        }
    } 

    private void SetFont(int option, bool selected) {
        MenuOptions[option].font = selected ? Selected : Unselected;
        MenuOptions[option].GetComponent<Renderer>().sharedMaterial = selected ? Selected.material : Unselected.material;
    }

    private void SetPauseState(bool b) {
        if (GameManager.currentState == GameManager.LevelStates.Countdown) {
            return;
        }

        if (b) {
            GameManager.SetState(GameManager.LevelStates.Menu);
        } else {
            GameManager.SetState(GameManager.LevelStates.Battle);
        }
        _paused = b;
        Background.SetActive(b);
        for (int i = 0; i < MenuOptions.Length; i++) {
            MenuOptions[i].gameObject.SetActive(b);
        }
        SetFont(_currentMenuOption, true);
    }
}
