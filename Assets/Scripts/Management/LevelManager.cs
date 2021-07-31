using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class LevelManager : MonoBehaviour
{
    public List<Texture2D> images = new List<Texture2D>();
    public List<Color> colors = new List<Color>();

    public ParticleSystem dustParticle;

    int num = 0;

    private void Awake()
    {
        //GetSprites();
    }

    public void NextLevel()
    {
        var main = dustParticle.main;

        main.startColor = colors[num];

        if (num >= colors.Count - 1) num = 0;
        else num++;

        var dustShape = dustParticle.shape;
        dustShape.texture = images[SavedData.savedData.spriteNum];

        if (SavedData.savedData.spriteNum >= images.Count - 1) SavedData.savedData.spriteNum = 0;
        else SavedData.savedData.spriteNum++;
    }

    /*
    void GetSprites()
    {
        images = new List<Sprite>();

        string myPath = "Assets/Sprites/ParticleIcons";
        DirectoryInfo dir = new DirectoryInfo(myPath);

        FileInfo[] info = dir.GetFiles("*.*");
        foreach (FileInfo f in info)
        {
            print(f);
            if (f.Extension == ".png")
            {
                images.Add(Resources.Load<Sprite>(f.Name));
            }
        }
    }
    */
}
