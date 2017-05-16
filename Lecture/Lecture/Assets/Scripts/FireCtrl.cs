using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public enum ZaryaBulletType
{
    baseLeft,
    baseRight,
    lShift,
    eKey,
    rKey,
    ultimate
};

public class FireCtrl : MonoBehaviour {

    public GameObject bullet;
    public Transform firePos;
    public float nextFire = 0.1f;
    private float fireTime = 0.0f;

    public AudioClip fireSfx;
    private AudioSource source = null;

    public MeshRenderer muzzleFlash;

    //
    private float AimRadius = 1f;    
    public GameObject DropGun;
    bool flag = true;                       // 총 유무 Flag
    public ZaryaBulletType zBT = ZaryaBulletType.baseRight;                 // 상태에 따라 bullet에 다른 오브젝트를 넣어서 처리 하자.
    public Transform aimPoint;              // ShotPoint

    // Effect
    public GameObject zaryaLeft;
    public GameObject zaryaRight;
    public GameObject zaryaShift;
    public GameObject zaryaEkey;
    public GameObject zaryaUltimate;
    //
    void Start () {
        source = GetComponent<AudioSource>();
        muzzleFlash.enabled = false;
        //source.clip = fireSfx;
	}
	
    public float rayheight = 1.5f;
    
	void Update () {

        // DrawRay
        Vector3 forward = aimPoint.TransformDirection(Vector3.forward) * 30;
        Debug.DrawRay(aimPoint.position, forward, Color.green);
        
        
        if (Input.GetMouseButton(0) && DropGun.active)
        {
            zBT = ZaryaBulletType.baseLeft;

            fireTime += Time.deltaTime;

            if(fireTime >= nextFire)
            {
                fireTime = 0.0f;
                Fire();
            }
        }

        if (Input.GetButtonDown("Fire3"))
        {
            zBT = ZaryaBulletType.lShift;
            Debug.Log("LeftShift");
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            zBT = ZaryaBulletType.ultimate;
            Debug.Log("야호");
        }

        switch (zBT)
        {
            case ZaryaBulletType.baseLeft:
                break;
            case ZaryaBulletType.baseRight:
                break;
            case ZaryaBulletType.lShift:
                break;
            case ZaryaBulletType.eKey:
                break;
            case ZaryaBulletType.rKey:
                break;
            case ZaryaBulletType.ultimate:
                break;
            default:
                break;
        }
         //if(Input.GetMouseButtonDown(1))       // 아이템 줍는 것 테스트용
         //{
         //    DropGun.SetActive(flag);
         //    flag = !flag;
         //}
    }

    private void Fire()
    {
        CreateBullet();
        source.PlayOneShot(fireSfx, 1f);        
        StartCoroutine(this.ShowMuzzleFlash());
    }

    public IEnumerator ShowMuzzleFlash()                   // 사격 섬광 효과
    {
        float scale = Random.Range(0.5f, 1f);
        muzzleFlash.transform.localScale = Vector3.one * scale;         // 크기를 조절하여 역동적으로.
        Quaternion rot = Quaternion.EulerAngles(0, 0, Random.Range(0, 360));
        muzzleFlash.transform.localRotation = rot;
        muzzleFlash.enabled = true;
        yield return new WaitForSeconds(Random.Range(0.02f, 0.1f));     // 리드미컬하게 보인다. 
        muzzleFlash.enabled = false;
    }

    private void CreateBullet()
    {

        //바스티온 랜덤 샷
        Quaternion aimRotation = Quaternion.identity;

        float randx = Random.Range(firePos.rotation.eulerAngles.x - AimRadius, firePos.rotation.eulerAngles.x + AimRadius);
        float randy = Random.Range(firePos.rotation.eulerAngles.y - AimRadius, firePos.rotation.eulerAngles.y + AimRadius);
        
        Vector3 aimVec = new Vector3(randx, randy);
        aimRotation.eulerAngles = aimVec;
        Instantiate(bullet, firePos.position, aimRotation);
        


        //if (zBT == ZaryaBulletType.baseRight)
        //    Instantiate(zaryaRight, firePos.position, Quaternion.identity);
        //else if(zBT == ZaryaBulletType.baseLeft)
        //    Instantiate(zaryaLeft, firePos.position, Quaternion.identity);



        //Debug.Log(randx);
        //Debug.Log(randy);
    }
}
