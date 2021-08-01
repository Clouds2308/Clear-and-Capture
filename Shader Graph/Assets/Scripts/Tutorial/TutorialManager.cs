using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] private GameObject _player;
    [SerializeField] private GameObject _drone;
    [SerializeField] private Weapon _weapon;
    public static TutorialManager instance;

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI _currBulletsText;
    [SerializeField] private TextMeshProUGUI _maxBulletsText;
    [SerializeField] private KeyCode _pauseKey;
    [SerializeField] private bool _isPaused;
    [SerializeField] private GameObject _pausePanel;
    [SerializeField] private GameObject _playerUIPanel;
    [SerializeField] private GameObject _entryPanel;
    [SerializeField] private Slider[] _volumeSliders;
    [SerializeField] private Slider _cameraSensSlider;

    private void Awake()
    {
        instance = this;

        _drone.SetActive(true);
        StartCoroutine(DroneReference());
        StartCoroutine(DestroyEntryPanel());
        LockCursor();
        _weapon = FindObjectOfType<Weapon>().GetComponent<Weapon>();
        _player = GameObject.FindGameObjectWithTag("Player");
        _cameraSensSlider.value = 0.5f;
    }

    private void Start()
    {
        _volumeSliders[0].value = AudioManager.instance.masterVolumePercent;
        _volumeSliders[1].value = AudioManager.instance.musicVolumePercent;
        _volumeSliders[2].value = AudioManager.instance.sfxVolumePercent;
    }

    IEnumerator DroneReference()
    {
        yield return new WaitForSeconds(0.01f);
        _drone.SetActive(false);
    }

    IEnumerator DestroyEntryPanel()
    {
        yield return new WaitForSeconds(2f);
        _entryPanel.SetActive(false);
    }

    public void LockCursor()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void UnLockCursor()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    private void Update()
    {
        _currBulletsText.text = _weapon.BulletInMag.ToString();
        _maxBulletsText.text = _weapon.MaxBulletsInMag.ToString();

        if(Input.GetKeyDown(_pauseKey) && !_isPaused)
        {
            Pause();
        }

        _player.GetComponent<PlayerMovement>().CameraSensitivity = _cameraSensSlider.value;
    }

    private void Pause()
    {
        _playerUIPanel.SetActive(false);
        _pausePanel.SetActive(true);        
        _isPaused = true;
        Time.timeScale = 0f;
        UnLockCursor();
        _weapon.enabled = false;
    }

    public void Resume()
    {
        _playerUIPanel.SetActive(true);
        _pausePanel.SetActive(false);
        _isPaused = false;
        Time.timeScale = 1f;
        LockCursor();
        _weapon.enabled = true;
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void SetMasterVolume(float value)
    {
        AudioManager.instance.SetVoulme(value, AudioManager.AudioChannel.Master);
    }

    public void SetMusicVolume(float value)
    {
        AudioManager.instance.SetVoulme(value, AudioManager.AudioChannel.Music);
    }

    public void SetSfxVolume(float value)
    {
        AudioManager.instance.SetVoulme(value, AudioManager.AudioChannel.Sfx);
    }

}
