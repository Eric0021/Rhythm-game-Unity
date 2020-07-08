using Melanchall.DryWetMidi.Core;

namespace Script.playscreen.Observer{
    public interface IObserver{
        void Update(NoteOnEvent e);
    }
}