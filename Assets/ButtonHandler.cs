using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonHandler : MonoBehaviour {
    public void OnPlayButton() {
        Main.GameContext.GameState = GameState.START_LEVEL;
    }
}
