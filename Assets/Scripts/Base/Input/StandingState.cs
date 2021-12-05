using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandingState : IChState
{
    // Start is called before the first frame update
    private Character _ich;
    private StateMachine state;
    public StandingState(Character ich, StateMachine state):base(ich, state)
    {
        this._ich = ich;
        Debug.Log("standing");
        this.state = state;
    }
    public override void Update()
    {

        CheckKey(KeyCode.W);
        CheckKey(KeyCode.A);
        CheckKey(KeyCode.D);
        CheckKey(KeyCode.S);
        //动画播放
        
    }
    public override void HandleInput()
    {
       
    }

    public void CheckKey(KeyCode key)
    {
            EventCenter.GetInstance().EventTrigger("StateKeydown", key);

    }
}
