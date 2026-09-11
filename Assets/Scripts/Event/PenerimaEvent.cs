using UnityEngine;

public class PenerimaEvent : MonoBehaviour
{
    void OnEnable()
    {
        Pengirimevent.OnTekanSpasi += Reaksi;
    }

    void OnDisable()
    {
        Pengirimevent.OnTekanSpasi -= Reaksi;
    }

    void Reaksi()
    {
        Debug.Log("Penerima: aku dengar event spasi!");
    }
}
