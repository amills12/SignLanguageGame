using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

// This code was made with the help of https://www.youtube.com/watch?v=iAbaqGYdnyI&ab_channel=CodeMonkey
public class HighScoreTable : MonoBehaviour
{
    public bool hasTable;
    private const int MAX_TABLE_SIZE = 7;
    private Transform scoreContainer;
    private Transform scoreTemplate;
    private List<Transform> scoreEntryTransformList;

    private void Awake(){
        // This enables the table control, this is used if you want to automate the highscore table.
        if(hasTable)
        {
            SetupDisplayHighscoreTable();
        }
    }

    // This function sets up the display table and organizes the score
    private void SetupDisplayHighscoreTable()
    {
        // This enables the table control, this is used if you want to automate the highscore table.
        scoreContainer = transform.Find("ScoreContainer");
        scoreTemplate = scoreContainer.Find("ScoreTemplate");

        scoreTemplate.gameObject.SetActive(false);

        string jsonString = PlayerPrefs.GetString("highscoreTable");
        Highscores highscores = JsonUtility.FromJson<Highscores>(jsonString);

        // If the highscores return null, then table is empty or got deleted
        if(highscores == null)
        {
            generateDefaultTable(highscores);
        }

        //Set up score table
        scoreEntryTransformList = new List<Transform>();
        foreach (ScoreEntry scoreEntry in highscores.scoreEntryList)
        {
            CreateScoreEntryTransform(scoreEntry, scoreContainer, scoreEntryTransformList);
        }
    }

    // This function adds a highscore to the display table
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

    public void addScoreEntry(int score, string name){
        
        // Pull up the current highscore list
        string jsonString = PlayerPrefs.GetString("highscoreTable");
        Highscores highscores = JsonUtility.FromJson<Highscores>(jsonString);

        // If the incoming score is greater than the last score in the list
        if (score > highscores.scoreEntryList[MAX_TABLE_SIZE-1].score)
        {
            // Create a new entry
            ScoreEntry scoreEntry = new ScoreEntry{ score = score, name = name};

            // Add the entry to the highscore list
            highscores.scoreEntryList.Add(scoreEntry);

            // Sort the array
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

            // Remove the last item
            highscores.scoreEntryList.RemoveAt(MAX_TABLE_SIZE);
        }

        // Save updated highscore list
        string json = JsonUtility.ToJson(highscores);
        PlayerPrefs.SetString("highscoreTable", json);
        PlayerPrefs.Save();
    }

    public void resetHighscores()
    {
        PlayerPrefs.DeleteKey("highscoreTable");

        for(int i = 0; i < scoreEntryTransformList.Count; i++)
        {
            Destroy(scoreEntryTransformList[i].gameObject);
        }
        
        SetupDisplayHighscoreTable();
    }

    public bool checkIfHighscore(int score)
    {
        // Pull up the current highscore list
        string jsonString = PlayerPrefs.GetString("highscoreTable");
        Highscores highscores = JsonUtility.FromJson<Highscores>(jsonString);

        if(highscores == null)
        {
            generateDefaultTable(highscores);
        }

        return score > highscores.scoreEntryList[MAX_TABLE_SIZE-1].score;
    }
    
    private void generateDefaultTable(Highscores highscores)
    {
        highscores = new Highscores{};
        highscores.scoreEntryList = new List<ScoreEntry>() {
            new ScoreEntry{ score = 5000, name = "PBJ"},
            new ScoreEntry{ score = 4000, name = "ACM"},
            new ScoreEntry{ score = 3000, name = "TOC"},
            new ScoreEntry{ score = 2000, name = "MEC"},
            new ScoreEntry{ score = 1000, name = "JAM"},
            new ScoreEntry{ score = 750, name = "DAH"},
            new ScoreEntry{ score = 500, name = "DAL"},
        };
        
        //Store this new table
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
