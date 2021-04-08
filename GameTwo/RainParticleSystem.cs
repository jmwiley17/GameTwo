using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace GameTwo
{
    public class RainParticleSystem : ParticleSystem
    {
        Rectangle _source;

        public bool IsRaining { get; set; } = true;

        public RainParticleSystem(Game game, Rectangle source) : base(game, 2000)
        {
            _source = source;
        }

        protected override void InitializeConstants()
        {
            textureFilename = "drop";
            minNumParticles = 1;
            maxNumParticles = 5;
        }

        protected override void InitializeParticle(ref Particle p, Vector2 where)
        {
            p.Initialize(where, Vector2.UnitY * 260, Vector2.Zero, Color.White, scale: RandomHelper.NextFloat(0.1f, 0.3f), lifetime: 3);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (IsRaining) AddParticles(_source);
        }
    }
}
