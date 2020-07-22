using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Script.SongSelectionScreen{
    public class DifficultyClick : MonoBehaviour{
        public Transform panelTransform;
        private List<GameObject> _buttons = new List<GameObject>();
        private void Start() {
            // add all the difficulty buttons into the _buttons list.
            // Call OnClick whenever any of these buttons are clicked.
            
            for (int i = 0; i < panelTransform.childCount; i++) {
                var child = panelTransform.GetChild(i);
                if (child.tag.Equals("DifficultyButton")) {
                    _buttons.Add(child.gameObject);
                }
            }
            
            foreach (GameObject button in _buttons) {
                button.GetComponent<Button>().onClick.AddListener(OnClick);
            }
        }
    
        private void OnClick() {
            // Enlarge the selected difficulty button
            // fade out the other ones.
            
            var clickedButton = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject;
            EnlargeAndFocus(clickedButton);

            foreach (GameObject button in _buttons) {
                if (!button.GetComponent<Button>().Equals(clickedButton.GetComponent<Button>())) {
                    // fade out the button.
                    ShrinkBackAndFadeOut(button);
                }
            }
        }

        private void ShrinkBackAndFadeOut(GameObject button) {
            button.transform.localScale = new Vector2(1f, 1f);
            
            Color color = button.transform.GetChild(0).GetComponent<Image>().color;
            color.a = 0.5f;
            button.transform.GetChild(0).GetComponent<Image>().color = color;
        }

        private void EnlargeAndFocus(GameObject button) {
            button.transform.localScale = new Vector2(1.19f, 1.19f);
            
            Button buttonObj = button.GetComponent<Button>();
            Color tempColor = buttonObj.transform.GetChild(0).GetComponent<Image>().color;
            tempColor.a = 1f;
            button.GetComponent<Button>().transform.GetChild(0).GetComponent<Image>().color = tempColor;
        }
    }
}