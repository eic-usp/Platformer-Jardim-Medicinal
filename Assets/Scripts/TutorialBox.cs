using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TutorialBox : MonoBehaviour {
    public GameObject text;

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Player") { 
            text.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(other.tag == "Player") { 
            text.SetActive(false);
        }
    }
}
