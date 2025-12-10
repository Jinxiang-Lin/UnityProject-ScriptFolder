using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class WateringState
{
    readonly float maxWaterLevel = 100f;
    readonly float minWaterLevel = 0f;
    float waterLevelDecreaseRate = 50f;
    //readonly float waterLevelIncreaseRate = 500f;
    readonly float wiltingThreshold = 40f;
    float currentWaterLevel = 0f;
    readonly PlayerController pc;

    public float WaterLevelDecreaseRate { get => waterLevelDecreaseRate; set => waterLevelDecreaseRate = value; }
    public float CurrentWaterLevel { get => currentWaterLevel; set => currentWaterLevel = value; }

    public float WiltingThreshold => wiltingThreshold;

    public float MaxWaterLevel => maxWaterLevel;

    public float MinWaterLevel => minWaterLevel;

    public WateringState(PlayerController pc)
    {
        this.pc = pc;
    }
    public void Enter()
    {
        currentWaterLevel = SaveSystem.LoadGameData(GameManager.Instance.CurrentId).current_water_level; // start with the wilting threshold level
        pc.WaterSlider.minValue = minWaterLevel;
        pc.WaterSlider.maxValue = maxWaterLevel;
        pc.WaterSlider.value = currentWaterLevel;
        
        Debug.LogError($"this water level need to be fixed 1 is a hard coded number !!!!!!!!");
    }
    public void LogicalUpdate()
    {
        currentWaterLevel -= waterLevelDecreaseRate * Time.deltaTime / 60f; // decrease water level per second
        currentWaterLevel = Mathf.Clamp(currentWaterLevel, minWaterLevel, maxWaterLevel);
        pc.WaterSlider.value = currentWaterLevel;
        //Debug.Log($"current water level is {currentWaterLevel}, decreased per second base");
    }
}
