using UnityEngine;
using System.Collections;

public class HostageController : MonoBehaviour
{

    [SerializeField] private Hostage_PathAI _followAI;
    [SerializeField] private Animator _hostageAnimator;

    private bool _canInteract;
    public bool CanInteract
    {
        get { return _canInteract; }
        set
        {
            _canInteract = value;
        }
    }

    private bool _canFollow;    


    private void Start()
    {
        _hostageAnimator = GetComponent<Animator>();
        GameEvents.current.onHostageFree += OnHostageFree;
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