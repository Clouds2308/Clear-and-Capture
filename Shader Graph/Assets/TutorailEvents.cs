using UnityEngine;

public class TutorailEvents : MonoBehaviour
{
    public static TutorailEvents current;

    private void Awake()
    {
        current = this;
    }
}
