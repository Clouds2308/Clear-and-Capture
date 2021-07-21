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
    }
    IEnumerator DroneReference()
    {
        yield return new WaitForSeconds(0.01f);
        _drone.SetActive(false);
    }

}
