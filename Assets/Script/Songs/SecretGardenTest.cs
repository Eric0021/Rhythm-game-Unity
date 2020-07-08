using UnityEngine;

namespace Script.Songs{
    public class SecretGardenTest : ISong{
        public string GetName() {
            return "SecretGardenTest";
        }

        public int GetDuration() {
            throw new System.NotImplementedException();
        }

        public AudioClip GetAudioClip() {
            AudioClip audioClip = Resources.Load<AudioClip>("SongLib/SecretGardenTest");
            return audioClip;
        }

        public float GetBpm() {
            return 55;
        }

        public float GetOffSet() {
            // wait time before beats start spawning.
            return 0.2f;
        }
    }
}