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

	using Castle.Rook.Compiler.AST.Util;


	public class ExpressionCollection : LinkedListBase
	{
		private readonly IASTNode owner;

		public ExpressionCollection(IASTNode owner)
		{
			this.owner = owner;
		}

		public int Add(IExpression exp)
		{
			exp.Parent = owner;
			return InnerList.Add(exp);
		}

		protected override void PrepareNode(object value)
		{
			((IExpression) value).Parent = owner;
		}

		public IExpression this [int index]
		{
			get { return InnerList[index] as IExpression; }
		}
	}
}
