using UnityEngine;

public class CameraController : MonoBehaviour {

    public BallController target;
    public float offset;
    public static bool follow;

    private void Awake()
    {
        follow = true;
    }

    void Update () 
    {
        if(follow)
        {
            Vector3 curPos = transform.position;
            curPos.y = target.transform.position.y + offset;
            transform.position = curPos;
        }
    }
}
