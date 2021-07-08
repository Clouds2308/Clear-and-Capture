using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    public Animator hand;
    public Animator gun;


    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            hand.SetTrigger("isFire");
            gun.SetTrigger("isFire");
        }

        if(Input.GetKeyDown(KeyCode.R))
        {
            hand.SetTrigger("isReload");
            gun.SetTrigger("isReload");
        }
    }
}
