using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallJump : MonoBehaviour{
    private List<Jump> _animation1 = new List<Jump>();
    private List<Jump> _animation2 = new List<Jump>();
    private List<Jump> _animation3 = new List<Jump>();
    private List<Jump> _animation4 = new List<Jump>();

    public void Call() {
        // play all the animations of a beat jump.
        
        List<Jump> targetAnimations = new List<Jump>();
        switch (gameObject.name) {
            case("column1"):
                targetAnimations = _animation1;
                break;
            case("column2"):
                targetAnimations = _animation2;
                break;
            case("column3"):
                targetAnimations = _animation3;
                break;
            case("column4"):
                targetAnimations = _animation4;
                break;
        }
        
        foreach (Jump jump in targetAnimations) {
            jump.PlayAnimation();
        }
    }
    
    // Start is called before the first frame update
    void Start() {
        // add all the animation corresponding to a column into the respective list.
        
        _animation1.Add(GameObject.Find("jump1").GetComponent<Jump>());
        _animation1.Add(GameObject.Find("squareAni1").GetComponent<Jump>());
        
        _animation2.Add(GameObject.Find("jump2").GetComponent<Jump>());
        _animation2.Add(GameObject.Find("squareAni2").GetComponent<Jump>());
        
        _animation3.Add(GameObject.Find("jump3").GetComponent<Jump>());
        _animation3.Add(GameObject.Find("squareAni3").GetComponent<Jump>());
        
        _animation4.Add(GameObject.Find("jump4").GetComponent<Jump>());
        _animation4.Add(GameObject.Find("squareAni4").GetComponent<Jump>());
    }
}
