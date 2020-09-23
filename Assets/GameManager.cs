using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public float currentHeight=0;
    public float score = 0;
    public float highestScore = 0;
    static public GameManager instance;
    [SerializeField] Text scoreText;
    [SerializeField] Text highestScoreText;
    [SerializeField] public  Text chainText;
    public GameObject[] Nodes;
    public GameObject baseColumn;
    private void Start()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
        highestScore = PlayerPrefs.GetInt("HighestScore");
        UpdateUI();
         CreateObstacle(0);
    }
    void CreateObstacle(int baseoffset)
    {
        for (int i = 0+(baseoffset*4); i < 4+ (baseoffset*4); i++)
        {
            CreateObstacleGroup(i);
        }
    }
    void CreateObstacleGroup(float offset)
    {
        GameObject parentColumn= Instantiate(baseColumn,Vector3.down*(6)* offset,Quaternion.identity);
        GameObject[] randomizedNodes = new GameObject[8];
        for (int i = 0; i < randomizedNodes.Length; i++)
        {
           randomizedNodes[i] = Nodes[Random.Range(0, Nodes.Length)]; 
        }
        int y = 0;
        for (int z = -1; z < 2; z++)
        {
            for (int r = -1; r < 2; r++)
            {
                if (r == 0 && z == 0) ;
                else
                {
                   GameObject instedNode= Instantiate(randomizedNodes[y]);
                    instedNode.transform.parent = parentColumn.transform;
                    instedNode.transform.localPosition = new Vector3(r, 0, z);
                    y++;
                }
            }
        }
        
    }
    public void AddScore(float _score)
    {
        score += _score;
        if (score>highestScore)
        {
            highestScore = score;
        }
        currentHeight += 1;
        if (currentHeight%4==1)
        {
            CreateObstacle(((int)currentHeight/4)+1);
        }
        UpdateUI();
    }
    public void SaveBeforeDeath()
    {
        PlayerPrefs.SetInt("HighestScore", (int)highestScore);
    }
    void UpdateUI()
    {
        scoreText.text = "" + score;
        highestScoreText.text = "" + highestScore;
    }
}
