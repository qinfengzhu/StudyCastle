// Copyright 2004-2006 Castle Project - http://www.castleproject.org/
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
//     http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

namespace Castle.Rook.Compiler.AST
{
	using System;

	using Castle.Rook.Compiler.Visitors;


	public class RangeExpression : AbstractExpression
	{
		private readonly IExpression lhs;
		private readonly IExpression rhs;
		private readonly bool inclusive;

		public RangeExpression(IExpression lhs, IExpression rhs, bool inclusive)
		{
			this.lhs = lhs;
			this.rhs = rhs;
			this.inclusive = inclusive;
		}

		public IExpression LeftHandSide
		{
			get { return lhs; }
		}

		public IExpression RightHandSide
		{
			get { return rhs; }
		}

		public bool Inclusive
		{
			get { return inclusive; }
		}

		public override bool Accept(IASTVisitor visitor)
		{
			return visitor.VisitRangeExpression(this);
		}

		public override IExpression Accept(IExpressionAttrVisitor visitor)
		{
			return visitor.VisitRangeExpression(this);
		}
	}
}
