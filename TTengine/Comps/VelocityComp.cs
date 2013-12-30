﻿#region File description

// --------------------------------------------------------------------------------------------------------------------
// <copyright file="VelocityComp.cs" company="GAMADU.COM">
//     Copyright © 2013 GAMADU.COM. All rights reserved.
//
//     Redistribution and use in source and binary forms, with or without modification, are
//     permitted provided that the following conditions are met:
//
//        1. Redistributions of source code must retain the above copyright notice, this list of
//           conditions and the following disclaimer.
//
//        2. Redistributions in binary form must reproduce the above copyright notice, this list
//           of conditions and the following disclaimer in the documentation and/or other materials
//           provided with the distribution.
//
//     THIS SOFTWARE IS PROVIDED BY GAMADU.COM 'AS IS' AND ANY EXPRESS OR IMPLIED
//     WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
//     FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL GAMADU.COM OR
//     CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR
//     CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR
//     SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON
//     ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING
//     NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF
//     ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
//
//     The views and conclusions contained in the software and documentation are those of the
//     authors and should not be interpreted as representing official policies, either expressed
//     or implied, of GAMADU.COM.
// </copyright>
// <summary>
//   The velocity.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
#endregion File description

namespace TTengine.Comps
{
    using System;
    using Microsoft.Xna.Framework;
    using Artemis.Interface;

    /// <summary>Velocity and acceleration of an Entity, contributing to its position change</summary>
    public class VelocityComp : IComponent
    {
        /// <summary>To radians.</summary>
        private const float ToRadians = (float)(Math.PI / 180.0);

        /// <summary>Initializes a new instance of the <see cref="VelocityComp" /> class.</summary>
        public VelocityComp()
            : this(0.0f, 0.0f, 0.0f)
        {
        }

        /// <summary>Initializes a new instance of the <see cref="VelocityComp" /> class.</summary>
        /// <param name="velocity">The velocity.</param>
        public VelocityComp(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        /// <summary>Gets or sets the velocity.</summary>
        /// <value>The velocity.</value>
        public Vector3 Velocity
        {
            get
            {
                return new Vector3(this.X, this.Y, this.Z);
            }

            set
            {
                this.X = value.X;
                this.Y = value.Y;
                this.Z = value.Z;
            }
        }

        /// <summary>Gets or sets the velocity X and Y components.</summary>
        /// <value>The 2D velocity.</value>
        public Vector2 Velocity2D
        {
            get
            {
                return new Vector2(this.X, this.Y);
            }

            set
            {
                this.X = value.X;
                this.Y = value.Y;
            }
        }

        /*
        /// <summary>Initializes a new instance of the <see cref="VelocityComp" /> class.</summary>
        /// <param name="velocity">The velocity.</param>
        /// <param name="angle">The angle.</param>
        public VelocityComp(float velocity, float angle)
        {
            this.Speed = velocity;
            this.Angle = angle;
        }
         */

        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }

        /*
        /// <summary>Gets or sets the angle.</summary>
        /// <value>The angle.</value>
        public float Angle { get; set; }

        /// <summary>Gets the angle as radians.</summary>
        /// <value>The angle as radians.</value>
        public float AngleAsRadians
        {
            get
            {
                return this.Angle * ToRadians;
            }
        }
        */

        /// <summary>Gets or sets the speed.</summary>
        /// <value>The speed.</value>
        public float Speed
        {
            get
            {
                return (float)Math.Sqrt(X * X + Y * Y + Z * Z);
            }
            set
            {
                // TODO
                throw new NotImplementedException();
            }
        }

        /*
        /// <summary>The add angle.</summary>
        /// <param name="angle">The angle.</param>
        public void AddAngle(float angle)
        {
            this.Angle = (this.Angle + angle) % 360;
        }
         */
    }
}