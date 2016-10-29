using UnityEngine;
using System.Collections;

public class RoboMoveTypeT : MonoBehaviour {
    //こちらではtransform.Translateを使った場合を書いておく。重力関係をどっちに合わせるかは考え中
    public float speed = 6.0f;
    public float rotateSpeed = 120;
    private float rotate;

    public Animator charaAnim;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        CharaMove();
        CharaRotation();
        CharaJump();
    }

    void CharaMove()
    {
        transform.Translate(0, 0, Input.GetAxisRaw("Vertical") * speed * Time.deltaTime);
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

    void CharaJump()
    {
        if (Input.GetButton("Jump"))
        {

        }
    }
}
