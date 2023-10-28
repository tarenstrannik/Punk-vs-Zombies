using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ContinueGameOperations : MonoBehaviour
{
    [SerializeField] private Button continueButton;
    [SerializeField] private Button leadersButton;
    
    [SerializeField] private GameObject newGameTextAttention;
    private void Start()
    {
        
        if(MainManager.Instance.SavedWave!=1)
        {
            continueButton.interactable = true;
            
            newGameTextAttention.SetActive(true);
        }
        else
        {
            continueButton.interactable = false;
            
            newGameTextAttention.SetActive(false);
        }

        if(MainManager.Instance.GetBestScore()==0)
        {
            leadersButton.interactable = false;
        }
        else
        {
            leadersButton.interactable = true;
        }
    }
}
