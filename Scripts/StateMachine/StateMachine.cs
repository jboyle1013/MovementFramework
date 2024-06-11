using Godot;
using System.Collections.Generic;
using System.Threading.Tasks;
using MovementFramework.Scripts.Character;

namespace MovementFramework.Scripts.StateMachine;


/// <summary>
/// Generic state machine. Initializes states and delegates engine callbacks
/// (_physics_process, _unhandled_input) to the active state.
/// </summary>
public partial class StateMachine : Node
{
    /// <summary>
    /// Emitted when transitioning to a new state.
    /// </summary>
    /// <param name="stateName"></param>
    /// <returns></returns>
    [Signal]
    public delegate void TransitionedEventHandler(string stateName);

    /// <summary>
    /// Path to the initial active state. We export it to be able to pick the initial state in the inspector.
    /// </summary>
    [Export]
    public NodePath InitialState;

    /// <summary>
    /// The current active state. At the start of the game, we get the `initial_state`.
    /// </summary>
    public State State;

    public override void _Ready()
    {
        // Use call_deferred to safely wait for the 'ready' signal
        CallDeferred(nameof(InitializeStateMachine));
    }
    public void InitializeStateMachine()
    {
        Node initialStateNode = GetNode(InitialState);
        if (initialStateNode is CharacterState initialCharacterState) 
        {
            State = initialCharacterState;
        }

        else
        {
            GD.PrintErr("Initial state node is not of type State.");
            return;
        }

        // The state machine assigns itself to the State objects' state_machine property.
        foreach (State child in GetChildren())
        {
            child._stateMachine = this;
        }

        State.EnterState();
    }

    /// <summary>
    /// The state machine subscribes to node callbacks and delegates them to the state objects.
    /// </summary>
    /// <param name="inputEvent"></param>
    public override void _UnhandledInput(InputEvent inputEvent)
    {
        // Add null check
        State?.HandleInputs(inputEvent);
    }

    public override void _Process(double delta)
    {
        // Add null check
        State?.PhysicsUpdate((float)delta);
    }

    public override void _PhysicsProcess(double delta)
    {
        // Add null check
        State?.PhysicsUpdate(delta);
    }
    /// <summary>
    /// This function calls the current state's exit() function, then changes the active state,
    /// and calls its enter function.
    /// It optionally takes a `msg` dictionary to pass to the next state's enter() function.
    /// </summary>
    /// <param name="targetStateName"></param>
    /// <param name="message"></param>
    public void TransitionTo(string targetStateName, Dictionary<string, bool> message = null)
    {
        //  Safety check, you could use an assert() here to report an error if the state name is incorrect.
        //  We don't use an assert here to help with code reuse. If you reuse a state in different state machines
        //  but you don't want them all, they won't be able to transition to states that aren't in the scene tree.
        if (!HasNode(targetStateName))
        {
            GD.PrintErr($"State machine does not have a state named '{targetStateName}'.");
            return;
        }

        State.Exit();
        State = GetNode<State>(targetStateName);
        State.EnterState();
        EmitSignal(nameof(Transitioned), State.Name);
    }
}