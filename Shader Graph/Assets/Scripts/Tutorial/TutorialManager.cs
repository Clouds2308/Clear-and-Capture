using UnityEngine;
using System.Collections;
using TMPro;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] private GameObject _drone;

    private void Awake()
    {
        _drone.SetActive(true);
        StartCoroutine(DroneReference());
    }   

    IEnumerator DroneReference()
    {
        yield return new WaitForSeconds(0.01f);
        _drone.SetActive(false);
    }       
        
}
