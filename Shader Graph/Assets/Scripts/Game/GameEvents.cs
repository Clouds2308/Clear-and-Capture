using UnityEngine;
using UnityEngine.Events;
using System;

public class GameEvents : MonoBehaviour
{
    public static GameEvents current;

    private void Awake()
    {
        current = this;
    }

    #region InteractableEvents         

    public event Action<int> onJammerTriggerEnter;
    public void JammerTriggerEnter(int id)
    {
        onJammerTriggerEnter?.Invoke(id);
    }

    public event Action<int> onJammerTriggeExit;
    public void JammerTriggerExit(int id)
    {
        onJammerTriggeExit?.Invoke(id);
    }

#endregion

    #region PlayerEvents

    public event Action onPlayerCaptured; 
    public void PlayerCapture()
    {
        onPlayerCaptured?.Invoke();
    }
    public event Action onPlayerDeath;
    public void PlayerDead()
    {
        onPlayerDeath?.Invoke();
    }

    public event Action onPlayerHeatlhChange;
    public void PlayerHealthChange()
    {
        onPlayerHeatlhChange?.Invoke();
    }

    #endregion

    #region HostageEvents

    public event Action onHostageFree;
    public void HostageFree()
    {
        onHostageFree?.Invoke();
    }
    public event Action onHostageColliderEnter;
    public void HostageColliderEnter()
    {
        onHostageColliderEnter?.Invoke();
    }
    public event Action onHostageColliderExit;
    public void HostageColliderExit()
    {
        onHostageColliderExit?.Invoke();
    }

    #endregion

    public event Action onRescueZoneEnter;
    public void RescueZoneEnter()
    {
        onRescueZoneEnter?.Invoke();
    }
        
}
