using UnityEngine;

public class Button : MonoBehaviour
{
    [SerializeField] private Transform player = null;

    [SerializeField] private Transform ghost = null;

    public bool IsPressed { get; private set; }

    private void Update()
    {
        IsPressed = false;

        if(IsOnButton(player)) IsPressed = true;
        if(IsOnButton(ghost)) IsPressed = true;

        Debug.Log($"IsPressed: {IsPressed}");
    }

    private bool IsOnButton(Transform target)
    {
        float distance = Vector3.Distance(
            transform.position, 
            target.position
        );

        return distance < 0.5f;
    }
}
