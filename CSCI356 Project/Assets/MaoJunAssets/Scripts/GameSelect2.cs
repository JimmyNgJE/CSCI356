using UnityEngine;

public class GameSelect2 : MonoBehaviour
{
    public static GameSelect2 Instance;

    public string SelectedCharacter { get; set; }
    public string SelectedDifficulty { get; set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            Debug.Log("GameSelect2 Singleton Initialized");
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
