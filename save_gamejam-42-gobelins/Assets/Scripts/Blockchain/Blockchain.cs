using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System;

public class Blockchain : Singleton<Blockchain>
{
    private string uri = "https://agile-journey-15910.herokuapp.com/txns";
    public float latestDifficulty = 1f;

    public void FetchDifficulty(Action<float> resultCallback) {
        StartCoroutine(GetText(resultCallback));
    }

    private IEnumerator GetText(Action<float> resultCallback) {
        UnityWebRequest www = UnityWebRequest.Get(uri);
        yield return www.SendWebRequest();
 
        if (www.result != UnityWebRequest.Result.Success) {
            Debug.Log(www.error);
            Debug.LogWarning("Web request failed.");
        }
        else {
            float difficulty = 1;
            // Show results as text
            bool success = float.TryParse(www.downloadHandler.text, out difficulty);

            Debug.Log("Blockchain difficulty fetched, result is :" + difficulty);

            if (!success) {
                difficulty = UnityEngine.Random.Range(0.6f, 2f);
                Debug.LogWarning("Failed to parse, randomize difficulty :" + difficulty);
            }

            difficulty = Mathf.Clamp(difficulty, 0.2f, 3);
            latestDifficulty = difficulty;

            resultCallback(difficulty);
        }
    }
}
