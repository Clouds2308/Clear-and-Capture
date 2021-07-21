using UnityEngine;
using System.Collections;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] private GameObject _drone;
    public static TutorialManager instance;

    private void Awake()
    {
        _drone.SetActive(true);
        StartCoroutine(DroneReference());

        instance = this;
    }   

    IEnumerator DroneReference()
    {
        yield return new WaitForSeconds(0.01f);
        _drone.SetActive(false);
    }       
        
}
