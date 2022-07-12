using UnityEngine;

public class HelixController : MonoBehaviour {

    private Vector3 startRotation;
    private Vector2 lastTapPos;
    private float helixDistance;
    public Transform topTransform;
    public Transform goalTransform;

    void Awake () 
    {
        startRotation = transform.localEulerAngles;
        helixDistance = topTransform.localPosition.y - (goalTransform.localPosition.y + .1f);
    }

	void Update () 
    {
        if (Input.GetMouseButton(0))
        {

            Vector2 curTapPos = Input.mousePosition;

            if (lastTapPos == Vector2.zero)
                lastTapPos = curTapPos;

            float delta = lastTapPos.x - curTapPos.x;
            lastTapPos = curTapPos;

            transform.Rotate(Vector3.up * delta);
        }
        if (Input.GetMouseButtonUp(0))
        {
            lastTapPos = Vector2.zero;
        }
    }
}
