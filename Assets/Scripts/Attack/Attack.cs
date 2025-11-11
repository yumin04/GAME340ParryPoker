using System.Collections;
using UnityEngine;

public class Attack : MonoBehaviour
{
    private static Attack instance;
    public static Attack GetInstance() => instance;
    
    [SerializeField] private GameObject background;     // 초록
    [SerializeField] private GameObject hittingSpace;   // 빨강
    [SerializeField] private GameObject indicator;      // 파랑 (내부에서 움직이는 선)
    
    [SerializeField] private float indicatorSpeed = 3f;
    [SerializeField] private float comboResetTime = 1f;

    private bool isMovingRight = true;
    private int combo = 0;
    
    private float currentRandomRatio;
    private float bgHeight;

    private GameObject currentIndicatorObject;
    private GameObject currentBackgroundObject;
    private int attackRoundRemaining;

    public void Awake()
    {
        InstantiateOneAttackObject();
    }

    public void OnEnable()
    {
        //GameEvents.OnAttackRoundEnd += CheckAttackRoundRemaining;
    }
    public void OnDisable()
    {
        //GameEvents.OnAttackRoundEnd -= CheckAttackRoundRemaining;
    }
    public void StartAttackLoop()
    {
        attackRoundRemaining = 5;
        Destroy(currentBackgroundObject);
        InstantiateOneAttackObject();
    }

    private void CheckAttackRoundRemaining()
    {
        if(attackRoundRemaining == 0)
        {
            //GameEvents.OnAttackPhaseEnd.Invoke(combo);
        }
        attackRoundRemaining--;
        CheckForCombo();
        InstantiateOneAttackObject();
    }



    private void InstantiateOneAttackObject()
    {
        // 1️⃣ Background 생성
        currentBackgroundObject = Instantiate(background, transform);
    
        // 2️⃣ Hitting Space 생성 (랜덤 위치)
        // Vector3 hitPos = GetRandomHittingSpacePosition();
        GameObject hit = Instantiate(hittingSpace, currentBackgroundObject.transform);
        hit.transform.localPosition = GetRandomHittingSpacePosition();

        // 3️⃣ Indicator 생성
        currentIndicatorObject = Instantiate(indicator, currentBackgroundObject.transform);
        currentIndicatorObject.transform.localPosition = GetIndicatorPosition();  
    }

    private Vector3 GetIndicatorPosition()
    {
        return new Vector3(0,0,0);
    }


    private Vector3 GetRandomHittingSpacePosition()
    {
        // within 0.1 top and bottom, of background
        currentRandomRatio = Random.Range(-0.4f, 0.4f);
        Vector3 pos = new Vector3(0,0,0);
        bgHeight = background.GetComponent<Renderer>().bounds.size.y / 2f;
        pos.y = bgHeight * currentRandomRatio;
        return pos;
    }
    private void CheckForCombo()
    {
        if (CalculateIndicatorOnHittingSpace())
        {
            
        }
    }
    private bool CalculateIndicatorOnHittingSpace()
    {
        float ratio = currentIndicatorObject.transform.localPosition.y / bgHeight;
        bool ans = ratio < currentRandomRatio + 0.1f && ratio > currentRandomRatio - 0.1f;
        Debug.Log("[DEBUG] Calculating for combo: " + ans.ToString());
        return ans;
    }
}