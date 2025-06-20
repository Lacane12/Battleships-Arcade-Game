using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class TorpedoController : MonoBehaviour
{
    
    public ParticleSystem explosionParticles;
    public float maxDistance = 700f; // ������������ ���������� �� ����������� �������
    public UnityEvent OnTorpedoDestroyed; // ������� ����������� �������

    private Rigidbody rb;
    private AnimationCurve speedCurve; // ������ ��������
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

        // ������������ ����� ������
        float timeSinceLaunch = Time.time - launchTime;

        // �������� ������� �� ������ ������
        float speed = speedCurve.Evaluate(timeSinceLaunch);

        // �������� ������� ������
        rb.velocity = transform.forward * speed * Time.deltaTime;

        // �������� ����������
        if (Vector3.Distance(startPosition, transform.position) > maxDistance)
        {
            OnTorpedoDestroyed.Invoke();
            Destroy(gameObject);
        }
    }

    public void SetSpeedCurve(AnimationCurve curve)
    {
        speedCurve = curve; // ������������� ������ ��������
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
