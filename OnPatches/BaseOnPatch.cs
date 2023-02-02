using MonoMod.RuntimeDetour.HookGen;
using System;
using System.Reflection;
using Terraria.ModLoader;

namespace FurgosFargoBossTimer.OnPatches
{
    public abstract class BaseOnPatch : ModSystem
    {
        protected abstract MethodBase method { get; }
        protected abstract Delegate Delegate { get; }

        public override void Load()
        {
            if (method is not null)
                HookEndpointManager.Add(method, Delegate);
        }
        public override void Unload()
        {
            if (method is not null)
                HookEndpointManager.Remove(method, Delegate);
        }
    }
}