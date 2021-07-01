using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StageIcon : MonoBehaviour {
    public int number;
    public TextMeshProUGUI text;
    public GameObject locked;
    public GameObject done;
    public GameState game;

    void Start() {
        text.text = (number + 1).ToString();
        if(game.GetPlantState(number)) {
            done.SetActive(true);
        }
        if(number > 0 && game.GetPlantState(number - 1)) {
            locked.SetActive(false);
        }
    }

}
