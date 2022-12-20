using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreathTween : MonoBehaviour
{
    // Start is called before the first frame update

    private Tweener tween;
    private Transform finger;
    public void Start()
    {


        //tween.Pause();
        // tween.SetAutoKill(false);

        //tween.Play();
    }

    public void BeginBreath(int type)
    {
        Vector3 pos = transform.position;
        if (type == 0)
        {
            tween = transform.DOLocalMoveX(pos.x + 10, 1).SetLoops(-1, LoopType.Yoyo);
        }
        else
        {
            tween = transform.DOLocalMoveX(pos.z + 10, 1).SetLoops(-1, LoopType.Yoyo);
        }
    }

    // 正放动画效果
    public void Forward()
    {
        tween.PlayForward();
    }
    // 倒放动画效果
    public void Back()
    {
        tween.PlayBackwards();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
