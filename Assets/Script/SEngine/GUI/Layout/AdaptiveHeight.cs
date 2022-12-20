using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdaptiveHeight : MonoBehaviour
{

    private RectTransform root;
    private RectTransform content;
    public void Start()
    {
        root = this.GetComponent<RectTransform>();
        content = this.transform.Find("content").GetComponent<RectTransform>();
        if (content)
            root.sizeDelta = new Vector2(root.rect.width, root.rect.height);
    }

    public void SetHeight(float height)
    {
        if (content)
        {
            root.sizeDelta = new Vector2(root.rect.width,height + root.rect.height); 
        }
    }

    public void RefreshRootHight()
    {
        if (content)
            root.sizeDelta = new Vector2(content.rect.width, content.rect.height);
    }
}
