using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EasySelectScript : MonoBehaviour
{
    public void PlayAnimation() {
        // only play animation if the last selected was not the same state.
        if (!gameObject.transform.parent.GetComponent<StateData>().IsLastSelected("easy")) {
            gameObject.GetComponent<Animator>().Play("EasySelect");
        }
    }
}
