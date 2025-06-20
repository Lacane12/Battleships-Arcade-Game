using UnityEngine;
using UnityEngine.Events;

public class TurretController : MonoBehaviour
{
    public float rotationSpeed = 30f;
    public float maxRotationAngle = 50f;  // �������� 50 �������� ����� � ������
    public float xRot = -6f;
    public UnityEvent Fire;  // ������� ��� ��������

    private float currentRotation = 0f;

    void Update()
    {
        // �������� ���� �� ������
        float horizontalInput = Input.GetAxis("Horizontal");  // A/D ��� �������

        // ������������ ������
        float rotationAmount = horizontalInput * rotationSpeed * Time.deltaTime;
        currentRotation = Mathf.Clamp(currentRotation + rotationAmount, -maxRotationAngle, maxRotationAngle);
        transform.localRotation = Quaternion.Euler(xRot, currentRotation, 0f);

        // �������
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Fire.Invoke();
        }
    }
}
