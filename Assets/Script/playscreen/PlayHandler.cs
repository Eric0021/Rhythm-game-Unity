using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Script.playscreen{
    public class PlayHandler : MonoBehaviour{
        // Start is called before the first frame update
        void Start() {
            StartCoroutine(CountDownThenAttachScripts());
        }

        IEnumerator CountDownThenAttachScripts() {
            var countDownManager = FindObjectOfType<CountDownManager>();
            
            yield return StartCoroutine(countDownManager.CountDown());

            AttachScripts();
        }

        private void AttachScripts() {
            GameObject panel = GameObject.Find("Panel");
            panel.AddComponent<BeatSpawnHandler>();
            panel.AddComponent<PlayerPress>();
        }
    }
}