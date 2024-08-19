using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private PlayerController playerController; 
    [SerializeField] private Slider healthSlider; 

    private void Start()
    {
        
        healthSlider.maxValue = playerController.maxHp; 
        healthSlider.value = playerController.curHp; 
    }

    private void Update()
    {
        
        healthSlider.value = playerController.curHp;
    }
}
