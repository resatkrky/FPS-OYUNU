using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    [SerializeField]
    private float speed = 5f;

    [SerializeField]
    private float lookSensitivity = 3f;

    [SerializeField]
    GameObject fpsCamera; // Oyuncumuzda olan kamerayı Player Nesnesine Tıkladığımızda eklememizi sağlar.

    private Vector3 velocity = Vector3.zero;
    private Vector3 rotation = Vector3.zero;
    private float CameraUpAndDownRotation = 0f;
    private float CurrentCameraUpAndDownRotation = 0f;

    private Rigidbody rb;

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    private void Update()
    {
        //Sağa ve sola dönmeyi sağlamak
        float _xMovement = Input.GetAxis("Horizontal"); //Sag ve sol donus
        float _zMovement = Input.GetAxis("Vertical"); //İleri geri


        Vector3 _movementHorizontal = transform.right * _xMovement;
        Vector3 _movementVertical = transform.forward * _zMovement;

        //WASD Son ilerleme vektoru hareketin hızı belirlenir
        Vector3 _movementVelocity = (_movementHorizontal + _movementVertical).normalized * speed ;

        //WASD ile hareketi uygulama
        Move(_movementVelocity);

        //mouse ile hareket etmek sağ ve sola
        float _yRotation = Input.GetAxis("Mouse X");
        Vector3 _rotationVector = new Vector3(0, _yRotation, 0) * lookSensitivity;

        //Mouse ile dönmeyi uygulama sağ ve sola
        Rotate(_rotationVector);

        //Mouse ile yukarı aşağı hareket 
        float _cameraUpDownRotation = Input.GetAxis("Mouse Y") *lookSensitivity;

        //Mouse ile yukaru aşağı uygulama
        RotateCamera(_cameraUpDownRotation);

    }

    //Fiziği Çalıştırır Rigidbody
    private void FixedUpdate()
    {
        if(velocity != Vector3.zero)
        {
            rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime); //fixedDeltaTime sn başı hızlanmayı ayarlar pcye göre oyun hızı değişmez
        }

        rb.MoveRotation(rb.rotation * Quaternion.Euler(rotation)); // Mouse sağ sol hareketi için

        if(fpsCamera != null)
        {
            CurrentCameraUpAndDownRotation -= CameraUpAndDownRotation; //Camerayı yukarı aşağı kaydırmak için
            CurrentCameraUpAndDownRotation = Mathf.Clamp(CurrentCameraUpAndDownRotation, -85, 85); // MAX Yukarı ve Aşağı limiti

            fpsCamera.transform.localEulerAngles = new Vector3(CurrentCameraUpAndDownRotation, 0, 0); //Mouse yukarı aşağı hareketi için
        }
    }

    //Bunların protipleri yukarda açıklama orada var alltakilerin hepsi için
    void Move(Vector3 movementVelocity)
    {
        velocity = movementVelocity;
    }

    void Rotate(Vector3 rotationVector)
    {
        rotation = rotationVector;
    }

    void RotateCamera(float cameraUpAndDownRotation)
    {
        CameraUpAndDownRotation = cameraUpAndDownRotation;
    }
}
