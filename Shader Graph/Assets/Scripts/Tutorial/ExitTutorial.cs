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

    private void OnCollisionStay(Collision collision)
    {
        if(collision.transform.CompareTag("Player"))
        {
            _exitPanel.SetActive(true);
            _weapon.enabled = false;         
            TutorialManager.instance.UnLockCursor();
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        _exitPanel.SetActive(false);
        _weapon.enabled = true;
        TutorialManager.instance.LockCursor();
    }

    public void RestartTutorial()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }    

}