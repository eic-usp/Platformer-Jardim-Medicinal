using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour {
    /*public Animator transition;
    public float transitionTime = 0.5f;
    public GameObject loadingScreen;
    public Slider slider;
    float progress;*/

    public void LoadSceneByNumber(int i) {
        StartCoroutine(LoadLevel(i,""));
    }

    public void LoadSceneByName(string name) {
        StartCoroutine(LoadLevel(-1, name));
    }

    IEnumerator LoadLevel(int levelIndex, string levelName) {
        //transition.SetTrigger("Start");
        AsyncOperation operation;
        if(levelIndex != -1) {
            operation = SceneManager.LoadSceneAsync(levelIndex);
        }else {
            operation = SceneManager.LoadSceneAsync(levelName);
        }
        //loadingScreen.SetActive(true);
        while(!operation.isDone) {
            /*progress = Mathf.Clamp01(operation.progress / .9f);
            slider.value = progress;*/
            yield return null;
        }
    }

    
}