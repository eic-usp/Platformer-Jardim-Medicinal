using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantCollectable : MonoBehaviour, Interactable{
    public string plantName;
    public int plantNumber;
    public GameState gameState;

    public void OnInteract() {
        /*gameState.SetPlantAsCollected(plantNumber);*/
        GoToGarden();
    }

    void GoToGarden() {
        SceneLoader sceneLoader = GameObject.Find("SceneLoader").GetComponent<SceneLoader>();
        sceneLoader.LoadSceneByName("Garden");
    }

    void SetPlantAsCollected() {

    }
}
