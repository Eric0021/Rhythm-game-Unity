using System;
using System.Collections.Generic;
using Script.Helper.Midi;
using UnityEngine;
using Random = System.Random;

namespace Script.playscreen.NotesStrategy{
    public class RandomSingleNotesStrategy : INoteStrategy{
        public SortedList<Double, List<string>> GetNotes(Difficulty difficulty) {
            // Returns a sorted list of notes in music note form (not numeric number).
            
            int min = 0;
            int max = 0;
            switch (difficulty) {
                case Difficulty.Easy:
                    // C5 -> B#5.
                    min = 60;
                    max = 72;
                    break;
                case Difficulty.Medium:
                    // C5 -> B#6.
                    min = 60;
                    max = 84;
                    break;
                case Difficulty.Hard:
                    // C3 -> B#5.
                    min = 48;
                    max = 84;
                    break;
            }
            
            MidiParseHandler midiParseHandler = new MidiParseHandler();
            SortedList<Double, List<string>> notes = midiParseHandler.Parse("SecretGardenTest");

            Random random = new Random();
            SortedList<Double, List<string>> newNotes = new SortedList<double, List<string>>();
            foreach (KeyValuePair<Double, List<string>> pair in notes) {
                newNotes.Add(pair.Key, new List<string>(new string[]{NoteTranslator.translate(random.Next(min, max).ToString())}));
            }

            return newNotes;
        }
    }
}