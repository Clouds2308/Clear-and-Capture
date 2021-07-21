using UnityEngine;
using System;

public class TutorialEvents : MonoBehaviour
{
    public static TutorialEvents current;

    private void Awake()
    {
        current = this;
    }

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
}
