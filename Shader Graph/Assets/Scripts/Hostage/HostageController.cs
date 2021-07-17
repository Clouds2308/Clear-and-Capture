using UnityEngine;
using System.Collections;

public class HostageController : MonoBehaviour
{

    private Animator _hostageAnimator;
    private Hostage_PathAI _followAI;

    private bool _canInteract;
    public bool CanInteract
    {
        get { return _canInteract; }
        set
        {
            _canInteract = value;
        }
    }

    public bool _canFollow;    


    private void Start()
    {
        _hostageAnimator = GetComponent<Animator>();
        GameEvents.current.onHostageFree += OnHostageFree;
        _followAI = GetComponent<Hostage_PathAI>();
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.transform.name == "Player")
            GameEvents.current.HostageColliderEnter();            
    }

    private void OnCollisionExit(Collision collision)
    {
        GameEvents.current.HostageColliderExit();
    }

    void OnHostageFree()
    {
        _hostageAnimator.SetTrigger("isStand");
        StartCoroutine(walkDelay(4f));
    }
          
    IEnumerator walkDelay(float time)
    {
        yield return new WaitForSeconds(time);
        _canFollow = true;
    }

    private void Update()
    {
        if(_canFollow == true)
            _followAI.enabled = true;
    }

}