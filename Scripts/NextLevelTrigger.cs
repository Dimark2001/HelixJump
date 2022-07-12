using UnityEngine;

public class NextLevelTrigger : MonoBehaviour
{
    public GameManager gameManager;
    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            gameManager.NextLevel();
        }
    }
}