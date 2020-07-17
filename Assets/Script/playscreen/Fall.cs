using System;
using Script.playscreen.Observer;
using UnityEngine;

namespace Script.playscreen{
    public class Fall : MonoBehaviour{
        public float secPerBeat = -5000;
        public float speedMultiplier = -5000;
        public float barHeight = -5000;

        private void Update() {
            // Using anchoredPosition here, maybe bad practice, fix for performance later.
            
            // only start making the beats fall if the variables are initialised.
            if (!secPerBeat.Equals(-5000) && !speedMultiplier.Equals(-5000) && !barHeight.Equals(-5000)) {
                var fallSpeed = barHeight / (4 * secPerBeat) * Time.deltaTime * speedMultiplier;
                GetComponent<RectTransform>().anchoredPosition -= new Vector2(0f, fallSpeed);
            }

            var bottomY = -1080;
            if (GetComponent<RectTransform>().anchoredPosition.y < bottomY-50) {
                Destroy(gameObject);
                PlayerPressHandler.RemoveBeat(gameObject);
            }
        }
    }
}