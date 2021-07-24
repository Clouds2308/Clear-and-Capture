using UnityEngine;

public class RainController : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {           
            foreach(Transform child in transform)
            {
                child.gameObject.SetActive(true);
            }
        }
    }
}
