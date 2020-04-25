using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    public GameObject MainMenuPanel;
    public GameObject InfoMessage;
    public InputField IMSubjectCodeInputField;
    public Text headerText;

    void Start()
    {
        // Check whether everything in its place 
        if (MainMenuPanel == null)
        {
            Debug.LogError("Error: The MainMenuPanel field can't be left unassigned. Disabling the script");
            enabled = false;
            return;
        }

        if (InfoMessage == null)
        {
            Debug.LogError("Error: The InfoMessage field can't be left unassigned. Disabling the script");
            enabled = false;
            return;
        }

        if (IMSubjectCodeInputField == null)
        {
            Debug.LogError("Error: The IMSubjectCodeInputField field can't be left unassigned. Disabling the script");
            enabled = false;
            return;
        }
    }

    public void StartNewSession()
    {
        try
        {
            //todo
            //int subjectNo = Int32.Parse(ESSubjectCodeInputField.text);
            //PlayerPrefs.SetInt("SubjectNo", subjectNo);
            //PlayerPrefs.SetInt("ConditionNo", 0);
            //PlayerPrefs.SetInt("TrialNo", 1);
            string text = headerText.text.Split(':')[1].Trim().Replace("\"", "");
            switch (text)
            {
                case "Перед пользователем - мобильные":
                    {
                        StartInFrontOfMobile();
                        return;
                    }
                case "Перед пользователем - стикеры":
                    {
                        StartInFrontOfStickers();
                        return;
                    }       
                case "Вокруг пользователя - мобильные":
                    {
                        StartAroundMobile();
                        return;
                    }
                case "Вокруг пользователя - стикеры":
                    {
                        StartAroundStickers();
                        return;
                    }
                case "Невидимые - волны":
                    {
                        StartHiddenWaves();
                        return;
                    }
                default: return;
            }
        }
        catch (Exception e)
        {
            Debug.LogException(e);
        }
    }

    public void StartInFrontOfMobile()
    {
        SceneManager.LoadSceneAsync("InFrontOfMobile");
    }

    public void StartInFrontOfStickers()
    {
        SceneManager.LoadSceneAsync("InFrontOfStickers");
    }

    public void StartAroundMobile()
    {
        SceneManager.LoadSceneAsync("AroundMobile");
    }

    public void StartAroundStickers()
    {
        SceneManager.LoadSceneAsync("AroundStickers");
    }

    public void StartHiddenWaves()
    {
        SceneManager.LoadSceneAsync("HiddenWaves");
    }

    public void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
