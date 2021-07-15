using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject _drone;

    private void Awake()
    {
        instance = this;
        _drone.SetActive(true);
        StartCoroutine(DroneReference());
    }

    private void Start()
    {
        GameEvents.current.onPlayerCaptured += HandlePlayerCapture;
        GameEvents.current.onPlayerDeath += HandlePlayerDeath;
        GameEvents.current.onRescueZoneEnter += HandleRescueZone;
    }


    private void HandlePlayerDeath()
    {
        Debug.Log("Player Died");
    }

    private void HandlePlayerCapture()
    {
        Debug.Log("Player Captured");
    }
    private void HandleRescueZone()
    {
        Debug.Log("Hostage Rescued");
    }

    IEnumerator DroneReference()
    {
        yield return new WaitForSeconds(0.01f);
        _drone.SetActive(false);
    }

}
