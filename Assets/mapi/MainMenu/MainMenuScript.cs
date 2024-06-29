using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Localization.Settings;
using UnityEngine.UI;
using TMPro;
//using DG.Tweening;

public class MainMenuScript : MonoBehaviour
{
    [SerializeField] private Button _btnDelSave;

    [Space]
    [Header("Main Buttons")]
    [SerializeField] private Button _btnFirst;
    [SerializeField] private Button _btnSecond;
    [SerializeField] private Button _btnSettings;
    [SerializeField] private Button _btnQuit;

    [Space]
    [Header("Settings Buttons")]
    [SerializeField] private Button _btnLanguages;
    [SerializeField] private Button _btnChangeMusic;
    [SerializeField] private Button _btnInfo;

    [Space]
    [Header("Music Buttons")]
    [SerializeField] private Button[] _btnMusic;

    [Space]
    [Header("Language")]
    [SerializeField] private TMP_Dropdown dropdown;

    [Space]
    [Header("Windows")]
    [SerializeField] private GameObject SettingsView;
    [SerializeField] private GameObject LanguagesView;
    [SerializeField] public GameObject MusicView;
    [SerializeField] public GameObject InfoView;

    int WindowInt = 0;
    bool SettingsBool = true;
    string levelToLoad;
    CanvasGroup SettingsFade;
    CanvasGroup LanguagesFade;
    CanvasGroup MusicFade;
    CanvasGroup InfoFade;
    float expandFadeDuration = 0.7f;
    float OtherExpandFadeDuration = 0.2f;

    MenuMusicScript _menuMusicScript;

    private void Awake()
    {
        _menuMusicScript = FindObjectOfType<MenuMusicScript>();

        SetMusic(SavePrefScript.Load(SavePrefScript.PrefTypes.Music));

        SettingsFade = SettingsView.GetComponent<CanvasGroup>();
        LanguagesFade = LanguagesView.GetComponent<CanvasGroup>();
        MusicFade = MusicView.GetComponent<CanvasGroup>();
        InfoFade = InfoView.GetComponent<CanvasGroup>();

        _btnFirst.onClick.AddListener(LoadLevelFirst);
        _btnSecond.onClick.AddListener(LoadLevelSecond);
        _btnSettings.onClick.AddListener(SettingsOpen);
        _btnQuit.onClick.AddListener(QuitGame);

        _btnDelSave.onClick.AddListener(DelSave);

        _btnLanguages.onClick.AddListener(LanguagesOpen);
        _btnChangeMusic.onClick.AddListener(MusicOpen);
        _btnInfo.onClick.AddListener(InfoOpen);


        _btnMusic[0].onClick.AddListener(() => SetMusic(0));
        _btnMusic[1].onClick.AddListener(() => SetMusic(1));
        _btnMusic[2].onClick.AddListener(() => SetMusic(2));
        _btnMusic[3].onClick.AddListener(() => SetMusic(3));
        _btnMusic[4].onClick.AddListener(() => SetMusic(4));
        _btnMusic[5].onClick.AddListener(() => SetMusic(5));
        _btnMusic[6].onClick.AddListener(() => SetMusic(6));
        _btnMusic[7].onClick.AddListener(() => SetMusic(7));
    }

