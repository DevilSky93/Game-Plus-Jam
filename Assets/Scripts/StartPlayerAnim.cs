using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPlayerAnim : MonoBehaviour
{
    private Animator _anim;
    // Start is called before the first frame update
    void Start()
    {
        _anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void WashingAnim()
    {
        _anim.SetTrigger("isWashing");
    }

    public void ChangeWalkingAnimBool(bool val)
    {
        _anim.SetBool("isWalking", val);
    }

    public void ChangePos(Vector3 pos)
    {
        transform.localPosition = pos;
    }

    public void EatingAnim(bool val)
    {
        _anim.SetBool("isEating", val);
    }
}
