using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundButton : MonoBehaviour {
    public GameObject[] doors;
    public bool acceptDrone = true;
    public SpriteRenderer buttonSprite;
    public Sprite[] sprites;
    int isPressing = 0;

    void OnTriggerEnter2D(Collider2D collider) {
        if(CheckTag(collider)) {
            if(isPressing == 0) {
                foreach(GameObject door in doors) {
                    door.GetComponent<Activatable>().OnActivate();
                }
                isPressing++;
                buttonSprite.sprite = sprites[1];
            }else isPressing++;
        }
    }

    void OnTriggerExit2D(Collider2D collider) {
        if(CheckTag(collider)) {
            if(isPressing == 1) {
                foreach(GameObject door in doors) {
                    door.GetComponent<Activatable>().OnActivate();
                }
                isPressing--;
                buttonSprite.sprite = sprites[0];
            }else isPressing--;
        }
    }

    bool CheckTag(Collider2D col) {
        if(acceptDrone) {
            return(col.tag != "Untagged");
        }else return(col.tag != "Untagged"  && col.tag != "Drone");
    }
}
