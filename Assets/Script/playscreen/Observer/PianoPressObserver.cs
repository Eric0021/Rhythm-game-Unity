using System;
using System.Collections.Generic;
using Melanchall.DryWetMidi.Core;

namespace Script.playscreen.Observer{
    public static class PianoPressObserver{
        private static List<PlayerPress> subjects = new List<PlayerPress>();

        public static void AddSubject(PlayerPress subject) {
            subjects.Add(subject);
        }

        public static void removeSubject(PlayerPress subject) {
            subjects.Remove(subject);
        }

        public static void UpdateSubjects(List<NoteOnEvent> e) {
            List<PlayerPress> toBeRemoved = new List<PlayerPress>();
            foreach (PlayerPress subject in subjects) {
                if (!subject) {
                    toBeRemoved.Add(subject);
                    continue;
                }
                subject.PianoKeyPress(e);
            }

            foreach (PlayerPress subject in toBeRemoved) {
                subjects.Remove(subject);
            }
        }
    }
}