using UnityEngine;

public class MouseObjectGrabber : MonoBehaviour
{
    // Объект, который мы захватываем
    private GameObject grabbedObject;

    // Компонент Rigidbody захваченного объекта
    private Rigidbody grabbedRigidbody;

    // Начальная позиция объекта относительно курсора мыши
    private Vector3 initialMousePosition;

    // Начальная позиция объекта
    private Vector3 initialObjectPosition;

    void Update()
    {
        // Если левая кнопка мыши нажата
        if (Input.GetMouseButtonDown(0))
        {
            TryGrabObject();
        }
        // Если левая кнопка мыши отпущена
        else if (Input.GetMouseButtonUp(0))
        {
            ReleaseObject();
        }
        // Если левая кнопка мыши удерживается
        else if (Input.GetMouseButton(0) && grabbedObject != null)
        {
            MoveObject();
        }
    }

    // Пытаемся захватить объект
    void TryGrabObject()
    {
        // Создаем луч из экрана в точку, куда указывает мышь
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        // Проверяем, пересекает ли луч какой-либо объект
        if (Physics.Raycast(ray, out hit))
        {
            // Проверяем, есть ли у объекта Rigidbody
            grabbedRigidbody = hit.collider.GetComponent<Rigidbody>();
            if (grabbedRigidbody != null)
            {
                grabbedObject = hit.collider.gameObject;
                // Сохраняем начальную позицию мыши и объекта
                initialMousePosition = Input.mousePosition;
                initialObjectPosition = grabbedObject.transform.position;
                Debug.Log($"Захвачен объект: {grabbedObject.name}");
            }
        }
    }

    // Отпускаем объект
    void ReleaseObject()
    {
        if (grabbedObject != null)
        {
            Debug.Log($"Объект {grabbedObject.name} отпущен");
            grabbedObject = null;
            grabbedRigidbody = null;
        }
    }

    // Перемещаем объект
    void MoveObject()
    {
        // Вычисляем разницу между текущей и начальной позицией мыши
        Vector3 mouseDelta = Input.mousePosition - initialMousePosition;

        // Преобразуем разницу в мировые координаты
        Vector3 worldDelta = new Vector3(mouseDelta.x, mouseDelta.y, 0) / 100f; // Делим на 100 для масштабирования

        // Перемещаем объект относительно начальной позиции
        grabbedObject.transform.position = initialObjectPosition + worldDelta;
    }
}