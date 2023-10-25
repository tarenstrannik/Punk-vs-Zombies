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


    private List<BestRecord> curBestRecords;
    [SerializeField] private int maxBestListCount = 10;

    public bool isGameActive = false;

    [SerializeField] private SceneAsset gameSceneAsset;
    public SceneAsset GameSceneAsset { get { return gameSceneAsset; } }

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
        UpdateBestList(GameManager.Instance.Score, tempCurPlayer);
    }

    public void SavePlayerData()
    {
        GameParam curWaveParam = new GameParam
        {
            paramName = "curWave",
            paramType = paramTypes.Int,
            paramIntValue = CurWave

        };
        GameParam maxWaveParam = new GameParam
        {
            paramName = "maxWave",
            paramType = paramTypes.Int,
            paramIntValue = MaxWave

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
                    curWaveParam,
                    maxWaveParam
                }
            }
        };


        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }
    public void SetCurWave(int wave)
    {
        CurWave = wave;
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
        CurPlayer = playerName;
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
            if (data.playerName != null)
                CurPlayer = data.playerName;
            else CurPlayer = "";
            if (data.records != null)
                curBestRecords = data.records.bestRecordList;
            else curBestRecords = new List<BestRecord> { };

            if (data.gameParams != null && data.gameParams.gameParam != null)
            {
                GameParam targetGameParamCurWave = data.gameParams.gameParam.FirstOrDefault(param => param.paramName == "curWave");

                // Проверяем, был ли найден объект GameParam с заданным paramName
                if (targetGameParamCurWave != null)
                {
                    CurWave = targetGameParamCurWave.paramIntValue;


                    // Делаем с объектом GameParam то, что вам нужно
                }
                else
                {
                    CurWave = 1;
                }
                GameParam targetGameParamMaxWave = data.gameParams.gameParam.FirstOrDefault(param => param.paramName == "maxWave");

                // Проверяем, был ли найден объект GameParam с заданным paramName
                if (targetGameParamMaxWave != null)
                {
                    MaxWave = targetGameParamMaxWave.paramIntValue;


                    // Делаем с объектом GameParam то, что вам нужно
                }
                else
                {
                    MaxWave = 1;
                }
            }
            else
            {
                CurWave = 1;
                MaxWave = 1;
            }


        }
        else
        {
            CurWave = 1;
            MaxWave = 1;
            curBestRecords = new List<BestRecord> { };
            CurPlayer = "";
        }
    }


    public void SetMaxWave(int maxWave)
    {
        MaxWave = maxWave;
    }
}
