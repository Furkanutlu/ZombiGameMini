using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100; // Maksimum can
    public int currentHealth;  // Mevcut can
    public Slider healthBar;   // UI Slider elemanı

    void Start()
    {
        currentHealth = maxHealth; // Oyuncunun canını başlat
        healthBar.maxValue = maxHealth; // Slider'ın maksimum değerini ayarla
        healthBar.value = currentHealth; // Mevcut canı göster
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage; // Oyuncu hasar alır
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth); // Can sınırlarını kontrol et
        healthBar.value = currentHealth; // Slider'ı güncelle

        if (currentHealth <= 0)
        {
            Die(); // Oyuncunun ölümü
        }
    }

    public void Heal(int healAmount)
    {
        currentHealth += healAmount; // Oyuncuyu iyileştir
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth); // Can sınırlarını kontrol et
        healthBar.value = currentHealth; // Slider'ı güncelle
    }

    void Die()
    {
        Debug.Log("Oyuncu öldü!");
        // Ölüm animasyonu veya sahne sonlandırma kodlarını buraya ekle
    }
}
