using Godot;
using System;
using System.Linq;
using Godot.Collections;
using MyFathersHomeProject.Scripts.Dialogue.Actor;
using MyFathersHomeProject.Scripts.Shared.Constants;

namespace DialogueManagerRuntime;
public partial class DialogueBalloon : CanvasLayer
{
	[Export] public string NextAction = "ui_accept";
	[Export] public string SkipAction = "ui_cancel";
	
	// constants
	private const string DefaultThemePath = "res://Assets/Textures/UI/speech-bubble-style.tres";
	private const string OliverThemePath = "res://Assets/Textures/UI/speech-bubble-oliver-style.tres";
	private const string SashaThemePath = "res://Assets/Textures/UI/speech-bubble-sasha-style.tres";

	// variables
	private bool _splitDialogueBalloons = false;
	
	Control balloon;

	RichTextLabel dialogueLabel;
	VBoxContainer responsesMenu;
	TextureRect  dialoguePointer;

	Resource resource;
	Array<Variant> temporaryGameStates = new Array<Variant>();
	bool isWaitingForInput = false;
	bool willHideBalloon = false;

	DialogueLine dialogueLine;

	DialogueLine DialogueLine
	{
		get => dialogueLine;
		set
		{
			isWaitingForInput = false;
			balloon.FocusMode = Control.FocusModeEnum.All;
			balloon.GrabFocus();

			if (value == null)
			{
				QueueFree();
				return;
			}

			dialogueLine = value;
			UpdateDialogue();
		}
	}


	public override void _Ready()
	{
		balloon = GetNode<Control>("%Balloon");
		//characterLabel = GetNode<RichTextLabel>("%CharacterLabel");
		dialogueLabel = GetNode<RichTextLabel>("%DialogueLabel");
		responsesMenu = GetNode<VBoxContainer>("%ResponsesMenu");
		dialoguePointer = GetNode<TextureRect>("%DialoguePointer");

		balloon.Hide();

		balloon.GuiInput += (@event) =>
		{
			if ((bool)dialogueLabel.Get("is_typing"))
			{
				bool mouseWasClicked = @event is InputEventMouseButton &&
				                       (@event as InputEventMouseButton).ButtonIndex == MouseButton.Left &&
				                       @event.IsPressed();
				bool skipButtonWasPressed = @event.IsActionPressed(SkipAction);
				if (mouseWasClicked || skipButtonWasPressed)
				{
					GetViewport().SetInputAsHandled();
					dialogueLabel.Call("skip_typing");
					return;
				}
			}

			if (!isWaitingForInput) return;
			if (dialogueLine.Responses.Count > 0) return;

			GetViewport().SetInputAsHandled();

			if (@event is InputEventMouseButton && @event.IsPressed() &&
			    (@event as InputEventMouseButton).ButtonIndex == MouseButton.Left)
			{
				Next(dialogueLine.NextId);
			}
			else if (@event.IsActionPressed(NextAction) && GetViewport().GuiGetFocusOwner() == balloon)
			{
				Next(dialogueLine.NextId);
			}
		};

		if (string.IsNullOrEmpty((string)responsesMenu.Get("next_action")))
		{
			responsesMenu.Set("next_action", NextAction);
		}

		responsesMenu.Connect("response_selected",
			Callable.From((DialogueResponse response) => { Next(response.NextId); }));

		DialogueManager.Mutated += OnMutated;
	}


	public override void _ExitTree()
	{
		DialogueManager.Mutated -= OnMutated;
	}


	public override void _UnhandledInput(InputEvent @event)
	{
		// Only the balloon is allowed to handle input while it's showing
		GetViewport().SetInputAsHandled();
	}


	public override async void _Notification(int what)
	{
		// Detect a change of locale and update the current dialogue line to show the new language
		if (what == NotificationTranslationChanged && IsInstanceValid(dialogueLabel))
		{
			float visibleRatio = dialogueLabel.VisibleRatio;
			DialogueLine =
				await DialogueManager.GetNextDialogueLine(resource, DialogueLine.Id, temporaryGameStates);
			if (visibleRatio < 1.0f)
			{
				dialogueLabel.Call("skip_typing");
			}
		}
	}


	public async void Start(Resource dialogueResource, string title, Array<Variant> extraGameStates = null)
	{
		if (!IsNodeReady())
		{
			await ToSignal(this, DialogueManagerRuntime.DialogueBalloon.SignalName.Ready);
		}

		temporaryGameStates = extraGameStates ?? new Array<Variant>();
		isWaitingForInput = false;
		resource = dialogueResource;

		DialogueLine = await DialogueManager.GetNextDialogueLine(resource, title, temporaryGameStates);
	}


	public async void Next(string nextId)
	{
		DialogueLine = await DialogueManager.GetNextDialogueLine(resource, nextId, temporaryGameStates);
	}


	#region Helpers

