using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomLookAndSpotlight : MonoBehaviour
{
    public Transform sphere;        // Sphere objesi
    public Light spotlight;         // Spotlight ışığı
    public float rotationSpeed = 1.0f; // Sphere'in dönüş hızı
    public float changeTargetInterval = 3.0f; // Hedef değiştirme süresi
    public float spotlightDistance = 1.0f;   // Spotlight'ın Sphere'in önünde olacağı mesafe

    private Vector3 targetPoint;    // Sphere'in bakacağı hedef nokta
    private float timer;            // Zamanlayıcı

    void Start()
    {
        // İlk hedef noktasını rastgele seç
        targetPoint = GetRandomPointOnMap();
    }

    void Update()
    {
        // Timer'ı güncelle
        timer += Time.deltaTime;

        // Belirlenen süre sonra yeni bir hedef belirle
        if (timer > changeTargetInterval)
        {
            targetPoint = GetRandomPointOnMap();
            timer = 0;
        }

        // Sphere'i hedefe doğru yumuşak bir şekilde döndür
        Quaternion targetRotation = Quaternion.LookRotation(targetPoint - sphere.position);
        sphere.rotation = Quaternion.Slerp(sphere.rotation, targetRotation, Time.deltaTime * rotationSpeed);

        // Spotlight'ı Sphere'in bakış yönüne göre güncelle
        UpdateSpotlight();
    }

    void UpdateSpotlight()
    {
        // Spotlight'ı Sphere'in ön kısmına koy
        Vector3 spotlightPosition = sphere.position + sphere.forward * spotlightDistance;
        spotlight.transform.position = spotlightPosition;

        // Spotlight'ın yönünü Sphere'in yönüne eşitle
        spotlight.transform.forward = sphere.forward;
    }

    // Haritada rastgele bir noktaya ulaşmak için fonksiyon
    Vector3 GetRandomPointOnMap()
    {
        // 1000*1000 harita boyutları
        float x = Random.Range(0, 750);
        float y = Random.Range(-100, 0);
        float z = Random.Range(0, 750);

        Vector3 randomPoint = new Vector3(x, y, z);

        // Eğer hedef çok yakınsa yeniden hedef seç 
        while (Vector3.Distance(randomPoint, sphere.position) < 50)
        {
            x = Random.Range(-500, 500);
            y = Random.Range(10, 100);
            z = Random.Range(-500, 500);
            randomPoint = new Vector3(x, y, z);
        }

        return randomPoint;
    }
}
