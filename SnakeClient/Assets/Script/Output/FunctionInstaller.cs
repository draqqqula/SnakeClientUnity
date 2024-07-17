using Assets.Input;
using Assets.Input.Writers;
using Assets.Output.Implementations.CommandExecutors;
using Assets.State;
using Assets.State.Executors;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class FunctionInstaller : MonoInstaller
{
    public MinimapController MinimapController;
    public CameraBinder CameraBinder;
    public FrameDisplay Display;
    public WebSocketController WebSocketController;
    public TimerController TimerController;
    public ScoreController ScoreController;
    public JoystickBehaviour JoystickBehaviour;
    public OptionBinder OptionBinder;
    public RespawnWindowDisplay RespawnWindowDisplay;
    public AbilityButtonController AbilityButtonController;
    public override void InstallBindings()
    {
        Container.BindInstance(Display);
        Container.BindInstance(CameraBinder);
        Container.BindInstance(MinimapController);
        Container.BindInstance(TimerController);
        Container.BindInstance(ScoreController);
        Container.BindInstance(JoystickBehaviour);
        Container.BindInstance(RespawnWindowDisplay);
        Container.BindInstance(AbilityButtonController);
        Container.BindInterfacesAndSelfTo<AbilityCooldownExecutor>().AsSingle();
        Container.BindInterfacesAndSelfTo<OptionInputWriter>().AsSingle();
        Container.BindInterfacesAndSelfTo<AbilityInputWriter>().AsSingle();
        Container.BindInterfacesAndSelfTo<MovementDirectionWriter>().AsSingle();
        Container.BindInterfacesAndSelfTo<EventExecutor>().AsSingle();
        Container.BindInterfacesAndSelfTo<AttachCameraExecutor>().AsSingle();
        Container.BindInterfacesAndSelfTo<MinimapExecutor>().AsSingle();
        Container.BindInterfacesAndSelfTo<TimerExecutor>().AsSingle();
        Container.BindInterfacesAndSelfTo<ScoreExecutor>().AsSingle();
        Container.BindInterfacesAndSelfTo<CommandReader>().AsSingle();
        Container.BindInterfacesAndSelfTo<RespawnExecutor>().AsSingle();
        Container.BindInstance(OptionBinder);
        Container.BindInstance(WebSocketController);
    }
}
