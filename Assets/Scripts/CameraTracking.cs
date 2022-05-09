using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTracking : MonoBehaviour
{
    public Transform player;
    private Vector3 offset;
    private Vector3 newPosition;
    private float height;

    private void Start()
    {
        offset = transform.position - player.position; //����������� ���������� �� ������ �� ������ ����� �� ���� �� ���������� �� ������ ��������� ����� �� �������� �����
    }
    private void FixedUpdate()
    {  
        if (player == null) return;         //���� ����� ����� �� ������ ��������� �� ��� ������� ����� ������ ������ ����� ������
        height = player.position.y + 3.83f; //��� ������ ������ �� ������� ������ + �������� ��������
        newPosition = new Vector3(player.position.x, height, offset.z + player.position.z);//���� ������ ���������� ���������� ��� XYZ
        transform.position = newPosition;   //���������� ���� ������
    }
}
