using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Resource
{
	#region Fieds

	#endregion

	#region Properties

	public string Name { get; private set; }

	public GameObject ResourceController { get; private set; }

	public Vector3 Position { get; set; }

	public Vector3 RotationDeg { get; set; }

	public ResourceTypes Type { get; private set; }

	#endregion

	#region Constructors

	public Resource(string name,
		GameObject controller,
		Vector3 position,
		Vector3 rotation,
		ResourceTypes type)
	{
		Name = name;
		ResourceController = controller;
		Position = position;
		RotationDeg = rotation;
		Type = type;
	}

	#endregion

	#region Public methods

	#endregion

	#region Private methods

	#endregion
}
