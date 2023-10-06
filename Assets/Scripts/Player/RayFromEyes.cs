using UnityEngine;

public class RayFromEyes : MonoBehaviour
{
    [SerializeField] private float _rayLength = 10f;
    [SerializeField] private Color _rayColor;
    [SerializeField] private LayerMask _layersToIgnore;
    
    [HideInInspector] public string hitObjectTag;
     
     void Update()
    {
        ThrowRay();
    }

    private void ThrowRay()
    {
        Camera mainCamera = Camera.main;

        if (mainCamera == null)
        {
            Debug.LogError("Main camera not found.");
            return;
        }

        Ray ray = mainCamera.ScreenPointToRay(new Vector3(Screen.width / 2f, Screen.height / 2f, 0f));
        RaycastHit hit;
        
        Debug.DrawRay(ray.origin, ray.direction * _rayLength, _rayColor);
        
        if (Physics.Raycast(ray, out hit, _rayLength, _layersToIgnore))
            hitObjectTag = hit.collider.gameObject.tag;
        else
            hitObjectTag = null;
    }
}