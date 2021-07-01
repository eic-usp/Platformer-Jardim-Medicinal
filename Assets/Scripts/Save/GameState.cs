using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "GameState")]
public class GameState : ScriptableObject{
    public bool[] plantsObtained;

    public void SetPlantAsCollected(int i) {
        plantsObtained[i] = true;
    }

    public bool GetPlantState(int number) {
        return (plantsObtained[number]);
    }
}