    private void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quit");
    }

    private void SettingsOpen()
    {
        if (SettingsBool != false)
        {
            //SettingsFade.DOFade(1f, expandFadeDuration).From(0f);
            SettingsView.gameObject.SetActive(true);
            SettingsBool = false;
        }
        else
        {
            StartCoroutine(FadeSettingsOpen());
            //SettingsFade.DOFade(0, expandFadeDuration).From(1f);
            //LanguagesFade.DOFade(0, OtherExpandFadeDuration).From(1f);
            //MusicFade.DOFade(0, OtherExpandFadeDuration).From(1f);
            //InfoFade.DOFade(0, OtherExpandFadeDuration).From(1f);
            SettingsBool = true;

            WindowInt = 0;
            StopCoroutine(FadeSettingsOpen());
        }
    }
    IEnumerator FadeSettingsOpen()
    {
        _btnSettings.onClick.RemoveListener(SettingsOpen);
        yield return new WaitForSeconds(1f);
        SettingsView.gameObject.SetActive(false);
        LanguagesView.gameObject.SetActive(false);
        MusicView.gameObject.SetActive(false);
        InfoView.gameObject.SetActive(false);
        _btnSettings.onClick.AddListener(SettingsOpen);
    }

    private void LanguagesOpen()
    {
        if (WindowInt != 3)
        {
            //LanguagesFade.DOFade(1f, expandFadeDuration).From(0f);
            LanguagesView.gameObject.SetActive(true);
            MusicView.gameObject.SetActive(false);
            InfoView.gameObject.SetActive(false);
            WindowInt = 3;
        }
        else
        {
            StartCoroutine(FadeLanguagesOpenn());
            //LanguagesFade.DOFade(0, expandFadeDuration).From(1f);
            WindowInt = 0;
            StopCoroutine(FadeLanguagesOpenn());
        }
    }
    IEnumerator FadeLanguagesOpenn()
    {
        _btnLanguages.onClick.RemoveListener(LanguagesOpen);
        yield return new WaitForSeconds(1f);
        LanguagesView.gameObject.SetActive(false);
        MusicView.gameObject.SetActive(false);
        InfoView.gameObject.SetActive(false);
        _btnLanguages.onClick.AddListener(LanguagesOpen);
    }

    private void MusicOpen()
    {
        if (WindowInt != 4)
        {
            //MusicFade.DOFade(1f, expandFadeDuration).From(0f);
            LanguagesView.gameObject.SetActive(false);
            MusicView.gameObject.SetActive(true);
            InfoView.gameObject.SetActive(false);
            WindowInt = 4;
        }
        else
        {
            StartCoroutine(FadeMusicOpen());
            //MusicFade.DOFade(0, expandFadeDuration).From(1f);
            WindowInt = 0;
            StopCoroutine(FadeMusicOpen());
        }
    }
    IEnumerator FadeMusicOpen()
    {
        _btnChangeMusic.onClick.RemoveListener(MusicOpen);
        yield return new WaitForSeconds(1f);
        LanguagesView.gameObject.SetActive(false);
        MusicView.gameObject.SetActive(false);
        InfoView.gameObject.SetActive(false);
        _btnChangeMusic.onClick.AddListener(MusicOpen);
    }

    private void InfoOpen()
    {
        if (WindowInt != 5)
        {
            //InfoFade.DOFade(1f, expandFadeDuration).From(0f);
            LanguagesView.gameObject.SetActive(false);
            MusicView.gameObject.SetActive(false);
            InfoView.gameObject.SetActive(true);
            WindowInt = 5;
        }
        else
        {
            StartCoroutine(FadeInfoOpen());
            //InfoFade.DOFade(0, expandFadeDuration).From(1f);
            WindowInt = 0;
            StopCoroutine(FadeInfoOpen());
        }
    }
    IEnumerator FadeInfoOpen()
    {
        _btnInfo.onClick.RemoveListener(InfoOpen);
        yield return new WaitForSeconds(1f);
        LanguagesView.gameObject.SetActive(false);
        MusicView.gameObject.SetActive(false);
        InfoView.gameObject.SetActive(false);
        _btnInfo.onClick.AddListener(InfoOpen);
    }

    private void LoadLevelFirst()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    private void LoadLevelSecond()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
    }


    private void SetMusic(int setMusic)
    {
        SavePrefScript.Save(SavePrefScript.PrefTypes.Music, setMusic);
        if (_menuMusicScript._int != setMusic)
        {
            _menuMusicScript._int = setMusic;
            _menuMusicScript.MakeMusic(setMusic);

            _btnMusic[0].GetComponent<Image>().color = setMusic == 0 ? Color.gray : Color.white;
            _btnMusic[1].GetComponent<Image>().color = setMusic == 1 ? Color.gray : Color.white;
            _btnMusic[2].GetComponent<Image>().color = setMusic == 2 ? Color.gray : Color.white;
            _btnMusic[3].GetComponent<Image>().color = setMusic == 3 ? Color.gray : Color.white;
            _btnMusic[4].GetComponent<Image>().color = setMusic == 4 ? Color.gray : Color.white;
            _btnMusic[5].GetComponent<Image>().color = setMusic == 5 ? Color.gray : Color.white;
            _btnMusic[6].GetComponent<Image>().color = setMusic == 6 ? Color.gray : Color.white;
            _btnMusic[7].GetComponent<Image>().color = setMusic == 7 ? Color.gray : Color.white;
        }
    }

    private void DelSave()
    {
        PlayerPrefs.DeleteAll();
        Debug.Log("Save deleted");
    }

    IEnumerator Start()
    {
        // Wait for the localization system to initialize
        yield return LocalizationSettings.InitializationOperation;

        // Generate list of available Locales
        var options = new List<TMP_Dropdown.OptionData>();
        int selected = 0;
        for (int i = 0; i < LocalizationSettings.AvailableLocales.Locales.Count; ++i)
        {
            var locale = LocalizationSettings.AvailableLocales.Locales[i];
            if (LocalizationSettings.SelectedLocale == locale)
                selected = i;
            options.Add(new TMP_Dropdown.OptionData(locale.name));
        }
        dropdown.options = options;

        dropdown.value = selected;
        dropdown.onValueChanged.AddListener(LocaleSelected);
    }

    static void LocaleSelected(int index)
    {
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[index];
    }
}