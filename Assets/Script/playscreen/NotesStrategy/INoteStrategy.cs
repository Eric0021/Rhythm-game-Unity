using System;
using System.Collections.Generic;

namespace Script.playscreen.NotesStrategy{
    // Notes on the beats can either be matching that of the song, or completely random (for note learning).
    public interface INoteStrategy{
        SortedList<Double, List<string>> GetNotes();
    }
}