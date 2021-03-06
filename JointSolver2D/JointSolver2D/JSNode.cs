﻿using System.Collections.Generic;

namespace JointSolver2D
{
    public class JSNode
    {
        public Vector2d Position;

        /// <summary>
        /// Set to true if the X direction of this node is restrained.
        /// </summary>
        public bool XRestrained;

        /// <summary>
        /// Set to true if the Y direction of this node is restrained.
        /// </summary>
        public bool YRestrained;

        public HashSet<JSVertex> Vertices = new HashSet<JSVertex>(); //Populated when the nodes are resolved

        /// <summary>
        /// The order of this node. 
        /// </summary>
        public int Number { get; protected internal set; }

        protected internal int EquationNo_Fx => Number * 2;
        protected internal int EquationNo_Fy => Number * 2 + 1;



        /// <summary>
        /// The index of vector x to which Rx of this node corresponds (where the bar/node forces are solved by the system of equations Ax = B)
        /// </summary>
        protected internal int VariableNumber_Rx;

        /// <summary>
        /// The index of vector x to which Ry of this node corresponds (where the bar/node forces are solved by the system of equations Ax = B)
        /// </summary>
        protected internal int VariableNumber_Ry;

        /// <summary>
        /// The external force applied at this node (excluding reactions)
        /// DO NOT SET. This feild is populated from adding a JSPointLoad to the JointAnalysis model.
        /// </summary>
        /// This feild is populated indirectly from adding a JSPointLoad to accomodate future improvements (e.g. bar lineloads).
        public Vector2d AppliedForces;

        /// <summary>
        /// The reaction force at this node (populated after a JointAnalysis).
        /// </summary>
        public Vector2d ReactionResult;

        /// <summary>
        /// The deflection at this node (populated after a JointAnalysis).
        /// </summary>
        public Vector2d Deflection;

        public JSNode(JSNode jSNode)
        {
            Position = jSNode.Position;
            XRestrained = jSNode.XRestrained;
            YRestrained = jSNode.YRestrained;
        }

        public JSNode(Vector2d position, bool xRestrained = false, bool yRestrained = false)
        {
            Position = position;
            XRestrained = xRestrained;
            YRestrained = yRestrained;
        }

        /// <summary>
        /// Resets the variables before they are populated by an analysis run.
        /// </summary>
        /// <param name="newNumber"></param>
        public void Reset(int newNumber = -1)
        {
            Number = newNumber;
            VariableNumber_Rx = -1;
            VariableNumber_Ry = -1;
            AppliedForces = new Vector2d();
            //ReactionResult = new Vector2d(); //These values are set by the Solver so do not need to be initialised to zero
            //Deflection = new Vector2d();
        }

        public override string ToString()
        {
            return $"{nameof(JSNode)} at {Position.ToString_Short()}";
        }
    }

}
