using System;
using System.Collections.Generic;
using Melanchall.DryWetMidi.Core;
using Script.Helper.Midi;
using Script.playscreen.Observer;
using UnityEngine;
using UnityEngine.UI;

namespace Script.playscreen{
    public class BeatCollider : MonoBehaviour{
        
        private void Start() {
            // add self to the list of active beats.
            PlayerPress.AddToBeats(gameObject);
        }

        private void Update() {
        }

        private void OnTriggerExit2D(Collider2D other) {
            // the note cannot be pressed once it gets too low.
            if (other.tag.Equals("Activator")) {
                PlayerPress.RemoveBeat(gameObject);
            }
        }
    }
}