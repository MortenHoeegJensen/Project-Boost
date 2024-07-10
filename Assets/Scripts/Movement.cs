using UnityEngine;

public class Movement : MonoBehaviour
{

    Rigidbody rigidbody;
    [SerializeField]
    float mainThrust = 1000.0f;
    [SerializeField]
    float rotationThrust = 100f;

    [SerializeField]
    AudioClip mainEngine;

    [SerializeField]
    AudioSource audioSource;

    [SerializeField]
    ParticleSystem boosterParticle;

    [SerializeField]
    ParticleSystem leftBooster;

    [SerializeField]
    ParticleSystem rightBooster;

    bool isTransitioning = false;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        // Nedenstående fungerer ik, men ved ikke om det er fordi AudioSource ligger på mainCamera, eller så har jeg overset noget?
        //audioSource = GetComponent<AudioSource>(); // Har også lavet forbindelsen inde i unity, så den her er måske unødvendig
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.W))
        {
            rigidbody.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime); // Time.deltaTime gør det frame independent

            if (!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(mainEngine);
            }

            if (!boosterParticle.isPlaying)
            {
                boosterParticle.Play();
            }

        }
        else
        {
            audioSource.Stop();
            boosterParticle.Stop();
        }
    }

    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            ApplyRotation(rotationThrust);
            if (!rightBooster.isPlaying)
            {
                rightBooster.Play();
            }
           

        }

        else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            ApplyRotation(-rotationThrust);
            if (!leftBooster.isPlaying)
            {
                leftBooster.Play();
            }
        }
        else
        {
            leftBooster.Stop();
            rightBooster.Stop();
        }
    }

    private void ApplyRotation(float rotationThisFrame)
    {
        rigidbody.freezeRotation = true; // freezing rotation so we can manually rotate
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime); // vector3(x,y,z)
        rigidbody.freezeRotation = false; // Physics system takes over
    }
}
