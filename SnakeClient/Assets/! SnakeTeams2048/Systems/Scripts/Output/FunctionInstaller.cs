using Assets.Input;
using Assets.Input.Writers;
using Assets.Output.Implementations.CommandExecutors;
using Assets.Script.Core.Serialization;
using Assets.Script.Output.Implementations.CommandExecutors;
using Assets.Script.Commands;
using Assets.Script.Commands.Interfaces;
using Assets.State;
using Assets.State.Executors;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Zenject;
using UnityEngine.Tilemaps;

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
    public RespawnWindowUIController RespawnWindowDisplay;
    public AbilityButtonController AbilityButtonController;
    public BonusNotificationUIController BonusNotification;
    public StatisticsDisplayUIController StatisticsDisplay;
    public override void InstallBindings()
    {
        SignalBusInstaller.Install(Container);
        Container.BindInstance(Display);
        Container.BindInstance(CameraBinder);
        Container.BindInstance(MinimapController);
        Container.BindInstance(TimerController);
        Container.BindInstance(ScoreController);
        Container.BindInstance(JoystickBehaviour);
        Container.BindInstance(RespawnWindowDisplay);
        Container.BindInstance(AbilityButtonController);
        Container.BindInstance(BonusNotification);
        Container.BindInterfacesAndSelfTo<CommandManager>().AsSingle();
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
        Container.BindInterfacesAndSelfTo<NotifyPowerUpExecutor>().AsSingle();
        Container.BindInterfacesAndSelfTo<DeclareRuntimeCommandExecutor>().AsSingle();
        Container.BindInterfacesAndSelfTo<RuntimeCommandExecutor>().AsSingle();
        Container.BindInstance(OptionBinder);
        Container.BindInstance(WebSocketController);
        Container.BindInstance(StatisticsDisplay);
    }
}
