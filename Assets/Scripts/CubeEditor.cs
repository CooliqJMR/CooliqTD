using UnityEngine;

[ExecuteInEditMode] //Позволяет работать скриптам в режиме разработки, а не игры.
[RequireComponent(typeof(WayPoint))] //Требование WayPoint на объекте.
public class CubeEditor : MonoBehaviour
{
    WayPoint wayPoint;

    private void Awake()
    {
        wayPoint = GetComponent<WayPoint>(); //Получаем ссылку на компонент WayPoint
    }

    void Update()
    {
        
        SnapToGrid();

        UpDateLabel();
    }

    private void UpDateLabel()
    {
        TextMesh label = GetComponentInChildren<TextMesh>(); //Получаем дочерний компонент куба.
        string labelName = $"{wayPoint.GetGritPos().x},{wayPoint.GetGritPos().y}";
        label.text = labelName; //Отображение(в единицах, а не десятках) координат куба по x и z на кубе.
        gameObject.name = labelName; //Назначаем новое имя в их координаты.
    }

    private void SnapToGrid()
    {
        int gridSize = wayPoint.GetGridSize();

        transform.position = new Vector3(wayPoint.GetGritPos().x * gridSize, 0f, wayPoint.GetGritPos().y * gridSize); //Определяем новую позицию объекта к которому привязат компонент(скрипт).
    }
}
