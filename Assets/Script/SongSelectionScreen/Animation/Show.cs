using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Show : StateMachineBehaviour{
    public string LastState;
    private string _stateName = "";
    private string _animationName = "";
    
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        // show the animation sprite
        if (stateInfo.IsName("HardSelect")) {
            _stateName = "HardSelect";
        }else if (stateInfo.IsName("MediumSelect")) {
            _stateName = "MediumSelect";
        }else if (stateInfo.IsName("EasySelect")) {
            _stateName = "EasySelect";
        }

        Debug.Log("entering "+_stateName);
        _animationName = _stateName + "Animation";
        GameObject selectAnimation = GameObject.Find(_animationName);
        Color color = selectAnimation.GetComponent<SpriteRenderer>().color;
        color.a = 1;
        selectAnimation.GetComponent<SpriteRenderer>().color = color;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    //  OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log("exiting "+_stateName);
        // hide the animation sprite.
        GameObject selectAnimation = GameObject.Find(_animationName);
        Color color = selectAnimation.GetComponent<SpriteRenderer>().color;
        color.a = 0;
        selectAnimation.GetComponent<SpriteRenderer>().color = color;
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
