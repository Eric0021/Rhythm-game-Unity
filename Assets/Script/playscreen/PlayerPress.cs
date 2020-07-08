using System;
using System.Collections.Generic;
using Melanchall.DryWetMidi.Core;
using Script.Helper.Midi;
using Script.playscreen.Observer;
using UnityEngine;
using UnityEngine.UI;

namespace Script.playscreen{
    public class PlayerPress : MonoBehaviour{
        // A list of the active beats that can be pressed.
        private static List<GameObject> _beats = new List<GameObject>();
        private static float _missHeight;

        private void Start() {
            PianoPressObserver.AddSubject(this);
        }

        public static void AddToBeats(GameObject beat) {
            _beats.Add(beat);
        }

        public static void RemoveBeat(GameObject beat) {
            _beats.Remove(beat);
        }
        public void PianoKeyPress(List<NoteOnEvent> e) {
            string allNotesPressed = GetAllNotesPressed(e);
            GameObject closestBeat = GetClosestBeat();

            // if there aren't any beats yet, don't do anything.
            if (!closestBeat) {
                return;
            }
            
            if (CorrectNotes(allNotesPressed, closestBeat.transform.GetChild(0).GetComponent<Text>().text)) {
                Destroy(closestBeat);
            }
        }

        private bool CorrectNotes(string allNotesPressed, string correctNotes) {
            var notesPressedArr = allNotesPressed.Split('+');
            var correctNotesArr = correctNotes.Split('+');
            
            print(notesPressedArr);
            print(correctNotesArr);

            if (notesPressedArr.Length == correctNotesArr.Length) {
                if (notesPressedArr.Equals(correctNotesArr)) {
                    return true;
                }
            }

            return false;
        }

        private string GetAllNotesPressed(List<NoteOnEvent> e) {
            int count = 0;
            string noteStringFull = "";
            foreach (NoteOnEvent note in e) {
                string noteString = NoteTranslator.translate(note.NoteNumber.ToString());
                if (count != 0) {
                    noteStringFull += "+";
                }

                noteStringFull += noteString;
                count++;
            }

            return noteStringFull;
        }

        private GameObject GetClosestBeat() {
            float closestY = float.MaxValue;
            GameObject closestBeat = null;
            foreach (GameObject beat in _beats) {
                float beatCentreY = beat.transform.position.y - beat.GetComponent<RectTransform>().rect.height / 2;
                float distance = Math.Abs(_missHeight - beatCentreY);
                if (distance < closestY) {
                    closestBeat = beat;
                    closestY = distance;
                }
            }

            return closestBeat;
        }

        public static void SetMissHeight(float setMissHeight) {
            _missHeight = setMissHeight;
        }
    }
}