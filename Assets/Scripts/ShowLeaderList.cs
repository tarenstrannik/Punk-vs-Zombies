using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShowLeaderList : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI  leadersList;
    // Start is called before the first frame update
    void OnEnable()
    {
        if (MainManager.Instance.GetLeaders() != null && MainManager.Instance.GetLeaders().Count > 0)
        {
            leadersList.text = "";
            int i = 1;
            foreach (MainManager.BestRecord bestRecord in MainManager.Instance.GetLeaders())
            {
                leadersList.text += $"{i}. {bestRecord.playerName} : {bestRecord.playerScore} \n";
                i++;
            }
        }
    }


}
