using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MediumSelectScript : MonoBehaviour
{
    public void PlayAnimation() {
        // only play animation if the last selected was not the same state.
        if (!gameObject.transform.parent.GetComponent<StateData>().IsLastSelected("medium")) {
            gameObject.GetComponent<Animator>().Play("MediumSelect");
        }
    }
}
