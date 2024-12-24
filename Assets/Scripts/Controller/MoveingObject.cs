using UnityEngine;

public class MovingObject : MonoBehaviour
{
    public float initialSpeed = 2f;
    public float acceleration = 0.1f;
    public float yBoundary = -10f;

    private float currentSpeed;
    private bool isStopped = false; // ����, �����������, ���������� �� ������

    void Start()
    {
        currentSpeed = initialSpeed;
    }

    void Update()
    {
        if (!isStopped)
        {
            // ������� ������ ����, ������ ���� �� �� ����������
            transform.Translate(Vector3.down * currentSpeed * Time.deltaTime);

            // �������� ������
            currentSpeed += acceleration * Time.deltaTime;

            // ���������, ������ �� ������ ������� �� ��� Y
            if (transform.position.y <= yBoundary)
            {
                Destroy(gameObject);
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        // ���������, ���������� �� ������ � ������ MovingObject ��� � ����� ������ ��������, ���� ��� ����������
        if (collision.gameObject.GetComponent<MovingObject>() != null)
        {
            isStopped = true; // ������������� ������ ��� ������������
            // ����� ����� �������� �������������� �������� ��� ������������, ���� �����
        }
    }
}