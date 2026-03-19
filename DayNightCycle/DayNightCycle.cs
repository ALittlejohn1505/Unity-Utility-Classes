using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    //Controls How long the Day cycle will be. counted in Seconds
    [Header("Day Settings")]
    [Tooltip("Length of a full day in seconds")]
    public float dayDuration = 60f;

    [Tooltip("Initial time of day (0 = midnight, 0.5 = noon, 1 = next midnight)")]
    [Range(0f, 1f)]
    public float timeOfDay = 0f;

    [Header("Sun Rotation")]
    [Tooltip("Offset to adjust the sun's starting rotation")]
    public Vector3 rotationOffset = new Vector3(-90f, 0f, 0f);

    //Variables for Controlling the Intensity and Color of the light throughout the Day/Night Cycle
    [Header("Light Settings")]
    public Light sunLight;

    [Tooltip("Controls brightness over the day")]
    public AnimationCurve lightIntensityCurve;

    [Tooltip("Controls sun color over the day")]
    public Gradient lightColorGradient;

    //Variables for Updating Sky Box based on Time of Day
    [Header("Skybox Settings")]
    //If Not Using Seperate Sky Boxes
    //public AnimationCurve skyboxExposureCurve;
    //public Material daySkybox;
    //public Material nightSkybox;

    private float timeRate;
    public SkyboxController skyboxController;

    void Start()
    {
        if (dayDuration <= 0f)
        {
            Debug.LogWarning("Day duration must be greater than 0!");
            dayDuration = 60f;
        }

        timeRate = 1f / dayDuration;
    }

    void Update()
    {
        // Advance time
        timeOfDay += timeRate * Time.deltaTime;
        timeOfDay %= 1f;

        UpdateSunRotation();
        UpdateLighting();
        //UpdateSkybox();
        if (skyboxController != null){
           skyboxController.SetTime(timeOfDay);
        }
    }

    void UpdateSunRotation()
    {
        // Convert timeOfDay to angle (0–360 degrees)
        float sunAngle = timeOfDay * 360f;

        // Apply rotation (X axis controls day/night cycle)
        transform.rotation = Quaternion.Euler(sunAngle + rotationOffset.x, rotationOffset.y, rotationOffset.z);
    }

    void UpdateLighting(){
        //Check if the object Providing the Light is not Null
        if(sunLight == null){return;}
        
        //if The Sunlight Object is not Null, update the Intensity based on time of Day
        // Intensity (0 at night, 1 at noon, etc.)
        sunLight.intensity = lightIntensityCurve.Evaluate(timeOfDay);

        // Color (sunrise orange → white → sunset red → dark)
        sunLight.color = lightColorGradient.Evaluate(timeOfDay);

        // Skybox exposure If Not Using Seperate Sky Boxes
        //float exposure = skyboxExposureCurve.Evaluate(timeOfDay);
        //RenderSettings.skybox.SetFloat("_Exposure", exposure);
        //DynamicGI.UpdateEnvironment();
    }

    //void UpdateSkybox(){
       // if (timeOfDay > 0.25f && timeOfDay < 0.75f){
       //     RenderSettings.skybox = daySkybox;
      //  }else{
       //     RenderSettings.skybox = nightSkybox;
       // }
        
    //}
}