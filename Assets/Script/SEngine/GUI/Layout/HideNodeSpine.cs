using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideNodeSpine : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
      
    }
   public void HidChildSpine()
    {
       //SkeletonAnimation[] tras = gameObject.transform.GetComponentsInChildren<SkeletonAnimation>();
       SkeletonGraphic[] tras = gameObject.transform.GetComponentsInChildren<SkeletonGraphic>();
        for (int i = 0; i < tras.Length; i++)
        {

            tras[i].Skeleton.A = 0.1f;

            Debug.Log(tras[i].Skeleton.A);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}