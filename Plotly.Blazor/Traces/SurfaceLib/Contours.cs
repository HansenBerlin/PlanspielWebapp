/*
 * THIS FILE WAS GENERATED BY PLOTLY.BLAZOR.GENERATOR
*/

using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text.Json.Serialization;

namespace Plotly.Blazor.Traces.SurfaceLib
{
    /// <summary>
    ///     The Contours class.
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCode("Plotly.Blazor.Generator", "1.0.0.0")]
    [Serializable]
    public class Contours : IEquatable<Contours>
    {
        /// <summary>
        ///     Gets or sets the X.
        /// </summary>
        [JsonPropertyName(@"x")]
        public Plotly.Blazor.Traces.SurfaceLib.ContoursLib.X X { get; set;} 

        /// <summary>
        ///     Gets or sets the Y.
        /// </summary>
        [JsonPropertyName(@"y")]
        public Plotly.Blazor.Traces.SurfaceLib.ContoursLib.Y Y { get; set;} 

        /// <summary>
        ///     Gets or sets the Z.
        /// </summary>
        [JsonPropertyName(@"z")]
        public Plotly.Blazor.Traces.SurfaceLib.ContoursLib.Z Z { get; set;} 

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            if (!(obj is Contours other)) return false;

            return ReferenceEquals(this, obj) || Equals(other);
        }

        /// <inheritdoc />
        public bool Equals([AllowNull] Contours other)
        {
            if (other == null) return false;
            if (ReferenceEquals(this, other)) return true;

            return 
                (
                    X == other.X &&
                    X != null && other.X != null &&
                    X.Equals(other.X)
                ) && 
                (
                    Y == other.Y &&
                    Y != null && other.Y != null &&
                    Y.Equals(other.Y)
                ) && 
                (
                    Z == other.Z &&
                    Z != null && other.Z != null &&
                    Z.Equals(other.Z)
                );
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            unchecked // Overflow is fine, just wrap
            {
                var hashCode = 41;
                if (X != null) hashCode = hashCode * 59 + X.GetHashCode();
                if (Y != null) hashCode = hashCode * 59 + Y.GetHashCode();
                if (Z != null) hashCode = hashCode * 59 + Z.GetHashCode();
                return hashCode;
            }
        }

        /// <summary>
        ///     Checks for equality of the left Contours and the right Contours.
        /// </summary>
        /// <param name="left">Left Contours.</param>
        /// <param name="right">Right Contours.</param>
        /// <returns>Boolean</returns>
        public static bool operator == (Contours left, Contours right)
        {
            return Equals(left, right);
        }

        /// <summary>
        ///     Checks for inequality of the left Contours and the right Contours.
        /// </summary>
        /// <param name="left">Left Contours.</param>
        /// <param name="right">Right Contours.</param>
        /// <returns>Boolean</returns>
        public static bool operator != (Contours left, Contours right)
        {
            return !Equals(left, right);
        }

        /// <summary>
        ///     Gets a deep copy of this instance.
        /// </summary>
        /// <returns>Contours</returns>
        public Contours DeepClone()
        {
            using var ms = new MemoryStream();
            var formatter = new BinaryFormatter();
            formatter.Serialize(ms, this);
            ms.Position = 0;
            return (Contours) formatter.Deserialize(ms);
        }
    }
}