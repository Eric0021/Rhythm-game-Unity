using UnityEngine;
using UnityEngine.UI;

public class swipe_menu : MonoBehaviour
{
    public GameObject scrollbar;
    public GameObject scrollRect;
    private float scroll_pos = 0;
    float[] pos;
    private float centre_pos;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        pos = new float[transform.childCount];
        float distance = 1f / (pos.Length - 0.8f);
        for (int i = 0; i < pos.Length; i++)
        {
            pos[i] = distance * i;
        }
        
        // for snapping the scroll view to a particular button. 
        scroll_pos = scrollbar.GetComponent<Scrollbar>().value;
        if(!Input.GetMouseButton(0))
        {
            // print(scrollRect.GetComponent<ScrollRect>().velocity.x.ToString());
            if (Mathf.Abs(float.Parse(scrollRect.GetComponent<ScrollRect>().velocity.x.ToString())) < 50) {
                for (int i = 0; i < pos.Length; i++)
                {
                    if (scroll_pos < pos[i] + (distance / 2) && scroll_pos > pos[i] - (distance / 2))
                    {
                        scrollbar.GetComponent<Scrollbar>().value = Mathf.Lerp(scrollbar.GetComponent<Scrollbar>().value, pos[i], 0.3f);
                    }
                }
            }
        }
        
        for (int i = 0; i < pos.Length; i++)
        {
            if (scroll_pos < pos[i] + (distance / 2) && scroll_pos > pos[i] - (distance / 2)) {
                // enlarge the button that is selected.
                transform.GetChild(i).localScale = Vector2.Lerp(transform.GetChild(i).localScale, new Vector2(1.1f, 1.1f), 0.1f);
                for (int j = 0; j < pos.Length; j++)
                {
                    // shrink any buttons not selected.
                    if (j!=i)
                    {
                        transform.GetChild(j).localScale = Vector2.Lerp(transform.GetChild(j).localScale, new Vector2(0.8f, 0.8f), 0.1f);
                    }
                }
            }
        }

    }
}