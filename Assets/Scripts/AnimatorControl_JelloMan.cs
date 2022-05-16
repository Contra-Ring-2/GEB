using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct CharacterState
{
    public CharacterState(string name, int idx)
    {
        _Name = name;
        _Index = idx;
    }
    public string getName()
    {
        return _Name;
    }
    public int getIndex()
    {
        return _Index;
    }
    string _Name;
    int _Index;
}

public class AnimatorControl_JelloMan : MonoBehaviour
{
    // Start is called before the first frame update
    public delegate void StateTrigger(string state);

    StateTrigger myStateTrigger;
    Animator myAnimator;

    List<CharacterState> State = new List<CharacterState>();
    
    

    void Start()
    {

        Animator animator;
        if (this.TryGetComponent<Animator>(out animator))
        {
            myAnimator = animator;
        }
        else 
        {
            Destroy(this);
        }

        CharacterState state_idle = new CharacterState("idle", 0);
        State.Add(state_idle);
        CharacterState state_argue = new CharacterState("argue", 1);
        State.Add(state_argue);
        CharacterState state_angry = new CharacterState("angry", 2);
        State.Add(state_angry);
        CharacterState state_walk = new CharacterState("walk", 3);
        State.Add(state_walk);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddEvent_StateChange(StateTrigger func)
    {
        myStateTrigger += func;
    }

    

    public string GetCurrentState()
    {
        int state_idx = myAnimator.GetInteger("State");
        foreach (var state in State)
        {
            if (state_idx == state.getIndex())
            {
                return state.getName();
            }
        }
        return "";

        
    }


    public void ChangeStae(string input)
    {
        bool flg = false;
        int idx = 0;
        foreach (var state in State)
        {
            Debug.Log(input +"  "+state.getName() + idx.ToString());
            if (input == state.getName())
            {
                
                flg = true;
                idx = state.getIndex();
                
            }
        }

        if (flg == true)
        {
            myAnimator.SetInteger("State", idx);
            if (myStateTrigger != null)
            {
                myStateTrigger(input);
            }
            
        }
        

        
    }


}
