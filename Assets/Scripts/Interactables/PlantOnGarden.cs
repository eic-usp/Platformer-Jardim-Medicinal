using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantOnGarden : MonoBehaviour, Interactable{
    public string plantName;
    public int plantNumber;
    public GameObject plant; //mudar pra pegar no awake
    public GameObject infoMenu;
    public GameState gameState;

    void Start() {
        if(gameState.plantsObtained[plantNumber])plant.SetActive(true);
    }

    public void OnInteract() {
        Debug.Log("interact");
        infoMenu.SetActive(true);
    }

}
