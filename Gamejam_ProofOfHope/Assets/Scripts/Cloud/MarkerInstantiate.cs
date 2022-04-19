using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarkerInstantiate : MonoBehaviour
{
    [SerializeField] private float duration = 4.0f;
    [SerializeField] private float currentAlpha = 1.0f;
    [SerializeField] private Material myMaterial;
    [SerializeField] private Renderer myModel;

    void Update()
    {
        currentAlpha -= 1 / duration * Time.deltaTime;
        if (currentAlpha > 0)
        {
            Color color = myModel.material.color;
            color.a = currentAlpha;
            myModel.material.color = color;
        }
        else
        {
            Destroy(myModel.material);
            Destroy(this.gameObject);
        }
    }

}
