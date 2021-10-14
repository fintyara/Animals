using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using SuperMaxim.Messaging;
using TMPro;

public class DayNightCycle : MonoBehaviour
{
    #region VAR
    [SerializeField] private List<DayNightState> States;
    [SerializeField] private DayNightState CurState;
    [Space(10)]
    [SerializeField] private DayNightState StartState;
    [Space(10)]
    [SerializeField] private Light MainLight;
    [SerializeField] private SpriteRenderer Sky;

    [SerializeField] private Type MyType;
    [Space(10)]   
    [SerializeField] private float curHour;
    [SerializeField] private float durationHour = 30;
    [ShowOnly]
    [SerializeField] private float timerTime;
    [SerializeField] private TextMeshProUGUI CountHourGUI;
    private enum Type
    {
        Light, Sprite
    }
    #endregion
    
    #region MONO
    private void Start()
    {
        SetState(StartState);
    }
    private void Update()
    {
        ControlColor();
        ControlIntensity();
        ControlTime();
    }
    #endregion

    #region FUNC
    public void NextState()
    {
        var newIndex = States.IndexOf(CurState) + 1;
        
        CurState = newIndex == States.Count ? States[0] : States[newIndex];
        
        Messenger.Default.Publish(new TimeOfDayPayload { NewState = CurState });
    }
    public void SetState(DayNightState _NewState)
    {
        if (!States.Contains(_NewState)) { Debug.Log("ERROR"); return; }

        CurState = _NewState;

        UpdateTime();
        SetColor();
        SetIntensity();

        if (CountHourGUI != null)
            CountHourGUI.text = curHour.ToString(CultureInfo.InvariantCulture);

        Messenger.Default.Publish(new TimeOfDayPayload { NewState = CurState });
    }
    public void UpdateTime()
    {
        curHour = CurState.timeBegin;
        timerTime = 0;   
    }
    private void SetColor()
    {
        if (MyType == Type.Light)
            MainLight.color = CurState.Color;
        else
            Sky.color = CurState.Color;
    }
    private void SetIntensity()
    {
        if (MyType == Type.Light)
            MainLight.intensity = CurState.intensity;
    }
    private void ControlColor()
    {
        if (MyType == Type.Light) 
            MainLight.color = Color.Lerp(MainLight.color, CurState.Color, CurState.speedChangeColor / durationHour * Time.deltaTime);
        else
            Sky.color = Color.Lerp(Sky.color, CurState.Color, CurState.speedChangeColor / durationHour * Time.deltaTime);
    }
    private void ControlIntensity()
    {
        if (MyType == Type.Light)
            MainLight.intensity = Mathf.Lerp(MainLight.intensity, CurState.intensity, CurState.speedChangeIntensity / durationHour * Time.deltaTime);
    }
    private void ControlTime()
    {
        timerTime += Time.deltaTime;

        if (timerTime >= durationHour)
        {         
            curHour += 1;
            timerTime = 0;
          
            if (curHour > CurState.timeEnd)         
                NextState();

            if (curHour == 24)
                curHour = 0;

            if (CountHourGUI != null)
                CountHourGUI.text = curHour.ToString();

            Messenger.Default.Publish(new TimePayload { time = (int)curHour });
        }
    }
    #endregion
}
