using UnityEngine;
using System;

public class TutorialEvents : MonoBehaviour
{
    public static TutorialEvents current;

    private void Awake()
    {
        current = this;
    }     
}
