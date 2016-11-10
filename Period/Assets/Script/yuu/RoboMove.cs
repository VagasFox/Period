using UnityEngine;
using System.Collections;

public class RoboMove : MonoBehaviour {
    //こっちはCharacterControllerを使用するタイプ
    public float speed = 6.0f;
    public float jumpSpeed = 5.0f;
    public float rotateSpeed = 120;
    private float yMove;
    private float rotate;
    public float gravity = 9.8f;
    private CharacterController controller;
    public StartDirection SD;
    public Vector3 DirectMove = Vector3.zero;

    public Animator charaAnim;

    private AudioSource MoveSound;
	// Use this for initialization
	void Start () {
        controller = GetComponent<CharacterController>();
        SD = GameObject.Find("Main Camera").GetComponent<StartDirection>();
        SoundManager.PlaySE(SE_Enum.MOVE, this.gameObject);
        MoveSound = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
        ActMove();
        CharaRotation();
        
        //頂点まで来た後に下に落ちるのを早める場合
        if (DirectMove.y >= 2)
        {
            DirectMove.y -= gravity * Time.deltaTime;
        }
        else if(gravity <= 9.8f)
        {
            DirectMove.y -= gravity * 5 * Time.deltaTime;
        }
        
        //常に同じ重力をかける場合
        //DirectMove.y -= gravity * Time.deltaTime;

        controller.Move(DirectMove * Time.deltaTime);
	}

    void ActMove()
    {
        yMove = DirectMove.y;
        DirectMove = transform.forward * Input.GetAxisRaw("Vertical") * speed;
        DirectMove.y += yMove;

        //runアニメーションを作動させる
        charaAnim.SetFloat("Speed", Mathf.Abs(Input.GetAxisRaw("Horizontal")) + Mathf.Abs(Input.GetAxisRaw("Vertical")));

        Vector3 MoveDir = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
        if (MoveDir != Vector3.zero)
        {
            if (MoveSound.isPlaying == false)
            {
                MoveSound.PlayOneShot(MoveSound.clip);
            }
            else
            {

            }
        }
        else
        {
            MoveSound.Stop();
        }

        //地上に居る時の判定（移動もこの中に入れてもいいかもしれない）
        if (controller.isGrounded)
        {
            //地上に居る時は下方向への加速が増えて行かないようにする。
            DirectMove.y = 0;
            
            if (Input.GetButton("Jump"))
            {
                DirectMove.y = jumpSpeed;
            }
        }
    }

    void CharaRotation()
    {
        if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) > 0.5)
        {
            rotate += Input.GetAxis("Horizontal") * rotateSpeed * Time.deltaTime;
            transform.eulerAngles = new Vector3(0, rotate, 0);
        }
    }
}
