using System;
using System.Collections.Generic;
using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Devices;
using Melanchall.DryWetMidi.Interaction;

public class MidiParseHandler{
    public SortedList<Double, List<string>> Parse(String songName) {
        var songMidi = MidiFile.Read("Assets\\Resources\\Midi\\"+songName+".mid");
        
        SortedList<Double, List<string>> beats = new SortedList<double, List<string>>();

        foreach(ITimedObject note in songMidi.GetTimedEventsAndNotes()) {
            // skip the opening events that are not notes.
            if (note.ToString().Contains("Event")) {
                continue;
            }
            
            // this is assuming the MIDI file is 384 ticks/beat.
            double beat = note.Time/384f;
            if (!beats.ContainsKey(beat)) {
                List<String> list = new List<string>(new []{note.ToString()});
                beats.Add(beat, list);
            }
            else {
                beats[beat].Add(note.ToString());
            }
        }


        return beats;
    }
}