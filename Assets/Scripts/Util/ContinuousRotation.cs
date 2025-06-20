using UnityEngine;

public class ContinuousRotation : MonoBehaviour
{
    public float rotationSpeed = 100f; // �������� �������� (������� � �������)

    void Update()
    {
        // ������� ������ �� ��� Z
        transform.Rotate(0f, 0f, rotationSpeed * Time.deltaTime);
    }
}
