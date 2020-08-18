using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    public float walkSpeed;
    public float runSpeed;
    //public float jumpSpeed;    
    //public int maxJumps;
    public Vector3 offset;
    public Transform bulletSpawn;
    public GameObject Bullet;
    public Rigidbody rb;
    //public int jumps;
    float horizontal,vertical;
    Vector3 direction;
    
     
    // Start is called before the first frame update
    void Start()
    {
        //jumps = maxJumps;
        //Cursor.visible = false;
        //Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        GameObject.Find("Main Camera").transform.position = new Vector3(transform.position.x,GameObject.Find("Main Camera").transform.position.y,transform.position.z) + offset;
        //character look at mouse position
        Ray camRay = GameObject.Find("Main Camera").GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
        float rayLenght;

        if(groundPlane.Raycast(camRay,out rayLenght))
        {
            Vector3 pointToLook = camRay.GetPoint(rayLenght);
            Debug.DrawLine(camRay.origin,pointToLook,Color.blue);

            transform.LookAt(new Vector3(pointToLook.x,transform.position.y,pointToLook.z));
        }
        
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");        
        direction = new Vector3(horizontal,0,vertical).normalized;
        
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            Shoot();
        }

        if(horizontal !=0 || vertical !=0)
        {
            Move();
        }        
    }

    void Shoot()
    {
        Debug.Log("Shoot");
        Instantiate(Bullet,bulletSpawn.position,bulletSpawn.rotation);
    }

    void Move()
    {
        if(Input.GetKey(KeyCode.LeftShift))
        {
            //run
            //Debug.Log("HERE 1");
            rb.velocity = new Vector3(direction.x * Time.fixedDeltaTime*runSpeed,rb.velocity.y,direction.z * Time.fixedDeltaTime*runSpeed);
        }else
        {
            //walk
            //Debug.Log("HERE 2");
            rb.velocity = new Vector3(direction.x * Time.fixedDeltaTime*walkSpeed,rb.velocity.y,direction.z * Time.fixedDeltaTime*walkSpeed);

            //rb.velocity =  direction * Time.fixedDeltaTime*walkSpeed;
        }
    }

    // void Jump()
    // {
    //     if(jumps > 0)
    //     {
    //         rb.velocity = new Vector3(rb.velocity.x, jumpSpeed*Time.fixedDeltaTime,rb.velocity.z);
    //         jumps--;
    //     }
    // }

    private void OnCollisionEnter(Collision other) {
        //jumps = maxJumps;
    }

}
