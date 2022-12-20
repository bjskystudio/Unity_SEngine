using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// �ò��ɼ��Ľ�����Ӧ�ؼ���������ƣ������Կ���Դ����
/// </summary>
public class EmptyGraphic : Graphic { 
    public bool m_Maskable = true;

    public EmptyGraphic()
    {
        useLegacyMeshGeneration = false;
    }

    protected override void OnPopulateMesh(VertexHelper vh)
    {
        //base.OnPopulateMesh(vh);
        vh.Clear();
    }
}
