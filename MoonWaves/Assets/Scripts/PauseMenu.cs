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

    private int _currentMenuOption;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		HandlePause();
        HandleSelect();
	    
	}

    private void HandleSelect() {
        if (_selectButtonReady && Input.GetAxis("Horizontal") > 0.5f) {
            _selectButtonReady = false;
            SetFont(_currentMenuOption, false);
            _currentMenuOption = (_currentMenuOption + 1) % 4;
            SetFont(_currentMenuOption, true);
        }

        if (!_selectButtonReady && Mathf.Abs(Input.GetAxis("Horizontal")) < 0.05f) {
            _selectButtonReady = true;
        }
    }

    private void HandlePause() {
        if (_pauseButtonReady && Input.GetAxis("Pause") > 0.5f) {
            _pauseButtonReady = false;
            SetPauseState(!_paused);
        }

        if (!_pauseButtonReady && Input.GetAxis("Pause") <= 0.01f) {
            _pauseButtonReady = true;
        }
    } 

    private void SetFont(int option, bool selected) {
        MenuOptions[_currentMenuOption].font = selected ? Selected : Unselected;
        MenuOptions[_currentMenuOption].GetComponent<Renderer>().sharedMaterial = selected ? Selected.material : Unselected.material;
    }

    private void SetPauseState(bool b) {
        _paused = b;
        Time.timeScale = b ? 0.01f : 1f;
        Background.SetActive(b);
    }
}
