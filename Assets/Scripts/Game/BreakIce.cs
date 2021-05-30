using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakIce : MonoBehaviour
{
    Animation animationChild;
    // Start is called before the first frame update
    void Start()
    {
        animationChild = this.GetComponentInChildren<Animation>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void check()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Player")
        {
            animationChild.Play();
        }

    }

    private void OnCollisionExit(Collision collision)
    {
        if(collision.collider.tag == "Player")
        {
            this.gameObject.SetActive(false);
        }
    }


}
