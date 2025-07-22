using Godot;
using System;
using MyFathersHomeProject.Scripts.Shared.Modules.Interactables;

namespace MyFathersHomeProject.Scripts.Interactables;
public partial class ClothesInteractable : ItemInteractable
{
	public override void Interact()
	{
		Console.WriteLine("WOOO!!! Triggered");
	}
}