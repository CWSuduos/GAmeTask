using UnityEngine;

public class TrailEffect : MonoBehaviour
{
    public Color trailColor = Color.white; // ���� �����
    public float trailTime = 2f; // ����� ����� �����, ��� ����������� � ������ �����

    private TrailRenderer trailRenderer;

    void Start()
    {
        // ��������� ��� �������� ��������� TrailRenderer
        trailRenderer = gameObject.AddComponent<TrailRenderer>();
        trailRenderer.time = trailTime;
        trailRenderer.startWidth = 0.1f; // ��������� ������ �����
        trailRenderer.endWidth = 0f; // �������� ������ �����, ����� ���� ������ �������
        trailRenderer.material = new Material(Shader.Find("Sprites/Default")); // ���������� ����������� ������ ��� ��������
        trailRenderer.startColor = trailColor;
        trailRenderer.endColor = new Color(trailColor.r, trailColor.g, trailColor.b, 0f); // ����� ����� ����� ����������
    }

}