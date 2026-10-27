using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plankton.Rendering.Shaders;

namespace Plankton.Rendering
{
    public class PrimitiveModelLib
    {

        public PrimitiveModel Cube;
        public PrimitiveModel Camera_16_9;
        public PrimitiveModel Empty;
        public PrimitiveModel Line;
        public PrimitiveModel Sphere;
        public PrimitiveModel Grid;
        public PrimitiveModel WireframeCube;
        public PrimitiveModel WireframeSphere;

        public List<PrimitiveModel> models;
        public ShaderCollection shaders;

        public PrimitiveModelLib()
        {
            string basepath = "Rendering/Shaders/PrimitiveModel/";
            shaders = new ShaderCollection(basepath);

            basepath = "Rendering/Models/";

            Cube = new PrimitiveModel(basepath  + "Cube.obj");
            Camera_16_9 = new PrimitiveModel(basepath + "16_9_Camera.dae");
            Empty = new PrimitiveModel(basepath + "Empty.dae");
            Line = new PrimitiveModel(basepath + "Line.dae");
            Sphere = new PrimitiveModel(basepath + "Sphere.dae");
            Grid = new PrimitiveModel(basepath + "Grid.dae");
            WireframeCube = new PrimitiveModel(basepath + "WireframeCube.dae");
            WireframeSphere = new PrimitiveModel(basepath + "WireframeSphere.dae");

            models = new List<PrimitiveModel> { Cube, Camera_16_9, Empty, Line, Sphere, Grid, WireframeCube, WireframeSphere};
            
            
            /*
            models = new();
            foreach (string path in paths)
            {
                models.Add(new PrimitiveModel(path));
            }
            */
            Debug.debugWindow.AddEntry("PrimitiveModelLib Constructor", "Successfully initiated");
        }

        public void RenderAllSolid()
        {
            foreach (PrimitiveModel model in models)
            {
                model.RenderSolid();
            }
        }

        public void RenderAllNonSolid()
        {
            foreach (PrimitiveModel model in models)
            {
                model.RenderNonSolid();
            }
        }

        public void RenderAll()
        {
            foreach(PrimitiveModel model in models)
            {
                model.Render();
            }
        }

        public void BufferAll()
        {
            foreach(PrimitiveModel model in models)
            {
                model.Buffer();
            }
        }

        public void ClearAll()
        {
            foreach(PrimitiveModel model in models)
            {
                model.Clear();
            }
        }
    }
}
