using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public Transform target;
    public Transform firePoint;
    public GameObject bullet;
    public GameObject rocket;
    public RectTransform targetCrossHair;

    float shootWaitTime = 0.2f;
    float shootUnitilTime = 0.0f;

    float rocketShootWaitTime = 1.0f;
    float rocketShootUnitilWaitTime = 0.0f;

    public float bulletSpeed = 2000.0f;
    public float rocketSpeed = 700.0f;

    bool rocketIsActive = false;
    bool otoWeaponIsActive = true;

    Vector3 cameraFirstPosition;

    public AudioSource fireSound;

    AudioSource soundFile;
    public AudioClip rocketSound;

    void Start()
    {
        Cursor.visible = false;

        soundFile = GetComponent<AudioSource>();

        cameraFirstPosition = transform.localPosition;
    }

    void Update()
    {
        if (transform.localPosition != cameraFirstPosition)
        {
            transform.localPosition = cameraFirstPosition;
        }

        Zoom();
        TargetPoint();
        OtoWeaponFire();
        RocketFire();
        WeaponChange();
        OtoWeaponSoundActiveOrDisable();
    }

    void Zoom()
    {
        if (Input.GetKey(KeyCode.X))
        {
            Camera.main.fieldOfView = 40;
        }
        else
        {
            Camera.main.fieldOfView = 60;
        }
    }

    void TargetPoint()
    {
        targetCrossHair.position = Input.mousePosition;

        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.gameObject.layer != LayerMask.NameToLayer("Bullet"))
            {
                target.position = new Vector3(hit.point.x, target.position.y, hit.point.z);
            }
        }

        float vibration_X = Random.Range(-2.0f, 2.0f);
        float vibration_Z = Random.Range(-2.0f, 2.0f);

        firePoint.LookAt(new Vector3(target.position.x + vibration_X, target.position.y, target.position.z + vibration_Z));
    }

    void OtoWeaponFire()
    {
        if (Input.GetMouseButton(0) && otoWeaponIsActive)
        {
            if (Time.time >= shootUnitilTime)
            {
                GameObject newBullet = Instantiate(bullet, firePoint.position, firePoint.rotation);
                newBullet.GetComponent<Rigidbody>().velocity = firePoint.forward * bulletSpeed * Time.deltaTime;
                shootUnitilTime = Time.time + shootWaitTime;

                Vibration();
            }
        }
    }

    void Vibration()
    {
        float cameraVibrationValue = Random.Range(0.4f, 0.6f);
        transform.localPosition += new Vector3(cameraVibrationValue, 0, cameraVibrationValue);
    }

    void RocketFire()
    {
        if (Input.GetMouseButton(0) && rocketIsActive)
        {
            if (Time.time >= rocketShootUnitilWaitTime)
            {
                soundFile.PlayOneShot(rocketSound, 0.1f);

                GameObject newRocket = Instantiate(rocket, firePoint.position, firePoint.rotation);
                newRocket.transform.LookAt(target);
                newRocket.GetComponent<Rigidbody>().velocity = firePoint.forward * rocketSpeed * Time.deltaTime;
                rocketShootUnitilWaitTime = Time.time + rocketShootWaitTime;
            }
        }
    }

    void WeaponChange()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            rocketIsActive = true;
            otoWeaponIsActive = false;
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            otoWeaponIsActive = true;
            rocketIsActive = false;
        }
    }

    void OtoWeaponSoundActiveOrDisable()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (otoWeaponIsActive)
            {
                fireSound.Play();
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (otoWeaponIsActive)
            {
                fireSound.Stop();
            }
        }
    }
}
