using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class BlockchainUI : MonoBehaviour
{
    public TextMeshProUGUI txt;
    public Image currentImage;
    public Sprite bull;
    public Sprite bear;
    public Sprite stable;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(this.UpdateUI());
    }

    private IEnumerator UpdateUI() {
        for (;;) {
            yield return new WaitForSeconds(1f);

            float difficulty = Blockchain.Instance.latestDifficulty;

            if (difficulty >= 1.5) {
                txt.text = "La blockchain traite plus de transactions que d'habitude.. (" + difficulty + ")";
                currentImage.sprite = bull;
            }
            else if (difficulty <= 0.8) {
                txt.text = "La blockchain traite moins de transactions que d'habitude.. (" + difficulty + ")";
                currentImage.sprite = bear;
            }
            else {
                txt.text = "La blockchain traite un nombre de transactions stables.. (" + difficulty + ")";
                currentImage.sprite = stable;
            }
        }
    }
}
