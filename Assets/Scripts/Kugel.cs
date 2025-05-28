using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Kugel : MonoBehaviour
{
    readonly int[] loesung = new int[5];

    public TextMeshProUGUI aufgabeAnzeige;
    public TextMeshProUGUI kommentarAnzeige;
    public TextMeshProUGUI[] loesungAnzeige = new TextMeshProUGUI[5];
    readonly int[] posIndex = new int[5];

    readonly float eingabeFaktor = 5;
    float yStrecke = -2;
    int punkte = 0;
    public TextMeshProUGUI punkteAnzeige;
    bool bewegtSich = false;

    int leben = 3;
    public TextMeshProUGUI lebenAnzeige;

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

        // Kugel startet oben
        transform.position = new Vector3(Random.Range(-7.5f, 7.5f), 12, 0);
        bewegtSich = true;
    }

    void Update()
    {
        if (bewegtSich)
        {
            transform.Translate(
                Input.GetAxis("Horizontal") * eingabeFaktor * Time.deltaTime,
                yStrecke * Time.deltaTime,
                0
            );

            // Falls Kugel zwischen den Plattformen
            if (transform.position.y < 0)
            {
                Fehler("Kein Treffer");
            }
        }


    }

    void OnCollisionEnter(Collision collision)
    {
        // Nummer der Platformen ermitteln
        int plattformNummer = System.Convert.ToInt32(collision.gameObject.name.Substring(8, 1));
        if (plattformNummer == posIndex[0])
        {
            bewegtSich = false;
            yStrecke *= 1.05f;
            kommentarAnzeige.text = "Richtige Lösung";
            punkte++;
            punkteAnzeige.text = "Punkte: " + punkte;
            Invoke(nameof(AufgabenStellen), 1.5f);
        }
        else
        {
            Fehler("Falsche Lösung");
        }
    }

    void Fehler(string text)
    {
        leben--;
        lebenAnzeige.text = "Leben: " + leben;
        bewegtSich = false;
        if (leben > 0)
        {
            yStrecke *= 1.05f;
            kommentarAnzeige.text = $"{text} nur noch {leben} Leben";
            Invoke(nameof(AufgabenStellen), 1.5f);
        }
        else
        {
            kommentarAnzeige.text = "Das war dein letztes Leben";
        }
    }
}
