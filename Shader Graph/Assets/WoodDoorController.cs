using UnityEngine;

public class WoodDoorController : MonoBehaviour
{
    private Animator _woodDoorAnimator;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            if(Input.GetKeyDown(KeyCode.F))
            {
                _woodDoorAnimator.SetTrigger("IsOpen");
            }
        }
    }
}
