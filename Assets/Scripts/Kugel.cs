using UnityEngine;
using TMPro;

public class Kugel : MonoBehaviour
{
    readonly int[] loesung = new int[5];

    public TextMeshProUGUI aufgabeAnzeige;
    public TextMeshProUGUI kommentarAnzeige;
    public TextMeshProUGUI[] loesungAnzeige = new TextMeshProUGUI[5];
    readonly int[] posIndex = new int[5];

    void Start()
    {
        AufgabenStellen();
    }

    void AufgabenStellen()
    {
        // Aufgabe und richtige Loesung
        int a = Random.Range(10, 31);
        int b = Random.Range(10, 31);
        loesung[0] = a + b;

        // Erste falsche Loesung, +/- 10
        int zz = Random.Range(1, 3);
        if (zz == 1)
        {
            loesung[1] = loesung[0] + 10;
        }
        else
        {
            loesung[1] = loesung[0] - 10;
        }

        // Drei weitere falsche Loesungen
        for (int i = 2; i < 5; i++)
        {
            bool vorhanden;
            do
            {
                loesung[i] = loesung[0] + Random.Range(-10, 10);
                vorhanden = false;
                for (int k = 0; k < i; k++)
                {
                    if (loesung[k] == loesung[i])
                    {
                        vorhanden = true;
                        break;
                    }
                }
            }
            while (vorhanden);
        }

        aufgabeAnzeige.text = $"{a} + {b} =";
        kommentarAnzeige.text = "";

        // Anfangspositionen der Loesungen festlegen
        for (int i = 0; i < 5; i++)
        {
            posIndex[i] = i;
        }

        // Positionen der Loesungen mischen
        for (int i = 0; i < 20; i++)
        {
            a = Random.Range(0, 5);
            b = Random.Range(0, 5);
            int temp = posIndex[a];
            posIndex[a] = posIndex[b];
            posIndex[b] = temp;
        }

        // Loesungen an gemischten Positionen anzeigen
        for (int i = 0; i < 5; i++)
        {
            loesungAnzeige[posIndex[i]].text = "" + loesung[i];
        }
    }
}
