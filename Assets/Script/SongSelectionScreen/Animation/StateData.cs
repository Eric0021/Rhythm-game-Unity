using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateData : MonoBehaviour{
    private string _selected = "";
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool IsLastSelected(string newSelected) {
        // update the selected state, and returns true if it is the same as last selected state.
        string temp = _selected;
        SetSelected(newSelected);
        return temp.Equals(newSelected);
    }

    public void SetSelected(string selected) {
        _selected = selected;
    }
}
