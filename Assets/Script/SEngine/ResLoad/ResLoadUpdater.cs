using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SEngine
{

    public class ResLoadUpdater : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            ResLoadManager.Instance.Update();
        }
    }

}
