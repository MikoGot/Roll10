using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ImageForNote", menuName = "ForNote", order = 1)]
public class ImageO : ScriptableObject {
	public string imageName;
	public ImageType imageType;
	public Sprite artwork;

}

public enum ImageType{
	CHARACTER,
	OBJECT,
	SYMBOL,
	FLAG,
	MONSTER
}