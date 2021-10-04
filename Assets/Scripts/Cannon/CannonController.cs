using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonController : MonoBehaviour
{
    [SerializeField]
    private float rotationSpeed = 1;
    [SerializeField]
    public float BlastPower = 5;
    [SerializeField]
    private GameObject Explosion;

    [SerializeField]
    private GameObject Cannonball;
    [SerializeField]
    public Transform ShotPoint;

    [SerializeField]
    private PlayerInput playerInput = null;
    [SerializeField]
    private float minZRotation = 0.0f;
    [SerializeField]
    private float maxZRotation = 0.0f;

    private void Update()
    {
        float horizontalRotation = playerInput.MouseX; // Input.GetAxis("Horizontal");
        float verticalRotation = -playerInput.MouseY; // Input.GetAxis("Vertical");

        Vector3 desiredAngles = transform.rotation.eulerAngles +
            (new Vector3(0, horizontalRotation, verticalRotation) * rotationSpeed);
        desiredAngles.z = Mathf.Clamp(desiredAngles.z, minZRotation, maxZRotation);

        Quaternion desiredRotation = Quaternion.Euler(desiredAngles);
        transform.rotation = desiredRotation;
    }

    public void Shoot()
    {
        GameObject CreatedCannonball = Instantiate(Cannonball, ShotPoint.position, ShotPoint.rotation);
        CreatedCannonball.GetComponent<Rigidbody>().velocity = ShotPoint.transform.up * BlastPower;

        // Added explosion for added effect
        Destroy(Instantiate(Explosion, ShotPoint.position, ShotPoint.rotation), 2);

        // Shake the screen for added effect
        Screenshake.ShakeAmount = 5;
    }

}
