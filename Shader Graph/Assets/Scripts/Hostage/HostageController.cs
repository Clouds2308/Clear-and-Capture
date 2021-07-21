using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HostageController : MonoBehaviour
{

    [SerializeField] private GameObject _crosshairPanel;
    [SerializeField] private GameObject _hostagePanel;
    [SerializeField] private Image _radialImage;

    private Animator _hostageAnimator;
    private Hostage_PathAI _followAI;

    private bool _shouldUpdate;
    private float _holdTime = 1.0f;
    private float _maxHoldTime = 1.0f;

    private bool _canInteract;
    public bool _canFollow;    


    private void Start()
    {
        _hostageAnimator = GetComponent<Animator>();
        _followAI = GetComponent<Hostage_PathAI>();
        _radialImage.enabled = false;
        _canInteract = true;
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            _hostagePanel.SetActive(true);
            _crosshairPanel.SetActive(false);

            if (Input.GetKey(KeyCode.Q) && _canInteract == true)
            {
                _shouldUpdate = false;
                _radialImage.enabled = true;
                _radialImage.fillAmount = _holdTime;

                if (_holdTime > 0)
                {
                    _holdTime -= Time.deltaTime;
                }
                else
                {
                    _holdTime = _maxHoldTime;
                    _radialImage.fillAmount = _maxHoldTime;
                    _radialImage.enabled = false;

                    _canInteract = false;
                    _hostagePanel.SetActive(false);
                    _crosshairPanel.SetActive(true);
                    OnHostageFree();
                }
            }
            else
            {
                if (_shouldUpdate)
                {
                    _holdTime += Time.deltaTime;
                    _radialImage.fillAmount = _holdTime;

                    if (_holdTime > _maxHoldTime)
                    {
                        _holdTime = _maxHoldTime;
                        _radialImage.fillAmount = _maxHoldTime;
                        _radialImage.enabled = false;
                        _shouldUpdate = false;
                    }
                }
            }

            if (Input.GetKeyUp(KeyCode.Q))
            {
                _shouldUpdate = true;
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        _hostagePanel.SetActive(false);
        _crosshairPanel.SetActive(true);
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