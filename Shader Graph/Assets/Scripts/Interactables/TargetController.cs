using UnityEngine;
using System.Collections;

public class TargetController : MonoBehaviour, IDestructibleByGun
{
    public bool isFall;
    public bool isRise = false;
    public float speed = 200f;
    private float xRot = 0f;    

    public void DestroyOnHit()
    {
        isFall = true;
        isRise = false;
    }

    private void Update()
    {
        if(isFall)
        {
            xRot -= Time.deltaTime * speed;
            transform.rotation = Quaternion.Euler(new Vector3(xRot, transform.rotation.y-90f, transform.rotation.z));

            if (xRot <= -90f)
            {
                xRot = -90f;
                StartCoroutine(timer());
            }
        }
        
        if(isRise)
        {
            xRot += Time.deltaTime * speed;
            transform.rotation = Quaternion.Euler(new Vector3(xRot, transform.rotation.y - 90f, transform.rotation.z));

            if (xRot >= 0f)
                xRot = 0f;
        }
    }

    IEnumerator timer()
    {
        yield return new WaitForSeconds(2f);
        isRise = true;
        isFall = false;
    }
}
