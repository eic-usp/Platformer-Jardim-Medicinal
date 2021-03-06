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
        if(gameState.GetPlantState(plantNumber))plant.SetActive(true);
    }

    public void OnInteract() {
        if(gameState.GetPlantState(plantNumber)) {
            GameObject.Find("PlayerSprite").GetComponent<PlayerMovement>().CanMove = false;
            infoMenu.SetActive(true);
        }
    }

}
