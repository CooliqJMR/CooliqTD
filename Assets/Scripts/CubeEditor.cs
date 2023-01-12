using UnityEngine;

[ExecuteInEditMode] //��������� �������� �������� � ������ ����������, � �� ����.
[RequireComponent(typeof(WayPoint))] //���������� WayPoint �� �������.
public class CubeEditor : MonoBehaviour
{
    WayPoint wayPoint;

    private void Awake()
    {
        wayPoint = GetComponent<WayPoint>(); //�������� ������ �� ��������� WayPoint
    }

    void Update()
    {
        
        SnapToGrid();

        UpDateLabel();
    }

    private void UpDateLabel()
    {
        TextMesh label = GetComponentInChildren<TextMesh>(); //�������� �������� ��������� ����.
        string labelName = $"{wayPoint.GetGritPos().x},{wayPoint.GetGritPos().y}";
        label.text = labelName; //�����������(� ��������, � �� ��������) ��������� ���� �� x � z �� ����.
        gameObject.name = labelName; //��������� ����� ��� � �� ����������.
    }

    private void SnapToGrid()
    {
        int gridSize = wayPoint.GetGridSize();

        transform.position = new Vector3(wayPoint.GetGritPos().x * gridSize, 0f, wayPoint.GetGritPos().y * gridSize); //���������� ����� ������� ������� � �������� �������� ���������(������).
    }
}
