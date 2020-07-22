using UnityEngine;
using UnityEngine.UI;

namespace Script.playscreen.Score{
    public class comboHandler : MonoBehaviour{

        // Start is called before the first frame update
        void Start() {
        }

        public void Increment() {
            Bounce();
            // increment the combo count.
            Text comboCount = gameObject.transform.GetChild(0).GetComponent<Text>();
            comboCount.text = (int.Parse(comboCount.text) + 1).ToString();
        }

        public void Bounce() {
            gameObject.GetComponent<Animator>().Play("comboBounce");
        }
    }
}