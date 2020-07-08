using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Melanchall.DryWetMidi.Interaction;
using Script.playscreen.NotesStrategy;
using Script.Songs;
using UnityEngine;
using UnityEngine.UI;
using Random = Unity.Mathematics.Random;

namespace Script.playscreen{
    public class BeatSpawnHandler : MonoBehaviour{
        private GameObject column;
        private AudioSource music;
        private ISong song;
        private SortedList<Double, List<string>> notes;
        private INoteStrategy _noteStrategy;
        private bool ready;

        private float beatWidth;
        private float beatHeight;

        private double songPositionInBeats;
        private float secPerBeat;
        private float speedMultiplier = 0.7f;
        private float barHeight;

        private int currentNoteIndex = 0;
        private double startingTime;
        private double offsetTime = 0;

        // This script only starts when the note strategy has been set.
        public void SetNoteStrategy(INoteStrategy strategy) {
            _noteStrategy = strategy;
            
            InitialiseObjects();
            SetBeatDim();
            SetMissHeight();

            startingTime = DateTimeOffset.Now.ToUnixTimeMilliseconds();
            ready = true;
            
            StartCoroutine(PlaySong());
        }
        private void SetMissHeight() {
            var button = GameObject.Find("Button");
            PlayerPress.SetMissHeight(button.transform.position.y - button.GetComponent<RectTransform>().rect.height/2);
        }
        
        public void SpawnBeats() {
            double nextNoteBeat = notes.Keys[currentNoteIndex];

            if (songPositionInBeats >= nextNoteBeat) {
                currentNoteIndex++;
                List<string> notesOnBeat = notes.Values[currentNoteIndex];

                int random = UnityEngine.Random.Range(1, 5);
                MakeBeat(random, notesOnBeat);
            }
        }

        private void MakeBeat(int colNum, List<String> notesOnBeat) {
            GameObject column = GetColumn(colNum);
            GameObject beat = new GameObject();
            
            SetTag(beat);
            SetRect(beat, column);
            SetBeatImage(beat);
            SetRigidBody(beat);
            SetText(notesOnBeat, beat.GetComponent<Image>());
            SetCircleCollider(beat);

            AttachScript(beat);
        }

        private void SetTag(GameObject beat) {
            beat.tag = "beat";
        }

        private void SetCircleCollider(GameObject beat) {
            CircleCollider2D collider = beat.AddComponent<CircleCollider2D>();
            collider.radius = collider.GetComponent<RectTransform>().rect.height / 2;
        }

        private void AttachScript(GameObject beat) {
            // attach the fall script to the beat.
            Fall fallScript = beat.AddComponent<Fall>();
            fallScript.speedMultiplier = speedMultiplier;
            fallScript.secPerBeat = secPerBeat;
            fallScript.barHeight = barHeight;

            beat.AddComponent<BeatCollider>();
        }

        private GameObject GetColumn(int colNum) {
            switch (colNum) {
                case 1:
                    return GameObject.Find("column1");
                case 2:
                    return GameObject.Find("column2");
                case 3:
                    return GameObject.Find("column3");
                case 4:
                    return GameObject.Find("column4");
                default:
                    return null;
            }
        }

        IEnumerator PlaySong() {
            // play the song after a delay, so that beats can spawn first.
            
            var delay = secPerBeat * 4 * (1 / speedMultiplier);
            yield return new WaitForSeconds(delay);

            music.Play();
        }

        private void InitialiseObjects() {
            InitialiseSong();
            InitialiseNotes();

            column = GameObject.Find("column1");
            secPerBeat = 60f / song.GetBpm();
            barHeight = 1080 - Math.Abs(GameObject.Find("Line").transform.position.y);
        }

        private void InitialiseNotes() {
            notes = _noteStrategy.GetNotes();
        }

        private void InitialiseSong() {
            var musicObj = GameObject.Find("Song");
            music = musicObj.GetComponent<AudioSource>();

            song = new SecretGardenTest();
            AudioClip audioClip = song.GetAudioClip();

            music.clip = audioClip;
        }

        private void SetText(List<String> notesOnBeat, Image beat) {
            GameObject text = new GameObject();
            Text beatText = text.AddComponent<Text>();

            int count = 0;
            string noteStringFull = "";
            foreach (string noteString in notesOnBeat) {
                if (count != 0) {
                    noteStringFull += "+";
                }

                noteStringFull += noteString;
                count++;
            }

            beatText.text = noteStringFull;
            beatText.color = Color.white;
            beatText.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
            
            text.transform.SetParent(beat.transform);

            RectTransform textRect = text.GetComponent<RectTransform>();
            textRect.anchoredPosition = new Vector3(6, 0);
            textRect.sizeDelta = new Vector2(100, 20);
            beatText.alignment = TextAnchor.MiddleCenter;
        }

        private void SetBeatImage(GameObject beat) {
            Image beatImage = beat.AddComponent<Image>();
            beatImage.sprite = Resources.Load<Sprite>("Images/beat");
        }

        private void SetRigidBody(GameObject beat) {
            Rigidbody2D rigidbody2D = beat.AddComponent<Rigidbody2D>();
            rigidbody2D.bodyType = RigidbodyType2D.Kinematic;
        }

        private void SetRect(GameObject beat, GameObject column) {
            RectTransform trans = beat.AddComponent<RectTransform>();

            // set the column as beat's parent.
            trans.transform.SetParent(column.transform);
            trans.localScale = Vector2.one;

            Vector2 dim = new Vector2(beatWidth, beatHeight);
            trans.sizeDelta = dim;

            trans.anchoredPosition = new Vector2(100f, -19f);
            trans.anchorMin = new Vector2(0, 1);
            trans.anchorMax = new Vector2(0, 1);
        }

        private void SetBeatDim() {
            // +10 to adjust for the empty space on the beatImage's sides.
            beatWidth = column.GetComponent<RectTransform>().rect.size.x + 5;
            beatHeight = 45;
        }

        private void Update() {
            // only start updating when note strategy has been set.
            if (!ready) {
                return;
            }
            
            // how many milliseconds has the song started.
            var songPosition = (music.time - song.GetOffSet()) * 1000;

            // spawning beats before the song starts.
            if (music.time.Equals(0)) {
                offsetTime = DateTimeOffset.Now.ToUnixTimeMilliseconds() - startingTime;
            }

            // offsetTime and songPosition are both in milliseconds.
            songPositionInBeats = ((songPosition + offsetTime) / secPerBeat) / 1000;

            SpawnBeats();
        }
    }
}