using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitTutorial : MonoBehaviour
{
    [SerializeField] private GameObject _exitPanel;
    private Weapon _weapon;

    private void Start()
    {
        _weapon = FindObjectOfType<Weapon>().GetComponent<Weapon>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.CompareTag("Player"))
        {
            _exitPanel.SetActive(true);            
            TutorialManager.instance.UnLockCursor();
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        _exitPanel.SetActive(false);
        TutorialManager.instance.LockCursor();
    }

    public void RestartTutorial()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

}