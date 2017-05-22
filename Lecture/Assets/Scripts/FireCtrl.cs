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
    vKey,
    ultimate
};

public class FireCtrl : MonoBehaviour
{
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
    private bool flag = true;                       // 총 유무 Flag
    public ZaryaBulletType zBT = ZaryaBulletType.baseRight;                 // 상태에 따라 bullet에 다른 오브젝트를 넣어서 처리 하자.
    public Transform aimPoint;              // ShotPoint

    // Effect
    public GameObject zaryaLeft;

    public GameObject zaryaRight;
    public GameObject zaryaShift;
    public GameObject zaryaEkey;
    public GameObject zaryaUltimate;

    //
    private void Start()
    {
        source = GetComponent<AudioSource>();
        muzzleFlash.enabled = false;
        //source.clip = fireSfx;
    }

    public float rayheight = 1.5f;

    // 실드 추가부분
    public bool Shields = false;            // 레이캐스트 결과를 나타내는 bool

    private bool hasShield = false;         // 플레이어가 실드를 가지고 있는지를 확인
    public GameObject shieldObject;

    //void hasShieldFalse()
    //{
    //    hasShield = false;
    //}

    private IEnumerator shieldRelease(GameObject obj, GameObject Shield)            // 실드 지속시간 완료 후 제거
    {
        yield return new WaitForSeconds(2.0f);
        Destroy(Shield);
        obj.GetComponent<MonsterController>().hasShield = false;
        Debug.Log("실드 릴리즈");
    }

    private void Update()
    {
        // DrawRay
        Vector3 forward = aimPoint.TransformDirection(Vector3.forward) * 30;
        Debug.DrawRay(aimPoint.position, forward, Color.green);

        // 추가 실드생성 코드
        Vector3 fwd = transform.TransformDirection(Vector3.forward);

        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.tag == "Monster")
                Shields = true;
            else
                Shields = false;
        }
        // Time 재 사용 시간, 설정, Damage 처리
        if (Shields && Input.GetKeyDown(KeyCode.E))         // 몬스터에게 실드 사용 !!
        {
            GameObject shieldInstance = Instantiate(shieldObject, hit.collider.gameObject.transform.position, Quaternion.identity) as GameObject;
            shieldInstance.transform.parent = hit.transform;
            shieldInstance.transform.position += Vector3.up * 1f;
            hit.collider.gameObject.GetComponent<MonsterController>().hasShield = true;
            StartCoroutine(shieldRelease(hit.collider.gameObject, shieldInstance));
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && hasShield == false)        // 플레이어에게 스스로 실드 사용
        {
            GameObject shieldInstance = Instantiate(shieldObject, transform.position, Quaternion.identity) as GameObject;
            shieldInstance.transform.parent = transform;
            shieldInstance.transform.position += Vector3.up * 1f;
            hasShield = true;
            Destroy(shieldInstance, 3.0f);
            //Invoke("hasShieldFalse",3.0f);
        }

        // 5.19 강의 추가 내용
        // Debug.DrawRay(firePos.postion, firePos.forward * 20.0f, Color.Green);

        //if (Input.GetMouseButton(0))
        //{
        //    RaycastHit Hit;

        //    fireTime += Time.deltaTime;

        //    if (fireTime >= nextFire)
        //    {
        //        fireTime = 0.0f;
        //        Fire(0);
        //    }
        //    if (Physics.Raycast(firePos.position, firePos.forward, out Hit, 50.0f))
        //    {
        //        if (Hit.collider.tag == "Monster" || Hit.collider.tag == "Barrel")
        //        {
        //            object[] _params = new object[2];
        //            _params[0] = Hit.point; // 레이가 맞은 Vector3 값
        //            if (Hit.collider.tag == "Monster") _params[1] = 20;        // 데미지 값
        //            else _params[1] = firePos.position;                        // 배럴은 총격을 맞은 이후 충격파를 나타내기 위해
        //            hit.collider.gameObject.SendMessage("OnDamage", _params, SendMessageOptions.DontRequireReceiver);
        //        }
        //    }
        //}
        //////// 추가 내용

        //if (Physics.Raycast(transform.position, fwd, Mathf.Infinity,))
        // // / / / / / // / / /

        if (Input.GetMouseButton(0) && DropGun.active)
        {
            zBT = ZaryaBulletType.baseLeft;

            fireTime += Time.deltaTime;

            if (fireTime >= nextFire)
            {
                fireTime = 0.0f;
                Fire(1);
            }
        }

        //if (Input.GetButtonDown("Fire3"))
        //{
        //    zBT = ZaryaBulletType.lShift;
        //    Debug.Log("LeftShift");
        //}

        if (Input.GetKeyDown(KeyCode.Q))
        {
            zBT = ZaryaBulletType.ultimate;
            Debug.Log("야호");
        }

        switch (zBT)
        {
            case ZaryaBulletType.baseLeft:
                /*
                 * 0.25 초마다  Hit 에게 Damage
                 */
                break;

            case ZaryaBulletType.baseRight:
                /*
                * Trail Renderer 부착할 것
                * 충돌시 이펙트 발생 , 충돌반경 기준 5.0f ShotDelay 0.8초
                */
                break;

            case ZaryaBulletType.lShift:
                // 실드 생성 , 프리팹 만들기
                // 레이캐스트로 충돌체 갖와서 그곳에 생성
                break;

            case ZaryaBulletType.eKey:
                //아군에게 실드 생성, 실드 상태에서 에너지를 입으면 자리야 분노게이지 생성++
                break;

            case ZaryaBulletType.rKey:
                // 에너지 충전 0.5초 하단 UI게이지 충전되는 모습 보여줄것
                break;

            case ZaryaBulletType.vKey:
                // 근접 공격, 전방 2 거리 부채꼴 모양 고정 데미지
                break;

            case ZaryaBulletType.ultimate:
                // 충돌하거나, 발사후 2초 후에 폭발이펙트 생성, 주변검색 ADD포스 , 중심으로 3초간 빨아들이기 0.1초마다 ADD포
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

    private void Fire(int ch)
    {
        if (ch == 1) CreateBullet();
        GameMgr.instance.PlaySfx(firePos.position, fireSfx);
        //source.PlayOneShot(fireSfx, 1f);
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