using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using HoArchive;

namespace Plankton.Rendering.Base
{
    internal abstract class ModelBase : BaseClass
    {
        public ModelBase(TOCEntry asset, ResourcePool resourcePool) : base(asset, resourcePool) { }
        abstract public List<ulong> GetGeometryIDs();
        abstract public List<ulong> GetModelIDs();
        abstract public void UpdateInstanceMatrices();
        public abstract void ApplyInstanceMatrixRecursive(InstanceInfo info, ref uint childAttr, bool onlyupdate);

        public List<Matrix4x4> geomMatrices;
        public List<Matrix4x4> modelInstanceMatrices;

    }
}
