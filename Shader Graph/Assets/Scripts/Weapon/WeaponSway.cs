using UnityEngine;

public class WeaponSway : MonoBehaviour
{
    [SerializeField] private PlayerInput _input;

    [Header("Position")]
    public float Amount;
    public float maxAmount;
    public float SmoothAmount;

    [Header("Rotation")]
    public float RotationAmount;
    public float MaxRotationAmount;
    public float SmoothRotation;

    [Space]
    public bool rotationX;
    public bool rotationY;
    public bool rotationZ;

    private Vector3 _initialPosition;
    private Quaternion _initialRotation;

    private float InputX;
    private float InputY;

    private void CalculateSway()
    {
        InputX = _input.MouseX * -1;
        InputY = _input.MouseY * -1;
    }

    private void MoveSway()
    {
        float moveX = Mathf.Clamp(InputX * Amount, -maxAmount, maxAmount);
        float moveY = Mathf.Clamp(InputY * Amount, -maxAmount, maxAmount);

        Vector3 finalPosition = new Vector3(moveX, moveY, 0);
        transform.localPosition = Vector3.Lerp(transform.localPosition, _initialPosition + finalPosition, Time.deltaTime * SmoothAmount);
    }

    private void TiltSway()
    {
        float tiltY = Mathf.Clamp(InputX * RotationAmount, -MaxRotationAmount, MaxRotationAmount);
        float tiltX = Mathf.Clamp(InputY * RotationAmount, -MaxRotationAmount, MaxRotationAmount);

        Quaternion finalRotaion = Quaternion.Euler(new Vector3(rotationX ? -tiltX : 0f, rotationY ? tiltY : 0f, rotationZ ? tiltY : 0f));
        transform.localRotation = Quaternion.Slerp(transform.localRotation, finalRotaion * _initialRotation, Time.deltaTime * SmoothRotation);
    }
    
    private void Start()
    {
        _initialPosition = transform.localPosition;
        _initialRotation = transform.localRotation;
    }

    private void Update()
    {
        CalculateSway();
        MoveSway();
        TiltSway();
    }

}
