using System;
using System.Collections.Generic;
using Script.Helper.Midi;

namespace Script.playscreen.NotesStrategy{
    public class RandomSingleNotesStrategy : INoteStrategy{
        public SortedList<Double, List<string>> GetNotes() {
            // returns a list of randomised notes, between C4 -> B#6.
            MidiParseHandler midiParseHandler = new MidiParseHandler();
            SortedList<Double, List<string>> notes = midiParseHandler.Parse("SecretGardenTest");

            Random random = new Random();
            SortedList<Double, List<string>> newNotes = new SortedList<double, List<string>>();
            foreach (KeyValuePair<Double, List<string>> pair in notes) {
                newNotes.Add(pair.Key, new List<string>(new string[]{NoteTranslator.translate(random.Next(48, 72).ToString())}));
            }

            return newNotes;
        }
    }
}