using UnityEngine;

public class DroneInput : InputCommon
{
    private bool _jump; //drone jump input

    public bool Jump
    {
        get { return _jump; }
        private set
        {
            _jump = value;
        }
    }

    protected override void GetInput()
    {
        base.GetInput();
        Jump = Input.GetButtonDown("Jump");
    }

    private void Update()
    {
        GetInput();
    }

}
