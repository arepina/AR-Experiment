using System;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    public GameObject MainMenuPanel;

    public Button[] ButtonsDependentOnTrackPosition;
    public Button RestoreLastSessionButton;

    public GameObject InfoMessage;

    public InputField IMSubjectCodeInputField;
    public InputField ESSubjectCodeInputField;
    public ToggleGroup IntensityToggleGroup;
    public ToggleGroup OrderToggleGroup;

    void Start()
    {
        // Check whether everything in its place 
        if (MainMenuPanel == null)
        {
            Debug.LogError("Error: The MainMenuPanel field can't be left unassigned. Disabling the script");
            enabled = false;
            return;
        }

        if (ButtonsDependentOnTrackPosition.Length == 0)
        {
            Debug.LogError("Error: The 'Intencities' array can't be empty. Disabling the script");
            enabled = false;
            return;
        }

        if (RestoreLastSessionButton == null)
        {
            Debug.LogError("Error: The RestoreLastSessionButton field can't be left unassigned. Disabling the script");
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

        if (IntensityToggleGroup == null)
        {
            Debug.LogError("Error: The IntensityToggleGroup field can't be left unassigned. Disabling the script");
            enabled = false;
            return;
        }

        if (OrderToggleGroup == null)
        {
            Debug.LogError("Error: The OrderToggleGroup field can't be left unassigned. Disabling the script");
            enabled = false;
            return;
        }
    }

    public void StartNewSession()
    {
        try
        {
            int subjectNo = Int32.Parse(ESSubjectCodeInputField.text);
            IEnumerator<Toggle> enumerator = IntensityToggleGroup.ActiveToggles().GetEnumerator();
            enumerator.MoveNext();
            float intensityOfObstacleAppearance = float.Parse(enumerator.Current.name, CultureInfo.InvariantCulture.NumberFormat);
            enumerator = OrderToggleGroup.ActiveToggles().GetEnumerator();
            enumerator.MoveNext();
            string orderOfDesignPresentation = enumerator.Current.name;

            PlayerPrefs.SetInt("SubjectNo", subjectNo);
            PlayerPrefs.SetFloat("Intensity", intensityOfObstacleAppearance);
            PlayerPrefs.SetString("ConditionsOrder", orderOfDesignPresentation);

            PlayerPrefs.SetInt("ConditionNo", 0);
            PlayerPrefs.SetInt("TrialNo", 1);
        }
        catch (Exception e)
        {
            Debug.LogException(e);
        }

        SceneManager.LoadSceneAsync("ExperimentSession");
    }

    public void MeasureIntensity()
    {
        int subjectNo = Int32.Parse(IMSubjectCodeInputField.text);
        PlayerPrefs.SetInt("SubjectNo", subjectNo);

        PlayerPrefs.DeleteKey("Intensity");
        PlayerPrefs.DeleteKey("ConditionsOrder");
        PlayerPrefs.DeleteKey("ConditionNo");
        PlayerPrefs.DeleteKey("TrialNo");

        SceneManager.LoadSceneAsync("IntensityMeasurement");
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
