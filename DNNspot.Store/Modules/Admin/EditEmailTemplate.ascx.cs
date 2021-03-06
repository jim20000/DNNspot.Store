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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DNNspot.Store.DataModel;
using WA;

namespace DNNspot.Store.Modules.Admin
{
    public partial class EditEmailTemplate : StoreAdminModuleBase
    {
        protected DotNetNuke.UI.UserControls.TextEditor txtBodyTemplate;

        public override List<AdminBreadcrumbLink> GetBreadcrumbs()
        {
            return new List<AdminBreadcrumbLink>()
               {
                   new AdminBreadcrumbLink() { Text = "Email Templates", Url = StoreUrls.Admin(ModuleDefs.Admin.Views.EmailTemplates) },
                   new AdminBreadcrumbLink() { Text = "Add / Edit Email Template" }
               };
        }        

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                StoreEmailTemplate toLoad = GetStoreEmailTemplateFromQueryString();
                if(toLoad != null)
                {
                    litTemplateName.Text = toLoad.UpToEmailTemplateByEmailTemplateId.NameKey;

                    txtSubjectTemplate.Text = toLoad.SubjectTemplate;
                    txtBodyTemplate.Text = toLoad.BodyTemplate;

                    //EmailNotifier notifier = new EmailNotifier();
                    //rptBodyTokens.DataSource = notifier.GetValidBodyTokens(toLoad.EmailTemplate);
                    //rptBodyTokens.DataBind();
                }
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            StoreEmailTemplate toSave = GetStoreEmailTemplateFromQueryString();
            if (toSave != null)
            {
                toSave.SubjectTemplate = txtSubjectTemplate.Text;
                toSave.BodyTemplate = txtBodyTemplate.Text;

                toSave.Save();

                Response.Redirect(StoreUrls.Admin(ModuleDefs.Admin.Views.EmailTemplates));
            }
        }

        private StoreEmailTemplate GetStoreEmailTemplateFromQueryString()
        {
            short? id = Parser.ToShort(Request.QueryString["id"]);
            StoreEmailTemplate toLoad = new StoreEmailTemplate();
            if(toLoad.LoadByPrimaryKey(StoreContext.CurrentStore.Id.Value, id.Value))
            {
                return toLoad;
            }
            return null;
        }
    }
}