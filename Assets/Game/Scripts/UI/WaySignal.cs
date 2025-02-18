
using System;
using UnityEngine;

using UnityEngine.UI;


public class WaySignal : ComponentBehavior
{
    
    [SerializeField] private Transform roundTrf;
    [SerializeField] private Image roundImg;
    [SerializeField] private Button signalBtn;
    [SerializeField] private float m_TimeFill = 5f;
    [SerializeField] private float m_Timer = 0;
    private bool isRoundActive = true;

    public bool IsRoundActive
    {
        get => isRoundActive;
        set
        {
            if (isRoundActive != value)
            {
                isRoundActive = value;
                roundTrf.gameObject.SetActive(isRoundActive);
            }
            
        }
    }

    public void Init(float timeFill, bool isActive)
    {
        m_TimeFill = timeFill;
        IsRoundActive = isActive;
        m_Timer = 0;
    }

    private void OnEnable()
    {
        signalBtn.onClick.AddListener(() =>
        {
            if (IsRoundActive)
            {
                int goldNum = Mathf.RoundToInt(m_TimeFill - m_Timer) * 10;
                ObserverManager.Notify(EventId.RewardGold,goldNum);
            }
            ObserverManager.Notify(EventId.SpawnNextWay);
        });
        ObserverManager.Attach(EventId.SpawnWay, _ => Disapperance());
    }

    private void OnDisable()
    {
        ObserverManager.Detach(EventId.SpawnWay, _ => Disapperance());
    }

    private void Disapperance()
    {
        transform.parent.gameObject.SetActive(false);
    }

    protected override void LoadComponent()
    {
        base.LoadComponent();
        if (roundTrf == null) roundTrf = transform.Find("Round");
        roundImg ??= roundTrf.GetComponent<Image>();
        signalBtn ??= transform.GetComponentInChildren<Button>();
    }

    private void Update()
    {
        if (!IsRoundActive ) return;
        if(m_Timer >= m_TimeFill){
            ObserverManager.Notify(EventId.SpawnNextWay);
            PoolingManager.Despawn(transform.parent.gameObject);
        }
        m_Timer += Time.deltaTime;
        roundImg.fillAmount = m_Timer / m_TimeFill;
    }
}
