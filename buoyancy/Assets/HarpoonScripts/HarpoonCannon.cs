using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HarpoonCannon : MonoBehaviour
{
    public GameObject harpoonAmmo;
    public GameObject spawnHarpoon;
    public GameObject mountCamera;
    public GameObject gun;
    //public GameObject
    public GameObject theCamera;
    public Transform cameraTransform;
    public bool usingGun = false;
    public bool wasUsingGun = false;
    public Vector3  tempPosition;
    public Quaternion tempRotate;
    public bool isLoaded = false;
    private float mountRot;
    private float gunRot;
    private float mountRotor;
    private float gunRotor;
    public float rotSpeed = 50;
    public float minTilt = 45f;
    public float maxTilt = 135f;
    private int counter = 0;
    private GameObject harpoon;




    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void FireHarpoon()
    {
        if (isLoaded == false)
        {
            
            harpoon = Instantiate(harpoonAmmo) as GameObject;
            harpoon.transform.parent = transform;
            harpoon.transform.position = spawnHarpoon.transform.position;
            counter = 0;
            isLoaded = true;
        }
        else {
            if (harpoon != null)
            {
                harpoon.GetComponent<Rigidbody>().velocity = gun.transform.up * 100;
                harpoon.transform.rotation = spawnHarpoon.transform.rotation;
                harpoon.transform.parent = null;
            }
            counter = 0;
            isLoaded = false;
         }
    }

    // Update is called once per frame
    void Update()
    {
        if(harpoon != null)
        {
            if (isLoaded)
            {
                harpoon.transform.rotation = spawnHarpoon.transform.rotation;
            }
        }
        

        if (counter > 66)
        {
            if (Input.GetButton("Fire1") || Input.GetKey(KeyCode.Space))
            {
                if (counter > 60)
                {
                    FireHarpoon();
                }
            }
        }
        else
        {
            counter++;
        }

        if (usingGun == true)
        {
            if (wasUsingGun == false)
            {
                tempPosition = cameraTransform.position;
                tempRotate = cameraTransform.rotation;
                cameraTransform.position = Vector3.Lerp(cameraTransform.position, mountCamera.transform.position, 20 * Time.deltaTime);

                //cameraTransform.position = mountCamera.transform.position;
                cameraTransform.rotation = mountCamera.transform.rotation;
                //theCamera.transform.SetParent(mountCamera);
                wasUsingGun = true;
            }
            else
            {
                cameraTransform.position = Vector3.Lerp(cameraTransform.position, mountCamera.transform.position, 20 * Time.deltaTime);
                cameraTransform.rotation = mountCamera.transform.rotation;
            }
          
            mountRot = transform.eulerAngles.y + (Input.GetAxis("Horizontal") * rotSpeed * Time.deltaTime);
            gunRot = gun.transform.eulerAngles.z + (Input.GetAxis("Vertical") * rotSpeed * Time.deltaTime);


            gunRot = Mathf.Clamp(gunRot, minTilt, maxTilt);
            transform.eulerAngles = new Vector3(transform.eulerAngles.x,  mountRot, transform.eulerAngles.z);
            gun.transform.eulerAngles = new Vector3(gun.transform.eulerAngles.x, gun.transform.eulerAngles.y, gunRot);



        }
        else
        {
            if (wasUsingGun == true)
            {
                //cameraTransform.position = Vector3.Lerp tempTransform.transform.position;

                cameraTransform.position = tempPosition;
                //cameraTransform.position = Vector3.Lerp(cameraTransform.position, tempPosition, 20*Time.deltaTime);
                cameraTransform.rotation = tempRotate;
                wasUsingGun = false;
            }
            
        }
    }
}
