using System.Collections;
using UnityEngine;

public class PassCheck : MonoBehaviour 
{
    
    [SerializeField] private ProgressBar pb;
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            CameraController.follow = true;
            StartCoroutine(Value());
            BallController.perfectPass++;
            BallController.point++;
        }
    }

    IEnumerator Value()
    {
        for(int i = 1; i <= Mathf.Round(100/BallController.passChecCount)+1; i++)
        {
            pb.BarValue += 1;
            yield return new WaitForSeconds(0.02f);
        }
    }
}
