using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

public class EndScreenManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI deathText;
    // Start is called before the first frame update
    void Start()
    {
        scoreText.text = "Score : " + GameManager.Instance.Score;
        deathText.text = "Deaths : " + GameManager.Instance.Deaths;
    }
}
