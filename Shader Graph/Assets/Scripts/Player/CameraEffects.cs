using System;
using UnityEngine;

public class CameraEffects : MonoBehaviour
{
    public Vector3 Amount = new Vector3(1f, 1f, 0f); //amount of shake
    public float Duration = 1;  //duration of shake
    public float Speed = 10;    //speed of shake

    public AnimationCurve Curve = AnimationCurve.EaseInOut(0, 1, 1, 0);  //amount over lifetime [0,1]

    //set it to true : Camera position set in reference to old position of camera
    //set it to false : Camera position set in absolute values or is fixed to an object
    public bool DeltaMovement = true;

    public Camera Camera;

    protected float time = 0;
    protected Vector3 LastPos;
    protected Vector3 NextPos;
    protected float LastFOV;
    protected float NextFOV;

    
    //Do the shake
    public void Shake()
    {
        ResetCam();
        time = Duration;
    }

    private void LateUpdate()
    {
        if(time>0)
        {
            time -= Time.deltaTime;
            if(time>0)
            {
                //next position based on perlin noise
                NextPos = (Mathf.PerlinNoise(time * Speed ,time * Speed * 2)-0.5f) * Amount.x * transform.right * Curve.Evaluate(1f - time / Duration) +
                          (Mathf.PerlinNoise(time * Speed * 2,time * Speed)-0.5f) * Amount.y * transform.up * Curve.Evaluate(1f - time / Duration);

                NextFOV = (Mathf.PerlinNoise(time * Speed * 2, time * Speed * 2)-0.5f) * Amount.z * Curve.Evaluate(1f - time/Duration);

                Camera.fieldOfView += (NextFOV - LastFOV);
                Camera.transform.Translate(DeltaMovement ? (NextPos - LastPos) : NextPos);

                LastPos = NextPos;
                LastFOV = NextFOV;
            }
            else
            {
                //last frame
                ResetCam();
            }
        }
    }
   
    private void ResetCam()
    {
        //reset the last delta
        Camera.transform.Translate(DeltaMovement ? -LastPos : Vector3.zero);
        Camera.fieldOfView -= LastFOV;


        //clear values
        LastPos = NextPos = Vector3.zero;
        LastFOV = NextFOV = 0f;
    }

    private void Awake()
    {
        Camera = GetComponent<Camera>();
    }
    
}
