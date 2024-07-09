using UnityEngine;

public class Movement : MonoBehaviour
{

    Rigidbody rigidbody;
    [SerializeField]
    float mainThrust = 1000.0f;
    [SerializeField]
    float rotationThrust = 100f;

    [SerializeField]
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        //audioSource = GetComponent<AudioSource>(); // Har ogs� lavet forbindelsen inde i unity, s� den her er m�ske un�dvendig
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            rigidbody.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime); // Time.deltaTime g�r det frame independent

            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }


            //Debug.Log("Pressed Space");
        }
        else
        {
            audioSource.Stop();
        }
    }

    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            ApplyRotation(rotationThrust);
            //Debug.Log("Pressed Left");
        }

        else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            ApplyRotation(-rotationThrust);
            //Debug.Log("Pressed Right");
        }
    }

    private void ApplyRotation(float rotationThisFrame)
    {
        rigidbody.freezeRotation = true; // freezing rotation so we can manually rotate
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime); // vector3(x,y,z)
        rigidbody.freezeRotation = false; // Physics system takes over
    }
}
