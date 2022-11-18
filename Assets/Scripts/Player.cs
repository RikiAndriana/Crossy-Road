using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class Player : MonoBehaviour
{
    public GameObject JumpSFX;
    public GameObject DieSFX;
    AudioSource AudioJump;
    AudioSource AudioDie;
    [SerializeField] TMP_Text stepText;
    [SerializeField] TMP_Text highestScoreText;
    [SerializeField] ParticleSystem dieParticles;
    [SerializeField, Range(0.1f, 1f)] float moveDuration = 0.2f;
    [SerializeField, Range(0.1f, 1f)] float jumpHeight = 0.5f;
    private int highestScore;
    private int minZPos;
    private int extent;
    private int backBoundary;
    private int leftBoundary;
    private int rightBoundary;
    [SerializeField] private int maxTravel;
    public int MaxTravel { get => maxTravel; }
    [SerializeField] private int currentTravel;
    public int CurrentTravel { get => currentTravel; }

    public bool IsDie { get => this.enabled == false; }

    private void Awake()
    {
        highestScore = PlayerPrefs.GetInt("Highest_Score");
    }
    private void Start()
    {
        JumpSFX = GameObject.FindGameObjectWithTag("SFX_Jump");
        AudioJump = JumpSFX.GetComponent<AudioSource>();
        DieSFX = GameObject.FindGameObjectWithTag("SFX_Die");
        AudioDie = DieSFX.GetComponent<AudioSource>();
    }
    public void SetUp(int minZPos, int extent)
    {
        backBoundary = minZPos - 1;
        leftBoundary = -(extent + 1);
        rightBoundary = extent + 1;

    }
    void Update()
    {
        var moveDir = Vector3.zero;
        if (Input.GetKey(KeyCode.UpArrow))
        {
            moveDir += new Vector3(0, 0, 1);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            moveDir += new Vector3(0, 0, -1);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            moveDir += new Vector3(1, 0, 0);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            moveDir += new Vector3(-1, 0, 0);
        }
        if (moveDir != Vector3.zero && IsJumping() == false)
            Jump(moveDir);

    }
    private void Jump(Vector3 targetDirection)
    {


        // atur rotasi
        var TargetPosition = transform.position + targetDirection;
        transform.LookAt(TargetPosition);

        // loncat ke atas
        // transform.DOMoveY(2f, 0.1f).OnComplete(() => transform.DOMoveY(0, 0.1f));
        // transform.DOMove(TargetPosition, 0.2f);
        var moveSeq = DOTween.Sequence(transform);
        moveSeq.Append(transform.DOMoveY(jumpHeight, moveDuration / 2));
        moveSeq.Append(transform.DOMoveY(0, moveDuration / 2));
        AudioJump.Play();


        if (Tree.AllPosition.Contains(TargetPosition))
            return;

        if (TargetPosition.z <= backBoundary ||
            TargetPosition.x <= leftBoundary ||
            TargetPosition.x >= rightBoundary)
            return;

        // gerak maju/mundur/samping
        transform.DOMoveX(TargetPosition.x, moveDuration);
        transform
            .DOMoveZ(TargetPosition.z, moveDuration)
            .OnComplete(UpdateTravel);
    }

    private void UpdateTravel()
    {
        currentTravel = (int)this.transform.position.z;
        if (currentTravel > maxTravel)
            maxTravel = currentTravel;
        stepText.text = "Score: " + maxTravel.ToString();

        if (currentTravel > highestScore)
        {
            highestScore = currentTravel;

            PlayerPrefs.SetInt("Highest_Score", highestScore);
            PlayerPrefs.Save();

        }
        highestScoreText.text = "Highest Score: " + highestScore.ToString();
    }
    public bool IsJumping()
    {
        return DOTween.IsTweening(transform);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (this.enabled == false)
            return;
        //di execute sekali pada frame ketika nempel pertama kali
        var car = other.GetComponent<Car>();
        if (car != null)
        {
            AnimateCrash(car);
        }
        // if (other.tag == "Car")
        // {
        //     // Debug.Log("Hit " + other.name);
        //     // AnimateDie();
        // }
    }
    private void AnimateCrash(Car car)
    {
        // var isRight = car.transform.rotation.y == 90;

        // transform.DOMove(isRight ? 8 : -8, 0.2f);
        // transform
        //     .DORotate(Vector3.forward * 360, 1f)
        //     .SetLoops(-1, LoopType.Restart);

        //Gepeng
        transform.DOScaleY(0.1f, 0.2f);
        transform.DOScaleX(1.1f, 0.2f);
        transform.DOScaleZ(1.1f, 0.2f);
        this.enabled = false;
        dieParticles.Play();
        AudioDie.Play();
    }
    private void OnTriggerStay(Collider other)
    {
        //di execute setiap frame selama masih nempel
    }
    private void OnTriggerExit(Collider other)
    {
        //di execute sekali pada frame ketika tidak nempel
    }
}
