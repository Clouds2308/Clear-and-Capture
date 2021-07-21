using System;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    //[Header("Player")]
    //[SerializeField] private Player _player;
    //[SerializeField] private Text _playerHealthText;

    //[Header("Gun")]
    //[SerializeField] private Weapon _gun;
    //[SerializeField] private Text _bulletsInMag;
    //[SerializeField] private Text _maxBullets;

    //[Header("Menu")]
    //[SerializeField] private GameObject _pauseMenuPanel;
    //[SerializeField] private Toggle[] _screenResToggles;
    //[SerializeField] private Toggle _fullScreenToggle;
    //[SerializeField] private int[] _screenWidths;
    //private bool _isGamePaused = false;
    //private int _activeScreenResIndex;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        //#region EventHandling
        //GameEvents.current.onPlayerHeatlhChange += HandlePlayerHealthChange;
        //#endregion

        //_activeScreenResIndex = PlayerPrefs.GetInt("screen res index");
        //bool isFullScreen = (PlayerPrefs.GetInt("fullscreen") == 1) ? true : false;

        //for(int i=0;i<_screenResToggles.Length;i++)
        //{
        //    _screenResToggles[i].isOn = i == _activeScreenResIndex;
        //}

        //_fullScreenToggle.isOn = isFullScreen;
    }

    //private void HandlePlayerHealthChange()
    //{
    //    _playerHealthText.text = _player.CurrentHealth.ToString();
    //}
 
    #region MenuHandler
    //public void PauseGame()
    //{
    //    _pauseMenuPanel.SetActive(true);
    //    Time.timeScale = 0;
    //    _isGamePaused = true;

    //    Cursor.visible = true;
    //    Cursor.lockState = CursorLockMode.None;
    //}
    //public void ResumeGame()
    //{
    //    _pauseMenuPanel.SetActive(false);
    //    Time.timeScale = 1f;
    //    _isGamePaused = false;

    //    Cursor.visible = false;
    //    Cursor.lockState = CursorLockMode.Locked;
    //}
    //public void GoToMainMenu()
    //{
    //}
    
    //public void SetScreenResolution(int i)
    //{
    //    if(_screenResToggles[i].isOn)
    //    {
    //        float _aspectRatio = 16 / 9f;
    //        Screen.SetResolution(_screenWidths[i], (int)(_screenWidths[i] / _aspectRatio), false);
    //        PlayerPrefs.SetInt("screen res index", _activeScreenResIndex);
    //        PlayerPrefs.Save();
    //    }
    //}
    //public void SetFullScreen(bool isFullScreen)
    //{
    //    for (int i = 0; i < _screenResToggles.Length; i++)
    //    {
    //        _screenResToggles[i].interactable = !isFullScreen;
    //    }

    //    if(isFullScreen)
    //    {
    //        Resolution[] allResolutions = Screen.resolutions;
    //        Resolution maxResolution = allResolutions[allResolutions.Length - 1];
    //        Screen.SetResolution(maxResolution.width, maxResolution.height, true);
    //    }
    //    else
    //    {
    //        SetScreenResolution(_activeScreenResIndex);
    //    }

    //    PlayerPrefs.SetInt("fullscreen", ((isFullScreen) ? 1 : 0));
    //    PlayerPrefs.Save();
    //}
       
    #endregion

    private void Update()
    {
        //_bulletsInMag.text = _gun.BulletInMag.ToString();
        //_maxBullets.text = _gun.MaxBulletsInMag.ToString();

        //if(Input.GetKeyDown(KeyCode.Escape))
        //{
        //    if (_isGamePaused != true)
        //        PauseGame();            
        //}
    }

}