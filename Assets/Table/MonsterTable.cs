using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExcelAsset]
public class MonsterTable : ScriptableObject
{
	public List<MonsterClass> Monster; // Replace 'EntityType' to an actual type that is serializable.
	public List<BossClass> Boss; // Replace 'EntityType' to an actual type that is serializable.
}
