using UnityEngine;

public class SkyboxController : MonoBehaviour
{
    [Header("References")]
    [Tooltip("Material using a blended skybox shader")]
    public Material skyboxMaterial;

    [Header("Blend Settings")]
    [Tooltip("Controls how the skybox blends over the day (0 = night, 1 = day)")]
    public AnimationCurve skyboxBlendCurve;

    [Tooltip("Name of the blend property in the shader")]
    public string blendPropertyName = "_Blend";

    [Header("Time Reference")]
    [Range(0f, 1f)]
    public float timeOfDay;

    void Update()
    {
        UpdateSkybox();
    }

    public void SetTime(float time)
    {
        timeOfDay = time;
        UpdateSkybox();
    }

    void UpdateSkybox()
    {
        if (skyboxMaterial == null) return;

        float blendValue = skyboxBlendCurve.Evaluate(timeOfDay);

        skyboxMaterial.SetFloat(blendPropertyName, blendValue);

        // Apply skybox globally
        RenderSettings.skybox = skyboxMaterial;

        RenderSettings.fogColor = Color.Lerp(Color.black, Color.gray, skyboxBlendCurve.Evaluate(timeOfDay));

        // Update lighting/reflections
        DynamicGI.UpdateEnvironment();

    }
}