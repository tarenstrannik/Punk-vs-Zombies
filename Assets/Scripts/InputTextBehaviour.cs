using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class InputTextBehaviour : MonoBehaviour
{
    [SerializeField] private Button okButton;
    private TMP_InputField inputText;

    private void OnEnable()
    {
        inputText = GetComponent<TMP_InputField>();
        if(MainManager.Instance.CurPlayer != "")
        {
            inputText.text=MainManager.Instance.CurPlayer;
        }
    }
    public void TextChanged()
    {
        if(inputText.text != "")
        {
            okButton.interactable = true;
            MainManager.Instance.TempCurPlayerSet(inputText.text);
        }
        else
        {
            okButton.interactable = false;
        }
    }
}
