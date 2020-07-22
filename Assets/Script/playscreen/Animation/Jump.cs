using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour
{
    public void PlayAnimation() {
        gameObject.GetComponent<Animator>().Play("jump");
    }
}
