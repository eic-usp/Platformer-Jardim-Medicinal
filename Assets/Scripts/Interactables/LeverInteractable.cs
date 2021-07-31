using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverInteractable : MonoBehaviour, Interactable{
    public GameObject[] doors;
    public SpriteRenderer leverSprite;
    public Sprite[] sprites;
    private int i = 0;

    public void OnInteract() {
        foreach(GameObject door in doors) {
            door.GetComponent<Activatable>().OnActivate();
            leverSprite.sprite = sprites[(i+1)%2];
        }
    }
}
