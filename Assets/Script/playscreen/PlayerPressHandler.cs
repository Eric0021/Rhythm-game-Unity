using System;
using System.Collections.Generic;
using System.Linq;
using Melanchall.DryWetMidi.Core;
using Script.Helper.Midi;
using Script.playscreen.Observer;
using Script.playscreen.Score;
using UnityEngine;
using UnityEngine.UI;

namespace Script.playscreen{
    // a static class that handles piano inputs.
    public class PlayerPressHandler : MonoBehaviour{
        // A list of the active beats that can be pressed.
        private static List<GameObject> _beats = new List<GameObject>();
        private static float _missHeight;
        private static ScoreHandler _scoreHandler = new ScoreHandler();

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
            // when key is pressed, if it matches the closest beat to the bar (missHeight), press the beat.
            
            string allNotesPressed = GetAllNotesPressed(e);
            GameObject closestBeat = GetClosestBeat();

            // if there aren't any beats yet, do nothing.
            if (!closestBeat) {
                return;
            }
            
            if (CorrectNotes(allNotesPressed, closestBeat.transform.GetChild(0).GetComponent<Text>().text)) {
                closestBeat.transform.parent.GetComponent<CallJump>().Call();
                _scoreHandler.IncrementCombo();
                Destroy(closestBeat);
                _beats.Remove(closestBeat);
            }
        }

        private bool CorrectNotes(string allNotesPressed, string correctNotes) {
            var notesPressedArr = allNotesPressed.Split('+');
            var correctNotesArr = correctNotes.Split('+');

            if (notesPressedArr.Length == correctNotesArr.Length) {
                if (!notesPressedArr.Except(correctNotesArr).Any()) {
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
                var rectTransform = beat.GetComponent<RectTransform>();
                float beatTop = rectTransform.anchoredPosition.y;
                float beatCentreY = rectTransform.anchoredPosition.y - rectTransform.rect.height / 2;
                
                // if beat is past the miss height, it cannot be pressed.
                if (beatTop < _missHeight) {
                    continue;
                }
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