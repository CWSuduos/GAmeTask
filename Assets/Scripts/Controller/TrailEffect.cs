using UnityEngine;

public class TrailEffect : MonoBehaviour
{
    public Color trailColor = Color.white; // Цвет следа
    public float trailTime = 2f; // Время жизни следа, что коррелирует с длиной следа

    private TrailRenderer trailRenderer;

    void Start()
    {
        // Добавляем или получаем компонент TrailRenderer
        trailRenderer = gameObject.AddComponent<TrailRenderer>();
        trailRenderer.time = trailTime;
        trailRenderer.startWidth = 0.1f; // Начальная ширина следа
        trailRenderer.endWidth = 0f; // Конечная ширина следа, чтобы след плавно исчезал
        trailRenderer.material = new Material(Shader.Find("Sprites/Default")); // Используем стандартный шейдер для спрайтов
        trailRenderer.startColor = trailColor;
        trailRenderer.endColor = new Color(trailColor.r, trailColor.g, trailColor.b, 0f); // Конец следа будет прозрачным
    }

}