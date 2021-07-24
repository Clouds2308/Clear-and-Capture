using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] private GameObject _drone;
    public static TutorialManager instance;

    private void Awake()
    {
        instance = this;

        _drone.SetActive(true);
        StartCoroutine(DroneReference());
        LockCursor();
    }
    IEnumerator DroneReference()
    {
        yield return new WaitForSeconds(0.01f);
        _drone.SetActive(false);
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

}
