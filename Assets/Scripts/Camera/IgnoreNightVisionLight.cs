using UnityEngine;

public class IgnoreNightVisionLight : MonoBehaviour
{
    [SerializeField] private Light limelight;
    
    private void OnPreCull()
    {
        limelight.enabled = false;
    }
    private void OnPreRender()
    {
        limelight.enabled = false;
    }
    private void OnPostRender()
    {
        limelight.enabled = true;
    }
}
