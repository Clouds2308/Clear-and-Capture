using UnityEngine;

public abstract class InputCommon : MonoBehaviour
{
    //input float values     
    protected float _mouseX;
    protected float _mouseY;
    protected float _inputX;
    protected float _inputZ;

    public float MouseX
    {
        get { return _mouseX; }
        private set
        {
            _mouseX = value;
        }
    }

    public float MouseY
    {
        get { return _mouseY; }
        private set
        {
            _mouseY = value;
        }
    }

    public float InputX
    {
        get { return _inputX; }
        private set
        {
            _inputX = value;
        }
    }

    public float InputZ
    {
        get { return _inputZ; }
        private set
        {
            _inputZ = value;
        }
    }

    protected virtual void GetInput()
    {
        MouseX = Input.GetAxisRaw("Mouse X");
        MouseY = Input.GetAxisRaw("Mouse Y");
        InputX = Input.GetAxis("Horizontal");
        InputZ = Input.GetAxis("Vertical");
    }

    private void Update()
    {
        GetInput();
    }

}
