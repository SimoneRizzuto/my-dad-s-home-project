using Godot;
using System;

namespace MyFathersHomeProject.Scripts.Shared.Signals;
public partial class CustomSignals : Node
{
    [Signal] public delegate void TriggeredEventHandler();
}
