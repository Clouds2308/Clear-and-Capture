using UnityEngine;

public class WeaponBob : MonoBehaviour
{
    
    [SerializeField] private Transform WeaponParent;
    private PlayerInput _playerInput;
    //private PlayerMovement _playerMovement;

    private Vector3 _weaponParentOrigin;
    private Vector3 _targetWeaponBobPosition;
    private float _idleCounter = Mathf.PI / 2;
    private float _moveCounter = Mathf.PI / 2;
    private float _bobSpeed;

    
    void BobWeapon(float p_z,float p_y_intensity)
    {
        _targetWeaponBobPosition = _weaponParentOrigin + new Vector3(0,Mathf.Sin(p_z * _bobSpeed) * p_y_intensity, 0);
    }

    private void Start()
    {
        _weaponParentOrigin = WeaponParent.localPosition;
        _playerInput = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInput>();
        //_playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
    }

    private void FixedUpdate()
    {
        if (_playerInput.InputX == 0 && _playerInput.InputZ == 0)
        {
            _bobSpeed = 3f;
            BobWeapon(_idleCounter, 0.005f);
            _idleCounter += Time.deltaTime;
            WeaponParent.localPosition = Vector3.Lerp(WeaponParent.localPosition, _targetWeaponBobPosition, Time.deltaTime);
        }
        else
        {
            _bobSpeed = 9f;
            BobWeapon(_moveCounter, 0.01f);
            _moveCounter += Time.deltaTime;
            WeaponParent.localPosition = Vector3.Lerp(WeaponParent.localPosition, _targetWeaponBobPosition, Time.deltaTime * 5f);            
        }

        if (_idleCounter > Mathf.PI * 2) _idleCounter = 0;
        if (_moveCounter > Mathf.PI * 2) _moveCounter = 0;
    }
    
}