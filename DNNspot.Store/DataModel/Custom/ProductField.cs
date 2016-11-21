/*
* This software is licensed under the GNU General Public License, version 2
* You may copy, distribute and modify the software as long as you track changes/dates of in source files and keep all modifications under GPL. You can distribute your application using a GPL library commercially, but you must also provide the source code.

* DNNspot Software (http://www.dnnspot.com)
* Copyright (C) 2013 Atriage Software LLC
* Authors: Kevin Southworth, Matthew Hall, Ryan Doom

* This program is free software; you can redistribute it and/or
* modify it under the terms of the GNU General Public License
* as published by the Free Software Foundation; either version 2
* of the License, or (at your option) any later version.

* This program is distributed in the hope that it will be useful,
* but WITHOUT ANY WARRANTY; without even the implied warranty of
* MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
* GNU General Public License for more details.

* You should have received a copy of the GNU General Public License
* along with this program; if not, write to the Free Software
* Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301, USA.

* Full license viewable here: http://www.gnu.org/licenses/gpl-2.0.txt
*/

/*
===============================================================================
                    EntitySpaces Studio by EntitySpaces, LLC
             Persistence Layer and Business Objects for Microsoft .NET
             EntitySpaces(TM) is a legal trademark of EntitySpaces, LLC
                          http://www.entityspaces.net
===============================================================================
EntitySpaces Version : 2012.1.0930.0
EntitySpaces Driver  : SQL
Date Generated       : 4/12/2013 3:32:33 PM
===============================================================================
*/

using System;
using System.Collections.Generic;
using System.Linq;
using EntitySpaces.Core;
using EntitySpaces.Interfaces;
using EntitySpaces.DynamicQuery;

namespace DNNspot.Store.DataModel
{
	public partial class ProductField : esProductField
	{
		public ProductField()
		{

        }
        public ProductFieldWidgetType WidgetTypeName
        {
            get { return WA.Enum<ProductFieldWidgetType>.TryParseOrDefault(this.WidgetType, ProductFieldWidgetType.Textbox); }
        }

        public static bool SlugExistsForProductField(int productId, string slug)
        {
            ProductFieldQuery q = new ProductFieldQuery();
            q.Select(q.ProductId, q.Slug);
            q.Where(q.ProductId == productId, q.Slug == slug);

            ProductField pf = new ProductField();
            return pf.Load(q);
        }

        public static string GetNextAvailableSlug(int productId, string slug)
        {
            for (int i = 2; i < 100; i++)
            {
                string nextSlug = slug.CreateSpecialSlug(true, 98) + i.ToString();
                if (!SlugExistsForProductField(productId, nextSlug))
                {
                    return nextSlug;
                }
            }
            throw new ApplicationException("Unable to automatically generate the next available slug");
        }

        public bool CanContainChoices
        {
            get
            {
                ProductFieldWidgetType widgetType = WidgetTypeName;
                switch (widgetType)
                {
                    case ProductFieldWidgetType.DropdownList:
                    case ProductFieldWidgetType.CheckboxList:
                    case ProductFieldWidgetType.RadioButtonList:
                        return true;
                    default:
                        return false;
                }
            }
        }

        public IList<ProductFieldChoice> GetChoicesInSortOrder()
        {
            //ProductFieldQuery q = new ProductFieldQuery();
            //q.OrderBy(q.SortOrder.Ascending);

            //ProductFieldCollection collection = new ProductFieldCollection();
            //collection.Load(q);

            //return collection;

            //this.ProductFieldChoiceCollectionByProductFieldId.Sort = ProductFieldChoiceMetadata.PropertyNames.SortOrder + " ASC";

            return this.ProductFieldChoiceCollectionByProductFieldId.AsQueryable().OrderBy(x => x.SortOrder).ToList();
        }
	}
}