using UnityEngine;

public class NormalDoor : MonoBehaviour
{
    private Animator _normalDoorAnimator;
    private bool _canOpen = true;

    public AudioClip DoorOpenAudio;

    private void Start()
    {
        _normalDoorAnimator = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") && _canOpen == true)
        {                        
            _normalDoorAnimator.SetTrigger("IsOpen");
            AudioManager.instance.PlaySound(DoorOpenAudio, transform.position);
            _canOpen = false;
        }
    }
}
