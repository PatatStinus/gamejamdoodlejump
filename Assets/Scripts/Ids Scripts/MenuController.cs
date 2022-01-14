using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuController : MonoBehaviour
{
    [Header("Volume setting")]
    [SerializeField] private TMP_Text volumeTextValue = null;
    [SerializeField] private Slider volumeSlider = null;
    [SerializeField] private float defaultVolume = 1.0f;

    [Header("GamePlay Settings")]
    [SerializeField] private TMP_Text MouseSenTextvalue = null;
    [SerializeField] private Slider MouseSenSlider = null;
    [SerializeField] private int defaultSen = 4;
    public int mainMouseSen = 4;

    [Header("Toggle Settings")]
    [SerializeField] private Toggle invertYToggle = null;

    [Header("Graphics Settings")]
    [SerializeField] private Slider brightnessSlider = null;
    [SerializeField] private TMP_Text brightnessTextValue = null;
    [SerializeField] private float defaultBrightness = 1;

    [Space(10)]
    [SerializeField] private TMP_Dropdown qualityDropdown;
    [SerializeField] private Toggle fullScreenToggle;

    private int _qaulityLevel;
    private bool _isFullScreen;
    private float _brightnessLevel;

    [Header("confirmation")]
    [SerializeField] private GameObject comfirmationPrompt = null;

    [Header("Levels To Load")]
    public string _newGameLevel;
    private string LevelToLoad;
    [SerializeField] private GameObject NoSavedGameDialog = null;

    [Header("Resolution Dropdowns")]
    public TMP_Dropdown resolutionDropDown;
    Resolution[] resolutions;

    private void Start()
    {
        resolutions = Screen.resolutions;

        resolutionDropDown.ClearOptions();

        List<string> options = new List<string>();
        QualitySettings.vSyncCount = 0;

        int currentResolutionIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + "x" + resolutions[i].height + "@" + resolutions[i].refreshRate + "hz";
            options.Add(option);
            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }
        resolutionDropDown.AddOptions(options);
        resolutionDropDown.value = currentResolutionIndex;
        resolutionDropDown.RefreshShownValue();
    }

    public void SetResolution(int resolutionIndex) 
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void NewGameDialogYes() 
    {
        SceneManager.LoadScene(_newGameLevel);
    }

    public void LoadGameDialogYes() 
    {
         if (PlayerPrefs.HasKey("SavedLevel"))
         {
             LevelToLoad = PlayerPrefs.GetString("SavedLevel");
             SceneManager.LoadScene(LevelToLoad);
         }
         else 
         {
            NoSavedGameDialog.SetActive(true); 
         }
    }

    public void ExitButton() 
    {
        Application.Quit();
    }

    public void SetVolume(float volume) 
    {
        AudioListener.volume = volume;
        volumeTextValue.text = volume.ToString("0.0");
    }

    public void VolumeApply() 
    {
        PlayerPrefs.SetFloat("masterVolume", AudioListener.volume);
        StartCoroutine(ComfirmationBox());
    }

    public void SetMouseSen(float sensitivity) 
    {
        mainMouseSen = Mathf.RoundToInt(sensitivity);
        MouseSenTextvalue.text = sensitivity.ToString("0");
    }

    public void GameplayApply() 
    {
        if (invertYToggle.isOn) 
        {
            PlayerPrefs.SetInt("masterInvertY", 1);
        }
        else
        {
            PlayerPrefs.SetInt("masterInvertY", 0);
        }
        PlayerPrefs.SetFloat("master", mainMouseSen);
        StartCoroutine(ComfirmationBox());
    }

    public void SetBrightness(float brightness) 
    {
        _brightnessLevel = brightness;
        brightnessTextValue.text = brightness.ToString("0");
    }

    public void SetFullScreen(bool isFullscreen) 
    {
        _isFullScreen = isFullscreen;
    }

    public void SetQaulity(int qaulityIndex) 
    {
        _qaulityLevel = qaulityIndex;
    }

    public void GraphicsApply()
    {
        PlayerPrefs.SetFloat("masterBrightness", _brightnessLevel);

        PlayerPrefs.SetInt("masterQaulity", _qaulityLevel);
        QualitySettings.SetQualityLevel(_qaulityLevel);

        PlayerPrefs.SetInt("masterFullscreen", (_isFullScreen ? 1 : 0));
        Screen.fullScreen = _isFullScreen;

        StartCoroutine(ComfirmationBox());
         
    }

    public void ResetBottun(string MenuType)
    {
        if (MenuType == "Graphics") 
        {
            brightnessSlider.value = defaultBrightness;
            brightnessTextValue.text = defaultBrightness.ToString("0");

            qualityDropdown.value = 1;
            QualitySettings.SetQualityLevel(1);

            fullScreenToggle.isOn = false;
            Screen.fullScreen = false;

            Resolution currentResolution = Screen.currentResolution;
            Screen.SetResolution(currentResolution.width, currentResolution.height, Screen.fullScreen);
            resolutionDropDown.value = resolutions.Length;
            GraphicsApply();
        }


        if (MenuType == "Audio") 
        {
            AudioListener.volume = defaultVolume;
            volumeSlider.value = defaultVolume;
            volumeTextValue.text = defaultVolume.ToString("0.0");
            VolumeApply();
        }

        if (MenuType == "Gameplay") 
        {
            MouseSenTextvalue.text = defaultSen.ToString("0");
            MouseSenSlider.value = defaultSen;
            mainMouseSen = defaultSen;
            GameplayApply();
        }
    }


    public IEnumerator ComfirmationBox() 
    {
        comfirmationPrompt.SetActive(true);
        yield return new WaitForSeconds(2);
        comfirmationPrompt.SetActive(false);
    }

}
