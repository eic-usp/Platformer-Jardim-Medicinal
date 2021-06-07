using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "GameState")]
public class GameState : ScriptableObject{
    public bool[] plantsObtained;

    /*void SetPlantAsCollected(int i) {
        plantsObtained[i] = true;
    }*/
}
