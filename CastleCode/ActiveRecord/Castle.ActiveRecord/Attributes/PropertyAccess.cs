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

namespace Castle.ActiveRecord
{
	using System;
	
	public enum PropertyAccess 
	{
		Property,
		Field,
		FieldCamelcase,
		FieldCamelcaseUnderscore,
		FieldPascalcaseMUnderscore,
		FieldLowercaseUnderscore,
		NosetterCamelcase,
		NosetterCamelcaseUnderscore,
		NosetterPascalcaseMUndersc,
		NosetterLowercaseUnderscore
	}
	
	public class PropertyAccessHelper
	{
		public static string ToString(PropertyAccess access)
		{
			switch (access)
			{
				case PropertyAccess.Property:
					return "property";
				case PropertyAccess.Field:
					return "field";
				case PropertyAccess.FieldCamelcase:
					return "field.camelcase";
				case PropertyAccess.FieldCamelcaseUnderscore:
					return "field.camelcase-underscore";
				case PropertyAccess.FieldPascalcaseMUnderscore:
					return "field.pascalcase-m-underscore";
				case PropertyAccess.FieldLowercaseUnderscore:
					return "field.lowercase-underscore";
				case PropertyAccess.NosetterCamelcase:
					return "nosetter.camelcase";
				case PropertyAccess.NosetterCamelcaseUnderscore:
					return "nosetter.camelcase-underscore";
				case PropertyAccess.NosetterPascalcaseMUndersc:
					return "nosetter.pascalcase-m-underscore";
				case PropertyAccess.NosetterLowercaseUnderscore:
					return "nosetter.lowercase-underscore";
				default:
					throw new InvalidOperationException("Invalid value for PropertyAccess");
			}
		}
	}
}
