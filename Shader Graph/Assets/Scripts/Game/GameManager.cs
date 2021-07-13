using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private void Awake()
    {
        instance = this;
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

}
