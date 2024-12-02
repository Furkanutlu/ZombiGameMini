using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    public Light directionalLight; // Güneşi temsil eden Directional Light
    public Material daySkybox; // Gündüz Skybox
    public Material nightSkybox; // Gece Skybox
    public float dayDuration = 120f; // Tam bir günün süresi (saniye)

    private float timeOfDay = 0f;

    void Update()
    {
        // Gün Zamanını İlerlet
        timeOfDay += Time.deltaTime / dayDuration;
        if (timeOfDay > 1f) timeOfDay = 0f;

        // Güneşin Rotasyonunu Ayarla
        float sunAngle = timeOfDay * 360f - 90f; // 0 - 360 derece arasında döner
        directionalLight.transform.rotation = Quaternion.Euler(new Vector3(sunAngle, 170f, 0f));

        // Güneşin Işık Rengini ve Yoğunluğunu Ayarla
        if (timeOfDay < 0.5f) // Gündüz
        {
            RenderSettings.skybox = daySkybox;
            directionalLight.color = Color.Lerp(Color.blue, Color.white, timeOfDay * 2);
            directionalLight.intensity = Mathf.Lerp(0.2f, 1f, timeOfDay * 2);
        }
        else // Gece
        {
            RenderSettings.skybox = nightSkybox;
            directionalLight.color = Color.Lerp(Color.white, Color.blue, (timeOfDay - 0.5f) * 2);
            directionalLight.intensity = Mathf.Lerp(1f, 0.2f, (timeOfDay - 0.5f) * 2);
        }
    }
}
