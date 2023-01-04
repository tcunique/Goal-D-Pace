using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;

    public Gradient gradient;

    //On déclare donc le fill qu'on avait crée pour remplir la barre d'hp
    public Image fill;

    //fonction pour l'initialisation de la vie
    public void SetMaxHealth(int health)
    {
        //C'est les paramètres dans le slider
        slider.maxValue = health;
        slider.value = health;

        //on va changer la couleur de la barre d'hp qui est caractérisé par fill
        //le gradient correspond au dégradé de couleur
        //1f correspond à la valeur max, donc il va renvoyer max au début donc du vert
        fill.color = gradient.Evaluate(1f);
    }

    //fonction pour changer la vie
    public void SetHealth(int health)
    {
        slider.value = health;

        //Ici c'est le même principe, mais normalizedValue en gros va prendre la valeur
        //de value. Normalized va transformer la valeur de value entre 0 et 1, qui est
        //de base entre 0 et 100.
        //0 et 1 c'est pour le gradient qui défini comme ça le dégradé
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }
}
