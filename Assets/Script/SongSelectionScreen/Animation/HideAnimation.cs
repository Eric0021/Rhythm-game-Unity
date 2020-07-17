using System.Collections.Generic;
using UnityEngine;

public class HideAnimation : StateMachineBehaviour{
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo,
        int layerIndex) {
        // hide all animation sprites when they are not playing.
        List<GameObject> animations = new List<GameObject>();
        animations.Add(GameObject.Find("HardSelectAnimation"));
        animations.Add(GameObject.Find("MediumSelectAnimation"));
        animations.Add(GameObject.Find("EasySelectAnimation"));

        foreach (GameObject animation in animations) {
            Color color = animation.GetComponent<SpriteRenderer>().color;
            color.a = 0;
            animation.GetComponent<SpriteRenderer>().color = color;
        }
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo,
        int layerIndex) {
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo,
        int layerIndex) {
    }

    public override void OnStateMove(Animator animator, AnimatorStateInfo stateInfo,
        int layerIndex) {
    }

    public override void OnStateIK(Animator animator, AnimatorStateInfo stateInfo,
        int layerIndex) {
    }
}