using UnityEngine;
using UnityEngine.Events;

public class TorpedoLauncher : MonoBehaviour
{
    public GameObject torpedoPrefab;  // Префаб торпеды
    public Transform launchPoint;     // Точка запуска торпед
    public AnimationCurve speedCurve; // Кривая скорости
    public float cooldown = 2f;       // Кулдаун перед следующим запуском
    public UnityEvent OnTorpedoLaunched; // Событие запуска торпеды

    private bool canLaunch = true; // Контроль кулдауна
    private TurretController turretController;

    void Start()
    {
        turretController = FindObjectOfType<TurretController>(); // Получаем доступ к турели
        turretController.Fire.AddListener(LaunchTorpedo);
    }


    public void LaunchTorpedo()
    {
        if (canLaunch)
        {
            // Создаем торпеду и устанавливаем её позицию и направление
            GameObject torpedo = Instantiate(torpedoPrefab, launchPoint.position, launchPoint.rotation);
            TorpedoController torpedoController = torpedo.GetComponent<TorpedoController>();

            // Передаем кривую скорости в контроллер торпеды
            torpedoController.SetSpeedCurve(speedCurve);

            OnTorpedoLaunched.Invoke();  // Вызываем событие запуска

            // Начинаем кулдаун
            canLaunch = false;
            Invoke(nameof(ResetCooldown), cooldown);
        }
    }

    private void ResetCooldown()
    {
        canLaunch = true;
    }
}
