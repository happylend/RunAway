using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassBox : BoxFather
{
    // Start is called before the first frame update
    private void Awake()
    {
        Init(BoxType.Grass, gameObject);
    }

    // Update is called once per frame
    private void Update()
    {
        NewMove(Player_Controller.RestartLayer);
    }
    private void LateUpdate()
    {
        Fall(gameObject);

    }
    
}
