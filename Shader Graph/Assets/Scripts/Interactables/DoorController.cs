using UnityEngine;
using System.Collections;

public class DoorController : MonoBehaviour
{
    [SerializeField] private Animator _doorAnimator;
    private bool doorOpen = false;

    private void Start()
    {
        _doorAnimator = GetComponent<Animator>();
    }       

    private void Update()
    {
        if (transform.childCount == 4 && !doorOpen)
        {
            StartCoroutine(DoorOpen());
        }
    }

    IEnumerator DoorOpen()
    {
        doorOpen = true;
        yield return new WaitForSeconds(1f);        
        _doorAnimator.SetTrigger("IsOpen");
    }
    
}
