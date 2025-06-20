using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class TorpedoController : MonoBehaviour
{
    
    public ParticleSystem explosionParticles;
    public float maxDistance = 700f; // Максимальное расстояние до уничтожения торпеды
    public UnityEvent OnTorpedoDestroyed; // Событие уничтожения торпеды

    private Rigidbody rb;
    private AnimationCurve speedCurve; // Кривая скорости
    private Vector3 startPosition;
    private float launchTime;
    bool isMoving = true;
    
    

    void Start()
    {
        startPosition = transform.position;
        rb = GetComponent<Rigidbody>();
        launchTime = Time.time;
    }

    void Update()
    {
        if (!isMoving)
            return;

        // Рассчитываем время полета
        float timeSinceLaunch = Time.time - launchTime;

        // Скорость торпеды на основе кривой
        float speed = speedCurve.Evaluate(timeSinceLaunch);

        // Движение торпеды вперед
        rb.velocity = transform.forward * speed * Time.deltaTime;

        // Проверка расстояния
        if (Vector3.Distance(startPosition, transform.position) > maxDistance)
        {
            OnTorpedoDestroyed.Invoke();
            Destroy(gameObject);
        }
    }

    public void SetSpeedCurve(AnimationCurve curve)
    {
        speedCurve = curve; // Устанавливаем кривую скорости
    }

    public void Explosion() 
    {
        rb.velocity = Vector3.zero;
        StartCoroutine(destroySequence());
    }

    IEnumerator destroySequence() 
    {
        explosionParticles.Play();
        OnTorpedoDestroyed.Invoke();
        isMoving = false;
        yield return new WaitForSeconds(4f);
        Destroy(gameObject);
    }
}
