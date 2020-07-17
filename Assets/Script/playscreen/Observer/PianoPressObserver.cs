using System;
using System.Collections.Generic;
using Melanchall.DryWetMidi.Core;

namespace Script.playscreen.Observer{
    public static class PianoPressObserver{
        private static List<PlayerPressHandler> subjects = new List<PlayerPressHandler>();

        public static void AddSubject(PlayerPressHandler subject) {
            subjects.Add(subject);
        }

        public static void removeSubject(PlayerPressHandler subject) {
            subjects.Remove(subject);
        }

        public static void UpdateSubjects(List<NoteOnEvent> e) {
            List<PlayerPressHandler> toBeRemoved = new List<PlayerPressHandler>();
            foreach (PlayerPressHandler subject in subjects) {
                if (!subject) {
                    toBeRemoved.Add(subject);
                    continue;
                }
                subject.PianoKeyPress(e);
            }

            foreach (PlayerPressHandler subject in toBeRemoved) {
                subjects.Remove(subject);
            }
        }
    }
}