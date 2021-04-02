using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HighScoreTable : MonoBehaviour
{
    private Transform scoreContainer;
    private Transform scoreTemplate;

    private void Awake(){
        scoreContainer = transform.Find("ScoreContainer");
        scoreTemplate = scoreContainer.Find("ScoreTemplate");

        scoreTemplate.gameObject.SetActive(false);

        float templateHeight = 50f;
        for(int i = 0; i < 8; i++)
        {
            Transform scoreTransform = Instantiate(scoreTemplate, scoreContainer);
            RectTransform scoreRectTransform = scoreTransform.GetComponent<RectTransform>();
            scoreRectTransform.anchoredPosition = new Vector2(0, -templateHeight * i);
            scoreTransform.gameObject.SetActive(true);

            int rank = i + 1;
            string rankString;
            rankString = rank + "";

            scoreTransform.Find("Rank").GetComponent<TextMeshProUGUI>().text = rankString;

            string name = "AAA";

            scoreTransform.Find("Name").GetComponent<TextMeshProUGUI>().text = name;

            int score = Random.Range(0, 10000);
            scoreTransform.Find("Score").GetComponent<TextMeshProUGUI>().text = score.ToString();
        }
    }
}
