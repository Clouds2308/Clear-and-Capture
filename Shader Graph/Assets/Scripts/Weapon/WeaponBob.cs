using UnityEngine;

public class WeaponBob : MonoBehaviour
{
    
    [SerializeField] private PlayerInput _input;
    [SerializeField] private PlayerMovement _movement;

    private Vector3 _weaponParentOrigin;
    private Vector3 _targetWeaponBobPosition;
    private float _idleCounter = Mathf.PI / 2;
    private float _moveCounter = Mathf.PI / 2;

    public float xMultiplier;
    public float yMultiplier;
    
    public Transform WeaponParent;
    
    void BobWeapon(float p_z,float p_x_intensity,float p_y_intensity)
    {
        _targetWeaponBobPosition = _weaponParentOrigin + new Vector3(Mathf.Cos(p_z * xMultiplier) * p_x_intensity, Mathf.Sin(p_z * yMultiplier) * p_y_intensity,0);
    }

    private void Start()
    {
        _weaponParentOrigin = WeaponParent.localPosition;
    }

    private void Update()
    {
        if (_input.InputX == 0 && _input.InputZ == 0)
        {
            BobWeapon(_idleCounter,0.001f,0.005f);
            _idleCounter += Time.deltaTime;
            WeaponParent.localPosition = Vector3.Lerp(WeaponParent.localPosition, _targetWeaponBobPosition, Time.deltaTime * 2f);
        }
        else
        {
            if (_movement.IsGrounded)
            {
                BobWeapon(_moveCounter, 0.05f, 0.007f);
                _moveCounter += Time.deltaTime;
                WeaponParent.localPosition = Vector3.Lerp(WeaponParent.localPosition, _targetWeaponBobPosition, Time.deltaTime * 10f);
            }
        }

        if (_idleCounter > Mathf.PI * 2) _idleCounter = 0;
        if (_moveCounter > Mathf.PI * 2) _moveCounter = 0;
    }
}