	private async void UpdateDialogue()
	{
		if (!IsNodeReady())
		{
			await ToSignal(this, DialogueManagerRuntime.DialogueBalloon.SignalName.Ready);
		}

		// Set up the character name
		//characterLabel.Visible = !string.IsNullOrEmpty(dialogueLine.Character);
		//characterLabel.Text = Tr(dialogueLine.Character, "dialogue");

		PlaceBubbleAboveActor();

		SetStyleBox();

		var nextAuto = false;

		// skip dialogue if tag exists
		var tags = dialogueLine.Tags.ToList();
		foreach (var tag in tags)
		{
			switch (tag)
			{
				case DialogueConstants.Tags.NextAuto:
					nextAuto = true;
					break;
				default:
					GD.PrintS("An unknown dialogue tag was set.", tag);
					break;
			}
		}

		// Set up the dialogue
		dialogueLabel.Hide();
		dialogueLabel.Set("dialogue_line", dialogueLine);

		// Set up the responses
		responsesMenu.Hide();
		responsesMenu.Set("responses", dialogueLine.Responses);

		// Type out the text
		balloon.Show();
		willHideBalloon = false;
		dialogueLabel.Show();
		if (!string.IsNullOrEmpty(dialogueLine.Text))
		{
			dialogueLabel.Call("type_out");
			await ToSignal(dialogueLabel, "finished_typing");
		}

		// Wait for input
		if (dialogueLine.Responses.Count > 0)
		{
			balloon.FocusMode = Control.FocusModeEnum.None;
			responsesMenu.Show();
		}
		else if (!string.IsNullOrEmpty(dialogueLine.Time))
		{
			float time = 0f;
			if (!float.TryParse(dialogueLine.Time, out time))
			{
				time = dialogueLine.Text.Length * 0.02f;
			}

			await ToSignal(GetTree().CreateTimer(time), "timeout");
			Next(dialogueLine.NextId);
		}
		else if (nextAuto)
		{
			Next(dialogueLine.NextId);
		}
		else
		{
			isWaitingForInput = true;
			balloon.FocusMode = Control.FocusModeEnum.All;
			balloon.GrabFocus();
		}
	}

	#endregion

	#region Custom Logic

	private const int HeightOffset = 5;
	private const int ActorDirectionOffset = 42;

	private void PlaceBubbleAboveActor()
	{
		var actorModules = GetTree().GetNodesInGroup(NodeGroup.ActorModule);
		Node2D actorSpeaking = null;

		foreach (var actorModule in actorModules)
		{
			var actor = actorModule.GetParentOrNull<Node2D>();
			if (actor == null) continue;

			if (actor.Name == dialogueLine.Character)
			{
				actorSpeaking = actor;
				break;
			}
		}

		if (actorSpeaking != null)
		{
			var actorTransform = GetActorTransformForCanvas(actorSpeaking);

			if (_splitDialogueBalloons)
			{
				switch (dialogueLine.Character)
				{
					case "Oliver":
						//theme = GD.Load<StyleBoxTexture>(OliverThemePath);
						break;
					default:
						//theme = GD.Load<StyleBoxTexture>(DefaultThemePath);
						break;
				}
			}
			else
			{
				var previousScale = Scale;

				Transform = actorTransform;
				Scale = previousScale;

				var actorDirectionOffset = GetActorDirectionOffset(actorSpeaking);

				var xx = Scale.X;
				var xy = actorTransform.X.Y;
				var yx = actorTransform.Y.X;
				var yy = Scale.Y;
				var ox = actorTransform.Origin.X - actorDirectionOffset;
				var oy = actorTransform.Origin.Y - HeightOffset;

				Transform = new Transform2D(xx, xy, yx, yy, ox, oy);
				
				// Transform dialoguePointer
				// Will need to know where actor is in relation to origin so can put the pointer on the right side and orient it correctly
				// Won't scale the pointer, just interested in putting at the base of the balloon
				// dialoguePointer
			}
		}
	}

	private static int GetActorDirectionOffset(Node2D actorSpeaking)
	{
		var actorModule = actorSpeaking.GetNode<ActorModule>("ActorModule");
		var actorDirectionOffset = 0;
		switch (actorModule.Character.LastDirection)
		{
			case Direction.Left:
				actorDirectionOffset = ActorDirectionOffset;
				break;
			case Direction.Right:
				actorDirectionOffset = -ActorDirectionOffset;
				break;
		}

		return actorDirectionOffset;
	}

	private Transform2D GetActorTransformForCanvas(Node2D actor)
	{
		var actorHeight = GetActorHeight(actor);
		var actorCurrentPosition = actor.Position;

		// place actor above itself by its own height
		actor.Position = new Vector2(actor.Position.X, actor.Position.Y - Math.Abs(actorHeight));

		// grab the actor transform 
		var actorTransform = actor.GetGlobalTransformWithCanvas();

		// place it back where it was
		actor.Position = actorCurrentPosition;

		return actorTransform;
	}

	private int GetActorHeight(Node2D actorSpeaking)
	{
		var actorModule = actorSpeaking.GetNode<ActorModule>("ActorModule");
		var rect = actorModule.MainShape.Shape.GetRect();
		return (int)rect.Size.Y;
	}

	private void SetStyleBox()
	{
		StyleBoxTexture theme;
		switch (dialogueLine.Character)
		{
			case "Sasha":
				theme = GD.Load<StyleBoxTexture>(SashaThemePath);
				// change colour of dialoguePointer
				break;
			case "Oliver":
				theme = GD.Load<StyleBoxTexture>(OliverThemePath); // uids not loading
				// change colour of dialoguePointer
				break;
			default:
				theme = GD.Load<StyleBoxTexture>(DefaultThemePath);
				// change colour of dialoguePointer
				break;
		}

		balloon.Theme.SetStylebox("panel", "PanelContainer", theme);
	}

	#endregion

	#region signals

	private void OnMutated(Dictionary _mutation)
	{
		isWaitingForInput = false;
		willHideBalloon = true;
		GetTree().CreateTimer(0.1f).Timeout += () =>
		{
			if (willHideBalloon)
			{
				willHideBalloon = false;
				balloon.Hide();
			}
		};
	}

	#endregion
}
