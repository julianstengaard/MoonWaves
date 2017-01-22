using System.Collections;
using System.Collections.Generic;
using Holoville.HOTween;
using UnityEngine;

public class Countdown : MonoBehaviour {
    public TextMesh[] MenuOptions;


    // Use this for initialization
    void Start () {
        StartCoroutine(RunIntroAnimation());
    }

    IEnumerator RunIntroAnimation() {
        yield return new WaitForSeconds(1f);

        MenuOptions[0].gameObject.SetActive(true);
        MenuOptions[0].characterSize = 0.001f;
        HOTween.To(MenuOptions[0], 0.3f, "characterSize", 0.2f, false, EaseType.EaseOutExpo, 0f);
        yield return new WaitForSeconds(2f);
        MenuOptions[0].gameObject.SetActive(false);

        for (int i = 1; i < MenuOptions.Length - 1; i++) {
            MenuOptions[i].gameObject.SetActive(true);
            MenuOptions[i].characterSize = 10f;
            HOTween.To(MenuOptions[i], 0.3f, "characterSize", 0.2f, false, EaseType.EaseOutExpo, 0f);
            yield return new WaitForSeconds(1f);
            MenuOptions[i].gameObject.SetActive(false);
        }

        MenuOptions[MenuOptions.Length - 1].gameObject.SetActive(true);
        MenuOptions[MenuOptions.Length - 1].characterSize = 0.001f;
        HOTween.To(MenuOptions[MenuOptions.Length - 1], 0.3f, "characterSize", 0.2f, false, EaseType.EaseOutExpo, 0f);
        GameManager.SetState(GameManager.LevelStates.Battle, false);
        yield return new WaitForSeconds(1f);
        MenuOptions[MenuOptions.Length - 1].gameObject.SetActive(false);
    }
}
