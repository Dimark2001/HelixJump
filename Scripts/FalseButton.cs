using UnityEngine;

public class FalseButton : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    void OnMouseUp()
    {
        Debug.Log("restart");
        gameManager.RestartLevel();
    }
}