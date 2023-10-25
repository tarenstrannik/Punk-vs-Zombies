using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine.SocialPlatforms.Impl;

public class MainManager : MonoBehaviour
{

    public int CurWave { get; set; }
    public int MaxWave { get; private set; }
    public string CurPlayer { get; private set; }

    private string tempCurPlayer;
    public static MainManager Instance { get; private set; }


    public int SavedWave { get; private set; }
    public float SavedPlayerHealth { get; private set; }
    public int SavedPlayerScore { get; private set; }

    private List<BestRecord> curBestRecords;
    [SerializeField] private int maxBestListCount = 10;

    public bool isGameActive = false;

    [SerializeField] private SceneAsset gameSceneAsset;
    public SceneAsset GameSceneAsset { get { return gameSceneAsset; } }

    public PlayerController playerController;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        LoadPlayerData();
    }
    [System.Serializable]
    class SaveData
    {
        public string playerName;
        public BestRecordList records;
        public GameParamsList gameParams;
    }
    [System.Serializable]
    public class BestRecord
    {
        public string playerName;
        public int playerScore;
    }
    [System.Serializable]

    public class GameParamsList
    {
        public List<GameParam> gameParam;
    }
    [System.Serializable]
    public enum paramTypes
    {
        String,
        Float,
        Int
    };
    [System.Serializable]
    public class GameParam
    {
        public string paramName;
        public paramTypes paramType;
        public float paramFloatValue;
        public string paramStringValue;
        public int paramIntValue;
    }

    [System.Serializable]
    public class BestRecordList
    {
        public List<BestRecord> bestRecordList;
    }

    public void TempCurPlayerSet(string value)
    {
        tempCurPlayer = value;
    }
    public void SetCurPlayerScore()
    {
        CurPlayer = tempCurPlayer;
        UpdateBestList(GameManager.Instance.Score, CurPlayer);
    }

    public void SavePlayerData()
    {
        SavedPlayerScore = GameManager.Instance.Score;
        SavedWave = CurWave;
        SavedPlayerHealth = playerController.PersonHealth;
        //Debug.Log(SavedWave + " " + CurWave + " " + SavedPlayerScore + " " + SavedPlayerHealth);
        GameParam savedWaveParam = new GameParam
        {
            paramName = "savedWave",
            paramType = paramTypes.Int,
            paramIntValue = CurWave

        };
        GameParam maxWaveParam = new GameParam
        {
            paramName = "maxWave",
            paramType = paramTypes.Int,
            paramIntValue = MaxWave

        };

        GameParam playerHealth = new GameParam
        {
            paramName = "playerHealth",
            paramType = paramTypes.Float,
            paramFloatValue = playerController.PersonHealth

        };

            GameParam playerScore = new GameParam
            {
                paramName = "playerScore",
                paramType = paramTypes.Int,
                paramIntValue = GameManager.Instance.Score

        };

SaveData data = new SaveData
        {
            playerName = CurPlayer,
            records = new BestRecordList
            {
                bestRecordList = curBestRecords
            },
            gameParams = new GameParamsList
            {
                gameParam = new List<GameParam>
                {
                    savedWaveParam,
                    maxWaveParam,
                    playerHealth,
                    playerScore
                }
            }
        };


        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }


    public int GetBestScore()
    {
        if (curBestRecords.Count > 0)
            return curBestRecords[0].playerScore;
        else return 0;
    }
    public List<BestRecord> GetLeaders()
    {

        if (curBestRecords.Count > 0)
            return curBestRecords;
        else return null;
    }

    public void UpdateBestList(int score, string playerName)
    {
        BestRecord record = new BestRecord();
        record.playerName = playerName;
        record.playerScore = score;
        curBestRecords.Insert(0, record);
        if (curBestRecords.Count > maxBestListCount)
        {
            curBestRecords.RemoveRange(maxBestListCount, curBestRecords.Count - maxBestListCount);

        }
    }


    public void LoadPlayerData()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);
            if (data != null)
            {
                if (data.playerName != null)
                {
                    CurPlayer = data.playerName;
                }
                else
                {
                    CurPlayer = "";
                }
                if (data.records != null)
                {
                    curBestRecords = data.records.bestRecordList;
                }
                else
                {
                    curBestRecords = new List<BestRecord> { };
                }

                if (data.gameParams != null && data.gameParams.gameParam != null)
                {
                    GameParam targetGameParamSavedWave = data.gameParams.gameParam.FirstOrDefault(param => param.paramName == "savedWave");

                    // ���������, ��� �� ������ ������ GameParam � �������� paramName
                    if (targetGameParamSavedWave != null)
                    {
                        SavedWave = targetGameParamSavedWave.paramIntValue;


                        // ������ � �������� GameParam ��, ��� ��� �����
                    }
                    else
                    {
                        SavedWave = 1;
                    }
                    GameParam targetGameParamMaxWave = data.gameParams.gameParam.FirstOrDefault(param => param.paramName == "maxWave");

                    // ���������, ��� �� ������ ������ GameParam � �������� paramName
                    if (targetGameParamMaxWave != null)
                    {
                        MaxWave = targetGameParamMaxWave.paramIntValue;


                        // ������ � �������� GameParam ��, ��� ��� �����
                    }
                    else
                    {
                        MaxWave = 1;
                    }

                    GameParam targetPlayerHealth = data.gameParams.gameParam.FirstOrDefault(param => param.paramName == "playerHealth");

                    // ���������, ��� �� ������ ������ GameParam � �������� paramName
                    if (targetPlayerHealth != null)
                    {

                        SavedPlayerHealth = targetPlayerHealth.paramFloatValue;
                        // ������ � �������� GameParam ��, ��� ��� �����
                    }
                    else
                    {
                        SavedPlayerHealth = 0;
                    }

                    GameParam targetScoreValue = data.gameParams.gameParam.FirstOrDefault(param => param.paramName == "playerScore");

                    // ���������, ��� �� ������ ������ GameParam � �������� paramName
                    if (targetScoreValue != null)
                    {
                        SavedPlayerScore = targetScoreValue.paramIntValue;
                        // ������ � �������� GameParam ��, ��� ��� �����
                    }
                    else
                    {
                        SavedPlayerScore = 0;
                    }


                }
                else
                {
                    SetDefaultParams();
                }
            }
            else
            {
                SetDefaultParams();
                SetDefaultPlayerParams();
            }
        }
        else
        {
            SetDefaultParams();
            SetDefaultPlayerParams();
        }
    }

    private void SetDefaultParams()
    {
        SavedWave = 1;
        CurWave = 1;
        MaxWave = 1;
        SavedPlayerHealth = 0;
        SavedPlayerScore = 0;
        
    }
    private void SetDefaultPlayerParams()
    {
        curBestRecords = new List<BestRecord> { };
        CurPlayer = "";
    }

    public void SetMaxWave(int maxWave)
    {
        MaxWave = maxWave;
    }
}
