using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadUpStealth : MonoBehaviour
{
    public GameObject eyePrefab;
    public GameObject exclamPrefab;

    public Canvas stealthCanvas;
    public Camera mainCamera;

    private RectTransform rectParent; //�θ��� rectTransform ������ ������ ����
    private RectTransform rectHp; //�ڽ��� rectTransform ������ ����

    //HideInInspector�� �ش� ���� �����, ���� ������ �ʿ䰡 ���� �� 
    public Vector3 offset = Vector3.zero; //HpBar ��ġ ������, offset�� ��� HpBar�� ��ġ �������
    public Transform enemyTr; //�� ĳ������ ��ġ

    //LateUpdate�� update ���� ������, ���� �������� Update���� ����Ǵ� ������ ���Ŀ� HpBar�� �����
    private void LateUpdate()
    {
        var screenPos = Camera.main.WorldToScreenPoint(enemyTr.position + offset); //������ǥ(3D)�� ��ũ����ǥ(2D)�� ����, offset�� ������Ʈ �Ӹ� ��ġ


        var localPos = Vector2.zero;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(rectParent, screenPos, mainCamera, out localPos); //��ũ����ǥ���� ĵ�������� ����� �� �ִ� ��ǥ�� ����?

        rectHp.localPosition = localPos; //�� ��ǥ�� localPos�� ����, �ű⿡ hpbar�� ���
    }
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
