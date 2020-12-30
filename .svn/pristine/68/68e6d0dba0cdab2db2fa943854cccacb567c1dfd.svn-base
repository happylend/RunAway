using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenBox : MonoBehaviour
{
    Animation animation;
    public static bool Open;
    private float time = 0f;
    // Start is called before the first frame update
    void Start()
    {
        animation = this.GetComponent<Animation>();
        Open = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Open)
        {
            time += Time.deltaTime;
            if (time > 0.4f)
            {
                Debug.Log("Open!");
                animation.Play();
                time = 0;
                Open = false;
            }
        }
    }
}
