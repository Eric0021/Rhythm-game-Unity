using System;
using Script.playscreen.Observer;
using UnityEngine;

namespace Script.playscreen{
    public class Fall : MonoBehaviour{
        public float secPerBeat = -5000;
        public float speedMultiplier = -5000;
        public float barHeight = -5000;

        private void Update() {
            // only start making the beats fall if the variables are initialised.
            if (!secPerBeat.Equals(-5000) && !speedMultiplier.Equals(-5000) && !barHeight.Equals(-5000)) {
                transform.position -= new Vector3(0f, barHeight / (4 * secPerBeat) * Time.deltaTime * speedMultiplier);
            }

            if (transform.position.y < -50) {
                Destroy(gameObject);
            }
        }
    }
}