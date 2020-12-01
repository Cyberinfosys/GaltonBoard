using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GaltonBoard : MonoBehaviour
{
    class Bar
    {
        public RectTransform rect;
        public Text text;
        public int height;
        public Bar(RectTransform rect, Text text, int height)
        {
            this.rect = rect;
            this.text = text;
            this.height = height;
        }
    }
    public GameObject prefab;
    public int time = 1;
    public int levelCount = 1;
    Dictionary<int, Bar> results = new Dictionary<int, Bar>();
    
    // Start is called before the first frame update
    void Start()
    {
        for (int i = -levelCount; i <= levelCount; i += 2)
        {
            //1     +1 -1      2
            //2   +2  0 -2     3
            //3  +3 +1 -1 -3   4
            GameObject go = Instantiate(prefab, transform);
            go.name = i.ToString();
            results.Add(i, new Bar(go.GetComponent<RectTransform>(), go.GetComponentInChildren<Text>(), 0));
        }
        Drop();
        Draw();
    }

    void Draw()
    {
        if (results.Count > 0)
        {
            foreach (int key in results.Keys)
            {
                Vector2 vector2 = results[key].rect.sizeDelta;
                vector2.y = results[key].height * 2;
                results[key].rect.sizeDelta = vector2;
                results[key].text.text = results[key].height.ToString();
            }
        }
    }

    void Drop()
    {
        for (int i = 0; i < time; i++)
        {
            int value = 0;
            for (int j = 0; j < levelCount; j++)
            {
                bool left = Random.Range(0, 2) == 1 ? true : false;
                if (left)
                    value++;
                else
                    value--;
            }
            results[value].height++;
        }
    }
}
