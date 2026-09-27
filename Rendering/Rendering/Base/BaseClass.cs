using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HoArchive;

namespace Plankton.Rendering.Base
{
    public abstract class BaseClass
    {
        public TOCEntry asset;
        public List<Action<BaseClass>> OnUpdate = new List<Action<BaseClass>>();
        public ResourcePool resourcePool;

        public BaseClass(TOCEntry asset, ResourcePool resourcePool)
        {
            this.asset = asset;
            asset.OnUpdate.Add(Update);
            this.resourcePool = resourcePool;
        }

        public virtual void Update(TOCEntry asset = null)
        {
            foreach(Action<BaseClass> act in OnUpdate)
            {
                act(this);
            }
        }
        public abstract void UpdateAssociates();
        public abstract void Init();
    }
}
