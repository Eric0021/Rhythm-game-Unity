namespace Script.Helper.Midi{
    public class NoteTranslator{
        public static string translate(string noteString) {
            string notes = "C C#D D#E F F#G G#A A#B ";
            int noteNum = 0;
            noteNum = int.Parse(noteString);
            string noteResult;
            int octave = noteNum / 12;
            noteResult = notes.Substring((noteNum % 12) * 2,  2);
            if (noteResult[1] == ' ') {
                // remove the space if the note is not a sharp.
                noteResult = noteResult[0].ToString();
            }

            return noteResult + octave;
        }
    }
}