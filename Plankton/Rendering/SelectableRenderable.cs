using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace Plankton.Rendering
{
    public abstract class SelectableRenderable
    {
        public bool isInstanced; // Instanced as in "references an in-game geometry". If true, it will be rendered seperately.
        public enumDefaultLightKitType defaultLightKit = enumDefaultLightKitType.NONE; // BSPs for example would set this to Environment
        public virtual ModelInstance GetModelInstance() { return new ModelInstance(); } // Default Implementation
        public virtual Matrix4x4 GetInstanceMatrix() { return Matrix4x4.Identity; }

        public virtual Vector3 GetPosition() { return Vector3.Zero; }
        public virtual void SetPosition(Vector3 pos) { }
        public virtual Vector3 GetRotation() { return Vector3.Zero; }
        public virtual void SetRotation(Vector3 pos) { }
        public virtual Vector3 GetScale() { return Vector3.One; }
        public virtual void SetScale(Vector3 scale) { }

        public virtual void AddRenderInstances(PrimitiveInstance baseinstance, RenderHelper helper) { }
    }
}
