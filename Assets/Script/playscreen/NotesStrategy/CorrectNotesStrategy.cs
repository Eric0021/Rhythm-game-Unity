﻿using System;
using System.Collections.Generic;

namespace Script.playscreen.NotesStrategy{
    public class CorrectNotesStrategy : INoteStrategy{
        public SortedList<Double, List<string>> GetNotes() {
            MidiParseHandler midiParseHandler = new MidiParseHandler();
            return midiParseHandler.Parse("SecretGardenTest");
        }
    }
}