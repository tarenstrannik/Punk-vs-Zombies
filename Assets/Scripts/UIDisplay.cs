using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Data.Common;
using UnityEngine.SceneManagement;
using UnityEditor;

[RequireComponent(typeof(AudioSource))] 
public class UIDisplay : MonoBehaviour
{
    
    [SerializeField] private float actionDelay = 0.5f;
    [SerializeField] private AudioClip buttonClick;

    private AudioSource audioSource;

    protected GameObject prevMenu=null;
    [SerializeField] protected GameObject curMenu;

    [SerializeField] protected Button quitButton;

    UnityEvent escapeEvent = new UnityEvent();
    protected virtual void Start()
    {
        
        escapeEvent.AddListener(() => OpenMenu(null));
        audioSource = GetComponent<AudioSource>();
    }
    
    protected virtual void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ActionWithDelay(escapeEvent);
        }
    }

    public void ButtonActionWithDelay(Button button)
    {
        ActionWithDelay(button.GetComponent<ButtonOptions>().ButtonAction);
    }

    private void ActionWithDelay(UnityEvent action)
    {
        PlayButtonSound();
        StartCoroutine(WaitForAction(action));
    }

    IEnumerator WaitForAction(UnityEvent unityEvent)
    {
        yield return new WaitForSecondsRealtime(actionDelay);
        unityEvent.Invoke();
    }
    private void PlayButtonSound()
    {
        if (buttonClick != null)
            audioSource.PlayOneShot(buttonClick);
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.ExitPlaymode();

#else
#if UNITY_WEBGL
        SceneManager.LoadScene(0);
#else
        Application.Quit();
#endif
#endif
    }


    public virtual void OpenMenu(UnityEngine.Object obj)
    {

        GameObject target = obj as GameObject;

        if (target != null && curMenu!=null)
        {

            prevMenu = curMenu;
            curMenu = target;
            prevMenu.SetActive(false);
            curMenu.SetActive(true);
        }
        else if(prevMenu!=null && curMenu!=null)
        {

            curMenu.SetActive(false);
            prevMenu.SetActive(true);
            curMenu = prevMenu;
            prevMenu = null;
        }
        else if(target==null && prevMenu==null && curMenu != null && !MainManager.Instance.isGameActive)
        {
            quitButton.GetComponent<ButtonOptions>().ButtonAction.Invoke();
        }
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }


    public void OpenNewGameMenu(UnityEngine.Object obj)
    {
        if (MainManager.Instance.MaxWave == 1)
        {
            SceneManager.LoadScene(MainManager.Instance.GameSceneName);
        }
        else
        {
            OpenMenu(obj);
        }
    }
}
