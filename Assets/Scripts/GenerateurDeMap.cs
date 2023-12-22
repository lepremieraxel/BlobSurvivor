using UnityEngine;

public class GenerateurDeMap : MonoBehaviour
{
    public int largeur = 256; // Largeur de la carte
    public int hauteur = 256; // Hauteur de la carte
    public float echelle = 20f; // Echelle du bruit de Perlin (impacte la "fréquence" des valeurs générées)
    public float amplitude = 50f; // Amplitude du bruit de Perlin (impacte la "hauteur" des valeurs générées)

    void Start()
    {
        // Affecter le sprite généré au SpriteRenderer attaché à cet objet
        GetComponent<SpriteRenderer>().sprite = GenererSprite();
    }

    Sprite GenererSprite()
    {
        // Créer un objet Texture2D pour représenter la carte
        Texture2D texture = new Texture2D(largeur, hauteur);

        // Générer les valeurs de la carte en fonction du bruit de Perlin
        for (int x = 0; x < largeur; x++)
        {
            for (int y = 0; y < hauteur; y++)
            {
                float valeurPerlin = Mathf.PerlinNoise(x / echelle, y / echelle) * amplitude;
                Color couleur = new Color(valeurPerlin, valeurPerlin, valeurPerlin);
                texture.SetPixel(x, y, couleur);
            }
        }

        // Appliquer les changements à la texture
        texture.Apply();

        // Créer un sprite à partir de la texture
        Sprite sprite = Sprite.Create(texture, new Rect(0, 0, largeur, hauteur), new Vector2(0.5f, 0.5f));

        return sprite;
    }
}