using UnityEngine;

public class ContinuousRotation : MonoBehaviour
{
    public float rotationSpeed = 100f; // Скорость вращения (градусы в секунду)

    void Update()
    {
        // Вращаем объект по оси Z
        transform.Rotate(0f, 0f, rotationSpeed * Time.deltaTime);
    }
}
