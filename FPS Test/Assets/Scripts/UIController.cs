using TMPro;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public TMP_Text currentAmmoText, remainingAmmoText;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void UpdateAmmoText(int currentAmmo, int remainingAmmo)
    {
        currentAmmoText.text = currentAmmo.ToString();
        remainingAmmoText.text = "/" + remainingAmmo;
    }
}
