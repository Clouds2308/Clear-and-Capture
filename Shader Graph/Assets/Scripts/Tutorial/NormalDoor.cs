using UnityEngine;

public class NormalDoor : MonoBehaviour
{
    private Animator _normalDoorAnimator;
    private bool _canOpen = true;

    private void Start()
    {
        _normalDoorAnimator = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") && _canOpen == true)
        {                        
            _normalDoorAnimator.SetTrigger("IsOpen");
            _canOpen = false;
        }
    }
}
