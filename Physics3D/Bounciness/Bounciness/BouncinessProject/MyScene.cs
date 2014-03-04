﻿// Copyright (C) 2012-2013 Weekend Game Studio
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to
// deal in the Software without restriction, including without limitation the
// rights to use, copy, modify, merge, publish, distribute, sublicense, and/or
// sell copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL
// THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
// FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS
// IN THE SOFTWARE.

#region Using Statements
using System.Collections.Generic;
using WaveEngine.Common.Graphics;
using WaveEngine.Common.Math;
using WaveEngine.Components;
using WaveEngine.Framework;
using WaveEngine.Framework.Services;
using WaveEngine.Materials;
using WaveEngine.Framework.Graphics;
using WaveEngine.Framework.Physics3D;
using WaveEngine.Components.Graphics3D;
using WaveEngine.Components.Cameras;
#endregion

namespace BouncinessProject
{
    public class MyScene : Scene
    {
        protected override void CreateScene()
        {
            RenderManager.BackgroundColor = Color.CornflowerBlue;
            RenderManager.DebugLines = true;

            FreeCamera camera = new FreeCamera("MainCamera", new Vector3(5, 5, 12), new Vector3(0, 2, 0));
            EntityManager.Add(camera.Entity);
            RenderManager.SetActiveCamera(camera.Entity);

            Entity ground = new Entity("Ground")
                .AddComponent(new Transform3D() { Position = new Vector3(0,-1,0), Scale = new Vector3(10, 1, 10) })
                .AddComponent(new BoxCollider())
                .AddComponent(Model.CreateCube())
                .AddComponent(new RigidBody3D() { IsKinematic = true })
                .AddComponent(new MaterialsMap(new BasicMaterial(Color.Gray) ))
                .AddComponent(new ModelRenderer());

            EntityManager.Add(ground);

            EntityManager.Add(CreateSphere("Sphere1", new Vector3(-3, 6, 0), 0.9f));
            EntityManager.Add(CreateSphere("Sphere2", new Vector3(0, 6, 0), 0.8f));
            EntityManager.Add(CreateSphere("Sphere3", new Vector3(3, 6, 0), 0.5f));
        }

        private Entity CreateSphere(string name, Vector3 position, float restitution)
        {
            Entity primitive = new Entity(name)
                .AddComponent(new Transform3D() { Position = position })
                .AddComponent(new SphereCollider())
                .AddComponent(Model.CreateSphere())
                .AddComponent(new RigidBody3D() { Restitution = restitution })
                .AddComponent(new MaterialsMap( new BasicMaterial(GetRandomColor()) ))
                .AddComponent(new ModelRenderer());

            return primitive;
        }

        private Color GetRandomColor()
        {
            var random = WaveServices.Random;
            return new Color((float)random.NextDouble(), (float)random.NextDouble(), (float)random.NextDouble(), 1f);
        }
    }
}
