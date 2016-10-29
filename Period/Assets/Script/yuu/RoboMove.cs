using UnityEngine;
using System.Collections;

public class RoboMove : MonoBehaviour {
    //こっちはCharacterControllerを使用するタイプ
    public float speed = 6.0f;
    public float rotateSpeed = 120;
    private float rotate;
    public float gravity = 0.3f;
    private CharacterController controller;
    private Vector3 DirectMove = Vector3.zero;

    public Animator charaAnim;
	// Use this for initialization
	void Start () {
        controller = GetComponent<CharacterController>();
	}
	
	// Update is called once per frame
	void Update () {
        ActMove();
        DirectMove.y -= gravity * 60 * Time.deltaTime;
        controller.Move(DirectMove * Time.deltaTime);
        CharaRotation();

	}

    void ActMove()
    {
        DirectMove = transform.forward * Input.GetAxisRaw("Vertical") * speed;
        
        //DirectMove.x = Input.GetAxisRaw("Horizontal") * speed;
        charaAnim.SetFloat("Speed", Mathf.Abs(Input.GetAxisRaw("Horizontal")) + Mathf.Abs(Input.GetAxisRaw("Vertical")));
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
