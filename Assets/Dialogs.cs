using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialogs : MonoBehaviour {
    public GameObject WelcomeDialog;
    public GameObject EndGameDialog;

    public void WelcomeButtonOk() {
        WelcomeDialog.GetComponent<Canvas>().enabled = false;
        WelcomeDialog.SetActive(false);
        Main.GameContext.GameState = GameState.START_LEVEL;
    }

    public void PlayAgainButton() {
        Main.GameContext.GameState = GameState.START_LEVEL;
    }

    internal void DismissAll() {
        WelcomeDialog.SetActive(false);
        EndGameDialog.SetActive(false);
    }
}
