using UnityEngine;

public class DontDestroy : MonoBehaviour
{
    void Awake()
    {
        // Ensure this object is not destroyed on scene load
        DontDestroyOnLoad(gameObject);
    }
}
