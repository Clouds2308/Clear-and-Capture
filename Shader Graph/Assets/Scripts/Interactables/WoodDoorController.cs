using UnityEngine;

public class WoodDoorController : MonoBehaviour
{
    [SerializeField] private AudioSource _doorOpenAudio;
    private Animator _woodDoorAnimator;
    private bool _hasOpened = false;

    private void Start()
    {
        _woodDoorAnimator = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        while (!_hasOpened)
        {
            if (other.CompareTag("Player"))
            {
                _doorOpenAudio.Play();
                _woodDoorAnimator.SetTrigger("IsOpen");
                _hasOpened = true;
            }
        }
    }
}
