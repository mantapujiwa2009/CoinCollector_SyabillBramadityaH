using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int totalKoin;
    private int koinTerkumpul = 0;
    public TextMeshProUGUI coinUi;
    private int jumlahZombieMati = 0;
    [SerializeField] private GameObject winScreenUI;

    void OnEnable()
    {
        Enemy.OnZombieMati += SaatZombieMati;
    }

    void OnDisable()
    {
        Enemy.OnZombieMati -= SaatZombieMati;
    }

     void SaatZombieMati(Enemy zombie)
    {
        jumlahZombieMati++;
        Debug.Log("GameManager dengar event. Zombie mati: " + jumlahZombieMati + " (" + zombie.name + ")");
    }

    void Start()
    {
        winScreenUI.SetActive(false);
        coinUi.text = koinTerkumpul.ToString() + "/" + totalKoin.ToString();
        // TODO: hitung jumlah koin di scene saat mulai 
        totalKoin = GameObject.FindGameObjectsWithTag("Coin").Length;
    }

    public void AmbilKoin()
    {
        koinTerkumpul++;
        coinUi.text = koinTerkumpul.ToString() + "/" + totalKoin.ToString();
        Debug.Log("Koin: " + koinTerkumpul + "/" + totalKoin);
        // TODO: jika koinTerkumpul == totalKoin, panggil Menang() 
        if (koinTerkumpul == totalKoin)
        {
            Menang();
        }
    }

    void Menang()
    {
        Debug.Log("KAMU MENANG!");
        winScreenUI.SetActive(true);
        Time.timeScale = 0f;
    }
}