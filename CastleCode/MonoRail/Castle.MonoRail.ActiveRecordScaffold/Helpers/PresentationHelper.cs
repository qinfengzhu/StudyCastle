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

namespace Castle.MonoRail.ActiveRecordScaffold.Helpers
{
	using System;
	using System.Collections;

	using Castle.ActiveRecord.Framework.Internal;
	using Castle.MonoRail.Framework.Helpers;

	public class PresentationHelper : AbstractHelper
	{
		public String StartAlternateTR(int index, String styleClass, String altStyleClass)
		{
			return String.Format("<tr class=\"{0}\">", index % 2 == 0 ? styleClass : altStyleClass );
		}

		public String BestAlignFor(Type type)
		{
			if (type == typeof(String))
			{
				return "left";
			}

			return "center";
		}

		public String LinkToBack(String text, IDictionary attributes)
		{
			return String.Format( "<a href=\"javascript:history.go(-1);\" {1}>{0}</a>", 
				text, GetAttributes(attributes) );
		}

		public String LinkToList(ActiveRecordModel model, String text, IDictionary attributes)
		{
			return String.Format( "<a href=\"list{0}.{1}\" {3}>{2}</a>", model.Type.Name, 
				Controller.Context.UrlInfo.Extension, text, GetAttributes(attributes) );
		}

		public String LinkToNew(ActiveRecordModel model, String text, IDictionary attributes)
		{
			return String.Format( "<a href=\"new{0}.{1}\" {3}>{2}</a>", model.Type.Name, 
				Controller.Context.UrlInfo.Extension, text, GetAttributes(attributes) );
		}

		public String LinkToEdit(ActiveRecordModel model, String text, object key, IDictionary attributes)
		{
			return String.Format( "<a href=\"edit{0}.{1}?id={4}\" {3}>{2}</a>", model.Type.Name, 
				Controller.Context.UrlInfo.Extension, text, GetAttributes(attributes), key );
		}

		public String LinkToConfirm(ActiveRecordModel model, String text, object key, IDictionary attributes)
		{
			return String.Format( "<a href=\"confirm{0}.{1}?id={4}\" {3}>{2}</a>", model.Type.Name, 
				Controller.Context.UrlInfo.Extension, text, GetAttributes(attributes), key );
		}

		public String LinkToRemove(ActiveRecordModel model, String text, object key, IDictionary attributes)
		{
			return String.Format( "<a href=\"remove{0}.{1}?id={4}\" {3}>{2}</a>", model.Type.Name, 
				Controller.Context.UrlInfo.Extension, text, GetAttributes(attributes), key );
		}

        public String Form(String action, String id, String method, String onSubmit)
        {
            HtmlHelper hh = new HtmlHelper();

            string fullAction = string.Format("{0}.{1}", action, Controller.Context.UrlInfo.Extension);

            return hh.Form(fullAction, id, method, onSubmit);
        }
	}
}
