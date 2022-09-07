using UnityEngine;
using UnityEngine.UI;

public class PreviewController : SingletonMonoBehaviour<PreviewController>
{
    [SerializeField] private Image previewImage;

    public void PreviewClothe(Clothe clothe)
    {
        UpdatePreview(clothe);
    }

    public void UpdatePreview(Clothe clothe)
    {
        previewImage.material.CopyPropertiesFromMaterial(ClotheController.instance.spriteRenderer.material);
        if (clothe != null)
        {
            Material material = previewImage.material;

            if (clothe.t_shirt.a > 0.5f) material.SetColor("_RedReplacer", clothe.t_shirt);
            if (clothe.blueReplacer.a > 0.5f) material.SetColor("_BlueReplacer", clothe.blueReplacer);
            if (clothe.pants.a > 0.5f) material.SetColor("_GreenReplacer", clothe.pants);
            if (clothe.shirt.a > 0.5f) material.SetColor("_YellowReplacer", clothe.shirt);
            if (clothe.underShirt.a > 0.5f) material.SetColor("_PurpleReplacer", clothe.underShirt);
            if (clothe.shoes.a > 0.5f) material.SetColor("_CyanReplacer", clothe.shoes);
            if (clothe.skin.a > 0.5f) material.SetColor("_WhiteReplacer", clothe.skin);
        }
    }

    public void Clear()
    {
        previewImage.material.CopyPropertiesFromMaterial(ClotheController.instance.spriteRenderer.material);        
    }
}
