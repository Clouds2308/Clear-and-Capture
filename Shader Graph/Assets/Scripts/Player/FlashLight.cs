using UnityEngine;

public class FlashLight : MonoBehaviour
{
    [SerializeField] private bool isLightOn;

    public GameObject spotLight;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.F))
        {
            isLightOn = !isLightOn;
            spotLight.SetActive(isLightOn);
        }
    }
}
