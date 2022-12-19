using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigidbodyMove : MonoBehaviour
{
    Rigidbody rb;
    public float speed = 10f;
    public float MaxSpeed;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }

    void Update()
    {
        float xMove = Input.GetAxis("Horizontal");
        float zMove = Input.GetAxis("Vertical");

        Vector3 getVel = new Vector3(xMove, 0, zMove) * speed;
        rb.velocity = getVel;
    }
}







/*/
public class PlayerMove : MonoBehaviour
{
    private Rigidbody rigidbody;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = this.Get
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
/*/