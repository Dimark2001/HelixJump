using System.Collections;
using UnityEngine;
using TMPro;

public class BallController : MonoBehaviour 
{
    private Vector3 startPos;
    public float impulseForce;
    public float squizz;
    public float endPos;
    public static int perfectPass = 0;
    public static int point;
    public static int passChecCount;
    private bool ignoreNextCollision;
    private bool isSuperSpeedActive;
    private bool squi;
    [SerializeField] private TextMeshProUGUI _text; 
    [SerializeField] private Rigidbody rb;
    [SerializeField] private ProgressBar pb;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private GameObject deadEffect;
    [SerializeField] private GameObject gameOverMenu;
    [SerializeField] private Gradient redGradient;
    [SerializeField] private Gradient greenGradient;
    [SerializeField] private Material _platformMaterial;
    [SerializeField] private Material _GoodMaterial;
    [SerializeField] private Material _badMaterial;
    [SerializeField] private Material _playerMaterial;

    private void Awake()
    {
        startPos = transform.position;
        squi = false;
        gameOverMenu.SetActive(false);
        point = 0;
        _text.text = "Score " + "0";
        passChecCount = FindObjectsOfType<PassCheck>().Length;
        Debug.Log(passChecCount);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (ignoreNextCollision)
            return;
        if(collision.gameObject.tag == "Wall")
        {
            if(isSuperSpeedActive)
            {
                pb.BarValue += Mathf.Round(100/passChecCount)+1;
                CameraController.follow = true;
                point += 10;
                collision.transform.parent.gameObject.SetActive(false);
                gameObject.GetComponent<MeshRenderer>().material.color = _playerMaterial.color;
                gameObject.GetComponentInChildren<TrailRenderer>().colorGradient = greenGradient;
            }
            else
            {
                if(collision.gameObject.GetComponent<MeshRenderer>().material.color == _platformMaterial.color)
                {
                    CameraController.follow = false;
                    StartCoroutine(Squeeze());
                    squi = true;
                    Debug.Log("Y");
                }
                if(collision.gameObject.GetComponent<MeshRenderer>().material.color == _GoodMaterial.color)
                {
                    CameraController.follow = false;
                    StartCoroutine(Squeeze());
                    squi = true;
                    collision.gameObject.SetActive(false);

                }
                if(collision.gameObject.GetComponent<MeshRenderer>().material.color == _badMaterial.color)
                {
                    CameraController.follow = false;
                    DeadMenu();
                    Instantiate(deadEffect, transform.position, Quaternion.identity);
                    gameObject.SetActive(false);
                }
            }
        }
        if(collision.gameObject.tag == "Helix")
        {
            CameraController.follow = false;
            StartCoroutine(Squeeze());
            squi = true;
            gameManager.NextLevel();
        }
        rb.velocity = Vector3.zero;
        rb.AddForce(Vector3.up * impulseForce, ForceMode.Impulse);
        ignoreNextCollision = true;
        Invoke("AllowCollision", .2f);
        perfectPass = 0;
        isSuperSpeedActive = false;
    }

    private void Update()
    {
        if (perfectPass >= 3 && !isSuperSpeedActive)
        {
            isSuperSpeedActive = true;
            gameObject.GetComponent<MeshRenderer>().material.color = _badMaterial.color;
            gameObject.GetComponentInChildren<TrailRenderer>().colorGradient = redGradient;
            rb.AddForce(Vector3.down * 3, ForceMode.Impulse);
        }
        if(squi)
        {
            float minimum = 0.4f;
            float maximum = 0.6f;
            transform.localScale = new Vector3(0.6f, Mathf.PingPong(Time.time, maximum-minimum)+minimum, 0.6f);
        }
        _text.text = "Score " + BallController.point.ToString();
    }
    private void AllowCollision()
    {
        ignoreNextCollision = false;
    }
    void DeadMenu()
    {
        gameOverMenu.transform.position = new Vector3(0, transform.position.y+2.4f, -4.5f);
        gameOverMenu.transform.rotation = Camera.main.transform.rotation;
        gameOverMenu.SetActive(true);
    }
    IEnumerator Squeeze()
    {
        yield return new WaitForSeconds(squizz);
        squi = false;
        transform.localScale = new Vector3(0.6f,0.6f,0.6f);
    }
}