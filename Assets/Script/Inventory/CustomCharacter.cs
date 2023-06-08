using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomCharacter : MonoBehaviour
{
    [SerializeField] GameObject _skin1;

    // Start is called before the first frame update
    void Start()
    {
        _skin1 = GameObject.Find("CharacterWithItem").GetComponent<Transform>().gameObject;
    }

    public void CustomCharacterWithWeapon(string url)
    {
        StartCoroutine(LoadImageFromUrl(url));
    }

    private IEnumerator LoadImageFromUrl(string url)
    {
        WWW wwwLoader = new WWW(url);
        yield return wwwLoader;
        _skin1.GetComponent<Image>().sprite = Sprite.Create(wwwLoader.texture, new Rect(0, 0, wwwLoader.texture.width, wwwLoader.texture.height), Vector2.one / 2);
    }
}
