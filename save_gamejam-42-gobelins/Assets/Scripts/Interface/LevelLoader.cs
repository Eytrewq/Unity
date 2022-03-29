using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public Animator transition;
    public float transitionTime;
    public Canvas instruction;

    private void Start()
    {
        StartCoroutine(Waiting());
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            LoadGame();
        }
    }
    public void LoadGame()
    {
        StartCoroutine(LoadIn());
    }
    IEnumerator LoadIn()
    {
        transition.SetTrigger("Start");
        instruction.enabled = false;
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(1);
    }
    IEnumerator Waiting()
    {
        yield return new WaitForSeconds(transitionTime);
        instruction.gameObject.SetActive(true);
    }
}
