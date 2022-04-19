using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBlockchain : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Blockchain.Instance.FetchDifficulty(this.ShowDifficulty);
    }

    void ShowDifficulty(float difficulty) {
        Debug.Log("Blockchain difficulty is :" + difficulty);
    }
}
