using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Script.playscreen{
    public class CountDownManager : MonoBehaviour{
        private bool finished = false;
        
        public IEnumerator CountDown() {
            var countDown = GameObject.Find("countDown");

            yield return StartCoroutine(ShowAndHide(countDown, 3f));
        }

        IEnumerator ShowAndHide(GameObject countDown, float duration) {
            // count down from duration, wait a second in between each count.
            var countText = countDown.GetComponent<Text>();
            countText.enabled = true;
            
            countText.enabled = true;
            for (var count = 3; count > 0; count--) {
                countText.text = count.ToString();
                yield return new WaitForSeconds(1);
            }
            
            countText.enabled = false;
            finished = true;
        }

        public bool Finished {
            get => finished;
            set => finished = value;
        }
    }
}