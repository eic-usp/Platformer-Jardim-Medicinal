using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour {
    public GameState game;
    public SceneLoader sceneLoader;
    // Start is called before the first frame update
    public void PlayGame() {
        if(game.GetPlantState(0) == true) {
            sceneLoader.LoadSceneByName("Garden");
        } else {
            sceneLoader.LoadSceneByName("Stage1");
        }
    }
}
