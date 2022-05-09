using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadGenerator : MonoBehaviour
{
    public GameObject player;
    public GameObject[] roadPref;
    public List<GameObject> roads = new List<GameObject>();

    private void Update()
    {
        if (player == null) return;
        int rand = Random.Range(0, roadPref.Length);
        GenerateRoad(rand);
    }
    private void GenerateRoad(int rand)
    {
        if (roads.Count < 6)
        {
            CreateRoad(rand);
        }
        if (player.transform.position.z - roads[0].transform.position.z > 14) //���� ������� ������ �� ������ ������������ ��������� ������ 14 �� �� ������� � ������� �����
        {
            Destroy(roads[0]);
            roads.RemoveAt(0);
        }
    }
    private void CreateRoad(int rand)
    {
        Vector3 position = new Vector3(0, 0, 12.59f); //12.59 ��� ����� ���������
        if (roads.Count > 0)
        { position = roads[roads.Count - 1].transform.position + new Vector3(0, 0, 12.59f); } //����� ��������� ��������� + �� ����� � �� ��� ����� ������� �����
        GameObject road = Instantiate(roadPref[rand], position, Quaternion.identity); //��� �����, Quaternion.identity - ��� ����� � ������ rotation ��� (0,0,0) ��� ����� �������� ���
        //road.transform.SetParent(transform);
        roads.Add(road);
    }
}
