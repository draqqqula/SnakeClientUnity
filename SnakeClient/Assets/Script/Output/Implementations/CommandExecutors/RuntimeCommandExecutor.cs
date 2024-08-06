﻿using Assets.Script.Commands.Interfaces;
using Assets.State;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Assets.Script.Output.Implementations.CommandExecutors
{
    internal class RuntimeCommandExecutor : ICommandExecutor
    {
        private ICommandCaller Controller { get; set; }
        [Inject]
        public void Construct(ICommandCaller controller)
        {
            Controller = controller;
        }

        public bool TryExecute(BinaryReader reader)
        {
            if (reader.ReadByte() != 12)
            {
                reader.BaseStream.Position -= 1;
                return false;
            }
            var variableId = reader.ReadByte();
            Debug.Log("Runtime recieved");
            Controller.Execute(variableId, reader);
            return true;
        }
    }
}
