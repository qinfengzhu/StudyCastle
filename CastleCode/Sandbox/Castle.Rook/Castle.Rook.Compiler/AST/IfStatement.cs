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
	using System.Collections;

	using Castle.Rook.Compiler.Visitors;

	public enum IfType
	{
		If,
		Unless
	}

	public class IfStatement : AbstractStatement
	{
		private StatementCollection trueStmts;
		private StatementCollection falseStmts;
		private IExpression condition;
		private IfType iftype;

		public IfStatement(IfType iftype)
		{
			trueStmts = new StatementCollection(this);
			falseStmts = new StatementCollection(this);
			this.iftype = iftype;
		}

		public StatementCollection TrueStatements
		{
			get { return trueStmts; }
		}

		public StatementCollection FalseStatements
		{
			get { return falseStmts; }
		}

		public IExpression Condition
		{
			get { return condition; }
			set { condition = value; }
		}

		public override bool Accept(IASTVisitor visitor)
		{
			return visitor.VisitIfStatement(this);
		}
	}
}
