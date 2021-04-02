using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

// This code was made with the help of https://www.youtube.com/watch?v=iAbaqGYdnyI&ab_channel=CodeMonkey
public class HighScoreTable : MonoBehaviour
{
    private Transform scoreContainer;
    private Transform scoreTemplate;
    private List<Transform> scoreEntryTransformList;

    private void Awake(){
        scoreContainer = transform.Find("ScoreContainer");
        scoreTemplate = scoreContainer.Find("ScoreTemplate");

        scoreTemplate.gameObject.SetActive(false);

        // scoreEntryList = new List<ScoreEntry>() {
        //     new ScoreEntry{ score = 11, name = "AAA"},
        //     new ScoreEntry{ score = 22, name = "BBB"},
        //     new ScoreEntry{ score = 33, name = "CCC"},
        //     new ScoreEntry{ score = 44, name = "DDD"},
        //     new ScoreEntry{ score = 55, name = "EEE"},
        //     new ScoreEntry{ score = 66, name = "FFF"},
        //     new ScoreEntry{ score = 77, name = "GGG"},
        // };

        string jsonString = PlayerPrefs.GetString("highscoreTable");
        Highscores highscores = JsonUtility.FromJson<Highscores>(jsonString);

        // Simple bubble sort on data
        for (int i = 0; i < highscores.scoreEntryList.Count; i++) {
            for (int j = i + 1; j < highscores.scoreEntryList.Count; j++) {
                if (highscores.scoreEntryList[j].score > highscores.scoreEntryList[i].score) {
                    // Preform a swap
                    ScoreEntry temp = highscores.scoreEntryList[i];
                    highscores.scoreEntryList[i] = highscores.scoreEntryList[j];
                    highscores.scoreEntryList[j] = temp;
                }
            }
        }

        scoreEntryTransformList = new List<Transform>();
        foreach (ScoreEntry scoreEntry in highscores.scoreEntryList)
        {
            CreateScoreEntryTransform(scoreEntry, scoreContainer, scoreEntryTransformList);
        }
    }
        

    // This function adds a highscore to the table
    private void CreateScoreEntryTransform(ScoreEntry scoreEntry, Transform container, List<Transform> transformList) {
        float templateHeight = 50f;

        Transform scoreTransform = Instantiate(scoreTemplate, container);
        RectTransform scoreRectTransform = scoreTransform.GetComponent<RectTransform>();
        scoreRectTransform.anchoredPosition = new Vector2(0, -templateHeight * transformList.Count);
        scoreTransform.gameObject.SetActive(true);

        int rank = transformList.Count + 1;
        string rankString;
        rankString = rank + "";

        scoreTransform.Find("Rank").GetComponent<TextMeshProUGUI>().text = rankString;

        // Grab the name and set the text
        string name = scoreEntry.name;
        scoreTransform.Find("Name").GetComponent<TextMeshProUGUI>().text = name;

        // Grab score and set text
        int score = scoreEntry.score;
        scoreTransform.Find("Score").GetComponent<TextMeshProUGUI>().text = score.ToString();

        transformList.Add(scoreTransform);
    }

    private void addScoreEntry(int score, string name){
        // Create a new entry
        ScoreEntry scoreEntry = new ScoreEntry{ score = score, name = name};

        // Pull up the current highscore list
        string jsonString = PlayerPrefs.GetString("highscoreTable");
        Highscores highscores = JsonUtility.FromJson<Highscores>(jsonString);

        // Add the entry to the highscore list
        highscores.scoreEntryList.Add(scoreEntry);

        // Save updated highscore list
        string json = JsonUtility.ToJson(highscores);
        PlayerPrefs.SetString("highscoreTable", json);
        PlayerPrefs.Save();
    }

    private class Highscores {
        public List<ScoreEntry> scoreEntryList;
    }

    // This is a structure for a single highscore entry
    [System.Serializable]
    private class ScoreEntry {
        public int score;
        public string name;
    }
}
