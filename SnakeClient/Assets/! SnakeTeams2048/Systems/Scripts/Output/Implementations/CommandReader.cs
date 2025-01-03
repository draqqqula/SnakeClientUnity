﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Assets.State
{
    public class CommandReader : IMessageReader
    {
        private IEnumerable<ICommandExecutor> Executors;

        [Inject]
        public void Construct(List<ICommandExecutor> executors)
        {
            Executors = executors;
        }

        public void Read(byte[] buffer)
        {
            using var stream = new MemoryStream(buffer);
            using var reader = new BinaryReader(stream);
            while (stream.Position < stream.Length - 1)
            {
                bool executorFound = false;
                foreach (var executor in Executors)
                {
                    if (executor.TryExecute(reader))
                    {
                        executorFound = true;
                        break;
                    }
                }
                if (!executorFound)
                {
                    break;
                }
            }
        }
    }
}
