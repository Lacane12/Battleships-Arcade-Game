using UnityEngine;
using UnityEngine.Events;

public class TurretController : MonoBehaviour
{
    public float rotationSpeed = 30f;
    public float maxRotationAngle = 50f;  // Максимум 50 градусов влево и вправо
    public float xRot = -6f;
    public UnityEvent Fire;  // Событие для выстрела

    private float currentRotation = 0f;

    void Update()
    {
        // Получаем ввод от игрока
        float horizontalInput = Input.GetAxis("Horizontal");  // A/D или стрелки

        // Поворачиваем турель
        float rotationAmount = horizontalInput * rotationSpeed * Time.deltaTime;
        currentRotation = Mathf.Clamp(currentRotation + rotationAmount, -maxRotationAngle, maxRotationAngle);
        transform.localRotation = Quaternion.Euler(xRot, currentRotation, 0f);

        // Выстрел
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Fire.Invoke();
        }
    }
}
