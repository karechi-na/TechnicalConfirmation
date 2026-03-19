using UnityEngine;

public class Exit : MonoBehaviour
{
    [SerializeField] private MeshRenderer meshRenderer = null;

    [SerializeField] private Button button = null;

    private void Update()
    {
        if(button.IsPressed) meshRenderer.enabled = !button.IsPressed;
        else 
            meshRenderer.enabled = true;
    }
}
