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

namespace NVelocity.Runtime.Parser.Node
{
	using System;
	using NVelocity.Context;

	public class ASTNotNode : SimpleNode
	{
		public ASTNotNode(int id) : base(id)
		{
		}

		public ASTNotNode(Parser p, int id) : base(p, id)
		{
		}

		/// <summary>Accept the visitor. *
		/// </summary>
		public override Object jjtAccept(ParserVisitor visitor, Object data)
		{
			return visitor.visit(this, data);
		}

		public override bool evaluate(InternalContextAdapter context)
		{
			if (jjtGetChild(0).evaluate(context))
				return false;
			else
				return true;
		}

		public override Object Value(InternalContextAdapter context)
		{
			return (evaluate(context) ? false : true);
		}
	}
}