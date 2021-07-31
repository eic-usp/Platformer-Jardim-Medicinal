using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "GameState")]
public class GameState : ScriptableObject{
    public bool[] plantsObtained;
    public int[] stageCoins;

    public void SetPlantAsCollected(int i) {
        plantsObtained[i] = true;
    }

    public bool GetPlantState(int number) {
        return (plantsObtained[number]);
    }

    public void reset() {
        for(int i = 0; i < plantsObtained.Length; i++){
            plantsObtained[i] = false;
            stageCoins[i] = 0;
        }
    }
}
