using UnityEngine;
using UnityEngine.Events;

public class TorpedoLauncher : MonoBehaviour
{
    public GameObject torpedoPrefab;  // ������ �������
    public Transform launchPoint;     // ����� ������� ������
    public AnimationCurve speedCurve; // ������ ��������
    public float cooldown = 2f;       // ������� ����� ��������� ��������
    public UnityEvent OnTorpedoLaunched; // ������� ������� �������

    private bool canLaunch = true; // �������� ��������
    private TurretController turretController;

    void Start()
    {
        turretController = FindObjectOfType<TurretController>(); // �������� ������ � ������
        turretController.Fire.AddListener(LaunchTorpedo);
    }


    public void LaunchTorpedo()
    {
        if (canLaunch)
        {
            // ������� ������� � ������������� � ������� � �����������
            GameObject torpedo = Instantiate(torpedoPrefab, launchPoint.position, launchPoint.rotation);
            TorpedoController torpedoController = torpedo.GetComponent<TorpedoController>();

            // �������� ������ �������� � ���������� �������
            torpedoController.SetSpeedCurve(speedCurve);

            OnTorpedoLaunched.Invoke();  // �������� ������� �������

            // �������� �������
            canLaunch = false;
            Invoke(nameof(ResetCooldown), cooldown);
        }
    }

    private void ResetCooldown()
    {
        canLaunch = true;
    }
}
